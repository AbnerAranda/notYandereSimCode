using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004DB RID: 1243
	public abstract class PostProcessingComponentBase
	{
		// Token: 0x06001F52 RID: 8018 RVA: 0x0002D199 File Offset: 0x0002B399
		public virtual DepthTextureMode GetCameraFlags()
		{
			return DepthTextureMode.None;
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001F53 RID: 8019
		public abstract bool active { get; }

		// Token: 0x06001F54 RID: 8020 RVA: 0x00002ACE File Offset: 0x00000CCE
		public virtual void OnEnable()
		{
		}

		// Token: 0x06001F55 RID: 8021 RVA: 0x00002ACE File Offset: 0x00000CCE
		public virtual void OnDisable()
		{
		}

		// Token: 0x06001F56 RID: 8022
		public abstract PostProcessingModel GetModel();

		// Token: 0x04003D56 RID: 15702
		public PostProcessingContext context;
	}
}
