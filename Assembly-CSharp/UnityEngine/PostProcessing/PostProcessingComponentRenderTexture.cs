using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004DE RID: 1246
	public abstract class PostProcessingComponentRenderTexture<T> : PostProcessingComponent<T> where T : PostProcessingModel
	{
		// Token: 0x06001F61 RID: 8033 RVA: 0x00002ACE File Offset: 0x00000CCE
		public virtual void Prepare(Material material)
		{
		}
	}
}
