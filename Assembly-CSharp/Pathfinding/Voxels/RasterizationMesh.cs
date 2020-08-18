using System;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding.Voxels
{
	// Token: 0x020005AC RID: 1452
	public class RasterizationMesh
	{
		// Token: 0x06002779 RID: 10105 RVA: 0x000045DB File Offset: 0x000027DB
		public RasterizationMesh()
		{
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x001AF42C File Offset: 0x001AD62C
		public RasterizationMesh(Vector3[] vertices, int[] triangles, Bounds bounds)
		{
			this.matrix = Matrix4x4.identity;
			this.vertices = vertices;
			this.numVertices = vertices.Length;
			this.triangles = triangles;
			this.numTriangles = triangles.Length;
			this.bounds = bounds;
			this.original = null;
			this.area = 0;
		}

		// Token: 0x0600277B RID: 10107 RVA: 0x001AF480 File Offset: 0x001AD680
		public RasterizationMesh(Vector3[] vertices, int[] triangles, Bounds bounds, Matrix4x4 matrix)
		{
			this.matrix = matrix;
			this.vertices = vertices;
			this.numVertices = vertices.Length;
			this.triangles = triangles;
			this.numTriangles = triangles.Length;
			this.bounds = bounds;
			this.original = null;
			this.area = 0;
		}

		// Token: 0x0600277C RID: 10108 RVA: 0x001AF4D0 File Offset: 0x001AD6D0
		public void RecalculateBounds()
		{
			Bounds bounds = new Bounds(this.matrix.MultiplyPoint3x4(this.vertices[0]), Vector3.zero);
			for (int i = 1; i < this.numVertices; i++)
			{
				bounds.Encapsulate(this.matrix.MultiplyPoint3x4(this.vertices[i]));
			}
			this.bounds = bounds;
		}

		// Token: 0x0600277D RID: 10109 RVA: 0x001AF536 File Offset: 0x001AD736
		public void Pool()
		{
			if (this.pool)
			{
				ArrayPool<int>.Release(ref this.triangles, false);
				ArrayPool<Vector3>.Release(ref this.vertices, false);
			}
		}

		// Token: 0x040041F5 RID: 16885
		public MeshFilter original;

		// Token: 0x040041F6 RID: 16886
		public int area;

		// Token: 0x040041F7 RID: 16887
		public Vector3[] vertices;

		// Token: 0x040041F8 RID: 16888
		public int[] triangles;

		// Token: 0x040041F9 RID: 16889
		public int numVertices;

		// Token: 0x040041FA RID: 16890
		public int numTriangles;

		// Token: 0x040041FB RID: 16891
		public Bounds bounds;

		// Token: 0x040041FC RID: 16892
		public Matrix4x4 matrix;

		// Token: 0x040041FD RID: 16893
		public bool pool;
	}
}
