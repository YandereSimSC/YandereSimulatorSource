using System;
using System.Collections.Generic;
using Pathfinding.Serialization;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200056C RID: 1388
	[JsonOptIn]
	public class NavMeshGraph : NavmeshBase, IUpdatableGraph
	{
		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06002511 RID: 9489 RVA: 0x0019D1AA File Offset: 0x0019B3AA
		protected override bool RecalculateNormals
		{
			get
			{
				return this.recalculateNormals;
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06002512 RID: 9490 RVA: 0x0019D1B2 File Offset: 0x0019B3B2
		public override float TileWorldSizeX
		{
			get
			{
				return this.forcedBoundsSize.x;
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06002513 RID: 9491 RVA: 0x0019D1BF File Offset: 0x0019B3BF
		public override float TileWorldSizeZ
		{
			get
			{
				return this.forcedBoundsSize.z;
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06002514 RID: 9492 RVA: 0x00194851 File Offset: 0x00192A51
		protected override float MaxTileConnectionEdgeDistance
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x0019D1CC File Offset: 0x0019B3CC
		public override GraphTransform CalculateTransform()
		{
			return new GraphTransform(Matrix4x4.TRS(this.offset, Quaternion.Euler(this.rotation), Vector3.one) * Matrix4x4.TRS((this.sourceMesh != null) ? (this.sourceMesh.bounds.min * this.scale) : Vector3.zero, Quaternion.identity, Vector3.one));
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x0002D171 File Offset: 0x0002B371
		GraphUpdateThreading IUpdatableGraph.CanUpdateAsync(GraphUpdateObject o)
		{
			return GraphUpdateThreading.UnityThread;
		}

		// Token: 0x06002517 RID: 9495 RVA: 0x00002ACE File Offset: 0x00000CCE
		void IUpdatableGraph.UpdateAreaInit(GraphUpdateObject o)
		{
		}

		// Token: 0x06002518 RID: 9496 RVA: 0x00002ACE File Offset: 0x00000CCE
		void IUpdatableGraph.UpdateAreaPost(GraphUpdateObject o)
		{
		}

		// Token: 0x06002519 RID: 9497 RVA: 0x0019D240 File Offset: 0x0019B440
		void IUpdatableGraph.UpdateArea(GraphUpdateObject o)
		{
			NavMeshGraph.UpdateArea(o, this);
		}

		// Token: 0x0600251A RID: 9498 RVA: 0x0019D24C File Offset: 0x0019B44C
		public static void UpdateArea(GraphUpdateObject o, INavmeshHolder graph)
		{
			Bounds bounds = graph.transform.InverseTransform(o.bounds);
			IntRect irect = new IntRect(Mathf.FloorToInt(bounds.min.x * 1000f), Mathf.FloorToInt(bounds.min.z * 1000f), Mathf.CeilToInt(bounds.max.x * 1000f), Mathf.CeilToInt(bounds.max.z * 1000f));
			Int3 a = new Int3(irect.xmin, 0, irect.ymin);
			Int3 b = new Int3(irect.xmin, 0, irect.ymax);
			Int3 c = new Int3(irect.xmax, 0, irect.ymin);
			Int3 d = new Int3(irect.xmax, 0, irect.ymax);
			int ymin = ((Int3)bounds.min).y;
			int ymax = ((Int3)bounds.max).y;
			graph.GetNodes(delegate(GraphNode _node)
			{
				TriangleMeshNode triangleMeshNode = _node as TriangleMeshNode;
				bool flag = false;
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				for (int i = 0; i < 3; i++)
				{
					Int3 vertexInGraphSpace = triangleMeshNode.GetVertexInGraphSpace(i);
					if (irect.Contains(vertexInGraphSpace.x, vertexInGraphSpace.z))
					{
						flag = true;
						break;
					}
					if (vertexInGraphSpace.x < irect.xmin)
					{
						num++;
					}
					if (vertexInGraphSpace.x > irect.xmax)
					{
						num2++;
					}
					if (vertexInGraphSpace.z < irect.ymin)
					{
						num3++;
					}
					if (vertexInGraphSpace.z > irect.ymax)
					{
						num4++;
					}
				}
				if (!flag && (num == 3 || num2 == 3 || num3 == 3 || num4 == 3))
				{
					return;
				}
				for (int j = 0; j < 3; j++)
				{
					int i2 = (j > 1) ? 0 : (j + 1);
					Int3 vertexInGraphSpace2 = triangleMeshNode.GetVertexInGraphSpace(j);
					Int3 vertexInGraphSpace3 = triangleMeshNode.GetVertexInGraphSpace(i2);
					if (VectorMath.SegmentsIntersectXZ(a, b, vertexInGraphSpace2, vertexInGraphSpace3))
					{
						flag = true;
						break;
					}
					if (VectorMath.SegmentsIntersectXZ(a, c, vertexInGraphSpace2, vertexInGraphSpace3))
					{
						flag = true;
						break;
					}
					if (VectorMath.SegmentsIntersectXZ(c, d, vertexInGraphSpace2, vertexInGraphSpace3))
					{
						flag = true;
						break;
					}
					if (VectorMath.SegmentsIntersectXZ(d, b, vertexInGraphSpace2, vertexInGraphSpace3))
					{
						flag = true;
						break;
					}
				}
				if (flag || triangleMeshNode.ContainsPointInGraphSpace(a) || triangleMeshNode.ContainsPointInGraphSpace(b) || triangleMeshNode.ContainsPointInGraphSpace(c) || triangleMeshNode.ContainsPointInGraphSpace(d))
				{
					flag = true;
				}
				if (!flag)
				{
					return;
				}
				int num5 = 0;
				int num6 = 0;
				for (int k = 0; k < 3; k++)
				{
					Int3 vertexInGraphSpace4 = triangleMeshNode.GetVertexInGraphSpace(k);
					if (vertexInGraphSpace4.y < ymin)
					{
						num6++;
					}
					if (vertexInGraphSpace4.y > ymax)
					{
						num5++;
					}
				}
				if (num6 == 3 || num5 == 3)
				{
					return;
				}
				o.WillUpdateNode(triangleMeshNode);
				o.Apply(triangleMeshNode);
			});
		}

		// Token: 0x0600251B RID: 9499 RVA: 0x0019D3AC File Offset: 0x0019B5AC
		[Obsolete("Set the mesh to ObjImporter.ImportFile(...) and scan the graph the normal way instead")]
		public void ScanInternal(string objMeshPath)
		{
			Mesh x = ObjImporter.ImportFile(objMeshPath);
			if (x == null)
			{
				Debug.LogError("Couldn't read .obj file at '" + objMeshPath + "'");
				return;
			}
			this.sourceMesh = x;
			IEnumerator<Progress> enumerator = this.ScanInternal().GetEnumerator();
			while (enumerator.MoveNext())
			{
			}
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x0019D3FA File Offset: 0x0019B5FA
		protected override IEnumerable<Progress> ScanInternal()
		{
			this.transform = this.CalculateTransform();
			this.tileZCount = (this.tileXCount = 1);
			this.tiles = new NavmeshTile[this.tileZCount * this.tileXCount];
			TriangleMeshNode.SetNavmeshHolder(AstarPath.active.data.GetGraphIndex(this), this);
			if (this.sourceMesh == null)
			{
				base.FillWithEmptyTiles();
				yield break;
			}
			yield return new Progress(0f, "Transforming Vertices");
			this.forcedBoundsSize = this.sourceMesh.bounds.size * this.scale;
			Vector3[] vertices = this.sourceMesh.vertices;
			List<Int3> intVertices = ListPool<Int3>.Claim(vertices.Length);
			Matrix4x4 matrix4x = Matrix4x4.TRS(-this.sourceMesh.bounds.min * this.scale, Quaternion.identity, Vector3.one * this.scale);
			for (int i = 0; i < vertices.Length; i++)
			{
				intVertices.Add((Int3)matrix4x.MultiplyPoint3x4(vertices[i]));
			}
			yield return new Progress(0.1f, "Compressing Vertices");
			Int3[] compressedVertices = null;
			int[] compressedTriangles = null;
			Polygon.CompressMesh(intVertices, new List<int>(this.sourceMesh.triangles), out compressedVertices, out compressedTriangles);
			ListPool<Int3>.Release(ref intVertices);
			yield return new Progress(0.2f, "Building Nodes");
			base.ReplaceTile(0, 0, compressedVertices, compressedTriangles);
			if (this.OnRecalculatedTiles != null)
			{
				this.OnRecalculatedTiles(this.tiles.Clone() as NavmeshTile[]);
			}
			yield break;
		}

		// Token: 0x0600251D RID: 9501 RVA: 0x0019D40C File Offset: 0x0019B60C
		protected override void DeserializeSettingsCompatibility(GraphSerializationContext ctx)
		{
			base.DeserializeSettingsCompatibility(ctx);
			this.sourceMesh = (ctx.DeserializeUnityObject() as Mesh);
			this.offset = ctx.DeserializeVector3();
			this.rotation = ctx.DeserializeVector3();
			this.scale = ctx.reader.ReadSingle();
			this.nearestSearchOnlyXZ = !ctx.reader.ReadBoolean();
		}

		// Token: 0x04004098 RID: 16536
		[JsonMember]
		public Mesh sourceMesh;

		// Token: 0x04004099 RID: 16537
		[JsonMember]
		public Vector3 offset;

		// Token: 0x0400409A RID: 16538
		[JsonMember]
		public Vector3 rotation;

		// Token: 0x0400409B RID: 16539
		[JsonMember]
		public float scale = 1f;

		// Token: 0x0400409C RID: 16540
		[JsonMember]
		public bool recalculateNormals = true;
	}
}
