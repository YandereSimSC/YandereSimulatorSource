using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200058B RID: 1419
	[AddComponentMenu("Pathfinding/Navmesh/RelevantGraphSurface")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_relevant_graph_surface.php")]
	public class RelevantGraphSurface : VersionedMonoBehaviour
	{
		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06002699 RID: 9881 RVA: 0x001A8AC4 File Offset: 0x001A6CC4
		public Vector3 Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x0600269A RID: 9882 RVA: 0x001A8ACC File Offset: 0x001A6CCC
		public RelevantGraphSurface Next
		{
			get
			{
				return this.next;
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x0600269B RID: 9883 RVA: 0x001A8AD4 File Offset: 0x001A6CD4
		public RelevantGraphSurface Prev
		{
			get
			{
				return this.prev;
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x0600269C RID: 9884 RVA: 0x001A8ADC File Offset: 0x001A6CDC
		public static RelevantGraphSurface Root
		{
			get
			{
				return RelevantGraphSurface.root;
			}
		}

		// Token: 0x0600269D RID: 9885 RVA: 0x001A8AE3 File Offset: 0x001A6CE3
		public void UpdatePosition()
		{
			this.position = base.transform.position;
		}

		// Token: 0x0600269E RID: 9886 RVA: 0x001A8AF6 File Offset: 0x001A6CF6
		private void OnEnable()
		{
			this.UpdatePosition();
			if (RelevantGraphSurface.root == null)
			{
				RelevantGraphSurface.root = this;
				return;
			}
			this.next = RelevantGraphSurface.root;
			RelevantGraphSurface.root.prev = this;
			RelevantGraphSurface.root = this;
		}

		// Token: 0x0600269F RID: 9887 RVA: 0x001A8B30 File Offset: 0x001A6D30
		private void OnDisable()
		{
			if (RelevantGraphSurface.root == this)
			{
				RelevantGraphSurface.root = this.next;
				if (RelevantGraphSurface.root != null)
				{
					RelevantGraphSurface.root.prev = null;
				}
			}
			else
			{
				if (this.prev != null)
				{
					this.prev.next = this.next;
				}
				if (this.next != null)
				{
					this.next.prev = this.prev;
				}
			}
			this.prev = null;
			this.next = null;
		}

		// Token: 0x060026A0 RID: 9888 RVA: 0x001A8BBC File Offset: 0x001A6DBC
		public static void UpdateAllPositions()
		{
			RelevantGraphSurface relevantGraphSurface = RelevantGraphSurface.root;
			while (relevantGraphSurface != null)
			{
				relevantGraphSurface.UpdatePosition();
				relevantGraphSurface = relevantGraphSurface.Next;
			}
		}

		// Token: 0x060026A1 RID: 9889 RVA: 0x001A8BE8 File Offset: 0x001A6DE8
		public static void FindAllGraphSurfaces()
		{
			RelevantGraphSurface[] array = UnityEngine.Object.FindObjectsOfType(typeof(RelevantGraphSurface)) as RelevantGraphSurface[];
			for (int i = 0; i < array.Length; i++)
			{
				array[i].OnDisable();
				array[i].OnEnable();
			}
		}

		// Token: 0x060026A2 RID: 9890 RVA: 0x001A8C28 File Offset: 0x001A6E28
		public void OnDrawGizmos()
		{
			Gizmos.color = new Color(0.22352941f, 0.827451f, 0.18039216f, 0.4f);
			Gizmos.DrawLine(base.transform.position - Vector3.up * this.maxRange, base.transform.position + Vector3.up * this.maxRange);
		}

		// Token: 0x060026A3 RID: 9891 RVA: 0x001A8C98 File Offset: 0x001A6E98
		public void OnDrawGizmosSelected()
		{
			Gizmos.color = new Color(0.22352941f, 0.827451f, 0.18039216f);
			Gizmos.DrawLine(base.transform.position - Vector3.up * this.maxRange, base.transform.position + Vector3.up * this.maxRange);
		}

		// Token: 0x0400417D RID: 16765
		private static RelevantGraphSurface root;

		// Token: 0x0400417E RID: 16766
		public float maxRange = 1f;

		// Token: 0x0400417F RID: 16767
		private RelevantGraphSurface prev;

		// Token: 0x04004180 RID: 16768
		private RelevantGraphSurface next;

		// Token: 0x04004181 RID: 16769
		private Vector3 position;
	}
}
