using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004B5 RID: 1205
	public sealed class GrainComponent : PostProcessingComponentRenderTexture<GrainModel>
	{
		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001E92 RID: 7826 RVA: 0x0017CBA2 File Offset: 0x0017ADA2
		public override bool active
		{
			get
			{
				return base.model.enabled && base.model.settings.intensity > 0f && SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBHalf) && !this.context.interrupted;
			}
		}

		// Token: 0x06001E93 RID: 7827 RVA: 0x0017CBE0 File Offset: 0x0017ADE0
		public override void OnDisable()
		{
			GraphicsUtils.Destroy(this.m_GrainLookupRT);
			this.m_GrainLookupRT = null;
		}

		// Token: 0x06001E94 RID: 7828 RVA: 0x0017CBF4 File Offset: 0x0017ADF4
		public override void Prepare(Material uberMaterial)
		{
			GrainModel.Settings settings = base.model.settings;
			uberMaterial.EnableKeyword("GRAIN");
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			float value = Random.value;
			float value2 = Random.value;
			if (this.m_GrainLookupRT == null || !this.m_GrainLookupRT.IsCreated())
			{
				GraphicsUtils.Destroy(this.m_GrainLookupRT);
				this.m_GrainLookupRT = new RenderTexture(192, 192, 0, RenderTextureFormat.ARGBHalf)
				{
					filterMode = FilterMode.Bilinear,
					wrapMode = TextureWrapMode.Repeat,
					anisoLevel = 0,
					name = "Grain Lookup Texture"
				};
				this.m_GrainLookupRT.Create();
			}
			Material material = this.context.materialFactory.Get("Hidden/Post FX/Grain Generator");
			material.SetFloat(GrainComponent.Uniforms._Phase, realtimeSinceStartup / 20f);
			Graphics.Blit(null, this.m_GrainLookupRT, material, settings.colored ? 1 : 0);
			uberMaterial.SetTexture(GrainComponent.Uniforms._GrainTex, this.m_GrainLookupRT);
			uberMaterial.SetVector(GrainComponent.Uniforms._Grain_Params1, new Vector2(settings.luminanceContribution, settings.intensity * 20f));
			uberMaterial.SetVector(GrainComponent.Uniforms._Grain_Params2, new Vector4((float)this.context.width / (float)this.m_GrainLookupRT.width / settings.size, (float)this.context.height / (float)this.m_GrainLookupRT.height / settings.size, value, value2));
		}

		// Token: 0x04003C68 RID: 15464
		private RenderTexture m_GrainLookupRT;

		// Token: 0x020006C2 RID: 1730
		private static class Uniforms
		{
			// Token: 0x04004728 RID: 18216
			internal static readonly int _Grain_Params1 = Shader.PropertyToID("_Grain_Params1");

			// Token: 0x04004729 RID: 18217
			internal static readonly int _Grain_Params2 = Shader.PropertyToID("_Grain_Params2");

			// Token: 0x0400472A RID: 18218
			internal static readonly int _GrainTex = Shader.PropertyToID("_GrainTex");

			// Token: 0x0400472B RID: 18219
			internal static readonly int _Phase = Shader.PropertyToID("_Phase");
		}
	}
}
