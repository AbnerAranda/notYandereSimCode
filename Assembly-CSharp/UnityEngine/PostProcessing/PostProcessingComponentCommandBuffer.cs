using System;
using UnityEngine.Rendering;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004DD RID: 1245
	public abstract class PostProcessingComponentCommandBuffer<T> : PostProcessingComponent<T> where T : PostProcessingModel
	{
		// Token: 0x06001F5D RID: 8029
		public abstract CameraEvent GetCameraEvent();

		// Token: 0x06001F5E RID: 8030
		public abstract string GetName();

		// Token: 0x06001F5F RID: 8031
		public abstract void PopulateCommandBuffer(CommandBuffer cb);
	}
}
