using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004B4 RID: 1204
	public sealed class FxaaComponent : PostProcessingComponentRenderTexture<AntialiasingModel>
	{
		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001E8F RID: 7823 RVA: 0x0017CABA File Offset: 0x0017ACBA
		public override bool active
		{
			get
			{
				return base.model.enabled && base.model.settings.method == AntialiasingModel.Method.Fxaa && !this.context.interrupted;
			}
		}

		// Token: 0x06001E90 RID: 7824 RVA: 0x0017CAEC File Offset: 0x0017ACEC
		public void Render(RenderTexture source, RenderTexture destination)
		{
			AntialiasingModel.FxaaSettings fxaaSettings = base.model.settings.fxaaSettings;
			Material material = this.context.materialFactory.Get("Hidden/Post FX/FXAA");
			AntialiasingModel.FxaaQualitySettings fxaaQualitySettings = AntialiasingModel.FxaaQualitySettings.presets[(int)fxaaSettings.preset];
			AntialiasingModel.FxaaConsoleSettings fxaaConsoleSettings = AntialiasingModel.FxaaConsoleSettings.presets[(int)fxaaSettings.preset];
			material.SetVector(FxaaComponent.Uniforms._QualitySettings, new Vector3(fxaaQualitySettings.subpixelAliasingRemovalAmount, fxaaQualitySettings.edgeDetectionThreshold, fxaaQualitySettings.minimumRequiredLuminance));
			material.SetVector(FxaaComponent.Uniforms._ConsoleSettings, new Vector4(fxaaConsoleSettings.subpixelSpreadAmount, fxaaConsoleSettings.edgeSharpnessAmount, fxaaConsoleSettings.edgeDetectionThreshold, fxaaConsoleSettings.minimumRequiredLuminance));
			Graphics.Blit(source, destination, material, 0);
		}

		// Token: 0x020006C1 RID: 1729
		private static class Uniforms
		{
			// Token: 0x04004726 RID: 18214
			internal static readonly int _QualitySettings = Shader.PropertyToID("_QualitySettings");

			// Token: 0x04004727 RID: 18215
			internal static readonly int _ConsoleSettings = Shader.PropertyToID("_ConsoleSettings");
		}
	}
}
