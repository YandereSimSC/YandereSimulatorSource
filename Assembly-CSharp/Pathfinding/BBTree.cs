using System;
using System.Diagnostics;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000575 RID: 1397
	public class BBTree : IAstarPooledObject
	{
		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x060025E5 RID: 9701 RVA: 0x001A2CF0 File Offset: 0x001A0EF0
		public Rect Size
		{
			get
			{
				if (this.count == 0)
				{
					return new Rect(0f, 0f, 0f, 0f);
				}
				IntRect rect = this.tree[0].rect;
				return Rect.MinMaxRect((float)rect.xmin * 0.001f, (float)rect.ymin * 0.001f, (float)rect.xmax * 0.001f, (float)rect.ymax * 0.001f);
			}
		}

		// Token: 0x060025E6 RID: 9702 RVA: 0x001A2D6C File Offset: 0x001A0F6C
		public void Clear()
		{
			this.count = 0;
			this.leafNodes = 0;
			if (this.tree != null)
			{
				ArrayPool<BBTree.BBTreeBox>.Release(ref this.tree, false);
			}
			if (this.nodeLookup != null)
			{
				for (int i = 0; i < this.nodeLookup.Length; i++)
				{
					this.nodeLookup[i] = null;
				}
				ArrayPool<TriangleMeshNode>.Release(ref this.nodeLookup, false);
			}
			this.tree = ArrayPool<BBTree.BBTreeBox>.Claim(0);
			this.nodeLookup = ArrayPool<TriangleMeshNode>.Claim(0);
		}

		// Token: 0x060025E7 RID: 9703 RVA: 0x001A2DE3 File Offset: 0x001A0FE3
		void IAstarPooledObject.OnEnterPool()
		{
			this.Clear();
		}

		// Token: 0x060025E8 RID: 9704 RVA: 0x001A2DEC File Offset: 0x001A0FEC
		private void EnsureCapacity(int c)
		{
			if (c > this.tree.Length)
			{
				BBTree.BBTreeBox[] array = ArrayPool<BBTree.BBTreeBox>.Claim(c);
				this.tree.CopyTo(array, 0);
				ArrayPool<BBTree.BBTreeBox>.Release(ref this.tree, false);
				this.tree = array;
			}
		}

		// Token: 0x060025E9 RID: 9705 RVA: 0x001A2E2C File Offset: 0x001A102C
		private void EnsureNodeCapacity(int c)
		{
			if (c > this.nodeLookup.Length)
			{
				TriangleMeshNode[] array = ArrayPool<TriangleMeshNode>.Claim(c);
				this.nodeLookup.CopyTo(array, 0);
				ArrayPool<TriangleMeshNode>.Release(ref this.nodeLookup, false);
				this.nodeLookup = array;
			}
		}

		// Token: 0x060025EA RID: 9706 RVA: 0x001A2E6C File Offset: 0x001A106C
		private int GetBox(IntRect rect)
		{
			if (this.count >= this.tree.Length)
			{
				this.EnsureCapacity(this.count + 1);
			}
			this.tree[this.count] = new BBTree.BBTreeBox(rect);
			this.count++;
			return this.count - 1;
		}

		// Token: 0x060025EB RID: 9707 RVA: 0x001A2EC4 File Offset: 0x001A10C4
		public void RebuildFrom(TriangleMeshNode[] nodes)
		{
			this.Clear();
			if (nodes.Length == 0)
			{
				return;
			}
			this.EnsureCapacity(Mathf.CeilToInt((float)nodes.Length * 2.1f));
			this.EnsureNodeCapacity(Mathf.CeilToInt((float)nodes.Length * 1.1f));
			int[] array = ArrayPool<int>.Claim(nodes.Length);
			for (int i = 0; i < nodes.Length; i++)
			{
				array[i] = i;
			}
			IntRect[] array2 = ArrayPool<IntRect>.Claim(nodes.Length);
			for (int j = 0; j < nodes.Length; j++)
			{
				Int3 @int;
				Int3 int2;
				Int3 int3;
				nodes[j].GetVertices(out @int, out int2, out int3);
				IntRect intRect = new IntRect(@int.x, @int.z, @int.x, @int.z);
				intRect = intRect.ExpandToContain(int2.x, int2.z);
				intRect = intRect.ExpandToContain(int3.x, int3.z);
				array2[j] = intRect;
			}
			this.RebuildFromInternal(nodes, array, array2, 0, nodes.Length, false);
			ArrayPool<int>.Release(ref array, false);
			ArrayPool<IntRect>.Release(ref array2, false);
		}

		// Token: 0x060025EC RID: 9708 RVA: 0x001A2FC0 File Offset: 0x001A11C0
		private static int SplitByX(TriangleMeshNode[] nodes, int[] permutation, int from, int to, int divider)
		{
			int num = to;
			for (int i = from; i < num; i++)
			{
				if (nodes[permutation[i]].position.x > divider)
				{
					num--;
					int num2 = permutation[num];
					permutation[num] = permutation[i];
					permutation[i] = num2;
					i--;
				}
			}
			return num;
		}

		// Token: 0x060025ED RID: 9709 RVA: 0x001A3008 File Offset: 0x001A1208
		private static int SplitByZ(TriangleMeshNode[] nodes, int[] permutation, int from, int to, int divider)
		{
			int num = to;
			for (int i = from; i < num; i++)
			{
				if (nodes[permutation[i]].position.z > divider)
				{
					num--;
					int num2 = permutation[num];
					permutation[num] = permutation[i];
					permutation[i] = num2;
					i--;
				}
			}
			return num;
		}

		// Token: 0x060025EE RID: 9710 RVA: 0x001A3050 File Offset: 0x001A1250
		private int RebuildFromInternal(TriangleMeshNode[] nodes, int[] permutation, IntRect[] nodeBounds, int from, int to, bool odd)
		{
			IntRect intRect = BBTree.NodeBounds(permutation, nodeBounds, from, to);
			int box = this.GetBox(intRect);
			if (to - from <= 4)
			{
				int num = this.tree[box].nodeOffset = this.leafNodes * 4;
				this.EnsureNodeCapacity(num + 4);
				this.leafNodes++;
				for (int i = 0; i < 4; i++)
				{
					this.nodeLookup[num + i] = ((i < to - from) ? nodes[permutation[from + i]] : null);
				}
				return box;
			}
			int num2;
			if (odd)
			{
				int divider = (intRect.xmin + intRect.xmax) / 2;
				num2 = BBTree.SplitByX(nodes, permutation, from, to, divider);
			}
			else
			{
				int divider2 = (intRect.ymin + intRect.ymax) / 2;
				num2 = BBTree.SplitByZ(nodes, permutation, from, to, divider2);
			}
			if (num2 == from || num2 == to)
			{
				if (!odd)
				{
					int divider3 = (intRect.xmin + intRect.xmax) / 2;
					num2 = BBTree.SplitByX(nodes, permutation, from, to, divider3);
				}
				else
				{
					int divider4 = (intRect.ymin + intRect.ymax) / 2;
					num2 = BBTree.SplitByZ(nodes, permutation, from, to, divider4);
				}
				if (num2 == from || num2 == to)
				{
					num2 = (from + to) / 2;
				}
			}
			this.tree[box].left = this.RebuildFromInternal(nodes, permutation, nodeBounds, from, num2, !odd);
			this.tree[box].right = this.RebuildFromInternal(nodes, permutation, nodeBounds, num2, to, !odd);
			return box;
		}

		// Token: 0x060025EF RID: 9711 RVA: 0x001A31CC File Offset: 0x001A13CC
		private static IntRect NodeBounds(int[] permutation, IntRect[] nodeBounds, int from, int to)
		{
			IntRect intRect = nodeBounds[permutation[from]];
			for (int i = from + 1; i < to; i++)
			{
				IntRect intRect2 = nodeBounds[permutation[i]];
				intRect.xmin = Math.Min(intRect.xmin, intRect2.xmin);
				intRect.ymin = Math.Min(intRect.ymin, intRect2.ymin);
				intRect.xmax = Math.Max(intRect.xmax, intRect2.xmax);
				intRect.ymax = Math.Max(intRect.ymax, intRect2.ymax);
			}
			return intRect;
		}

		// Token: 0x060025F0 RID: 9712 RVA: 0x001A325C File Offset: 0x001A145C
		[Conditional("ASTARDEBUG")]
		private static void DrawDebugRect(IntRect rect)
		{
			UnityEngine.Debug.DrawLine(new Vector3((float)rect.xmin, 0f, (float)rect.ymin), new Vector3((float)rect.xmax, 0f, (float)rect.ymin), Color.white);
			UnityEngine.Debug.DrawLine(new Vector3((float)rect.xmin, 0f, (float)rect.ymax), new Vector3((float)rect.xmax, 0f, (float)rect.ymax), Color.white);
			UnityEngine.Debug.DrawLine(new Vector3((float)rect.xmin, 0f, (float)rect.ymin), new Vector3((float)rect.xmin, 0f, (float)rect.ymax), Color.white);
			UnityEngine.Debug.DrawLine(new Vector3((float)rect.xmax, 0f, (float)rect.ymin), new Vector3((float)rect.xmax, 0f, (float)rect.ymax), Color.white);
		}

		// Token: 0x060025F1 RID: 9713 RVA: 0x001A3354 File Offset: 0x001A1554
		[Conditional("ASTARDEBUG")]
		private static void DrawDebugNode(TriangleMeshNode node, float yoffset, Color color)
		{
			UnityEngine.Debug.DrawLine((Vector3)node.GetVertex(1) + Vector3.up * yoffset, (Vector3)node.GetVertex(2) + Vector3.up * yoffset, color);
			UnityEngine.Debug.DrawLine((Vector3)node.GetVertex(0) + Vector3.up * yoffset, (Vector3)node.GetVertex(1) + Vector3.up * yoffset, color);
			UnityEngine.Debug.DrawLine((Vector3)node.GetVertex(2) + Vector3.up * yoffset, (Vector3)node.GetVertex(0) + Vector3.up * yoffset, color);
		}

		// Token: 0x060025F2 RID: 9714 RVA: 0x001A341B File Offset: 0x001A161B
		public NNInfoInternal QueryClosest(Vector3 p, NNConstraint constraint, out float distance)
		{
			distance = float.PositiveInfinity;
			return this.QueryClosest(p, constraint, ref distance, new NNInfoInternal(null));
		}

		// Token: 0x060025F3 RID: 9715 RVA: 0x001A3434 File Offset: 0x001A1634
		public NNInfoInternal QueryClosestXZ(Vector3 p, NNConstraint constraint, ref float distance, NNInfoInternal previous)
		{
			float num = distance * distance;
			float num2 = num;
			if (this.count > 0 && BBTree.SquaredRectPointDistance(this.tree[0].rect, p) < num)
			{
				this.SearchBoxClosestXZ(0, p, ref num, constraint, ref previous);
				if (num < num2)
				{
					distance = Mathf.Sqrt(num);
				}
			}
			return previous;
		}

		// Token: 0x060025F4 RID: 9716 RVA: 0x001A3488 File Offset: 0x001A1688
		private void SearchBoxClosestXZ(int boxi, Vector3 p, ref float closestSqrDist, NNConstraint constraint, ref NNInfoInternal nnInfo)
		{
			BBTree.BBTreeBox bbtreeBox = this.tree[boxi];
			if (bbtreeBox.IsLeaf)
			{
				TriangleMeshNode[] array = this.nodeLookup;
				for (int i = 0; i < 4; i++)
				{
					if (array[bbtreeBox.nodeOffset + i] == null)
					{
						return;
					}
					TriangleMeshNode triangleMeshNode = array[bbtreeBox.nodeOffset + i];
					if (constraint == null || constraint.Suitable(triangleMeshNode))
					{
						Vector3 vector = triangleMeshNode.ClosestPointOnNodeXZ(p);
						float num = (vector.x - p.x) * (vector.x - p.x) + (vector.z - p.z) * (vector.z - p.z);
						if (nnInfo.constrainedNode == null || num < closestSqrDist - 1E-06f || (num <= closestSqrDist + 1E-06f && Mathf.Abs(vector.y - p.y) < Mathf.Abs(nnInfo.constClampedPosition.y - p.y)))
						{
							nnInfo.constrainedNode = triangleMeshNode;
							nnInfo.constClampedPosition = vector;
							closestSqrDist = num;
						}
					}
				}
			}
			else
			{
				int left = bbtreeBox.left;
				int right = bbtreeBox.right;
				float num2;
				float num3;
				this.GetOrderedChildren(ref left, ref right, out num2, out num3, p);
				if (num2 <= closestSqrDist)
				{
					this.SearchBoxClosestXZ(left, p, ref closestSqrDist, constraint, ref nnInfo);
				}
				if (num3 <= closestSqrDist)
				{
					this.SearchBoxClosestXZ(right, p, ref closestSqrDist, constraint, ref nnInfo);
				}
			}
		}

		// Token: 0x060025F5 RID: 9717 RVA: 0x001A35E0 File Offset: 0x001A17E0
		public NNInfoInternal QueryClosest(Vector3 p, NNConstraint constraint, ref float distance, NNInfoInternal previous)
		{
			float num = distance * distance;
			float num2 = num;
			if (this.count > 0 && BBTree.SquaredRectPointDistance(this.tree[0].rect, p) < num)
			{
				this.SearchBoxClosest(0, p, ref num, constraint, ref previous);
				if (num < num2)
				{
					distance = Mathf.Sqrt(num);
				}
			}
			return previous;
		}

		// Token: 0x060025F6 RID: 9718 RVA: 0x001A3634 File Offset: 0x001A1834
		private void SearchBoxClosest(int boxi, Vector3 p, ref float closestSqrDist, NNConstraint constraint, ref NNInfoInternal nnInfo)
		{
			BBTree.BBTreeBox bbtreeBox = this.tree[boxi];
			if (bbtreeBox.IsLeaf)
			{
				TriangleMeshNode[] array = this.nodeLookup;
				for (int i = 0; i < 4; i++)
				{
					if (array[bbtreeBox.nodeOffset + i] == null)
					{
						return;
					}
					TriangleMeshNode triangleMeshNode = array[bbtreeBox.nodeOffset + i];
					Vector3 vector = triangleMeshNode.ClosestPointOnNode(p);
					float sqrMagnitude = (vector - p).sqrMagnitude;
					if (sqrMagnitude < closestSqrDist && (constraint == null || constraint.Suitable(triangleMeshNode)))
					{
						nnInfo.constrainedNode = triangleMeshNode;
						nnInfo.constClampedPosition = vector;
						closestSqrDist = sqrMagnitude;
					}
				}
			}
			else
			{
				int left = bbtreeBox.left;
				int right = bbtreeBox.right;
				float num;
				float num2;
				this.GetOrderedChildren(ref left, ref right, out num, out num2, p);
				if (num < closestSqrDist)
				{
					this.SearchBoxClosest(left, p, ref closestSqrDist, constraint, ref nnInfo);
				}
				if (num2 < closestSqrDist)
				{
					this.SearchBoxClosest(right, p, ref closestSqrDist, constraint, ref nnInfo);
				}
			}
		}

		// Token: 0x060025F7 RID: 9719 RVA: 0x001A3710 File Offset: 0x001A1910
		private void GetOrderedChildren(ref int first, ref int second, out float firstDist, out float secondDist, Vector3 p)
		{
			firstDist = BBTree.SquaredRectPointDistance(this.tree[first].rect, p);
			secondDist = BBTree.SquaredRectPointDistance(this.tree[second].rect, p);
			if (secondDist < firstDist)
			{
				int num = first;
				first = second;
				second = num;
				float num2 = firstDist;
				firstDist = secondDist;
				secondDist = num2;
			}
		}

		// Token: 0x060025F8 RID: 9720 RVA: 0x001A3771 File Offset: 0x001A1971
		public TriangleMeshNode QueryInside(Vector3 p, NNConstraint constraint)
		{
			if (this.count == 0 || !this.tree[0].Contains(p))
			{
				return null;
			}
			return this.SearchBoxInside(0, p, constraint);
		}

		// Token: 0x060025F9 RID: 9721 RVA: 0x001A379C File Offset: 0x001A199C
		private TriangleMeshNode SearchBoxInside(int boxi, Vector3 p, NNConstraint constraint)
		{
			BBTree.BBTreeBox bbtreeBox = this.tree[boxi];
			if (bbtreeBox.IsLeaf)
			{
				TriangleMeshNode[] array = this.nodeLookup;
				for (int i = 0; i < 4; i++)
				{
					if (array[bbtreeBox.nodeOffset + i] == null)
					{
						break;
					}
					TriangleMeshNode triangleMeshNode = array[bbtreeBox.nodeOffset + i];
					if (triangleMeshNode.ContainsPoint((Int3)p) && (constraint == null || constraint.Suitable(triangleMeshNode)))
					{
						return triangleMeshNode;
					}
				}
			}
			else
			{
				if (this.tree[bbtreeBox.left].Contains(p))
				{
					TriangleMeshNode triangleMeshNode2 = this.SearchBoxInside(bbtreeBox.left, p, constraint);
					if (triangleMeshNode2 != null)
					{
						return triangleMeshNode2;
					}
				}
				if (this.tree[bbtreeBox.right].Contains(p))
				{
					TriangleMeshNode triangleMeshNode3 = this.SearchBoxInside(bbtreeBox.right, p, constraint);
					if (triangleMeshNode3 != null)
					{
						return triangleMeshNode3;
					}
				}
			}
			return null;
		}

		// Token: 0x060025FA RID: 9722 RVA: 0x001A3868 File Offset: 0x001A1A68
		public void OnDrawGizmos()
		{
			Gizmos.color = new Color(1f, 1f, 1f, 0.5f);
			if (this.count == 0)
			{
				return;
			}
			this.OnDrawGizmos(0, 0);
		}

		// Token: 0x060025FB RID: 9723 RVA: 0x001A389C File Offset: 0x001A1A9C
		private void OnDrawGizmos(int boxi, int depth)
		{
			BBTree.BBTreeBox bbtreeBox = this.tree[boxi];
			Vector3 a = (Vector3)new Int3(bbtreeBox.rect.xmin, 0, bbtreeBox.rect.ymin);
			Vector3 vector = (Vector3)new Int3(bbtreeBox.rect.xmax, 0, bbtreeBox.rect.ymax);
			Vector3 vector2 = (a + vector) * 0.5f;
			Vector3 vector3 = (vector - vector2) * 2f;
			vector3 = new Vector3(vector3.x, 1f, vector3.z);
			vector2.y += (float)(depth * 2);
			Gizmos.color = AstarMath.IntToColor(depth, 1f);
			Gizmos.DrawCube(vector2, vector3);
			if (!bbtreeBox.IsLeaf)
			{
				this.OnDrawGizmos(bbtreeBox.left, depth + 1);
				this.OnDrawGizmos(bbtreeBox.right, depth + 1);
			}
		}

		// Token: 0x060025FC RID: 9724 RVA: 0x001A3984 File Offset: 0x001A1B84
		private static bool NodeIntersectsCircle(TriangleMeshNode node, Vector3 p, float radius)
		{
			return float.IsPositiveInfinity(radius) || (p - node.ClosestPointOnNode(p)).sqrMagnitude < radius * radius;
		}

		// Token: 0x060025FD RID: 9725 RVA: 0x001A39B8 File Offset: 0x001A1BB8
		private static bool RectIntersectsCircle(IntRect r, Vector3 p, float radius)
		{
			if (float.IsPositiveInfinity(radius))
			{
				return true;
			}
			Vector3 vector = p;
			p.x = Math.Max(p.x, (float)r.xmin * 0.001f);
			p.x = Math.Min(p.x, (float)r.xmax * 0.001f);
			p.z = Math.Max(p.z, (float)r.ymin * 0.001f);
			p.z = Math.Min(p.z, (float)r.ymax * 0.001f);
			return (p.x - vector.x) * (p.x - vector.x) + (p.z - vector.z) * (p.z - vector.z) < radius * radius;
		}

		// Token: 0x060025FE RID: 9726 RVA: 0x001A3A8C File Offset: 0x001A1C8C
		private static float SquaredRectPointDistance(IntRect r, Vector3 p)
		{
			Vector3 vector = p;
			p.x = Math.Max(p.x, (float)r.xmin * 0.001f);
			p.x = Math.Min(p.x, (float)r.xmax * 0.001f);
			p.z = Math.Max(p.z, (float)r.ymin * 0.001f);
			p.z = Math.Min(p.z, (float)r.ymax * 0.001f);
			return (p.x - vector.x) * (p.x - vector.x) + (p.z - vector.z) * (p.z - vector.z);
		}

		// Token: 0x040040F0 RID: 16624
		private BBTree.BBTreeBox[] tree;

		// Token: 0x040040F1 RID: 16625
		private TriangleMeshNode[] nodeLookup;

		// Token: 0x040040F2 RID: 16626
		private int count;

		// Token: 0x040040F3 RID: 16627
		private int leafNodes;

		// Token: 0x040040F4 RID: 16628
		private const int MaximumLeafSize = 4;

		// Token: 0x02000743 RID: 1859
		private struct BBTreeBox
		{
			// Token: 0x1700068A RID: 1674
			// (get) Token: 0x06002D2B RID: 11563 RVA: 0x001CD64B File Offset: 0x001CB84B
			public bool IsLeaf
			{
				get
				{
					return this.nodeOffset >= 0;
				}
			}

			// Token: 0x06002D2C RID: 11564 RVA: 0x001CD65C File Offset: 0x001CB85C
			public BBTreeBox(IntRect rect)
			{
				this.nodeOffset = -1;
				this.rect = rect;
				this.left = (this.right = -1);
			}

			// Token: 0x06002D2D RID: 11565 RVA: 0x001CD688 File Offset: 0x001CB888
			public BBTreeBox(int nodeOffset, IntRect rect)
			{
				this.nodeOffset = nodeOffset;
				this.rect = rect;
				this.left = (this.right = -1);
			}

			// Token: 0x06002D2E RID: 11566 RVA: 0x001CD6B4 File Offset: 0x001CB8B4
			public bool Contains(Vector3 point)
			{
				Int3 @int = (Int3)point;
				return this.rect.Contains(@int.x, @int.z);
			}

			// Token: 0x04004998 RID: 18840
			public IntRect rect;

			// Token: 0x04004999 RID: 18841
			public int nodeOffset;

			// Token: 0x0400499A RID: 18842
			public int left;

			// Token: 0x0400499B RID: 18843
			public int right;
		}
	}
}
