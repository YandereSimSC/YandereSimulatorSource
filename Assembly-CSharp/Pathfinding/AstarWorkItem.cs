using System;

namespace Pathfinding
{
	// Token: 0x02000533 RID: 1331
	public struct AstarWorkItem
	{
		// Token: 0x06002335 RID: 9013 RVA: 0x001942C4 File Offset: 0x001924C4
		public AstarWorkItem(Func<bool, bool> update)
		{
			this.init = null;
			this.initWithContext = null;
			this.updateWithContext = null;
			this.update = update;
		}

		// Token: 0x06002336 RID: 9014 RVA: 0x001942E2 File Offset: 0x001924E2
		public AstarWorkItem(Func<IWorkItemContext, bool, bool> update)
		{
			this.init = null;
			this.initWithContext = null;
			this.updateWithContext = update;
			this.update = null;
		}

		// Token: 0x06002337 RID: 9015 RVA: 0x00194300 File Offset: 0x00192500
		public AstarWorkItem(Action init, Func<bool, bool> update = null)
		{
			this.init = init;
			this.initWithContext = null;
			this.update = update;
			this.updateWithContext = null;
		}

		// Token: 0x06002338 RID: 9016 RVA: 0x0019431E File Offset: 0x0019251E
		public AstarWorkItem(Action<IWorkItemContext> init, Func<IWorkItemContext, bool, bool> update = null)
		{
			this.init = null;
			this.initWithContext = init;
			this.update = null;
			this.updateWithContext = update;
		}

		// Token: 0x04003F53 RID: 16211
		public Action init;

		// Token: 0x04003F54 RID: 16212
		public Action<IWorkItemContext> initWithContext;

		// Token: 0x04003F55 RID: 16213
		public Func<bool, bool> update;

		// Token: 0x04003F56 RID: 16214
		public Func<IWorkItemContext, bool, bool> updateWithContext;
	}
}
