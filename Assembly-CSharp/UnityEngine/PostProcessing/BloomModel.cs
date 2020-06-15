using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004CD RID: 1229
	[Serializable]
	public class BloomModel : PostProcessingModel
	{
		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001F06 RID: 7942 RVA: 0x001836E2 File Offset: 0x001818E2
		// (set) Token: 0x06001F07 RID: 7943 RVA: 0x001836EA File Offset: 0x001818EA
		public BloomModel.Settings settings
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

		// Token: 0x06001F08 RID: 7944 RVA: 0x001836F3 File Offset: 0x001818F3
		public override void Reset()
		{
			this.m_Settings = BloomModel.Settings.defaultSettings;
		}

		// Token: 0x04003D2A RID: 15658
		[SerializeField]
		private BloomModel.Settings m_Settings = BloomModel.Settings.defaultSettings;

		// Token: 0x020006EE RID: 1774
		[Serializable]
		public struct BloomSettings
		{
			// Token: 0x17000647 RID: 1607
			// (get) Token: 0x06002C66 RID: 11366 RVA: 0x001CDF68 File Offset: 0x001CC168
			// (set) Token: 0x06002C65 RID: 11365 RVA: 0x001CDF5A File Offset: 0x001CC15A
			public float thresholdLinear
			{
				get
				{
					return Mathf.GammaToLinearSpace(this.threshold);
				}
				set
				{
					this.threshold = Mathf.LinearToGammaSpace(value);
				}
			}

			// Token: 0x17000648 RID: 1608
			// (get) Token: 0x06002C67 RID: 11367 RVA: 0x001CDF78 File Offset: 0x001CC178
			public static BloomModel.BloomSettings defaultSettings
			{
				get
				{
					return new BloomModel.BloomSettings
					{
						intensity = 0.5f,
						threshold = 1.1f,
						softKnee = 0.5f,
						radius = 4f,
						antiFlicker = false
					};
				}
			}

			// Token: 0x0400489E RID: 18590
			[Min(0f)]
			[Tooltip("Strength of the bloom filter.")]
			public float intensity;

			// Token: 0x0400489F RID: 18591
			[Min(0f)]
			[Tooltip("Filters out pixels under this level of brightness.")]
			public float threshold;

			// Token: 0x040048A0 RID: 18592
			[Range(0f, 1f)]
			[Tooltip("Makes transition between under/over-threshold gradual (0 = hard threshold, 1 = soft threshold).")]
			public float softKnee;

			// Token: 0x040048A1 RID: 18593
			[Range(1f, 7f)]
			[Tooltip("Changes extent of veiling effects in a screen resolution-independent fashion.")]
			public float radius;

			// Token: 0x040048A2 RID: 18594
			[Tooltip("Reduces flashing noise with an additional filter.")]
			public bool antiFlicker;
		}

		// Token: 0x020006EF RID: 1775
		[Serializable]
		public struct LensDirtSettings
		{
			// Token: 0x17000649 RID: 1609
			// (get) Token: 0x06002C68 RID: 11368 RVA: 0x001CDFC8 File Offset: 0x001CC1C8
			public static BloomModel.LensDirtSettings defaultSettings
			{
				get
				{
					return new BloomModel.LensDirtSettings
					{
						texture = null,
						intensity = 3f
					};
				}
			}

			// Token: 0x040048A3 RID: 18595
			[Tooltip("Dirtiness texture to add smudges or dust to the lens.")]
			public Texture texture;

			// Token: 0x040048A4 RID: 18596
			[Min(0f)]
			[Tooltip("Amount of lens dirtiness.")]
			public float intensity;
		}

		// Token: 0x020006F0 RID: 1776
		[Serializable]
		public struct Settings
		{
			// Token: 0x1700064A RID: 1610
			// (get) Token: 0x06002C69 RID: 11369 RVA: 0x001CDFF4 File Offset: 0x001CC1F4
			public static BloomModel.Settings defaultSettings
			{
				get
				{
					return new BloomModel.Settings
					{
						bloom = BloomModel.BloomSettings.defaultSettings,
						lensDirt = BloomModel.LensDirtSettings.defaultSettings
					};
				}
			}

			// Token: 0x040048A5 RID: 18597
			public BloomModel.BloomSettings bloom;

			// Token: 0x040048A6 RID: 18598
			public BloomModel.LensDirtSettings lensDirt;
		}
	}
}
