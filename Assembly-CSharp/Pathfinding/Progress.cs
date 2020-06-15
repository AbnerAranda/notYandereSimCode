using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200055B RID: 1371
	public struct Progress
	{
		// Token: 0x0600244B RID: 9291 RVA: 0x0019BB15 File Offset: 0x00199D15
		public Progress(float progress, string description)
		{
			this.progress = progress;
			this.description = description;
		}

		// Token: 0x0600244C RID: 9292 RVA: 0x0019BB25 File Offset: 0x00199D25
		public Progress MapTo(float min, float max, string prefix = null)
		{
			return new Progress(Mathf.Lerp(min, max, this.progress), prefix + this.description);
		}

		// Token: 0x0600244D RID: 9293 RVA: 0x0019BB48 File Offset: 0x00199D48
		public override string ToString()
		{
			return this.progress.ToString("0.0") + " " + this.description;
		}

		// Token: 0x040040A0 RID: 16544
		public readonly float progress;

		// Token: 0x040040A1 RID: 16545
		public readonly string description;
	}
}
