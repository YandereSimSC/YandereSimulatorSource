using System;
using UnityEngine;

namespace Pathfinding.Voxels
{
	// Token: 0x020005AB RID: 1451
	[Obsolete("Use RasterizationMesh instead")]
	public class ExtraMesh : RasterizationMesh
	{
		// Token: 0x06002777 RID: 10103 RVA: 0x001AF413 File Offset: 0x001AD613
		public ExtraMesh(Vector3[] vertices, int[] triangles, Bounds bounds) : base(vertices, triangles, bounds)
		{
		}

		// Token: 0x06002778 RID: 10104 RVA: 0x001AF41E File Offset: 0x001AD61E
		public ExtraMesh(Vector3[] vertices, int[] triangles, Bounds bounds, Matrix4x4 matrix) : base(vertices, triangles, bounds, matrix)
		{
		}
	}
}
