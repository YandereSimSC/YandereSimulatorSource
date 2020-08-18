using System;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200052A RID: 1322
	[AddComponentMenu("Pathfinding/Link")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_node_link.php")]
	public class NodeLink : GraphModifier
	{
		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x060022CC RID: 8908 RVA: 0x00191A71 File Offset: 0x0018FC71
		public Transform Start
		{
			get
			{
				return base.transform;
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x060022CD RID: 8909 RVA: 0x00191A79 File Offset: 0x0018FC79
		public Transform End
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x060022CE RID: 8910 RVA: 0x00191A81 File Offset: 0x0018FC81
		public override void OnPostScan()
		{
			if (AstarPath.active.isScanning)
			{
				this.InternalOnPostScan();
				return;
			}
			AstarPath.active.AddWorkItem(new AstarWorkItem(delegate(bool force)
			{
				this.InternalOnPostScan();
				return true;
			}));
		}

		// Token: 0x060022CF RID: 8911 RVA: 0x00191AB1 File Offset: 0x0018FCB1
		public void InternalOnPostScan()
		{
			this.Apply();
		}

		// Token: 0x060022D0 RID: 8912 RVA: 0x00191AB9 File Offset: 0x0018FCB9
		public override void OnGraphsPostUpdate()
		{
			if (!AstarPath.active.isScanning)
			{
				AstarPath.active.AddWorkItem(new AstarWorkItem(delegate(bool force)
				{
					this.InternalOnPostScan();
					return true;
				}));
			}
		}

		// Token: 0x060022D1 RID: 8913 RVA: 0x00191AE4 File Offset: 0x0018FCE4
		public virtual void Apply()
		{
			if (this.Start == null || this.End == null || AstarPath.active == null)
			{
				return;
			}
			GraphNode node = AstarPath.active.GetNearest(this.Start.position).node;
			GraphNode node2 = AstarPath.active.GetNearest(this.End.position).node;
			if (node == null || node2 == null)
			{
				return;
			}
			if (this.deleteConnection)
			{
				node.RemoveConnection(node2);
				if (!this.oneWay)
				{
					node2.RemoveConnection(node);
					return;
				}
			}
			else
			{
				uint cost = (uint)Math.Round((double)((float)(node.position - node2.position).costMagnitude * this.costFactor));
				node.AddConnection(node2, cost);
				if (!this.oneWay)
				{
					node2.AddConnection(node, cost);
				}
			}
		}

		// Token: 0x060022D2 RID: 8914 RVA: 0x00191BB8 File Offset: 0x0018FDB8
		public void OnDrawGizmos()
		{
			if (this.Start == null || this.End == null)
			{
				return;
			}
			Draw.Gizmos.Bezier(this.Start.position, this.End.position, this.deleteConnection ? Color.red : Color.green);
		}

		// Token: 0x04003F18 RID: 16152
		public Transform end;

		// Token: 0x04003F19 RID: 16153
		public float costFactor = 1f;

		// Token: 0x04003F1A RID: 16154
		public bool oneWay;

		// Token: 0x04003F1B RID: 16155
		public bool deleteConnection;
	}
}
