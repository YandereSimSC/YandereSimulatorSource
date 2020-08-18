using System;
using System.Collections.Generic;
using Pathfinding.Serialization;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000569 RID: 1385
	public class LevelGridNode : GridNodeBase
	{
		// Token: 0x060024F8 RID: 9464 RVA: 0x0019CB01 File Offset: 0x0019AD01
		public LevelGridNode(AstarPath astar) : base(astar)
		{
		}

		// Token: 0x060024F9 RID: 9465 RVA: 0x0019CB0A File Offset: 0x0019AD0A
		public static LayerGridGraph GetGridGraph(uint graphIndex)
		{
			return LevelGridNode._gridGraphs[(int)graphIndex];
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x0019CB14 File Offset: 0x0019AD14
		public static void SetGridGraph(int graphIndex, LayerGridGraph graph)
		{
			GridNode.SetGridGraph(graphIndex, graph);
			if (LevelGridNode._gridGraphs.Length <= graphIndex)
			{
				LayerGridGraph[] array = new LayerGridGraph[graphIndex + 1];
				for (int i = 0; i < LevelGridNode._gridGraphs.Length; i++)
				{
					array[i] = LevelGridNode._gridGraphs[i];
				}
				LevelGridNode._gridGraphs = array;
			}
			LevelGridNode._gridGraphs[graphIndex] = graph;
		}

		// Token: 0x060024FB RID: 9467 RVA: 0x0019CB65 File Offset: 0x0019AD65
		public void ResetAllGridConnections()
		{
			this.gridConnections = ulong.MaxValue;
		}

		// Token: 0x060024FC RID: 9468 RVA: 0x0019CB6F File Offset: 0x0019AD6F
		public bool HasAnyGridConnections()
		{
			return this.gridConnections != ulong.MaxValue;
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x060024FD RID: 9469 RVA: 0x0002D171 File Offset: 0x0002B371
		public override bool HasConnectionsToAllEightNeighbours
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x060024FE RID: 9470 RVA: 0x0019CB7E File Offset: 0x0019AD7E
		// (set) Token: 0x060024FF RID: 9471 RVA: 0x0019CB89 File Offset: 0x0019AD89
		public int LayerCoordinateInGrid
		{
			get
			{
				return this.nodeInGridIndex >> 24;
			}
			set
			{
				this.nodeInGridIndex = ((this.nodeInGridIndex & 16777215) | value << 24);
			}
		}

		// Token: 0x06002500 RID: 9472 RVA: 0x0019CBA2 File Offset: 0x0019ADA2
		public void SetPosition(Int3 position)
		{
			this.position = position;
		}

		// Token: 0x06002501 RID: 9473 RVA: 0x0019CBAB File Offset: 0x0019ADAB
		public override int GetGizmoHashCode()
		{
			return base.GetGizmoHashCode() ^ (int)(805306457UL * this.gridConnections);
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x0019CBC4 File Offset: 0x0019ADC4
		public override GridNodeBase GetNeighbourAlongDirection(int direction)
		{
			if (this.GetConnection(direction))
			{
				LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
				return gridGraph.nodes[base.NodeInGridIndex + gridGraph.neighbourOffsets[direction] + gridGraph.lastScannedWidth * gridGraph.lastScannedDepth * this.GetConnectionValue(direction)];
			}
			return null;
		}

		// Token: 0x06002503 RID: 9475 RVA: 0x0019CC14 File Offset: 0x0019AE14
		public override void ClearConnections(bool alsoReverse)
		{
			if (alsoReverse)
			{
				LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
				int[] neighbourOffsets = gridGraph.neighbourOffsets;
				LevelGridNode[] nodes = gridGraph.nodes;
				for (int i = 0; i < 4; i++)
				{
					int connectionValue = this.GetConnectionValue(i);
					if (connectionValue != 255)
					{
						LevelGridNode levelGridNode = nodes[base.NodeInGridIndex + neighbourOffsets[i] + gridGraph.lastScannedWidth * gridGraph.lastScannedDepth * connectionValue];
						if (levelGridNode != null)
						{
							levelGridNode.SetConnectionValue((i + 2) % 4, 255);
						}
					}
				}
			}
			this.ResetAllGridConnections();
		}

		// Token: 0x06002504 RID: 9476 RVA: 0x0019CC98 File Offset: 0x0019AE98
		public override void GetConnections(Action<GraphNode> action)
		{
			LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			LevelGridNode[] nodes = gridGraph.nodes;
			int nodeInGridIndex = base.NodeInGridIndex;
			for (int i = 0; i < 4; i++)
			{
				int connectionValue = this.GetConnectionValue(i);
				if (connectionValue != 255)
				{
					LevelGridNode levelGridNode = nodes[nodeInGridIndex + neighbourOffsets[i] + gridGraph.lastScannedWidth * gridGraph.lastScannedDepth * connectionValue];
					if (levelGridNode != null)
					{
						action(levelGridNode);
					}
				}
			}
		}

		// Token: 0x06002505 RID: 9477 RVA: 0x0019CD10 File Offset: 0x0019AF10
		public override void FloodFill(Stack<GraphNode> stack, uint region)
		{
			int nodeInGridIndex = base.NodeInGridIndex;
			LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			LevelGridNode[] nodes = gridGraph.nodes;
			for (int i = 0; i < 4; i++)
			{
				int connectionValue = this.GetConnectionValue(i);
				if (connectionValue != 255)
				{
					LevelGridNode levelGridNode = nodes[nodeInGridIndex + neighbourOffsets[i] + gridGraph.lastScannedWidth * gridGraph.lastScannedDepth * connectionValue];
					if (levelGridNode != null && levelGridNode.Area != region)
					{
						levelGridNode.Area = region;
						stack.Push(levelGridNode);
					}
				}
			}
		}

		// Token: 0x06002506 RID: 9478 RVA: 0x0019CD9A File Offset: 0x0019AF9A
		public bool GetConnection(int i)
		{
			return (this.gridConnections >> i * 8 & 255UL) != 255UL;
		}

		// Token: 0x06002507 RID: 9479 RVA: 0x0019CDBB File Offset: 0x0019AFBB
		public void SetConnectionValue(int dir, int value)
		{
			this.gridConnections = ((this.gridConnections & ~(255UL << dir * 8)) | (ulong)((ulong)((long)value) << dir * 8));
		}

		// Token: 0x06002508 RID: 9480 RVA: 0x0019CDE2 File Offset: 0x0019AFE2
		public int GetConnectionValue(int dir)
		{
			return (int)(this.gridConnections >> dir * 8 & 255UL);
		}

		// Token: 0x06002509 RID: 9481 RVA: 0x0019CDFC File Offset: 0x0019AFFC
		public override bool GetPortal(GraphNode other, List<Vector3> left, List<Vector3> right, bool backwards)
		{
			if (backwards)
			{
				return true;
			}
			LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			LevelGridNode[] nodes = gridGraph.nodes;
			int nodeInGridIndex = base.NodeInGridIndex;
			for (int i = 0; i < 4; i++)
			{
				int connectionValue = this.GetConnectionValue(i);
				if (connectionValue != 255 && other == nodes[nodeInGridIndex + neighbourOffsets[i] + gridGraph.lastScannedWidth * gridGraph.lastScannedDepth * connectionValue])
				{
					Vector3 a = (Vector3)(this.position + other.position) * 0.5f;
					Vector3 vector = Vector3.Cross(gridGraph.collision.up, (Vector3)(other.position - this.position));
					vector.Normalize();
					vector *= gridGraph.nodeSize * 0.5f;
					left.Add(a - vector);
					right.Add(a + vector);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600250A RID: 9482 RVA: 0x0019CF04 File Offset: 0x0019B104
		public override void UpdateRecursiveG(Path path, PathNode pathNode, PathHandler handler)
		{
			handler.heap.Add(pathNode);
			pathNode.UpdateG(path);
			LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			LevelGridNode[] nodes = gridGraph.nodes;
			int nodeInGridIndex = base.NodeInGridIndex;
			for (int i = 0; i < 4; i++)
			{
				int connectionValue = this.GetConnectionValue(i);
				if (connectionValue != 255)
				{
					LevelGridNode levelGridNode = nodes[nodeInGridIndex + neighbourOffsets[i] + gridGraph.lastScannedWidth * gridGraph.lastScannedDepth * connectionValue];
					PathNode pathNode2 = handler.GetPathNode(levelGridNode);
					if (pathNode2 != null && pathNode2.parent == pathNode && pathNode2.pathID == handler.PathID)
					{
						levelGridNode.UpdateRecursiveG(path, pathNode2, handler);
					}
				}
			}
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x0019CFB8 File Offset: 0x0019B1B8
		public override void Open(Path path, PathNode pathNode, PathHandler handler)
		{
			LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			uint[] neighbourCosts = gridGraph.neighbourCosts;
			LevelGridNode[] nodes = gridGraph.nodes;
			int nodeInGridIndex = base.NodeInGridIndex;
			for (int i = 0; i < 4; i++)
			{
				int connectionValue = this.GetConnectionValue(i);
				if (connectionValue != 255)
				{
					GraphNode graphNode = nodes[nodeInGridIndex + neighbourOffsets[i] + gridGraph.lastScannedWidth * gridGraph.lastScannedDepth * connectionValue];
					if (path.CanTraverse(graphNode))
					{
						PathNode pathNode2 = handler.GetPathNode(graphNode);
						if (pathNode2.pathID != handler.PathID)
						{
							pathNode2.parent = pathNode;
							pathNode2.pathID = handler.PathID;
							pathNode2.cost = neighbourCosts[i];
							pathNode2.H = path.CalculateHScore(graphNode);
							pathNode2.UpdateG(path);
							handler.heap.Add(pathNode2);
						}
						else
						{
							uint num = neighbourCosts[i];
							if (pathNode.G + num + path.GetTraversalCost(graphNode) < pathNode2.G)
							{
								pathNode2.cost = num;
								pathNode2.parent = pathNode;
								graphNode.UpdateRecursiveG(path, pathNode2, handler);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600250C RID: 9484 RVA: 0x0019D0DF File Offset: 0x0019B2DF
		public override void SerializeNode(GraphSerializationContext ctx)
		{
			base.SerializeNode(ctx);
			ctx.SerializeInt3(this.position);
			ctx.writer.Write(this.gridFlags);
			ctx.writer.Write(this.gridConnections);
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x0019D118 File Offset: 0x0019B318
		public override void DeserializeNode(GraphSerializationContext ctx)
		{
			base.DeserializeNode(ctx);
			this.position = ctx.DeserializeInt3();
			this.gridFlags = ctx.reader.ReadUInt16();
			if (ctx.meta.version < AstarSerializer.V3_9_0)
			{
				this.gridConnections = ((ulong)ctx.reader.ReadUInt32() | 18446744069414584320UL);
				return;
			}
			this.gridConnections = ctx.reader.ReadUInt64();
		}

		// Token: 0x0400408F RID: 16527
		private static LayerGridGraph[] _gridGraphs = new LayerGridGraph[0];

		// Token: 0x04004090 RID: 16528
		public ulong gridConnections;

		// Token: 0x04004091 RID: 16529
		protected static LayerGridGraph[] gridGraphs;

		// Token: 0x04004092 RID: 16530
		public const int NoConnection = 255;

		// Token: 0x04004093 RID: 16531
		public const int ConnectionMask = 255;

		// Token: 0x04004094 RID: 16532
		private const int ConnectionStride = 8;

		// Token: 0x04004095 RID: 16533
		public const int MaxLayerCount = 255;
	}
}
