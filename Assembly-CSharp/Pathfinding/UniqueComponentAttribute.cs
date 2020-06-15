using System;

namespace Pathfinding
{
	// Token: 0x020005A4 RID: 1444
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class UniqueComponentAttribute : Attribute
	{
		// Token: 0x04004264 RID: 16996
		public string tag;
	}
}
