using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004BC RID: 1212
	[Serializable]
	public class AntialiasingModel : PostProcessingModel
	{
		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001EC0 RID: 7872 RVA: 0x0017E1E5 File Offset: 0x0017C3E5
		// (set) Token: 0x06001EC1 RID: 7873 RVA: 0x0017E1ED File Offset: 0x0017C3ED
		public AntialiasingModel.Settings settings
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

		// Token: 0x06001EC2 RID: 7874 RVA: 0x0017E1F6 File Offset: 0x0017C3F6
		public override void Reset()
		{
			this.m_Settings = AntialiasingModel.Settings.defaultSettings;
		}

		// Token: 0x04003C79 RID: 15481
		[SerializeField]
		private AntialiasingModel.Settings m_Settings = AntialiasingModel.Settings.defaultSettings;

		// Token: 0x020006CE RID: 1742
		public enum Method
		{
			// Token: 0x0400479C RID: 18332
			Fxaa,
			// Token: 0x0400479D RID: 18333
			Taa
		}

		// Token: 0x020006CF RID: 1743
		public enum FxaaPreset
		{
			// Token: 0x0400479F RID: 18335
			ExtremePerformance,
			// Token: 0x040047A0 RID: 18336
			Performance,
			// Token: 0x040047A1 RID: 18337
			Default,
			// Token: 0x040047A2 RID: 18338
			Quality,
			// Token: 0x040047A3 RID: 18339
			ExtremeQuality
		}

		// Token: 0x020006D0 RID: 1744
		[Serializable]
		public struct FxaaQualitySettings
		{
			// Token: 0x040047A4 RID: 18340
			[Tooltip("The amount of desired sub-pixel aliasing removal. Effects the sharpeness of the output.")]
			[Range(0f, 1f)]
			public float subpixelAliasingRemovalAmount;

			// Token: 0x040047A5 RID: 18341
			[Tooltip("The minimum amount of local contrast required to qualify a region as containing an edge.")]
			[Range(0.063f, 0.333f)]
			public float edgeDetectionThreshold;

			// Token: 0x040047A6 RID: 18342
			[Tooltip("Local contrast adaptation value to disallow the algorithm from executing on the darker regions.")]
			[Range(0f, 0.0833f)]
			public float minimumRequiredLuminance;

			// Token: 0x040047A7 RID: 18343
			public static AntialiasingModel.FxaaQualitySettings[] presets = new AntialiasingModel.FxaaQualitySettings[]
			{
				new AntialiasingModel.FxaaQualitySettings
				{
					subpixelAliasingRemovalAmount = 0f,
					edgeDetectionThreshold = 0.333f,
					minimumRequiredLuminance = 0.0833f
				},
				new AntialiasingModel.FxaaQualitySettings
				{
					subpixelAliasingRemovalAmount = 0.25f,
					edgeDetectionThreshold = 0.25f,
					minimumRequiredLuminance = 0.0833f
				},
				new AntialiasingModel.FxaaQualitySettings
				{
					subpixelAliasingRemovalAmount = 0.75f,
					edgeDetectionThreshold = 0.166f,
					minimumRequiredLuminance = 0.0833f
				},
				new AntialiasingModel.FxaaQualitySettings
				{
					subpixelAliasingRemovalAmount = 1f,
					edgeDetectionThreshold = 0.125f,
					minimumRequiredLuminance = 0.0625f
				},
				new AntialiasingModel.FxaaQualitySettings
				{
					subpixelAliasingRemovalAmount = 1f,
					edgeDetectionThreshold = 0.063f,
					minimumRequiredLuminance = 0.0312f
				}
			};
		}

		// Token: 0x020006D1 RID: 1745
		[Serializable]
		public struct FxaaConsoleSettings
		{
			// Token: 0x040047A8 RID: 18344
			[Tooltip("The amount of spread applied to the sampling coordinates while sampling for subpixel information.")]
			[Range(0.33f, 0.5f)]
			public float subpixelSpreadAmount;

			// Token: 0x040047A9 RID: 18345
			[Tooltip("This value dictates how sharp the edges in the image are kept; a higher value implies sharper edges.")]
			[Range(2f, 8f)]
			public float edgeSharpnessAmount;

			// Token: 0x040047AA RID: 18346
			[Tooltip("The minimum amount of local contrast required to qualify a region as containing an edge.")]
			[Range(0.125f, 0.25f)]
			public float edgeDetectionThreshold;

			// Token: 0x040047AB RID: 18347
			[Tooltip("Local contrast adaptation value to disallow the algorithm from executing on the darker regions.")]
			[Range(0.04f, 0.06f)]
			public float minimumRequiredLuminance;

			// Token: 0x040047AC RID: 18348
			public static AntialiasingModel.FxaaConsoleSettings[] presets = new AntialiasingModel.FxaaConsoleSettings[]
			{
				new AntialiasingModel.FxaaConsoleSettings
				{
					subpixelSpreadAmount = 0.33f,
					edgeSharpnessAmount = 8f,
					edgeDetectionThreshold = 0.25f,
					minimumRequiredLuminance = 0.06f
				},
				new AntialiasingModel.FxaaConsoleSettings
				{
					subpixelSpreadAmount = 0.33f,
					edgeSharpnessAmount = 8f,
					edgeDetectionThreshold = 0.125f,
					minimumRequiredLuminance = 0.06f
				},
				new AntialiasingModel.FxaaConsoleSettings
				{
					subpixelSpreadAmount = 0.5f,
					edgeSharpnessAmount = 8f,
					edgeDetectionThreshold = 0.125f,
					minimumRequiredLuminance = 0.05f
				},
				new AntialiasingModel.FxaaConsoleSettings
				{
					subpixelSpreadAmount = 0.5f,
					edgeSharpnessAmount = 4f,
					edgeDetectionThreshold = 0.125f,
					minimumRequiredLuminance = 0.04f
				},
				new AntialiasingModel.FxaaConsoleSettings
				{
					subpixelSpreadAmount = 0.5f,
					edgeSharpnessAmount = 2f,
					edgeDetectionThreshold = 0.125f,
					minimumRequiredLuminance = 0.04f
				}
			};
		}

		// Token: 0x020006D2 RID: 1746
		[Serializable]
		public struct FxaaSettings
		{
			// Token: 0x1700063E RID: 1598
			// (get) Token: 0x06002C00 RID: 11264 RVA: 0x001C8224 File Offset: 0x001C6424
			public static AntialiasingModel.FxaaSettings defaultSettings
			{
				get
				{
					return new AntialiasingModel.FxaaSettings
					{
						preset = AntialiasingModel.FxaaPreset.Default
					};
				}
			}

			// Token: 0x040047AD RID: 18349
			public AntialiasingModel.FxaaPreset preset;
		}

		// Token: 0x020006D3 RID: 1747
		[Serializable]
		public struct TaaSettings
		{
			// Token: 0x1700063F RID: 1599
			// (get) Token: 0x06002C01 RID: 11265 RVA: 0x001C8244 File Offset: 0x001C6444
			public static AntialiasingModel.TaaSettings defaultSettings
			{
				get
				{
					return new AntialiasingModel.TaaSettings
					{
						jitterSpread = 0.75f,
						sharpen = 0.3f,
						stationaryBlending = 0.95f,
						motionBlending = 0.85f
					};
				}
			}

			// Token: 0x040047AE RID: 18350
			[Tooltip("The diameter (in texels) inside which jitter samples are spread. Smaller values result in crisper but more aliased output, while larger values result in more stable but blurrier output.")]
			[Range(0.1f, 1f)]
			public float jitterSpread;

			// Token: 0x040047AF RID: 18351
			[Tooltip("Controls the amount of sharpening applied to the color buffer.")]
			[Range(0f, 3f)]
			public float sharpen;

			// Token: 0x040047B0 RID: 18352
			[Tooltip("The blend coefficient for a stationary fragment. Controls the percentage of history sample blended into the final color.")]
			[Range(0f, 0.99f)]
			public float stationaryBlending;

			// Token: 0x040047B1 RID: 18353
			[Tooltip("The blend coefficient for a fragment with significant motion. Controls the percentage of history sample blended into the final color.")]
			[Range(0f, 0.99f)]
			public float motionBlending;
		}

		// Token: 0x020006D4 RID: 1748
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000640 RID: 1600
			// (get) Token: 0x06002C02 RID: 11266 RVA: 0x001C828C File Offset: 0x001C648C
			public static AntialiasingModel.Settings defaultSettings
			{
				get
				{
					return new AntialiasingModel.Settings
					{
						method = AntialiasingModel.Method.Fxaa,
						fxaaSettings = AntialiasingModel.FxaaSettings.defaultSettings,
						taaSettings = AntialiasingModel.TaaSettings.defaultSettings
					};
				}
			}

			// Token: 0x040047B2 RID: 18354
			public AntialiasingModel.Method method;

			// Token: 0x040047B3 RID: 18355
			public AntialiasingModel.FxaaSettings fxaaSettings;

			// Token: 0x040047B4 RID: 18356
			public AntialiasingModel.TaaSettings taaSettings;
		}
	}
}
