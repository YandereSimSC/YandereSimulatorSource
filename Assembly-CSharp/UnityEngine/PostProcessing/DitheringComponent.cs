using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004B1 RID: 1201
	public sealed class DitheringComponent : PostProcessingComponentRenderTexture<DitheringModel>
	{
		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06001E7C RID: 7804 RVA: 0x0017C2B2 File Offset: 0x0017A4B2
		public override bool active
		{
			get
			{
				return base.model.enabled && !this.context.interrupted;
			}
		}

		// Token: 0x06001E7D RID: 7805 RVA: 0x0017C2D1 File Offset: 0x0017A4D1
		public override void OnDisable()
		{
			this.noiseTextures = null;
		}

		// Token: 0x06001E7E RID: 7806 RVA: 0x0017C2DC File Offset: 0x0017A4DC
		private void LoadNoiseTextures()
		{
			this.noiseTextures = new Texture2D[64];
			for (int i = 0; i < 64; i++)
			{
				this.noiseTextures[i] = Resources.Load<Texture2D>("Bluenoise64/LDR_LLL1_" + i);
			}
		}

		// Token: 0x06001E7F RID: 7807 RVA: 0x0017C320 File Offset: 0x0017A520
		public override void Prepare(Material uberMaterial)
		{
			int num = this.textureIndex + 1;
			this.textureIndex = num;
			if (num >= 64)
			{
				this.textureIndex = 0;
			}
			float value = Random.value;
			float value2 = Random.value;
			if (this.noiseTextures == null)
			{
				this.LoadNoiseTextures();
			}
			Texture2D texture2D = this.noiseTextures[this.textureIndex];
			uberMaterial.EnableKeyword("DITHERING");
			uberMaterial.SetTexture(DitheringComponent.Uniforms._DitheringTex, texture2D);
			uberMaterial.SetVector(DitheringComponent.Uniforms._DitheringCoords, new Vector4((float)this.context.width / (float)texture2D.width, (float)this.context.height / (float)texture2D.height, value, value2));
		}

		// Token: 0x04003C59 RID: 15449
		private Texture2D[] noiseTextures;

		// Token: 0x04003C5A RID: 15450
		private int textureIndex;

		// Token: 0x04003C5B RID: 15451
		private const int k_TextureCount = 64;

		// Token: 0x020006BE RID: 1726
		private static class Uniforms
		{
			// Token: 0x04004719 RID: 18201
			internal static readonly int _DitheringTex = Shader.PropertyToID("_DitheringTex");

			// Token: 0x0400471A RID: 18202
			internal static readonly int _DitheringCoords = Shader.PropertyToID("_DitheringCoords");
		}
	}
}
