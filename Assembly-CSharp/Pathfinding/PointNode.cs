using System;
using Pathfinding.Serialization;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000570 RID: 1392
	public class PointNode : GraphNode
	{
		// Token: 0x06002582 RID: 9602 RVA: 0x0019CBA2 File Offset: 0x0019ADA2
		public void SetPosition(Int3 value)
		{
			this.position = value;
		}

		// Token: 0x06002583 RID: 9603 RVA: 0x001948E0 File Offset: 0x00192AE0
		public PointNode(AstarPath astar) : base(astar)
		{
		}

		// Token: 0x06002584 RID: 9604 RVA: 0x001A04F8 File Offset: 0x0019E6F8
		public override void GetConnections(Action<GraphNode> action)
		{
			if (this.connections == null)
			{
				return;
			}
			for (int i = 0; i < this.connections.Length; i++)
			{
				action(this.connections[i].node);
			}
		}

		// Token: 0x06002585 RID: 9605 RVA: 0x001A0538 File Offset: 0x0019E738
		public override void ClearConnections(bool alsoReverse)
		{
			if (alsoReverse && this.connections != null)
			{
				for (int i = 0; i < this.connections.Length; i++)
				{
					this.connections[i].node.RemoveConnection(this);
				}
			}
			this.connections = null;
		}

		// Token: 0x06002586 RID: 9606 RVA: 0x001A0584 File Offset: 0x0019E784
		public override void UpdateRecursiveG(Path path, PathNode pathNode, PathHandler handler)
		{
			pathNode.UpdateG(path);
			handler.heap.Add(pathNode);
			for (int i = 0; i < this.connections.Length; i++)
			{
				GraphNode node = this.connections[i].node;
				PathNode pathNode2 = handler.GetPathNode(node);
				if (pathNode2.parent == pathNode && pathNode2.pathID == handler.PathID)
				{
					node.UpdateRecursiveG(path, pathNode2, handler);
				}
			}
		}

		// Token: 0x06002587 RID: 9607 RVA: 0x001A05F4 File Offset: 0x0019E7F4
		public override bool ContainsConnection(GraphNode node)
		{
			if (this.connections == null)
			{
				return false;
			}
			for (int i = 0; i < this.connections.Length; i++)
			{
				if (this.connections[i].node == node)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x001A0638 File Offset: 0x0019E838
		public override void AddConnection(GraphNode node, uint cost)
		{
			if (node == null)
			{
				throw new ArgumentNullException();
			}
			if (this.connections != null)
			{
				for (int i = 0; i < this.connections.Length; i++)
				{
					if (this.connections[i].node == node)
					{
						this.connections[i].cost = cost;
						return;
					}
				}
			}
			int num = (this.connections != null) ? this.connections.Length : 0;
			Connection[] array = new Connection[num + 1];
			for (int j = 0; j < num; j++)
			{
				array[j] = this.connections[j];
			}
			array[num] = new Connection(node, cost, byte.MaxValue);
			this.connections = array;
		}

		// Token: 0x06002589 RID: 9609 RVA: 0x001A06E8 File Offset: 0x0019E8E8
		public override void RemoveConnection(GraphNode node)
		{
			if (this.connections == null)
			{
				return;
			}
			for (int i = 0; i < this.connections.Length; i++)
			{
				if (this.connections[i].node == node)
				{
					int num = this.connections.Length;
					Connection[] array = new Connection[num - 1];
					for (int j = 0; j < i; j++)
					{
						array[j] = this.connections[j];
					}
					for (int k = i + 1; k < num; k++)
					{
						array[k - 1] = this.connections[k];
					}
					this.connections = array;
					return;
				}
			}
		}

		// Token: 0x0600258A RID: 9610 RVA: 0x001A078C File Offset: 0x0019E98C
		public override void Open(Path path, PathNode pathNode, PathHandler handler)
		{
			if (this.connections == null)
			{
				return;
			}
			for (int i = 0; i < this.connections.Length; i++)
			{
				GraphNode node = this.connections[i].node;
				if (path.CanTraverse(node))
				{
					PathNode pathNode2 = handler.GetPathNode(node);
					if (pathNode2.pathID != handler.PathID)
					{
						pathNode2.parent = pathNode;
						pathNode2.pathID = handler.PathID;
						pathNode2.cost = this.connections[i].cost;
						pathNode2.H = path.CalculateHScore(node);
						pathNode2.UpdateG(path);
						handler.heap.Add(pathNode2);
					}
					else
					{
						uint cost = this.connections[i].cost;
						if (pathNode.G + cost + path.GetTraversalCost(node) < pathNode2.G)
						{
							pathNode2.cost = cost;
							pathNode2.parent = pathNode;
							node.UpdateRecursiveG(path, pathNode2, handler);
						}
					}
				}
			}
		}

		// Token: 0x0600258B RID: 9611 RVA: 0x001A087C File Offset: 0x0019EA7C
		public override int GetGizmoHashCode()
		{
			int num = base.GetGizmoHashCode();
			if (this.connections != null)
			{
				for (int i = 0; i < this.connections.Length; i++)
				{
					num ^= 17 * this.connections[i].GetHashCode();
				}
			}
			return num;
		}

		// Token: 0x0600258C RID: 9612 RVA: 0x001A08C9 File Offset: 0x0019EAC9
		public override void SerializeNode(GraphSerializationContext ctx)
		{
			base.SerializeNode(ctx);
			ctx.SerializeInt3(this.position);
		}

		// Token: 0x0600258D RID: 9613 RVA: 0x001A08DE File Offset: 0x0019EADE
		public override void DeserializeNode(GraphSerializationContext ctx)
		{
			base.DeserializeNode(ctx);
			this.position = ctx.DeserializeInt3();
		}

		// Token: 0x0600258E RID: 9614 RVA: 0x001A08F4 File Offset: 0x0019EAF4
		public override void SerializeReferences(GraphSerializationContext ctx)
		{
			if (this.connections == null)
			{
				ctx.writer.Write(-1);
				return;
			}
			ctx.writer.Write(this.connections.Length);
			for (int i = 0; i < this.connections.Length; i++)
			{
				ctx.SerializeNodeReference(this.connections[i].node);
				ctx.writer.Write(this.connections[i].cost);
			}
		}

		// Token: 0x0600258F RID: 9615 RVA: 0x001A0970 File Offset: 0x0019EB70
		public override void DeserializeReferences(GraphSerializationContext ctx)
		{
			int num = ctx.reader.ReadInt32();
			if (num == -1)
			{
				this.connections = null;
				return;
			}
			this.connections = new Connection[num];
			for (int i = 0; i < num; i++)
			{
				this.connections[i] = new Connection(ctx.DeserializeNodeReference(), ctx.reader.ReadUInt32(), byte.MaxValue);
			}
		}

		// Token: 0x040040BF RID: 16575
		public Connection[] connections;

		// Token: 0x040040C0 RID: 16576
		public GameObject gameObject;
	}
}
