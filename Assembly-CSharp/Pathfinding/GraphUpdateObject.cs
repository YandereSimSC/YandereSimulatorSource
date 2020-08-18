using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000547 RID: 1351
	public class GraphUpdateObject
	{
		// Token: 0x060023FC RID: 9212 RVA: 0x00195F8C File Offset: 0x0019418C
		public virtual void WillUpdateNode(GraphNode node)
		{
			if (this.trackChangedNodes && node != null)
			{
				if (this.changedNodes == null)
				{
					this.changedNodes = ListPool<GraphNode>.Claim();
					this.backupData = ListPool<uint>.Claim();
					this.backupPositionData = ListPool<Int3>.Claim();
				}
				this.changedNodes.Add(node);
				this.backupPositionData.Add(node.position);
				this.backupData.Add(node.Penalty);
				this.backupData.Add(node.Flags);
				GridNode gridNode = node as GridNode;
				if (gridNode != null)
				{
					this.backupData.Add((uint)gridNode.InternalGridFlags);
				}
			}
		}

		// Token: 0x060023FD RID: 9213 RVA: 0x00196030 File Offset: 0x00194230
		public virtual void RevertFromBackup()
		{
			if (!this.trackChangedNodes)
			{
				throw new InvalidOperationException("Changed nodes have not been tracked, cannot revert from backup. Please set trackChangedNodes to true before applying the update.");
			}
			if (this.changedNodes == null)
			{
				return;
			}
			int num = 0;
			for (int i = 0; i < this.changedNodes.Count; i++)
			{
				this.changedNodes[i].Penalty = this.backupData[num];
				num++;
				this.changedNodes[i].Flags = this.backupData[num];
				num++;
				GridNode gridNode = this.changedNodes[i] as GridNode;
				if (gridNode != null)
				{
					gridNode.InternalGridFlags = (ushort)this.backupData[num];
					num++;
				}
				this.changedNodes[i].position = this.backupPositionData[i];
			}
			ListPool<GraphNode>.Release(ref this.changedNodes);
			ListPool<uint>.Release(ref this.backupData);
			ListPool<Int3>.Release(ref this.backupPositionData);
		}

		// Token: 0x060023FE RID: 9214 RVA: 0x00196128 File Offset: 0x00194328
		public virtual void Apply(GraphNode node)
		{
			if (this.shape == null || this.shape.Contains(node))
			{
				node.Penalty = (uint)((ulong)node.Penalty + (ulong)((long)this.addPenalty));
				if (this.modifyWalkability)
				{
					node.Walkable = this.setWalkability;
				}
				if (this.modifyTag)
				{
					node.Tag = (uint)this.setTag;
				}
			}
		}

		// Token: 0x060023FF RID: 9215 RVA: 0x00196189 File Offset: 0x00194389
		public GraphUpdateObject()
		{
		}

		// Token: 0x06002400 RID: 9216 RVA: 0x001961B8 File Offset: 0x001943B8
		public GraphUpdateObject(Bounds b)
		{
			this.bounds = b;
		}

		// Token: 0x04003FC4 RID: 16324
		public Bounds bounds;

		// Token: 0x04003FC5 RID: 16325
		public bool requiresFloodFill = true;

		// Token: 0x04003FC6 RID: 16326
		public bool updatePhysics = true;

		// Token: 0x04003FC7 RID: 16327
		public bool resetPenaltyOnPhysics = true;

		// Token: 0x04003FC8 RID: 16328
		public bool updateErosion = true;

		// Token: 0x04003FC9 RID: 16329
		public NNConstraint nnConstraint = NNConstraint.None;

		// Token: 0x04003FCA RID: 16330
		public int addPenalty;

		// Token: 0x04003FCB RID: 16331
		public bool modifyWalkability;

		// Token: 0x04003FCC RID: 16332
		public bool setWalkability;

		// Token: 0x04003FCD RID: 16333
		public bool modifyTag;

		// Token: 0x04003FCE RID: 16334
		public int setTag;

		// Token: 0x04003FCF RID: 16335
		public bool trackChangedNodes;

		// Token: 0x04003FD0 RID: 16336
		public List<GraphNode> changedNodes;

		// Token: 0x04003FD1 RID: 16337
		private List<uint> backupData;

		// Token: 0x04003FD2 RID: 16338
		private List<Int3> backupPositionData;

		// Token: 0x04003FD3 RID: 16339
		public GraphUpdateShape shape;
	}
}
