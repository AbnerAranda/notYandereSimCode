using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004D1 RID: 1233
	[Serializable]
	public class DepthOfFieldModel : PostProcessingModel
	{
		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001F1D RID: 7965 RVA: 0x00183820 File Offset: 0x00181A20
		// (set) Token: 0x06001F1E RID: 7966 RVA: 0x00183828 File Offset: 0x00181A28
		public DepthOfFieldModel.Settings settings
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

		// Token: 0x06001F1F RID: 7967 RVA: 0x00183831 File Offset: 0x00181A31
		public override void Reset()
		{
			this.m_Settings = DepthOfFieldModel.Settings.defaultSettings;
		}

		// Token: 0x04003D30 RID: 15664
		[SerializeField]
		private DepthOfFieldModel.Settings m_Settings = DepthOfFieldModel.Settings.defaultSettings;

		// Token: 0x02000700 RID: 1792
		public enum KernelSize
		{
			// Token: 0x040048F2 RID: 18674
			Small,
			// Token: 0x040048F3 RID: 18675
			Medium,
			// Token: 0x040048F4 RID: 18676
			Large,
			// Token: 0x040048F5 RID: 18677
			VeryLarge
		}

		// Token: 0x02000701 RID: 1793
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000657 RID: 1623
			// (get) Token: 0x06002C76 RID: 11382 RVA: 0x001CE5CC File Offset: 0x001CC7CC
			public static DepthOfFieldModel.Settings defaultSettings
			{
				get
				{
					return new DepthOfFieldModel.Settings
					{
						focusDistance = 10f,
						aperture = 5.6f,
						focalLength = 50f,
						useCameraFov = false,
						kernelSize = DepthOfFieldModel.KernelSize.Medium
					};
				}
			}

			// Token: 0x040048F6 RID: 18678
			[Min(0.1f)]
			[Tooltip("Distance to the point of focus.")]
			public float focusDistance;

			// Token: 0x040048F7 RID: 18679
			[Range(0.05f, 32f)]
			[Tooltip("Ratio of aperture (known as f-stop or f-number). The smaller the value is, the shallower the depth of field is.")]
			public float aperture;

			// Token: 0x040048F8 RID: 18680
			[Range(1f, 300f)]
			[Tooltip("Distance between the lens and the film. The larger the value is, the shallower the depth of field is.")]
			public float focalLength;

			// Token: 0x040048F9 RID: 18681
			[Tooltip("Calculate the focal length automatically from the field-of-view value set on the camera. Using this setting isn't recommended.")]
			public bool useCameraFov;

			// Token: 0x040048FA RID: 18682
			[Tooltip("Convolution kernel size of the bokeh filter, which determines the maximum radius of bokeh. It also affects the performance (the larger the kernel is, the longer the GPU time is required).")]
			public DepthOfFieldModel.KernelSize kernelSize;
		}
	}
}
