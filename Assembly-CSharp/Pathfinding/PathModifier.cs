using System;

namespace Pathfinding
{
	// Token: 0x02000582 RID: 1410
	[Serializable]
	public abstract class PathModifier : IPathModifier
	{
		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x0600264D RID: 9805
		public abstract int Order { get; }

		// Token: 0x0600264E RID: 9806 RVA: 0x001A5EC7 File Offset: 0x001A40C7
		public void Awake(Seeker seeker)
		{
			this.seeker = seeker;
			if (seeker != null)
			{
				seeker.RegisterModifier(this);
			}
		}

		// Token: 0x0600264F RID: 9807 RVA: 0x001A5EE0 File Offset: 0x001A40E0
		public void OnDestroy(Seeker seeker)
		{
			if (seeker != null)
			{
				seeker.DeregisterModifier(this);
			}
		}

		// Token: 0x06002650 RID: 9808 RVA: 0x00002ACE File Offset: 0x00000CCE
		public virtual void PreProcess(Path path)
		{
		}

		// Token: 0x06002651 RID: 9809
		public abstract void Apply(Path path);

		// Token: 0x04004130 RID: 16688
		[NonSerialized]
		public Seeker seeker;
	}
}
