using System;

namespace Pathfinding
{
	// Token: 0x02000536 RID: 1334
	public struct Connection
	{
		// Token: 0x06002345 RID: 9029 RVA: 0x00194528 File Offset: 0x00192728
		public Connection(GraphNode node, uint cost, byte shapeEdge = 255)
		{
			this.node = node;
			this.cost = cost;
			this.shapeEdge = shapeEdge;
		}

		// Token: 0x06002346 RID: 9030 RVA: 0x0019453F File Offset: 0x0019273F
		public override int GetHashCode()
		{
			return this.node.GetHashCode() ^ (int)this.cost;
		}

		// Token: 0x06002347 RID: 9031 RVA: 0x00194554 File Offset: 0x00192754
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			Connection connection = (Connection)obj;
			return connection.node == this.node && connection.cost == this.cost && connection.shapeEdge == this.shapeEdge;
		}

		// Token: 0x04003F5C RID: 16220
		public GraphNode node;

		// Token: 0x04003F5D RID: 16221
		public uint cost;

		// Token: 0x04003F5E RID: 16222
		public byte shapeEdge;
	}
}
