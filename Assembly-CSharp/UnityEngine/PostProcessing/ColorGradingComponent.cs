using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004AF RID: 1199
	public sealed class ColorGradingComponent : PostProcessingComponentRenderTexture<ColorGradingModel>
	{
		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06001E5C RID: 7772 RVA: 0x0017B0C0 File Offset: 0x001792C0
		public override bool active
		{
			get
			{
				return base.model.enabled && !this.context.interrupted;
			}
		}

		// Token: 0x06001E5D RID: 7773 RVA: 0x0017B0DF File Offset: 0x001792DF
		private float StandardIlluminantY(float x)
		{
			return 2.87f * x - 3f * x * x - 0.27509508f;
		}

		// Token: 0x06001E5E RID: 7774 RVA: 0x0017B0F8 File Offset: 0x001792F8
		private Vector3 CIExyToLMS(float x, float y)
		{
			float num = 1f;
			float num2 = num * x / y;
			float num3 = num * (1f - x - y) / y;
			float x2 = 0.7328f * num2 + 0.4296f * num - 0.1624f * num3;
			float y2 = -0.7036f * num2 + 1.6975f * num + 0.0061f * num3;
			float z = 0.003f * num2 + 0.0136f * num + 0.9834f * num3;
			return new Vector3(x2, y2, z);
		}

		// Token: 0x06001E5F RID: 7775 RVA: 0x0017B170 File Offset: 0x00179370
		private Vector3 CalculateColorBalance(float temperature, float tint)
		{
			float num = temperature / 55f;
			float num2 = tint / 55f;
			float x = 0.31271f - num * ((num < 0f) ? 0.1f : 0.05f);
			float y = this.StandardIlluminantY(x) + num2 * 0.05f;
			Vector3 vector = new Vector3(0.949237f, 1.03542f, 1.08728f);
			Vector3 vector2 = this.CIExyToLMS(x, y);
			return new Vector3(vector.x / vector2.x, vector.y / vector2.y, vector.z / vector2.z);
		}

		// Token: 0x06001E60 RID: 7776 RVA: 0x0017B20C File Offset: 0x0017940C
		private static Color NormalizeColor(Color c)
		{
			float num = (c.r + c.g + c.b) / 3f;
			if (Mathf.Approximately(num, 0f))
			{
				return new Color(1f, 1f, 1f, c.a);
			}
			return new Color
			{
				r = c.r / num,
				g = c.g / num,
				b = c.b / num,
				a = c.a
			};
		}

		// Token: 0x06001E61 RID: 7777 RVA: 0x0017B29F File Offset: 0x0017949F
		private static Vector3 ClampVector(Vector3 v, float min, float max)
		{
			return new Vector3(Mathf.Clamp(v.x, min, max), Mathf.Clamp(v.y, min, max), Mathf.Clamp(v.z, min, max));
		}

		// Token: 0x06001E62 RID: 7778 RVA: 0x0017B2D0 File Offset: 0x001794D0
		public static Vector3 GetLiftValue(Color lift)
		{
			Color color = ColorGradingComponent.NormalizeColor(lift);
			float num = (color.r + color.g + color.b) / 3f;
			float x = (color.r - num) * 0.1f + lift.a;
			float y = (color.g - num) * 0.1f + lift.a;
			float z = (color.b - num) * 0.1f + lift.a;
			return ColorGradingComponent.ClampVector(new Vector3(x, y, z), -1f, 1f);
		}

		// Token: 0x06001E63 RID: 7779 RVA: 0x0017B358 File Offset: 0x00179558
		public static Vector3 GetGammaValue(Color gamma)
		{
			Color color = ColorGradingComponent.NormalizeColor(gamma);
			float num = (color.r + color.g + color.b) / 3f;
			gamma.a *= ((gamma.a < 0f) ? 0.8f : 5f);
			float b = Mathf.Pow(2f, (color.r - num) * 0.5f) + gamma.a;
			float b2 = Mathf.Pow(2f, (color.g - num) * 0.5f) + gamma.a;
			float b3 = Mathf.Pow(2f, (color.b - num) * 0.5f) + gamma.a;
			float x = 1f / Mathf.Max(0.01f, b);
			float y = 1f / Mathf.Max(0.01f, b2);
			float z = 1f / Mathf.Max(0.01f, b3);
			return ColorGradingComponent.ClampVector(new Vector3(x, y, z), 0f, 5f);
		}

		// Token: 0x06001E64 RID: 7780 RVA: 0x0017B45C File Offset: 0x0017965C
		public static Vector3 GetGainValue(Color gain)
		{
			Color color = ColorGradingComponent.NormalizeColor(gain);
			float num = (color.r + color.g + color.b) / 3f;
			gain.a *= ((gain.a > 0f) ? 3f : 1f);
			float x = Mathf.Pow(2f, (color.r - num) * 0.5f) + gain.a;
			float y = Mathf.Pow(2f, (color.g - num) * 0.5f) + gain.a;
			float z = Mathf.Pow(2f, (color.b - num) * 0.5f) + gain.a;
			return ColorGradingComponent.ClampVector(new Vector3(x, y, z), 0f, 4f);
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x0017B524 File Offset: 0x00179724
		public static void CalculateLiftGammaGain(Color lift, Color gamma, Color gain, out Vector3 outLift, out Vector3 outGamma, out Vector3 outGain)
		{
			outLift = ColorGradingComponent.GetLiftValue(lift);
			outGamma = ColorGradingComponent.GetGammaValue(gamma);
			outGain = ColorGradingComponent.GetGainValue(gain);
		}

		// Token: 0x06001E66 RID: 7782 RVA: 0x0017B54C File Offset: 0x0017974C
		public static Vector3 GetSlopeValue(Color slope)
		{
			Color color = ColorGradingComponent.NormalizeColor(slope);
			float num = (color.r + color.g + color.b) / 3f;
			slope.a *= 0.5f;
			float x = (color.r - num) * 0.1f + slope.a + 1f;
			float y = (color.g - num) * 0.1f + slope.a + 1f;
			float z = (color.b - num) * 0.1f + slope.a + 1f;
			return ColorGradingComponent.ClampVector(new Vector3(x, y, z), 0f, 2f);
		}

		// Token: 0x06001E67 RID: 7783 RVA: 0x0017B5F4 File Offset: 0x001797F4
		public static Vector3 GetPowerValue(Color power)
		{
			Color color = ColorGradingComponent.NormalizeColor(power);
			float num = (color.r + color.g + color.b) / 3f;
			power.a *= 0.5f;
			float b = (color.r - num) * 0.1f + power.a + 1f;
			float b2 = (color.g - num) * 0.1f + power.a + 1f;
			float b3 = (color.b - num) * 0.1f + power.a + 1f;
			float x = 1f / Mathf.Max(0.01f, b);
			float y = 1f / Mathf.Max(0.01f, b2);
			float z = 1f / Mathf.Max(0.01f, b3);
			return ColorGradingComponent.ClampVector(new Vector3(x, y, z), 0.5f, 2.5f);
		}

		// Token: 0x06001E68 RID: 7784 RVA: 0x0017B6D8 File Offset: 0x001798D8
		public static Vector3 GetOffsetValue(Color offset)
		{
			Color color = ColorGradingComponent.NormalizeColor(offset);
			float num = (color.r + color.g + color.b) / 3f;
			offset.a *= 0.5f;
			float x = (color.r - num) * 0.05f + offset.a;
			float y = (color.g - num) * 0.05f + offset.a;
			float z = (color.b - num) * 0.05f + offset.a;
			return ColorGradingComponent.ClampVector(new Vector3(x, y, z), -0.8f, 0.8f);
		}

		// Token: 0x06001E69 RID: 7785 RVA: 0x0017B76E File Offset: 0x0017996E
		public static void CalculateSlopePowerOffset(Color slope, Color power, Color offset, out Vector3 outSlope, out Vector3 outPower, out Vector3 outOffset)
		{
			outSlope = ColorGradingComponent.GetSlopeValue(slope);
			outPower = ColorGradingComponent.GetPowerValue(power);
			outOffset = ColorGradingComponent.GetOffsetValue(offset);
		}

		// Token: 0x06001E6A RID: 7786 RVA: 0x0017B796 File Offset: 0x00179996
		private TextureFormat GetCurveFormat()
		{
			if (SystemInfo.SupportsTextureFormat(TextureFormat.RGBAHalf))
			{
				return TextureFormat.RGBAHalf;
			}
			return TextureFormat.RGBA32;
		}

		// Token: 0x06001E6B RID: 7787 RVA: 0x0017B7A8 File Offset: 0x001799A8
		private Texture2D GetCurveTexture()
		{
			if (this.m_GradingCurves == null)
			{
				this.m_GradingCurves = new Texture2D(128, 2, this.GetCurveFormat(), false, true)
				{
					name = "Internal Curves Texture",
					hideFlags = HideFlags.DontSave,
					anisoLevel = 0,
					wrapMode = TextureWrapMode.Clamp,
					filterMode = FilterMode.Bilinear
				};
			}
			ColorGradingModel.CurvesSettings curves = base.model.settings.curves;
			curves.hueVShue.Cache();
			curves.hueVSsat.Cache();
			for (int i = 0; i < 128; i++)
			{
				float t = (float)i * 0.0078125f;
				float r = curves.hueVShue.Evaluate(t);
				float g = curves.hueVSsat.Evaluate(t);
				float b = curves.satVSsat.Evaluate(t);
				float a = curves.lumVSsat.Evaluate(t);
				this.m_pixels[i] = new Color(r, g, b, a);
				float a2 = curves.master.Evaluate(t);
				float r2 = curves.red.Evaluate(t);
				float g2 = curves.green.Evaluate(t);
				float b2 = curves.blue.Evaluate(t);
				this.m_pixels[i + 128] = new Color(r2, g2, b2, a2);
			}
			this.m_GradingCurves.SetPixels(this.m_pixels);
			this.m_GradingCurves.Apply(false, false);
			return this.m_GradingCurves;
		}

		// Token: 0x06001E6C RID: 7788 RVA: 0x0017B914 File Offset: 0x00179B14
		private bool IsLogLutValid(RenderTexture lut)
		{
			return lut != null && lut.IsCreated() && lut.height == 32;
		}

		// Token: 0x06001E6D RID: 7789 RVA: 0x0017B933 File Offset: 0x00179B33
		private RenderTextureFormat GetLutFormat()
		{
			if (SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBHalf))
			{
				return RenderTextureFormat.ARGBHalf;
			}
			return RenderTextureFormat.ARGB32;
		}

		// Token: 0x06001E6E RID: 7790 RVA: 0x0017B940 File Offset: 0x00179B40
		private void GenerateLut()
		{
			ColorGradingModel.Settings settings = base.model.settings;
			if (!this.IsLogLutValid(base.model.bakedLut))
			{
				GraphicsUtils.Destroy(base.model.bakedLut);
				base.model.bakedLut = new RenderTexture(1024, 32, 0, this.GetLutFormat())
				{
					name = "Color Grading Log LUT",
					hideFlags = HideFlags.DontSave,
					filterMode = FilterMode.Bilinear,
					wrapMode = TextureWrapMode.Clamp,
					anisoLevel = 0
				};
			}
			Material material = this.context.materialFactory.Get("Hidden/Post FX/Lut Generator");
			material.SetVector(ColorGradingComponent.Uniforms._LutParams, new Vector4(32f, 0.00048828125f, 0.015625f, 1.032258f));
			material.shaderKeywords = null;
			ColorGradingModel.TonemappingSettings tonemapping = settings.tonemapping;
			ColorGradingModel.Tonemapper tonemapper = tonemapping.tonemapper;
			if (tonemapper != ColorGradingModel.Tonemapper.ACES)
			{
				if (tonemapper == ColorGradingModel.Tonemapper.Neutral)
				{
					material.EnableKeyword("TONEMAPPING_NEUTRAL");
					float num = tonemapping.neutralBlackIn * 20f + 1f;
					float num2 = tonemapping.neutralBlackOut * 10f + 1f;
					float num3 = tonemapping.neutralWhiteIn / 20f;
					float num4 = 1f - tonemapping.neutralWhiteOut / 20f;
					float t = num / num2;
					float t2 = num3 / num4;
					float y = Mathf.Max(0f, Mathf.LerpUnclamped(0.57f, 0.37f, t));
					float z = Mathf.LerpUnclamped(0.01f, 0.24f, t2);
					float w = Mathf.Max(0f, Mathf.LerpUnclamped(0.02f, 0.2f, t));
					material.SetVector(ColorGradingComponent.Uniforms._NeutralTonemapperParams1, new Vector4(0.2f, y, z, w));
					material.SetVector(ColorGradingComponent.Uniforms._NeutralTonemapperParams2, new Vector4(0.02f, 0.3f, tonemapping.neutralWhiteLevel, tonemapping.neutralWhiteClip / 10f));
				}
			}
			else
			{
				material.EnableKeyword("TONEMAPPING_FILMIC");
			}
			material.SetFloat(ColorGradingComponent.Uniforms._HueShift, settings.basic.hueShift / 360f);
			material.SetFloat(ColorGradingComponent.Uniforms._Saturation, settings.basic.saturation);
			material.SetFloat(ColorGradingComponent.Uniforms._Contrast, settings.basic.contrast);
			material.SetVector(ColorGradingComponent.Uniforms._Balance, this.CalculateColorBalance(settings.basic.temperature, settings.basic.tint));
			Vector3 v;
			Vector3 v2;
			Vector3 v3;
			ColorGradingComponent.CalculateLiftGammaGain(settings.colorWheels.linear.lift, settings.colorWheels.linear.gamma, settings.colorWheels.linear.gain, out v, out v2, out v3);
			material.SetVector(ColorGradingComponent.Uniforms._Lift, v);
			material.SetVector(ColorGradingComponent.Uniforms._InvGamma, v2);
			material.SetVector(ColorGradingComponent.Uniforms._Gain, v3);
			Vector3 v4;
			Vector3 v5;
			Vector3 v6;
			ColorGradingComponent.CalculateSlopePowerOffset(settings.colorWheels.log.slope, settings.colorWheels.log.power, settings.colorWheels.log.offset, out v4, out v5, out v6);
			material.SetVector(ColorGradingComponent.Uniforms._Slope, v4);
			material.SetVector(ColorGradingComponent.Uniforms._Power, v5);
			material.SetVector(ColorGradingComponent.Uniforms._Offset, v6);
			material.SetVector(ColorGradingComponent.Uniforms._ChannelMixerRed, settings.channelMixer.red);
			material.SetVector(ColorGradingComponent.Uniforms._ChannelMixerGreen, settings.channelMixer.green);
			material.SetVector(ColorGradingComponent.Uniforms._ChannelMixerBlue, settings.channelMixer.blue);
			material.SetTexture(ColorGradingComponent.Uniforms._Curves, this.GetCurveTexture());
			Graphics.Blit(null, base.model.bakedLut, material, 0);
		}

		// Token: 0x06001E6F RID: 7791 RVA: 0x0017BCEC File Offset: 0x00179EEC
		public override void Prepare(Material uberMaterial)
		{
			if (base.model.isDirty || !this.IsLogLutValid(base.model.bakedLut))
			{
				this.GenerateLut();
				base.model.isDirty = false;
			}
			uberMaterial.EnableKeyword(this.context.profile.debugViews.IsModeActive(BuiltinDebugViewsModel.Mode.PreGradingLog) ? "COLOR_GRADING_LOG_VIEW" : "COLOR_GRADING");
			RenderTexture bakedLut = base.model.bakedLut;
			uberMaterial.SetTexture(ColorGradingComponent.Uniforms._LogLut, bakedLut);
			uberMaterial.SetVector(ColorGradingComponent.Uniforms._LogLut_Params, new Vector3(1f / (float)bakedLut.width, 1f / (float)bakedLut.height, (float)bakedLut.height - 1f));
			float value = Mathf.Exp(base.model.settings.basic.postExposure * 0.6931472f);
			uberMaterial.SetFloat(ColorGradingComponent.Uniforms._ExposureEV, value);
		}

		// Token: 0x06001E70 RID: 7792 RVA: 0x0017BDD8 File Offset: 0x00179FD8
		public void OnGUI()
		{
			RenderTexture bakedLut = base.model.bakedLut;
			GUI.DrawTexture(new Rect(this.context.viewport.x * (float)Screen.width + 8f, 8f, (float)bakedLut.width, (float)bakedLut.height), bakedLut);
		}

		// Token: 0x06001E71 RID: 7793 RVA: 0x0017BE2F File Offset: 0x0017A02F
		public override void OnDisable()
		{
			GraphicsUtils.Destroy(this.m_GradingCurves);
			GraphicsUtils.Destroy(base.model.bakedLut);
			this.m_GradingCurves = null;
			base.model.bakedLut = null;
		}

		// Token: 0x04003C51 RID: 15441
		private const int k_InternalLogLutSize = 32;

		// Token: 0x04003C52 RID: 15442
		private const int k_CurvePrecision = 128;

		// Token: 0x04003C53 RID: 15443
		private const float k_CurveStep = 0.0078125f;

		// Token: 0x04003C54 RID: 15444
		private Texture2D m_GradingCurves;

		// Token: 0x04003C55 RID: 15445
		private Color[] m_pixels = new Color[256];

		// Token: 0x020006BC RID: 1724
		private static class Uniforms
		{
			// Token: 0x040046FA RID: 18170
			internal static readonly int _LutParams = Shader.PropertyToID("_LutParams");

			// Token: 0x040046FB RID: 18171
			internal static readonly int _NeutralTonemapperParams1 = Shader.PropertyToID("_NeutralTonemapperParams1");

			// Token: 0x040046FC RID: 18172
			internal static readonly int _NeutralTonemapperParams2 = Shader.PropertyToID("_NeutralTonemapperParams2");

			// Token: 0x040046FD RID: 18173
			internal static readonly int _HueShift = Shader.PropertyToID("_HueShift");

			// Token: 0x040046FE RID: 18174
			internal static readonly int _Saturation = Shader.PropertyToID("_Saturation");

			// Token: 0x040046FF RID: 18175
			internal static readonly int _Contrast = Shader.PropertyToID("_Contrast");

			// Token: 0x04004700 RID: 18176
			internal static readonly int _Balance = Shader.PropertyToID("_Balance");

			// Token: 0x04004701 RID: 18177
			internal static readonly int _Lift = Shader.PropertyToID("_Lift");

			// Token: 0x04004702 RID: 18178
			internal static readonly int _InvGamma = Shader.PropertyToID("_InvGamma");

			// Token: 0x04004703 RID: 18179
			internal static readonly int _Gain = Shader.PropertyToID("_Gain");

			// Token: 0x04004704 RID: 18180
			internal static readonly int _Slope = Shader.PropertyToID("_Slope");

			// Token: 0x04004705 RID: 18181
			internal static readonly int _Power = Shader.PropertyToID("_Power");

			// Token: 0x04004706 RID: 18182
			internal static readonly int _Offset = Shader.PropertyToID("_Offset");

			// Token: 0x04004707 RID: 18183
			internal static readonly int _ChannelMixerRed = Shader.PropertyToID("_ChannelMixerRed");

			// Token: 0x04004708 RID: 18184
			internal static readonly int _ChannelMixerGreen = Shader.PropertyToID("_ChannelMixerGreen");

			// Token: 0x04004709 RID: 18185
			internal static readonly int _ChannelMixerBlue = Shader.PropertyToID("_ChannelMixerBlue");

			// Token: 0x0400470A RID: 18186
			internal static readonly int _Curves = Shader.PropertyToID("_Curves");

			// Token: 0x0400470B RID: 18187
			internal static readonly int _LogLut = Shader.PropertyToID("_LogLut");

			// Token: 0x0400470C RID: 18188
			internal static readonly int _LogLut_Params = Shader.PropertyToID("_LogLut_Params");

			// Token: 0x0400470D RID: 18189
			internal static readonly int _ExposureEV = Shader.PropertyToID("_ExposureEV");
		}
	}
}
