using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004D7 RID: 1239
	[Serializable]
	public class ScreenSpaceReflectionModel : PostProcessingModel
	{
		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001F35 RID: 7989 RVA: 0x00183946 File Offset: 0x00181B46
		// (set) Token: 0x06001F36 RID: 7990 RVA: 0x0018394E File Offset: 0x00181B4E
		public ScreenSpaceReflectionModel.Settings settings
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

		// Token: 0x06001F37 RID: 7991 RVA: 0x00183957 File Offset: 0x00181B57
		public override void Reset()
		{
			this.m_Settings = ScreenSpaceReflectionModel.Settings.defaultSettings;
		}

		// Token: 0x04003D36 RID: 15670
		[SerializeField]
		private ScreenSpaceReflectionModel.Settings m_Settings = ScreenSpaceReflectionModel.Settings.defaultSettings;

		// Token: 0x02000708 RID: 1800
		public enum SSRResolution
		{
			// Token: 0x04004912 RID: 18706
			High,
			// Token: 0x04004913 RID: 18707
			Low = 2
		}

		// Token: 0x02000709 RID: 1801
		public enum SSRReflectionBlendType
		{
			// Token: 0x04004915 RID: 18709
			PhysicallyBased,
			// Token: 0x04004916 RID: 18710
			Additive
		}

		// Token: 0x0200070A RID: 1802
		[Serializable]
		public struct IntensitySettings
		{
			// Token: 0x04004917 RID: 18711
			[Tooltip("Nonphysical multiplier for the SSR reflections. 1.0 is physically based.")]
			[Range(0f, 2f)]
			public float reflectionMultiplier;

			// Token: 0x04004918 RID: 18712
			[Tooltip("How far away from the maxDistance to begin fading SSR.")]
			[Range(0f, 1000f)]
			public float fadeDistance;

			// Token: 0x04004919 RID: 18713
			[Tooltip("Amplify Fresnel fade out. Increase if floor reflections look good close to the surface and bad farther 'under' the floor.")]
			[Range(0f, 1f)]
			public float fresnelFade;

			// Token: 0x0400491A RID: 18714
			[Tooltip("Higher values correspond to a faster Fresnel fade as the reflection changes from the grazing angle.")]
			[Range(0.1f, 10f)]
			public float fresnelFadePower;
		}

		// Token: 0x0200070B RID: 1803
		[Serializable]
		public struct ReflectionSettings
		{
			// Token: 0x0400491B RID: 18715
			[Tooltip("How the reflections are blended into the render.")]
			public ScreenSpaceReflectionModel.SSRReflectionBlendType blendType;

			// Token: 0x0400491C RID: 18716
			[Tooltip("Half resolution SSRR is much faster, but less accurate.")]
			public ScreenSpaceReflectionModel.SSRResolution reflectionQuality;

			// Token: 0x0400491D RID: 18717
			[Tooltip("Maximum reflection distance in world units.")]
			[Range(0.1f, 300f)]
			public float maxDistance;

			// Token: 0x0400491E RID: 18718
			[Tooltip("Max raytracing length.")]
			[Range(16f, 1024f)]
			public int iterationCount;

			// Token: 0x0400491F RID: 18719
			[Tooltip("Log base 2 of ray tracing coarse step size. Higher traces farther, lower gives better quality silhouettes.")]
			[Range(1f, 16f)]
			public int stepSize;

			// Token: 0x04004920 RID: 18720
			[Tooltip("Typical thickness of columns, walls, furniture, and other objects that reflection rays might pass behind.")]
			[Range(0.01f, 10f)]
			public float widthModifier;

			// Token: 0x04004921 RID: 18721
			[Tooltip("Blurriness of reflections.")]
			[Range(0.1f, 8f)]
			public float reflectionBlur;

			// Token: 0x04004922 RID: 18722
			[Tooltip("Disable for a performance gain in scenes where most glossy objects are horizontal, like floors, water, and tables. Leave on for scenes with glossy vertical objects.")]
			public bool reflectBackfaces;
		}

		// Token: 0x0200070C RID: 1804
		[Serializable]
		public struct ScreenEdgeMask
		{
			// Token: 0x04004923 RID: 18723
			[Tooltip("Higher = fade out SSRR near the edge of the screen so that reflections don't pop under camera motion.")]
			[Range(0f, 1f)]
			public float intensity;
		}

		// Token: 0x0200070D RID: 1805
		[Serializable]
		public struct Settings
		{
			// Token: 0x1700065D RID: 1629
			// (get) Token: 0x06002C7C RID: 11388 RVA: 0x001CE758 File Offset: 0x001CC958
			public static ScreenSpaceReflectionModel.Settings defaultSettings
			{
				get
				{
					return new ScreenSpaceReflectionModel.Settings
					{
						reflection = new ScreenSpaceReflectionModel.ReflectionSettings
						{
							blendType = ScreenSpaceReflectionModel.SSRReflectionBlendType.PhysicallyBased,
							reflectionQuality = ScreenSpaceReflectionModel.SSRResolution.Low,
							maxDistance = 100f,
							iterationCount = 256,
							stepSize = 3,
							widthModifier = 0.5f,
							reflectionBlur = 1f,
							reflectBackfaces = false
						},
						intensity = new ScreenSpaceReflectionModel.IntensitySettings
						{
							reflectionMultiplier = 1f,
							fadeDistance = 100f,
							fresnelFade = 1f,
							fresnelFadePower = 1f
						},
						screenEdgeMask = new ScreenSpaceReflectionModel.ScreenEdgeMask
						{
							intensity = 0.03f
						}
					};
				}
			}

			// Token: 0x04004924 RID: 18724
			public ScreenSpaceReflectionModel.ReflectionSettings reflection;

			// Token: 0x04004925 RID: 18725
			public ScreenSpaceReflectionModel.IntensitySettings intensity;

			// Token: 0x04004926 RID: 18726
			public ScreenSpaceReflectionModel.ScreenEdgeMask screenEdgeMask;
		}
	}
}
