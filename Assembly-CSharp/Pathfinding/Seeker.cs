using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000519 RID: 1305
	[AddComponentMenu("Pathfinding/Seeker")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_seeker.php")]
	public class Seeker : VersionedMonoBehaviour
	{
		// Token: 0x0600215C RID: 8540 RVA: 0x0018A478 File Offset: 0x00188678
		public Seeker()
		{
			this.onPathDelegate = new OnPathDelegate(this.OnPathComplete);
			this.onPartialPathDelegate = new OnPathDelegate(this.OnPartialPathComplete);
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x0018A4E7 File Offset: 0x001886E7
		protected override void Awake()
		{
			base.Awake();
			this.startEndModifier.Awake(this);
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x0018A4FB File Offset: 0x001886FB
		public Path GetCurrentPath()
		{
			return this.path;
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x0018A503 File Offset: 0x00188703
		public void CancelCurrentPathRequest(bool pool = true)
		{
			if (!this.IsDone())
			{
				this.path.FailWithError("Canceled by script (Seeker.CancelCurrentPathRequest)");
				if (pool)
				{
					this.path.Claim(this.path);
					this.path.Release(this.path, false);
				}
			}
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x0018A543 File Offset: 0x00188743
		public void OnDestroy()
		{
			this.ReleaseClaimedPath();
			this.startEndModifier.OnDestroy(this);
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x0018A557 File Offset: 0x00188757
		public void ReleaseClaimedPath()
		{
			if (this.prevPath != null)
			{
				this.prevPath.Release(this, true);
				this.prevPath = null;
			}
		}

		// Token: 0x06002162 RID: 8546 RVA: 0x0018A575 File Offset: 0x00188775
		public void RegisterModifier(IPathModifier modifier)
		{
			this.modifiers.Add(modifier);
			this.modifiers.Sort((IPathModifier a, IPathModifier b) => a.Order.CompareTo(b.Order));
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x0018A5AD File Offset: 0x001887AD
		public void DeregisterModifier(IPathModifier modifier)
		{
			this.modifiers.Remove(modifier);
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x0018A5BC File Offset: 0x001887BC
		public void PostProcess(Path path)
		{
			this.RunModifiers(Seeker.ModifierPass.PostProcess, path);
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x0018A5C8 File Offset: 0x001887C8
		public void RunModifiers(Seeker.ModifierPass pass, Path path)
		{
			if (pass == Seeker.ModifierPass.PreProcess)
			{
				if (this.preProcessPath != null)
				{
					this.preProcessPath(path);
				}
				for (int i = 0; i < this.modifiers.Count; i++)
				{
					this.modifiers[i].PreProcess(path);
				}
				return;
			}
			if (pass == Seeker.ModifierPass.PostProcess)
			{
				if (this.postProcessPath != null)
				{
					this.postProcessPath(path);
				}
				for (int j = 0; j < this.modifiers.Count; j++)
				{
					this.modifiers[j].Apply(path);
				}
			}
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x0018A655 File Offset: 0x00188855
		public bool IsDone()
		{
			return this.path == null || this.path.PipelineState >= PathState.Returned;
		}

		// Token: 0x06002167 RID: 8551 RVA: 0x0018A672 File Offset: 0x00188872
		private void OnPathComplete(Path path)
		{
			this.OnPathComplete(path, true, true);
		}

		// Token: 0x06002168 RID: 8552 RVA: 0x0018A680 File Offset: 0x00188880
		private void OnPathComplete(Path p, bool runModifiers, bool sendCallbacks)
		{
			if (p != null && p != this.path && sendCallbacks)
			{
				return;
			}
			if (this == null || p == null || p != this.path)
			{
				return;
			}
			if (!this.path.error && runModifiers)
			{
				this.RunModifiers(Seeker.ModifierPass.PostProcess, this.path);
			}
			if (sendCallbacks)
			{
				p.Claim(this);
				this.lastCompletedNodePath = p.path;
				this.lastCompletedVectorPath = p.vectorPath;
				if (this.tmpPathCallback != null)
				{
					this.tmpPathCallback(p);
				}
				if (this.pathCallback != null)
				{
					this.pathCallback(p);
				}
				if (this.prevPath != null)
				{
					this.prevPath.Release(this, true);
				}
				this.prevPath = p;
				if (!this.drawGizmos)
				{
					this.ReleaseClaimedPath();
				}
			}
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x0018A74D File Offset: 0x0018894D
		private void OnPartialPathComplete(Path p)
		{
			this.OnPathComplete(p, true, false);
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x0018A758 File Offset: 0x00188958
		private void OnMultiPathComplete(Path p)
		{
			this.OnPathComplete(p, false, true);
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x0018A763 File Offset: 0x00188963
		[Obsolete("Use ABPath.Construct(start, end, null) instead")]
		public ABPath GetNewPath(Vector3 start, Vector3 end)
		{
			return ABPath.Construct(start, end, null);
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x0018A76D File Offset: 0x0018896D
		public Path StartPath(Vector3 start, Vector3 end)
		{
			return this.StartPath(start, end, null);
		}

		// Token: 0x0600216D RID: 8557 RVA: 0x0018A778 File Offset: 0x00188978
		public Path StartPath(Vector3 start, Vector3 end, OnPathDelegate callback)
		{
			return this.StartPath(ABPath.Construct(start, end, null), callback);
		}

		// Token: 0x0600216E RID: 8558 RVA: 0x0018A789 File Offset: 0x00188989
		public Path StartPath(Vector3 start, Vector3 end, OnPathDelegate callback, int graphMask)
		{
			return this.StartPath(ABPath.Construct(start, end, null), callback, graphMask);
		}

		// Token: 0x0600216F RID: 8559 RVA: 0x0018A79C File Offset: 0x0018899C
		public Path StartPath(Path p, OnPathDelegate callback = null)
		{
			if (p.nnConstraint.graphMask == -1)
			{
				p.nnConstraint.graphMask = this.graphMask;
			}
			this.StartPathInternal(p, callback);
			return p;
		}

		// Token: 0x06002170 RID: 8560 RVA: 0x0018A7C6 File Offset: 0x001889C6
		public Path StartPath(Path p, OnPathDelegate callback, int graphMask)
		{
			p.nnConstraint.graphMask = graphMask;
			this.StartPathInternal(p, callback);
			return p;
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x0018A7E0 File Offset: 0x001889E0
		private void StartPathInternal(Path p, OnPathDelegate callback)
		{
			MultiTargetPath multiTargetPath = p as MultiTargetPath;
			if (multiTargetPath != null)
			{
				OnPathDelegate[] array = new OnPathDelegate[multiTargetPath.targetPoints.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.onPartialPathDelegate;
				}
				multiTargetPath.callbacks = array;
				p.callback = (OnPathDelegate)Delegate.Combine(p.callback, new OnPathDelegate(this.OnMultiPathComplete));
			}
			else
			{
				p.callback = (OnPathDelegate)Delegate.Combine(p.callback, this.onPathDelegate);
			}
			p.enabledTags = this.traversableTags;
			p.tagPenalties = this.tagPenalties;
			if (this.path != null && this.path.PipelineState <= PathState.Processing && this.path.CompleteState != PathCompleteState.Error && this.lastPathID == (uint)this.path.pathID)
			{
				this.path.FailWithError("Canceled path because a new one was requested.\nThis happens when a new path is requested from the seeker when one was already being calculated.\nFor example if a unit got a new order, you might request a new path directly instead of waiting for the now invalid path to be calculated. Which is probably what you want.\nIf you are getting this a lot, you might want to consider how you are scheduling path requests.");
			}
			this.path = p;
			this.tmpPathCallback = callback;
			this.lastPathID = (uint)this.path.pathID;
			this.RunModifiers(Seeker.ModifierPass.PreProcess, this.path);
			AstarPath.StartPath(this.path, false);
		}

		// Token: 0x06002172 RID: 8562 RVA: 0x0018A8FC File Offset: 0x00188AFC
		public MultiTargetPath StartMultiTargetPath(Vector3 start, Vector3[] endPoints, bool pathsForAll, OnPathDelegate callback = null, int graphMask = -1)
		{
			MultiTargetPath multiTargetPath = MultiTargetPath.Construct(start, endPoints, null, null);
			multiTargetPath.pathsForAll = pathsForAll;
			this.StartPath(multiTargetPath, callback, graphMask);
			return multiTargetPath;
		}

		// Token: 0x06002173 RID: 8563 RVA: 0x0018A928 File Offset: 0x00188B28
		public MultiTargetPath StartMultiTargetPath(Vector3[] startPoints, Vector3 end, bool pathsForAll, OnPathDelegate callback = null, int graphMask = -1)
		{
			MultiTargetPath multiTargetPath = MultiTargetPath.Construct(startPoints, end, null, null);
			multiTargetPath.pathsForAll = pathsForAll;
			this.StartPath(multiTargetPath, callback, graphMask);
			return multiTargetPath;
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x0018A953 File Offset: 0x00188B53
		[Obsolete("You can use StartPath instead of this method now. It will behave identically.")]
		public MultiTargetPath StartMultiTargetPath(MultiTargetPath p, OnPathDelegate callback = null, int graphMask = -1)
		{
			this.StartPath(p, callback, graphMask);
			return p;
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x0018A960 File Offset: 0x00188B60
		public void OnDrawGizmos()
		{
			if (this.lastCompletedNodePath == null || !this.drawGizmos)
			{
				return;
			}
			if (this.detailedGizmos)
			{
				Gizmos.color = new Color(0.7f, 0.5f, 0.1f, 0.5f);
				if (this.lastCompletedNodePath != null)
				{
					for (int i = 0; i < this.lastCompletedNodePath.Count - 1; i++)
					{
						Gizmos.DrawLine((Vector3)this.lastCompletedNodePath[i].position, (Vector3)this.lastCompletedNodePath[i + 1].position);
					}
				}
			}
			Gizmos.color = new Color(0f, 1f, 0f, 1f);
			if (this.lastCompletedVectorPath != null)
			{
				for (int j = 0; j < this.lastCompletedVectorPath.Count - 1; j++)
				{
					Gizmos.DrawLine(this.lastCompletedVectorPath[j], this.lastCompletedVectorPath[j + 1]);
				}
			}
		}

		// Token: 0x04003E92 RID: 16018
		public bool drawGizmos = true;

		// Token: 0x04003E93 RID: 16019
		public bool detailedGizmos;

		// Token: 0x04003E94 RID: 16020
		[HideInInspector]
		public StartEndModifier startEndModifier = new StartEndModifier();

		// Token: 0x04003E95 RID: 16021
		[HideInInspector]
		public int traversableTags = -1;

		// Token: 0x04003E96 RID: 16022
		[HideInInspector]
		public int[] tagPenalties = new int[32];

		// Token: 0x04003E97 RID: 16023
		[HideInInspector]
		public int graphMask = -1;

		// Token: 0x04003E98 RID: 16024
		public OnPathDelegate pathCallback;

		// Token: 0x04003E99 RID: 16025
		public OnPathDelegate preProcessPath;

		// Token: 0x04003E9A RID: 16026
		public OnPathDelegate postProcessPath;

		// Token: 0x04003E9B RID: 16027
		[NonSerialized]
		private List<Vector3> lastCompletedVectorPath;

		// Token: 0x04003E9C RID: 16028
		[NonSerialized]
		private List<GraphNode> lastCompletedNodePath;

		// Token: 0x04003E9D RID: 16029
		[NonSerialized]
		protected Path path;

		// Token: 0x04003E9E RID: 16030
		[NonSerialized]
		private Path prevPath;

		// Token: 0x04003E9F RID: 16031
		private readonly OnPathDelegate onPathDelegate;

		// Token: 0x04003EA0 RID: 16032
		private readonly OnPathDelegate onPartialPathDelegate;

		// Token: 0x04003EA1 RID: 16033
		private OnPathDelegate tmpPathCallback;

		// Token: 0x04003EA2 RID: 16034
		protected uint lastPathID;

		// Token: 0x04003EA3 RID: 16035
		private readonly List<IPathModifier> modifiers = new List<IPathModifier>();

		// Token: 0x0200070B RID: 1803
		public enum ModifierPass
		{
			// Token: 0x040048BC RID: 18620
			PreProcess,
			// Token: 0x040048BD RID: 18621
			PostProcess = 2
		}
	}
}
