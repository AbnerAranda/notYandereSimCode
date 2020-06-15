using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004D2 RID: 1234
	[Serializable]
	public class DitheringModel : PostProcessingModel
	{
		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001F21 RID: 7969 RVA: 0x00183851 File Offset: 0x00181A51
		// (set) Token: 0x06001F22 RID: 7970 RVA: 0x00183859 File Offset: 0x00181A59
		public DitheringModel.Settings settings
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

		// Token: 0x06001F23 RID: 7971 RVA: 0x00183862 File Offset: 0x00181A62
		public override void Reset()
		{
			this.m_Settings = DitheringModel.Settings.defaultSettings;
		}

		// Token: 0x04003D31 RID: 15665
		[SerializeField]
		private DitheringModel.Settings m_Settings = DitheringModel.Settings.defaultSettings;

		// Token: 0x02000702 RID: 1794
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000658 RID: 1624
			// (get) Token: 0x06002C77 RID: 11383 RVA: 0x001CE618 File Offset: 0x001CC818
			public static DitheringModel.Settings defaultSettings
			{
				get
				{
					return default(DitheringModel.Settings);
				}
			}
		}
	}
}
