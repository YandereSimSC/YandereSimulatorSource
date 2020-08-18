using System;

namespace Pathfinding
{
	// Token: 0x02000583 RID: 1411
	[Serializable]
	public abstract class MonoModifier : VersionedMonoBehaviour, IPathModifier
	{
		// Token: 0x06002653 RID: 9811 RVA: 0x001A5EF2 File Offset: 0x001A40F2
		protected virtual void OnEnable()
		{
			this.seeker = base.GetComponent<Seeker>();
			if (this.seeker != null)
			{
				this.seeker.RegisterModifier(this);
			}
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x001A5F1A File Offset: 0x001A411A
		protected virtual void OnDisable()
		{
			if (this.seeker != null)
			{
				this.seeker.DeregisterModifier(this);
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06002655 RID: 9813
		public abstract int Order { get; }

		// Token: 0x06002656 RID: 9814 RVA: 0x00002ACE File Offset: 0x00000CCE
		public virtual void PreProcess(Path path)
		{
		}

		// Token: 0x06002657 RID: 9815
		public abstract void Apply(Path path);

		// Token: 0x04004131 RID: 16689
		[NonSerialized]
		public Seeker seeker;
	}
}
