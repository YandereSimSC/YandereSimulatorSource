using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004C2 RID: 1218
	[Serializable]
	public class DitheringModel : PostProcessingModel
	{
		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06001EDF RID: 7903 RVA: 0x0017E385 File Offset: 0x0017C585
		// (set) Token: 0x06001EE0 RID: 7904 RVA: 0x0017E38D File Offset: 0x0017C58D
		public DitheringModel.Settings settings
		{
			get
			{
				return this.m_Settings;
			}
			set
			{
				this.m_Settings = value;
			}
		}

		// Token: 0x06001EE1 RID: 7905 RVA: 0x0017E396 File Offset: 0x0017C596
		public override void Reset()
		{
			this.m_Settings = DitheringModel.Settings.defaultSettings;
		}

		// Token: 0x04003C81 RID: 15489
		[SerializeField]
		private DitheringModel.Settings m_Settings = DitheringModel.Settings.defaultSettings;

		// Token: 0x020006E9 RID: 1769
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000652 RID: 1618
			// (get) Token: 0x06002C15 RID: 11285 RVA: 0x001C8980 File Offset: 0x001C6B80
			public static DitheringModel.Settings defaultSettings
			{
				get
				{
					return default(DitheringModel.Settings);
				}
			}
		}
	}
}
