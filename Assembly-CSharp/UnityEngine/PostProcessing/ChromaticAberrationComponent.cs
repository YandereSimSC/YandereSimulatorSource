﻿using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004AE RID: 1198
	public sealed class ChromaticAberrationComponent : PostProcessingComponentRenderTexture<ChromaticAberrationModel>
	{
		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06001E58 RID: 7768 RVA: 0x0017AF54 File Offset: 0x00179154
		public override bool active
		{
			get
			{
				return base.model.enabled && base.model.settings.intensity > 0f && !this.context.interrupted;
			}
		}

		// Token: 0x06001E59 RID: 7769 RVA: 0x0017AF8A File Offset: 0x0017918A
		public override void OnDisable()
		{
			GraphicsUtils.Destroy(this.m_SpectrumLut);
			this.m_SpectrumLut = null;
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x0017AFA0 File Offset: 0x001791A0
		public override void Prepare(Material uberMaterial)
		{
			ChromaticAberrationModel.Settings settings = base.model.settings;
			Texture2D texture2D = settings.spectralTexture;
			if (texture2D == null)
			{
				if (this.m_SpectrumLut == null)
				{
					this.m_SpectrumLut = new Texture2D(3, 1, TextureFormat.RGB24, false)
					{
						name = "Chromatic Aberration Spectrum Lookup",
						filterMode = FilterMode.Bilinear,
						wrapMode = TextureWrapMode.Clamp,
						anisoLevel = 0,
						hideFlags = HideFlags.DontSave
					};
					Color[] pixels = new Color[]
					{
						new Color(1f, 0f, 0f),
						new Color(0f, 1f, 0f),
						new Color(0f, 0f, 1f)
					};
					this.m_SpectrumLut.SetPixels(pixels);
					this.m_SpectrumLut.Apply();
				}
				texture2D = this.m_SpectrumLut;
			}
			uberMaterial.EnableKeyword("CHROMATIC_ABERRATION");
			uberMaterial.SetFloat(ChromaticAberrationComponent.Uniforms._ChromaticAberration_Amount, settings.intensity * 0.03f);
			uberMaterial.SetTexture(ChromaticAberrationComponent.Uniforms._ChromaticAberration_Spectrum, texture2D);
		}

		// Token: 0x04003C50 RID: 15440
		private Texture2D m_SpectrumLut;

		// Token: 0x020006BB RID: 1723
		private static class Uniforms
		{
			// Token: 0x040046F8 RID: 18168
			internal static readonly int _ChromaticAberration_Amount = Shader.PropertyToID("_ChromaticAberration_Amount");

			// Token: 0x040046F9 RID: 18169
			internal static readonly int _ChromaticAberration_Spectrum = Shader.PropertyToID("_ChromaticAberration_Spectrum");
		}
	}
}
