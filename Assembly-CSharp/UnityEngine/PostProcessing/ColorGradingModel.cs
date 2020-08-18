using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004C0 RID: 1216
	[Serializable]
	public class ColorGradingModel : PostProcessingModel
	{
		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001ED2 RID: 7890 RVA: 0x0017E2EC File Offset: 0x0017C4EC
		// (set) Token: 0x06001ED3 RID: 7891 RVA: 0x0017E2F4 File Offset: 0x0017C4F4
		public ColorGradingModel.Settings settings
		{
			get
			{
				return this.m_Settings;
			}
			set
			{
				this.m_Settings = value;
				this.OnValidate();
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001ED4 RID: 7892 RVA: 0x0017E303 File Offset: 0x0017C503
		// (set) Token: 0x06001ED5 RID: 7893 RVA: 0x0017E30B File Offset: 0x0017C50B
		public bool isDirty { get; internal set; }

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001ED6 RID: 7894 RVA: 0x0017E314 File Offset: 0x0017C514
		// (set) Token: 0x06001ED7 RID: 7895 RVA: 0x0017E31C File Offset: 0x0017C51C
		public RenderTexture bakedLut { get; internal set; }

		// Token: 0x06001ED8 RID: 7896 RVA: 0x0017E325 File Offset: 0x0017C525
		public override void Reset()
		{
			this.m_Settings = ColorGradingModel.Settings.defaultSettings;
			this.OnValidate();
		}

		// Token: 0x06001ED9 RID: 7897 RVA: 0x0017E338 File Offset: 0x0017C538
		public override void OnValidate()
		{
			this.isDirty = true;
		}

		// Token: 0x04003C7D RID: 15485
		[SerializeField]
		private ColorGradingModel.Settings m_Settings = ColorGradingModel.Settings.defaultSettings;

		// Token: 0x020006DD RID: 1757
		public enum Tonemapper
		{
			// Token: 0x040047D6 RID: 18390
			None,
			// Token: 0x040047D7 RID: 18391
			ACES,
			// Token: 0x040047D8 RID: 18392
			Neutral
		}

		// Token: 0x020006DE RID: 1758
		[Serializable]
		public struct TonemappingSettings
		{
			// Token: 0x17000649 RID: 1609
			// (get) Token: 0x06002C0C RID: 11276 RVA: 0x001C8470 File Offset: 0x001C6670
			public static ColorGradingModel.TonemappingSettings defaultSettings
			{
				get
				{
					return new ColorGradingModel.TonemappingSettings
					{
						tonemapper = ColorGradingModel.Tonemapper.Neutral,
						neutralBlackIn = 0.02f,
						neutralWhiteIn = 10f,
						neutralBlackOut = 0f,
						neutralWhiteOut = 10f,
						neutralWhiteLevel = 5.3f,
						neutralWhiteClip = 10f
					};
				}
			}

			// Token: 0x040047D9 RID: 18393
			[Tooltip("Tonemapping algorithm to use at the end of the color grading process. Use \"Neutral\" if you need a customizable tonemapper or \"Filmic\" to give a standard filmic look to your scenes.")]
			public ColorGradingModel.Tonemapper tonemapper;

			// Token: 0x040047DA RID: 18394
			[Range(-0.1f, 0.1f)]
			public float neutralBlackIn;

			// Token: 0x040047DB RID: 18395
			[Range(1f, 20f)]
			public float neutralWhiteIn;

			// Token: 0x040047DC RID: 18396
			[Range(-0.09f, 0.1f)]
			public float neutralBlackOut;

			// Token: 0x040047DD RID: 18397
			[Range(1f, 19f)]
			public float neutralWhiteOut;

			// Token: 0x040047DE RID: 18398
			[Range(0.1f, 20f)]
			public float neutralWhiteLevel;

			// Token: 0x040047DF RID: 18399
			[Range(1f, 10f)]
			public float neutralWhiteClip;
		}

		// Token: 0x020006DF RID: 1759
		[Serializable]
		public struct BasicSettings
		{
			// Token: 0x1700064A RID: 1610
			// (get) Token: 0x06002C0D RID: 11277 RVA: 0x001C84D8 File Offset: 0x001C66D8
			public static ColorGradingModel.BasicSettings defaultSettings
			{
				get
				{
					return new ColorGradingModel.BasicSettings
					{
						postExposure = 0f,
						temperature = 0f,
						tint = 0f,
						hueShift = 0f,
						saturation = 1f,
						contrast = 1f
					};
				}
			}

			// Token: 0x040047E0 RID: 18400
			[Tooltip("Adjusts the overall exposure of the scene in EV units. This is applied after HDR effect and right before tonemapping so it won't affect previous effects in the chain.")]
			public float postExposure;

			// Token: 0x040047E1 RID: 18401
			[Range(-100f, 100f)]
			[Tooltip("Sets the white balance to a custom color temperature.")]
			public float temperature;

			// Token: 0x040047E2 RID: 18402
			[Range(-100f, 100f)]
			[Tooltip("Sets the white balance to compensate for a green or magenta tint.")]
			public float tint;

			// Token: 0x040047E3 RID: 18403
			[Range(-180f, 180f)]
			[Tooltip("Shift the hue of all colors.")]
			public float hueShift;

			// Token: 0x040047E4 RID: 18404
			[Range(0f, 2f)]
			[Tooltip("Pushes the intensity of all colors.")]
			public float saturation;

			// Token: 0x040047E5 RID: 18405
			[Range(0f, 2f)]
			[Tooltip("Expands or shrinks the overall range of tonal values.")]
			public float contrast;
		}

		// Token: 0x020006E0 RID: 1760
		[Serializable]
		public struct ChannelMixerSettings
		{
			// Token: 0x1700064B RID: 1611
			// (get) Token: 0x06002C0E RID: 11278 RVA: 0x001C8538 File Offset: 0x001C6738
			public static ColorGradingModel.ChannelMixerSettings defaultSettings
			{
				get
				{
					return new ColorGradingModel.ChannelMixerSettings
					{
						red = new Vector3(1f, 0f, 0f),
						green = new Vector3(0f, 1f, 0f),
						blue = new Vector3(0f, 0f, 1f),
						currentEditingChannel = 0
					};
				}
			}

			// Token: 0x040047E6 RID: 18406
			public Vector3 red;

			// Token: 0x040047E7 RID: 18407
			public Vector3 green;

			// Token: 0x040047E8 RID: 18408
			public Vector3 blue;

			// Token: 0x040047E9 RID: 18409
			[HideInInspector]
			public int currentEditingChannel;
		}

		// Token: 0x020006E1 RID: 1761
		[Serializable]
		public struct LogWheelsSettings
		{
			// Token: 0x1700064C RID: 1612
			// (get) Token: 0x06002C0F RID: 11279 RVA: 0x001C85A8 File Offset: 0x001C67A8
			public static ColorGradingModel.LogWheelsSettings defaultSettings
			{
				get
				{
					return new ColorGradingModel.LogWheelsSettings
					{
						slope = Color.clear,
						power = Color.clear,
						offset = Color.clear
					};
				}
			}

			// Token: 0x040047EA RID: 18410
			[Trackball("GetSlopeValue")]
			public Color slope;

			// Token: 0x040047EB RID: 18411
			[Trackball("GetPowerValue")]
			public Color power;

			// Token: 0x040047EC RID: 18412
			[Trackball("GetOffsetValue")]
			public Color offset;
		}

		// Token: 0x020006E2 RID: 1762
		[Serializable]
		public struct LinearWheelsSettings
		{
			// Token: 0x1700064D RID: 1613
			// (get) Token: 0x06002C10 RID: 11280 RVA: 0x001C85E4 File Offset: 0x001C67E4
			public static ColorGradingModel.LinearWheelsSettings defaultSettings
			{
				get
				{
					return new ColorGradingModel.LinearWheelsSettings
					{
						lift = Color.clear,
						gamma = Color.clear,
						gain = Color.clear
					};
				}
			}

			// Token: 0x040047ED RID: 18413
			[Trackball("GetLiftValue")]
			public Color lift;

			// Token: 0x040047EE RID: 18414
			[Trackball("GetGammaValue")]
			public Color gamma;

			// Token: 0x040047EF RID: 18415
			[Trackball("GetGainValue")]
			public Color gain;
		}

		// Token: 0x020006E3 RID: 1763
		public enum ColorWheelMode
		{
			// Token: 0x040047F1 RID: 18417
			Linear,
			// Token: 0x040047F2 RID: 18418
			Log
		}

		// Token: 0x020006E4 RID: 1764
		[Serializable]
		public struct ColorWheelsSettings
		{
			// Token: 0x1700064E RID: 1614
			// (get) Token: 0x06002C11 RID: 11281 RVA: 0x001C8620 File Offset: 0x001C6820
			public static ColorGradingModel.ColorWheelsSettings defaultSettings
			{
				get
				{
					return new ColorGradingModel.ColorWheelsSettings
					{
						mode = ColorGradingModel.ColorWheelMode.Log,
						log = ColorGradingModel.LogWheelsSettings.defaultSettings,
						linear = ColorGradingModel.LinearWheelsSettings.defaultSettings
					};
				}
			}

			// Token: 0x040047F3 RID: 18419
			public ColorGradingModel.ColorWheelMode mode;

			// Token: 0x040047F4 RID: 18420
			[TrackballGroup]
			public ColorGradingModel.LogWheelsSettings log;

			// Token: 0x040047F5 RID: 18421
			[TrackballGroup]
			public ColorGradingModel.LinearWheelsSettings linear;
		}

		// Token: 0x020006E5 RID: 1765
		[Serializable]
		public struct CurvesSettings
		{
			// Token: 0x1700064F RID: 1615
			// (get) Token: 0x06002C12 RID: 11282 RVA: 0x001C8658 File Offset: 0x001C6858
			public static ColorGradingModel.CurvesSettings defaultSettings
			{
				get
				{
					return new ColorGradingModel.CurvesSettings
					{
						master = new ColorGradingCurve(new AnimationCurve(new Keyframe[]
						{
							new Keyframe(0f, 0f, 1f, 1f),
							new Keyframe(1f, 1f, 1f, 1f)
						}), 0f, false, new Vector2(0f, 1f)),
						red = new ColorGradingCurve(new AnimationCurve(new Keyframe[]
						{
							new Keyframe(0f, 0f, 1f, 1f),
							new Keyframe(1f, 1f, 1f, 1f)
						}), 0f, false, new Vector2(0f, 1f)),
						green = new ColorGradingCurve(new AnimationCurve(new Keyframe[]
						{
							new Keyframe(0f, 0f, 1f, 1f),
							new Keyframe(1f, 1f, 1f, 1f)
						}), 0f, false, new Vector2(0f, 1f)),
						blue = new ColorGradingCurve(new AnimationCurve(new Keyframe[]
						{
							new Keyframe(0f, 0f, 1f, 1f),
							new Keyframe(1f, 1f, 1f, 1f)
						}), 0f, false, new Vector2(0f, 1f)),
						hueVShue = new ColorGradingCurve(new AnimationCurve(), 0.5f, true, new Vector2(0f, 1f)),
						hueVSsat = new ColorGradingCurve(new AnimationCurve(), 0.5f, true, new Vector2(0f, 1f)),
						satVSsat = new ColorGradingCurve(new AnimationCurve(), 0.5f, false, new Vector2(0f, 1f)),
						lumVSsat = new ColorGradingCurve(new AnimationCurve(), 0.5f, false, new Vector2(0f, 1f)),
						e_CurrentEditingCurve = 0,
						e_CurveY = true,
						e_CurveR = false,
						e_CurveG = false,
						e_CurveB = false
					};
				}
			}

			// Token: 0x040047F6 RID: 18422
			public ColorGradingCurve master;

			// Token: 0x040047F7 RID: 18423
			public ColorGradingCurve red;

			// Token: 0x040047F8 RID: 18424
			public ColorGradingCurve green;

			// Token: 0x040047F9 RID: 18425
			public ColorGradingCurve blue;

			// Token: 0x040047FA RID: 18426
			public ColorGradingCurve hueVShue;

			// Token: 0x040047FB RID: 18427
			public ColorGradingCurve hueVSsat;

			// Token: 0x040047FC RID: 18428
			public ColorGradingCurve satVSsat;

			// Token: 0x040047FD RID: 18429
			public ColorGradingCurve lumVSsat;

			// Token: 0x040047FE RID: 18430
			[HideInInspector]
			public int e_CurrentEditingCurve;

			// Token: 0x040047FF RID: 18431
			[HideInInspector]
			public bool e_CurveY;

			// Token: 0x04004800 RID: 18432
			[HideInInspector]
			public bool e_CurveR;

			// Token: 0x04004801 RID: 18433
			[HideInInspector]
			public bool e_CurveG;

			// Token: 0x04004802 RID: 18434
			[HideInInspector]
			public bool e_CurveB;
		}

		// Token: 0x020006E6 RID: 1766
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000650 RID: 1616
			// (get) Token: 0x06002C13 RID: 11283 RVA: 0x001C88E0 File Offset: 0x001C6AE0
			public static ColorGradingModel.Settings defaultSettings
			{
				get
				{
					return new ColorGradingModel.Settings
					{
						tonemapping = ColorGradingModel.TonemappingSettings.defaultSettings,
						basic = ColorGradingModel.BasicSettings.defaultSettings,
						channelMixer = ColorGradingModel.ChannelMixerSettings.defaultSettings,
						colorWheels = ColorGradingModel.ColorWheelsSettings.defaultSettings,
						curves = ColorGradingModel.CurvesSettings.defaultSettings
					};
				}
			}

			// Token: 0x04004803 RID: 18435
			public ColorGradingModel.TonemappingSettings tonemapping;

			// Token: 0x04004804 RID: 18436
			public ColorGradingModel.BasicSettings basic;

			// Token: 0x04004805 RID: 18437
			public ColorGradingModel.ChannelMixerSettings channelMixer;

			// Token: 0x04004806 RID: 18438
			public ColorGradingModel.ColorWheelsSettings colorWheels;

			// Token: 0x04004807 RID: 18439
			public ColorGradingModel.CurvesSettings curves;
		}
	}
}
