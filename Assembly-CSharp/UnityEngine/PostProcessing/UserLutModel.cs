using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004D8 RID: 1240
	[Serializable]
	public class UserLutModel : PostProcessingModel
	{
		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001F39 RID: 7993 RVA: 0x00183977 File Offset: 0x00181B77
		// (set) Token: 0x06001F3A RID: 7994 RVA: 0x0018397F File Offset: 0x00181B7F
		public UserLutModel.Settings settings
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

		// Token: 0x06001F3B RID: 7995 RVA: 0x00183988 File Offset: 0x00181B88
		public override void Reset()
		{
			this.m_Settings = UserLutModel.Settings.defaultSettings;
		}

		// Token: 0x04003D37 RID: 15671
		[SerializeField]
		private UserLutModel.Settings m_Settings = UserLutModel.Settings.defaultSettings;

		// Token: 0x0200070E RID: 1806
		[Serializable]
		public struct Settings
		{
			// Token: 0x1700065E RID: 1630
			// (get) Token: 0x06002C7D RID: 11389 RVA: 0x001CE82C File Offset: 0x001CCA2C
			public static UserLutModel.Settings defaultSettings
			{
				get
				{
					return new UserLutModel.Settings
					{
						lut = null,
						contribution = 1f
					};
				}
			}

			// Token: 0x04004927 RID: 18727
			[Tooltip("Custom lookup texture (strip format, e.g. 256x16).")]
			public Texture2D lut;

			// Token: 0x04004928 RID: 18728
			[Range(0f, 1f)]
			[Tooltip("Blending factor.")]
			public float contribution;
		}
	}
}
