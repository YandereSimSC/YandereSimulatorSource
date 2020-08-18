using System;
using System.Collections.Generic;
using System.Text;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000598 RID: 1432
	public class MultiTargetPath : ABPath
	{
		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x060026F1 RID: 9969 RVA: 0x001AA878 File Offset: 0x001A8A78
		// (set) Token: 0x060026F2 RID: 9970 RVA: 0x001AA880 File Offset: 0x001A8A80
		public bool inverted { get; protected set; }

		// Token: 0x060026F4 RID: 9972 RVA: 0x001AA8A6 File Offset: 0x001A8AA6
		public static MultiTargetPath Construct(Vector3[] startPoints, Vector3 target, OnPathDelegate[] callbackDelegates, OnPathDelegate callback = null)
		{
			MultiTargetPath multiTargetPath = MultiTargetPath.Construct(target, startPoints, callbackDelegates, callback);
			multiTargetPath.inverted = true;
			return multiTargetPath;
		}

		// Token: 0x060026F5 RID: 9973 RVA: 0x001AA8B8 File Offset: 0x001A8AB8
		public static MultiTargetPath Construct(Vector3 start, Vector3[] targets, OnPathDelegate[] callbackDelegates, OnPathDelegate callback = null)
		{
			MultiTargetPath path = PathPool.GetPath<MultiTargetPath>();
			path.Setup(start, targets, callbackDelegates, callback);
			return path;
		}

		// Token: 0x060026F6 RID: 9974 RVA: 0x001AA8CC File Offset: 0x001A8ACC
		protected void Setup(Vector3 start, Vector3[] targets, OnPathDelegate[] callbackDelegates, OnPathDelegate callback)
		{
			this.inverted = false;
			this.callback = callback;
			this.callbacks = callbackDelegates;
			if (this.callbacks != null && this.callbacks.Length != targets.Length)
			{
				throw new ArgumentException("The targets array must have the same length as the callbackDelegates array");
			}
			this.targetPoints = targets;
			this.originalStartPoint = start;
			this.startPoint = start;
			this.startIntPoint = (Int3)start;
			if (targets.Length == 0)
			{
				base.FailWithError("No targets were assigned to the MultiTargetPath");
				return;
			}
			this.endPoint = targets[0];
			this.originalTargetPoints = new Vector3[this.targetPoints.Length];
			for (int i = 0; i < this.targetPoints.Length; i++)
			{
				this.originalTargetPoints[i] = this.targetPoints[i];
			}
		}

		// Token: 0x060026F7 RID: 9975 RVA: 0x001AA98B File Offset: 0x001A8B8B
		protected override void Reset()
		{
			base.Reset();
			this.pathsForAll = true;
			this.chosenTarget = -1;
			this.sequentialTarget = 0;
			this.inverted = true;
			this.heuristicMode = MultiTargetPath.HeuristicMode.Sequential;
		}

		// Token: 0x060026F8 RID: 9976 RVA: 0x001AA9B8 File Offset: 0x001A8BB8
		protected override void OnEnterPool()
		{
			if (this.vectorPaths != null)
			{
				for (int i = 0; i < this.vectorPaths.Length; i++)
				{
					if (this.vectorPaths[i] != null)
					{
						ListPool<Vector3>.Release(this.vectorPaths[i]);
					}
				}
			}
			this.vectorPaths = null;
			this.vectorPath = null;
			if (this.nodePaths != null)
			{
				for (int j = 0; j < this.nodePaths.Length; j++)
				{
					if (this.nodePaths[j] != null)
					{
						ListPool<GraphNode>.Release(this.nodePaths[j]);
					}
				}
			}
			this.nodePaths = null;
			this.path = null;
			this.callbacks = null;
			this.targetNodes = null;
			this.targetsFound = null;
			this.targetPoints = null;
			this.originalTargetPoints = null;
			base.OnEnterPool();
		}

		// Token: 0x060026F9 RID: 9977 RVA: 0x001AAA70 File Offset: 0x001A8C70
		private void ChooseShortestPath()
		{
			this.chosenTarget = -1;
			if (this.nodePaths != null)
			{
				uint num = 2147483647u;
				for (int i = 0; i < this.nodePaths.Length; i++)
				{
					List<GraphNode> list = this.nodePaths[i];
					if (list != null)
					{
						uint g = this.pathHandler.GetPathNode(list[this.inverted ? 0 : (list.Count - 1)]).G;
						if (this.chosenTarget == -1 || g < num)
						{
							this.chosenTarget = i;
							num = g;
						}
					}
				}
			}
		}

		// Token: 0x060026FA RID: 9978 RVA: 0x001AAAF4 File Offset: 0x001A8CF4
		private void SetPathParametersForReturn(int target)
		{
			this.path = this.nodePaths[target];
			this.vectorPath = this.vectorPaths[target];
			if (this.inverted)
			{
				this.startNode = this.targetNodes[target];
				this.startPoint = this.targetPoints[target];
				this.originalStartPoint = this.originalTargetPoints[target];
				return;
			}
			this.endNode = this.targetNodes[target];
			this.endPoint = this.targetPoints[target];
			this.originalEndPoint = this.originalTargetPoints[target];
		}

		// Token: 0x060026FB RID: 9979 RVA: 0x001AAB8C File Offset: 0x001A8D8C
		protected override void ReturnPath()
		{
			if (base.error)
			{
				if (this.callbacks != null)
				{
					for (int i = 0; i < this.callbacks.Length; i++)
					{
						if (this.callbacks[i] != null)
						{
							this.callbacks[i](this);
						}
					}
				}
				if (this.callback != null)
				{
					this.callback(this);
				}
				return;
			}
			bool flag = false;
			if (this.inverted)
			{
				this.endPoint = this.startPoint;
				this.endNode = this.startNode;
				this.originalEndPoint = this.originalStartPoint;
			}
			for (int j = 0; j < this.nodePaths.Length; j++)
			{
				if (this.nodePaths[j] != null)
				{
					this.completeState = PathCompleteState.Complete;
					flag = true;
				}
				else
				{
					this.completeState = PathCompleteState.Error;
				}
				if (this.callbacks != null && this.callbacks[j] != null)
				{
					this.SetPathParametersForReturn(j);
					this.callbacks[j](this);
					this.vectorPaths[j] = this.vectorPath;
				}
			}
			if (flag)
			{
				this.completeState = PathCompleteState.Complete;
				this.SetPathParametersForReturn(this.chosenTarget);
			}
			else
			{
				this.completeState = PathCompleteState.Error;
			}
			if (this.callback != null)
			{
				this.callback(this);
			}
		}

		// Token: 0x060026FC RID: 9980 RVA: 0x001AACB0 File Offset: 0x001A8EB0
		protected void FoundTarget(PathNode nodeR, int i)
		{
			nodeR.flag1 = false;
			this.Trace(nodeR);
			this.vectorPaths[i] = this.vectorPath;
			this.nodePaths[i] = this.path;
			this.vectorPath = ListPool<Vector3>.Claim();
			this.path = ListPool<GraphNode>.Claim();
			this.targetsFound[i] = true;
			this.targetNodeCount--;
			if (!this.pathsForAll)
			{
				base.CompleteState = PathCompleteState.Complete;
				this.targetNodeCount = 0;
				return;
			}
			if (this.targetNodeCount <= 0)
			{
				base.CompleteState = PathCompleteState.Complete;
				return;
			}
			this.RecalculateHTarget(false);
		}

		// Token: 0x060026FD RID: 9981 RVA: 0x001AAD44 File Offset: 0x001A8F44
		protected void RebuildOpenList()
		{
			BinaryHeap heap = this.pathHandler.heap;
			for (int i = 0; i < heap.numberOfItems; i++)
			{
				PathNode node = heap.GetNode(i);
				node.H = base.CalculateHScore(node.node);
				heap.SetF(i, node.F);
			}
			this.pathHandler.heap.Rebuild();
		}

		// Token: 0x060026FE RID: 9982 RVA: 0x001AADA8 File Offset: 0x001A8FA8
		protected override void Prepare()
		{
			this.nnConstraint.tags = this.enabledTags;
			NNInfo nearest = AstarPath.active.GetNearest(this.startPoint, this.nnConstraint);
			this.startNode = nearest.node;
			if (this.startNode == null)
			{
				base.FailWithError("Could not find start node for multi target path");
				return;
			}
			if (!base.CanTraverse(this.startNode))
			{
				base.FailWithError("The node closest to the start point could not be traversed");
				return;
			}
			PathNNConstraint pathNNConstraint = this.nnConstraint as PathNNConstraint;
			if (pathNNConstraint != null)
			{
				pathNNConstraint.SetStart(nearest.node);
			}
			this.vectorPaths = new List<Vector3>[this.targetPoints.Length];
			this.nodePaths = new List<GraphNode>[this.targetPoints.Length];
			this.targetNodes = new GraphNode[this.targetPoints.Length];
			this.targetsFound = new bool[this.targetPoints.Length];
			this.targetNodeCount = this.targetPoints.Length;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			for (int i = 0; i < this.targetPoints.Length; i++)
			{
				NNInfo nearest2 = AstarPath.active.GetNearest(this.targetPoints[i], this.nnConstraint);
				this.targetNodes[i] = nearest2.node;
				this.targetPoints[i] = nearest2.position;
				if (this.targetNodes[i] != null)
				{
					flag3 = true;
					this.endNode = this.targetNodes[i];
				}
				bool flag4 = false;
				if (nearest2.node != null && base.CanTraverse(nearest2.node))
				{
					flag = true;
				}
				else
				{
					flag4 = true;
				}
				if (nearest2.node != null && nearest2.node.Area == this.startNode.Area)
				{
					flag2 = true;
				}
				else
				{
					flag4 = true;
				}
				if (flag4)
				{
					this.targetsFound[i] = true;
					this.targetNodeCount--;
				}
			}
			this.startPoint = nearest.position;
			this.startIntPoint = (Int3)this.startPoint;
			if (!flag3)
			{
				base.FailWithError("Couldn't find nodes close to the all of the end points");
				return;
			}
			if (!flag)
			{
				base.FailWithError("No target nodes could be traversed");
				return;
			}
			if (!flag2)
			{
				base.FailWithError("There are no valid paths to the targets");
				return;
			}
			this.RecalculateHTarget(true);
		}

		// Token: 0x060026FF RID: 9983 RVA: 0x001AAFCC File Offset: 0x001A91CC
		private void RecalculateHTarget(bool firstTime)
		{
			if (!this.pathsForAll)
			{
				this.heuristic = Heuristic.None;
				this.heuristicScale = 0f;
				return;
			}
			switch (this.heuristicMode)
			{
			case MultiTargetPath.HeuristicMode.None:
				this.heuristic = Heuristic.None;
				this.heuristicScale = 0f;
				goto IL_226;
			case MultiTargetPath.HeuristicMode.Average:
				if (!firstTime)
				{
					return;
				}
				break;
			case MultiTargetPath.HeuristicMode.MovingAverage:
				break;
			case MultiTargetPath.HeuristicMode.Midpoint:
				if (!firstTime)
				{
					return;
				}
				goto IL_D4;
			case MultiTargetPath.HeuristicMode.MovingMidpoint:
				goto IL_D4;
			case MultiTargetPath.HeuristicMode.Sequential:
			{
				if (!firstTime && !this.targetsFound[this.sequentialTarget])
				{
					return;
				}
				float num = 0f;
				for (int i = 0; i < this.targetPoints.Length; i++)
				{
					if (!this.targetsFound[i])
					{
						float sqrMagnitude = (this.targetNodes[i].position - this.startNode.position).sqrMagnitude;
						if (sqrMagnitude > num)
						{
							num = sqrMagnitude;
							this.hTarget = (Int3)this.targetPoints[i];
							this.sequentialTarget = i;
						}
					}
				}
				goto IL_226;
			}
			default:
				goto IL_226;
			}
			Vector3 vector = Vector3.zero;
			int num2 = 0;
			for (int j = 0; j < this.targetPoints.Length; j++)
			{
				if (!this.targetsFound[j])
				{
					vector += (Vector3)this.targetNodes[j].position;
					num2++;
				}
			}
			if (num2 == 0)
			{
				throw new Exception("Should not happen");
			}
			vector /= (float)num2;
			this.hTarget = (Int3)vector;
			goto IL_226;
			IL_D4:
			Vector3 vector2 = Vector3.zero;
			Vector3 vector3 = Vector3.zero;
			bool flag = false;
			for (int k = 0; k < this.targetPoints.Length; k++)
			{
				if (!this.targetsFound[k])
				{
					if (!flag)
					{
						vector2 = (Vector3)this.targetNodes[k].position;
						vector3 = (Vector3)this.targetNodes[k].position;
						flag = true;
					}
					else
					{
						vector2 = Vector3.Min((Vector3)this.targetNodes[k].position, vector2);
						vector3 = Vector3.Max((Vector3)this.targetNodes[k].position, vector3);
					}
				}
			}
			Int3 hTarget = (Int3)((vector2 + vector3) * 0.5f);
			this.hTarget = hTarget;
			IL_226:
			if (!firstTime)
			{
				this.RebuildOpenList();
			}
		}

		// Token: 0x06002700 RID: 9984 RVA: 0x001AB208 File Offset: 0x001A9408
		protected override void Initialize()
		{
			PathNode pathNode = this.pathHandler.GetPathNode(this.startNode);
			pathNode.node = this.startNode;
			pathNode.pathID = base.pathID;
			pathNode.parent = null;
			pathNode.cost = 0u;
			pathNode.G = base.GetTraversalCost(this.startNode);
			pathNode.H = base.CalculateHScore(this.startNode);
			for (int i = 0; i < this.targetNodes.Length; i++)
			{
				if (this.startNode == this.targetNodes[i])
				{
					this.FoundTarget(pathNode, i);
				}
				else if (this.targetNodes[i] != null)
				{
					this.pathHandler.GetPathNode(this.targetNodes[i]).flag1 = true;
				}
			}
			if (this.targetNodeCount <= 0)
			{
				base.CompleteState = PathCompleteState.Complete;
				return;
			}
			this.startNode.Open(this, pathNode, this.pathHandler);
			this.searchedNodes++;
			if (this.pathHandler.heap.isEmpty)
			{
				base.FailWithError("No open points, the start node didn't open any nodes");
				return;
			}
			this.currentR = this.pathHandler.heap.Remove();
		}

		// Token: 0x06002701 RID: 9985 RVA: 0x001AB327 File Offset: 0x001A9527
		protected override void Cleanup()
		{
			this.ChooseShortestPath();
			this.ResetFlags();
		}

		// Token: 0x06002702 RID: 9986 RVA: 0x001AB338 File Offset: 0x001A9538
		private void ResetFlags()
		{
			if (this.targetNodes != null)
			{
				for (int i = 0; i < this.targetNodes.Length; i++)
				{
					if (this.targetNodes[i] != null)
					{
						this.pathHandler.GetPathNode(this.targetNodes[i]).flag1 = false;
					}
				}
			}
		}

		// Token: 0x06002703 RID: 9987 RVA: 0x001AB384 File Offset: 0x001A9584
		protected override void CalculateStep(long targetTick)
		{
			int num = 0;
			while (base.CompleteState == PathCompleteState.NotCalculated)
			{
				this.searchedNodes++;
				if (this.currentR.flag1)
				{
					for (int i = 0; i < this.targetNodes.Length; i++)
					{
						if (!this.targetsFound[i] && this.currentR.node == this.targetNodes[i])
						{
							this.FoundTarget(this.currentR, i);
							if (base.CompleteState != PathCompleteState.NotCalculated)
							{
								break;
							}
						}
					}
					if (this.targetNodeCount <= 0)
					{
						base.CompleteState = PathCompleteState.Complete;
						return;
					}
				}
				this.currentR.node.Open(this, this.currentR, this.pathHandler);
				if (this.pathHandler.heap.isEmpty)
				{
					base.CompleteState = PathCompleteState.Complete;
					return;
				}
				this.currentR = this.pathHandler.heap.Remove();
				if (num > 500)
				{
					if (DateTime.UtcNow.Ticks >= targetTick)
					{
						return;
					}
					num = 0;
				}
				num++;
			}
		}

		// Token: 0x06002704 RID: 9988 RVA: 0x001AB484 File Offset: 0x001A9684
		protected override void Trace(PathNode node)
		{
			base.Trace(node);
			if (this.inverted)
			{
				int num = this.path.Count / 2;
				for (int i = 0; i < num; i++)
				{
					GraphNode value = this.path[i];
					this.path[i] = this.path[this.path.Count - i - 1];
					this.path[this.path.Count - i - 1] = value;
				}
				for (int j = 0; j < num; j++)
				{
					Vector3 value2 = this.vectorPath[j];
					this.vectorPath[j] = this.vectorPath[this.vectorPath.Count - j - 1];
					this.vectorPath[this.vectorPath.Count - j - 1] = value2;
				}
			}
		}

		// Token: 0x06002705 RID: 9989 RVA: 0x001AB568 File Offset: 0x001A9768
		internal override string DebugString(PathLog logMode)
		{
			if (logMode == PathLog.None || (!base.error && logMode == PathLog.OnlyErrors))
			{
				return "";
			}
			StringBuilder debugStringBuilder = this.pathHandler.DebugStringBuilder;
			debugStringBuilder.Length = 0;
			base.DebugStringPrefix(logMode, debugStringBuilder);
			if (!base.error)
			{
				debugStringBuilder.Append("\nShortest path was ");
				debugStringBuilder.Append((this.chosenTarget == -1) ? "undefined" : this.nodePaths[this.chosenTarget].Count.ToString());
				debugStringBuilder.Append(" nodes long");
				if (logMode == PathLog.Heavy)
				{
					debugStringBuilder.Append("\nPaths (").Append(this.targetsFound.Length).Append("):");
					Vector3 endPoint;
					for (int i = 0; i < this.targetsFound.Length; i++)
					{
						debugStringBuilder.Append("\n\n\tPath ").Append(i).Append(" Found: ").Append(this.targetsFound[i]);
						if (this.nodePaths[i] != null)
						{
							debugStringBuilder.Append("\n\t\tLength: ");
							debugStringBuilder.Append(this.nodePaths[i].Count);
							if (this.nodePaths[i][this.nodePaths[i].Count - 1] != null)
							{
								PathNode pathNode = this.pathHandler.GetPathNode(this.endNode);
								if (pathNode != null)
								{
									debugStringBuilder.Append("\n\t\tEnd Node");
									debugStringBuilder.Append("\n\t\t\tG: ");
									debugStringBuilder.Append(pathNode.G);
									debugStringBuilder.Append("\n\t\t\tH: ");
									debugStringBuilder.Append(pathNode.H);
									debugStringBuilder.Append("\n\t\t\tF: ");
									debugStringBuilder.Append(pathNode.F);
									debugStringBuilder.Append("\n\t\t\tPoint: ");
									StringBuilder stringBuilder = debugStringBuilder;
									endPoint = this.endPoint;
									stringBuilder.Append(endPoint.ToString());
									debugStringBuilder.Append("\n\t\t\tGraph: ");
									debugStringBuilder.Append(this.endNode.GraphIndex);
								}
								else
								{
									debugStringBuilder.Append("\n\t\tEnd Node: Null");
								}
							}
						}
					}
					debugStringBuilder.Append("\nStart Node");
					debugStringBuilder.Append("\n\tPoint: ");
					StringBuilder stringBuilder2 = debugStringBuilder;
					endPoint = this.endPoint;
					stringBuilder2.Append(endPoint.ToString());
					debugStringBuilder.Append("\n\tGraph: ");
					debugStringBuilder.Append(this.startNode.GraphIndex);
					debugStringBuilder.Append("\nBinary Heap size at completion: ");
					debugStringBuilder.AppendLine((this.pathHandler.heap == null) ? "Null" : (this.pathHandler.heap.numberOfItems - 2).ToString());
				}
			}
			base.DebugStringSuffix(logMode, debugStringBuilder);
			return debugStringBuilder.ToString();
		}

		// Token: 0x040041A0 RID: 16800
		public OnPathDelegate[] callbacks;

		// Token: 0x040041A1 RID: 16801
		public GraphNode[] targetNodes;

		// Token: 0x040041A2 RID: 16802
		protected int targetNodeCount;

		// Token: 0x040041A3 RID: 16803
		public bool[] targetsFound;

		// Token: 0x040041A4 RID: 16804
		public Vector3[] targetPoints;

		// Token: 0x040041A5 RID: 16805
		public Vector3[] originalTargetPoints;

		// Token: 0x040041A6 RID: 16806
		public List<Vector3>[] vectorPaths;

		// Token: 0x040041A7 RID: 16807
		public List<GraphNode>[] nodePaths;

		// Token: 0x040041A8 RID: 16808
		public bool pathsForAll = true;

		// Token: 0x040041A9 RID: 16809
		public int chosenTarget = -1;

		// Token: 0x040041AA RID: 16810
		private int sequentialTarget;

		// Token: 0x040041AB RID: 16811
		public MultiTargetPath.HeuristicMode heuristicMode = MultiTargetPath.HeuristicMode.Sequential;

		// Token: 0x02000758 RID: 1880
		public enum HeuristicMode
		{
			// Token: 0x04004A03 RID: 18947
			None,
			// Token: 0x04004A04 RID: 18948
			Average,
			// Token: 0x04004A05 RID: 18949
			MovingAverage,
			// Token: 0x04004A06 RID: 18950
			Midpoint,
			// Token: 0x04004A07 RID: 18951
			MovingMidpoint,
			// Token: 0x04004A08 RID: 18952
			Sequential
		}
	}
}
