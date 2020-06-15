using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004B8 RID: 1208
	public sealed class MinAttribute : PropertyAttribute
	{
		// Token: 0x06001E81 RID: 7809 RVA: 0x0017F738 File Offset: 0x0017D938
		public MinAttribute(float min)
		{
			this.min = min;
		}

		// Token: 0x04003CF6 RID: 15606
		public readonly float min;
	}
}
