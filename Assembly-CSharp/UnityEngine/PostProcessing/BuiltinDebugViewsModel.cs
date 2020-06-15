using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004CE RID: 1230
	[Serializable]
	public class BuiltinDebugViewsModel : PostProcessingModel
	{
		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06001F0A RID: 7946 RVA: 0x00183713 File Offset: 0x00181913
		// (set) Token: 0x06001F0B RID: 7947 RVA: 0x0018371B File Offset: 0x0018191B
		public BuiltinDebugViewsModel.Settings settings
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

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06001F0C RID: 7948 RVA: 0x00183724 File Offset: 0x00181924
		public bool willInterrupt
		{
			get
			{
				return !this.IsModeActive(BuiltinDebugViewsModel.Mode.None) && !this.IsModeActive(BuiltinDebugViewsModel.Mode.EyeAdaptation) && !this.IsModeActive(BuiltinDebugViewsModel.Mode.PreGradingLog) && !this.IsModeActive(BuiltinDebugViewsModel.Mode.LogLut) && !this.IsModeActive(BuiltinDebugViewsModel.Mode.UserLut);
			}
		}

		// Token: 0x06001F0D RID: 7949 RVA: 0x00183757 File Offset: 0x00181957
		public override void Reset()
		{
			this.settings = BuiltinDebugViewsModel.Settings.defaultSettings;
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x00183764 File Offset: 0x00181964
		public bool IsModeActive(BuiltinDebugViewsModel.Mode mode)
		{
			return this.m_Settings.mode == mode;
		}

		// Token: 0x04003D2B RID: 15659
		[SerializeField]
		private BuiltinDebugViewsModel.Settings m_Settings = BuiltinDebugViewsModel.Settings.defaultSettings;

		// Token: 0x020006F1 RID: 1777
		[Serializable]
		public struct DepthSettings
		{
			// Token: 0x1700064B RID: 1611
			// (get) Token: 0x06002C6A RID: 11370 RVA: 0x001CE024 File Offset: 0x001CC224
			public static BuiltinDebugViewsModel.DepthSettings defaultSettings
			{
				get
				{
					return new BuiltinDebugViewsModel.DepthSettings
					{
						scale = 1f
					};
				}
			}

			// Token: 0x040048A7 RID: 18599
			[Range(0f, 1f)]
			[Tooltip("Scales the camera far plane before displaying the depth map.")]
			public float scale;
		}

		// Token: 0x020006F2 RID: 1778
		[Serializable]
		public struct MotionVectorsSettings
		{
			// Token: 0x1700064C RID: 1612
			// (get) Token: 0x06002C6B RID: 11371 RVA: 0x001CE048 File Offset: 0x001CC248
			public static BuiltinDebugViewsModel.MotionVectorsSettings defaultSettings
			{
				get
				{
					return new BuiltinDebugViewsModel.MotionVectorsSettings
					{
						sourceOpacity = 1f,
						motionImageOpacity = 0f,
						motionImageAmplitude = 16f,
						motionVectorsOpacity = 1f,
						motionVectorsResolution = 24,
						motionVectorsAmplitude = 64f
					};
				}
			}

			// Token: 0x040048A8 RID: 18600
			[Range(0f, 1f)]
			[Tooltip("Opacity of the source render.")]
			public float sourceOpacity;

			// Token: 0x040048A9 RID: 18601
			[Range(0f, 1f)]
			[Tooltip("Opacity of the per-pixel motion vector colors.")]
			public float motionImageOpacity;

			// Token: 0x040048AA RID: 18602
			[Min(0f)]
			[Tooltip("Because motion vectors are mainly very small vectors, you can use this setting to make them more visible.")]
			public float motionImageAmplitude;

			// Token: 0x040048AB RID: 18603
			[Range(0f, 1f)]
			[Tooltip("Opacity for the motion vector arrows.")]
			public float motionVectorsOpacity;

			// Token: 0x040048AC RID: 18604
			[Range(8f, 64f)]
			[Tooltip("The arrow density on screen.")]
			public int motionVectorsResolution;

			// Token: 0x040048AD RID: 18605
			[Min(0f)]
			[Tooltip("Tweaks the arrows length.")]
			public float motionVectorsAmplitude;
		}

		// Token: 0x020006F3 RID: 1779
		public enum Mode
		{
			// Token: 0x040048AF RID: 18607
			None,
			// Token: 0x040048B0 RID: 18608
			Depth,
			// Token: 0x040048B1 RID: 18609
			Normals,
			// Token: 0x040048B2 RID: 18610
			MotionVectors,
			// Token: 0x040048B3 RID: 18611
			AmbientOcclusion,
			// Token: 0x040048B4 RID: 18612
			EyeAdaptation,
			// Token: 0x040048B5 RID: 18613
			FocusPlane,
			// Token: 0x040048B6 RID: 18614
			PreGradingLog,
			// Token: 0x040048B7 RID: 18615
			LogLut,
			// Token: 0x040048B8 RID: 18616
			UserLut
		}

		// Token: 0x020006F4 RID: 1780
		[Serializable]
		public struct Settings
		{
			// Token: 0x1700064D RID: 1613
			// (get) Token: 0x06002C6C RID: 11372 RVA: 0x001CE0A4 File Offset: 0x001CC2A4
			public static BuiltinDebugViewsModel.Settings defaultSettings
			{
				get
				{
					return new BuiltinDebugViewsModel.Settings
					{
						mode = BuiltinDebugViewsModel.Mode.None,
						depth = BuiltinDebugViewsModel.DepthSettings.defaultSettings,
						motionVectors = BuiltinDebugViewsModel.MotionVectorsSettings.defaultSettings
					};
				}
			}

			// Token: 0x040048B9 RID: 18617
			public BuiltinDebugViewsModel.Mode mode;

			// Token: 0x040048BA RID: 18618
			public BuiltinDebugViewsModel.DepthSettings depth;

			// Token: 0x040048BB RID: 18619
			public BuiltinDebugViewsModel.MotionVectorsSettings motionVectors;
		}
	}
}
