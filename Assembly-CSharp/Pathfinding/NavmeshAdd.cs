using System;
using Pathfinding.Util;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pathfinding
{
	// Token: 0x02000588 RID: 1416
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_navmesh_add.php")]
	public class NavmeshAdd : NavmeshClipper
	{
		// Token: 0x06002675 RID: 9845 RVA: 0x001A7A10 File Offset: 0x001A5C10
		public override bool RequiresUpdate()
		{
			return (this.tr.position - this.lastPosition).sqrMagnitude > this.updateDistance * this.updateDistance || (this.useRotationAndScale && Quaternion.Angle(this.lastRotation, this.tr.rotation) > this.updateRotationDistance);
		}

		// Token: 0x06002676 RID: 9846 RVA: 0x001A7A74 File Offset: 0x001A5C74
		public override void ForceUpdate()
		{
			this.lastPosition = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
		}

		// Token: 0x06002677 RID: 9847 RVA: 0x001A7A90 File Offset: 0x001A5C90
		protected override void Awake()
		{
			base.Awake();
			this.tr = base.transform;
		}

		// Token: 0x06002678 RID: 9848 RVA: 0x001A7AA4 File Offset: 0x001A5CA4
		internal override void NotifyUpdated()
		{
			this.lastPosition = this.tr.position;
			if (this.useRotationAndScale)
			{
				this.lastRotation = this.tr.rotation;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06002679 RID: 9849 RVA: 0x001A7AD0 File Offset: 0x001A5CD0
		public Vector3 Center
		{
			get
			{
				return this.tr.position + (this.useRotationAndScale ? this.tr.TransformPoint(this.center) : this.center);
			}
		}

		// Token: 0x0600267A RID: 9850 RVA: 0x001A7B04 File Offset: 0x001A5D04
		[ContextMenu("Rebuild Mesh")]
		public void RebuildMesh()
		{
			if (this.type != NavmeshAdd.MeshType.CustomMesh)
			{
				if (this.verts == null || this.verts.Length != 4 || this.tris == null || this.tris.Length != 6)
				{
					this.verts = new Vector3[4];
					this.tris = new int[6];
				}
				this.tris[0] = 0;
				this.tris[1] = 1;
				this.tris[2] = 2;
				this.tris[3] = 0;
				this.tris[4] = 2;
				this.tris[5] = 3;
				this.verts[0] = new Vector3(-this.rectangleSize.x * 0.5f, 0f, -this.rectangleSize.y * 0.5f);
				this.verts[1] = new Vector3(this.rectangleSize.x * 0.5f, 0f, -this.rectangleSize.y * 0.5f);
				this.verts[2] = new Vector3(this.rectangleSize.x * 0.5f, 0f, this.rectangleSize.y * 0.5f);
				this.verts[3] = new Vector3(-this.rectangleSize.x * 0.5f, 0f, this.rectangleSize.y * 0.5f);
				return;
			}
			if (this.mesh == null)
			{
				this.verts = null;
				this.tris = null;
				return;
			}
			this.verts = this.mesh.vertices;
			this.tris = this.mesh.triangles;
		}

		// Token: 0x0600267B RID: 9851 RVA: 0x001A7CB4 File Offset: 0x001A5EB4
		internal override Rect GetBounds(GraphTransform inverseTransform)
		{
			if (this.verts == null)
			{
				this.RebuildMesh();
			}
			Int3[] array = ArrayPool<Int3>.Claim((this.verts != null) ? this.verts.Length : 0);
			int[] array2;
			this.GetMesh(ref array, out array2, inverseTransform);
			Rect result = default(Rect);
			for (int i = 0; i < array2.Length; i++)
			{
				Vector3 vector = (Vector3)array[array2[i]];
				if (i == 0)
				{
					result = new Rect(vector.x, vector.z, 0f, 0f);
				}
				else
				{
					result.xMax = Math.Max(result.xMax, vector.x);
					result.yMax = Math.Max(result.yMax, vector.z);
					result.xMin = Math.Min(result.xMin, vector.x);
					result.yMin = Math.Min(result.yMin, vector.z);
				}
			}
			ArrayPool<Int3>.Release(ref array, false);
			return result;
		}

		// Token: 0x0600267C RID: 9852 RVA: 0x001A7DB4 File Offset: 0x001A5FB4
		public void GetMesh(ref Int3[] vbuffer, out int[] tbuffer, GraphTransform inverseTransform = null)
		{
			if (this.verts == null)
			{
				this.RebuildMesh();
			}
			if (this.verts == null)
			{
				tbuffer = ArrayPool<int>.Claim(0);
				return;
			}
			if (vbuffer == null || vbuffer.Length < this.verts.Length)
			{
				if (vbuffer != null)
				{
					ArrayPool<Int3>.Release(ref vbuffer, false);
				}
				vbuffer = ArrayPool<Int3>.Claim(this.verts.Length);
			}
			tbuffer = this.tris;
			if (this.useRotationAndScale)
			{
				Matrix4x4 matrix4x = Matrix4x4.TRS(this.tr.position + this.center, this.tr.rotation, this.tr.localScale * this.meshScale);
				for (int i = 0; i < this.verts.Length; i++)
				{
					Vector3 vector = matrix4x.MultiplyPoint3x4(this.verts[i]);
					if (inverseTransform != null)
					{
						vector = inverseTransform.InverseTransform(vector);
					}
					vbuffer[i] = (Int3)vector;
				}
				return;
			}
			Vector3 a = this.tr.position + this.center;
			for (int j = 0; j < this.verts.Length; j++)
			{
				Vector3 vector2 = a + this.verts[j] * this.meshScale;
				if (inverseTransform != null)
				{
					vector2 = inverseTransform.InverseTransform(vector2);
				}
				vbuffer[j] = (Int3)vector2;
			}
		}

		// Token: 0x04004156 RID: 16726
		public NavmeshAdd.MeshType type;

		// Token: 0x04004157 RID: 16727
		public Mesh mesh;

		// Token: 0x04004158 RID: 16728
		private Vector3[] verts;

		// Token: 0x04004159 RID: 16729
		private int[] tris;

		// Token: 0x0400415A RID: 16730
		public Vector2 rectangleSize = new Vector2(1f, 1f);

		// Token: 0x0400415B RID: 16731
		public float meshScale = 1f;

		// Token: 0x0400415C RID: 16732
		public Vector3 center;

		// Token: 0x0400415D RID: 16733
		[FormerlySerializedAs("useRotation")]
		public bool useRotationAndScale;

		// Token: 0x0400415E RID: 16734
		[Tooltip("Distance between positions to require an update of the navmesh\nA smaller distance gives better accuracy, but requires more updates when moving the object over time, so it is often slower.")]
		public float updateDistance = 0.4f;

		// Token: 0x0400415F RID: 16735
		[Tooltip("How many degrees rotation that is required for an update to the navmesh. Should be between 0 and 180.")]
		public float updateRotationDistance = 10f;

		// Token: 0x04004160 RID: 16736
		protected Transform tr;

		// Token: 0x04004161 RID: 16737
		private Vector3 lastPosition;

		// Token: 0x04004162 RID: 16738
		private Quaternion lastRotation;

		// Token: 0x04004163 RID: 16739
		public static readonly Color GizmoColor = new Color(0.36862746f, 0.9372549f, 0.14509805f);

		// Token: 0x02000756 RID: 1878
		public enum MeshType
		{
			// Token: 0x040049FC RID: 18940
			Rectangle,
			// Token: 0x040049FD RID: 18941
			CustomMesh
		}
	}
}
