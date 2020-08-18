using System;
using System.Collections.Generic;
using Pathfinding.Serialization;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200056E RID: 1390
	public class GridNode : GridNodeBase
	{
		// Token: 0x06002558 RID: 9560 RVA: 0x0019CB01 File Offset: 0x0019AD01
		public GridNode(AstarPath astar) : base(astar)
		{
		}

		// Token: 0x06002559 RID: 9561 RVA: 0x0019FC39 File Offset: 0x0019DE39
		public static GridGraph GetGridGraph(uint graphIndex)
		{
			return GridNode._gridGraphs[(int)graphIndex];
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x0019FC44 File Offset: 0x0019DE44
		public static void SetGridGraph(int graphIndex, GridGraph graph)
		{
			if (GridNode._gridGraphs.Length <= graphIndex)
			{
				GridGraph[] array = new GridGraph[graphIndex + 1];
				for (int i = 0; i < GridNode._gridGraphs.Length; i++)
				{
					array[i] = GridNode._gridGraphs[i];
				}
				GridNode._gridGraphs = array;
			}
			GridNode._gridGraphs[graphIndex] = graph;
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x0600255B RID: 9563 RVA: 0x0019FC8E File Offset: 0x0019DE8E
		// (set) Token: 0x0600255C RID: 9564 RVA: 0x0019FC96 File Offset: 0x0019DE96
		internal ushort InternalGridFlags
		{
			get
			{
				return this.gridFlags;
			}
			set
			{
				this.gridFlags = value;
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x0600255D RID: 9565 RVA: 0x0019FC9F File Offset: 0x0019DE9F
		public override bool HasConnectionsToAllEightNeighbours
		{
			get
			{
				return (this.InternalGridFlags & 255) == 255;
			}
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x0019FCB4 File Offset: 0x0019DEB4
		public bool HasConnectionInDirection(int dir)
		{
			return (this.gridFlags >> dir & 1) != 0;
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x0019FCC6 File Offset: 0x0019DEC6
		[Obsolete("Use HasConnectionInDirection")]
		public bool GetConnectionInternal(int dir)
		{
			return this.HasConnectionInDirection(dir);
		}

		// Token: 0x06002560 RID: 9568 RVA: 0x0019FCCF File Offset: 0x0019DECF
		public void SetConnectionInternal(int dir, bool value)
		{
			this.gridFlags = (ushort)(((int)this.gridFlags & ~(1 << dir)) | (value ? 1 : 0) << (dir & 31));
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x0019FCF3 File Offset: 0x0019DEF3
		public void SetAllConnectionInternal(int connections)
		{
			this.gridFlags = (ushort)(((int)this.gridFlags & -256) | connections);
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x0019FD0A File Offset: 0x0019DF0A
		public void ResetConnectionsInternal()
		{
			this.gridFlags = (ushort)((int)this.gridFlags & -256);
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06002563 RID: 9571 RVA: 0x0019FD1F File Offset: 0x0019DF1F
		// (set) Token: 0x06002564 RID: 9572 RVA: 0x0019FD30 File Offset: 0x0019DF30
		public bool EdgeNode
		{
			get
			{
				return (this.gridFlags & 1024) > 0;
			}
			set
			{
				this.gridFlags = (ushort)(((int)this.gridFlags & -1025) | (value ? 1024 : 0));
			}
		}

		// Token: 0x06002565 RID: 9573 RVA: 0x0019FD54 File Offset: 0x0019DF54
		public override GridNodeBase GetNeighbourAlongDirection(int direction)
		{
			if (this.HasConnectionInDirection(direction))
			{
				GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
				return gridGraph.nodes[base.NodeInGridIndex + gridGraph.neighbourOffsets[direction]];
			}
			return null;
		}

		// Token: 0x06002566 RID: 9574 RVA: 0x0019FD90 File Offset: 0x0019DF90
		public override void ClearConnections(bool alsoReverse)
		{
			if (alsoReverse)
			{
				for (int i = 0; i < 8; i++)
				{
					GridNode gridNode = this.GetNeighbourAlongDirection(i) as GridNode;
					if (gridNode != null)
					{
						gridNode.SetConnectionInternal((i < 4) ? ((i + 2) % 4) : ((i - 2) % 4 + 4), false);
					}
				}
			}
			this.ResetConnectionsInternal();
		}

		// Token: 0x06002567 RID: 9575 RVA: 0x0019FDDC File Offset: 0x0019DFDC
		public override void GetConnections(Action<GraphNode> action)
		{
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			GridNode[] nodes = gridGraph.nodes;
			for (int i = 0; i < 8; i++)
			{
				if (this.HasConnectionInDirection(i))
				{
					GridNode gridNode = nodes[base.NodeInGridIndex + neighbourOffsets[i]];
					if (gridNode != null)
					{
						action(gridNode);
					}
				}
			}
		}

		// Token: 0x06002568 RID: 9576 RVA: 0x0019FE30 File Offset: 0x0019E030
		public Vector3 ClosestPointOnNode(Vector3 p)
		{
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			p = gridGraph.transform.InverseTransform(p);
			int num = base.NodeInGridIndex % gridGraph.width;
			int num2 = base.NodeInGridIndex / gridGraph.width;
			float y = gridGraph.transform.InverseTransform((Vector3)this.position).y;
			Vector3 point = new Vector3(Mathf.Clamp(p.x, (float)num, (float)num + 1f), y, Mathf.Clamp(p.z, (float)num2, (float)num2 + 1f));
			return gridGraph.transform.Transform(point);
		}

		// Token: 0x06002569 RID: 9577 RVA: 0x0019FED0 File Offset: 0x0019E0D0
		public override bool GetPortal(GraphNode other, List<Vector3> left, List<Vector3> right, bool backwards)
		{
			if (backwards)
			{
				return true;
			}
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			GridNode[] nodes = gridGraph.nodes;
			for (int i = 0; i < 4; i++)
			{
				if (this.HasConnectionInDirection(i) && other == nodes[base.NodeInGridIndex + neighbourOffsets[i]])
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
			for (int j = 4; j < 8; j++)
			{
				if (this.HasConnectionInDirection(j) && other == nodes[base.NodeInGridIndex + neighbourOffsets[j]])
				{
					bool flag = false;
					bool flag2 = false;
					if (this.HasConnectionInDirection(j - 4))
					{
						GridNode gridNode = nodes[base.NodeInGridIndex + neighbourOffsets[j - 4]];
						if (gridNode.Walkable && gridNode.HasConnectionInDirection((j - 4 + 1) % 4))
						{
							flag = true;
						}
					}
					if (this.HasConnectionInDirection((j - 4 + 1) % 4))
					{
						GridNode gridNode2 = nodes[base.NodeInGridIndex + neighbourOffsets[(j - 4 + 1) % 4]];
						if (gridNode2.Walkable && gridNode2.HasConnectionInDirection(j - 4))
						{
							flag2 = true;
						}
					}
					Vector3 a2 = (Vector3)(this.position + other.position) * 0.5f;
					Vector3 vector2 = Vector3.Cross(gridGraph.collision.up, (Vector3)(other.position - this.position));
					vector2.Normalize();
					vector2 *= gridGraph.nodeSize * 1.4142f;
					left.Add(a2 - (flag2 ? vector2 : Vector3.zero));
					right.Add(a2 + (flag ? vector2 : Vector3.zero));
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600256A RID: 9578 RVA: 0x001A0104 File Offset: 0x0019E304
		public override void FloodFill(Stack<GraphNode> stack, uint region)
		{
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			GridNode[] nodes = gridGraph.nodes;
			int nodeInGridIndex = base.NodeInGridIndex;
			for (int i = 0; i < 8; i++)
			{
				if (this.HasConnectionInDirection(i))
				{
					GridNode gridNode = nodes[nodeInGridIndex + neighbourOffsets[i]];
					if (gridNode != null && gridNode.Area != region)
					{
						gridNode.Area = region;
						stack.Push(gridNode);
					}
				}
			}
		}

		// Token: 0x0600256B RID: 9579 RVA: 0x001A016C File Offset: 0x0019E36C
		public override void UpdateRecursiveG(Path path, PathNode pathNode, PathHandler handler)
		{
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			GridNode[] nodes = gridGraph.nodes;
			pathNode.UpdateG(path);
			handler.heap.Add(pathNode);
			ushort pathID = handler.PathID;
			int nodeInGridIndex = base.NodeInGridIndex;
			for (int i = 0; i < 8; i++)
			{
				if (this.HasConnectionInDirection(i))
				{
					GridNode gridNode = nodes[nodeInGridIndex + neighbourOffsets[i]];
					PathNode pathNode2 = handler.GetPathNode(gridNode);
					if (pathNode2.parent == pathNode && pathNode2.pathID == pathID)
					{
						gridNode.UpdateRecursiveG(path, pathNode2, handler);
					}
				}
			}
		}

		// Token: 0x0600256C RID: 9580 RVA: 0x001A0200 File Offset: 0x0019E400
		public override void Open(Path path, PathNode pathNode, PathHandler handler)
		{
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			ushort pathID = handler.PathID;
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			uint[] neighbourCosts = gridGraph.neighbourCosts;
			GridNode[] nodes = gridGraph.nodes;
			int nodeInGridIndex = base.NodeInGridIndex;
			for (int i = 0; i < 8; i++)
			{
				if (this.HasConnectionInDirection(i))
				{
					GridNode gridNode = nodes[nodeInGridIndex + neighbourOffsets[i]];
					if (path.CanTraverse(gridNode))
					{
						PathNode pathNode2 = handler.GetPathNode(gridNode);
						uint num = neighbourCosts[i];
						if (pathNode2.pathID != pathID)
						{
							pathNode2.parent = pathNode;
							pathNode2.pathID = pathID;
							pathNode2.cost = num;
							pathNode2.H = path.CalculateHScore(gridNode);
							pathNode2.UpdateG(path);
							handler.heap.Add(pathNode2);
						}
						else if (pathNode.G + num + path.GetTraversalCost(gridNode) < pathNode2.G)
						{
							pathNode2.cost = num;
							pathNode2.parent = pathNode;
							gridNode.UpdateRecursiveG(path, pathNode2, handler);
						}
					}
				}
			}
		}

		// Token: 0x0600256D RID: 9581 RVA: 0x001A0306 File Offset: 0x0019E506
		public override void SerializeNode(GraphSerializationContext ctx)
		{
			base.SerializeNode(ctx);
			ctx.SerializeInt3(this.position);
			ctx.writer.Write(this.gridFlags);
		}

		// Token: 0x0600256E RID: 9582 RVA: 0x001A032C File Offset: 0x0019E52C
		public override void DeserializeNode(GraphSerializationContext ctx)
		{
			base.DeserializeNode(ctx);
			this.position = ctx.DeserializeInt3();
			this.gridFlags = ctx.reader.ReadUInt16();
		}

		// Token: 0x040040B1 RID: 16561
		private static GridGraph[] _gridGraphs = new GridGraph[0];

		// Token: 0x040040B2 RID: 16562
		private const int GridFlagsConnectionOffset = 0;

		// Token: 0x040040B3 RID: 16563
		private const int GridFlagsConnectionBit0 = 1;

		// Token: 0x040040B4 RID: 16564
		private const int GridFlagsConnectionMask = 255;

		// Token: 0x040040B5 RID: 16565
		private const int GridFlagsEdgeNodeOffset = 10;

		// Token: 0x040040B6 RID: 16566
		private const int GridFlagsEdgeNodeMask = 1024;
	}
}
