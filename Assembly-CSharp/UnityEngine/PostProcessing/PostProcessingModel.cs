using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004D0 RID: 1232
	[Serializable]
	public abstract class PostProcessingModel
	{
		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001F2B RID: 7979 RVA: 0x0017F1F4 File Offset: 0x0017D3F4
		// (set) Token: 0x06001F2C RID: 7980 RVA: 0x0017F1FC File Offset: 0x0017D3FC
		public bool enabled
		{
			get
			{
				return this.m_Enabled;
			}
			set
			{
				this.m_Enabled = value;
				if (value)
				{
					this.OnValidate();
				}
			}
		}

		// Token: 0x06001F2D RID: 7981
		public abstract void Reset();

		// Token: 0x06001F2E RID: 7982 RVA: 0x00002ACE File Offset: 0x00000CCE
		public virtual void OnValidate()
		{
		}

		// Token: 0x04003CAD RID: 15533
		[SerializeField]
		[GetSet("enabled")]
		private bool m_Enabled;
	}
}
