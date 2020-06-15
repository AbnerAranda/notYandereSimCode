using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004D9 RID: 1241
	[Serializable]
	public class VignetteModel : PostProcessingModel
	{
		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001F3D RID: 7997 RVA: 0x001839A8 File Offset: 0x00181BA8
		// (set) Token: 0x06001F3E RID: 7998 RVA: 0x001839B0 File Offset: 0x00181BB0
		public VignetteModel.Settings settings
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

		// Token: 0x06001F3F RID: 7999 RVA: 0x001839B9 File Offset: 0x00181BB9
		public override void Reset()
		{
			this.m_Settings = VignetteModel.Settings.defaultSettings;
		}

		// Token: 0x04003D38 RID: 15672
		[SerializeField]
		private VignetteModel.Settings m_Settings = VignetteModel.Settings.defaultSettings;

		// Token: 0x0200070F RID: 1807
		public enum Mode
		{
			// Token: 0x0400492A RID: 18730
			Classic,
			// Token: 0x0400492B RID: 18731
			Masked
		}

		// Token: 0x02000710 RID: 1808
		[Serializable]
		public struct Settings
		{
			// Token: 0x1700065F RID: 1631
			// (get) Token: 0x06002C7E RID: 11390 RVA: 0x001CE858 File Offset: 0x001CCA58
			public static VignetteModel.Settings defaultSettings
			{
				get
				{
					return new VignetteModel.Settings
					{
						mode = VignetteModel.Mode.Classic,
						color = new Color(0f, 0f, 0f, 1f),
						center = new Vector2(0.5f, 0.5f),
						intensity = 0.45f,
						smoothness = 0.2f,
						roundness = 1f,
						mask = null,
						opacity = 1f,
						rounded = false
					};
				}
			}

			// Token: 0x0400492C RID: 18732
			[Tooltip("Use the \"Classic\" mode for parametric controls. Use the \"Masked\" mode to use your own texture mask.")]
			public VignetteModel.Mode mode;

			// Token: 0x0400492D RID: 18733
			[ColorUsage(false)]
			[Tooltip("Vignette color. Use the alpha channel for transparency.")]
			public Color color;

			// Token: 0x0400492E RID: 18734
			[Tooltip("Sets the vignette center point (screen center is [0.5,0.5]).")]
			public Vector2 center;

			// Token: 0x0400492F RID: 18735
			[Range(0f, 1f)]
			[Tooltip("Amount of vignetting on screen.")]
			public float intensity;

			// Token: 0x04004930 RID: 18736
			[Range(0.01f, 1f)]
			[Tooltip("Smoothness of the vignette borders.")]
			public float smoothness;

			// Token: 0x04004931 RID: 18737
			[Range(0f, 1f)]
			[Tooltip("Lower values will make a square-ish vignette.")]
			public float roundness;

			// Token: 0x04004932 RID: 18738
			[Tooltip("A black and white mask to use as a vignette.")]
			public Texture mask;

			// Token: 0x04004933 RID: 18739
			[Range(0f, 1f)]
			[Tooltip("Mask opacity.")]
			public float opacity;

			// Token: 0x04004934 RID: 18740
			[Tooltip("Should the vignette be perfectly round or be dependent on the current aspect ratio?")]
			public bool rounded;
		}
	}
}
