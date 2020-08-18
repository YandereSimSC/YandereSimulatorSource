using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200053B RID: 1339
	public abstract class Path : IPathInternals
	{
		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06002386 RID: 9094 RVA: 0x00194DEE File Offset: 0x00192FEE
		// (set) Token: 0x06002387 RID: 9095 RVA: 0x00194DF6 File Offset: 0x00192FF6
		internal PathState PipelineState { get; private set; }

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06002388 RID: 9096 RVA: 0x00194DFF File Offset: 0x00192FFF
		// (set) Token: 0x06002389 RID: 9097 RVA: 0x00194E08 File Offset: 0x00193008
		public PathCompleteState CompleteState
		{
			get
			{
				return this.completeState;
			}
			protected set
			{
				object obj = this.stateLock;
				lock (obj)
				{
					if (this.completeState != PathCompleteState.Error)
					{
						this.completeState = value;
					}
				}
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x0600238A RID: 9098 RVA: 0x00194E54 File Offset: 0x00193054
		public bool error
		{
			get
			{
				return this.CompleteState == PathCompleteState.Error;
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x0600238B RID: 9099 RVA: 0x00194E5F File Offset: 0x0019305F
		// (set) Token: 0x0600238C RID: 9100 RVA: 0x00194E67 File Offset: 0x00193067
		public string errorLog { get; private set; }

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x0600238D RID: 9101 RVA: 0x00194E70 File Offset: 0x00193070
		// (set) Token: 0x0600238E RID: 9102 RVA: 0x00194E78 File Offset: 0x00193078
		bool IPathInternals.Pooled { get; set; }

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x0600238F RID: 9103 RVA: 0x0002D171 File Offset: 0x0002B371
		[Obsolete("Has been renamed to 'Pooled' to use more widely underestood terminology", true)]
		internal bool recycled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06002390 RID: 9104 RVA: 0x00194E81 File Offset: 0x00193081
		// (set) Token: 0x06002391 RID: 9105 RVA: 0x00194E89 File Offset: 0x00193089
		internal ushort pathID { get; private set; }

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06002392 RID: 9106 RVA: 0x00194E92 File Offset: 0x00193092
		// (set) Token: 0x06002393 RID: 9107 RVA: 0x00194E9A File Offset: 0x0019309A
		public int[] tagPenalties
		{
			get
			{
				return this.manualTagPenalties;
			}
			set
			{
				if (value == null || value.Length != 32)
				{
					this.manualTagPenalties = null;
					this.internalTagPenalties = Path.ZeroTagPenalties;
					return;
				}
				this.manualTagPenalties = value;
				this.internalTagPenalties = value;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06002394 RID: 9108 RVA: 0x0002D171 File Offset: 0x0002B371
		internal virtual bool FloodingPath
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002395 RID: 9109 RVA: 0x00194EC8 File Offset: 0x001930C8
		public float GetTotalLength()
		{
			if (this.vectorPath == null)
			{
				return float.PositiveInfinity;
			}
			float num = 0f;
			for (int i = 0; i < this.vectorPath.Count - 1; i++)
			{
				num += Vector3.Distance(this.vectorPath[i], this.vectorPath[i + 1]);
			}
			return num;
		}

		// Token: 0x06002396 RID: 9110 RVA: 0x00194F24 File Offset: 0x00193124
		public IEnumerator WaitForPath()
		{
			if (this.PipelineState == PathState.Created)
			{
				throw new InvalidOperationException("This path has not been started yet");
			}
			while (this.PipelineState != PathState.Returned)
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x06002397 RID: 9111 RVA: 0x00002DA6 File Offset: 0x00000FA6
		public void BlockUntilCalculated()
		{
			AstarPath.BlockUntilCalculated(this);
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x00194F34 File Offset: 0x00193134
		internal uint CalculateHScore(GraphNode node)
		{
			switch (this.heuristic)
			{
			case Heuristic.Manhattan:
			{
				Int3 position = node.position;
				uint num = (uint)((float)(Math.Abs(this.hTarget.x - position.x) + Math.Abs(this.hTarget.y - position.y) + Math.Abs(this.hTarget.z - position.z)) * this.heuristicScale);
				if (this.hTargetNode != null)
				{
					num = Math.Max(num, AstarPath.active.euclideanEmbedding.GetHeuristic(node.NodeIndex, this.hTargetNode.NodeIndex));
				}
				return num;
			}
			case Heuristic.DiagonalManhattan:
			{
				Int3 @int = this.GetHTarget() - node.position;
				@int.x = Math.Abs(@int.x);
				@int.y = Math.Abs(@int.y);
				@int.z = Math.Abs(@int.z);
				int num2 = Math.Min(@int.x, @int.z);
				int num3 = Math.Max(@int.x, @int.z);
				uint num = (uint)((float)(14 * num2 / 10 + (num3 - num2) + @int.y) * this.heuristicScale);
				if (this.hTargetNode != null)
				{
					num = Math.Max(num, AstarPath.active.euclideanEmbedding.GetHeuristic(node.NodeIndex, this.hTargetNode.NodeIndex));
				}
				return num;
			}
			case Heuristic.Euclidean:
			{
				uint num = (uint)((float)(this.GetHTarget() - node.position).costMagnitude * this.heuristicScale);
				if (this.hTargetNode != null)
				{
					num = Math.Max(num, AstarPath.active.euclideanEmbedding.GetHeuristic(node.NodeIndex, this.hTargetNode.NodeIndex));
				}
				return num;
			}
			default:
				return 0u;
			}
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x001950FC File Offset: 0x001932FC
		internal uint GetTagPenalty(int tag)
		{
			return (uint)this.internalTagPenalties[tag];
		}

		// Token: 0x0600239A RID: 9114 RVA: 0x00195106 File Offset: 0x00193306
		internal Int3 GetHTarget()
		{
			return this.hTarget;
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x0019510E File Offset: 0x0019330E
		internal bool CanTraverse(GraphNode node)
		{
			if (this.traversalProvider != null)
			{
				return this.traversalProvider.CanTraverse(this, node);
			}
			return node.Walkable && (this.enabledTags >> (int)node.Tag & 1) != 0;
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x00195145 File Offset: 0x00193345
		internal uint GetTraversalCost(GraphNode node)
		{
			if (this.traversalProvider != null)
			{
				return this.traversalProvider.GetTraversalCost(this, node);
			}
			return this.GetTagPenalty((int)node.Tag) + node.Penalty;
		}

		// Token: 0x0600239D RID: 9117 RVA: 0x00195170 File Offset: 0x00193370
		internal virtual uint GetConnectionSpecialCost(GraphNode a, GraphNode b, uint currentCost)
		{
			return currentCost;
		}

		// Token: 0x0600239E RID: 9118 RVA: 0x00195173 File Offset: 0x00193373
		public bool IsDone()
		{
			return this.CompleteState > PathCompleteState.NotCalculated;
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x00195180 File Offset: 0x00193380
		void IPathInternals.AdvanceState(PathState s)
		{
			object obj = this.stateLock;
			lock (obj)
			{
				this.PipelineState = (PathState)Math.Max((int)this.PipelineState, (int)s);
			}
		}

		// Token: 0x060023A0 RID: 9120 RVA: 0x001951CC File Offset: 0x001933CC
		[Obsolete("Use the 'PipelineState' property instead")]
		public PathState GetState()
		{
			return this.PipelineState;
		}

		// Token: 0x060023A1 RID: 9121 RVA: 0x001951D4 File Offset: 0x001933D4
		internal void FailWithError(string msg)
		{
			this.Error();
			if (this.errorLog != "")
			{
				this.errorLog = this.errorLog + "\n" + msg;
				return;
			}
			this.errorLog = msg;
		}

		// Token: 0x060023A2 RID: 9122 RVA: 0x0019520D File Offset: 0x0019340D
		[Obsolete("Use FailWithError instead")]
		internal void LogError(string msg)
		{
			this.Log(msg);
		}

		// Token: 0x060023A3 RID: 9123 RVA: 0x00195216 File Offset: 0x00193416
		[Obsolete("Use FailWithError instead")]
		internal void Log(string msg)
		{
			this.errorLog += msg;
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x0019522A File Offset: 0x0019342A
		public void Error()
		{
			this.CompleteState = PathCompleteState.Error;
		}

		// Token: 0x060023A5 RID: 9125 RVA: 0x00195234 File Offset: 0x00193434
		private void ErrorCheck()
		{
			if (!this.hasBeenReset)
			{
				this.FailWithError("Please use the static Construct function for creating paths, do not use the normal constructors.");
			}
			if (((IPathInternals)this).Pooled)
			{
				this.FailWithError("The path is currently in a path pool. Are you sending the path for calculation twice?");
			}
			if (this.pathHandler == null)
			{
				this.FailWithError("Field pathHandler is not set. Please report this bug.");
			}
			if (this.PipelineState > PathState.Processing)
			{
				this.FailWithError("This path has already been processed. Do not request a path with the same path object twice.");
			}
		}

		// Token: 0x060023A6 RID: 9126 RVA: 0x0019528E File Offset: 0x0019348E
		protected virtual void OnEnterPool()
		{
			if (this.vectorPath != null)
			{
				ListPool<Vector3>.Release(ref this.vectorPath);
			}
			if (this.path != null)
			{
				ListPool<GraphNode>.Release(ref this.path);
			}
			this.callback = null;
			this.immediateCallback = null;
			this.traversalProvider = null;
		}

		// Token: 0x060023A7 RID: 9127 RVA: 0x001952CC File Offset: 0x001934CC
		protected virtual void Reset()
		{
			if (AstarPath.active == null)
			{
				throw new NullReferenceException("No AstarPath object found in the scene. Make sure there is one or do not create paths in Awake");
			}
			this.hasBeenReset = true;
			this.PipelineState = PathState.Created;
			this.releasedNotSilent = false;
			this.pathHandler = null;
			this.callback = null;
			this.immediateCallback = null;
			this.errorLog = "";
			this.completeState = PathCompleteState.NotCalculated;
			this.path = ListPool<GraphNode>.Claim();
			this.vectorPath = ListPool<Vector3>.Claim();
			this.currentR = null;
			this.duration = 0f;
			this.searchedNodes = 0;
			this.nnConstraint = PathNNConstraint.Default;
			this.next = null;
			this.heuristic = AstarPath.active.heuristic;
			this.heuristicScale = AstarPath.active.heuristicScale;
			this.enabledTags = -1;
			this.tagPenalties = null;
			this.pathID = AstarPath.active.GetNextPathID();
			this.hTarget = Int3.zero;
			this.hTargetNode = null;
			this.traversalProvider = null;
		}

		// Token: 0x060023A8 RID: 9128 RVA: 0x001953C0 File Offset: 0x001935C0
		public void Claim(object o)
		{
			if (o == null)
			{
				throw new ArgumentNullException("o");
			}
			for (int i = 0; i < this.claimed.Count; i++)
			{
				if (this.claimed[i] == o)
				{
					throw new ArgumentException("You have already claimed the path with that object (" + o + "). Are you claiming the path with the same object twice?");
				}
			}
			this.claimed.Add(o);
		}

		// Token: 0x060023A9 RID: 9129 RVA: 0x00195422 File Offset: 0x00193622
		[Obsolete("Use Release(o, true) instead")]
		internal void ReleaseSilent(object o)
		{
			this.Release(o, true);
		}

		// Token: 0x060023AA RID: 9130 RVA: 0x0019542C File Offset: 0x0019362C
		public void Release(object o, bool silent = false)
		{
			if (o == null)
			{
				throw new ArgumentNullException("o");
			}
			for (int i = 0; i < this.claimed.Count; i++)
			{
				if (this.claimed[i] == o)
				{
					this.claimed.RemoveAt(i);
					if (!silent)
					{
						this.releasedNotSilent = true;
					}
					if (this.claimed.Count == 0 && this.releasedNotSilent)
					{
						PathPool.Pool(this);
					}
					return;
				}
			}
			if (this.claimed.Count == 0)
			{
				throw new ArgumentException("You are releasing a path which is not claimed at all (most likely it has been pooled already). Are you releasing the path with the same object (" + o + ") twice?\nCheck out the documentation on path pooling for help.");
			}
			throw new ArgumentException("You are releasing a path which has not been claimed with this object (" + o + "). Are you releasing the path with the same object twice?\nCheck out the documentation on path pooling for help.");
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x001954D8 File Offset: 0x001936D8
		protected virtual void Trace(PathNode from)
		{
			PathNode pathNode = from;
			int num = 0;
			while (pathNode != null)
			{
				pathNode = pathNode.parent;
				num++;
				if (num > 2048)
				{
					Debug.LogWarning("Infinite loop? >2048 node path. Remove this message if you really have that long paths (Path.cs, Trace method)");
					break;
				}
			}
			if (this.path.Capacity < num)
			{
				this.path.Capacity = num;
			}
			if (this.vectorPath.Capacity < num)
			{
				this.vectorPath.Capacity = num;
			}
			pathNode = from;
			for (int i = 0; i < num; i++)
			{
				this.path.Add(pathNode.node);
				pathNode = pathNode.parent;
			}
			int num2 = num / 2;
			for (int j = 0; j < num2; j++)
			{
				GraphNode value = this.path[j];
				this.path[j] = this.path[num - j - 1];
				this.path[num - j - 1] = value;
			}
			for (int k = 0; k < num; k++)
			{
				this.vectorPath.Add((Vector3)this.path[k].position);
			}
		}

		// Token: 0x060023AC RID: 9132 RVA: 0x001955F0 File Offset: 0x001937F0
		protected void DebugStringPrefix(PathLog logMode, StringBuilder text)
		{
			text.Append(this.error ? "Path Failed : " : "Path Completed : ");
			text.Append("Computation Time ");
			text.Append(this.duration.ToString((logMode == PathLog.Heavy) ? "0.000 ms " : "0.00 ms "));
			text.Append("Searched Nodes ").Append(this.searchedNodes);
			if (!this.error)
			{
				text.Append(" Path Length ");
				text.Append((this.path == null) ? "Null" : this.path.Count.ToString());
			}
		}

		// Token: 0x060023AD RID: 9133 RVA: 0x0019569C File Offset: 0x0019389C
		protected void DebugStringSuffix(PathLog logMode, StringBuilder text)
		{
			if (this.error)
			{
				text.Append("\nError: ").Append(this.errorLog);
			}
			if (logMode == PathLog.Heavy && !AstarPath.active.IsUsingMultithreading)
			{
				text.Append("\nCallback references ");
				if (this.callback != null)
				{
					text.Append(this.callback.Target.GetType().FullName).AppendLine();
				}
				else
				{
					text.AppendLine("NULL");
				}
			}
			text.Append("\nPath Number ").Append(this.pathID).Append(" (unique id)");
		}

		// Token: 0x060023AE RID: 9134 RVA: 0x0019573C File Offset: 0x0019393C
		internal virtual string DebugString(PathLog logMode)
		{
			if (logMode == PathLog.None || (!this.error && logMode == PathLog.OnlyErrors))
			{
				return "";
			}
			StringBuilder debugStringBuilder = this.pathHandler.DebugStringBuilder;
			debugStringBuilder.Length = 0;
			this.DebugStringPrefix(logMode, debugStringBuilder);
			this.DebugStringSuffix(logMode, debugStringBuilder);
			return debugStringBuilder.ToString();
		}

		// Token: 0x060023AF RID: 9135 RVA: 0x00195787 File Offset: 0x00193987
		protected virtual void ReturnPath()
		{
			if (this.callback != null)
			{
				this.callback(this);
			}
		}

		// Token: 0x060023B0 RID: 9136 RVA: 0x001957A0 File Offset: 0x001939A0
		protected void PrepareBase(PathHandler pathHandler)
		{
			if (pathHandler.PathID > this.pathID)
			{
				pathHandler.ClearPathIDs();
			}
			this.pathHandler = pathHandler;
			pathHandler.InitializeForPath(this);
			if (this.internalTagPenalties == null || this.internalTagPenalties.Length != 32)
			{
				this.internalTagPenalties = Path.ZeroTagPenalties;
			}
			try
			{
				this.ErrorCheck();
			}
			catch (Exception ex)
			{
				this.FailWithError(ex.Message);
			}
		}

		// Token: 0x060023B1 RID: 9137
		protected abstract void Prepare();

		// Token: 0x060023B2 RID: 9138 RVA: 0x00002ACE File Offset: 0x00000CCE
		protected virtual void Cleanup()
		{
		}

		// Token: 0x060023B3 RID: 9139
		protected abstract void Initialize();

		// Token: 0x060023B4 RID: 9140
		protected abstract void CalculateStep(long targetTick);

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x060023B5 RID: 9141 RVA: 0x00195818 File Offset: 0x00193A18
		PathHandler IPathInternals.PathHandler
		{
			get
			{
				return this.pathHandler;
			}
		}

		// Token: 0x060023B6 RID: 9142 RVA: 0x00195820 File Offset: 0x00193A20
		void IPathInternals.OnEnterPool()
		{
			this.OnEnterPool();
		}

		// Token: 0x060023B7 RID: 9143 RVA: 0x00195828 File Offset: 0x00193A28
		void IPathInternals.Reset()
		{
			this.Reset();
		}

		// Token: 0x060023B8 RID: 9144 RVA: 0x00195830 File Offset: 0x00193A30
		void IPathInternals.ReturnPath()
		{
			this.ReturnPath();
		}

		// Token: 0x060023B9 RID: 9145 RVA: 0x00195838 File Offset: 0x00193A38
		void IPathInternals.PrepareBase(PathHandler handler)
		{
			this.PrepareBase(handler);
		}

		// Token: 0x060023BA RID: 9146 RVA: 0x00195841 File Offset: 0x00193A41
		void IPathInternals.Prepare()
		{
			this.Prepare();
		}

		// Token: 0x060023BB RID: 9147 RVA: 0x00195849 File Offset: 0x00193A49
		void IPathInternals.Cleanup()
		{
			this.Cleanup();
		}

		// Token: 0x060023BC RID: 9148 RVA: 0x00195851 File Offset: 0x00193A51
		void IPathInternals.Initialize()
		{
			this.Initialize();
		}

		// Token: 0x060023BD RID: 9149 RVA: 0x00195859 File Offset: 0x00193A59
		void IPathInternals.CalculateStep(long targetTick)
		{
			this.CalculateStep(targetTick);
		}

		// Token: 0x04003F72 RID: 16242
		protected PathHandler pathHandler;

		// Token: 0x04003F73 RID: 16243
		public OnPathDelegate callback;

		// Token: 0x04003F74 RID: 16244
		public OnPathDelegate immediateCallback;

		// Token: 0x04003F76 RID: 16246
		private object stateLock = new object();

		// Token: 0x04003F77 RID: 16247
		public ITraversalProvider traversalProvider;

		// Token: 0x04003F78 RID: 16248
		protected PathCompleteState completeState;

		// Token: 0x04003F7A RID: 16250
		public List<GraphNode> path;

		// Token: 0x04003F7B RID: 16251
		public List<Vector3> vectorPath;

		// Token: 0x04003F7C RID: 16252
		protected PathNode currentR;

		// Token: 0x04003F7D RID: 16253
		internal float duration;

		// Token: 0x04003F7E RID: 16254
		internal int searchedNodes;

		// Token: 0x04003F80 RID: 16256
		protected bool hasBeenReset;

		// Token: 0x04003F81 RID: 16257
		public NNConstraint nnConstraint = PathNNConstraint.Default;

		// Token: 0x04003F82 RID: 16258
		internal Path next;

		// Token: 0x04003F83 RID: 16259
		public Heuristic heuristic;

		// Token: 0x04003F84 RID: 16260
		public float heuristicScale = 1f;

		// Token: 0x04003F86 RID: 16262
		protected GraphNode hTargetNode;

		// Token: 0x04003F87 RID: 16263
		protected Int3 hTarget;

		// Token: 0x04003F88 RID: 16264
		public int enabledTags = -1;

		// Token: 0x04003F89 RID: 16265
		private static readonly int[] ZeroTagPenalties = new int[32];

		// Token: 0x04003F8A RID: 16266
		protected int[] internalTagPenalties;

		// Token: 0x04003F8B RID: 16267
		protected int[] manualTagPenalties;

		// Token: 0x04003F8C RID: 16268
		private List<object> claimed = new List<object>();

		// Token: 0x04003F8D RID: 16269
		private bool releasedNotSilent;
	}
}
