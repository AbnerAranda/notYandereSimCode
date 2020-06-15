using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004E0 RID: 1248
	[Serializable]
	public abstract class PostProcessingModel
	{
		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001F6D RID: 8045 RVA: 0x001846C0 File Offset: 0x001828C0
		// (set) Token: 0x06001F6E RID: 8046 RVA: 0x001846C8 File Offset: 0x001828C8
		public bool enabled
		{
			get
			{
				return this.m_Enabled;
			}
			set
			{
				this.m_Enabled = value;
				if (value)
				{
					this.OnValidate();
				}
			}
		}

		// Token: 0x06001F6F RID: 8047
		public abstract void Reset();

		// Token: 0x06001F70 RID: 8048 RVA: 0x00002ACE File Offset: 0x00000CCE
		public virtual void OnValidate()
		{
		}

		// Token: 0x04003D5D RID: 15709
		[SerializeField]
		[GetSet("enabled")]
		private bool m_Enabled;
	}
}
