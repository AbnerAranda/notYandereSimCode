using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004CF RID: 1231
	[Serializable]
	public class ChromaticAberrationModel : PostProcessingModel
	{
		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001F10 RID: 7952 RVA: 0x00183787 File Offset: 0x00181987
		// (set) Token: 0x06001F11 RID: 7953 RVA: 0x0018378F File Offset: 0x0018198F
		public ChromaticAberrationModel.Settings settings
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

		// Token: 0x06001F12 RID: 7954 RVA: 0x00183798 File Offset: 0x00181998
		public override void Reset()
		{
			this.m_Settings = ChromaticAberrationModel.Settings.defaultSettings;
		}

		// Token: 0x04003D2C RID: 15660
		[SerializeField]
		private ChromaticAberrationModel.Settings m_Settings = ChromaticAberrationModel.Settings.defaultSettings;

		// Token: 0x020006F5 RID: 1781
		[Serializable]
		public struct Settings
		{
			// Token: 0x1700064E RID: 1614
			// (get) Token: 0x06002C6D RID: 11373 RVA: 0x001CE0DC File Offset: 0x001CC2DC
			public static ChromaticAberrationModel.Settings defaultSettings
			{
				get
				{
					return new ChromaticAberrationModel.Settings
					{
						spectralTexture = null,
						intensity = 0.1f
					};
				}
			}

			// Token: 0x040048BC RID: 18620
			[Tooltip("Shift the hue of chromatic aberrations.")]
			public Texture2D spectralTexture;

			// Token: 0x040048BD RID: 18621
			[Range(0f, 1f)]
			[Tooltip("Amount of tangential distortion.")]
			public float intensity;
		}
	}
}
