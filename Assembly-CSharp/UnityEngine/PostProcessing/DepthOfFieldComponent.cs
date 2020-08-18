using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004B0 RID: 1200
	public sealed class DepthOfFieldComponent : PostProcessingComponentRenderTexture<DepthOfFieldModel>
	{
		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06001E73 RID: 7795 RVA: 0x0017BE77 File Offset: 0x0017A077
		public override bool active
		{
			get
			{
				return base.model.enabled && !this.context.interrupted;
			}
		}

		// Token: 0x06001E74 RID: 7796 RVA: 0x0002291C File Offset: 0x00020B1C
		public override DepthTextureMode GetCameraFlags()
		{
			return DepthTextureMode.Depth;
		}

		// Token: 0x06001E75 RID: 7797 RVA: 0x0017BE98 File Offset: 0x0017A098
		private float CalculateFocalLength()
		{
			DepthOfFieldModel.Settings settings = base.model.settings;
			if (!settings.useCameraFov)
			{
				return settings.focalLength / 1000f;
			}
			float num = this.context.camera.fieldOfView * 0.017453292f;
			return 0.012f / Mathf.Tan(0.5f * num);
		}

		// Token: 0x06001E76 RID: 7798 RVA: 0x0017BEF0 File Offset: 0x0017A0F0
		private float CalculateMaxCoCRadius(int screenHeight)
		{
			float num = (float)base.model.settings.kernelSize * 4f + 6f;
			return Mathf.Min(0.05f, num / (float)screenHeight);
		}

		// Token: 0x06001E77 RID: 7799 RVA: 0x0017BF29 File Offset: 0x0017A129
		private bool CheckHistory(int width, int height)
		{
			return this.m_CoCHistory != null && this.m_CoCHistory.IsCreated() && this.m_CoCHistory.width == width && this.m_CoCHistory.height == height;
		}

		// Token: 0x06001E78 RID: 7800 RVA: 0x0017BF64 File Offset: 0x0017A164
		private RenderTextureFormat SelectFormat(RenderTextureFormat primary, RenderTextureFormat secondary)
		{
			if (SystemInfo.SupportsRenderTextureFormat(primary))
			{
				return primary;
			}
			if (SystemInfo.SupportsRenderTextureFormat(secondary))
			{
				return secondary;
			}
			return RenderTextureFormat.Default;
		}

		// Token: 0x06001E79 RID: 7801 RVA: 0x0017BF7C File Offset: 0x0017A17C
		public void Prepare(RenderTexture source, Material uberMaterial, bool antialiasCoC, Vector2 taaJitter, float taaBlending)
		{
			DepthOfFieldModel.Settings settings = base.model.settings;
			RenderTextureFormat format = RenderTextureFormat.DefaultHDR;
			RenderTextureFormat format2 = this.SelectFormat(RenderTextureFormat.R8, RenderTextureFormat.RHalf);
			float num = this.CalculateFocalLength();
			float num2 = Mathf.Max(settings.focusDistance, num);
			float num3 = (float)source.width / (float)source.height;
			float num4 = num * num / (settings.aperture * (num2 - num) * 0.024f * 2f);
			float num5 = this.CalculateMaxCoCRadius(source.height);
			Material material = this.context.materialFactory.Get("Hidden/Post FX/Depth Of Field");
			material.SetFloat(DepthOfFieldComponent.Uniforms._Distance, num2);
			material.SetFloat(DepthOfFieldComponent.Uniforms._LensCoeff, num4);
			material.SetFloat(DepthOfFieldComponent.Uniforms._MaxCoC, num5);
			material.SetFloat(DepthOfFieldComponent.Uniforms._RcpMaxCoC, 1f / num5);
			material.SetFloat(DepthOfFieldComponent.Uniforms._RcpAspect, 1f / num3);
			RenderTexture renderTexture = this.context.renderTextureFactory.Get(this.context.width, this.context.height, 0, format2, RenderTextureReadWrite.Linear, FilterMode.Bilinear, TextureWrapMode.Clamp, "FactoryTempTexture");
			Graphics.Blit(null, renderTexture, material, 0);
			if (antialiasCoC)
			{
				material.SetTexture(DepthOfFieldComponent.Uniforms._CoCTex, renderTexture);
				float z = this.CheckHistory(this.context.width, this.context.height) ? taaBlending : 0f;
				material.SetVector(DepthOfFieldComponent.Uniforms._TaaParams, new Vector3(taaJitter.x, taaJitter.y, z));
				RenderTexture temporary = RenderTexture.GetTemporary(this.context.width, this.context.height, 0, format2);
				Graphics.Blit(this.m_CoCHistory, temporary, material, 1);
				this.context.renderTextureFactory.Release(renderTexture);
				if (this.m_CoCHistory != null)
				{
					RenderTexture.ReleaseTemporary(this.m_CoCHistory);
				}
				renderTexture = (this.m_CoCHistory = temporary);
			}
			RenderTexture renderTexture2 = this.context.renderTextureFactory.Get(this.context.width / 2, this.context.height / 2, 0, format, RenderTextureReadWrite.Default, FilterMode.Bilinear, TextureWrapMode.Clamp, "FactoryTempTexture");
			material.SetTexture(DepthOfFieldComponent.Uniforms._CoCTex, renderTexture);
			Graphics.Blit(source, renderTexture2, material, 2);
			RenderTexture renderTexture3 = this.context.renderTextureFactory.Get(this.context.width / 2, this.context.height / 2, 0, format, RenderTextureReadWrite.Default, FilterMode.Bilinear, TextureWrapMode.Clamp, "FactoryTempTexture");
			Graphics.Blit(renderTexture2, renderTexture3, material, (int)(3 + settings.kernelSize));
			Graphics.Blit(renderTexture3, renderTexture2, material, 7);
			uberMaterial.SetVector(DepthOfFieldComponent.Uniforms._DepthOfFieldParams, new Vector3(num2, num4, num5));
			if (this.context.profile.debugViews.IsModeActive(BuiltinDebugViewsModel.Mode.FocusPlane))
			{
				uberMaterial.EnableKeyword("DEPTH_OF_FIELD_COC_VIEW");
				this.context.Interrupt();
			}
			else
			{
				uberMaterial.SetTexture(DepthOfFieldComponent.Uniforms._DepthOfFieldTex, renderTexture2);
				uberMaterial.SetTexture(DepthOfFieldComponent.Uniforms._DepthOfFieldCoCTex, renderTexture);
				uberMaterial.EnableKeyword("DEPTH_OF_FIELD");
			}
			this.context.renderTextureFactory.Release(renderTexture3);
		}

		// Token: 0x06001E7A RID: 7802 RVA: 0x0017C288 File Offset: 0x0017A488
		public override void OnDisable()
		{
			if (this.m_CoCHistory != null)
			{
				RenderTexture.ReleaseTemporary(this.m_CoCHistory);
			}
			this.m_CoCHistory = null;
		}

		// Token: 0x04003C56 RID: 15446
		private const string k_ShaderString = "Hidden/Post FX/Depth Of Field";

		// Token: 0x04003C57 RID: 15447
		private RenderTexture m_CoCHistory;

		// Token: 0x04003C58 RID: 15448
		private const float k_FilmHeight = 0.024f;

		// Token: 0x020006BD RID: 1725
		private static class Uniforms
		{
			// Token: 0x0400470E RID: 18190
			internal static readonly int _DepthOfFieldTex = Shader.PropertyToID("_DepthOfFieldTex");

			// Token: 0x0400470F RID: 18191
			internal static readonly int _DepthOfFieldCoCTex = Shader.PropertyToID("_DepthOfFieldCoCTex");

			// Token: 0x04004710 RID: 18192
			internal static readonly int _Distance = Shader.PropertyToID("_Distance");

			// Token: 0x04004711 RID: 18193
			internal static readonly int _LensCoeff = Shader.PropertyToID("_LensCoeff");

			// Token: 0x04004712 RID: 18194
			internal static readonly int _MaxCoC = Shader.PropertyToID("_MaxCoC");

			// Token: 0x04004713 RID: 18195
			internal static readonly int _RcpMaxCoC = Shader.PropertyToID("_RcpMaxCoC");

			// Token: 0x04004714 RID: 18196
			internal static readonly int _RcpAspect = Shader.PropertyToID("_RcpAspect");

			// Token: 0x04004715 RID: 18197
			internal static readonly int _MainTex = Shader.PropertyToID("_MainTex");

			// Token: 0x04004716 RID: 18198
			internal static readonly int _CoCTex = Shader.PropertyToID("_CoCTex");

			// Token: 0x04004717 RID: 18199
			internal static readonly int _TaaParams = Shader.PropertyToID("_TaaParams");

			// Token: 0x04004718 RID: 18200
			internal static readonly int _DepthOfFieldParams = Shader.PropertyToID("_DepthOfFieldParams");
		}
	}
}
