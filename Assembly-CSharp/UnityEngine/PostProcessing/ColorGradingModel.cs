using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004D0 RID: 1232
	[Serializable]
	public class ColorGradingModel : PostProcessingModel
	{
		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001F14 RID: 7956 RVA: 0x001837B8 File Offset: 0x001819B8
		// (set) Token: 0x06001F15 RID: 7957 RVA: 0x001837C0 File Offset: 0x001819C0
		public ColorGradingModel.Settings settings
		{
			get
			{
				return this.m_Settings;
			}
			set
			{
				this.m_Settings = value;
				this.OnValidate();
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001F16 RID: 7958 RVA: 0x001837CF File Offset: 0x001819CF
		// (set) Token: 0x06001F17 RID: 7959 RVA: 0x001837D7 File Offset: 0x001819D7
		public bool isDirty { get; internal set; }

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001F18 RID: 7960 RVA: 0x001837E0 File Offset: 0x001819E0
		// (set) Token: 0x06001F19 RID: 7961 RVA: 0x001837E8 File Offset: 0x001819E8
		public RenderTexture bakedLut { get; internal set; }

		// Token: 0x06001F1A RID: 7962 RVA: 0x001837F1 File Offset: 0x001819F1
		public override void Reset()
		{
			this.m_Settings = ColorGradingModel.Settings.defaultSettings;
			this.OnValidate();
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x00183804 File Offset: 0x00181A04
		public override void OnValidate()
		{
			this.isDirty = true;
		}

		// Token: 0x04003D2D RID: 15661
		[SerializeField]
		private ColorGradingModel.Settings m_Settings = ColorGradingModel.Settings.defaultSettings;

		// Token: 0x020006F6 RID: 1782
		public enum Tonemapper
		{
			// Token: 0x040048BF RID: 18623
			None,
			// Token: 0x040048C0 RID: 18624
			ACES,
			// Token: 0x040048C1 RID: 18625
			Neutral
		}

		// Token: 0x020006F7 RID: 1783
		[Serializable]
		public struct TonemappingSettings
		{
			// Token: 0x1700064F RID: 1615
			// (get) Token: 0x06002C6E RID: 11374 RVA: 0x001CE108 File Offset: 0x001CC308
			public static ColorGradingModel.TonemappingSettings defaultSettings
			{
				get
				{
					return new ColorGradingModel.TonemappingSettings
					{
						tonemapper = ColorGradingModel.Tonemapper.Neutral,
						neutralBlackIn = 0.02f,
						neutralWhiteIn = 10f,
						neutralBlackOut = 0f,
						neutralWhiteOut = 10f,
						neutralWhiteLevel = 5.3f,
						neutralWhiteClip = 10f
					};
				}
			}

			// Token: 0x040048C2 RID: 18626
			[Tooltip("Tonemapping algorithm to use at the end of the color grading process. Use \"Neutral\" if you need a customizable tonemapper or \"Filmic\" to give a standard filmic look to your scenes.")]
			public ColorGradingModel.Tonemapper tonemapper;

			// Token: 0x040048C3 RID: 18627
			[Range(-0.1f, 0.1f)]
			public float neutralBlackIn;

			// Token: 0x040048C4 RID: 18628
			[Range(1f, 20f)]
			public float neutralWhiteIn;

			// Token: 0x040048C5 RID: 18629
			[Range(-0.09f, 0.1f)]
			public float neutralBlackOut;

			// Token: 0x040048C6 RID: 18630
			[Range(1f, 19f)]
			public float neutralWhiteOut;

			// Token: 0x040048C7 RID: 18631
			[Range(0.1f, 20f)]
			public float neutralWhiteLevel;

			// Token: 0x040048C8 RID: 18632
			[Range(1f, 10f)]
			public float neutralWhiteClip;
		}

		// Token: 0x020006F8 RID: 1784
		[Serializable]
		public struct BasicSettings
		{
			// Token: 0x17000650 RID: 1616
			// (get) Token: 0x06002C6F RID: 11375 RVA: 0x001CE170 File Offset: 0x001CC370
			public static ColorGradingModel.BasicSettings defaultSettings
			{
				get
				{
					return new ColorGradingModel.BasicSettings
					{
						postExposure = 0f,
						temperature = 0f,
						tint = 0f,
						hueShift = 0f,
						saturation = 1f,
						contrast = 1f
					};
				}
			}

			// Token: 0x040048C9 RID: 18633
			[Tooltip("Adjusts the overall exposure of the scene in EV units. This is applied after HDR effect and right before tonemapping so it won't affect previous effects in the chain.")]
			public float postExposure;

			// Token: 0x040048CA RID: 18634
			[Range(-100f, 100f)]
			[Tooltip("Sets the white balance to a custom color temperature.")]
			public float temperature;

			// Token: 0x040048CB RID: 18635
			[Range(-100f, 100f)]
			[Tooltip("Sets the white balance to compensate for a green or magenta tint.")]
			public float tint;

			// Token: 0x040048CC RID: 18636
			[Range(-180f, 180f)]
			[Tooltip("Shift the hue of all colors.")]
			public float hueShift;

			// Token: 0x040048CD RID: 18637
			[Range(0f, 2f)]
			[Tooltip("Pushes the intensity of all colors.")]
			public float saturation;

			// Token: 0x040048CE RID: 18638
			[Range(0f, 2f)]
			[Tooltip("Expands or shrinks the overall range of tonal values.")]
			public float contrast;
		}

		// Token: 0x020006F9 RID: 1785
		[Serializable]
		public struct ChannelMixerSettings
		{
			// Token: 0x17000651 RID: 1617
			// (get) Token: 0x06002C70 RID: 11376 RVA: 0x001CE1D0 File Offset: 0x001CC3D0
			public static ColorGradingModel.ChannelMixerSettings defaultSettings
			{
				get
				{
					return new ColorGradingModel.ChannelMixerSettings
					{
						red = new Vector3(1f, 0f, 0f),
						green = new Vector3(0f, 1f, 0f),
						blue = new Vector3(0f, 0f, 1f),
						currentEditingChannel = 0
					};
				}
			}

			// Token: 0x040048CF RID: 18639
			public Vector3 red;

			// Token: 0x040048D0 RID: 18640
			public Vector3 green;

			// Token: 0x040048D1 RID: 18641
			public Vector3 blue;

			// Token: 0x040048D2 RID: 18642
			[HideInInspector]
			public int currentEditingChannel;
		}

		// Token: 0x020006FA RID: 1786
		[Serializable]
		public struct LogWheelsSettings
		{
			// Token: 0x17000652 RID: 1618
			// (get) Token: 0x06002C71 RID: 11377 RVA: 0x001CE240 File Offset: 0x001CC440
			public static ColorGradingModel.LogWheelsSettings defaultSettings
			{
				get
				{
					return new ColorGradingModel.LogWheelsSettings
					{
						slope = Color.clear,
						power = Color.clear,
						offset = Color.clear
					};
				}
			}

			// Token: 0x040048D3 RID: 18643
			[Trackball("GetSlopeValue")]
			public Color slope;

			// Token: 0x040048D4 RID: 18644
			[Trackball("GetPowerValue")]
			public Color power;

			// Token: 0x040048D5 RID: 18645
			[Trackball("GetOffsetValue")]
			public Color offset;
		}

		// Token: 0x020006FB RID: 1787
		[Serializable]
		public struct LinearWheelsSettings
		{
			// Token: 0x17000653 RID: 1619
			// (get) Token: 0x06002C72 RID: 11378 RVA: 0x001CE27C File Offset: 0x001CC47C
			public static ColorGradingModel.LinearWheelsSettings defaultSettings
			{
				get
				{
					return new ColorGradingModel.LinearWheelsSettings
					{
						lift = Color.clear,
						gamma = Color.clear,
						gain = Color.clear
					};
				}
			}

			// Token: 0x040048D6 RID: 18646
			[Trackball("GetLiftValue")]
			public Color lift;

			// Token: 0x040048D7 RID: 18647
			[Trackball("GetGammaValue")]
			public Color gamma;

			// Token: 0x040048D8 RID: 18648
			[Trackball("GetGainValue")]
			public Color gain;
		}

		// Token: 0x020006FC RID: 1788
		public enum ColorWheelMode
		{
			// Token: 0x040048DA RID: 18650
			Linear,
			// Token: 0x040048DB RID: 18651
			Log
		}

		// Token: 0x020006FD RID: 1789
		[Serializable]
		public struct ColorWheelsSettings
		{
			// Token: 0x17000654 RID: 1620
			// (get) Token: 0x06002C73 RID: 11379 RVA: 0x001CE2B8 File Offset: 0x001CC4B8
			public static ColorGradingModel.ColorWheelsSettings defaultSettings
			{
				get
				{
					return new ColorGradingModel.ColorWheelsSettings
					{
						mode = ColorGradingModel.ColorWheelMode.Log,
						log = ColorGradingModel.LogWheelsSettings.defaultSettings,
						linear = ColorGradingModel.LinearWheelsSettings.defaultSettings
					};
				}
			}

			// Token: 0x040048DC RID: 18652
			public ColorGradingModel.ColorWheelMode mode;

			// Token: 0x040048DD RID: 18653
			[TrackballGroup]
			public ColorGradingModel.LogWheelsSettings log;

			// Token: 0x040048DE RID: 18654
			[TrackballGroup]
			public ColorGradingModel.LinearWheelsSettings linear;
		}

		// Token: 0x020006FE RID: 1790
		[Serializable]
		public struct CurvesSettings
		{
			// Token: 0x17000655 RID: 1621
			// (get) Token: 0x06002C74 RID: 11380 RVA: 0x001CE2F0 File Offset: 0x001CC4F0
			public static ColorGradingModel.CurvesSettings defaultSettings
			{
				get
				{
					return new ColorGradingModel.CurvesSettings
					{
						master = new ColorGradingCurve(new AnimationCurve(new Keyframe[]
						{
							new Keyframe(0f, 0f, 1f, 1f),
							new Keyframe(1f, 1f, 1f, 1f)
						}), 0f, false, new Vector2(0f, 1f)),
						red = new ColorGradingCurve(new AnimationCurve(new Keyframe[]
						{
							new Keyframe(0f, 0f, 1f, 1f),
							new Keyframe(1f, 1f, 1f, 1f)
						}), 0f, false, new Vector2(0f, 1f)),
						green = new ColorGradingCurve(new AnimationCurve(new Keyframe[]
						{
							new Keyframe(0f, 0f, 1f, 1f),
							new Keyframe(1f, 1f, 1f, 1f)
						}), 0f, false, new Vector2(0f, 1f)),
						blue = new ColorGradingCurve(new AnimationCurve(new Keyframe[]
						{
							new Keyframe(0f, 0f, 1f, 1f),
							new Keyframe(1f, 1f, 1f, 1f)
						}), 0f, false, new Vector2(0f, 1f)),
						hueVShue = new ColorGradingCurve(new AnimationCurve(), 0.5f, true, new Vector2(0f, 1f)),
						hueVSsat = new ColorGradingCurve(new AnimationCurve(), 0.5f, true, new Vector2(0f, 1f)),
						satVSsat = new ColorGradingCurve(new AnimationCurve(), 0.5f, false, new Vector2(0f, 1f)),
						lumVSsat = new ColorGradingCurve(new AnimationCurve(), 0.5f, false, new Vector2(0f, 1f)),
						e_CurrentEditingCurve = 0,
						e_CurveY = true,
						e_CurveR = false,
						e_CurveG = false,
						e_CurveB = false
					};
				}
			}

			// Token: 0x040048DF RID: 18655
			public ColorGradingCurve master;

			// Token: 0x040048E0 RID: 18656
			public ColorGradingCurve red;

			// Token: 0x040048E1 RID: 18657
			public ColorGradingCurve green;

			// Token: 0x040048E2 RID: 18658
			public ColorGradingCurve blue;

			// Token: 0x040048E3 RID: 18659
			public ColorGradingCurve hueVShue;

			// Token: 0x040048E4 RID: 18660
			public ColorGradingCurve hueVSsat;

			// Token: 0x040048E5 RID: 18661
			public ColorGradingCurve satVSsat;

			// Token: 0x040048E6 RID: 18662
			public ColorGradingCurve lumVSsat;

			// Token: 0x040048E7 RID: 18663
			[HideInInspector]
			public int e_CurrentEditingCurve;

			// Token: 0x040048E8 RID: 18664
			[HideInInspector]
			public bool e_CurveY;

			// Token: 0x040048E9 RID: 18665
			[HideInInspector]
			public bool e_CurveR;

			// Token: 0x040048EA RID: 18666
			[HideInInspector]
			public bool e_CurveG;

			// Token: 0x040048EB RID: 18667
			[HideInInspector]
			public bool e_CurveB;
		}

		// Token: 0x020006FF RID: 1791
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000656 RID: 1622
			// (get) Token: 0x06002C75 RID: 11381 RVA: 0x001CE578 File Offset: 0x001CC778
			public static ColorGradingModel.Settings defaultSettings
			{
				get
				{
					return new ColorGradingModel.Settings
					{
						tonemapping = ColorGradingModel.TonemappingSettings.defaultSettings,
						basic = ColorGradingModel.BasicSettings.defaultSettings,
						channelMixer = ColorGradingModel.ChannelMixerSettings.defaultSettings,
						colorWheels = ColorGradingModel.ColorWheelsSettings.defaultSettings,
						curves = ColorGradingModel.CurvesSettings.defaultSettings
					};
				}
			}

			// Token: 0x040048EC RID: 18668
			public ColorGradingModel.TonemappingSettings tonemapping;

			// Token: 0x040048ED RID: 18669
			public ColorGradingModel.BasicSettings basic;

			// Token: 0x040048EE RID: 18670
			public ColorGradingModel.ChannelMixerSettings channelMixer;

			// Token: 0x040048EF RID: 18671
			public ColorGradingModel.ColorWheelsSettings colorWheels;

			// Token: 0x040048F0 RID: 18672
			public ColorGradingModel.CurvesSettings curves;
		}
	}
}
