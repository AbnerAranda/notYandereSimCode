using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004D6 RID: 1238
	[Serializable]
	public class MotionBlurModel : PostProcessingModel
	{
		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001F31 RID: 7985 RVA: 0x00183915 File Offset: 0x00181B15
		// (set) Token: 0x06001F32 RID: 7986 RVA: 0x0018391D File Offset: 0x00181B1D
		public MotionBlurModel.Settings settings
		{
			get
			{
				return this.m_Settings;
			}
			set
			{
				this.m_Settings = value;
			}
		}

		// Token: 0x06001F33 RID: 7987 RVA: 0x00183926 File Offset: 0x00181B26
		public override void Reset()
		{
			this.m_Settings = MotionBlurModel.Settings.defaultSettings;
		}

		// Token: 0x04003D35 RID: 15669
		[SerializeField]
		private MotionBlurModel.Settings m_Settings = MotionBlurModel.Settings.defaultSettings;

		// Token: 0x02000707 RID: 1799
		[Serializable]
		public struct Settings
		{
			// Token: 0x1700065C RID: 1628
			// (get) Token: 0x06002C7B RID: 11387 RVA: 0x001CE720 File Offset: 0x001CC920
			public static MotionBlurModel.Settings defaultSettings
			{
				get
				{
					return new MotionBlurModel.Settings
					{
						shutterAngle = 270f,
						sampleCount = 10,
						frameBlending = 0f
					};
				}
			}

			// Token: 0x0400490E RID: 18702
			[Range(0f, 360f)]
			[Tooltip("The angle of rotary shutter. Larger values give longer exposure.")]
			public float shutterAngle;

			// Token: 0x0400490F RID: 18703
			[Range(4f, 32f)]
			[Tooltip("The amount of sample points, which affects quality and performances.")]
			public int sampleCount;

			// Token: 0x04004910 RID: 18704
			[Range(0f, 1f)]
			[Tooltip("The strength of multiple frame blending. The opacity of preceding frames are determined from this coefficient and time differences.")]
			public float frameBlending;
		}
	}
}
