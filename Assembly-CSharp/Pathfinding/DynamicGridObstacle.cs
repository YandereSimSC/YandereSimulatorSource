using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200055B RID: 1371
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_dynamic_grid_obstacle.php")]
	public class DynamicGridObstacle : GraphModifier
	{
		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06002437 RID: 9271 RVA: 0x001968DC File Offset: 0x00194ADC
		private Bounds bounds
		{
			get
			{
				if (this.coll != null)
				{
					return this.coll.bounds;
				}
				Bounds bounds = this.coll2D.bounds;
				bounds.extents += new Vector3(0f, 0f, 10000f);
				return bounds;
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06002438 RID: 9272 RVA: 0x00196936 File Offset: 0x00194B36
		private bool colliderEnabled
		{
			get
			{
				if (!(this.coll != null))
				{
					return this.coll2D.enabled;
				}
				return this.coll.enabled;
			}
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x00196960 File Offset: 0x00194B60
		protected override void Awake()
		{
			base.Awake();
			this.coll = base.GetComponent<Collider>();
			this.coll2D = base.GetComponent<Collider2D>();
			this.tr = base.transform;
			if (this.coll == null && this.coll2D == null)
			{
				throw new Exception("A collider or 2D collider must be attached to the GameObject(" + base.gameObject.name + ") for the DynamicGridObstacle to work");
			}
			this.prevBounds = this.bounds;
			this.prevRotation = this.tr.rotation;
			this.prevEnabled = false;
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x001969F7 File Offset: 0x00194BF7
		public override void OnPostScan()
		{
			this.prevEnabled = this.colliderEnabled;
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x00196A08 File Offset: 0x00194C08
		private void Update()
		{
			if (this.coll == null && this.coll2D == null)
			{
				Debug.LogError("Removed collider from DynamicGridObstacle", this);
				base.enabled = false;
				return;
			}
			if (AstarPath.active == null || AstarPath.active.isScanning || Time.realtimeSinceStartup - this.lastCheckTime < this.checkTime || !Application.isPlaying)
			{
				return;
			}
			this.lastCheckTime = Time.realtimeSinceStartup;
			if (this.colliderEnabled)
			{
				Bounds bounds = this.bounds;
				Quaternion rotation = this.tr.rotation;
				Vector3 vector = this.prevBounds.min - bounds.min;
				Vector3 vector2 = this.prevBounds.max - bounds.max;
				float num = bounds.extents.magnitude * Quaternion.Angle(this.prevRotation, rotation) * 0.017453292f;
				if (vector.sqrMagnitude > this.updateError * this.updateError || vector2.sqrMagnitude > this.updateError * this.updateError || num > this.updateError || !this.prevEnabled)
				{
					this.DoUpdateGraphs();
					return;
				}
			}
			else if (this.prevEnabled)
			{
				this.DoUpdateGraphs();
			}
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x00196B4C File Offset: 0x00194D4C
		protected override void OnDisable()
		{
			base.OnDisable();
			if (AstarPath.active != null && Application.isPlaying)
			{
				GraphUpdateObject ob = new GraphUpdateObject(this.prevBounds);
				AstarPath.active.UpdateGraphs(ob);
				this.prevEnabled = false;
			}
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x00196B94 File Offset: 0x00194D94
		public void DoUpdateGraphs()
		{
			if (this.coll == null && this.coll2D == null)
			{
				return;
			}
			if (!this.colliderEnabled)
			{
				AstarPath.active.UpdateGraphs(this.prevBounds);
			}
			else
			{
				Bounds bounds = this.bounds;
				Bounds bounds2 = bounds;
				bounds2.Encapsulate(this.prevBounds);
				if (DynamicGridObstacle.BoundsVolume(bounds2) < DynamicGridObstacle.BoundsVolume(bounds) + DynamicGridObstacle.BoundsVolume(this.prevBounds))
				{
					AstarPath.active.UpdateGraphs(bounds2);
				}
				else
				{
					AstarPath.active.UpdateGraphs(this.prevBounds);
					AstarPath.active.UpdateGraphs(bounds);
				}
				this.prevBounds = bounds;
			}
			this.prevEnabled = this.colliderEnabled;
			this.prevRotation = this.tr.rotation;
			this.lastCheckTime = Time.realtimeSinceStartup;
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x00196C5F File Offset: 0x00194E5F
		private static float BoundsVolume(Bounds b)
		{
			return Math.Abs(b.size.x * b.size.y * b.size.z);
		}

		// Token: 0x0400401D RID: 16413
		private Collider coll;

		// Token: 0x0400401E RID: 16414
		private Collider2D coll2D;

		// Token: 0x0400401F RID: 16415
		private Transform tr;

		// Token: 0x04004020 RID: 16416
		public float updateError = 1f;

		// Token: 0x04004021 RID: 16417
		public float checkTime = 0.2f;

		// Token: 0x04004022 RID: 16418
		private Bounds prevBounds;

		// Token: 0x04004023 RID: 16419
		private Quaternion prevRotation;

		// Token: 0x04004024 RID: 16420
		private bool prevEnabled;

		// Token: 0x04004025 RID: 16421
		private float lastCheckTime = -9999f;
	}
}
