using System;
using System.Collections.Generic;
using Pathfinding.Serialization;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000572 RID: 1394
	public class TriangleMeshNode : MeshNode
	{
		// Token: 0x06002594 RID: 9620 RVA: 0x001A09D4 File Offset: 0x0019EBD4
		public TriangleMeshNode(AstarPath astar) : base(astar)
		{
		}

		// Token: 0x06002595 RID: 9621 RVA: 0x001A09DD File Offset: 0x0019EBDD
		public static INavmeshHolder GetNavmeshHolder(uint graphIndex)
		{
			return TriangleMeshNode._navmeshHolders[(int)graphIndex];
		}

		// Token: 0x06002596 RID: 9622 RVA: 0x001A09E8 File Offset: 0x0019EBE8
		public static void SetNavmeshHolder(int graphIndex, INavmeshHolder graph)
		{
			object obj = TriangleMeshNode.lockObject;
			lock (obj)
			{
				if (graphIndex >= TriangleMeshNode._navmeshHolders.Length)
				{
					INavmeshHolder[] array = new INavmeshHolder[graphIndex + 1];
					TriangleMeshNode._navmeshHolders.CopyTo(array, 0);
					TriangleMeshNode._navmeshHolders = array;
				}
				TriangleMeshNode._navmeshHolders[graphIndex] = graph;
			}
		}

		// Token: 0x06002597 RID: 9623 RVA: 0x001A0A50 File Offset: 0x0019EC50
		public void UpdatePositionFromVertices()
		{
			Int3 lhs;
			Int3 rhs;
			Int3 rhs2;
			this.GetVertices(out lhs, out rhs, out rhs2);
			this.position = (lhs + rhs + rhs2) * 0.333333f;
		}

		// Token: 0x06002598 RID: 9624 RVA: 0x001A0A86 File Offset: 0x0019EC86
		public int GetVertexIndex(int i)
		{
			if (i == 0)
			{
				return this.v0;
			}
			if (i != 1)
			{
				return this.v2;
			}
			return this.v1;
		}

		// Token: 0x06002599 RID: 9625 RVA: 0x001A0AA3 File Offset: 0x0019ECA3
		public int GetVertexArrayIndex(int i)
		{
			return TriangleMeshNode.GetNavmeshHolder(base.GraphIndex).GetVertexArrayIndex((i == 0) ? this.v0 : ((i == 1) ? this.v1 : this.v2));
		}

		// Token: 0x0600259A RID: 9626 RVA: 0x001A0AD4 File Offset: 0x0019ECD4
		public void GetVertices(out Int3 v0, out Int3 v1, out Int3 v2)
		{
			INavmeshHolder navmeshHolder = TriangleMeshNode.GetNavmeshHolder(base.GraphIndex);
			v0 = navmeshHolder.GetVertex(this.v0);
			v1 = navmeshHolder.GetVertex(this.v1);
			v2 = navmeshHolder.GetVertex(this.v2);
		}

		// Token: 0x0600259B RID: 9627 RVA: 0x001A0B24 File Offset: 0x0019ED24
		public void GetVerticesInGraphSpace(out Int3 v0, out Int3 v1, out Int3 v2)
		{
			INavmeshHolder navmeshHolder = TriangleMeshNode.GetNavmeshHolder(base.GraphIndex);
			v0 = navmeshHolder.GetVertexInGraphSpace(this.v0);
			v1 = navmeshHolder.GetVertexInGraphSpace(this.v1);
			v2 = navmeshHolder.GetVertexInGraphSpace(this.v2);
		}

		// Token: 0x0600259C RID: 9628 RVA: 0x001A0B73 File Offset: 0x0019ED73
		public override Int3 GetVertex(int i)
		{
			return TriangleMeshNode.GetNavmeshHolder(base.GraphIndex).GetVertex(this.GetVertexIndex(i));
		}

		// Token: 0x0600259D RID: 9629 RVA: 0x001A0B8C File Offset: 0x0019ED8C
		public Int3 GetVertexInGraphSpace(int i)
		{
			return TriangleMeshNode.GetNavmeshHolder(base.GraphIndex).GetVertexInGraphSpace(this.GetVertexIndex(i));
		}

		// Token: 0x0600259E RID: 9630 RVA: 0x000AD561 File Offset: 0x000AB761
		public override int GetVertexCount()
		{
			return 3;
		}

		// Token: 0x0600259F RID: 9631 RVA: 0x001A0BA8 File Offset: 0x0019EDA8
		public override Vector3 ClosestPointOnNode(Vector3 p)
		{
			Int3 ob;
			Int3 ob2;
			Int3 ob3;
			this.GetVertices(out ob, out ob2, out ob3);
			return Polygon.ClosestPointOnTriangle((Vector3)ob, (Vector3)ob2, (Vector3)ob3, p);
		}

		// Token: 0x060025A0 RID: 9632 RVA: 0x001A0BDC File Offset: 0x0019EDDC
		internal Int3 ClosestPointOnNodeXZInGraphSpace(Vector3 p)
		{
			Int3 @int;
			Int3 int2;
			Int3 int3;
			this.GetVerticesInGraphSpace(out @int, out int2, out int3);
			p = TriangleMeshNode.GetNavmeshHolder(base.GraphIndex).transform.InverseTransform(p);
			Int3 int4 = (Int3)Polygon.ClosestPointOnTriangleXZ((Vector3)@int, (Vector3)int2, (Vector3)int3, p);
			if (this.ContainsPointInGraphSpace(int4))
			{
				return int4;
			}
			for (int i = -1; i <= 1; i++)
			{
				for (int j = -1; j <= 1; j++)
				{
					if (i != 0 || j != 0)
					{
						Int3 int5 = new Int3(int4.x + i, int4.y, int4.z + j);
						if (this.ContainsPointInGraphSpace(int5))
						{
							return int5;
						}
					}
				}
			}
			long sqrMagnitudeLong = (@int - int4).sqrMagnitudeLong;
			long sqrMagnitudeLong2 = (int2 - int4).sqrMagnitudeLong;
			long sqrMagnitudeLong3 = (int3 - int4).sqrMagnitudeLong;
			if (sqrMagnitudeLong >= sqrMagnitudeLong2)
			{
				if (sqrMagnitudeLong2 >= sqrMagnitudeLong3)
				{
					return int3;
				}
				return int2;
			}
			else
			{
				if (sqrMagnitudeLong >= sqrMagnitudeLong3)
				{
					return int3;
				}
				return @int;
			}
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x001A0CDC File Offset: 0x0019EEDC
		public override Vector3 ClosestPointOnNodeXZ(Vector3 p)
		{
			Int3 ob;
			Int3 ob2;
			Int3 ob3;
			this.GetVertices(out ob, out ob2, out ob3);
			return Polygon.ClosestPointOnTriangleXZ((Vector3)ob, (Vector3)ob2, (Vector3)ob3, p);
		}

		// Token: 0x060025A2 RID: 9634 RVA: 0x001A0D0D File Offset: 0x0019EF0D
		public override bool ContainsPoint(Vector3 p)
		{
			return this.ContainsPointInGraphSpace((Int3)TriangleMeshNode.GetNavmeshHolder(base.GraphIndex).transform.InverseTransform(p));
		}

		// Token: 0x060025A3 RID: 9635 RVA: 0x001A0D30 File Offset: 0x0019EF30
		public override bool ContainsPointInGraphSpace(Int3 p)
		{
			Int3 @int;
			Int3 int2;
			Int3 int3;
			this.GetVerticesInGraphSpace(out @int, out int2, out int3);
			return (long)(int2.x - @int.x) * (long)(p.z - @int.z) - (long)(p.x - @int.x) * (long)(int2.z - @int.z) <= 0L && (long)(int3.x - int2.x) * (long)(p.z - int2.z) - (long)(p.x - int2.x) * (long)(int3.z - int2.z) <= 0L && (long)(@int.x - int3.x) * (long)(p.z - int3.z) - (long)(p.x - int3.x) * (long)(@int.z - int3.z) <= 0L;
		}

		// Token: 0x060025A4 RID: 9636 RVA: 0x001A0E10 File Offset: 0x0019F010
		public override void UpdateRecursiveG(Path path, PathNode pathNode, PathHandler handler)
		{
			pathNode.UpdateG(path);
			handler.heap.Add(pathNode);
			if (this.connections == null)
			{
				return;
			}
			for (int i = 0; i < this.connections.Length; i++)
			{
				GraphNode node = this.connections[i].node;
				PathNode pathNode2 = handler.GetPathNode(node);
				if (pathNode2.parent == pathNode && pathNode2.pathID == handler.PathID)
				{
					node.UpdateRecursiveG(path, pathNode2, handler);
				}
			}
		}

		// Token: 0x060025A5 RID: 9637 RVA: 0x001A0E88 File Offset: 0x0019F088
		public override void Open(Path path, PathNode pathNode, PathHandler handler)
		{
			if (this.connections == null)
			{
				return;
			}
			bool flag = pathNode.flag2;
			for (int i = this.connections.Length - 1; i >= 0; i--)
			{
				Connection connection = this.connections[i];
				GraphNode node = connection.node;
				if (path.CanTraverse(connection.node))
				{
					PathNode pathNode2 = handler.GetPathNode(connection.node);
					if (pathNode2 != pathNode.parent)
					{
						uint num = connection.cost;
						if (flag || pathNode2.flag2)
						{
							num = path.GetConnectionSpecialCost(this, connection.node, num);
						}
						if (pathNode2.pathID != handler.PathID)
						{
							pathNode2.node = connection.node;
							pathNode2.parent = pathNode;
							pathNode2.pathID = handler.PathID;
							pathNode2.cost = num;
							pathNode2.H = path.CalculateHScore(node);
							pathNode2.UpdateG(path);
							handler.heap.Add(pathNode2);
						}
						else if (pathNode.G + num + path.GetTraversalCost(node) < pathNode2.G)
						{
							pathNode2.cost = num;
							pathNode2.parent = pathNode;
							node.UpdateRecursiveG(path, pathNode2, handler);
						}
					}
				}
			}
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x001A0FBC File Offset: 0x0019F1BC
		public int SharedEdge(GraphNode other)
		{
			int result = -1;
			for (int i = 0; i < this.connections.Length; i++)
			{
				if (this.connections[i].node == other)
				{
					result = (int)this.connections[i].shapeEdge;
				}
			}
			return result;
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x001A1008 File Offset: 0x0019F208
		public override bool GetPortal(GraphNode toNode, List<Vector3> left, List<Vector3> right, bool backwards)
		{
			int num;
			int num2;
			return this.GetPortal(toNode, left, right, backwards, out num, out num2);
		}

		// Token: 0x060025A8 RID: 9640 RVA: 0x001A1024 File Offset: 0x0019F224
		public bool GetPortal(GraphNode toNode, List<Vector3> left, List<Vector3> right, bool backwards, out int aIndex, out int bIndex)
		{
			aIndex = -1;
			bIndex = -1;
			if (backwards || toNode.GraphIndex != base.GraphIndex)
			{
				return false;
			}
			TriangleMeshNode triangleMeshNode = toNode as TriangleMeshNode;
			int num = this.SharedEdge(triangleMeshNode);
			if (num == 255)
			{
				return false;
			}
			if (num == -1)
			{
				for (int i = 0; i < this.connections.Length; i++)
				{
					if (this.connections[i].node.GraphIndex != base.GraphIndex)
					{
						NodeLink3Node nodeLink3Node = this.connections[i].node as NodeLink3Node;
						if (nodeLink3Node != null && nodeLink3Node.GetOther(this) == triangleMeshNode)
						{
							nodeLink3Node.GetPortal(triangleMeshNode, left, right, false);
							return true;
						}
					}
				}
				return false;
			}
			aIndex = num;
			bIndex = (num + 1) % this.GetVertexCount();
			Int3 vertex = this.GetVertex(num);
			Int3 vertex2 = this.GetVertex((num + 1) % this.GetVertexCount());
			int num2 = this.GetVertexIndex(0) >> 12 & 524287;
			int num3 = triangleMeshNode.GetVertexIndex(0) >> 12 & 524287;
			if (num2 != num3)
			{
				INavmeshHolder navmeshHolder = TriangleMeshNode.GetNavmeshHolder(base.GraphIndex);
				int num4;
				int num5;
				navmeshHolder.GetTileCoordinates(num2, out num4, out num5);
				int num6;
				int num7;
				navmeshHolder.GetTileCoordinates(num3, out num6, out num7);
				int i2;
				if (Math.Abs(num4 - num6) == 1)
				{
					i2 = 2;
				}
				else
				{
					if (Math.Abs(num5 - num7) != 1)
					{
						return false;
					}
					i2 = 0;
				}
				int num8 = triangleMeshNode.SharedEdge(this);
				if (num8 == 255)
				{
					throw new Exception("Connection used edge in one direction, but not in the other direction. Has the wrong overload of AddConnection been used?");
				}
				if (num8 != -1)
				{
					int num9 = Math.Min(vertex[i2], vertex2[i2]);
					int num10 = Math.Max(vertex[i2], vertex2[i2]);
					Int3 vertex3 = triangleMeshNode.GetVertex(num8);
					Int3 vertex4 = triangleMeshNode.GetVertex((num8 + 1) % triangleMeshNode.GetVertexCount());
					num9 = Math.Max(num9, Math.Min(vertex3[i2], vertex4[i2]));
					num10 = Math.Min(num10, Math.Max(vertex3[i2], vertex4[i2]));
					if (vertex[i2] < vertex2[i2])
					{
						vertex[i2] = num9;
						vertex2[i2] = num10;
					}
					else
					{
						vertex[i2] = num10;
						vertex2[i2] = num9;
					}
				}
			}
			if (left != null)
			{
				left.Add((Vector3)vertex);
				right.Add((Vector3)vertex2);
			}
			return true;
		}

		// Token: 0x060025A9 RID: 9641 RVA: 0x001A128C File Offset: 0x0019F48C
		public override float SurfaceArea()
		{
			INavmeshHolder navmeshHolder = TriangleMeshNode.GetNavmeshHolder(base.GraphIndex);
			return (float)Math.Abs(VectorMath.SignedTriangleAreaTimes2XZ(navmeshHolder.GetVertex(this.v0), navmeshHolder.GetVertex(this.v1), navmeshHolder.GetVertex(this.v2))) * 0.5f;
		}

		// Token: 0x060025AA RID: 9642 RVA: 0x001A12DC File Offset: 0x0019F4DC
		public override Vector3 RandomPointOnSurface()
		{
			float value;
			float value2;
			do
			{
				value = UnityEngine.Random.value;
				value2 = UnityEngine.Random.value;
			}
			while (value + value2 > 1f);
			INavmeshHolder navmeshHolder = TriangleMeshNode.GetNavmeshHolder(base.GraphIndex);
			return (Vector3)(navmeshHolder.GetVertex(this.v1) - navmeshHolder.GetVertex(this.v0)) * value + (Vector3)(navmeshHolder.GetVertex(this.v2) - navmeshHolder.GetVertex(this.v0)) * value2 + (Vector3)navmeshHolder.GetVertex(this.v0);
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x001A1376 File Offset: 0x0019F576
		public override void SerializeNode(GraphSerializationContext ctx)
		{
			base.SerializeNode(ctx);
			ctx.writer.Write(this.v0);
			ctx.writer.Write(this.v1);
			ctx.writer.Write(this.v2);
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x001A13B2 File Offset: 0x0019F5B2
		public override void DeserializeNode(GraphSerializationContext ctx)
		{
			base.DeserializeNode(ctx);
			this.v0 = ctx.reader.ReadInt32();
			this.v1 = ctx.reader.ReadInt32();
			this.v2 = ctx.reader.ReadInt32();
		}

		// Token: 0x040040C1 RID: 16577
		public int v0;

		// Token: 0x040040C2 RID: 16578
		public int v1;

		// Token: 0x040040C3 RID: 16579
		public int v2;

		// Token: 0x040040C4 RID: 16580
		protected static INavmeshHolder[] _navmeshHolders = new INavmeshHolder[0];

		// Token: 0x040040C5 RID: 16581
		protected static readonly object lockObject = new object();
	}
}
