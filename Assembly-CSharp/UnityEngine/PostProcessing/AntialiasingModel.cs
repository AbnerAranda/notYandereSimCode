using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004CC RID: 1228
	[Serializable]
	public class AntialiasingModel : PostProcessingModel
	{
		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001F02 RID: 7938 RVA: 0x001836B1 File Offset: 0x001818B1
		// (set) Token: 0x06001F03 RID: 7939 RVA: 0x001836B9 File Offset: 0x001818B9
		public AntialiasingModel.Settings settings
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

		// Token: 0x06001F04 RID: 7940 RVA: 0x001836C2 File Offset: 0x001818C2
		public override void Reset()
		{
			this.m_Settings = AntialiasingModel.Settings.defaultSettings;
		}

		// Token: 0x04003D29 RID: 15657
		[SerializeField]
		private AntialiasingModel.Settings m_Settings = AntialiasingModel.Settings.defaultSettings;

		// Token: 0x020006E7 RID: 1767
		public enum Method
		{
			// Token: 0x04004885 RID: 18565
			Fxaa,
			// Token: 0x04004886 RID: 18566
			Taa
		}

		// Token: 0x020006E8 RID: 1768
		public enum FxaaPreset
		{
			// Token: 0x04004888 RID: 18568
			ExtremePerformance,
			// Token: 0x04004889 RID: 18569
			Performance,
			// Token: 0x0400488A RID: 18570
			Default,
			// Token: 0x0400488B RID: 18571
			Quality,
			// Token: 0x0400488C RID: 18572
			ExtremeQuality
		}

		// Token: 0x020006E9 RID: 1769
		[Serializable]
		public struct FxaaQualitySettings
		{
			// Token: 0x0400488D RID: 18573
			[Tooltip("The amount of desired sub-pixel aliasing removal. Effects the sharpeness of the output.")]
			[Range(0f, 1f)]
			public float subpixelAliasingRemovalAmount;

			// Token: 0x0400488E RID: 18574
			[Tooltip("The minimum amount of local contrast required to qualify a region as containing an edge.")]
			[Range(0.063f, 0.333f)]
			public float edgeDetectionThreshold;

			// Token: 0x0400488F RID: 18575
			[Tooltip("Local contrast adaptation value to disallow the algorithm from executing on the darker regions.")]
			[Range(0f, 0.0833f)]
			public float minimumRequiredLuminance;

			// Token: 0x04004890 RID: 18576
			public static AntialiasingModel.FxaaQualitySettings[] presets = new AntialiasingModel.FxaaQualitySettings[]
			{
				new AntialiasingModel.FxaaQualitySettings
				{
					subpixelAliasingRemovalAmount = 0f,
					edgeDetectionThreshold = 0.333f,
					minimumRequiredLuminance = 0.0833f
				},
				new AntialiasingModel.FxaaQualitySettings
				{
					subpixelAliasingRemovalAmount = 0.25f,
					edgeDetectionThreshold = 0.25f,
					minimumRequiredLuminance = 0.0833f
				},
				new AntialiasingModel.FxaaQualitySettings
				{
					subpixelAliasingRemovalAmount = 0.75f,
					edgeDetectionThreshold = 0.166f,
					minimumRequiredLuminance = 0.0833f
				},
				new AntialiasingModel.FxaaQualitySettings
				{
					subpixelAliasingRemovalAmount = 1f,
					edgeDetectionThreshold = 0.125f,
					minimumRequiredLuminance = 0.0625f
				},
				new AntialiasingModel.FxaaQualitySettings
				{
					subpixelAliasingRemovalAmount = 1f,
					edgeDetectionThreshold = 0.063f,
					minimumRequiredLuminance = 0.0312f
				}
			};
		}

		// Token: 0x020006EA RID: 1770
		[Serializable]
		public struct FxaaConsoleSettings
		{
			// Token: 0x04004891 RID: 18577
			[Tooltip("The amount of spread applied to the sampling coordinates while sampling for subpixel information.")]
			[Range(0.33f, 0.5f)]
			public float subpixelSpreadAmount;

			// Token: 0x04004892 RID: 18578
			[Tooltip("This value dictates how sharp the edges in the image are kept; a higher value implies sharper edges.")]
			[Range(2f, 8f)]
			public float edgeSharpnessAmount;

			// Token: 0x04004893 RID: 18579
			[Tooltip("The minimum amount of local contrast required to qualify a region as containing an edge.")]
			[Range(0.125f, 0.25f)]
			public float edgeDetectionThreshold;

			// Token: 0x04004894 RID: 18580
			[Tooltip("Local contrast adaptation value to disallow the algorithm from executing on the darker regions.")]
			[Range(0.04f, 0.06f)]
			public float minimumRequiredLuminance;

			// Token: 0x04004895 RID: 18581
			public static AntialiasingModel.FxaaConsoleSettings[] presets = new AntialiasingModel.FxaaConsoleSettings[]
			{
				new AntialiasingModel.FxaaConsoleSettings
				{
					subpixelSpreadAmount = 0.33f,
					edgeSharpnessAmount = 8f,
					edgeDetectionThreshold = 0.25f,
					minimumRequiredLuminance = 0.06f
				},
				new AntialiasingModel.FxaaConsoleSettings
				{
					subpixelSpreadAmount = 0.33f,
					edgeSharpnessAmount = 8f,
					edgeDetectionThreshold = 0.125f,
					minimumRequiredLuminance = 0.06f
				},
				new AntialiasingModel.FxaaConsoleSettings
				{
					subpixelSpreadAmount = 0.5f,
					edgeSharpnessAmount = 8f,
					edgeDetectionThreshold = 0.125f,
					minimumRequiredLuminance = 0.05f
				},
				new AntialiasingModel.FxaaConsoleSettings
				{
					subpixelSpreadAmount = 0.5f,
					edgeSharpnessAmount = 4f,
					edgeDetectionThreshold = 0.125f,
					minimumRequiredLuminance = 0.04f
				},
				new AntialiasingModel.FxaaConsoleSettings
				{
					subpixelSpreadAmount = 0.5f,
					edgeSharpnessAmount = 2f,
					edgeDetectionThreshold = 0.125f,
					minimumRequiredLuminance = 0.04f
				}
			};
		}

		// Token: 0x020006EB RID: 1771
		[Serializable]
		public struct FxaaSettings
		{
			// Token: 0x17000644 RID: 1604
			// (get) Token: 0x06002C62 RID: 11362 RVA: 0x001CDEBC File Offset: 0x001CC0BC
			public static AntialiasingModel.FxaaSettings defaultSettings
			{
				get
				{
					return new AntialiasingModel.FxaaSettings
					{
						preset = AntialiasingModel.FxaaPreset.Default
					};
				}
			}

			// Token: 0x04004896 RID: 18582
			public AntialiasingModel.FxaaPreset preset;
		}

		// Token: 0x020006EC RID: 1772
		[Serializable]
		public struct TaaSettings
		{
			// Token: 0x17000645 RID: 1605
			// (get) Token: 0x06002C63 RID: 11363 RVA: 0x001CDEDC File Offset: 0x001CC0DC
			public static AntialiasingModel.TaaSettings defaultSettings
			{
				get
				{
					return new AntialiasingModel.TaaSettings
					{
						jitterSpread = 0.75f,
						sharpen = 0.3f,
						stationaryBlending = 0.95f,
						motionBlending = 0.85f
					};
				}
			}

			// Token: 0x04004897 RID: 18583
			[Tooltip("The diameter (in texels) inside which jitter samples are spread. Smaller values result in crisper but more aliased output, while larger values result in more stable but blurrier output.")]
			[Range(0.1f, 1f)]
			public float jitterSpread;

			// Token: 0x04004898 RID: 18584
			[Tooltip("Controls the amount of sharpening applied to the color buffer.")]
			[Range(0f, 3f)]
			public float sharpen;

			// Token: 0x04004899 RID: 18585
			[Tooltip("The blend coefficient for a stationary fragment. Controls the percentage of history sample blended into the final color.")]
			[Range(0f, 0.99f)]
			public float stationaryBlending;

			// Token: 0x0400489A RID: 18586
			[Tooltip("The blend coefficient for a fragment with significant motion. Controls the percentage of history sample blended into the final color.")]
			[Range(0f, 0.99f)]
			public float motionBlending;
		}

		// Token: 0x020006ED RID: 1773
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000646 RID: 1606
			// (get) Token: 0x06002C64 RID: 11364 RVA: 0x001CDF24 File Offset: 0x001CC124
			public static AntialiasingModel.Settings defaultSettings
			{
				get
				{
					return new AntialiasingModel.Settings
					{
						method = AntialiasingModel.Method.Fxaa,
						fxaaSettings = AntialiasingModel.FxaaSettings.defaultSettings,
						taaSettings = AntialiasingModel.TaaSettings.defaultSettings
					};
				}
			}

			// Token: 0x0400489B RID: 18587
			public AntialiasingModel.Method method;

			// Token: 0x0400489C RID: 18588
			public AntialiasingModel.FxaaSettings fxaaSettings;

			// Token: 0x0400489D RID: 18589
			public AntialiasingModel.TaaSettings taaSettings;
		}
	}
}
