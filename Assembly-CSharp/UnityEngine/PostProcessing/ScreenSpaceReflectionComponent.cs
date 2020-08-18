using System;
using UnityEngine.Rendering;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004B7 RID: 1207
	public sealed class ScreenSpaceReflectionComponent : PostProcessingComponentCommandBuffer<ScreenSpaceReflectionModel>
	{
		// Token: 0x06001EA1 RID: 7841 RVA: 0x0002291C File Offset: 0x00020B1C
		public override DepthTextureMode GetCameraFlags()
		{
			return DepthTextureMode.Depth;
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06001EA2 RID: 7842 RVA: 0x0017D053 File Offset: 0x0017B253
		public override bool active
		{
			get
			{
				return base.model.enabled && this.context.isGBufferAvailable && !this.context.interrupted;
			}
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x0017D080 File Offset: 0x0017B280
		public override void OnEnable()
		{
			this.m_ReflectionTextures[0] = Shader.PropertyToID("_ReflectionTexture0");
			this.m_ReflectionTextures[1] = Shader.PropertyToID("_ReflectionTexture1");
			this.m_ReflectionTextures[2] = Shader.PropertyToID("_ReflectionTexture2");
			this.m_ReflectionTextures[3] = Shader.PropertyToID("_ReflectionTexture3");
			this.m_ReflectionTextures[4] = Shader.PropertyToID("_ReflectionTexture4");
		}

		// Token: 0x06001EA4 RID: 7844 RVA: 0x0017D0E7 File Offset: 0x0017B2E7
		public override string GetName()
		{
			return "Screen Space Reflection";
		}

		// Token: 0x06001EA5 RID: 7845 RVA: 0x0017D0EE File Offset: 0x0017B2EE
		public override CameraEvent GetCameraEvent()
		{
			return CameraEvent.AfterFinalPass;
		}

		// Token: 0x06001EA6 RID: 7846 RVA: 0x0017D0F4 File Offset: 0x0017B2F4
		public override void PopulateCommandBuffer(CommandBuffer cb)
		{
			ScreenSpaceReflectionModel.Settings settings = base.model.settings;
			Camera camera = this.context.camera;
			int num = (settings.reflection.reflectionQuality == ScreenSpaceReflectionModel.SSRResolution.High) ? 1 : 2;
			int num2 = this.context.width / num;
			int num3 = this.context.height / num;
			float num4 = (float)this.context.width;
			float num5 = (float)this.context.height;
			float num6 = num4 / 2f;
			float num7 = num5 / 2f;
			Material material = this.context.materialFactory.Get("Hidden/Post FX/Screen Space Reflection");
			material.SetInt(ScreenSpaceReflectionComponent.Uniforms._RayStepSize, settings.reflection.stepSize);
			material.SetInt(ScreenSpaceReflectionComponent.Uniforms._AdditiveReflection, (settings.reflection.blendType == ScreenSpaceReflectionModel.SSRReflectionBlendType.Additive) ? 1 : 0);
			material.SetInt(ScreenSpaceReflectionComponent.Uniforms._BilateralUpsampling, this.k_BilateralUpsample ? 1 : 0);
			material.SetInt(ScreenSpaceReflectionComponent.Uniforms._TreatBackfaceHitAsMiss, this.k_TreatBackfaceHitAsMiss ? 1 : 0);
			material.SetInt(ScreenSpaceReflectionComponent.Uniforms._AllowBackwardsRays, settings.reflection.reflectBackfaces ? 1 : 0);
			material.SetInt(ScreenSpaceReflectionComponent.Uniforms._TraceBehindObjects, this.k_TraceBehindObjects ? 1 : 0);
			material.SetInt(ScreenSpaceReflectionComponent.Uniforms._MaxSteps, settings.reflection.iterationCount);
			material.SetInt(ScreenSpaceReflectionComponent.Uniforms._FullResolutionFiltering, 0);
			material.SetInt(ScreenSpaceReflectionComponent.Uniforms._HalfResolution, (settings.reflection.reflectionQuality != ScreenSpaceReflectionModel.SSRResolution.High) ? 1 : 0);
			material.SetInt(ScreenSpaceReflectionComponent.Uniforms._HighlightSuppression, this.k_HighlightSuppression ? 1 : 0);
			float value = num4 / (-2f * Mathf.Tan(camera.fieldOfView / 180f * 3.1415927f * 0.5f));
			material.SetFloat(ScreenSpaceReflectionComponent.Uniforms._PixelsPerMeterAtOneMeter, value);
			material.SetFloat(ScreenSpaceReflectionComponent.Uniforms._ScreenEdgeFading, settings.screenEdgeMask.intensity);
			material.SetFloat(ScreenSpaceReflectionComponent.Uniforms._ReflectionBlur, settings.reflection.reflectionBlur);
			material.SetFloat(ScreenSpaceReflectionComponent.Uniforms._MaxRayTraceDistance, settings.reflection.maxDistance);
			material.SetFloat(ScreenSpaceReflectionComponent.Uniforms._FadeDistance, settings.intensity.fadeDistance);
			material.SetFloat(ScreenSpaceReflectionComponent.Uniforms._LayerThickness, settings.reflection.widthModifier);
			material.SetFloat(ScreenSpaceReflectionComponent.Uniforms._SSRMultiplier, settings.intensity.reflectionMultiplier);
			material.SetFloat(ScreenSpaceReflectionComponent.Uniforms._FresnelFade, settings.intensity.fresnelFade);
			material.SetFloat(ScreenSpaceReflectionComponent.Uniforms._FresnelFadePower, settings.intensity.fresnelFadePower);
			Matrix4x4 projectionMatrix = camera.projectionMatrix;
			Vector4 value2 = new Vector4(-2f / (num4 * projectionMatrix[0]), -2f / (num5 * projectionMatrix[5]), (1f - projectionMatrix[2]) / projectionMatrix[0], (1f + projectionMatrix[6]) / projectionMatrix[5]);
			Vector3 v = float.IsPositiveInfinity(camera.farClipPlane) ? new Vector3(camera.nearClipPlane, -1f, 1f) : new Vector3(camera.nearClipPlane * camera.farClipPlane, camera.nearClipPlane - camera.farClipPlane, camera.farClipPlane);
			material.SetVector(ScreenSpaceReflectionComponent.Uniforms._ReflectionBufferSize, new Vector2((float)num2, (float)num3));
			material.SetVector(ScreenSpaceReflectionComponent.Uniforms._ScreenSize, new Vector2(num4, num5));
			material.SetVector(ScreenSpaceReflectionComponent.Uniforms._InvScreenSize, new Vector2(1f / num4, 1f / num5));
			material.SetVector(ScreenSpaceReflectionComponent.Uniforms._ProjInfo, value2);
			material.SetVector(ScreenSpaceReflectionComponent.Uniforms._CameraClipInfo, v);
			Matrix4x4 lhs = default(Matrix4x4);
			lhs.SetRow(0, new Vector4(num6, 0f, 0f, num6));
			lhs.SetRow(1, new Vector4(0f, num7, 0f, num7));
			lhs.SetRow(2, new Vector4(0f, 0f, 1f, 0f));
			lhs.SetRow(3, new Vector4(0f, 0f, 0f, 1f));
			Matrix4x4 value3 = lhs * projectionMatrix;
			material.SetMatrix(ScreenSpaceReflectionComponent.Uniforms._ProjectToPixelMatrix, value3);
			material.SetMatrix(ScreenSpaceReflectionComponent.Uniforms._WorldToCameraMatrix, camera.worldToCameraMatrix);
			material.SetMatrix(ScreenSpaceReflectionComponent.Uniforms._CameraToWorldMatrix, camera.worldToCameraMatrix.inverse);
			RenderTextureFormat format = this.context.isHdr ? RenderTextureFormat.ARGBHalf : RenderTextureFormat.ARGB32;
			int normalAndRoughnessTexture = ScreenSpaceReflectionComponent.Uniforms._NormalAndRoughnessTexture;
			int hitPointTexture = ScreenSpaceReflectionComponent.Uniforms._HitPointTexture;
			int blurTexture = ScreenSpaceReflectionComponent.Uniforms._BlurTexture;
			int filteredReflections = ScreenSpaceReflectionComponent.Uniforms._FilteredReflections;
			int finalReflectionTexture = ScreenSpaceReflectionComponent.Uniforms._FinalReflectionTexture;
			int tempTexture = ScreenSpaceReflectionComponent.Uniforms._TempTexture;
			cb.GetTemporaryRT(normalAndRoughnessTexture, -1, -1, 0, FilterMode.Point, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
			cb.GetTemporaryRT(hitPointTexture, num2, num3, 0, FilterMode.Bilinear, RenderTextureFormat.ARGBHalf, RenderTextureReadWrite.Linear);
			for (int i = 0; i < 5; i++)
			{
				cb.GetTemporaryRT(this.m_ReflectionTextures[i], num2 >> i, num3 >> i, 0, FilterMode.Bilinear, format);
			}
			cb.GetTemporaryRT(filteredReflections, num2, num3, 0, this.k_BilateralUpsample ? FilterMode.Point : FilterMode.Bilinear, format);
			cb.GetTemporaryRT(finalReflectionTexture, num2, num3, 0, FilterMode.Point, format);
			cb.Blit(BuiltinRenderTextureType.CameraTarget, normalAndRoughnessTexture, material, 6);
			cb.Blit(BuiltinRenderTextureType.CameraTarget, hitPointTexture, material, 0);
			cb.Blit(BuiltinRenderTextureType.CameraTarget, filteredReflections, material, 5);
			cb.Blit(filteredReflections, this.m_ReflectionTextures[0], material, 8);
			for (int j = 1; j < 5; j++)
			{
				int nameID = this.m_ReflectionTextures[j - 1];
				int num8 = j;
				cb.GetTemporaryRT(blurTexture, num2 >> num8, num3 >> num8, 0, FilterMode.Bilinear, format);
				cb.SetGlobalVector(ScreenSpaceReflectionComponent.Uniforms._Axis, new Vector4(1f, 0f, 0f, 0f));
				cb.SetGlobalFloat(ScreenSpaceReflectionComponent.Uniforms._CurrentMipLevel, (float)j - 1f);
				cb.Blit(nameID, blurTexture, material, 2);
				cb.SetGlobalVector(ScreenSpaceReflectionComponent.Uniforms._Axis, new Vector4(0f, 1f, 0f, 0f));
				nameID = this.m_ReflectionTextures[j];
				cb.Blit(blurTexture, nameID, material, 2);
				cb.ReleaseTemporaryRT(blurTexture);
			}
			cb.Blit(this.m_ReflectionTextures[0], finalReflectionTexture, material, 3);
			cb.GetTemporaryRT(tempTexture, camera.pixelWidth, camera.pixelHeight, 0, FilterMode.Bilinear, format);
			cb.Blit(BuiltinRenderTextureType.CameraTarget, tempTexture, material, 1);
			cb.Blit(tempTexture, BuiltinRenderTextureType.CameraTarget);
			cb.ReleaseTemporaryRT(tempTexture);
		}

		// Token: 0x04003C6C RID: 15468
		private bool k_HighlightSuppression;

		// Token: 0x04003C6D RID: 15469
		private bool k_TraceBehindObjects = true;

		// Token: 0x04003C6E RID: 15470
		private bool k_TreatBackfaceHitAsMiss;

		// Token: 0x04003C6F RID: 15471
		private bool k_BilateralUpsample = true;

		// Token: 0x04003C70 RID: 15472
		private readonly int[] m_ReflectionTextures = new int[5];

		// Token: 0x020006C7 RID: 1735
		private static class Uniforms
		{
			// Token: 0x04004756 RID: 18262
			internal static readonly int _RayStepSize = Shader.PropertyToID("_RayStepSize");

			// Token: 0x04004757 RID: 18263
			internal static readonly int _AdditiveReflection = Shader.PropertyToID("_AdditiveReflection");

			// Token: 0x04004758 RID: 18264
			internal static readonly int _BilateralUpsampling = Shader.PropertyToID("_BilateralUpsampling");

			// Token: 0x04004759 RID: 18265
			internal static readonly int _TreatBackfaceHitAsMiss = Shader.PropertyToID("_TreatBackfaceHitAsMiss");

			// Token: 0x0400475A RID: 18266
			internal static readonly int _AllowBackwardsRays = Shader.PropertyToID("_AllowBackwardsRays");

			// Token: 0x0400475B RID: 18267
			internal static readonly int _TraceBehindObjects = Shader.PropertyToID("_TraceBehindObjects");

			// Token: 0x0400475C RID: 18268
			internal static readonly int _MaxSteps = Shader.PropertyToID("_MaxSteps");

			// Token: 0x0400475D RID: 18269
			internal static readonly int _FullResolutionFiltering = Shader.PropertyToID("_FullResolutionFiltering");

			// Token: 0x0400475E RID: 18270
			internal static readonly int _HalfResolution = Shader.PropertyToID("_HalfResolution");

			// Token: 0x0400475F RID: 18271
			internal static readonly int _HighlightSuppression = Shader.PropertyToID("_HighlightSuppression");

			// Token: 0x04004760 RID: 18272
			internal static readonly int _PixelsPerMeterAtOneMeter = Shader.PropertyToID("_PixelsPerMeterAtOneMeter");

			// Token: 0x04004761 RID: 18273
			internal static readonly int _ScreenEdgeFading = Shader.PropertyToID("_ScreenEdgeFading");

			// Token: 0x04004762 RID: 18274
			internal static readonly int _ReflectionBlur = Shader.PropertyToID("_ReflectionBlur");

			// Token: 0x04004763 RID: 18275
			internal static readonly int _MaxRayTraceDistance = Shader.PropertyToID("_MaxRayTraceDistance");

			// Token: 0x04004764 RID: 18276
			internal static readonly int _FadeDistance = Shader.PropertyToID("_FadeDistance");

			// Token: 0x04004765 RID: 18277
			internal static readonly int _LayerThickness = Shader.PropertyToID("_LayerThickness");

			// Token: 0x04004766 RID: 18278
			internal static readonly int _SSRMultiplier = Shader.PropertyToID("_SSRMultiplier");

			// Token: 0x04004767 RID: 18279
			internal static readonly int _FresnelFade = Shader.PropertyToID("_FresnelFade");

			// Token: 0x04004768 RID: 18280
			internal static readonly int _FresnelFadePower = Shader.PropertyToID("_FresnelFadePower");

			// Token: 0x04004769 RID: 18281
			internal static readonly int _ReflectionBufferSize = Shader.PropertyToID("_ReflectionBufferSize");

			// Token: 0x0400476A RID: 18282
			internal static readonly int _ScreenSize = Shader.PropertyToID("_ScreenSize");

			// Token: 0x0400476B RID: 18283
			internal static readonly int _InvScreenSize = Shader.PropertyToID("_InvScreenSize");

			// Token: 0x0400476C RID: 18284
			internal static readonly int _ProjInfo = Shader.PropertyToID("_ProjInfo");

			// Token: 0x0400476D RID: 18285
			internal static readonly int _CameraClipInfo = Shader.PropertyToID("_CameraClipInfo");

			// Token: 0x0400476E RID: 18286
			internal static readonly int _ProjectToPixelMatrix = Shader.PropertyToID("_ProjectToPixelMatrix");

			// Token: 0x0400476F RID: 18287
			internal static readonly int _WorldToCameraMatrix = Shader.PropertyToID("_WorldToCameraMatrix");

			// Token: 0x04004770 RID: 18288
			internal static readonly int _CameraToWorldMatrix = Shader.PropertyToID("_CameraToWorldMatrix");

			// Token: 0x04004771 RID: 18289
			internal static readonly int _Axis = Shader.PropertyToID("_Axis");

			// Token: 0x04004772 RID: 18290
			internal static readonly int _CurrentMipLevel = Shader.PropertyToID("_CurrentMipLevel");

			// Token: 0x04004773 RID: 18291
			internal static readonly int _NormalAndRoughnessTexture = Shader.PropertyToID("_NormalAndRoughnessTexture");

			// Token: 0x04004774 RID: 18292
			internal static readonly int _HitPointTexture = Shader.PropertyToID("_HitPointTexture");

			// Token: 0x04004775 RID: 18293
			internal static readonly int _BlurTexture = Shader.PropertyToID("_BlurTexture");

			// Token: 0x04004776 RID: 18294
			internal static readonly int _FilteredReflections = Shader.PropertyToID("_FilteredReflections");

			// Token: 0x04004777 RID: 18295
			internal static readonly int _FinalReflectionTexture = Shader.PropertyToID("_FinalReflectionTexture");

			// Token: 0x04004778 RID: 18296
			internal static readonly int _TempTexture = Shader.PropertyToID("_TempTexture");
		}

		// Token: 0x020006C8 RID: 1736
		private enum PassIndex
		{
			// Token: 0x0400477A RID: 18298
			RayTraceStep,
			// Token: 0x0400477B RID: 18299
			CompositeFinal,
			// Token: 0x0400477C RID: 18300
			Blur,
			// Token: 0x0400477D RID: 18301
			CompositeSSR,
			// Token: 0x0400477E RID: 18302
			MinMipGeneration,
			// Token: 0x0400477F RID: 18303
			HitPointToReflections,
			// Token: 0x04004780 RID: 18304
			BilateralKeyPack,
			// Token: 0x04004781 RID: 18305
			BlitDepthAsCSZ,
			// Token: 0x04004782 RID: 18306
			PoissonBlur
		}
	}
}
