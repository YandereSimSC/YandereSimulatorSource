using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004C4 RID: 1220
	[Serializable]
	public class FogModel : PostProcessingModel
	{
		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001EE7 RID: 7911 RVA: 0x0017E3E7 File Offset: 0x0017C5E7
		// (set) Token: 0x06001EE8 RID: 7912 RVA: 0x0017E3EF File Offset: 0x0017C5EF
		public FogModel.Settings settings
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

		// Token: 0x06001EE9 RID: 7913 RVA: 0x0017E3F8 File Offset: 0x0017C5F8
		public override void Reset()
		{
			this.m_Settings = FogModel.Settings.defaultSettings;
		}

		// Token: 0x04003C83 RID: 15491
		[SerializeField]
		private FogModel.Settings m_Settings = FogModel.Settings.defaultSettings;

		// Token: 0x020006EC RID: 1772
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000654 RID: 1620
			// (get) Token: 0x06002C17 RID: 11287 RVA: 0x001C8A24 File Offset: 0x001C6C24
			public static FogModel.Settings defaultSettings
			{
				get
				{
					return new FogModel.Settings
					{
						excludeSkybox = true
					};
				}
			}

			// Token: 0x04004820 RID: 18464
			[Tooltip("Should the fog affect the skybox?")]
			public bool excludeSkybox;
		}
	}
}
