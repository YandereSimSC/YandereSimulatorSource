using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004BF RID: 1215
	[Serializable]
	public class ChromaticAberrationModel : PostProcessingModel
	{
		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001ECE RID: 7886 RVA: 0x0017E2BB File Offset: 0x0017C4BB
		// (set) Token: 0x06001ECF RID: 7887 RVA: 0x0017E2C3 File Offset: 0x0017C4C3
		public ChromaticAberrationModel.Settings settings
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

		// Token: 0x06001ED0 RID: 7888 RVA: 0x0017E2CC File Offset: 0x0017C4CC
		public override void Reset()
		{
			this.m_Settings = ChromaticAberrationModel.Settings.defaultSettings;
		}

		// Token: 0x04003C7C RID: 15484
		[SerializeField]
		private ChromaticAberrationModel.Settings m_Settings = ChromaticAberrationModel.Settings.defaultSettings;

		// Token: 0x020006DC RID: 1756
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000648 RID: 1608
			// (get) Token: 0x06002C0B RID: 11275 RVA: 0x001C8444 File Offset: 0x001C6644
			public static ChromaticAberrationModel.Settings defaultSettings
			{
				get
				{
					return new ChromaticAberrationModel.Settings
					{
						spectralTexture = null,
						intensity = 0.1f
					};
				}
			}

			// Token: 0x040047D3 RID: 18387
			[Tooltip("Shift the hue of chromatic aberrations.")]
			public Texture2D spectralTexture;

			// Token: 0x040047D4 RID: 18388
			[Range(0f, 1f)]
			[Tooltip("Amount of tangential distortion.")]
			public float intensity;
		}
	}
}
