using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200052C RID: 1324
	public class NodeLink3Node : PointNode
	{
		// Token: 0x060022EE RID: 8942 RVA: 0x00192673 File Offset: 0x00190873
		public NodeLink3Node(AstarPath active) : base(active)
		{
		}

		// Token: 0x060022EF RID: 8943 RVA: 0x0019267C File Offset: 0x0019087C
		public override bool GetPortal(GraphNode other, List<Vector3> left, List<Vector3> right, bool backwards)
		{
			if (this.connections.Length < 2)
			{
				return false;
			}
			if (this.connections.Length != 2)
			{
				throw new Exception("Invalid NodeLink3Node. Expected 2 connections, found " + this.connections.Length);
			}
			if (left != null)
			{
				left.Add(this.portalA);
				right.Add(this.portalB);
			}
			return true;
		}

		// Token: 0x060022F0 RID: 8944 RVA: 0x001926DC File Offset: 0x001908DC
		public GraphNode GetOther(GraphNode a)
		{
			if (this.connections.Length < 2)
			{
				return null;
			}
			if (this.connections.Length != 2)
			{
				throw new Exception("Invalid NodeLink3Node. Expected 2 connections, found " + this.connections.Length);
			}
			if (a != this.connections[0].node)
			{
				return (this.connections[0].node as NodeLink3Node).GetOtherInternal(this);
			}
			return (this.connections[1].node as NodeLink3Node).GetOtherInternal(this);
		}

		// Token: 0x060022F1 RID: 8945 RVA: 0x0019276C File Offset: 0x0019096C
		private GraphNode GetOtherInternal(GraphNode a)
		{
			if (this.connections.Length < 2)
			{
				return null;
			}
			if (a != this.connections[0].node)
			{
				return this.connections[0].node;
			}
			return this.connections[1].node;
		}

		// Token: 0x04003F29 RID: 16169
		public NodeLink3 link;

		// Token: 0x04003F2A RID: 16170
		public Vector3 portalA;

		// Token: 0x04003F2B RID: 16171
		public Vector3 portalB;
	}
}
