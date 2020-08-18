using System;
using System.Collections.Generic;
using Pathfinding.Serialization;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000538 RID: 1336
	public abstract class MeshNode : GraphNode
	{
		// Token: 0x0600236F RID: 9071 RVA: 0x001948E0 File Offset: 0x00192AE0
		protected MeshNode(AstarPath astar) : base(astar)
		{
		}

		// Token: 0x06002370 RID: 9072
		public abstract Int3 GetVertex(int i);

		// Token: 0x06002371 RID: 9073
		public abstract int GetVertexCount();

		// Token: 0x06002372 RID: 9074
		public abstract Vector3 ClosestPointOnNode(Vector3 p);

		// Token: 0x06002373 RID: 9075
		public abstract Vector3 ClosestPointOnNodeXZ(Vector3 p);

		// Token: 0x06002374 RID: 9076 RVA: 0x001948EC File Offset: 0x00192AEC
		public override void ClearConnections(bool alsoReverse)
		{
			if (alsoReverse && this.connections != null)
			{
				for (int i = 0; i < this.connections.Length; i++)
				{
					if (this.connections[i].node != null)
					{
						this.connections[i].node.RemoveConnection(this);
					}
				}
			}
			ArrayPool<Connection>.Release(ref this.connections, true);
		}

		// Token: 0x06002375 RID: 9077 RVA: 0x00194950 File Offset: 0x00192B50
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

		// Token: 0x06002376 RID: 9078 RVA: 0x00194990 File Offset: 0x00192B90
		public override void FloodFill(Stack<GraphNode> stack, uint region)
		{
			if (this.connections == null)
			{
				return;
			}
			for (int i = 0; i < this.connections.Length; i++)
			{
				GraphNode node = this.connections[i].node;
				if (node.Area != region)
				{
					node.Area = region;
					stack.Push(node);
				}
			}
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x001949E4 File Offset: 0x00192BE4
		public override bool ContainsConnection(GraphNode node)
		{
			for (int i = 0; i < this.connections.Length; i++)
			{
				if (this.connections[i].node == node)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x00194A1C File Offset: 0x00192C1C
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

		// Token: 0x06002379 RID: 9081 RVA: 0x00194A89 File Offset: 0x00192C89
		public override void AddConnection(GraphNode node, uint cost)
		{
			this.AddConnection(node, cost, -1);
		}

		// Token: 0x0600237A RID: 9082 RVA: 0x00194A94 File Offset: 0x00192C94
		public void AddConnection(GraphNode node, uint cost, int shapeEdge)
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
						this.connections[i].shapeEdge = ((shapeEdge >= 0) ? ((byte)shapeEdge) : this.connections[i].shapeEdge);
						return;
					}
				}
			}
			int num = (this.connections != null) ? this.connections.Length : 0;
			Connection[] array = ArrayPool<Connection>.ClaimWithExactLength(num + 1);
			for (int j = 0; j < num; j++)
			{
				array[j] = this.connections[j];
			}
			array[num] = new Connection(node, cost, (byte)shapeEdge);
			if (this.connections != null)
			{
				ArrayPool<Connection>.Release(ref this.connections, true);
			}
			this.connections = array;
		}

		// Token: 0x0600237B RID: 9083 RVA: 0x00194B80 File Offset: 0x00192D80
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
					Connection[] array = ArrayPool<Connection>.ClaimWithExactLength(num - 1);
					for (int j = 0; j < i; j++)
					{
						array[j] = this.connections[j];
					}
					for (int k = i + 1; k < num; k++)
					{
						array[k - 1] = this.connections[k];
					}
					if (this.connections != null)
					{
						ArrayPool<Connection>.Release(ref this.connections, true);
					}
					this.connections = array;
					return;
				}
			}
		}

		// Token: 0x0600237C RID: 9084 RVA: 0x00194C39 File Offset: 0x00192E39
		public virtual bool ContainsPoint(Int3 point)
		{
			return this.ContainsPoint((Vector3)point);
		}

		// Token: 0x0600237D RID: 9085
		public abstract bool ContainsPoint(Vector3 point);

		// Token: 0x0600237E RID: 9086
		public abstract bool ContainsPointInGraphSpace(Int3 point);

		// Token: 0x0600237F RID: 9087 RVA: 0x00194C48 File Offset: 0x00192E48
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

		// Token: 0x06002380 RID: 9088 RVA: 0x00194C98 File Offset: 0x00192E98
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
				ctx.writer.Write(this.connections[i].shapeEdge);
			}
		}

		// Token: 0x06002381 RID: 9089 RVA: 0x00194D30 File Offset: 0x00192F30
		public override void DeserializeReferences(GraphSerializationContext ctx)
		{
			int num = ctx.reader.ReadInt32();
			if (num == -1)
			{
				this.connections = null;
				return;
			}
			this.connections = ArrayPool<Connection>.ClaimWithExactLength(num);
			for (int i = 0; i < num; i++)
			{
				this.connections[i] = new Connection(ctx.DeserializeNodeReference(), ctx.reader.ReadUInt32(), (ctx.meta.version < AstarSerializer.V4_1_0) ? byte.MaxValue : ctx.reader.ReadByte());
			}
		}

		// Token: 0x04003F71 RID: 16241
		public Connection[] connections;
	}
}
