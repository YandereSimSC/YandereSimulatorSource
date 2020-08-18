using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Pathfinding.Serialization;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200056D RID: 1389
	public abstract class NavmeshBase : NavGraph, INavmesh, INavmeshHolder, ITransformedGraph, IRaycastableGraph
	{
		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x0600251F RID: 9503
		public abstract float TileWorldSizeX { get; }

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06002520 RID: 9504
		public abstract float TileWorldSizeZ { get; }

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06002521 RID: 9505
		protected abstract float MaxTileConnectionEdgeDistance { get; }

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06002522 RID: 9506 RVA: 0x0019D488 File Offset: 0x0019B688
		GraphTransform ITransformedGraph.transform
		{
			get
			{
				return this.transform;
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06002523 RID: 9507
		protected abstract bool RecalculateNormals { get; }

		// Token: 0x06002524 RID: 9508
		public abstract GraphTransform CalculateTransform();

		// Token: 0x06002525 RID: 9509 RVA: 0x0019D490 File Offset: 0x0019B690
		public NavmeshTile GetTile(int x, int z)
		{
			return this.tiles[x + z * this.tileXCount];
		}

		// Token: 0x06002526 RID: 9510 RVA: 0x0019D4A4 File Offset: 0x0019B6A4
		public Int3 GetVertex(int index)
		{
			int num = index >> 12 & 524287;
			return this.tiles[num].GetVertex(index);
		}

		// Token: 0x06002527 RID: 9511 RVA: 0x0019D4CC File Offset: 0x0019B6CC
		public Int3 GetVertexInGraphSpace(int index)
		{
			int num = index >> 12 & 524287;
			return this.tiles[num].GetVertexInGraphSpace(index);
		}

		// Token: 0x06002528 RID: 9512 RVA: 0x0019D4F2 File Offset: 0x0019B6F2
		public static int GetTileIndex(int index)
		{
			return index >> 12 & 524287;
		}

		// Token: 0x06002529 RID: 9513 RVA: 0x0019D4FE File Offset: 0x0019B6FE
		public int GetVertexArrayIndex(int index)
		{
			return index & 4095;
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x0019D507 File Offset: 0x0019B707
		public void GetTileCoordinates(int tileIndex, out int x, out int z)
		{
			z = tileIndex / this.tileXCount;
			x = tileIndex - z * this.tileXCount;
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x0019D520 File Offset: 0x0019B720
		public NavmeshTile[] GetTiles()
		{
			return this.tiles;
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x0019D528 File Offset: 0x0019B728
		public Bounds GetTileBounds(IntRect rect)
		{
			return this.GetTileBounds(rect.xmin, rect.ymin, rect.Width, rect.Height);
		}

		// Token: 0x0600252D RID: 9517 RVA: 0x0019D54A File Offset: 0x0019B74A
		public Bounds GetTileBounds(int x, int z, int width = 1, int depth = 1)
		{
			return this.transform.Transform(this.GetTileBoundsInGraphSpace(x, z, width, depth));
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x0019D562 File Offset: 0x0019B762
		public Bounds GetTileBoundsInGraphSpace(IntRect rect)
		{
			return this.GetTileBoundsInGraphSpace(rect.xmin, rect.ymin, rect.Width, rect.Height);
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x0019D584 File Offset: 0x0019B784
		public Bounds GetTileBoundsInGraphSpace(int x, int z, int width = 1, int depth = 1)
		{
			Bounds result = default(Bounds);
			result.SetMinMax(new Vector3((float)x * this.TileWorldSizeX, 0f, (float)z * this.TileWorldSizeZ), new Vector3((float)(x + width) * this.TileWorldSizeX, this.forcedBoundsSize.y, (float)(z + depth) * this.TileWorldSizeZ));
			return result;
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x0019D5E4 File Offset: 0x0019B7E4
		public Int2 GetTileCoordinates(Vector3 position)
		{
			position = this.transform.InverseTransform(position);
			position.x /= this.TileWorldSizeX;
			position.z /= this.TileWorldSizeZ;
			return new Int2((int)position.x, (int)position.z);
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x0019D634 File Offset: 0x0019B834
		protected override void OnDestroy()
		{
			base.OnDestroy();
			TriangleMeshNode.SetNavmeshHolder(this.active.data.GetGraphIndex(this), null);
			if (this.tiles != null)
			{
				for (int i = 0; i < this.tiles.Length; i++)
				{
					ObjectPool<BBTree>.Release(ref this.tiles[i].bbTree);
				}
			}
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x0019D68B File Offset: 0x0019B88B
		public override void RelocateNodes(Matrix4x4 deltaMatrix)
		{
			this.RelocateNodes(deltaMatrix * this.transform);
		}

		// Token: 0x06002533 RID: 9523 RVA: 0x0019D6A0 File Offset: 0x0019B8A0
		public void RelocateNodes(GraphTransform newTransform)
		{
			this.transform = newTransform;
			if (this.tiles != null)
			{
				for (int i = 0; i < this.tiles.Length; i++)
				{
					NavmeshTile navmeshTile = this.tiles[i];
					if (navmeshTile != null)
					{
						navmeshTile.vertsInGraphSpace.CopyTo(navmeshTile.verts, 0);
						this.transform.Transform(navmeshTile.verts);
						for (int j = 0; j < navmeshTile.nodes.Length; j++)
						{
							navmeshTile.nodes[j].UpdatePositionFromVertices();
						}
						navmeshTile.bbTree.RebuildFrom(navmeshTile.nodes);
					}
				}
			}
		}

		// Token: 0x06002534 RID: 9524 RVA: 0x0019D730 File Offset: 0x0019B930
		protected NavmeshTile NewEmptyTile(int x, int z)
		{
			return new NavmeshTile
			{
				x = x,
				z = z,
				w = 1,
				d = 1,
				verts = new Int3[0],
				vertsInGraphSpace = new Int3[0],
				tris = new int[0],
				nodes = new TriangleMeshNode[0],
				bbTree = ObjectPool<BBTree>.Claim(),
				graph = this
			};
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x0019D7A0 File Offset: 0x0019B9A0
		public override void GetNodes(Action<GraphNode> action)
		{
			if (this.tiles == null)
			{
				return;
			}
			for (int i = 0; i < this.tiles.Length; i++)
			{
				if (this.tiles[i] != null && this.tiles[i].x + this.tiles[i].z * this.tileXCount == i)
				{
					TriangleMeshNode[] nodes = this.tiles[i].nodes;
					if (nodes != null)
					{
						for (int j = 0; j < nodes.Length; j++)
						{
							action(nodes[j]);
						}
					}
				}
			}
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x0019D820 File Offset: 0x0019BA20
		public IntRect GetTouchingTiles(Bounds bounds)
		{
			bounds = this.transform.InverseTransform(bounds);
			return IntRect.Intersection(new IntRect(Mathf.FloorToInt(bounds.min.x / this.TileWorldSizeX), Mathf.FloorToInt(bounds.min.z / this.TileWorldSizeZ), Mathf.FloorToInt(bounds.max.x / this.TileWorldSizeX), Mathf.FloorToInt(bounds.max.z / this.TileWorldSizeZ)), new IntRect(0, 0, this.tileXCount - 1, this.tileZCount - 1));
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x0019D8BC File Offset: 0x0019BABC
		public IntRect GetTouchingTilesInGraphSpace(Rect rect)
		{
			return IntRect.Intersection(new IntRect(Mathf.FloorToInt(rect.xMin / this.TileWorldSizeX), Mathf.FloorToInt(rect.yMin / this.TileWorldSizeZ), Mathf.FloorToInt(rect.xMax / this.TileWorldSizeX), Mathf.FloorToInt(rect.yMax / this.TileWorldSizeZ)), new IntRect(0, 0, this.tileXCount - 1, this.tileZCount - 1));
		}

		// Token: 0x06002538 RID: 9528 RVA: 0x0019D938 File Offset: 0x0019BB38
		public IntRect GetTouchingTilesRound(Bounds bounds)
		{
			bounds = this.transform.InverseTransform(bounds);
			return IntRect.Intersection(new IntRect(Mathf.RoundToInt(bounds.min.x / this.TileWorldSizeX), Mathf.RoundToInt(bounds.min.z / this.TileWorldSizeZ), Mathf.RoundToInt(bounds.max.x / this.TileWorldSizeX) - 1, Mathf.RoundToInt(bounds.max.z / this.TileWorldSizeZ) - 1), new IntRect(0, 0, this.tileXCount - 1, this.tileZCount - 1));
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x0019D9D8 File Offset: 0x0019BBD8
		protected void ConnectTileWithNeighbours(NavmeshTile tile, bool onlyUnflagged = false)
		{
			if (tile.w != 1 || tile.d != 1)
			{
				throw new ArgumentException("Tile widths or depths other than 1 are not supported. The fields exist mainly for possible future expansions.");
			}
			for (int i = -1; i <= 1; i++)
			{
				int num = tile.z + i;
				if (num >= 0 && num < this.tileZCount)
				{
					for (int j = -1; j <= 1; j++)
					{
						int num2 = tile.x + j;
						if (num2 >= 0 && num2 < this.tileXCount && j == 0 != (i == 0))
						{
							NavmeshTile navmeshTile = this.tiles[num2 + num * this.tileXCount];
							if (!onlyUnflagged || !navmeshTile.flag)
							{
								this.ConnectTiles(navmeshTile, tile);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x0019DA78 File Offset: 0x0019BC78
		protected void RemoveConnectionsFromTile(NavmeshTile tile)
		{
			if (tile.x > 0)
			{
				int num = tile.x - 1;
				for (int i = tile.z; i < tile.z + tile.d; i++)
				{
					this.RemoveConnectionsFromTo(this.tiles[num + i * this.tileXCount], tile);
				}
			}
			if (tile.x + tile.w < this.tileXCount)
			{
				int num2 = tile.x + tile.w;
				for (int j = tile.z; j < tile.z + tile.d; j++)
				{
					this.RemoveConnectionsFromTo(this.tiles[num2 + j * this.tileXCount], tile);
				}
			}
			if (tile.z > 0)
			{
				int num3 = tile.z - 1;
				for (int k = tile.x; k < tile.x + tile.w; k++)
				{
					this.RemoveConnectionsFromTo(this.tiles[k + num3 * this.tileXCount], tile);
				}
			}
			if (tile.z + tile.d < this.tileZCount)
			{
				int num4 = tile.z + tile.d;
				for (int l = tile.x; l < tile.x + tile.w; l++)
				{
					this.RemoveConnectionsFromTo(this.tiles[l + num4 * this.tileXCount], tile);
				}
			}
		}

		// Token: 0x0600253B RID: 9531 RVA: 0x0019DBD4 File Offset: 0x0019BDD4
		protected void RemoveConnectionsFromTo(NavmeshTile a, NavmeshTile b)
		{
			if (a == null || b == null)
			{
				return;
			}
			if (a == b)
			{
				return;
			}
			int num = b.x + b.z * this.tileXCount;
			for (int i = 0; i < a.nodes.Length; i++)
			{
				TriangleMeshNode triangleMeshNode = a.nodes[i];
				if (triangleMeshNode.connections != null)
				{
					for (int j = 0; j < triangleMeshNode.connections.Length; j++)
					{
						TriangleMeshNode triangleMeshNode2 = triangleMeshNode.connections[j].node as TriangleMeshNode;
						if (triangleMeshNode2 != null && (triangleMeshNode2.GetVertexIndex(0) >> 12 & 524287) == num)
						{
							triangleMeshNode.RemoveConnection(triangleMeshNode.connections[j].node);
							j--;
						}
					}
				}
			}
		}

		// Token: 0x0600253C RID: 9532 RVA: 0x0019DC84 File Offset: 0x0019BE84
		public override NNInfoInternal GetNearest(Vector3 position, NNConstraint constraint, GraphNode hint)
		{
			return this.GetNearestForce(position, (constraint != null && constraint.distanceXZ) ? NavmeshBase.NNConstraintDistanceXZ : null);
		}

		// Token: 0x0600253D RID: 9533 RVA: 0x0019DCA0 File Offset: 0x0019BEA0
		public override NNInfoInternal GetNearestForce(Vector3 position, NNConstraint constraint)
		{
			if (this.tiles == null)
			{
				return default(NNInfoInternal);
			}
			Int2 tileCoordinates = this.GetTileCoordinates(position);
			tileCoordinates.x = Mathf.Clamp(tileCoordinates.x, 0, this.tileXCount - 1);
			tileCoordinates.y = Mathf.Clamp(tileCoordinates.y, 0, this.tileZCount - 1);
			int num = Math.Max(this.tileXCount, this.tileZCount);
			NNInfoInternal nninfoInternal = default(NNInfoInternal);
			float positiveInfinity = float.PositiveInfinity;
			bool flag = this.nearestSearchOnlyXZ || (constraint != null && constraint.distanceXZ);
			int num2 = 0;
			while (num2 < num && positiveInfinity >= (float)(num2 - 2) * Math.Max(this.TileWorldSizeX, this.TileWorldSizeX))
			{
				int num3 = Math.Min(num2 + tileCoordinates.y + 1, this.tileZCount);
				for (int i = Math.Max(-num2 + tileCoordinates.y, 0); i < num3; i++)
				{
					int num4 = Math.Abs(num2 - Math.Abs(i - tileCoordinates.y));
					int num5 = num4;
					do
					{
						int num6 = -num5 + tileCoordinates.x;
						if (num6 >= 0 && num6 < this.tileXCount)
						{
							NavmeshTile navmeshTile = this.tiles[num6 + i * this.tileXCount];
							if (navmeshTile != null)
							{
								if (flag)
								{
									nninfoInternal = navmeshTile.bbTree.QueryClosestXZ(position, constraint, ref positiveInfinity, nninfoInternal);
								}
								else
								{
									nninfoInternal = navmeshTile.bbTree.QueryClosest(position, constraint, ref positiveInfinity, nninfoInternal);
								}
							}
						}
						num5 = -num5;
					}
					while (num5 != num4);
				}
				num2++;
			}
			nninfoInternal.node = nninfoInternal.constrainedNode;
			nninfoInternal.constrainedNode = null;
			nninfoInternal.clampedPosition = nninfoInternal.constClampedPosition;
			return nninfoInternal;
		}

		// Token: 0x0600253E RID: 9534 RVA: 0x0019DE50 File Offset: 0x0019C050
		public GraphNode PointOnNavmesh(Vector3 position, NNConstraint constraint)
		{
			if (this.tiles == null)
			{
				return null;
			}
			Int2 tileCoordinates = this.GetTileCoordinates(position);
			if (tileCoordinates.x < 0 || tileCoordinates.y < 0 || tileCoordinates.x >= this.tileXCount || tileCoordinates.y >= this.tileZCount)
			{
				return null;
			}
			NavmeshTile tile = this.GetTile(tileCoordinates.x, tileCoordinates.y);
			if (tile != null)
			{
				return tile.bbTree.QueryInside(position, constraint);
			}
			return null;
		}

		// Token: 0x0600253F RID: 9535 RVA: 0x0019DEC4 File Offset: 0x0019C0C4
		protected void FillWithEmptyTiles()
		{
			for (int i = 0; i < this.tileZCount; i++)
			{
				for (int j = 0; j < this.tileXCount; j++)
				{
					this.tiles[i * this.tileXCount + j] = this.NewEmptyTile(j, i);
				}
			}
		}

		// Token: 0x06002540 RID: 9536 RVA: 0x0019DF0C File Offset: 0x0019C10C
		protected static void CreateNodeConnections(TriangleMeshNode[] nodes)
		{
			List<Connection> list = ListPool<Connection>.Claim();
			Dictionary<Int2, int> dictionary = ObjectPoolSimple<Dictionary<Int2, int>>.Claim();
			dictionary.Clear();
			for (int i = 0; i < nodes.Length; i++)
			{
				TriangleMeshNode triangleMeshNode = nodes[i];
				int vertexCount = triangleMeshNode.GetVertexCount();
				for (int j = 0; j < vertexCount; j++)
				{
					Int2 key = new Int2(triangleMeshNode.GetVertexIndex(j), triangleMeshNode.GetVertexIndex((j + 1) % vertexCount));
					if (!dictionary.ContainsKey(key))
					{
						dictionary.Add(key, i);
					}
				}
			}
			foreach (TriangleMeshNode triangleMeshNode2 in nodes)
			{
				list.Clear();
				int vertexCount2 = triangleMeshNode2.GetVertexCount();
				for (int l = 0; l < vertexCount2; l++)
				{
					int vertexIndex = triangleMeshNode2.GetVertexIndex(l);
					int vertexIndex2 = triangleMeshNode2.GetVertexIndex((l + 1) % vertexCount2);
					int num;
					if (dictionary.TryGetValue(new Int2(vertexIndex2, vertexIndex), out num))
					{
						TriangleMeshNode triangleMeshNode3 = nodes[num];
						int vertexCount3 = triangleMeshNode3.GetVertexCount();
						for (int m = 0; m < vertexCount3; m++)
						{
							if (triangleMeshNode3.GetVertexIndex(m) == vertexIndex2 && triangleMeshNode3.GetVertexIndex((m + 1) % vertexCount3) == vertexIndex)
							{
								list.Add(new Connection(triangleMeshNode3, (uint)(triangleMeshNode2.position - triangleMeshNode3.position).costMagnitude, (byte)l));
								break;
							}
						}
					}
				}
				triangleMeshNode2.connections = list.ToArrayFromPool<Connection>();
			}
			dictionary.Clear();
			ObjectPoolSimple<Dictionary<Int2, int>>.Release(ref dictionary);
			ListPool<Connection>.Release(ref list);
		}

		// Token: 0x06002541 RID: 9537 RVA: 0x0019E088 File Offset: 0x0019C288
		protected void ConnectTiles(NavmeshTile tile1, NavmeshTile tile2)
		{
			if (tile1 == null || tile2 == null)
			{
				return;
			}
			if (tile1.nodes == null)
			{
				throw new ArgumentException("tile1 does not contain any nodes");
			}
			if (tile2.nodes == null)
			{
				throw new ArgumentException("tile2 does not contain any nodes");
			}
			int num = Mathf.Clamp(tile2.x, tile1.x, tile1.x + tile1.w - 1);
			int num2 = Mathf.Clamp(tile1.x, tile2.x, tile2.x + tile2.w - 1);
			int num3 = Mathf.Clamp(tile2.z, tile1.z, tile1.z + tile1.d - 1);
			int num4 = Mathf.Clamp(tile1.z, tile2.z, tile2.z + tile2.d - 1);
			int i;
			int i2;
			int num5;
			int num6;
			float num7;
			if (num == num2)
			{
				i = 2;
				i2 = 0;
				num5 = num3;
				num6 = num4;
				num7 = this.TileWorldSizeZ;
			}
			else
			{
				if (num3 != num4)
				{
					throw new ArgumentException("Tiles are not adjacent (neither x or z coordinates match)");
				}
				i = 0;
				i2 = 2;
				num5 = num;
				num6 = num2;
				num7 = this.TileWorldSizeX;
			}
			if (Math.Abs(num5 - num6) != 1)
			{
				throw new ArgumentException(string.Concat(new object[]
				{
					"Tiles are not adjacent (tile coordinates must differ by exactly 1. Got '",
					num5,
					"' and '",
					num6,
					"')"
				}));
			}
			int num8 = (int)Math.Round((double)((float)Math.Max(num5, num6) * num7 * 1000f));
			TriangleMeshNode[] nodes = tile1.nodes;
			TriangleMeshNode[] nodes2 = tile2.nodes;
			foreach (TriangleMeshNode triangleMeshNode in nodes)
			{
				int vertexCount = triangleMeshNode.GetVertexCount();
				for (int k = 0; k < vertexCount; k++)
				{
					Int3 vertexInGraphSpace = triangleMeshNode.GetVertexInGraphSpace(k);
					Int3 vertexInGraphSpace2 = triangleMeshNode.GetVertexInGraphSpace((k + 1) % vertexCount);
					if (Math.Abs(vertexInGraphSpace[i] - num8) < 2 && Math.Abs(vertexInGraphSpace2[i] - num8) < 2)
					{
						int num9 = Math.Min(vertexInGraphSpace[i2], vertexInGraphSpace2[i2]);
						int num10 = Math.Max(vertexInGraphSpace[i2], vertexInGraphSpace2[i2]);
						if (num9 != num10)
						{
							foreach (TriangleMeshNode triangleMeshNode2 in nodes2)
							{
								int vertexCount2 = triangleMeshNode2.GetVertexCount();
								for (int m = 0; m < vertexCount2; m++)
								{
									Int3 vertexInGraphSpace3 = triangleMeshNode2.GetVertexInGraphSpace(m);
									Int3 vertexInGraphSpace4 = triangleMeshNode2.GetVertexInGraphSpace((m + 1) % vertexCount);
									if (Math.Abs(vertexInGraphSpace3[i] - num8) < 2 && Math.Abs(vertexInGraphSpace4[i] - num8) < 2)
									{
										int num11 = Math.Min(vertexInGraphSpace3[i2], vertexInGraphSpace4[i2]);
										int num12 = Math.Max(vertexInGraphSpace3[i2], vertexInGraphSpace4[i2]);
										if (num11 != num12 && num10 > num11 && num9 < num12 && ((vertexInGraphSpace == vertexInGraphSpace3 && vertexInGraphSpace2 == vertexInGraphSpace4) || (vertexInGraphSpace == vertexInGraphSpace4 && vertexInGraphSpace2 == vertexInGraphSpace3) || VectorMath.SqrDistanceSegmentSegment((Vector3)vertexInGraphSpace, (Vector3)vertexInGraphSpace2, (Vector3)vertexInGraphSpace3, (Vector3)vertexInGraphSpace4) < this.MaxTileConnectionEdgeDistance * this.MaxTileConnectionEdgeDistance))
										{
											uint costMagnitude = (uint)(triangleMeshNode.position - triangleMeshNode2.position).costMagnitude;
											triangleMeshNode.AddConnection(triangleMeshNode2, costMagnitude, k);
											triangleMeshNode2.AddConnection(triangleMeshNode, costMagnitude, m);
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06002542 RID: 9538 RVA: 0x0019E429 File Offset: 0x0019C629
		public void StartBatchTileUpdate()
		{
			if (this.batchTileUpdate)
			{
				throw new InvalidOperationException("Calling StartBatchLoad when batching is already enabled");
			}
			this.batchTileUpdate = true;
		}

		// Token: 0x06002543 RID: 9539 RVA: 0x0019E448 File Offset: 0x0019C648
		private void DestroyNodes(List<MeshNode> nodes)
		{
			for (int i = 0; i < this.batchNodesToDestroy.Count; i++)
			{
				this.batchNodesToDestroy[i].TemporaryFlag1 = true;
			}
			for (int j = 0; j < this.batchNodesToDestroy.Count; j++)
			{
				MeshNode meshNode = this.batchNodesToDestroy[j];
				for (int k = 0; k < meshNode.connections.Length; k++)
				{
					GraphNode node = meshNode.connections[k].node;
					if (!node.TemporaryFlag1)
					{
						node.RemoveConnection(meshNode);
					}
				}
				ArrayPool<Connection>.Release(ref meshNode.connections, true);
				meshNode.Destroy();
			}
		}

		// Token: 0x06002544 RID: 9540 RVA: 0x0019E4E9 File Offset: 0x0019C6E9
		private void TryConnect(int tileIdx1, int tileIdx2)
		{
			if (this.tiles[tileIdx1].flag && this.tiles[tileIdx2].flag && tileIdx1 >= tileIdx2)
			{
				return;
			}
			this.ConnectTiles(this.tiles[tileIdx1], this.tiles[tileIdx2]);
		}

		// Token: 0x06002545 RID: 9541 RVA: 0x0019E524 File Offset: 0x0019C724
		public void EndBatchTileUpdate()
		{
			if (!this.batchTileUpdate)
			{
				throw new InvalidOperationException("Calling EndBatchTileUpdate when batching had not yet been started");
			}
			this.batchTileUpdate = false;
			this.DestroyNodes(this.batchNodesToDestroy);
			this.batchNodesToDestroy.ClearFast<MeshNode>();
			for (int i = 0; i < this.batchUpdatedTiles.Count; i++)
			{
				this.tiles[this.batchUpdatedTiles[i]].flag = true;
			}
			for (int j = 0; j < this.batchUpdatedTiles.Count; j++)
			{
				int num = this.batchUpdatedTiles[j] % this.tileXCount;
				int num2 = this.batchUpdatedTiles[j] / this.tileXCount;
				if (num > 0)
				{
					this.TryConnect(this.batchUpdatedTiles[j], this.batchUpdatedTiles[j] - 1);
				}
				if (num < this.tileXCount - 1)
				{
					this.TryConnect(this.batchUpdatedTiles[j], this.batchUpdatedTiles[j] + 1);
				}
				if (num2 > 0)
				{
					this.TryConnect(this.batchUpdatedTiles[j], this.batchUpdatedTiles[j] - this.tileXCount);
				}
				if (num2 < this.tileZCount - 1)
				{
					this.TryConnect(this.batchUpdatedTiles[j], this.batchUpdatedTiles[j] + this.tileXCount);
				}
			}
			for (int k = 0; k < this.batchUpdatedTiles.Count; k++)
			{
				this.tiles[this.batchUpdatedTiles[k]].flag = false;
			}
			this.batchUpdatedTiles.ClearFast<int>();
		}

		// Token: 0x06002546 RID: 9542 RVA: 0x0019E6B8 File Offset: 0x0019C8B8
		protected void ClearTile(int x, int z)
		{
			if (!this.batchTileUpdate)
			{
				throw new Exception("Must be called during a batch update. See StartBatchTileUpdate");
			}
			NavmeshTile tile = this.GetTile(x, z);
			if (tile == null)
			{
				return;
			}
			TriangleMeshNode[] nodes = tile.nodes;
			for (int i = 0; i < nodes.Length; i++)
			{
				if (nodes[i] != null)
				{
					this.batchNodesToDestroy.Add(nodes[i]);
				}
			}
			ObjectPool<BBTree>.Release(ref tile.bbTree);
			this.tiles[x + z * this.tileXCount] = null;
		}

		// Token: 0x06002547 RID: 9543 RVA: 0x0019E72C File Offset: 0x0019C92C
		private void PrepareNodeRecycling(int x, int z, Int3[] verts, int[] tris, TriangleMeshNode[] recycledNodeBuffer)
		{
			NavmeshTile tile = this.GetTile(x, z);
			if (tile == null || tile.nodes.Length == 0)
			{
				return;
			}
			TriangleMeshNode[] nodes = tile.nodes;
			Dictionary<int, int> dictionary = this.nodeRecyclingHashBuffer;
			int i = 0;
			int num = 0;
			while (i < tris.Length)
			{
				dictionary[verts[tris[i]].GetHashCode() + verts[tris[i + 1]].GetHashCode() + verts[tris[i + 2]].GetHashCode()] = num;
				i += 3;
				num++;
			}
			List<Connection> list = ListPool<Connection>.Claim();
			for (int j = 0; j < nodes.Length; j++)
			{
				TriangleMeshNode triangleMeshNode = nodes[j];
				Int3 rhs;
				Int3 rhs2;
				Int3 rhs3;
				triangleMeshNode.GetVerticesInGraphSpace(out rhs, out rhs2, out rhs3);
				int key = rhs.GetHashCode() + rhs2.GetHashCode() + rhs3.GetHashCode();
				int num2;
				if (dictionary.TryGetValue(key, out num2) && verts[tris[3 * num2]] == rhs && verts[tris[3 * num2 + 1]] == rhs2 && verts[tris[3 * num2 + 2]] == rhs3)
				{
					recycledNodeBuffer[num2] = triangleMeshNode;
					nodes[j] = null;
					for (int k = 0; k < triangleMeshNode.connections.Length; k++)
					{
						if (triangleMeshNode.connections[k].node.GraphIndex != triangleMeshNode.GraphIndex)
						{
							list.Add(triangleMeshNode.connections[k]);
						}
					}
					ArrayPool<Connection>.Release(ref triangleMeshNode.connections, true);
					if (list.Count > 0)
					{
						triangleMeshNode.connections = list.ToArrayFromPool<Connection>();
						list.Clear();
					}
				}
			}
			dictionary.Clear();
			ListPool<Connection>.Release(ref list);
		}

		// Token: 0x06002548 RID: 9544 RVA: 0x0019E914 File Offset: 0x0019CB14
		public void ReplaceTile(int x, int z, Int3[] verts, int[] tris)
		{
			int num = 1;
			int num2 = 1;
			if (x + num > this.tileXCount || z + num2 > this.tileZCount || x < 0 || z < 0)
			{
				throw new ArgumentException(string.Concat(new object[]
				{
					"Tile is placed at an out of bounds position or extends out of the graph bounds (",
					x,
					", ",
					z,
					" [",
					num,
					", ",
					num2,
					"] ",
					this.tileXCount,
					" ",
					this.tileZCount,
					")"
				}));
			}
			if (tris.Length % 3 != 0)
			{
				throw new ArgumentException("Triangle array's length must be a multiple of 3 (tris)");
			}
			if (verts.Length > 4095)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"Too many vertices in the tile (",
					verts.Length,
					" > ",
					4095,
					")\nYou can enable ASTAR_RECAST_LARGER_TILES under the 'Optimizations' tab in the A* Inspector to raise this limit. Or you can use a smaller tile size to reduce the likelihood of this happening."
				}));
				verts = new Int3[0];
				tris = new int[0];
			}
			bool flag = !this.batchTileUpdate;
			if (flag)
			{
				this.StartBatchTileUpdate();
			}
			NavmeshTile navmeshTile = new NavmeshTile
			{
				x = x,
				z = z,
				w = num,
				d = num2,
				tris = tris,
				bbTree = ObjectPool<BBTree>.Claim(),
				graph = this
			};
			if (!Mathf.Approximately((float)x * this.TileWorldSizeX * 1000f, (float)Math.Round((double)((float)x * this.TileWorldSizeX * 1000f))))
			{
				Debug.LogWarning("Possible numerical imprecision. Consider adjusting tileSize and/or cellSize");
			}
			if (!Mathf.Approximately((float)z * this.TileWorldSizeZ * 1000f, (float)Math.Round((double)((float)z * this.TileWorldSizeZ * 1000f))))
			{
				Debug.LogWarning("Possible numerical imprecision. Consider adjusting tileSize and/or cellSize");
			}
			Int3 rhs = (Int3)new Vector3((float)x * this.TileWorldSizeX, 0f, (float)z * this.TileWorldSizeZ);
			for (int i = 0; i < verts.Length; i++)
			{
				verts[i] += rhs;
			}
			navmeshTile.vertsInGraphSpace = verts;
			navmeshTile.verts = (Int3[])verts.Clone();
			this.transform.Transform(navmeshTile.verts);
			TriangleMeshNode[] array = navmeshTile.nodes = new TriangleMeshNode[tris.Length / 3];
			this.PrepareNodeRecycling(x, z, navmeshTile.vertsInGraphSpace, tris, navmeshTile.nodes);
			this.ClearTile(x, z);
			this.tiles[x + z * this.tileXCount] = navmeshTile;
			this.batchUpdatedTiles.Add(x + z * this.tileXCount);
			this.CreateNodes(array, navmeshTile.tris, x + z * this.tileXCount, (uint)this.active.data.GetGraphIndex(this));
			navmeshTile.bbTree.RebuildFrom(array);
			NavmeshBase.CreateNodeConnections(navmeshTile.nodes);
			if (flag)
			{
				this.EndBatchTileUpdate();
			}
		}

		// Token: 0x06002549 RID: 9545 RVA: 0x0019EC1C File Offset: 0x0019CE1C
		protected void CreateNodes(TriangleMeshNode[] buffer, int[] tris, int tileIndex, uint graphIndex)
		{
			if (buffer == null || buffer.Length < tris.Length / 3)
			{
				throw new ArgumentException("buffer must be non null and at least as large as tris.Length/3");
			}
			tileIndex <<= 12;
			for (int i = 0; i < buffer.Length; i++)
			{
				TriangleMeshNode triangleMeshNode = buffer[i];
				if (triangleMeshNode == null)
				{
					triangleMeshNode = (buffer[i] = new TriangleMeshNode(this.active));
				}
				triangleMeshNode.Walkable = true;
				triangleMeshNode.Tag = 0u;
				triangleMeshNode.Penalty = this.initialPenalty;
				triangleMeshNode.GraphIndex = graphIndex;
				triangleMeshNode.v0 = (tris[i * 3] | tileIndex);
				triangleMeshNode.v1 = (tris[i * 3 + 1] | tileIndex);
				triangleMeshNode.v2 = (tris[i * 3 + 2] | tileIndex);
				if (this.RecalculateNormals && !VectorMath.IsClockwiseXZ(triangleMeshNode.GetVertexInGraphSpace(0), triangleMeshNode.GetVertexInGraphSpace(1), triangleMeshNode.GetVertexInGraphSpace(2)))
				{
					Memory.Swap<int>(ref triangleMeshNode.v0, ref triangleMeshNode.v2);
				}
				triangleMeshNode.UpdatePositionFromVertices();
			}
		}

		// Token: 0x0600254A RID: 9546 RVA: 0x0019ECFC File Offset: 0x0019CEFC
		public bool Linecast(Vector3 origin, Vector3 end)
		{
			return this.Linecast(origin, end, base.GetNearest(origin, NNConstraint.None).node);
		}

		// Token: 0x0600254B RID: 9547 RVA: 0x0019ED17 File Offset: 0x0019CF17
		public bool Linecast(Vector3 origin, Vector3 end, GraphNode hint, out GraphHitInfo hit)
		{
			return NavmeshBase.Linecast(this, origin, end, hint, out hit, null);
		}

		// Token: 0x0600254C RID: 9548 RVA: 0x0019ED28 File Offset: 0x0019CF28
		public bool Linecast(Vector3 origin, Vector3 end, GraphNode hint)
		{
			GraphHitInfo graphHitInfo;
			return NavmeshBase.Linecast(this, origin, end, hint, out graphHitInfo, null);
		}

		// Token: 0x0600254D RID: 9549 RVA: 0x0019ED41 File Offset: 0x0019CF41
		public bool Linecast(Vector3 origin, Vector3 end, GraphNode hint, out GraphHitInfo hit, List<GraphNode> trace)
		{
			return NavmeshBase.Linecast(this, origin, end, hint, out hit, trace);
		}

		// Token: 0x0600254E RID: 9550 RVA: 0x0019ED17 File Offset: 0x0019CF17
		public static bool Linecast(NavmeshBase graph, Vector3 origin, Vector3 end, GraphNode hint, out GraphHitInfo hit)
		{
			return NavmeshBase.Linecast(graph, origin, end, hint, out hit, null);
		}

		// Token: 0x0600254F RID: 9551 RVA: 0x0019ED50 File Offset: 0x0019CF50
		static NavmeshBase()
		{
			Side[] array = new Side[3];
			for (int i = 0; i < NavmeshBase.LinecastShapeEdgeLookup.Length; i++)
			{
				array[0] = (Side)(i & 3);
				array[1] = (Side)(i >> 2 & 3);
				array[2] = (Side)(i >> 4 & 3);
				NavmeshBase.LinecastShapeEdgeLookup[i] = byte.MaxValue;
				if (array[0] != (Side)3 && array[1] != (Side)3 && array[2] != (Side)3)
				{
					int num = int.MaxValue;
					for (int j = 0; j < 3; j++)
					{
						if ((array[j] == Side.Left || array[j] == Side.Colinear) && (array[(j + 1) % 3] == Side.Right || array[(j + 1) % 3] == Side.Colinear))
						{
							int num2 = ((array[j] == Side.Colinear) ? 1 : 0) + ((array[(j + 1) % 3] == Side.Colinear) ? 1 : 0);
							if (num2 < num)
							{
								NavmeshBase.LinecastShapeEdgeLookup[i] = (byte)j;
								num = num2;
							}
						}
					}
				}
			}
		}

		// Token: 0x06002550 RID: 9552 RVA: 0x0019EE34 File Offset: 0x0019D034
		public static bool Linecast(NavmeshBase graph, Vector3 origin, Vector3 end, GraphNode hint, out GraphHitInfo hit, List<GraphNode> trace)
		{
			hit = default(GraphHitInfo);
			if (float.IsNaN(origin.x + origin.y + origin.z))
			{
				throw new ArgumentException("origin is NaN");
			}
			if (float.IsNaN(end.x + end.y + end.z))
			{
				throw new ArgumentException("end is NaN");
			}
			TriangleMeshNode triangleMeshNode = hint as TriangleMeshNode;
			if (triangleMeshNode == null)
			{
				triangleMeshNode = (graph.GetNearest(origin, NavmeshBase.NNConstraintNone).node as TriangleMeshNode);
				if (triangleMeshNode == null)
				{
					Debug.LogError("Could not find a valid node to start from");
					hit.origin = origin;
					hit.point = origin;
					return true;
				}
			}
			Int3 @int = triangleMeshNode.ClosestPointOnNodeXZInGraphSpace(origin);
			hit.origin = graph.transform.Transform((Vector3)@int);
			if (!triangleMeshNode.Walkable)
			{
				hit.node = triangleMeshNode;
				hit.point = hit.origin;
				hit.tangentOrigin = hit.origin;
				return true;
			}
			Int3 int2 = (Int3)graph.transform.InverseTransform(end);
			if (@int == int2)
			{
				hit.point = hit.origin;
				hit.node = triangleMeshNode;
				return false;
			}
			int num = 0;
			Int3 int3;
			Int3 int4;
			Int3 int5;
			int num3;
			for (;;)
			{
				num++;
				if (num > 2000)
				{
					break;
				}
				if (trace != null)
				{
					trace.Add(triangleMeshNode);
				}
				triangleMeshNode.GetVerticesInGraphSpace(out int3, out int4, out int5);
				int num2 = (int)VectorMath.SideXZ(@int, int2, int3);
				num2 |= (int)((int)VectorMath.SideXZ(@int, int2, int4) << 2);
				num2 |= (int)((int)VectorMath.SideXZ(@int, int2, int5) << 4);
				num3 = (int)NavmeshBase.LinecastShapeEdgeLookup[num2];
				if (VectorMath.SideXZ((num3 == 0) ? int3 : ((num3 == 1) ? int4 : int5), (num3 == 0) ? int4 : ((num3 == 1) ? int5 : int3), int2) != Side.Left)
				{
					goto Block_13;
				}
				if (num3 == 255)
				{
					goto Block_14;
				}
				bool flag = false;
				Connection[] connections = triangleMeshNode.connections;
				for (int i = 0; i < connections.Length; i++)
				{
					if ((int)connections[i].shapeEdge == num3)
					{
						TriangleMeshNode triangleMeshNode2 = connections[i].node as TriangleMeshNode;
						if (triangleMeshNode2 != null && triangleMeshNode2.Walkable)
						{
							Connection[] connections2 = triangleMeshNode2.connections;
							int num4 = -1;
							for (int j = 0; j < connections2.Length; j++)
							{
								if (connections2[j].node == triangleMeshNode)
								{
									num4 = (int)connections2[j].shapeEdge;
									break;
								}
							}
							if (num4 != -1)
							{
								Side side = VectorMath.SideXZ(@int, int2, triangleMeshNode2.GetVertexInGraphSpace(num4));
								Side side2 = VectorMath.SideXZ(@int, int2, triangleMeshNode2.GetVertexInGraphSpace((num4 + 1) % 3));
								flag = ((side == Side.Right || side == Side.Colinear) && (side2 == Side.Left || side2 == Side.Colinear));
								if (flag)
								{
									triangleMeshNode = triangleMeshNode2;
									break;
								}
							}
						}
					}
				}
				if (!flag)
				{
					goto Block_23;
				}
			}
			Debug.LogError("Linecast was stuck in infinite loop. Breaking.");
			return true;
			Block_13:
			hit.point = end;
			hit.node = triangleMeshNode;
			return false;
			Block_14:
			Debug.LogError("Line does not intersect node at all");
			hit.node = triangleMeshNode;
			hit.point = (hit.tangentOrigin = hit.origin);
			return true;
			Block_23:
			Vector3 vector = (Vector3)((num3 == 0) ? int3 : ((num3 == 1) ? int4 : int5));
			Vector3 vector2 = (Vector3)((num3 == 0) ? int4 : ((num3 == 1) ? int5 : int3));
			Vector3 point = VectorMath.LineIntersectionPointXZ(vector, vector2, (Vector3)@int, (Vector3)int2);
			hit.point = graph.transform.Transform(point);
			hit.node = triangleMeshNode;
			Vector3 vector3 = graph.transform.Transform(vector);
			Vector3 a = graph.transform.Transform(vector2);
			hit.tangent = a - vector3;
			hit.tangentOrigin = vector3;
			return true;
		}

		// Token: 0x06002551 RID: 9553 RVA: 0x0019F1D0 File Offset: 0x0019D3D0
		public override void OnDrawGizmos(RetainedGizmos gizmos, bool drawNodes)
		{
			if (!drawNodes)
			{
				return;
			}
			using (GraphGizmoHelper singleFrameGizmoHelper = gizmos.GetSingleFrameGizmoHelper(this.active))
			{
				Bounds bounds = default(Bounds);
				bounds.SetMinMax(Vector3.zero, this.forcedBoundsSize);
				singleFrameGizmoHelper.builder.DrawWireCube(this.CalculateTransform(), bounds, Color.white);
			}
			if (this.tiles != null)
			{
				for (int i = 0; i < this.tiles.Length; i++)
				{
					if (this.tiles[i] != null)
					{
						RetainedGizmos.Hasher hasher = new RetainedGizmos.Hasher(this.active);
						hasher.AddHash(this.showMeshOutline ? 1 : 0);
						hasher.AddHash(this.showMeshSurface ? 1 : 0);
						hasher.AddHash(this.showNodeConnections ? 1 : 0);
						TriangleMeshNode[] nodes = this.tiles[i].nodes;
						for (int j = 0; j < nodes.Length; j++)
						{
							hasher.HashNode(nodes[j]);
						}
						if (!gizmos.Draw(hasher))
						{
							using (GraphGizmoHelper gizmoHelper = gizmos.GetGizmoHelper(this.active, hasher))
							{
								if (this.showMeshSurface || this.showMeshOutline)
								{
									this.CreateNavmeshSurfaceVisualization(this.tiles[i], gizmoHelper);
								}
								if (this.showMeshSurface || this.showMeshOutline)
								{
									NavmeshBase.CreateNavmeshOutlineVisualization(this.tiles[i], gizmoHelper);
								}
								if (this.showNodeConnections)
								{
									for (int k = 0; k < nodes.Length; k++)
									{
										gizmoHelper.DrawConnections(nodes[k]);
									}
								}
							}
						}
						gizmos.Draw(hasher);
					}
				}
			}
			if (this.active.showUnwalkableNodes)
			{
				base.DrawUnwalkableNodes(this.active.unwalkableNodeDebugSize);
			}
		}

		// Token: 0x06002552 RID: 9554 RVA: 0x0019F3A4 File Offset: 0x0019D5A4
		private void CreateNavmeshSurfaceVisualization(NavmeshTile tile, GraphGizmoHelper helper)
		{
			Vector3[] array = ArrayPool<Vector3>.Claim(tile.nodes.Length * 3);
			Color[] array2 = ArrayPool<Color>.Claim(tile.nodes.Length * 3);
			for (int i = 0; i < tile.nodes.Length; i++)
			{
				TriangleMeshNode triangleMeshNode = tile.nodes[i];
				Int3 ob;
				Int3 ob2;
				Int3 ob3;
				triangleMeshNode.GetVertices(out ob, out ob2, out ob3);
				array[i * 3] = (Vector3)ob;
				array[i * 3 + 1] = (Vector3)ob2;
				array[i * 3 + 2] = (Vector3)ob3;
				Color color = helper.NodeColor(triangleMeshNode);
				array2[i * 3] = (array2[i * 3 + 1] = (array2[i * 3 + 2] = color));
			}
			if (this.showMeshSurface)
			{
				helper.DrawTriangles(array, array2, tile.nodes.Length);
			}
			if (this.showMeshOutline)
			{
				helper.DrawWireTriangles(array, array2, tile.nodes.Length);
			}
			ArrayPool<Vector3>.Release(ref array, false);
			ArrayPool<Color>.Release(ref array2, false);
		}

		// Token: 0x06002553 RID: 9555 RVA: 0x0019F4A8 File Offset: 0x0019D6A8
		private static void CreateNavmeshOutlineVisualization(NavmeshTile tile, GraphGizmoHelper helper)
		{
			bool[] array = new bool[3];
			for (int i = 0; i < tile.nodes.Length; i++)
			{
				array[0] = (array[1] = (array[2] = false));
				TriangleMeshNode triangleMeshNode = tile.nodes[i];
				for (int j = 0; j < triangleMeshNode.connections.Length; j++)
				{
					TriangleMeshNode triangleMeshNode2 = triangleMeshNode.connections[j].node as TriangleMeshNode;
					if (triangleMeshNode2 != null && triangleMeshNode2.GraphIndex == triangleMeshNode.GraphIndex)
					{
						for (int k = 0; k < 3; k++)
						{
							for (int l = 0; l < 3; l++)
							{
								if (triangleMeshNode.GetVertexIndex(k) == triangleMeshNode2.GetVertexIndex((l + 1) % 3) && triangleMeshNode.GetVertexIndex((k + 1) % 3) == triangleMeshNode2.GetVertexIndex(l))
								{
									array[k] = true;
									k = 3;
									break;
								}
							}
						}
					}
				}
				Color color = helper.NodeColor(triangleMeshNode);
				for (int m = 0; m < 3; m++)
				{
					if (!array[m])
					{
						helper.builder.DrawLine((Vector3)triangleMeshNode.GetVertex(m), (Vector3)triangleMeshNode.GetVertex((m + 1) % 3), color);
					}
				}
			}
		}

		// Token: 0x06002554 RID: 9556 RVA: 0x0019F5DC File Offset: 0x0019D7DC
		protected override void SerializeExtraInfo(GraphSerializationContext ctx)
		{
			BinaryWriter writer = ctx.writer;
			if (this.tiles == null)
			{
				writer.Write(-1);
				return;
			}
			writer.Write(this.tileXCount);
			writer.Write(this.tileZCount);
			for (int i = 0; i < this.tileZCount; i++)
			{
				for (int j = 0; j < this.tileXCount; j++)
				{
					NavmeshTile navmeshTile = this.tiles[j + i * this.tileXCount];
					if (navmeshTile == null)
					{
						throw new Exception("NULL Tile");
					}
					writer.Write(navmeshTile.x);
					writer.Write(navmeshTile.z);
					if (navmeshTile.x == j && navmeshTile.z == i)
					{
						writer.Write(navmeshTile.w);
						writer.Write(navmeshTile.d);
						writer.Write(navmeshTile.tris.Length);
						for (int k = 0; k < navmeshTile.tris.Length; k++)
						{
							writer.Write(navmeshTile.tris[k]);
						}
						writer.Write(navmeshTile.verts.Length);
						for (int l = 0; l < navmeshTile.verts.Length; l++)
						{
							ctx.SerializeInt3(navmeshTile.verts[l]);
						}
						writer.Write(navmeshTile.vertsInGraphSpace.Length);
						for (int m = 0; m < navmeshTile.vertsInGraphSpace.Length; m++)
						{
							ctx.SerializeInt3(navmeshTile.vertsInGraphSpace[m]);
						}
						writer.Write(navmeshTile.nodes.Length);
						for (int n = 0; n < navmeshTile.nodes.Length; n++)
						{
							navmeshTile.nodes[n].SerializeNode(ctx);
						}
					}
				}
			}
		}

		// Token: 0x06002555 RID: 9557 RVA: 0x0019F788 File Offset: 0x0019D988
		protected override void DeserializeExtraInfo(GraphSerializationContext ctx)
		{
			BinaryReader reader = ctx.reader;
			this.tileXCount = reader.ReadInt32();
			if (this.tileXCount < 0)
			{
				return;
			}
			this.tileZCount = reader.ReadInt32();
			this.transform = this.CalculateTransform();
			this.tiles = new NavmeshTile[this.tileXCount * this.tileZCount];
			TriangleMeshNode.SetNavmeshHolder((int)ctx.graphIndex, this);
			for (int i = 0; i < this.tileZCount; i++)
			{
				for (int j = 0; j < this.tileXCount; j++)
				{
					int num = j + i * this.tileXCount;
					int num2 = reader.ReadInt32();
					if (num2 < 0)
					{
						throw new Exception("Invalid tile coordinates (x < 0)");
					}
					int num3 = reader.ReadInt32();
					if (num3 < 0)
					{
						throw new Exception("Invalid tile coordinates (z < 0)");
					}
					if (num2 != j || num3 != i)
					{
						this.tiles[num] = this.tiles[num3 * this.tileXCount + num2];
					}
					else
					{
						NavmeshTile[] array = this.tiles;
						int num4 = num;
						NavmeshTile navmeshTile = new NavmeshTile();
						navmeshTile.x = num2;
						navmeshTile.z = num3;
						navmeshTile.w = reader.ReadInt32();
						navmeshTile.d = reader.ReadInt32();
						navmeshTile.bbTree = ObjectPool<BBTree>.Claim();
						navmeshTile.graph = this;
						NavmeshTile navmeshTile2 = navmeshTile;
						array[num4] = navmeshTile;
						NavmeshTile navmeshTile3 = navmeshTile2;
						int num5 = reader.ReadInt32();
						if (num5 % 3 != 0)
						{
							throw new Exception("Corrupt data. Triangle indices count must be divisable by 3. Read " + num5);
						}
						navmeshTile3.tris = new int[num5];
						for (int k = 0; k < navmeshTile3.tris.Length; k++)
						{
							navmeshTile3.tris[k] = reader.ReadInt32();
						}
						navmeshTile3.verts = new Int3[reader.ReadInt32()];
						for (int l = 0; l < navmeshTile3.verts.Length; l++)
						{
							navmeshTile3.verts[l] = ctx.DeserializeInt3();
						}
						if (ctx.meta.version.Major >= 4)
						{
							navmeshTile3.vertsInGraphSpace = new Int3[reader.ReadInt32()];
							if (navmeshTile3.vertsInGraphSpace.Length != navmeshTile3.verts.Length)
							{
								throw new Exception("Corrupt data. Array lengths did not match");
							}
							for (int m = 0; m < navmeshTile3.verts.Length; m++)
							{
								navmeshTile3.vertsInGraphSpace[m] = ctx.DeserializeInt3();
							}
						}
						else
						{
							navmeshTile3.vertsInGraphSpace = new Int3[navmeshTile3.verts.Length];
							navmeshTile3.verts.CopyTo(navmeshTile3.vertsInGraphSpace, 0);
							this.transform.InverseTransform(navmeshTile3.vertsInGraphSpace);
						}
						int num6 = reader.ReadInt32();
						navmeshTile3.nodes = new TriangleMeshNode[num6];
						num <<= 12;
						for (int n = 0; n < navmeshTile3.nodes.Length; n++)
						{
							TriangleMeshNode triangleMeshNode = new TriangleMeshNode(this.active);
							navmeshTile3.nodes[n] = triangleMeshNode;
							triangleMeshNode.DeserializeNode(ctx);
							triangleMeshNode.v0 = (navmeshTile3.tris[n * 3] | num);
							triangleMeshNode.v1 = (navmeshTile3.tris[n * 3 + 1] | num);
							triangleMeshNode.v2 = (navmeshTile3.tris[n * 3 + 2] | num);
							triangleMeshNode.UpdatePositionFromVertices();
						}
						navmeshTile3.bbTree.RebuildFrom(navmeshTile3.nodes);
					}
				}
			}
		}

		// Token: 0x06002556 RID: 9558 RVA: 0x0019FAC8 File Offset: 0x0019DCC8
		protected override void PostDeserialization(GraphSerializationContext ctx)
		{
			if (ctx.meta.version < AstarSerializer.V4_1_0 && this.tiles != null)
			{
				Dictionary<TriangleMeshNode, Connection[]> conns = this.tiles.SelectMany((NavmeshTile s) => s.nodes).ToDictionary((TriangleMeshNode n) => n, (TriangleMeshNode n) => n.connections ?? new Connection[0]);
				NavmeshTile[] array = this.tiles;
				for (int i = 0; i < array.Length; i++)
				{
					NavmeshBase.CreateNodeConnections(array[i].nodes);
				}
				foreach (NavmeshTile tile in this.tiles)
				{
					this.ConnectTileWithNeighbours(tile, false);
				}
				this.GetNodes(delegate(GraphNode node)
				{
					TriangleMeshNode triNode = node as TriangleMeshNode;
					IEnumerable<Connection> source = conns[triNode];
					Func<Connection, bool> <>9__4;
					Func<Connection, bool> predicate;
					if ((predicate = <>9__4) == null)
					{
						predicate = (<>9__4 = ((Connection conn) => !triNode.ContainsConnection(conn.node)));
					}
					foreach (Connection connection in source.Where(predicate).ToList<Connection>())
					{
						triNode.AddConnection(connection.node, connection.cost, (int)connection.shapeEdge);
					}
				});
			}
			this.transform = this.CalculateTransform();
		}

		// Token: 0x0400409D RID: 16541
		public const int VertexIndexMask = 4095;

		// Token: 0x0400409E RID: 16542
		public const int TileIndexMask = 524287;

		// Token: 0x0400409F RID: 16543
		public const int TileIndexOffset = 12;

		// Token: 0x040040A0 RID: 16544
		[JsonMember]
		public Vector3 forcedBoundsSize = new Vector3(100f, 40f, 100f);

		// Token: 0x040040A1 RID: 16545
		[JsonMember]
		public bool showMeshOutline = true;

		// Token: 0x040040A2 RID: 16546
		[JsonMember]
		public bool showNodeConnections;

		// Token: 0x040040A3 RID: 16547
		[JsonMember]
		public bool showMeshSurface;

		// Token: 0x040040A4 RID: 16548
		public int tileXCount;

		// Token: 0x040040A5 RID: 16549
		public int tileZCount;

		// Token: 0x040040A6 RID: 16550
		protected NavmeshTile[] tiles;

		// Token: 0x040040A7 RID: 16551
		[JsonMember]
		public bool nearestSearchOnlyXZ;

		// Token: 0x040040A8 RID: 16552
		private bool batchTileUpdate;

		// Token: 0x040040A9 RID: 16553
		private List<int> batchUpdatedTiles = new List<int>();

		// Token: 0x040040AA RID: 16554
		private List<MeshNode> batchNodesToDestroy = new List<MeshNode>();

		// Token: 0x040040AB RID: 16555
		public GraphTransform transform = new GraphTransform(Matrix4x4.identity);

		// Token: 0x040040AC RID: 16556
		public Action<NavmeshTile[]> OnRecalculatedTiles;

		// Token: 0x040040AD RID: 16557
		private static readonly NNConstraint NNConstraintDistanceXZ = new NNConstraint
		{
			distanceXZ = true
		};

		// Token: 0x040040AE RID: 16558
		private Dictionary<int, int> nodeRecyclingHashBuffer = new Dictionary<int, int>();

		// Token: 0x040040AF RID: 16559
		private static readonly NNConstraint NNConstraintNone = NNConstraint.None;

		// Token: 0x040040B0 RID: 16560
		private static readonly byte[] LinecastShapeEdgeLookup = new byte[64];
	}
}
