﻿using System;
using UnityEngine.Rendering;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004B3 RID: 1203
	public sealed class FogComponent : PostProcessingComponentCommandBuffer<FogModel>
	{
		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001E89 RID: 7817 RVA: 0x0017C925 File Offset: 0x0017AB25
		public override bool active
		{
			get
			{
				return base.model.enabled && this.context.isGBufferAvailable && RenderSettings.fog && !this.context.interrupted;
			}
		}

		// Token: 0x06001E8A RID: 7818 RVA: 0x0017C958 File Offset: 0x0017AB58
		public override string GetName()
		{
			return "Fog";
		}

		// Token: 0x06001E8B RID: 7819 RVA: 0x0002291C File Offset: 0x00020B1C
		public override DepthTextureMode GetCameraFlags()
		{
			return DepthTextureMode.Depth;
		}

		// Token: 0x06001E8C RID: 7820 RVA: 0x0017C95F File Offset: 0x0017AB5F
		public override CameraEvent GetCameraEvent()
		{
			return CameraEvent.AfterImageEffectsOpaque;
		}

		// Token: 0x06001E8D RID: 7821 RVA: 0x0017C964 File Offset: 0x0017AB64
		public override void PopulateCommandBuffer(CommandBuffer cb)
		{
			FogModel.Settings settings = base.model.settings;
			Material material = this.context.materialFactory.Get("Hidden/Post FX/Fog");
			material.shaderKeywords = null;
			Color value = GraphicsUtils.isLinearColorSpace ? RenderSettings.fogColor.linear : RenderSettings.fogColor;
			material.SetColor(FogComponent.Uniforms._FogColor, value);
			material.SetFloat(FogComponent.Uniforms._Density, RenderSettings.fogDensity);
			material.SetFloat(FogComponent.Uniforms._Start, RenderSettings.fogStartDistance);
			material.SetFloat(FogComponent.Uniforms._End, RenderSettings.fogEndDistance);
			switch (RenderSettings.fogMode)
			{
			case FogMode.Linear:
				material.EnableKeyword("FOG_LINEAR");
				break;
			case FogMode.Exponential:
				material.EnableKeyword("FOG_EXP");
				break;
			case FogMode.ExponentialSquared:
				material.EnableKeyword("FOG_EXP2");
				break;
			}
			RenderTextureFormat format = this.context.isHdr ? RenderTextureFormat.DefaultHDR : RenderTextureFormat.Default;
			cb.GetTemporaryRT(FogComponent.Uniforms._TempRT, this.context.width, this.context.height, 24, FilterMode.Bilinear, format);
			cb.Blit(BuiltinRenderTextureType.CameraTarget, FogComponent.Uniforms._TempRT);
			cb.Blit(FogComponent.Uniforms._TempRT, BuiltinRenderTextureType.CameraTarget, material, settings.excludeSkybox ? 1 : 0);
			cb.ReleaseTemporaryRT(FogComponent.Uniforms._TempRT);
		}

		// Token: 0x04003C67 RID: 15463
		private const string k_ShaderString = "Hidden/Post FX/Fog";

		// Token: 0x020006C0 RID: 1728
		private static class Uniforms
		{
			// Token: 0x04004721 RID: 18209
			internal static readonly int _FogColor = Shader.PropertyToID("_FogColor");

			// Token: 0x04004722 RID: 18210
			internal static readonly int _Density = Shader.PropertyToID("_Density");

			// Token: 0x04004723 RID: 18211
			internal static readonly int _Start = Shader.PropertyToID("_Start");

			// Token: 0x04004724 RID: 18212
			internal static readonly int _End = Shader.PropertyToID("_End");

			// Token: 0x04004725 RID: 18213
			internal static readonly int _TempRT = Shader.PropertyToID("_TempRT");
		}
	}
}
