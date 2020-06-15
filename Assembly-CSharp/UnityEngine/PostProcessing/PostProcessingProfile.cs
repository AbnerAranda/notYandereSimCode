using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004E1 RID: 1249
	public class PostProcessingProfile : ScriptableObject
	{
		// Token: 0x04003D5E RID: 15710
		public BuiltinDebugViewsModel debugViews = new BuiltinDebugViewsModel();

		// Token: 0x04003D5F RID: 15711
		public FogModel fog = new FogModel();

		// Token: 0x04003D60 RID: 15712
		public AntialiasingModel antialiasing = new AntialiasingModel();

		// Token: 0x04003D61 RID: 15713
		public AmbientOcclusionModel ambientOcclusion = new AmbientOcclusionModel();

		// Token: 0x04003D62 RID: 15714
		public ScreenSpaceReflectionModel screenSpaceReflection = new ScreenSpaceReflectionModel();

		// Token: 0x04003D63 RID: 15715
		public DepthOfFieldModel depthOfField = new DepthOfFieldModel();

		// Token: 0x04003D64 RID: 15716
		public MotionBlurModel motionBlur = new MotionBlurModel();

		// Token: 0x04003D65 RID: 15717
		public EyeAdaptationModel eyeAdaptation = new EyeAdaptationModel();

		// Token: 0x04003D66 RID: 15718
		public BloomModel bloom = new BloomModel();

		// Token: 0x04003D67 RID: 15719
		public ColorGradingModel colorGrading = new ColorGradingModel();

		// Token: 0x04003D68 RID: 15720
		public UserLutModel userLut = new UserLutModel();

		// Token: 0x04003D69 RID: 15721
		public ChromaticAberrationModel chromaticAberration = new ChromaticAberrationModel();

		// Token: 0x04003D6A RID: 15722
		public GrainModel grain = new GrainModel();

		// Token: 0x04003D6B RID: 15723
		public VignetteModel vignette = new VignetteModel();

		// Token: 0x04003D6C RID: 15724
		public DitheringModel dithering = new DitheringModel();
	}
}
