using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004D4 RID: 1236
	[Serializable]
	public class FogModel : PostProcessingModel
	{
		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001F29 RID: 7977 RVA: 0x001838B3 File Offset: 0x00181AB3
		// (set) Token: 0x06001F2A RID: 7978 RVA: 0x001838BB File Offset: 0x00181ABB
		public FogModel.Settings settings
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

		// Token: 0x06001F2B RID: 7979 RVA: 0x001838C4 File Offset: 0x00181AC4
		public override void Reset()
		{
			this.m_Settings = FogModel.Settings.defaultSettings;
		}

		// Token: 0x04003D33 RID: 15667
		[SerializeField]
		private FogModel.Settings m_Settings = FogModel.Settings.defaultSettings;

		// Token: 0x02000705 RID: 1797
		[Serializable]
		public struct Settings
		{
			// Token: 0x1700065A RID: 1626
			// (get) Token: 0x06002C79 RID: 11385 RVA: 0x001CE6BC File Offset: 0x001CC8BC
			public static FogModel.Settings defaultSettings
			{
				get
				{
					return new FogModel.Settings
					{
						excludeSkybox = true
					};
				}
			}

			// Token: 0x04004909 RID: 18697
			[Tooltip("Should the fog affect the skybox?")]
			public bool excludeSkybox;
		}
	}
}
