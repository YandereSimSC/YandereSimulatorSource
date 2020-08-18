using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000517 RID: 1303
	public class RichFunnel : RichPathPart
	{
		// Token: 0x06002149 RID: 8521 RVA: 0x00189424 File Offset: 0x00187624
		public RichFunnel()
		{
			this.left = ListPool<Vector3>.Claim();
			this.right = ListPool<Vector3>.Claim();
			this.nodes = new List<TriangleMeshNode>();
			this.graph = null;
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x00189472 File Offset: 0x00187672
		public RichFunnel Initialize(RichPath path, NavmeshBase graph)
		{
			if (graph == null)
			{
				throw new ArgumentNullException("graph");
			}
			if (this.graph != null)
			{
				throw new InvalidOperationException("Trying to initialize an already initialized object. " + graph);
			}
			this.graph = graph;
			this.path = path;
			return this;
		}

		// Token: 0x0600214B RID: 8523 RVA: 0x001894AA File Offset: 0x001876AA
		public override void OnEnterPool()
		{
			this.left.Clear();
			this.right.Clear();
			this.nodes.Clear();
			this.graph = null;
			this.currentNode = 0;
			this.checkForDestroyedNodesCounter = 0;
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x0600214C RID: 8524 RVA: 0x001894E4 File Offset: 0x001876E4
		public TriangleMeshNode CurrentNode
		{
			get
			{
				TriangleMeshNode triangleMeshNode = this.nodes[this.currentNode];
				if (!triangleMeshNode.Destroyed)
				{
					return triangleMeshNode;
				}
				return null;
			}
		}

		// Token: 0x0600214D RID: 8525 RVA: 0x00189510 File Offset: 0x00187710
		public void BuildFunnelCorridor(List<GraphNode> nodes, int start, int end)
		{
			this.exactStart = (nodes[start] as MeshNode).ClosestPointOnNode(this.exactStart);
			this.exactEnd = (nodes[end] as MeshNode).ClosestPointOnNode(this.exactEnd);
			this.left.Clear();
			this.right.Clear();
			this.left.Add(this.exactStart);
			this.right.Add(this.exactStart);
			this.nodes.Clear();
			if (this.funnelSimplification)
			{
				List<GraphNode> list = ListPool<GraphNode>.Claim(end - start);
				this.SimplifyPath(this.graph, nodes, start, end, list, this.exactStart, this.exactEnd);
				if (this.nodes.Capacity < list.Count)
				{
					this.nodes.Capacity = list.Count;
				}
				for (int i = 0; i < list.Count; i++)
				{
					TriangleMeshNode triangleMeshNode = list[i] as TriangleMeshNode;
					if (triangleMeshNode != null)
					{
						this.nodes.Add(triangleMeshNode);
					}
				}
				ListPool<GraphNode>.Release(ref list);
			}
			else
			{
				if (this.nodes.Capacity < end - start)
				{
					this.nodes.Capacity = end - start;
				}
				for (int j = start; j <= end; j++)
				{
					TriangleMeshNode triangleMeshNode2 = nodes[j] as TriangleMeshNode;
					if (triangleMeshNode2 != null)
					{
						this.nodes.Add(triangleMeshNode2);
					}
				}
			}
			for (int k = 0; k < this.nodes.Count - 1; k++)
			{
				this.nodes[k].GetPortal(this.nodes[k + 1], this.left, this.right, false);
			}
			this.left.Add(this.exactEnd);
			this.right.Add(this.exactEnd);
		}

		// Token: 0x0600214E RID: 8526 RVA: 0x001896D8 File Offset: 0x001878D8
		private void SimplifyPath(IRaycastableGraph graph, List<GraphNode> nodes, int start, int end, List<GraphNode> result, Vector3 startPoint, Vector3 endPoint)
		{
			if (graph == null)
			{
				throw new ArgumentNullException("graph");
			}
			if (start > end)
			{
				throw new ArgumentException("start >= end");
			}
			int num = start;
			int num2 = 0;
			while (num2++ <= 1000)
			{
				if (start == end)
				{
					result.Add(nodes[end]);
					return;
				}
				int count = result.Count;
				int i = end + 1;
				int num3 = start + 1;
				bool flag = false;
				while (i > num3 + 1)
				{
					int num4 = (i + num3) / 2;
					Vector3 start2 = (start == num) ? startPoint : ((Vector3)nodes[start].position);
					Vector3 end2 = (num4 == end) ? endPoint : ((Vector3)nodes[num4].position);
					GraphHitInfo graphHitInfo;
					if (graph.Linecast(start2, end2, nodes[start], out graphHitInfo))
					{
						i = num4;
					}
					else
					{
						flag = true;
						num3 = num4;
					}
				}
				if (!flag)
				{
					result.Add(nodes[start]);
					start = num3;
				}
				else
				{
					Vector3 start3 = (start == num) ? startPoint : ((Vector3)nodes[start].position);
					Vector3 end3 = (num3 == end) ? endPoint : ((Vector3)nodes[num3].position);
					GraphHitInfo graphHitInfo2;
					graph.Linecast(start3, end3, nodes[start], out graphHitInfo2, result);
					long num5 = 0L;
					long num6 = 0L;
					for (int j = start; j <= num3; j++)
					{
						num5 += (long)((ulong)nodes[j].Penalty + (ulong)((long)((this.path.seeker != null) ? this.path.seeker.tagPenalties[(int)nodes[j].Tag] : 0)));
					}
					for (int k = count; k < result.Count; k++)
					{
						num6 += (long)((ulong)result[k].Penalty + (ulong)((long)((this.path.seeker != null) ? this.path.seeker.tagPenalties[(int)result[k].Tag] : 0)));
					}
					if ((double)num5 * 1.4 * (double)(num3 - start + 1) < (double)(num6 * (long)(result.Count - count)) || result[result.Count - 1] != nodes[num3])
					{
						result.RemoveRange(count, result.Count - count);
						result.Add(nodes[start]);
						start++;
					}
					else
					{
						result.RemoveAt(result.Count - 1);
						start = num3;
					}
				}
			}
			Debug.LogError("Was the path really long or have we got cought in an infinite loop?");
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x00189960 File Offset: 0x00187B60
		private void UpdateFunnelCorridor(int splitIndex, List<TriangleMeshNode> prefix)
		{
			this.nodes.RemoveRange(0, splitIndex);
			this.nodes.InsertRange(0, prefix);
			this.left.Clear();
			this.right.Clear();
			this.left.Add(this.exactStart);
			this.right.Add(this.exactStart);
			for (int i = 0; i < this.nodes.Count - 1; i++)
			{
				this.nodes[i].GetPortal(this.nodes[i + 1], this.left, this.right, false);
			}
			this.left.Add(this.exactEnd);
			this.right.Add(this.exactEnd);
		}

		// Token: 0x06002150 RID: 8528 RVA: 0x00189A28 File Offset: 0x00187C28
		private bool CheckForDestroyedNodes()
		{
			int i = 0;
			int count = this.nodes.Count;
			while (i < count)
			{
				if (this.nodes[i].Destroyed)
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06002151 RID: 8529 RVA: 0x00189A64 File Offset: 0x00187C64
		public float DistanceToEndOfPath
		{
			get
			{
				TriangleMeshNode triangleMeshNode = this.CurrentNode;
				Vector3 b = (triangleMeshNode != null) ? triangleMeshNode.ClosestPointOnNode(this.currentPosition) : this.currentPosition;
				return (this.exactEnd - b).magnitude;
			}
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x00189AA4 File Offset: 0x00187CA4
		public Vector3 ClampToNavmesh(Vector3 position)
		{
			if (this.path.transform != null)
			{
				position = this.path.transform.InverseTransform(position);
			}
			this.ClampToNavmeshInternal(ref position);
			if (this.path.transform != null)
			{
				position = this.path.transform.Transform(position);
			}
			return position;
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x00189AFC File Offset: 0x00187CFC
		public Vector3 Update(Vector3 position, List<Vector3> buffer, int numCorners, out bool lastCorner, out bool requiresRepath)
		{
			if (this.path.transform != null)
			{
				position = this.path.transform.InverseTransform(position);
			}
			lastCorner = false;
			requiresRepath = false;
			if (this.checkForDestroyedNodesCounter >= 10)
			{
				this.checkForDestroyedNodesCounter = 0;
				requiresRepath |= this.CheckForDestroyedNodes();
			}
			else
			{
				this.checkForDestroyedNodesCounter++;
			}
			bool flag = this.ClampToNavmeshInternal(ref position);
			this.currentPosition = position;
			if (flag)
			{
				requiresRepath = true;
				lastCorner = false;
				buffer.Add(position);
			}
			else if (!this.FindNextCorners(position, this.currentNode, buffer, numCorners, out lastCorner))
			{
				Debug.LogError("Failed to find next corners in the path");
				buffer.Add(position);
			}
			if (this.path.transform != null)
			{
				for (int i = 0; i < buffer.Count; i++)
				{
					buffer[i] = this.path.transform.Transform(buffer[i]);
				}
				position = this.path.transform.Transform(position);
			}
			return position;
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x00189BF8 File Offset: 0x00187DF8
		private bool ClampToNavmeshInternal(ref Vector3 position)
		{
			TriangleMeshNode triangleMeshNode = this.nodes[this.currentNode];
			if (triangleMeshNode.Destroyed)
			{
				return true;
			}
			if (triangleMeshNode.ContainsPoint(position))
			{
				return false;
			}
			Queue<TriangleMeshNode> queue = RichFunnel.navmeshClampQueue;
			List<TriangleMeshNode> list = RichFunnel.navmeshClampList;
			Dictionary<TriangleMeshNode, TriangleMeshNode> dictionary = RichFunnel.navmeshClampDict;
			triangleMeshNode.TemporaryFlag1 = true;
			dictionary[triangleMeshNode] = null;
			queue.Enqueue(triangleMeshNode);
			list.Add(triangleMeshNode);
			float num = float.PositiveInfinity;
			Vector3 vector = position;
			TriangleMeshNode triangleMeshNode2 = null;
			while (queue.Count > 0)
			{
				TriangleMeshNode triangleMeshNode3 = queue.Dequeue();
				Vector3 vector2 = triangleMeshNode3.ClosestPointOnNodeXZ(position);
				float num2 = VectorMath.MagnitudeXZ(vector2 - position);
				if (num2 <= num * 1.05f + 0.001f)
				{
					if (num2 < num)
					{
						num = num2;
						vector = vector2;
						triangleMeshNode2 = triangleMeshNode3;
					}
					for (int i = 0; i < triangleMeshNode3.connections.Length; i++)
					{
						TriangleMeshNode triangleMeshNode4 = triangleMeshNode3.connections[i].node as TriangleMeshNode;
						if (triangleMeshNode4 != null && !triangleMeshNode4.TemporaryFlag1)
						{
							triangleMeshNode4.TemporaryFlag1 = true;
							dictionary[triangleMeshNode4] = triangleMeshNode3;
							queue.Enqueue(triangleMeshNode4);
							list.Add(triangleMeshNode4);
						}
					}
				}
			}
			for (int j = 0; j < list.Count; j++)
			{
				list[j].TemporaryFlag1 = false;
			}
			list.ClearFast<TriangleMeshNode>();
			int num3 = this.nodes.IndexOf(triangleMeshNode2);
			position.x = vector.x;
			position.z = vector.z;
			if (num3 == -1)
			{
				List<TriangleMeshNode> list2 = RichFunnel.navmeshClampList;
				while (num3 == -1)
				{
					list2.Add(triangleMeshNode2);
					triangleMeshNode2 = dictionary[triangleMeshNode2];
					num3 = this.nodes.IndexOf(triangleMeshNode2);
				}
				this.exactStart = position;
				this.UpdateFunnelCorridor(num3, list2);
				list2.ClearFast<TriangleMeshNode>();
				this.currentNode = 0;
			}
			else
			{
				this.currentNode = num3;
			}
			dictionary.Clear();
			return this.currentNode + 1 < this.nodes.Count && this.nodes[this.currentNode + 1].Destroyed;
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x00189E1D File Offset: 0x0018801D
		public void FindWalls(List<Vector3> wallBuffer, float range)
		{
			this.FindWalls(this.currentNode, wallBuffer, this.currentPosition, range);
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x00189E34 File Offset: 0x00188034
		private void FindWalls(int nodeIndex, List<Vector3> wallBuffer, Vector3 position, float range)
		{
			if (range <= 0f)
			{
				return;
			}
			bool flag = false;
			bool flag2 = false;
			range *= range;
			position.y = 0f;
			int num = 0;
			while (!flag || !flag2)
			{
				if ((num >= 0 || !flag) && (num <= 0 || !flag2))
				{
					if (num < 0 && nodeIndex + num < 0)
					{
						flag = true;
					}
					else if (num > 0 && nodeIndex + num >= this.nodes.Count)
					{
						flag2 = true;
					}
					else
					{
						TriangleMeshNode triangleMeshNode = (nodeIndex + num - 1 < 0) ? null : this.nodes[nodeIndex + num - 1];
						TriangleMeshNode triangleMeshNode2 = this.nodes[nodeIndex + num];
						TriangleMeshNode triangleMeshNode3 = (nodeIndex + num + 1 >= this.nodes.Count) ? null : this.nodes[nodeIndex + num + 1];
						if (triangleMeshNode2.Destroyed)
						{
							break;
						}
						if ((triangleMeshNode2.ClosestPointOnNodeXZ(position) - position).sqrMagnitude > range)
						{
							if (num < 0)
							{
								flag = true;
							}
							else
							{
								flag2 = true;
							}
						}
						else
						{
							for (int i = 0; i < 3; i++)
							{
								this.triBuffer[i] = 0;
							}
							for (int j = 0; j < triangleMeshNode2.connections.Length; j++)
							{
								TriangleMeshNode triangleMeshNode4 = triangleMeshNode2.connections[j].node as TriangleMeshNode;
								if (triangleMeshNode4 != null)
								{
									int num2 = -1;
									for (int k = 0; k < 3; k++)
									{
										for (int l = 0; l < 3; l++)
										{
											if (triangleMeshNode2.GetVertex(k) == triangleMeshNode4.GetVertex((l + 1) % 3) && triangleMeshNode2.GetVertex((k + 1) % 3) == triangleMeshNode4.GetVertex(l))
											{
												num2 = k;
												k = 3;
												break;
											}
										}
									}
									if (num2 != -1)
									{
										this.triBuffer[num2] = ((triangleMeshNode4 == triangleMeshNode || triangleMeshNode4 == triangleMeshNode3) ? 2 : 1);
									}
								}
							}
							for (int m = 0; m < 3; m++)
							{
								if (this.triBuffer[m] == 0)
								{
									wallBuffer.Add((Vector3)triangleMeshNode2.GetVertex(m));
									wallBuffer.Add((Vector3)triangleMeshNode2.GetVertex((m + 1) % 3));
								}
							}
						}
					}
				}
				num = ((num < 0) ? (-num) : (-num - 1));
			}
			if (this.path.transform != null)
			{
				for (int n = 0; n < wallBuffer.Count; n++)
				{
					wallBuffer[n] = this.path.transform.Transform(wallBuffer[n]);
				}
			}
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x0018A0B8 File Offset: 0x001882B8
		private bool FindNextCorners(Vector3 origin, int startIndex, List<Vector3> funnelPath, int numCorners, out bool lastCorner)
		{
			lastCorner = false;
			if (this.left == null)
			{
				throw new Exception("left list is null");
			}
			if (this.right == null)
			{
				throw new Exception("right list is null");
			}
			if (funnelPath == null)
			{
				throw new ArgumentNullException("funnelPath");
			}
			if (this.left.Count != this.right.Count)
			{
				throw new ArgumentException("left and right lists must have equal length");
			}
			int count = this.left.Count;
			if (count == 0)
			{
				throw new ArgumentException("no diagonals");
			}
			if (count - startIndex < 3)
			{
				funnelPath.Add(this.left[count - 1]);
				lastCorner = true;
				return true;
			}
			while (this.left[startIndex + 1] == this.left[startIndex + 2] && this.right[startIndex + 1] == this.right[startIndex + 2])
			{
				startIndex++;
				if (count - startIndex <= 3)
				{
					return false;
				}
			}
			Vector3 vector = this.left[startIndex + 2];
			if (vector == this.left[startIndex + 1])
			{
				vector = this.right[startIndex + 2];
			}
			while (VectorMath.IsColinearXZ(origin, this.left[startIndex + 1], this.right[startIndex + 1]) || VectorMath.RightOrColinearXZ(this.left[startIndex + 1], this.right[startIndex + 1], vector) == VectorMath.RightOrColinearXZ(this.left[startIndex + 1], this.right[startIndex + 1], origin))
			{
				startIndex++;
				if (count - startIndex < 3)
				{
					funnelPath.Add(this.left[count - 1]);
					lastCorner = true;
					return true;
				}
				vector = this.left[startIndex + 2];
				if (vector == this.left[startIndex + 1])
				{
					vector = this.right[startIndex + 2];
				}
			}
			Vector3 vector2 = origin;
			Vector3 vector3 = this.left[startIndex + 1];
			Vector3 vector4 = this.right[startIndex + 1];
			int num = startIndex + 1;
			int num2 = startIndex + 1;
			int i = startIndex + 2;
			while (i < count)
			{
				if (funnelPath.Count >= numCorners)
				{
					return true;
				}
				if (funnelPath.Count > 2000)
				{
					Debug.LogWarning("Avoiding infinite loop. Remove this check if you have this long paths.");
					break;
				}
				Vector3 vector5 = this.left[i];
				Vector3 vector6 = this.right[i];
				if (VectorMath.SignedTriangleAreaTimes2XZ(vector2, vector4, vector6) < 0f)
				{
					goto IL_2AE;
				}
				if (vector2 == vector4 || VectorMath.SignedTriangleAreaTimes2XZ(vector2, vector3, vector6) <= 0f)
				{
					vector4 = vector6;
					num = i;
					goto IL_2AE;
				}
				funnelPath.Add(vector3);
				vector2 = vector3;
				int num3 = num2;
				vector3 = vector2;
				vector4 = vector2;
				num2 = num3;
				num = num3;
				i = num3;
				IL_2FB:
				i++;
				continue;
				IL_2AE:
				if (VectorMath.SignedTriangleAreaTimes2XZ(vector2, vector3, vector5) > 0f)
				{
					goto IL_2FB;
				}
				if (vector2 == vector3 || VectorMath.SignedTriangleAreaTimes2XZ(vector2, vector4, vector5) >= 0f)
				{
					vector3 = vector5;
					num2 = i;
					goto IL_2FB;
				}
				funnelPath.Add(vector4);
				vector2 = vector4;
				int num4 = num;
				vector3 = vector2;
				vector4 = vector2;
				num2 = num4;
				num = num4;
				i = num4;
				goto IL_2FB;
			}
			lastCorner = true;
			funnelPath.Add(this.left[count - 1]);
			return true;
		}

		// Token: 0x04003E7F RID: 15999
		private readonly List<Vector3> left;

		// Token: 0x04003E80 RID: 16000
		private readonly List<Vector3> right;

		// Token: 0x04003E81 RID: 16001
		private List<TriangleMeshNode> nodes;

		// Token: 0x04003E82 RID: 16002
		public Vector3 exactStart;

		// Token: 0x04003E83 RID: 16003
		public Vector3 exactEnd;

		// Token: 0x04003E84 RID: 16004
		private NavmeshBase graph;

		// Token: 0x04003E85 RID: 16005
		private int currentNode;

		// Token: 0x04003E86 RID: 16006
		private Vector3 currentPosition;

		// Token: 0x04003E87 RID: 16007
		private int checkForDestroyedNodesCounter;

		// Token: 0x04003E88 RID: 16008
		private RichPath path;

		// Token: 0x04003E89 RID: 16009
		private int[] triBuffer = new int[3];

		// Token: 0x04003E8A RID: 16010
		public bool funnelSimplification = true;

		// Token: 0x04003E8B RID: 16011
		private static Queue<TriangleMeshNode> navmeshClampQueue = new Queue<TriangleMeshNode>();

		// Token: 0x04003E8C RID: 16012
		private static List<TriangleMeshNode> navmeshClampList = new List<TriangleMeshNode>();

		// Token: 0x04003E8D RID: 16013
		private static Dictionary<TriangleMeshNode, TriangleMeshNode> navmeshClampDict = new Dictionary<TriangleMeshNode, TriangleMeshNode>();
	}
}
