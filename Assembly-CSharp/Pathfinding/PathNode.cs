using System;

namespace Pathfinding
{
	// Token: 0x0200053D RID: 1341
	public class PathNode
	{
		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x060023CC RID: 9164 RVA: 0x001958AB File Offset: 0x00193AAB
		// (set) Token: 0x060023CD RID: 9165 RVA: 0x001958B9 File Offset: 0x00193AB9
		public uint cost
		{
			get
			{
				return this.flags & 268435455u;
			}
			set
			{
				this.flags = ((this.flags & 4026531840u) | value);
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x060023CE RID: 9166 RVA: 0x001958CF File Offset: 0x00193ACF
		// (set) Token: 0x060023CF RID: 9167 RVA: 0x001958E0 File Offset: 0x00193AE0
		public bool flag1
		{
			get
			{
				return (this.flags & 268435456u) > 0u;
			}
			set
			{
				this.flags = ((this.flags & 4026531839u) | (value ? 268435456u : 0u));
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x060023D0 RID: 9168 RVA: 0x00195900 File Offset: 0x00193B00
		// (set) Token: 0x060023D1 RID: 9169 RVA: 0x00195911 File Offset: 0x00193B11
		public bool flag2
		{
			get
			{
				return (this.flags & 536870912u) > 0u;
			}
			set
			{
				this.flags = ((this.flags & 3758096383u) | (value ? 536870912u : 0u));
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x060023D2 RID: 9170 RVA: 0x00195931 File Offset: 0x00193B31
		// (set) Token: 0x060023D3 RID: 9171 RVA: 0x00195939 File Offset: 0x00193B39
		public uint G
		{
			get
			{
				return this.g;
			}
			set
			{
				this.g = value;
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x060023D4 RID: 9172 RVA: 0x00195942 File Offset: 0x00193B42
		// (set) Token: 0x060023D5 RID: 9173 RVA: 0x0019594A File Offset: 0x00193B4A
		public uint H
		{
			get
			{
				return this.h;
			}
			set
			{
				this.h = value;
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x060023D6 RID: 9174 RVA: 0x00195953 File Offset: 0x00193B53
		public uint F
		{
			get
			{
				return this.g + this.h;
			}
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x00195962 File Offset: 0x00193B62
		public void UpdateG(Path path)
		{
			this.g = this.parent.g + this.cost + path.GetTraversalCost(this.node);
		}

		// Token: 0x04003F8E RID: 16270
		public GraphNode node;

		// Token: 0x04003F8F RID: 16271
		public PathNode parent;

		// Token: 0x04003F90 RID: 16272
		public ushort pathID;

		// Token: 0x04003F91 RID: 16273
		public ushort heapIndex = ushort.MaxValue;

		// Token: 0x04003F92 RID: 16274
		private uint flags;

		// Token: 0x04003F93 RID: 16275
		private const uint CostMask = 268435455u;

		// Token: 0x04003F94 RID: 16276
		private const int Flag1Offset = 28;

		// Token: 0x04003F95 RID: 16277
		private const uint Flag1Mask = 268435456u;

		// Token: 0x04003F96 RID: 16278
		private const int Flag2Offset = 29;

		// Token: 0x04003F97 RID: 16279
		private const uint Flag2Mask = 536870912u;

		// Token: 0x04003F98 RID: 16280
		private uint g;

		// Token: 0x04003F99 RID: 16281
		private uint h;
	}
}
