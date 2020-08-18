using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pathfinding
{
	// Token: 0x0200058A RID: 1418
	[AddComponentMenu("Pathfinding/Navmesh/Navmesh Cut")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_navmesh_cut.php")]
	public class NavmeshCut : NavmeshClipper
	{
		// Token: 0x0600268A RID: 9866 RVA: 0x001A8088 File Offset: 0x001A6288
		protected override void Awake()
		{
			base.Awake();
			this.tr = base.transform;
		}

		// Token: 0x0600268B RID: 9867 RVA: 0x001A809C File Offset: 0x001A629C
		protected override void OnEnable()
		{
			base.OnEnable();
			this.lastPosition = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
			this.lastRotation = this.tr.rotation;
		}

		// Token: 0x0600268C RID: 9868 RVA: 0x001A80CF File Offset: 0x001A62CF
		public override void ForceUpdate()
		{
			this.lastPosition = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
		}

		// Token: 0x0600268D RID: 9869 RVA: 0x001A80EC File Offset: 0x001A62EC
		public override bool RequiresUpdate()
		{
			return (this.tr.position - this.lastPosition).sqrMagnitude > this.updateDistance * this.updateDistance || (this.useRotationAndScale && Quaternion.Angle(this.lastRotation, this.tr.rotation) > this.updateRotationDistance);
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x00002ACE File Offset: 0x00000CCE
		public virtual void UsedForCut()
		{
		}

		// Token: 0x0600268F RID: 9871 RVA: 0x001A8150 File Offset: 0x001A6350
		internal override void NotifyUpdated()
		{
			this.lastPosition = this.tr.position;
			if (this.useRotationAndScale)
			{
				this.lastRotation = this.tr.rotation;
			}
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x001A817C File Offset: 0x001A637C
		private void CalculateMeshContour()
		{
			if (this.mesh == null)
			{
				return;
			}
			NavmeshCut.edges.Clear();
			NavmeshCut.pointers.Clear();
			Vector3[] vertices = this.mesh.vertices;
			int[] triangles = this.mesh.triangles;
			for (int i = 0; i < triangles.Length; i += 3)
			{
				if (VectorMath.IsClockwiseXZ(vertices[triangles[i]], vertices[triangles[i + 1]], vertices[triangles[i + 2]]))
				{
					int num = triangles[i];
					triangles[i] = triangles[i + 2];
					triangles[i + 2] = num;
				}
				NavmeshCut.edges[new Int2(triangles[i], triangles[i + 1])] = i;
				NavmeshCut.edges[new Int2(triangles[i + 1], triangles[i + 2])] = i;
				NavmeshCut.edges[new Int2(triangles[i + 2], triangles[i])] = i;
			}
			for (int j = 0; j < triangles.Length; j += 3)
			{
				for (int k = 0; k < 3; k++)
				{
					if (!NavmeshCut.edges.ContainsKey(new Int2(triangles[j + (k + 1) % 3], triangles[j + k % 3])))
					{
						NavmeshCut.pointers[triangles[j + k % 3]] = triangles[j + (k + 1) % 3];
					}
				}
			}
			List<Vector3[]> list = new List<Vector3[]>();
			List<Vector3> list2 = ListPool<Vector3>.Claim();
			for (int l = 0; l < vertices.Length; l++)
			{
				if (NavmeshCut.pointers.ContainsKey(l))
				{
					list2.Clear();
					int num2 = l;
					do
					{
						int num3 = NavmeshCut.pointers[num2];
						if (num3 == -1)
						{
							break;
						}
						NavmeshCut.pointers[num2] = -1;
						list2.Add(vertices[num2]);
						num2 = num3;
						if (num2 == -1)
						{
							goto Block_9;
						}
					}
					while (num2 != l);
					IL_1E4:
					if (list2.Count > 0)
					{
						list.Add(list2.ToArray());
						goto IL_1F9;
					}
					goto IL_1F9;
					Block_9:
					Debug.LogError("Invalid Mesh '" + this.mesh.name + " in " + base.gameObject.name);
					goto IL_1E4;
				}
				IL_1F9:;
			}
			ListPool<Vector3>.Release(ref list2);
			this.contours = list.ToArray();
		}

		// Token: 0x06002691 RID: 9873 RVA: 0x001A83A8 File Offset: 0x001A65A8
		internal override Rect GetBounds(GraphTransform inverseTranform)
		{
			List<List<Vector3>> list = ListPool<List<Vector3>>.Claim();
			this.GetContour(list);
			Rect result = default(Rect);
			for (int i = 0; i < list.Count; i++)
			{
				List<Vector3> list2 = list[i];
				for (int j = 0; j < list2.Count; j++)
				{
					Vector3 vector = inverseTranform.InverseTransform(list2[j]);
					if (j == 0)
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
			}
			ListPool<List<Vector3>>.Release(ref list);
			return result;
		}

		// Token: 0x06002692 RID: 9874 RVA: 0x001A84AC File Offset: 0x001A66AC
		public void GetContour(List<List<Vector3>> buffer)
		{
			if (this.circleResolution < 3)
			{
				this.circleResolution = 3;
			}
			switch (this.type)
			{
			case NavmeshCut.MeshType.Rectangle:
			{
				List<Vector3> list = ListPool<Vector3>.Claim();
				list.Add(new Vector3(-this.rectangleSize.x, 0f, -this.rectangleSize.y) * 0.5f);
				list.Add(new Vector3(this.rectangleSize.x, 0f, -this.rectangleSize.y) * 0.5f);
				list.Add(new Vector3(this.rectangleSize.x, 0f, this.rectangleSize.y) * 0.5f);
				list.Add(new Vector3(-this.rectangleSize.x, 0f, this.rectangleSize.y) * 0.5f);
				bool reverse = this.rectangleSize.x < 0f ^ this.rectangleSize.y < 0f;
				this.TransformBuffer(list, reverse);
				buffer.Add(list);
				return;
			}
			case NavmeshCut.MeshType.Circle:
			{
				List<Vector3> list = ListPool<Vector3>.Claim(this.circleResolution);
				for (int i = 0; i < this.circleResolution; i++)
				{
					list.Add(new Vector3(Mathf.Cos((float)(i * 2) * 3.1415927f / (float)this.circleResolution), 0f, Mathf.Sin((float)(i * 2) * 3.1415927f / (float)this.circleResolution)) * this.circleRadius);
				}
				bool reverse = this.circleRadius < 0f;
				this.TransformBuffer(list, reverse);
				buffer.Add(list);
				return;
			}
			case NavmeshCut.MeshType.CustomMesh:
				if (this.mesh != this.lastMesh || this.contours == null)
				{
					this.CalculateMeshContour();
					this.lastMesh = this.mesh;
				}
				if (this.contours != null)
				{
					bool reverse = this.meshScale < 0f;
					for (int j = 0; j < this.contours.Length; j++)
					{
						Vector3[] array = this.contours[j];
						List<Vector3> list = ListPool<Vector3>.Claim(array.Length);
						for (int k = 0; k < array.Length; k++)
						{
							list.Add(array[k] * this.meshScale);
						}
						this.TransformBuffer(list, reverse);
						buffer.Add(list);
					}
				}
				return;
			default:
				return;
			}
		}

		// Token: 0x06002693 RID: 9875 RVA: 0x001A8714 File Offset: 0x001A6914
		private void TransformBuffer(List<Vector3> buffer, bool reverse)
		{
			Vector3 vector = this.center;
			if (this.useRotationAndScale)
			{
				Matrix4x4 localToWorldMatrix = this.tr.localToWorldMatrix;
				for (int i = 0; i < buffer.Count; i++)
				{
					buffer[i] = localToWorldMatrix.MultiplyPoint3x4(buffer[i] + vector);
				}
				reverse ^= VectorMath.ReversesFaceOrientationsXZ(localToWorldMatrix);
			}
			else
			{
				vector += this.tr.position;
				for (int j = 0; j < buffer.Count; j++)
				{
					int index = j;
					buffer[index] += vector;
				}
			}
			if (reverse)
			{
				buffer.Reverse();
			}
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x001A87C0 File Offset: 0x001A69C0
		public void OnDrawGizmos()
		{
			if (this.tr == null)
			{
				this.tr = base.transform;
			}
			List<List<Vector3>> list = ListPool<List<Vector3>>.Claim();
			this.GetContour(list);
			Gizmos.color = NavmeshCut.GizmoColor;
			for (int i = 0; i < list.Count; i++)
			{
				List<Vector3> list2 = list[i];
				for (int j = 0; j < list2.Count; j++)
				{
					Vector3 from = list2[j];
					Vector3 to = list2[(j + 1) % list2.Count];
					Gizmos.DrawLine(from, to);
				}
			}
			ListPool<List<Vector3>>.Release(ref list);
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x001A884F File Offset: 0x001A6A4F
		internal float GetY(GraphTransform transform)
		{
			return transform.InverseTransform(this.useRotationAndScale ? this.tr.TransformPoint(this.center) : (this.tr.position + this.center)).y;
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x001A8890 File Offset: 0x001A6A90
		public void OnDrawGizmosSelected()
		{
			List<List<Vector3>> list = ListPool<List<Vector3>>.Claim();
			this.GetContour(list);
			Color color = Color.Lerp(NavmeshCut.GizmoColor, Color.white, 0.5f);
			color.a *= 0.5f;
			Gizmos.color = color;
			NavmeshBase navmeshBase = (AstarPath.active != null) ? (AstarPath.active.data.recastGraph ?? AstarPath.active.data.navmesh) : null;
			GraphTransform graphTransform = (navmeshBase != null) ? navmeshBase.transform : GraphTransform.identityTransform;
			float y = this.GetY(graphTransform);
			float y2 = y - this.height * 0.5f;
			float y3 = y + this.height * 0.5f;
			for (int i = 0; i < list.Count; i++)
			{
				List<Vector3> list2 = list[i];
				for (int j = 0; j < list2.Count; j++)
				{
					Vector3 vector = graphTransform.InverseTransform(list2[j]);
					Vector3 vector2 = graphTransform.InverseTransform(list2[(j + 1) % list2.Count]);
					Vector3 point = vector;
					Vector3 point2 = vector2;
					Vector3 point3 = vector;
					Vector3 point4 = vector2;
					point.y = (point2.y = y2);
					point3.y = (point4.y = y3);
					Gizmos.DrawLine(graphTransform.Transform(point), graphTransform.Transform(point2));
					Gizmos.DrawLine(graphTransform.Transform(point3), graphTransform.Transform(point4));
					Gizmos.DrawLine(graphTransform.Transform(point), graphTransform.Transform(point3));
				}
			}
			ListPool<List<Vector3>>.Release(ref list);
		}

		// Token: 0x04004168 RID: 16744
		[Tooltip("Shape of the cut")]
		public NavmeshCut.MeshType type;

		// Token: 0x04004169 RID: 16745
		[Tooltip("The contour(s) of the mesh will be extracted. This mesh should only be a 2D surface, not a volume (see documentation).")]
		public Mesh mesh;

		// Token: 0x0400416A RID: 16746
		public Vector2 rectangleSize = new Vector2(1f, 1f);

		// Token: 0x0400416B RID: 16747
		public float circleRadius = 1f;

		// Token: 0x0400416C RID: 16748
		public int circleResolution = 6;

		// Token: 0x0400416D RID: 16749
		public float height = 1f;

		// Token: 0x0400416E RID: 16750
		[Tooltip("Scale of the custom mesh")]
		public float meshScale = 1f;

		// Token: 0x0400416F RID: 16751
		public Vector3 center;

		// Token: 0x04004170 RID: 16752
		[Tooltip("Distance between positions to require an update of the navmesh\nA smaller distance gives better accuracy, but requires more updates when moving the object over time, so it is often slower.")]
		public float updateDistance = 0.4f;

		// Token: 0x04004171 RID: 16753
		[Tooltip("Only makes a split in the navmesh, but does not remove the geometry to make a hole")]
		public bool isDual;

		// Token: 0x04004172 RID: 16754
		public bool cutsAddedGeom = true;

		// Token: 0x04004173 RID: 16755
		[Tooltip("How many degrees rotation that is required for an update to the navmesh. Should be between 0 and 180.")]
		public float updateRotationDistance = 10f;

		// Token: 0x04004174 RID: 16756
		[Tooltip("Includes rotation in calculations. This is slower since a lot more matrix multiplications are needed but gives more flexibility.")]
		[FormerlySerializedAs("useRotation")]
		public bool useRotationAndScale;

		// Token: 0x04004175 RID: 16757
		private Vector3[][] contours;

		// Token: 0x04004176 RID: 16758
		protected Transform tr;

		// Token: 0x04004177 RID: 16759
		private Mesh lastMesh;

		// Token: 0x04004178 RID: 16760
		private Vector3 lastPosition;

		// Token: 0x04004179 RID: 16761
		private Quaternion lastRotation;

		// Token: 0x0400417A RID: 16762
		private static readonly Dictionary<Int2, int> edges = new Dictionary<Int2, int>();

		// Token: 0x0400417B RID: 16763
		private static readonly Dictionary<int, int> pointers = new Dictionary<int, int>();

		// Token: 0x0400417C RID: 16764
		public static readonly Color GizmoColor = new Color(0.14509805f, 0.72156864f, 0.9372549f);

		// Token: 0x02000757 RID: 1879
		public enum MeshType
		{
			// Token: 0x040049FF RID: 18943
			Rectangle,
			// Token: 0x04004A00 RID: 18944
			Circle,
			// Token: 0x04004A01 RID: 18945
			CustomMesh
		}
	}
}
