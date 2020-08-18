using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000527 RID: 1319
	public static class GraphUtilities
	{
		// Token: 0x0600229A RID: 8858 RVA: 0x00190DF8 File Offset: 0x0018EFF8
		public static List<Vector3> GetContours(NavGraph graph)
		{
			List<Vector3> result = ListPool<Vector3>.Claim();
			if (graph is INavmesh)
			{
				GraphUtilities.GetContours(graph as INavmesh, delegate(List<Int3> vertices, bool cycle)
				{
					int index = cycle ? (vertices.Count - 1) : 0;
					for (int i = 0; i < vertices.Count; i++)
					{
						result.Add((Vector3)vertices[index]);
						result.Add((Vector3)vertices[i]);
						index = i;
					}
				});
			}
			else if (graph is GridGraph)
			{
				GraphUtilities.GetContours(graph as GridGraph, delegate(Vector3[] vertices)
				{
					int num = vertices.Length - 1;
					for (int i = 0; i < vertices.Length; i++)
					{
						result.Add(vertices[num]);
						result.Add(vertices[i]);
						num = i;
					}
				}, 0f, null);
			}
			return result;
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x00190E64 File Offset: 0x0018F064
		public static void GetContours(INavmesh navmesh, Action<List<Int3>, bool> results)
		{
			bool[] uses = new bool[3];
			Dictionary<int, int> outline = new Dictionary<int, int>();
			Dictionary<int, Int3> vertexPositions = new Dictionary<int, Int3>();
			HashSet<int> hasInEdge = new HashSet<int>();
			navmesh.GetNodes(delegate(GraphNode _node)
			{
				TriangleMeshNode triangleMeshNode = _node as TriangleMeshNode;
				uses[0] = (uses[1] = (uses[2] = false));
				if (triangleMeshNode != null)
				{
					for (int i = 0; i < triangleMeshNode.connections.Length; i++)
					{
						TriangleMeshNode triangleMeshNode2 = triangleMeshNode.connections[i].node as TriangleMeshNode;
						if (triangleMeshNode2 != null)
						{
							int num = triangleMeshNode.SharedEdge(triangleMeshNode2);
							if (num != -1)
							{
								uses[num] = true;
							}
						}
					}
					for (int j = 0; j < 3; j++)
					{
						if (!uses[j])
						{
							int i2 = j;
							int i3 = (j + 1) % triangleMeshNode.GetVertexCount();
							outline[triangleMeshNode.GetVertexIndex(i2)] = triangleMeshNode.GetVertexIndex(i3);
							hasInEdge.Add(triangleMeshNode.GetVertexIndex(i3));
							vertexPositions[triangleMeshNode.GetVertexIndex(i2)] = triangleMeshNode.GetVertex(i2);
							vertexPositions[triangleMeshNode.GetVertexIndex(i3)] = triangleMeshNode.GetVertex(i3);
						}
					}
				}
			});
			Polygon.TraceContours(outline, hasInEdge, delegate(List<int> chain, bool cycle)
			{
				List<Int3> list = ListPool<Int3>.Claim();
				for (int i = 0; i < chain.Count; i++)
				{
					list.Add(vertexPositions[chain[i]]);
				}
				results(list, cycle);
			});
		}

		// Token: 0x0600229C RID: 8860 RVA: 0x00190EDC File Offset: 0x0018F0DC
		public static void GetContours(GridGraph grid, Action<Vector3[]> callback, float yMergeThreshold, GridNodeBase[] nodes = null)
		{
			HashSet<GridNodeBase> hashSet = (nodes != null) ? new HashSet<GridNodeBase>(nodes) : null;
			if (grid is LayerGridGraph)
			{
				GridNodeBase[] array;
				if ((array = nodes) == null)
				{
					GridNodeBase[] nodes2 = (grid as LayerGridGraph).nodes;
					array = nodes2;
				}
				nodes = array;
			}
			GridNodeBase[] array2;
			if ((array2 = nodes) == null)
			{
				GridNodeBase[] nodes2 = grid.nodes;
				array2 = nodes2;
			}
			nodes = array2;
			int[] neighbourXOffsets = grid.neighbourXOffsets;
			int[] neighbourZOffsets = grid.neighbourZOffsets;
			int[] array3;
			if (grid.neighbours != NumNeighbours.Six)
			{
				RuntimeHelpers.InitializeArray(array3 = new int[4], fieldof(<PrivateImplementationDetails>.02E4414E7DFA0F3AA2387EE8EA7AB31431CB406A).FieldHandle);
			}
			else
			{
				array3 = GridGraph.hexagonNeighbourIndices;
			}
			int[] array4 = array3;
			float num = (grid.neighbours == NumNeighbours.Six) ? 0.33333334f : 0.5f;
			if (nodes != null)
			{
				List<Vector3> list = ListPool<Vector3>.Claim();
				HashSet<int> hashSet2 = new HashSet<int>();
				foreach (GridNodeBase gridNodeBase in nodes)
				{
					if (gridNodeBase != null && gridNodeBase.Walkable && (!gridNodeBase.HasConnectionsToAllEightNeighbours || hashSet != null))
					{
						for (int j = 0; j < array4.Length; j++)
						{
							int num2 = gridNodeBase.NodeIndex << 4 | j;
							GridNodeBase neighbourAlongDirection = gridNodeBase.GetNeighbourAlongDirection(array4[j]);
							if ((neighbourAlongDirection == null || (hashSet != null && !hashSet.Contains(neighbourAlongDirection))) && !hashSet2.Contains(num2))
							{
								list.ClearFast<Vector3>();
								int num3 = j;
								GridNodeBase gridNodeBase2 = gridNodeBase;
								for (;;)
								{
									int num4 = gridNodeBase2.NodeIndex << 4 | num3;
									if (num4 == num2 && list.Count > 0)
									{
										break;
									}
									hashSet2.Add(num4);
									GridNodeBase neighbourAlongDirection2 = gridNodeBase2.GetNeighbourAlongDirection(array4[num3]);
									if (neighbourAlongDirection2 == null || (hashSet != null && !hashSet.Contains(neighbourAlongDirection2)))
									{
										int num5 = array4[num3];
										num3 = (num3 + 1) % array4.Length;
										int num6 = array4[num3];
										Vector3 vector = new Vector3((float)gridNodeBase2.XCoordinateInGrid + 0.5f, 0f, (float)gridNodeBase2.ZCoordinateInGrid + 0.5f);
										vector.x += (float)(neighbourXOffsets[num5] + neighbourXOffsets[num6]) * num;
										vector.z += (float)(neighbourZOffsets[num5] + neighbourZOffsets[num6]) * num;
										vector.y = grid.transform.InverseTransform((Vector3)gridNodeBase2.position).y;
										if (list.Count >= 2)
										{
											Vector3 b = list[list.Count - 2];
											Vector3 vector2 = list[list.Count - 1] - b;
											Vector3 vector3 = vector - b;
											if (((Mathf.Abs(vector2.x) > 0.01f || Mathf.Abs(vector3.x) > 0.01f) && (Mathf.Abs(vector2.z) > 0.01f || Mathf.Abs(vector3.z) > 0.01f)) || Mathf.Abs(vector2.y) > yMergeThreshold || Mathf.Abs(vector3.y) > yMergeThreshold)
											{
												list.Add(vector);
											}
											else
											{
												list[list.Count - 1] = vector;
											}
										}
										else
										{
											list.Add(vector);
										}
									}
									else
									{
										gridNodeBase2 = neighbourAlongDirection2;
										num3 = (num3 + array4.Length / 2 + 1) % array4.Length;
									}
								}
								Vector3[] array5 = list.ToArray();
								grid.transform.Transform(array5);
								callback(array5);
							}
						}
					}
				}
				ListPool<Vector3>.Release(ref list);
			}
		}
	}
}
