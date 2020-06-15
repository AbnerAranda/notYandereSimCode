using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004D5 RID: 1237
	[Serializable]
	public class GrainModel : PostProcessingModel
	{
		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001F2D RID: 7981 RVA: 0x001838E4 File Offset: 0x00181AE4
		// (set) Token: 0x06001F2E RID: 7982 RVA: 0x001838EC File Offset: 0x00181AEC
		public GrainModel.Settings settings
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

		// Token: 0x06001F2F RID: 7983 RVA: 0x001838F5 File Offset: 0x00181AF5
		public override void Reset()
		{
			this.m_Settings = GrainModel.Settings.defaultSettings;
		}

		// Token: 0x04003D34 RID: 15668
		[SerializeField]
		private GrainModel.Settings m_Settings = GrainModel.Settings.defaultSettings;

		// Token: 0x02000706 RID: 1798
		[Serializable]
		public struct Settings
		{
			// Token: 0x1700065B RID: 1627
			// (get) Token: 0x06002C7A RID: 11386 RVA: 0x001CE6DC File Offset: 0x001CC8DC
			public static GrainModel.Settings defaultSettings
			{
				get
				{
					return new GrainModel.Settings
					{
						colored = true,
						intensity = 0.5f,
						size = 1f,
						luminanceContribution = 0.8f
					};
				}
			}

			// Token: 0x0400490A RID: 18698
			[Tooltip("Enable the use of colored grain.")]
			public bool colored;

			// Token: 0x0400490B RID: 18699
			[Range(0f, 1f)]
			[Tooltip("Grain strength. Higher means more visible grain.")]
			public float intensity;

			// Token: 0x0400490C RID: 18700
			[Range(0.3f, 3f)]
			[Tooltip("Grain particle size.")]
			public float size;

			// Token: 0x0400490D RID: 18701
			[Range(0f, 1f)]
			[Tooltip("Controls the noisiness response curve based on scene luminance. Lower values mean less noise in dark areas.")]
			public float luminanceContribution;
		}
	}
}
