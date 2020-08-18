using System;
using System.Text;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000591 RID: 1425
	public class ABPath : Path
	{
		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x060026BB RID: 9915 RVA: 0x0002291C File Offset: 0x00020B1C
		protected virtual bool hasEndPoint
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060026BD RID: 9917 RVA: 0x001A933D File Offset: 0x001A753D
		public static ABPath Construct(Vector3 start, Vector3 end, OnPathDelegate callback = null)
		{
			ABPath path = PathPool.GetPath<ABPath>();
			path.Setup(start, end, callback);
			return path;
		}

		// Token: 0x060026BE RID: 9918 RVA: 0x001A934D File Offset: 0x001A754D
		protected void Setup(Vector3 start, Vector3 end, OnPathDelegate callbackDelegate)
		{
			this.callback = callbackDelegate;
			this.UpdateStartEnd(start, end);
		}

		// Token: 0x060026BF RID: 9919 RVA: 0x001A935E File Offset: 0x001A755E
		protected void UpdateStartEnd(Vector3 start, Vector3 end)
		{
			this.originalStartPoint = start;
			this.originalEndPoint = end;
			this.startPoint = start;
			this.endPoint = end;
			this.startIntPoint = (Int3)start;
			this.hTarget = (Int3)end;
		}

		// Token: 0x060026C0 RID: 9920 RVA: 0x001A9394 File Offset: 0x001A7594
		internal override uint GetConnectionSpecialCost(GraphNode a, GraphNode b, uint currentCost)
		{
			if (this.startNode != null && this.endNode != null)
			{
				if (a == this.startNode)
				{
					return (uint)((double)(this.startIntPoint - ((b == this.endNode) ? this.hTarget : b.position)).costMagnitude * (currentCost * 1.0 / (double)(a.position - b.position).costMagnitude));
				}
				if (b == this.startNode)
				{
					return (uint)((double)(this.startIntPoint - ((a == this.endNode) ? this.hTarget : a.position)).costMagnitude * (currentCost * 1.0 / (double)(a.position - b.position).costMagnitude));
				}
				if (a == this.endNode)
				{
					return (uint)((double)(this.hTarget - b.position).costMagnitude * (currentCost * 1.0 / (double)(a.position - b.position).costMagnitude));
				}
				if (b == this.endNode)
				{
					return (uint)((double)(this.hTarget - a.position).costMagnitude * (currentCost * 1.0 / (double)(a.position - b.position).costMagnitude));
				}
			}
			else
			{
				if (a == this.startNode)
				{
					return (uint)((double)(this.startIntPoint - b.position).costMagnitude * (currentCost * 1.0 / (double)(a.position - b.position).costMagnitude));
				}
				if (b == this.startNode)
				{
					return (uint)((double)(this.startIntPoint - a.position).costMagnitude * (currentCost * 1.0 / (double)(a.position - b.position).costMagnitude));
				}
			}
			return currentCost;
		}

		// Token: 0x060026C1 RID: 9921 RVA: 0x001A95B4 File Offset: 0x001A77B4
		protected override void Reset()
		{
			base.Reset();
			this.startNode = null;
			this.endNode = null;
			this.originalStartPoint = Vector3.zero;
			this.originalEndPoint = Vector3.zero;
			this.startPoint = Vector3.zero;
			this.endPoint = Vector3.zero;
			this.calculatePartial = false;
			this.partialBestTarget = null;
			this.startIntPoint = default(Int3);
			this.hTarget = default(Int3);
			this.endNodeCosts = null;
			this.gridSpecialCaseNode = null;
		}

		// Token: 0x060026C2 RID: 9922 RVA: 0x001A9638 File Offset: 0x001A7838
		protected virtual bool EndPointGridGraphSpecialCase(GraphNode closestWalkableEndNode)
		{
			GridNode gridNode = closestWalkableEndNode as GridNode;
			if (gridNode != null)
			{
				GridGraph gridGraph = GridNode.GetGridGraph(gridNode.GraphIndex);
				GridNode gridNode2 = AstarPath.active.GetNearest(this.originalEndPoint, NNConstraint.None).node as GridNode;
				if (gridNode != gridNode2 && gridNode2 != null && gridNode.GraphIndex == gridNode2.GraphIndex)
				{
					int num = gridNode.NodeInGridIndex % gridGraph.width;
					int num2 = gridNode.NodeInGridIndex / gridGraph.width;
					int num3 = gridNode2.NodeInGridIndex % gridGraph.width;
					int num4 = gridNode2.NodeInGridIndex / gridGraph.width;
					bool flag = false;
					switch (gridGraph.neighbours)
					{
					case NumNeighbours.Four:
						if ((num == num3 && Math.Abs(num2 - num4) == 1) || (num2 == num4 && Math.Abs(num - num3) == 1))
						{
							flag = true;
						}
						break;
					case NumNeighbours.Eight:
						if (Math.Abs(num - num3) <= 1 && Math.Abs(num2 - num4) <= 1)
						{
							flag = true;
						}
						break;
					case NumNeighbours.Six:
						for (int i = 0; i < 6; i++)
						{
							int num5 = num3 + gridGraph.neighbourXOffsets[GridGraph.hexagonNeighbourIndices[i]];
							int num6 = num4 + gridGraph.neighbourZOffsets[GridGraph.hexagonNeighbourIndices[i]];
							if (num == num5 && num2 == num6)
							{
								flag = true;
								break;
							}
						}
						break;
					default:
						throw new Exception("Unhandled NumNeighbours");
					}
					if (flag)
					{
						this.SetFlagOnSurroundingGridNodes(gridNode2, 1, true);
						this.endPoint = (Vector3)gridNode2.position;
						this.hTarget = gridNode2.position;
						this.endNode = gridNode2;
						this.hTargetNode = this.endNode;
						this.gridSpecialCaseNode = gridNode2;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060026C3 RID: 9923 RVA: 0x001A97E0 File Offset: 0x001A79E0
		private void SetFlagOnSurroundingGridNodes(GridNode gridNode, int flag, bool flagState)
		{
			GridGraph gridGraph = GridNode.GetGridGraph(gridNode.GraphIndex);
			int num = (gridGraph.neighbours == NumNeighbours.Four) ? 4 : ((gridGraph.neighbours == NumNeighbours.Eight) ? 8 : 6);
			int num2 = gridNode.NodeInGridIndex % gridGraph.width;
			int num3 = gridNode.NodeInGridIndex / gridGraph.width;
			if (flag != 1 && flag != 2)
			{
				throw new ArgumentOutOfRangeException("flag");
			}
			for (int i = 0; i < num; i++)
			{
				int num4;
				int num5;
				if (gridGraph.neighbours == NumNeighbours.Six)
				{
					num4 = num2 + gridGraph.neighbourXOffsets[GridGraph.hexagonNeighbourIndices[i]];
					num5 = num3 + gridGraph.neighbourZOffsets[GridGraph.hexagonNeighbourIndices[i]];
				}
				else
				{
					num4 = num2 + gridGraph.neighbourXOffsets[i];
					num5 = num3 + gridGraph.neighbourZOffsets[i];
				}
				if (num4 >= 0 && num5 >= 0 && num4 < gridGraph.width && num5 < gridGraph.depth)
				{
					GridNode node = gridGraph.nodes[num5 * gridGraph.width + num4];
					PathNode pathNode = this.pathHandler.GetPathNode(node);
					if (flag == 1)
					{
						pathNode.flag1 = flagState;
					}
					else
					{
						pathNode.flag2 = flagState;
					}
				}
			}
		}

		// Token: 0x060026C4 RID: 9924 RVA: 0x001A98FC File Offset: 0x001A7AFC
		protected override void Prepare()
		{
			this.nnConstraint.tags = this.enabledTags;
			NNInfo nearest = AstarPath.active.GetNearest(this.startPoint, this.nnConstraint);
			PathNNConstraint pathNNConstraint = this.nnConstraint as PathNNConstraint;
			if (pathNNConstraint != null)
			{
				pathNNConstraint.SetStart(nearest.node);
			}
			this.startPoint = nearest.position;
			this.startIntPoint = (Int3)this.startPoint;
			this.startNode = nearest.node;
			if (this.startNode == null)
			{
				base.FailWithError("Couldn't find a node close to the start point");
				return;
			}
			if (!base.CanTraverse(this.startNode))
			{
				base.FailWithError("The node closest to the start point could not be traversed");
				return;
			}
			if (this.hasEndPoint)
			{
				NNInfo nearest2 = AstarPath.active.GetNearest(this.endPoint, this.nnConstraint);
				this.endPoint = nearest2.position;
				this.endNode = nearest2.node;
				if (this.endNode == null)
				{
					base.FailWithError("Couldn't find a node close to the end point");
					return;
				}
				if (!base.CanTraverse(this.endNode))
				{
					base.FailWithError("The node closest to the end point could not be traversed");
					return;
				}
				if (this.startNode.Area != this.endNode.Area)
				{
					base.FailWithError("There is no valid path to the target");
					return;
				}
				if (!this.EndPointGridGraphSpecialCase(nearest2.node))
				{
					this.hTarget = (Int3)this.endPoint;
					this.hTargetNode = this.endNode;
					this.pathHandler.GetPathNode(this.endNode).flag1 = true;
				}
			}
		}

		// Token: 0x060026C5 RID: 9925 RVA: 0x001A9A74 File Offset: 0x001A7C74
		protected virtual void CompletePathIfStartIsValidTarget()
		{
			if (this.hasEndPoint && this.pathHandler.GetPathNode(this.startNode).flag1)
			{
				this.CompleteWith(this.startNode);
				this.Trace(this.pathHandler.GetPathNode(this.startNode));
			}
		}

		// Token: 0x060026C6 RID: 9926 RVA: 0x001A9AC4 File Offset: 0x001A7CC4
		protected override void Initialize()
		{
			if (this.startNode != null)
			{
				this.pathHandler.GetPathNode(this.startNode).flag2 = true;
			}
			if (this.endNode != null)
			{
				this.pathHandler.GetPathNode(this.endNode).flag2 = true;
			}
			PathNode pathNode = this.pathHandler.GetPathNode(this.startNode);
			pathNode.node = this.startNode;
			pathNode.pathID = this.pathHandler.PathID;
			pathNode.parent = null;
			pathNode.cost = 0u;
			pathNode.G = base.GetTraversalCost(this.startNode);
			pathNode.H = base.CalculateHScore(this.startNode);
			this.CompletePathIfStartIsValidTarget();
			if (base.CompleteState == PathCompleteState.Complete)
			{
				return;
			}
			this.startNode.Open(this, pathNode, this.pathHandler);
			this.searchedNodes++;
			this.partialBestTarget = pathNode;
			if (!this.pathHandler.heap.isEmpty)
			{
				this.currentR = this.pathHandler.heap.Remove();
				return;
			}
			if (this.calculatePartial)
			{
				base.CompleteState = PathCompleteState.Partial;
				this.Trace(this.partialBestTarget);
				return;
			}
			base.FailWithError("No open points, the start node didn't open any nodes");
		}

		// Token: 0x060026C7 RID: 9927 RVA: 0x001A9BF8 File Offset: 0x001A7DF8
		protected override void Cleanup()
		{
			if (this.startNode != null)
			{
				PathNode pathNode = this.pathHandler.GetPathNode(this.startNode);
				pathNode.flag1 = false;
				pathNode.flag2 = false;
			}
			if (this.endNode != null)
			{
				PathNode pathNode2 = this.pathHandler.GetPathNode(this.endNode);
				pathNode2.flag1 = false;
				pathNode2.flag2 = false;
			}
			if (this.gridSpecialCaseNode != null)
			{
				PathNode pathNode3 = this.pathHandler.GetPathNode(this.gridSpecialCaseNode);
				pathNode3.flag1 = false;
				pathNode3.flag2 = false;
				this.SetFlagOnSurroundingGridNodes(this.gridSpecialCaseNode, 1, false);
				this.SetFlagOnSurroundingGridNodes(this.gridSpecialCaseNode, 2, false);
			}
		}

		// Token: 0x060026C8 RID: 9928 RVA: 0x001A9C94 File Offset: 0x001A7E94
		private void CompleteWith(GraphNode node)
		{
			if (this.endNode != node)
			{
				GridNode gridNode = node as GridNode;
				if (gridNode == null)
				{
					throw new Exception("Some path is not cleaning up the flag1 field. This is a bug.");
				}
				this.endPoint = gridNode.ClosestPointOnNode(this.originalEndPoint);
				this.endNode = node;
			}
			base.CompleteState = PathCompleteState.Complete;
		}

		// Token: 0x060026C9 RID: 9929 RVA: 0x001A9CE0 File Offset: 0x001A7EE0
		protected override void CalculateStep(long targetTick)
		{
			int num = 0;
			while (base.CompleteState == PathCompleteState.NotCalculated)
			{
				this.searchedNodes++;
				if (this.currentR.flag1)
				{
					this.CompleteWith(this.currentR.node);
					break;
				}
				if (this.currentR.H < this.partialBestTarget.H)
				{
					this.partialBestTarget = this.currentR;
				}
				this.currentR.node.Open(this, this.currentR, this.pathHandler);
				if (this.pathHandler.heap.isEmpty)
				{
					if (this.calculatePartial && this.partialBestTarget != null)
					{
						base.CompleteState = PathCompleteState.Partial;
						this.Trace(this.partialBestTarget);
						return;
					}
					base.FailWithError("Searched whole area but could not find target");
					return;
				}
				else
				{
					this.currentR = this.pathHandler.heap.Remove();
					if (num > 500)
					{
						if (DateTime.UtcNow.Ticks >= targetTick)
						{
							return;
						}
						num = 0;
						if (this.searchedNodes > 1000000)
						{
							throw new Exception("Probable infinite loop. Over 1,000,000 nodes searched");
						}
					}
					num++;
				}
			}
			if (base.CompleteState == PathCompleteState.Complete)
			{
				this.Trace(this.currentR);
				return;
			}
			if (this.calculatePartial && this.partialBestTarget != null)
			{
				base.CompleteState = PathCompleteState.Partial;
				this.Trace(this.partialBestTarget);
			}
		}

		// Token: 0x060026CA RID: 9930 RVA: 0x001A9E3C File Offset: 0x001A803C
		internal override string DebugString(PathLog logMode)
		{
			if (logMode == PathLog.None || (!base.error && logMode == PathLog.OnlyErrors))
			{
				return "";
			}
			StringBuilder stringBuilder = new StringBuilder();
			base.DebugStringPrefix(logMode, stringBuilder);
			if (!base.error && logMode == PathLog.Heavy)
			{
				Vector3 vector;
				if (this.hasEndPoint && this.endNode != null)
				{
					PathNode pathNode = this.pathHandler.GetPathNode(this.endNode);
					stringBuilder.Append("\nEnd Node\n\tG: ");
					stringBuilder.Append(pathNode.G);
					stringBuilder.Append("\n\tH: ");
					stringBuilder.Append(pathNode.H);
					stringBuilder.Append("\n\tF: ");
					stringBuilder.Append(pathNode.F);
					stringBuilder.Append("\n\tPoint: ");
					StringBuilder stringBuilder2 = stringBuilder;
					vector = this.endPoint;
					stringBuilder2.Append(vector.ToString());
					stringBuilder.Append("\n\tGraph: ");
					stringBuilder.Append(this.endNode.GraphIndex);
				}
				stringBuilder.Append("\nStart Node");
				stringBuilder.Append("\n\tPoint: ");
				StringBuilder stringBuilder3 = stringBuilder;
				vector = this.startPoint;
				stringBuilder3.Append(vector.ToString());
				stringBuilder.Append("\n\tGraph: ");
				if (this.startNode != null)
				{
					stringBuilder.Append(this.startNode.GraphIndex);
				}
				else
				{
					stringBuilder.Append("< null startNode >");
				}
			}
			base.DebugStringSuffix(logMode, stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x060026CB RID: 9931 RVA: 0x001A9FAC File Offset: 0x001A81AC
		[Obsolete]
		public Vector3 GetMovementVector(Vector3 point)
		{
			if (this.vectorPath == null || this.vectorPath.Count == 0)
			{
				return Vector3.zero;
			}
			if (this.vectorPath.Count == 1)
			{
				return this.vectorPath[0] - point;
			}
			float num = float.PositiveInfinity;
			int num2 = 0;
			for (int i = 0; i < this.vectorPath.Count - 1; i++)
			{
				float sqrMagnitude = (VectorMath.ClosestPointOnSegment(this.vectorPath[i], this.vectorPath[i + 1], point) - point).sqrMagnitude;
				if (sqrMagnitude < num)
				{
					num = sqrMagnitude;
					num2 = i;
				}
			}
			return this.vectorPath[num2 + 1] - point;
		}

		// Token: 0x04004188 RID: 16776
		public GraphNode startNode;

		// Token: 0x04004189 RID: 16777
		public GraphNode endNode;

		// Token: 0x0400418A RID: 16778
		public Vector3 originalStartPoint;

		// Token: 0x0400418B RID: 16779
		public Vector3 originalEndPoint;

		// Token: 0x0400418C RID: 16780
		public Vector3 startPoint;

		// Token: 0x0400418D RID: 16781
		public Vector3 endPoint;

		// Token: 0x0400418E RID: 16782
		public Int3 startIntPoint;

		// Token: 0x0400418F RID: 16783
		public bool calculatePartial;

		// Token: 0x04004190 RID: 16784
		protected PathNode partialBestTarget;

		// Token: 0x04004191 RID: 16785
		protected int[] endNodeCosts;

		// Token: 0x04004192 RID: 16786
		private GridNode gridSpecialCaseNode;
	}
}
