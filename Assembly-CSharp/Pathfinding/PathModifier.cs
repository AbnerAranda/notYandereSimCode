using System;

namespace Pathfinding
{
	// Token: 0x02000598 RID: 1432
	[Serializable]
	public abstract class PathModifier : IPathModifier
	{
		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x060026A3 RID: 9891
		public abstract int Order { get; }

		// Token: 0x060026A4 RID: 9892 RVA: 0x001ABAB3 File Offset: 0x001A9CB3
		public void Awake(Seeker seeker)
		{
			this.seeker = seeker;
			if (seeker != null)
			{
				seeker.RegisterModifier(this);
			}
		}

		// Token: 0x060026A5 RID: 9893 RVA: 0x001ABACC File Offset: 0x001A9CCC
		public void OnDestroy(Seeker seeker)
		{
			if (seeker != null)
			{
				seeker.DeregisterModifier(this);
			}
		}

		// Token: 0x060026A6 RID: 9894 RVA: 0x00002ACE File Offset: 0x00000CCE
		public virtual void PreProcess(Path path)
		{
		}

		// Token: 0x060026A7 RID: 9895
		public abstract void Apply(Path path);

		// Token: 0x0400420E RID: 16910
		[NonSerialized]
		public Seeker seeker;
	}
}
