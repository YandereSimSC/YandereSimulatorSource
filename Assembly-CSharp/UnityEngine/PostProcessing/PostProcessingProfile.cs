using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004D1 RID: 1233
	public class PostProcessingProfile : ScriptableObject
	{
		// Token: 0x04003CAE RID: 15534
		public BuiltinDebugViewsModel debugViews = new BuiltinDebugViewsModel();

		// Token: 0x04003CAF RID: 15535
		public FogModel fog = new FogModel();

		// Token: 0x04003CB0 RID: 15536
		public AntialiasingModel antialiasing = new AntialiasingModel();

		// Token: 0x04003CB1 RID: 15537
		public AmbientOcclusionModel ambientOcclusion = new AmbientOcclusionModel();

		// Token: 0x04003CB2 RID: 15538
		public ScreenSpaceReflectionModel screenSpaceReflection = new ScreenSpaceReflectionModel();

		// Token: 0x04003CB3 RID: 15539
		public DepthOfFieldModel depthOfField = new DepthOfFieldModel();

		// Token: 0x04003CB4 RID: 15540
		public MotionBlurModel motionBlur = new MotionBlurModel();

		// Token: 0x04003CB5 RID: 15541
		public EyeAdaptationModel eyeAdaptation = new EyeAdaptationModel();

		// Token: 0x04003CB6 RID: 15542
		public BloomModel bloom = new BloomModel();

		// Token: 0x04003CB7 RID: 15543
		public ColorGradingModel colorGrading = new ColorGradingModel();

		// Token: 0x04003CB8 RID: 15544
		public UserLutModel userLut = new UserLutModel();

		// Token: 0x04003CB9 RID: 15545
		public ChromaticAberrationModel chromaticAberration = new ChromaticAberrationModel();

		// Token: 0x04003CBA RID: 15546
		public GrainModel grain = new GrainModel();

		// Token: 0x04003CBB RID: 15547
		public VignetteModel vignette = new VignetteModel();

		// Token: 0x04003CBC RID: 15548
		public DitheringModel dithering = new DitheringModel();
	}
}
