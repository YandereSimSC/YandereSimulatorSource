using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000577 RID: 1399
	[Serializable]
	public class EuclideanEmbedding
	{
		// Token: 0x06002600 RID: 9728 RVA: 0x001A3B4E File Offset: 0x001A1D4E
		private uint GetRandom()
		{
			this.rval = 12820163u * this.rval + 1140671485u;
			return this.rval;
		}

		// Token: 0x06002601 RID: 9729 RVA: 0x001A3B70 File Offset: 0x001A1D70
		private void EnsureCapacity(int index)
		{
			if (index > this.maxNodeIndex)
			{
				object obj = this.lockObj;
				lock (obj)
				{
					if (index > this.maxNodeIndex)
					{
						if (index >= this.costs.Length)
						{
							uint[] array = new uint[Math.Max(index * 2, this.pivots.Length * 2)];
							for (int i = 0; i < this.costs.Length; i++)
							{
								array[i] = this.costs[i];
							}
							this.costs = array;
						}
						this.maxNodeIndex = index;
					}
				}
			}
		}

		// Token: 0x06002602 RID: 9730 RVA: 0x001A3C0C File Offset: 0x001A1E0C
		public uint GetHeuristic(int nodeIndex1, int nodeIndex2)
		{
			nodeIndex1 *= this.pivotCount;
			nodeIndex2 *= this.pivotCount;
			if (nodeIndex1 >= this.costs.Length || nodeIndex2 >= this.costs.Length)
			{
				this.EnsureCapacity((nodeIndex1 > nodeIndex2) ? nodeIndex1 : nodeIndex2);
			}
			uint num = 0u;
			for (int i = 0; i < this.pivotCount; i++)
			{
				uint num2 = (uint)Math.Abs((int)(this.costs[nodeIndex1 + i] - this.costs[nodeIndex2 + i]));
				if (num2 > num)
				{
					num = num2;
				}
			}
			return num;
		}

		// Token: 0x06002603 RID: 9731 RVA: 0x001A3C88 File Offset: 0x001A1E88
		private void GetClosestWalkableNodesToChildrenRecursively(Transform tr, List<GraphNode> nodes)
		{
			foreach (object obj in tr)
			{
				Transform transform = (Transform)obj;
				NNInfo nearest = AstarPath.active.GetNearest(transform.position, NNConstraint.Default);
				if (nearest.node != null && nearest.node.Walkable)
				{
					nodes.Add(nearest.node);
				}
				this.GetClosestWalkableNodesToChildrenRecursively(transform, nodes);
			}
		}

		// Token: 0x06002604 RID: 9732 RVA: 0x001A3D14 File Offset: 0x001A1F14
		private void PickNRandomNodes(int count, List<GraphNode> buffer)
		{
			int n = 0;
			Action<GraphNode> <>9__0;
			foreach (NavGraph navGraph in AstarPath.active.graphs)
			{
				Action<GraphNode> action;
				if ((action = <>9__0) == null)
				{
					action = (<>9__0 = delegate(GraphNode node)
					{
						if (!node.Destroyed && node.Walkable)
						{
							int n = n;
							n++;
							if ((ulong)this.GetRandom() % (ulong)((long)n) < (ulong)((long)count))
							{
								if (buffer.Count < count)
								{
									buffer.Add(node);
									return;
								}
								buffer[(int)((ulong)this.GetRandom() % (ulong)((long)buffer.Count))] = node;
							}
						}
					});
				}
				navGraph.GetNodes(action);
			}
		}

		// Token: 0x06002605 RID: 9733 RVA: 0x001A3D84 File Offset: 0x001A1F84
		private GraphNode PickAnyWalkableNode()
		{
			NavGraph[] graphs = AstarPath.active.graphs;
			GraphNode first = null;
			Action<GraphNode> <>9__0;
			foreach (NavGraph navGraph in graphs)
			{
				Action<GraphNode> action;
				if ((action = <>9__0) == null)
				{
					action = (<>9__0 = delegate(GraphNode node)
					{
						if (node != null && node.Walkable && first == null)
						{
							first = node;
						}
					});
				}
				navGraph.GetNodes(action);
			}
			return first;
		}

		// Token: 0x06002606 RID: 9734 RVA: 0x001A3DE4 File Offset: 0x001A1FE4
		public void RecalculatePivots()
		{
			if (this.mode == HeuristicOptimizationMode.None)
			{
				this.pivotCount = 0;
				this.pivots = null;
				return;
			}
			this.rval = (uint)this.seed;
			List<GraphNode> list = ListPool<GraphNode>.Claim();
			switch (this.mode)
			{
			case HeuristicOptimizationMode.Random:
				this.PickNRandomNodes(this.spreadOutCount, list);
				break;
			case HeuristicOptimizationMode.RandomSpreadOut:
			{
				if (this.pivotPointRoot != null)
				{
					this.GetClosestWalkableNodesToChildrenRecursively(this.pivotPointRoot, list);
				}
				if (list.Count == 0)
				{
					GraphNode graphNode = this.PickAnyWalkableNode();
					if (graphNode == null)
					{
						Debug.LogError("Could not find any walkable node in any of the graphs.");
						ListPool<GraphNode>.Release(ref list);
						return;
					}
					list.Add(graphNode);
				}
				int num = this.spreadOutCount - list.Count;
				for (int i = 0; i < num; i++)
				{
					list.Add(null);
				}
				break;
			}
			case HeuristicOptimizationMode.Custom:
				if (this.pivotPointRoot == null)
				{
					throw new Exception("heuristicOptimizationMode is HeuristicOptimizationMode.Custom, but no 'customHeuristicOptimizationPivotsRoot' is set");
				}
				this.GetClosestWalkableNodesToChildrenRecursively(this.pivotPointRoot, list);
				break;
			default:
				throw new Exception("Invalid HeuristicOptimizationMode: " + this.mode);
			}
			this.pivots = list.ToArray();
			ListPool<GraphNode>.Release(ref list);
		}

		// Token: 0x06002607 RID: 9735 RVA: 0x001A3F14 File Offset: 0x001A2114
		public void RecalculateCosts()
		{
			if (this.pivots == null)
			{
				this.RecalculatePivots();
			}
			if (this.mode == HeuristicOptimizationMode.None)
			{
				return;
			}
			this.pivotCount = 0;
			for (int i = 0; i < this.pivots.Length; i++)
			{
				if (this.pivots[i] != null && (this.pivots[i].Destroyed || !this.pivots[i].Walkable))
				{
					throw new Exception("Invalid pivot nodes (destroyed or unwalkable)");
				}
			}
			if (this.mode != HeuristicOptimizationMode.RandomSpreadOut)
			{
				for (int j = 0; j < this.pivots.Length; j++)
				{
					if (this.pivots[j] == null)
					{
						throw new Exception("Invalid pivot nodes (null)");
					}
				}
			}
			Debug.Log("Recalculating costs...");
			this.pivotCount = this.pivots.Length;
			Action<int> startCostCalculation = null;
			int numComplete = 0;
			OnPathDelegate onComplete = delegate(Path path)
			{
				int numComplete = numComplete;
				numComplete++;
				if (numComplete == this.pivotCount)
				{
					this.ApplyGridGraphEndpointSpecialCase();
				}
			};
			startCostCalculation = delegate(int pivotIndex)
			{
				GraphNode pivot = this.pivots[pivotIndex];
				FloodPath floodPath = null;
				floodPath = FloodPath.Construct(pivot, onComplete);
				floodPath.immediateCallback = delegate(Path _p)
				{
					_p.Claim(this);
					MeshNode meshNode = pivot as MeshNode;
					uint costOffset = 0u;
					if (meshNode != null && meshNode.connections != null)
					{
						for (int l = 0; l < meshNode.connections.Length; l++)
						{
							costOffset = Math.Max(costOffset, meshNode.connections[l].cost);
						}
					}
					NavGraph[] graphs = AstarPath.active.graphs;
					Action<GraphNode> <>9__3;
					for (int m = graphs.Length - 1; m >= 0; m--)
					{
						NavGraph navGraph = graphs[m];
						Action<GraphNode> action;
						if ((action = <>9__3) == null)
						{
							action = (<>9__3 = delegate(GraphNode node)
							{
								int num6 = node.NodeIndex * this.pivotCount + pivotIndex;
								this.EnsureCapacity(num6);
								PathNode pathNode = ((IPathInternals)floodPath).PathHandler.GetPathNode(node);
								if (costOffset > 0u)
								{
									this.costs[num6] = ((pathNode.pathID == floodPath.pathID && pathNode.parent != null) ? Math.Max(pathNode.parent.G - costOffset, 0u) : 0u);
									return;
								}
								this.costs[num6] = ((pathNode.pathID == floodPath.pathID) ? pathNode.G : 0u);
							});
						}
						navGraph.GetNodes(action);
					}
					if (this.mode == HeuristicOptimizationMode.RandomSpreadOut && pivotIndex < this.pivots.Length - 1)
					{
						if (this.pivots[pivotIndex + 1] == null)
						{
							int num = -1;
							uint num2 = 0u;
							int num3 = this.maxNodeIndex / this.pivotCount;
							for (int n = 1; n < num3; n++)
							{
								uint num4 = 1073741824u;
								for (int num5 = 0; num5 <= pivotIndex; num5++)
								{
									num4 = Math.Min(num4, this.costs[n * this.pivotCount + num5]);
								}
								GraphNode node2 = ((IPathInternals)floodPath).PathHandler.GetPathNode(n).node;
								if ((num4 > num2 || num == -1) && node2 != null && !node2.Destroyed && node2.Walkable)
								{
									num = n;
									num2 = num4;
								}
							}
							if (num == -1)
							{
								Debug.LogError("Failed generating random pivot points for heuristic optimizations");
								return;
							}
							this.pivots[pivotIndex + 1] = ((IPathInternals)floodPath).PathHandler.GetPathNode(num).node;
						}
						startCostCalculation(pivotIndex + 1);
					}
					_p.Release(this, false);
				};
				AstarPath.StartPath(floodPath, true);
			};
			if (this.mode != HeuristicOptimizationMode.RandomSpreadOut)
			{
				for (int k = 0; k < this.pivots.Length; k++)
				{
					startCostCalculation(k);
				}
			}
			else
			{
				startCostCalculation(0);
			}
			this.dirty = false;
		}

		// Token: 0x06002608 RID: 9736 RVA: 0x001A404C File Offset: 0x001A224C
		private void ApplyGridGraphEndpointSpecialCase()
		{
			NavGraph[] graphs = AstarPath.active.graphs;
			for (int i = 0; i < graphs.Length; i++)
			{
				GridGraph gridGraph = graphs[i] as GridGraph;
				if (gridGraph != null)
				{
					GridNode[] nodes = gridGraph.nodes;
					int num = (gridGraph.neighbours == NumNeighbours.Four) ? 4 : ((gridGraph.neighbours == NumNeighbours.Eight) ? 8 : 6);
					for (int j = 0; j < gridGraph.depth; j++)
					{
						for (int k = 0; k < gridGraph.width; k++)
						{
							GridNode gridNode = nodes[j * gridGraph.width + k];
							if (!gridNode.Walkable)
							{
								int num2 = gridNode.NodeIndex * this.pivotCount;
								for (int l = 0; l < this.pivotCount; l++)
								{
									this.costs[num2 + l] = uint.MaxValue;
								}
								for (int m = 0; m < num; m++)
								{
									int num3;
									int num4;
									if (gridGraph.neighbours == NumNeighbours.Six)
									{
										num3 = k + gridGraph.neighbourXOffsets[GridGraph.hexagonNeighbourIndices[m]];
										num4 = j + gridGraph.neighbourZOffsets[GridGraph.hexagonNeighbourIndices[m]];
									}
									else
									{
										num3 = k + gridGraph.neighbourXOffsets[m];
										num4 = j + gridGraph.neighbourZOffsets[m];
									}
									if (num3 >= 0 && num4 >= 0 && num3 < gridGraph.width && num4 < gridGraph.depth)
									{
										GridNode gridNode2 = gridGraph.nodes[num4 * gridGraph.width + num3];
										if (gridNode2.Walkable)
										{
											for (int n = 0; n < this.pivotCount; n++)
											{
												uint val = this.costs[gridNode2.NodeIndex * this.pivotCount + n] + gridGraph.neighbourCosts[m];
												this.costs[num2 + n] = Math.Min(this.costs[num2 + n], val);
											}
										}
									}
								}
								for (int num5 = 0; num5 < this.pivotCount; num5++)
								{
									if (this.costs[num2 + num5] == 4294967295u)
									{
										this.costs[num2 + num5] = 0u;
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06002609 RID: 9737 RVA: 0x001A4260 File Offset: 0x001A2460
		public void OnDrawGizmos()
		{
			if (this.pivots != null)
			{
				for (int i = 0; i < this.pivots.Length; i++)
				{
					Gizmos.color = new Color(0.62352943f, 0.36862746f, 0.7607843f, 0.8f);
					if (this.pivots[i] != null && !this.pivots[i].Destroyed)
					{
						Gizmos.DrawCube((Vector3)this.pivots[i].position, Vector3.one);
					}
				}
			}
		}

		// Token: 0x040040FA RID: 16634
		public HeuristicOptimizationMode mode;

		// Token: 0x040040FB RID: 16635
		public int seed;

		// Token: 0x040040FC RID: 16636
		public Transform pivotPointRoot;

		// Token: 0x040040FD RID: 16637
		public int spreadOutCount = 1;

		// Token: 0x040040FE RID: 16638
		[NonSerialized]
		public bool dirty;

		// Token: 0x040040FF RID: 16639
		private uint[] costs = new uint[8];

		// Token: 0x04004100 RID: 16640
		private int maxNodeIndex;

		// Token: 0x04004101 RID: 16641
		private int pivotCount;

		// Token: 0x04004102 RID: 16642
		private GraphNode[] pivots;

		// Token: 0x04004103 RID: 16643
		private const uint ra = 12820163u;

		// Token: 0x04004104 RID: 16644
		private const uint rc = 1140671485u;

		// Token: 0x04004105 RID: 16645
		private uint rval;

		// Token: 0x04004106 RID: 16646
		private object lockObj = new object();
	}
}
