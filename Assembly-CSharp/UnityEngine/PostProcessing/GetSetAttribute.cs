using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004B7 RID: 1207
	public sealed class GetSetAttribute : PropertyAttribute
	{
		// Token: 0x06001E80 RID: 7808 RVA: 0x0017F729 File Offset: 0x0017D929
		public GetSetAttribute(string name)
		{
			this.name = name;
		}

		// Token: 0x04003CF4 RID: 15604
		public readonly string name;

		// Token: 0x04003CF5 RID: 15605
		public bool dirty;
	}
}
