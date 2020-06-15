using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004B9 RID: 1209
	public sealed class TrackballAttribute : PropertyAttribute
	{
		// Token: 0x06001E82 RID: 7810 RVA: 0x0017F747 File Offset: 0x0017D947
		public TrackballAttribute(string method)
		{
			this.method = method;
		}

		// Token: 0x04003CF7 RID: 15607
		public readonly string method;
	}
}
