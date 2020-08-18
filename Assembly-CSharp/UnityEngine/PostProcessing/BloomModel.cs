using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004BD RID: 1213
	[Serializable]
	public class BloomModel : PostProcessingModel
	{
		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001EC4 RID: 7876 RVA: 0x0017E216 File Offset: 0x0017C416
		// (set) Token: 0x06001EC5 RID: 7877 RVA: 0x0017E21E File Offset: 0x0017C41E
		public BloomModel.Settings settings
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

		// Token: 0x06001EC6 RID: 7878 RVA: 0x0017E227 File Offset: 0x0017C427
		public override void Reset()
		{
			this.m_Settings = BloomModel.Settings.defaultSettings;
		}

		// Token: 0x04003C7A RID: 15482
		[SerializeField]
		private BloomModel.Settings m_Settings = BloomModel.Settings.defaultSettings;

		// Token: 0x020006D5 RID: 1749
		[Serializable]
		public struct BloomSettings
		{
			// Token: 0x17000641 RID: 1601
			// (get) Token: 0x06002C04 RID: 11268 RVA: 0x001C82D0 File Offset: 0x001C64D0
			// (set) Token: 0x06002C03 RID: 11267 RVA: 0x001C82C2 File Offset: 0x001C64C2
			public float thresholdLinear
			{
				get
				{
					return Mathf.GammaToLinearSpace(this.threshold);
				}
				set
				{
					this.threshold = Mathf.LinearToGammaSpace(value);
				}
			}

			// Token: 0x17000642 RID: 1602
			// (get) Token: 0x06002C05 RID: 11269 RVA: 0x001C82E0 File Offset: 0x001C64E0
			public static BloomModel.BloomSettings defaultSettings
			{
				get
				{
					return new BloomModel.BloomSettings
					{
						intensity = 0.5f,
						threshold = 1.1f,
						softKnee = 0.5f,
						radius = 4f,
						antiFlicker = false
					};
				}
			}

			// Token: 0x040047B5 RID: 18357
			[Min(0f)]
			[Tooltip("Strength of the bloom filter.")]
			public float intensity;

			// Token: 0x040047B6 RID: 18358
			[Min(0f)]
			[Tooltip("Filters out pixels under this level of brightness.")]
			public float threshold;

			// Token: 0x040047B7 RID: 18359
			[Range(0f, 1f)]
			[Tooltip("Makes transition between under/over-threshold gradual (0 = hard threshold, 1 = soft threshold).")]
			public float softKnee;

			// Token: 0x040047B8 RID: 18360
			[Range(1f, 7f)]
			[Tooltip("Changes extent of veiling effects in a screen resolution-independent fashion.")]
			public float radius;

			// Token: 0x040047B9 RID: 18361
			[Tooltip("Reduces flashing noise with an additional filter.")]
			public bool antiFlicker;
		}

		// Token: 0x020006D6 RID: 1750
		[Serializable]
		public struct LensDirtSettings
		{
			// Token: 0x17000643 RID: 1603
			// (get) Token: 0x06002C06 RID: 11270 RVA: 0x001C8330 File Offset: 0x001C6530
			public static BloomModel.LensDirtSettings defaultSettings
			{
				get
				{
					return new BloomModel.LensDirtSettings
					{
						texture = null,
						intensity = 3f
					};
				}
			}

			// Token: 0x040047BA RID: 18362
			[Tooltip("Dirtiness texture to add smudges or dust to the lens.")]
			public Texture texture;

			// Token: 0x040047BB RID: 18363
			[Min(0f)]
			[Tooltip("Amount of lens dirtiness.")]
			public float intensity;
		}

		// Token: 0x020006D7 RID: 1751
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000644 RID: 1604
			// (get) Token: 0x06002C07 RID: 11271 RVA: 0x001C835C File Offset: 0x001C655C
			public static BloomModel.Settings defaultSettings
			{
				get
				{
					return new BloomModel.Settings
					{
						bloom = BloomModel.BloomSettings.defaultSettings,
						lensDirt = BloomModel.LensDirtSettings.defaultSettings
					};
				}
			}

			// Token: 0x040047BC RID: 18364
			public BloomModel.BloomSettings bloom;

			// Token: 0x040047BD RID: 18365
			public BloomModel.LensDirtSettings lensDirt;
		}
	}
}
