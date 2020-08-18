using System;
using System.Collections.Generic;
using Pathfinding.Serialization;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000537 RID: 1335
	public abstract class GraphNode
	{
		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06002348 RID: 9032 RVA: 0x00194599 File Offset: 0x00192799
		public NavGraph Graph
		{
			get
			{
				if (!this.Destroyed)
				{
					return AstarData.GetGraph(this);
				}
				return null;
			}
		}

		// Token: 0x06002349 RID: 9033 RVA: 0x001945AB File Offset: 0x001927AB
		protected GraphNode(AstarPath astar)
		{
			if (astar != null)
			{
				this.nodeIndex = astar.GetNewNodeIndex();
				astar.InitializeNode(this);
				return;
			}
			throw new Exception("No active AstarPath object to bind to");
		}

		// Token: 0x0600234A RID: 9034 RVA: 0x001945D4 File Offset: 0x001927D4
		internal void Destroy()
		{
			if (this.Destroyed)
			{
				return;
			}
			this.ClearConnections(true);
			if (AstarPath.active != null)
			{
				AstarPath.active.DestroyNode(this);
			}
			this.NodeIndex = 268435454;
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x0600234B RID: 9035 RVA: 0x00194609 File Offset: 0x00192809
		public bool Destroyed
		{
			get
			{
				return this.NodeIndex == 268435454;
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x0600234C RID: 9036 RVA: 0x00194618 File Offset: 0x00192818
		// (set) Token: 0x0600234D RID: 9037 RVA: 0x00194626 File Offset: 0x00192826
		public int NodeIndex
		{
			get
			{
				return this.nodeIndex & 268435455;
			}
			private set
			{
				this.nodeIndex = ((this.nodeIndex & -268435456) | value);
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x0600234E RID: 9038 RVA: 0x0019463C File Offset: 0x0019283C
		// (set) Token: 0x0600234F RID: 9039 RVA: 0x0019464D File Offset: 0x0019284D
		internal bool TemporaryFlag1
		{
			get
			{
				return (this.nodeIndex & 268435456) != 0;
			}
			set
			{
				this.nodeIndex = ((this.nodeIndex & -268435457) | (value ? 268435456 : 0));
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06002350 RID: 9040 RVA: 0x0019466D File Offset: 0x0019286D
		// (set) Token: 0x06002351 RID: 9041 RVA: 0x0019467E File Offset: 0x0019287E
		internal bool TemporaryFlag2
		{
			get
			{
				return (this.nodeIndex & 536870912) != 0;
			}
			set
			{
				this.nodeIndex = ((this.nodeIndex & -536870913) | (value ? 536870912 : 0));
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06002352 RID: 9042 RVA: 0x0019469E File Offset: 0x0019289E
		// (set) Token: 0x06002353 RID: 9043 RVA: 0x001946A6 File Offset: 0x001928A6
		public uint Flags
		{
			get
			{
				return this.flags;
			}
			set
			{
				this.flags = value;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06002354 RID: 9044 RVA: 0x001946AF File Offset: 0x001928AF
		// (set) Token: 0x06002355 RID: 9045 RVA: 0x001946B7 File Offset: 0x001928B7
		public uint Penalty
		{
			get
			{
				return this.penalty;
			}
			set
			{
				if (value > 16777215u)
				{
					Debug.LogWarning("Very high penalty applied. Are you sure negative values haven't underflowed?\nPenalty values this high could with long paths cause overflows and in some cases infinity loops because of that.\nPenalty value applied: " + value);
				}
				this.penalty = value;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06002356 RID: 9046 RVA: 0x001946DD File Offset: 0x001928DD
		// (set) Token: 0x06002357 RID: 9047 RVA: 0x001946EA File Offset: 0x001928EA
		public bool Walkable
		{
			get
			{
				return (this.flags & 1u) > 0u;
			}
			set
			{
				this.flags = ((this.flags & 4294967294u) | (value ? 1u : 0u));
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06002358 RID: 9048 RVA: 0x00194703 File Offset: 0x00192903
		// (set) Token: 0x06002359 RID: 9049 RVA: 0x00194713 File Offset: 0x00192913
		public uint Area
		{
			get
			{
				return (this.flags & 262142u) >> 1;
			}
			set
			{
				this.flags = ((this.flags & 4294705153u) | value << 1);
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x0600235A RID: 9050 RVA: 0x0019472B File Offset: 0x0019292B
		// (set) Token: 0x0600235B RID: 9051 RVA: 0x0019473C File Offset: 0x0019293C
		public uint GraphIndex
		{
			get
			{
				return (this.flags & 4278190080u) >> 24;
			}
			set
			{
				this.flags = ((this.flags & 16777215u) | value << 24);
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x0600235C RID: 9052 RVA: 0x00194755 File Offset: 0x00192955
		// (set) Token: 0x0600235D RID: 9053 RVA: 0x00194766 File Offset: 0x00192966
		public uint Tag
		{
			get
			{
				return (this.flags & 16252928u) >> 19;
			}
			set
			{
				this.flags = ((this.flags & 4278714367u) | value << 19);
			}
		}

		// Token: 0x0600235E RID: 9054 RVA: 0x00194780 File Offset: 0x00192980
		public virtual void UpdateRecursiveG(Path path, PathNode pathNode, PathHandler handler)
		{
			pathNode.UpdateG(path);
			handler.heap.Add(pathNode);
			this.GetConnections(delegate(GraphNode other)
			{
				PathNode pathNode2 = handler.GetPathNode(other);
				if (pathNode2.parent == pathNode && pathNode2.pathID == handler.PathID)
				{
					other.UpdateRecursiveG(path, pathNode2, handler);
				}
			});
		}

		// Token: 0x0600235F RID: 9055 RVA: 0x001947E4 File Offset: 0x001929E4
		public virtual void FloodFill(Stack<GraphNode> stack, uint region)
		{
			this.GetConnections(delegate(GraphNode other)
			{
				if (other.Area != region)
				{
					other.Area = region;
					stack.Push(other);
				}
			});
		}

		// Token: 0x06002360 RID: 9056
		public abstract void GetConnections(Action<GraphNode> action);

		// Token: 0x06002361 RID: 9057
		public abstract void AddConnection(GraphNode node, uint cost);

		// Token: 0x06002362 RID: 9058
		public abstract void RemoveConnection(GraphNode node);

		// Token: 0x06002363 RID: 9059
		public abstract void ClearConnections(bool alsoReverse);

		// Token: 0x06002364 RID: 9060 RVA: 0x00194818 File Offset: 0x00192A18
		public virtual bool ContainsConnection(GraphNode node)
		{
			bool contains = false;
			this.GetConnections(delegate(GraphNode neighbour)
			{
				contains |= (neighbour == node);
			});
			return contains;
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x00002ACE File Offset: 0x00000CCE
		public virtual void RecalculateConnectionCosts()
		{
		}

		// Token: 0x06002366 RID: 9062 RVA: 0x0002D171 File Offset: 0x0002B371
		public virtual bool GetPortal(GraphNode other, List<Vector3> left, List<Vector3> right, bool backwards)
		{
			return false;
		}

		// Token: 0x06002367 RID: 9063
		public abstract void Open(Path path, PathNode pathNode, PathHandler handler);

		// Token: 0x06002368 RID: 9064 RVA: 0x00194851 File Offset: 0x00192A51
		public virtual float SurfaceArea()
		{
			return 0f;
		}

		// Token: 0x06002369 RID: 9065 RVA: 0x00194858 File Offset: 0x00192A58
		public virtual Vector3 RandomPointOnSurface()
		{
			return (Vector3)this.position;
		}

		// Token: 0x0600236A RID: 9066 RVA: 0x00194865 File Offset: 0x00192A65
		public virtual int GetGizmoHashCode()
		{
			return this.position.GetHashCode() ^ (int)(19u * this.Penalty) ^ (int)(41u * this.flags);
		}

		// Token: 0x0600236B RID: 9067 RVA: 0x0019488C File Offset: 0x00192A8C
		public virtual void SerializeNode(GraphSerializationContext ctx)
		{
			ctx.writer.Write(this.Penalty);
			ctx.writer.Write(this.Flags);
		}

		// Token: 0x0600236C RID: 9068 RVA: 0x001948B0 File Offset: 0x00192AB0
		public virtual void DeserializeNode(GraphSerializationContext ctx)
		{
			this.Penalty = ctx.reader.ReadUInt32();
			this.Flags = ctx.reader.ReadUInt32();
			this.GraphIndex = ctx.graphIndex;
		}

		// Token: 0x0600236D RID: 9069 RVA: 0x00002ACE File Offset: 0x00000CCE
		public virtual void SerializeReferences(GraphSerializationContext ctx)
		{
		}

		// Token: 0x0600236E RID: 9070 RVA: 0x00002ACE File Offset: 0x00000CCE
		public virtual void DeserializeReferences(GraphSerializationContext ctx)
		{
		}

		// Token: 0x04003F5F RID: 16223
		private int nodeIndex;

		// Token: 0x04003F60 RID: 16224
		protected uint flags;

		// Token: 0x04003F61 RID: 16225
		private uint penalty;

		// Token: 0x04003F62 RID: 16226
		private const int NodeIndexMask = 268435455;

		// Token: 0x04003F63 RID: 16227
		private const int DestroyedNodeIndex = 268435454;

		// Token: 0x04003F64 RID: 16228
		private const int TemporaryFlag1Mask = 268435456;

		// Token: 0x04003F65 RID: 16229
		private const int TemporaryFlag2Mask = 536870912;

		// Token: 0x04003F66 RID: 16230
		public Int3 position;

		// Token: 0x04003F67 RID: 16231
		private const int FlagsWalkableOffset = 0;

		// Token: 0x04003F68 RID: 16232
		private const uint FlagsWalkableMask = 1u;

		// Token: 0x04003F69 RID: 16233
		private const int FlagsAreaOffset = 1;

		// Token: 0x04003F6A RID: 16234
		private const uint FlagsAreaMask = 262142u;

		// Token: 0x04003F6B RID: 16235
		private const int FlagsGraphOffset = 24;

		// Token: 0x04003F6C RID: 16236
		private const uint FlagsGraphMask = 4278190080u;

		// Token: 0x04003F6D RID: 16237
		public const uint MaxAreaIndex = 131071u;

		// Token: 0x04003F6E RID: 16238
		public const uint MaxGraphIndex = 255u;

		// Token: 0x04003F6F RID: 16239
		private const int FlagsTagOffset = 19;

		// Token: 0x04003F70 RID: 16240
		private const uint FlagsTagMask = 16252928u;
	}
}
