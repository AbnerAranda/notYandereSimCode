using System;

namespace Pathfinding
{
	// Token: 0x02000597 RID: 1431
	public interface IPathModifier
	{
		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x060026A0 RID: 9888
		int Order { get; }

		// Token: 0x060026A1 RID: 9889
		void Apply(Path path);

		// Token: 0x060026A2 RID: 9890
		void PreProcess(Path path);
	}
}
