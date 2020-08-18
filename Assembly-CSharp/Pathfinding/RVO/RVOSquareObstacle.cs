using System;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x020005CB RID: 1483
	[AddComponentMenu("Pathfinding/Local Avoidance/Square Obstacle")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_r_v_o_1_1_r_v_o_square_obstacle.php")]
	public class RVOSquareObstacle : RVOObstacle
	{
		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x060028B8 RID: 10424 RVA: 0x0002D171 File Offset: 0x0002B371
		protected override bool StaticObstacle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x060028B9 RID: 10425 RVA: 0x0002291C File Offset: 0x00020B1C
		protected override bool ExecuteInEditor
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x060028BA RID: 10426 RVA: 0x0002291C File Offset: 0x00020B1C
		protected override bool LocalCoordinates
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x060028BB RID: 10427 RVA: 0x001B9598 File Offset: 0x001B7798
		protected override float Height
		{
			get
			{
				return this.height;
			}
		}

		// Token: 0x060028BC RID: 10428 RVA: 0x0002D171 File Offset: 0x0002B371
		protected override bool AreGizmosDirty()
		{
			return false;
		}

		// Token: 0x060028BD RID: 10429 RVA: 0x001B95A0 File Offset: 0x001B77A0
		protected override void CreateObstacles()
		{
			this.size.x = Mathf.Abs(this.size.x);
			this.size.y = Mathf.Abs(this.size.y);
			this.height = Mathf.Abs(this.height);
			Vector3[] array = new Vector3[]
			{
				new Vector3(1f, 0f, -1f),
				new Vector3(1f, 0f, 1f),
				new Vector3(-1f, 0f, 1f),
				new Vector3(-1f, 0f, -1f)
			};
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Scale(new Vector3(this.size.x * 0.5f, 0f, this.size.y * 0.5f));
				array[i] += new Vector3(this.center.x, 0f, this.center.y);
			}
			base.AddObstacle(array, this.height);
		}

		// Token: 0x040042C4 RID: 17092
		public float height = 1f;

		// Token: 0x040042C5 RID: 17093
		public Vector2 size = Vector3.one;

		// Token: 0x040042C6 RID: 17094
		public Vector2 center = Vector3.zero;
	}
}
