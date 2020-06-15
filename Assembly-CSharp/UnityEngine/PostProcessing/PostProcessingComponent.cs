using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004DC RID: 1244
	public abstract class PostProcessingComponent<T> : PostProcessingComponentBase where T : PostProcessingModel
	{
		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001F58 RID: 8024 RVA: 0x001845F6 File Offset: 0x001827F6
		// (set) Token: 0x06001F59 RID: 8025 RVA: 0x001845FE File Offset: 0x001827FE
		public T model { get; internal set; }

		// Token: 0x06001F5A RID: 8026 RVA: 0x00184607 File Offset: 0x00182807
		public virtual void Init(PostProcessingContext pcontext, T pmodel)
		{
			this.context = pcontext;
			this.model = pmodel;
		}

		// Token: 0x06001F5B RID: 8027 RVA: 0x00184617 File Offset: 0x00182817
		public override PostProcessingModel GetModel()
		{
			return this.model;
		}
	}
}
