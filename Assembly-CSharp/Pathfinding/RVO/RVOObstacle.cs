using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x020005C9 RID: 1481
	public abstract class RVOObstacle : VersionedMonoBehaviour
	{
		// Token: 0x0600289D RID: 10397
		protected abstract void CreateObstacles();

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x0600289E RID: 10398
		protected abstract bool ExecuteInEditor { get; }

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x0600289F RID: 10399
		protected abstract bool LocalCoordinates { get; }

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x060028A0 RID: 10400
		protected abstract bool StaticObstacle { get; }

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x060028A1 RID: 10401
		protected abstract float Height { get; }

		// Token: 0x060028A2 RID: 10402
		protected abstract bool AreGizmosDirty();

		// Token: 0x060028A3 RID: 10403 RVA: 0x001B8E69 File Offset: 0x001B7069
		public void OnDrawGizmos()
		{
			this.OnDrawGizmos(false);
		}

		// Token: 0x060028A4 RID: 10404 RVA: 0x001B8E72 File Offset: 0x001B7072
		public void OnDrawGizmosSelected()
		{
			this.OnDrawGizmos(true);
		}

		// Token: 0x060028A5 RID: 10405 RVA: 0x001B8E7C File Offset: 0x001B707C
		public void OnDrawGizmos(bool selected)
		{
			this.gizmoDrawing = true;
			Gizmos.color = new Color(0.615f, 1f, 0.06f, selected ? 1f : 0.7f);
			MovementPlane movementPlane = (RVOSimulator.active != null) ? RVOSimulator.active.movementPlane : MovementPlane.XZ;
			Vector3 vector = (movementPlane == MovementPlane.XZ) ? Vector3.up : (-Vector3.forward);
			if (this.gizmoVerts == null || this.AreGizmosDirty() || this._obstacleMode != this.obstacleMode)
			{
				this._obstacleMode = this.obstacleMode;
				if (this.gizmoVerts == null)
				{
					this.gizmoVerts = new List<Vector3[]>();
				}
				else
				{
					this.gizmoVerts.Clear();
				}
				this.CreateObstacles();
			}
			Matrix4x4 matrix = this.GetMatrix();
			for (int i = 0; i < this.gizmoVerts.Count; i++)
			{
				Vector3[] array = this.gizmoVerts[i];
				int j = 0;
				int num = array.Length - 1;
				while (j < array.Length)
				{
					Gizmos.DrawLine(matrix.MultiplyPoint3x4(array[j]), matrix.MultiplyPoint3x4(array[num]));
					num = j++;
				}
				if (selected)
				{
					int k = 0;
					int num2 = array.Length - 1;
					while (k < array.Length)
					{
						Vector3 vector2 = matrix.MultiplyPoint3x4(array[num2]);
						Vector3 vector3 = matrix.MultiplyPoint3x4(array[k]);
						if (movementPlane != MovementPlane.XY)
						{
							Gizmos.DrawLine(vector2 + vector * this.Height, vector3 + vector * this.Height);
							Gizmos.DrawLine(vector2, vector2 + vector * this.Height);
						}
						Vector3 vector4 = (vector2 + vector3) * 0.5f;
						Vector3 normalized = (vector3 - vector2).normalized;
						if (!(normalized == Vector3.zero))
						{
							Vector3 vector5 = Vector3.Cross(vector, normalized);
							Gizmos.DrawLine(vector4, vector4 + vector5);
							Gizmos.DrawLine(vector4 + vector5, vector4 + vector5 * 0.5f + normalized * 0.5f);
							Gizmos.DrawLine(vector4 + vector5, vector4 + vector5 * 0.5f - normalized * 0.5f);
						}
						num2 = k++;
					}
				}
			}
			this.gizmoDrawing = false;
		}

		// Token: 0x060028A6 RID: 10406 RVA: 0x001B90FF File Offset: 0x001B72FF
		protected virtual Matrix4x4 GetMatrix()
		{
			if (!this.LocalCoordinates)
			{
				return Matrix4x4.identity;
			}
			return base.transform.localToWorldMatrix;
		}

		// Token: 0x060028A7 RID: 10407 RVA: 0x001B911C File Offset: 0x001B731C
		public void OnDisable()
		{
			if (this.addedObstacles != null)
			{
				if (this.sim == null)
				{
					throw new Exception("This should not happen! Make sure you are not overriding the OnEnable function");
				}
				for (int i = 0; i < this.addedObstacles.Count; i++)
				{
					this.sim.RemoveObstacle(this.addedObstacles[i]);
				}
			}
		}

		// Token: 0x060028A8 RID: 10408 RVA: 0x001B9174 File Offset: 0x001B7374
		public void OnEnable()
		{
			if (this.addedObstacles != null)
			{
				if (this.sim == null)
				{
					throw new Exception("This should not happen! Make sure you are not overriding the OnDisable function");
				}
				for (int i = 0; i < this.addedObstacles.Count; i++)
				{
					ObstacleVertex obstacleVertex = this.addedObstacles[i];
					ObstacleVertex obstacleVertex2 = obstacleVertex;
					do
					{
						obstacleVertex.layer = this.layer;
						obstacleVertex = obstacleVertex.next;
					}
					while (obstacleVertex != obstacleVertex2);
					this.sim.AddObstacle(this.addedObstacles[i]);
				}
			}
		}

		// Token: 0x060028A9 RID: 10409 RVA: 0x001B91F0 File Offset: 0x001B73F0
		public void Start()
		{
			this.addedObstacles = new List<ObstacleVertex>();
			this.sourceObstacles = new List<Vector3[]>();
			this.prevUpdateMatrix = this.GetMatrix();
			this.CreateObstacles();
		}

		// Token: 0x060028AA RID: 10410 RVA: 0x001B921C File Offset: 0x001B741C
		public void Update()
		{
			Matrix4x4 matrix = this.GetMatrix();
			if (matrix != this.prevUpdateMatrix)
			{
				for (int i = 0; i < this.addedObstacles.Count; i++)
				{
					this.sim.UpdateObstacle(this.addedObstacles[i], this.sourceObstacles[i], matrix);
				}
				this.prevUpdateMatrix = matrix;
			}
		}

		// Token: 0x060028AB RID: 10411 RVA: 0x001B927F File Offset: 0x001B747F
		protected void FindSimulator()
		{
			if (RVOSimulator.active == null)
			{
				throw new InvalidOperationException("No RVOSimulator could be found in the scene. Please add one to any GameObject");
			}
			this.sim = RVOSimulator.active.GetSimulator();
		}

		// Token: 0x060028AC RID: 10412 RVA: 0x001B92AC File Offset: 0x001B74AC
		protected void AddObstacle(Vector3[] vertices, float height)
		{
			if (vertices == null)
			{
				throw new ArgumentNullException("Vertices Must Not Be Null");
			}
			if (height < 0f)
			{
				throw new ArgumentOutOfRangeException("Height must be non-negative");
			}
			if (vertices.Length < 2)
			{
				throw new ArgumentException("An obstacle must have at least two vertices");
			}
			if (this.sim == null)
			{
				this.FindSimulator();
			}
			if (this.gizmoDrawing)
			{
				Vector3[] array = new Vector3[vertices.Length];
				this.WindCorrectly(vertices);
				Array.Copy(vertices, array, vertices.Length);
				this.gizmoVerts.Add(array);
				return;
			}
			if (vertices.Length == 2)
			{
				this.AddObstacleInternal(vertices, height);
				return;
			}
			this.WindCorrectly(vertices);
			this.AddObstacleInternal(vertices, height);
		}

		// Token: 0x060028AD RID: 10413 RVA: 0x001B9346 File Offset: 0x001B7546
		private void AddObstacleInternal(Vector3[] vertices, float height)
		{
			this.addedObstacles.Add(this.sim.AddObstacle(vertices, height, this.GetMatrix(), this.layer, true));
			this.sourceObstacles.Add(vertices);
		}

		// Token: 0x060028AE RID: 10414 RVA: 0x001B937C File Offset: 0x001B757C
		private void WindCorrectly(Vector3[] vertices)
		{
			int num = 0;
			float num2 = float.PositiveInfinity;
			Matrix4x4 matrix = this.GetMatrix();
			for (int i = 0; i < vertices.Length; i++)
			{
				float x = matrix.MultiplyPoint3x4(vertices[i]).x;
				if (x < num2)
				{
					num = i;
					num2 = x;
				}
			}
			Vector3 vector = matrix.MultiplyPoint3x4(vertices[(num - 1 + vertices.Length) % vertices.Length]);
			Vector3 vector2 = matrix.MultiplyPoint3x4(vertices[num]);
			Vector3 vector3 = matrix.MultiplyPoint3x4(vertices[(num + 1) % vertices.Length]);
			MovementPlane movementPlane;
			if (this.sim != null)
			{
				movementPlane = this.sim.movementPlane;
			}
			else if (RVOSimulator.active)
			{
				movementPlane = RVOSimulator.active.movementPlane;
			}
			else
			{
				movementPlane = MovementPlane.XZ;
			}
			if (movementPlane == MovementPlane.XY)
			{
				vector.z = vector.y;
				vector2.z = vector2.y;
				vector3.z = vector3.y;
			}
			if (VectorMath.IsClockwiseXZ(vector, vector2, vector3) != (this.obstacleMode == RVOObstacle.ObstacleVertexWinding.KeepIn))
			{
				Array.Reverse(vertices);
			}
		}

		// Token: 0x040042B3 RID: 17075
		public RVOObstacle.ObstacleVertexWinding obstacleMode;

		// Token: 0x040042B4 RID: 17076
		public RVOLayer layer = RVOLayer.DefaultObstacle;

		// Token: 0x040042B5 RID: 17077
		protected Simulator sim;

		// Token: 0x040042B6 RID: 17078
		private List<ObstacleVertex> addedObstacles;

		// Token: 0x040042B7 RID: 17079
		private List<Vector3[]> sourceObstacles;

		// Token: 0x040042B8 RID: 17080
		private bool gizmoDrawing;

		// Token: 0x040042B9 RID: 17081
		private List<Vector3[]> gizmoVerts;

		// Token: 0x040042BA RID: 17082
		private RVOObstacle.ObstacleVertexWinding _obstacleMode;

		// Token: 0x040042BB RID: 17083
		private Matrix4x4 prevUpdateMatrix;

		// Token: 0x0200076F RID: 1903
		public enum ObstacleVertexWinding
		{
			// Token: 0x04004A5E RID: 19038
			KeepOut,
			// Token: 0x04004A5F RID: 19039
			KeepIn
		}
	}
}
