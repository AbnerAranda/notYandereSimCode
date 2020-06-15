using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004CB RID: 1227
	[Serializable]
	public class AmbientOcclusionModel : PostProcessingModel
	{
		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001EFE RID: 7934 RVA: 0x00183680 File Offset: 0x00181880
		// (set) Token: 0x06001EFF RID: 7935 RVA: 0x00183688 File Offset: 0x00181888
		public AmbientOcclusionModel.Settings settings
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

		// Token: 0x06001F00 RID: 7936 RVA: 0x00183691 File Offset: 0x00181891
		public override void Reset()
		{
			this.m_Settings = AmbientOcclusionModel.Settings.defaultSettings;
		}

		// Token: 0x04003D28 RID: 15656
		[SerializeField]
		private AmbientOcclusionModel.Settings m_Settings = AmbientOcclusionModel.Settings.defaultSettings;

		// Token: 0x020006E5 RID: 1765
		public enum SampleCount
		{
			// Token: 0x04004879 RID: 18553
			Lowest = 3,
			// Token: 0x0400487A RID: 18554
			Low = 6,
			// Token: 0x0400487B RID: 18555
			Medium = 10,
			// Token: 0x0400487C RID: 18556
			High = 16
		}

		// Token: 0x020006E6 RID: 1766
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000643 RID: 1603
			// (get) Token: 0x06002C5F RID: 11359 RVA: 0x001CDBF0 File Offset: 0x001CBDF0
			public static AmbientOcclusionModel.Settings defaultSettings
			{
				get
				{
					return new AmbientOcclusionModel.Settings
					{
						intensity = 1f,
						radius = 0.3f,
						sampleCount = AmbientOcclusionModel.SampleCount.Medium,
						downsampling = true,
						forceForwardCompatibility = false,
						ambientOnly = false,
						highPrecision = false
					};
				}
			}

			// Token: 0x0400487D RID: 18557
			[Range(0f, 4f)]
			[Tooltip("Degree of darkness produced by the effect.")]
			public float intensity;

			// Token: 0x0400487E RID: 18558
			[Min(0.0001f)]
			[Tooltip("Radius of sample points, which affects extent of darkened areas.")]
			public float radius;

			// Token: 0x0400487F RID: 18559
			[Tooltip("Number of sample points, which affects quality and performance.")]
			public AmbientOcclusionModel.SampleCount sampleCount;

			// Token: 0x04004880 RID: 18560
			[Tooltip("Halves the resolution of the effect to increase performance at the cost of visual quality.")]
			public bool downsampling;

			// Token: 0x04004881 RID: 18561
			[Tooltip("Forces compatibility with Forward rendered objects when working with the Deferred rendering path.")]
			public bool forceForwardCompatibility;

			// Token: 0x04004882 RID: 18562
			[Tooltip("Enables the ambient-only mode in that the effect only affects ambient lighting. This mode is only available with the Deferred rendering path and HDR rendering.")]
			public bool ambientOnly;

			// Token: 0x04004883 RID: 18563
			[Tooltip("Toggles the use of a higher precision depth texture with the forward rendering path (may impact performances). Has no effect with the deferred rendering path.")]
			public bool highPrecision;
		}
	}
}
