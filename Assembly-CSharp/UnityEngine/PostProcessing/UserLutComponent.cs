using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004B9 RID: 1209
	public sealed class UserLutComponent : PostProcessingComponentRenderTexture<UserLutModel>
	{
		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06001EB5 RID: 7861 RVA: 0x0017DF2C File Offset: 0x0017C12C
		public override bool active
		{
			get
			{
				UserLutModel.Settings settings = base.model.settings;
				return base.model.enabled && settings.lut != null && settings.contribution > 0f && settings.lut.height == (int)Mathf.Sqrt((float)settings.lut.width) && !this.context.interrupted;
			}
		}

		// Token: 0x06001EB6 RID: 7862 RVA: 0x0017DF9C File Offset: 0x0017C19C
		public override void Prepare(Material uberMaterial)
		{
			UserLutModel.Settings settings = base.model.settings;
			uberMaterial.EnableKeyword("USER_LUT");
			uberMaterial.SetTexture(UserLutComponent.Uniforms._UserLut, settings.lut);
			uberMaterial.SetVector(UserLutComponent.Uniforms._UserLut_Params, new Vector4(1f / (float)settings.lut.width, 1f / (float)settings.lut.height, (float)settings.lut.height - 1f, settings.contribution));
		}

		// Token: 0x06001EB7 RID: 7863 RVA: 0x0017E020 File Offset: 0x0017C220
		public void OnGUI()
		{
			UserLutModel.Settings settings = base.model.settings;
			GUI.DrawTexture(new Rect(this.context.viewport.x * (float)Screen.width + 8f, 8f, (float)settings.lut.width, (float)settings.lut.height), settings.lut);
		}

		// Token: 0x020006CA RID: 1738
		private static class Uniforms
		{
			// Token: 0x04004788 RID: 18312
			internal static readonly int _UserLut = Shader.PropertyToID("_UserLut");

			// Token: 0x04004789 RID: 18313
			internal static readonly int _UserLut_Params = Shader.PropertyToID("_UserLut_Params");
		}
	}
}
