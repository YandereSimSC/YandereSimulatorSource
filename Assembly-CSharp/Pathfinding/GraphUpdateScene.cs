using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pathfinding
{
	// Token: 0x0200051F RID: 1311
	[AddComponentMenu("Pathfinding/GraphUpdateScene")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_graph_update_scene.php")]
	public class GraphUpdateScene : GraphModifier
	{
		// Token: 0x06002245 RID: 8773 RVA: 0x0018DC70 File Offset: 0x0018BE70
		public void Start()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			if (!this.firstApplied && this.applyOnStart)
			{
				this.Apply();
			}
		}

		// Token: 0x06002246 RID: 8774 RVA: 0x0018DC90 File Offset: 0x0018BE90
		public override void OnPostScan()
		{
			if (this.applyOnScan)
			{
				this.Apply();
			}
		}

		// Token: 0x06002247 RID: 8775 RVA: 0x0018DCA0 File Offset: 0x0018BEA0
		public virtual void InvertSettings()
		{
			this.setWalkability = !this.setWalkability;
			this.penaltyDelta = -this.penaltyDelta;
			if (this.setTagInvert == 0)
			{
				this.setTagInvert = this.setTag;
				this.setTag = 0;
				return;
			}
			this.setTag = this.setTagInvert;
			this.setTagInvert = 0;
		}

		// Token: 0x06002248 RID: 8776 RVA: 0x0018DCF8 File Offset: 0x0018BEF8
		public void RecalcConvex()
		{
			this.convexPoints = (this.convex ? Polygon.ConvexHullXZ(this.points) : null);
		}

		// Token: 0x06002249 RID: 8777 RVA: 0x00002ACE File Offset: 0x00000CCE
		[Obsolete("World space can no longer be used as it does not work well with rotated graphs. Use transform.InverseTransformPoint to transform points to local space.", true)]
		private void ToggleUseWorldSpace()
		{
		}

		// Token: 0x0600224A RID: 8778 RVA: 0x00002ACE File Offset: 0x00000CCE
		[Obsolete("The Y coordinate is no longer important. Use the position of the object instead", true)]
		public void LockToY()
		{
		}

		// Token: 0x0600224B RID: 8779 RVA: 0x0018DD18 File Offset: 0x0018BF18
		public Bounds GetBounds()
		{
			if (this.points == null || this.points.Length == 0)
			{
				Collider component = base.GetComponent<Collider>();
				Collider2D component2 = base.GetComponent<Collider2D>();
				Renderer component3 = base.GetComponent<Renderer>();
				Bounds bounds;
				if (component != null)
				{
					bounds = component.bounds;
				}
				else if (component2 != null)
				{
					bounds = component2.bounds;
					bounds.size = new Vector3(bounds.size.x, bounds.size.y, Mathf.Max(bounds.size.z, 1f));
				}
				else
				{
					if (!(component3 != null))
					{
						return new Bounds(Vector3.zero, Vector3.zero);
					}
					bounds = component3.bounds;
				}
				if (this.legacyMode && bounds.size.y < this.minBoundsHeight)
				{
					bounds.size = new Vector3(bounds.size.x, this.minBoundsHeight, bounds.size.z);
				}
				return bounds;
			}
			return GraphUpdateShape.GetBounds(this.convex ? this.convexPoints : this.points, (this.legacyMode && this.legacyUseWorldSpace) ? Matrix4x4.identity : base.transform.localToWorldMatrix, this.minBoundsHeight);
		}

		// Token: 0x0600224C RID: 8780 RVA: 0x0018DE5C File Offset: 0x0018C05C
		public void Apply()
		{
			if (AstarPath.active == null)
			{
				Debug.LogError("There is no AstarPath object in the scene", this);
				return;
			}
			GraphUpdateObject graphUpdateObject;
			if (this.points == null || this.points.Length == 0)
			{
				PolygonCollider2D component = base.GetComponent<PolygonCollider2D>();
				if (component != null)
				{
					Vector2[] array = component.points;
					Vector3[] array2 = new Vector3[array.Length];
					for (int i = 0; i < array2.Length; i++)
					{
						Vector2 vector = array[i] + component.offset;
						array2[i] = new Vector3(vector.x, 0f, vector.y);
					}
					Matrix4x4 matrix = base.transform.localToWorldMatrix * Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(-90f, 0f, 0f), Vector3.one);
					GraphUpdateShape shape = new GraphUpdateShape(this.points, this.convex, matrix, this.minBoundsHeight);
					graphUpdateObject = new GraphUpdateObject(this.GetBounds());
					graphUpdateObject.shape = shape;
				}
				else
				{
					Bounds bounds = this.GetBounds();
					if (bounds.center == Vector3.zero && bounds.size == Vector3.zero)
					{
						Debug.LogError("Cannot apply GraphUpdateScene, no points defined and no renderer or collider attached", this);
						return;
					}
					graphUpdateObject = new GraphUpdateObject(bounds);
				}
			}
			else
			{
				GraphUpdateShape graphUpdateShape;
				if (this.legacyMode && !this.legacyUseWorldSpace)
				{
					Vector3[] array3 = new Vector3[this.points.Length];
					for (int j = 0; j < this.points.Length; j++)
					{
						array3[j] = base.transform.TransformPoint(this.points[j]);
					}
					graphUpdateShape = new GraphUpdateShape(array3, this.convex, Matrix4x4.identity, this.minBoundsHeight);
				}
				else
				{
					graphUpdateShape = new GraphUpdateShape(this.points, this.convex, (this.legacyMode && this.legacyUseWorldSpace) ? Matrix4x4.identity : base.transform.localToWorldMatrix, this.minBoundsHeight);
				}
				graphUpdateObject = new GraphUpdateObject(graphUpdateShape.GetBounds());
				graphUpdateObject.shape = graphUpdateShape;
			}
			this.firstApplied = true;
			graphUpdateObject.modifyWalkability = this.modifyWalkability;
			graphUpdateObject.setWalkability = this.setWalkability;
			graphUpdateObject.addPenalty = this.penaltyDelta;
			graphUpdateObject.updatePhysics = this.updatePhysics;
			graphUpdateObject.updateErosion = this.updateErosion;
			graphUpdateObject.resetPenaltyOnPhysics = this.resetPenaltyOnPhysics;
			graphUpdateObject.modifyTag = this.modifyTag;
			graphUpdateObject.setTag = this.setTag;
			AstarPath.active.UpdateGraphs(graphUpdateObject);
		}

		// Token: 0x0600224D RID: 8781 RVA: 0x0018E0E5 File Offset: 0x0018C2E5
		private void OnDrawGizmos()
		{
			this.OnDrawGizmos(false);
		}

		// Token: 0x0600224E RID: 8782 RVA: 0x0018E0EE File Offset: 0x0018C2EE
		private void OnDrawGizmosSelected()
		{
			this.OnDrawGizmos(true);
		}

		// Token: 0x0600224F RID: 8783 RVA: 0x0018E0F8 File Offset: 0x0018C2F8
		private void OnDrawGizmos(bool selected)
		{
			Color color = selected ? new Color(0.8901961f, 0.23921569f, 0.08627451f, 1f) : new Color(0.8901961f, 0.23921569f, 0.08627451f, 0.9f);
			if (selected)
			{
				Gizmos.color = Color.Lerp(color, new Color(1f, 1f, 1f, 0.2f), 0.9f);
				Bounds bounds = this.GetBounds();
				Gizmos.DrawCube(bounds.center, bounds.size);
				Gizmos.DrawWireCube(bounds.center, bounds.size);
			}
			if (this.points == null)
			{
				return;
			}
			if (this.convex)
			{
				color.a *= 0.5f;
			}
			Gizmos.color = color;
			Matrix4x4 matrix4x = (this.legacyMode && this.legacyUseWorldSpace) ? Matrix4x4.identity : base.transform.localToWorldMatrix;
			if (this.convex)
			{
				color.r -= 0.1f;
				color.g -= 0.2f;
				color.b -= 0.1f;
				Gizmos.color = color;
			}
			if (selected || !this.convex)
			{
				for (int i = 0; i < this.points.Length; i++)
				{
					Gizmos.DrawLine(matrix4x.MultiplyPoint3x4(this.points[i]), matrix4x.MultiplyPoint3x4(this.points[(i + 1) % this.points.Length]));
				}
			}
			if (this.convex)
			{
				if (this.convexPoints == null)
				{
					this.RecalcConvex();
				}
				Gizmos.color = (selected ? new Color(0.8901961f, 0.23921569f, 0.08627451f, 1f) : new Color(0.8901961f, 0.23921569f, 0.08627451f, 0.9f));
				for (int j = 0; j < this.convexPoints.Length; j++)
				{
					Gizmos.DrawLine(matrix4x.MultiplyPoint3x4(this.convexPoints[j]), matrix4x.MultiplyPoint3x4(this.convexPoints[(j + 1) % this.convexPoints.Length]));
				}
			}
			Vector3[] array = this.convex ? this.convexPoints : this.points;
			if (selected && array != null && array.Length != 0)
			{
				Gizmos.color = new Color(1f, 1f, 1f, 0.2f);
				float num = array[0].y;
				float num2 = array[0].y;
				for (int k = 0; k < array.Length; k++)
				{
					num = Mathf.Min(num, array[k].y);
					num2 = Mathf.Max(num2, array[k].y);
				}
				float num3 = Mathf.Max(this.minBoundsHeight - (num2 - num), 0f) * 0.5f;
				num -= num3;
				num2 += num3;
				for (int l = 0; l < array.Length; l++)
				{
					int num4 = (l + 1) % array.Length;
					Vector3 from = matrix4x.MultiplyPoint3x4(array[l] + Vector3.up * (num - array[l].y));
					Vector3 vector = matrix4x.MultiplyPoint3x4(array[l] + Vector3.up * (num2 - array[l].y));
					Vector3 to = matrix4x.MultiplyPoint3x4(array[num4] + Vector3.up * (num - array[num4].y));
					Vector3 to2 = matrix4x.MultiplyPoint3x4(array[num4] + Vector3.up * (num2 - array[num4].y));
					Gizmos.DrawLine(from, vector);
					Gizmos.DrawLine(from, to);
					Gizmos.DrawLine(vector, to2);
				}
			}
		}

		// Token: 0x06002250 RID: 8784 RVA: 0x0018E4E4 File Offset: 0x0018C6E4
		public void DisableLegacyMode()
		{
			if (this.legacyMode)
			{
				this.legacyMode = false;
				if (this.legacyUseWorldSpace)
				{
					this.legacyUseWorldSpace = false;
					for (int i = 0; i < this.points.Length; i++)
					{
						this.points[i] = base.transform.InverseTransformPoint(this.points[i]);
					}
					this.RecalcConvex();
				}
			}
		}

		// Token: 0x06002251 RID: 8785 RVA: 0x0018E54B File Offset: 0x0018C74B
		protected override void Awake()
		{
			if (this.serializedVersion == 0)
			{
				if (this.points != null && this.points.Length != 0)
				{
					this.legacyMode = true;
				}
				this.serializedVersion = 1;
			}
			base.Awake();
		}

		// Token: 0x04003EB2 RID: 16050
		public Vector3[] points;

		// Token: 0x04003EB3 RID: 16051
		private Vector3[] convexPoints;

		// Token: 0x04003EB4 RID: 16052
		public bool convex = true;

		// Token: 0x04003EB5 RID: 16053
		public float minBoundsHeight = 1f;

		// Token: 0x04003EB6 RID: 16054
		public int penaltyDelta;

		// Token: 0x04003EB7 RID: 16055
		public bool modifyWalkability;

		// Token: 0x04003EB8 RID: 16056
		public bool setWalkability;

		// Token: 0x04003EB9 RID: 16057
		public bool applyOnStart = true;

		// Token: 0x04003EBA RID: 16058
		public bool applyOnScan = true;

		// Token: 0x04003EBB RID: 16059
		public bool updatePhysics;

		// Token: 0x04003EBC RID: 16060
		public bool resetPenaltyOnPhysics = true;

		// Token: 0x04003EBD RID: 16061
		public bool updateErosion = true;

		// Token: 0x04003EBE RID: 16062
		public bool modifyTag;

		// Token: 0x04003EBF RID: 16063
		public int setTag;

		// Token: 0x04003EC0 RID: 16064
		[HideInInspector]
		public bool legacyMode;

		// Token: 0x04003EC1 RID: 16065
		private int setTagInvert;

		// Token: 0x04003EC2 RID: 16066
		private bool firstApplied;

		// Token: 0x04003EC3 RID: 16067
		[SerializeField]
		private int serializedVersion;

		// Token: 0x04003EC4 RID: 16068
		[SerializeField]
		[FormerlySerializedAs("useWorldSpace")]
		private bool legacyUseWorldSpace;
	}
}
