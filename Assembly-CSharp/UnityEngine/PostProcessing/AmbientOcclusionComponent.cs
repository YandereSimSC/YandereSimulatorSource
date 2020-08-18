using System;
using UnityEngine.Rendering;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004AB RID: 1195
	public sealed class AmbientOcclusionComponent : PostProcessingComponentCommandBuffer<AmbientOcclusionModel>
	{
		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001E42 RID: 7746 RVA: 0x0017A294 File Offset: 0x00178494
		private AmbientOcclusionComponent.OcclusionSource occlusionSource
		{
			get
			{
				if (this.context.isGBufferAvailable && !base.model.settings.forceForwardCompatibility)
				{
					return AmbientOcclusionComponent.OcclusionSource.GBuffer;
				}
				if (base.model.settings.highPrecision && (!this.context.isGBufferAvailable || base.model.settings.forceForwardCompatibility))
				{
					return AmbientOcclusionComponent.OcclusionSource.DepthTexture;
				}
				return AmbientOcclusionComponent.OcclusionSource.DepthNormalsTexture;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06001E43 RID: 7747 RVA: 0x0017A2F8 File Offset: 0x001784F8
		private bool ambientOnlySupported
		{
			get
			{
				return this.context.isHdr && base.model.settings.ambientOnly && this.context.isGBufferAvailable && !base.model.settings.forceForwardCompatibility;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06001E44 RID: 7748 RVA: 0x0017A346 File Offset: 0x00178546
		public override bool active
		{
			get
			{
				return base.model.enabled && base.model.settings.intensity > 0f && !this.context.interrupted;
			}
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x0017A37C File Offset: 0x0017857C
		public override DepthTextureMode GetCameraFlags()
		{
			DepthTextureMode depthTextureMode = DepthTextureMode.None;
			if (this.occlusionSource == AmbientOcclusionComponent.OcclusionSource.DepthTexture)
			{
				depthTextureMode |= DepthTextureMode.Depth;
			}
			if (this.occlusionSource != AmbientOcclusionComponent.OcclusionSource.GBuffer)
			{
				depthTextureMode |= DepthTextureMode.DepthNormals;
			}
			return depthTextureMode;
		}

		// Token: 0x06001E46 RID: 7750 RVA: 0x0017A3A5 File Offset: 0x001785A5
		public override string GetName()
		{
			return "Ambient Occlusion";
		}

		// Token: 0x06001E47 RID: 7751 RVA: 0x0017A3AC File Offset: 0x001785AC
		public override CameraEvent GetCameraEvent()
		{
			if (!this.ambientOnlySupported || this.context.profile.debugViews.IsModeActive(BuiltinDebugViewsModel.Mode.AmbientOcclusion))
			{
				return CameraEvent.BeforeImageEffectsOpaque;
			}
			return CameraEvent.BeforeReflections;
		}

		// Token: 0x06001E48 RID: 7752 RVA: 0x0017A3D4 File Offset: 0x001785D4
		public override void PopulateCommandBuffer(CommandBuffer cb)
		{
			AmbientOcclusionModel.Settings settings = base.model.settings;
			Material mat = this.context.materialFactory.Get("Hidden/Post FX/Blit");
			Material material = this.context.materialFactory.Get("Hidden/Post FX/Ambient Occlusion");
			material.shaderKeywords = null;
			material.SetFloat(AmbientOcclusionComponent.Uniforms._Intensity, settings.intensity);
			material.SetFloat(AmbientOcclusionComponent.Uniforms._Radius, settings.radius);
			material.SetFloat(AmbientOcclusionComponent.Uniforms._Downsample, settings.downsampling ? 0.5f : 1f);
			material.SetInt(AmbientOcclusionComponent.Uniforms._SampleCount, (int)settings.sampleCount);
			if (!this.context.isGBufferAvailable && RenderSettings.fog)
			{
				material.SetVector(AmbientOcclusionComponent.Uniforms._FogParams, new Vector3(RenderSettings.fogDensity, RenderSettings.fogStartDistance, RenderSettings.fogEndDistance));
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
			}
			else
			{
				material.EnableKeyword("FOG_OFF");
			}
			int width = this.context.width;
			int height = this.context.height;
			int num = settings.downsampling ? 2 : 1;
			int nameID = AmbientOcclusionComponent.Uniforms._OcclusionTexture1;
			cb.GetTemporaryRT(nameID, width / num, height / num, 0, FilterMode.Bilinear, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
			cb.Blit(null, nameID, material, (int)this.occlusionSource);
			int occlusionTexture = AmbientOcclusionComponent.Uniforms._OcclusionTexture2;
			cb.GetTemporaryRT(occlusionTexture, width, height, 0, FilterMode.Bilinear, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
			cb.SetGlobalTexture(AmbientOcclusionComponent.Uniforms._MainTex, nameID);
			cb.Blit(nameID, occlusionTexture, material, (this.occlusionSource == AmbientOcclusionComponent.OcclusionSource.GBuffer) ? 4 : 3);
			cb.ReleaseTemporaryRT(nameID);
			nameID = AmbientOcclusionComponent.Uniforms._OcclusionTexture;
			cb.GetTemporaryRT(nameID, width, height, 0, FilterMode.Bilinear, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
			cb.SetGlobalTexture(AmbientOcclusionComponent.Uniforms._MainTex, occlusionTexture);
			cb.Blit(occlusionTexture, nameID, material, 5);
			cb.ReleaseTemporaryRT(occlusionTexture);
			if (this.context.profile.debugViews.IsModeActive(BuiltinDebugViewsModel.Mode.AmbientOcclusion))
			{
				cb.SetGlobalTexture(AmbientOcclusionComponent.Uniforms._MainTex, nameID);
				cb.Blit(nameID, BuiltinRenderTextureType.CameraTarget, material, 8);
				this.context.Interrupt();
			}
			else if (this.ambientOnlySupported)
			{
				cb.SetRenderTarget(this.m_MRT, BuiltinRenderTextureType.CameraTarget);
				cb.DrawMesh(GraphicsUtils.quad, Matrix4x4.identity, material, 0, 7);
			}
			else
			{
				RenderTextureFormat format = this.context.isHdr ? RenderTextureFormat.DefaultHDR : RenderTextureFormat.Default;
				int tempRT = AmbientOcclusionComponent.Uniforms._TempRT;
				cb.GetTemporaryRT(tempRT, this.context.width, this.context.height, 0, FilterMode.Bilinear, format);
				cb.Blit(BuiltinRenderTextureType.CameraTarget, tempRT, mat, 0);
				cb.SetGlobalTexture(AmbientOcclusionComponent.Uniforms._MainTex, tempRT);
				cb.Blit(tempRT, BuiltinRenderTextureType.CameraTarget, material, 6);
				cb.ReleaseTemporaryRT(tempRT);
			}
			cb.ReleaseTemporaryRT(nameID);
		}

		// Token: 0x04003C48 RID: 15432
		private const string k_BlitShaderString = "Hidden/Post FX/Blit";

		// Token: 0x04003C49 RID: 15433
		private const string k_ShaderString = "Hidden/Post FX/Ambient Occlusion";

		// Token: 0x04003C4A RID: 15434
		private readonly RenderTargetIdentifier[] m_MRT = new RenderTargetIdentifier[]
		{
			BuiltinRenderTextureType.GBuffer0,
			BuiltinRenderTextureType.CameraTarget
		};

		// Token: 0x020006B5 RID: 1717
		private static class Uniforms
		{
			// Token: 0x040046D0 RID: 18128
			internal static readonly int _Intensity = Shader.PropertyToID("_Intensity");

			// Token: 0x040046D1 RID: 18129
			internal static readonly int _Radius = Shader.PropertyToID("_Radius");

			// Token: 0x040046D2 RID: 18130
			internal static readonly int _FogParams = Shader.PropertyToID("_FogParams");

			// Token: 0x040046D3 RID: 18131
			internal static readonly int _Downsample = Shader.PropertyToID("_Downsample");

			// Token: 0x040046D4 RID: 18132
			internal static readonly int _SampleCount = Shader.PropertyToID("_SampleCount");

			// Token: 0x040046D5 RID: 18133
			internal static readonly int _OcclusionTexture1 = Shader.PropertyToID("_OcclusionTexture1");

			// Token: 0x040046D6 RID: 18134
			internal static readonly int _OcclusionTexture2 = Shader.PropertyToID("_OcclusionTexture2");

			// Token: 0x040046D7 RID: 18135
			internal static readonly int _OcclusionTexture = Shader.PropertyToID("_OcclusionTexture");

			// Token: 0x040046D8 RID: 18136
			internal static readonly int _MainTex = Shader.PropertyToID("_MainTex");

			// Token: 0x040046D9 RID: 18137
			internal static readonly int _TempRT = Shader.PropertyToID("_TempRT");
		}

		// Token: 0x020006B6 RID: 1718
		private enum OcclusionSource
		{
			// Token: 0x040046DB RID: 18139
			DepthTexture,
			// Token: 0x040046DC RID: 18140
			DepthNormalsTexture,
			// Token: 0x040046DD RID: 18141
			GBuffer
		}
	}
}
