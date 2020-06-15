using System;

namespace Pathfinding
{
	// Token: 0x02000599 RID: 1433
	[Serializable]
	public abstract class MonoModifier : VersionedMonoBehaviour, IPathModifier
	{
		// Token: 0x060026A9 RID: 9897 RVA: 0x001ABADE File Offset: 0x001A9CDE
		protected virtual void OnEnable()
		{
			this.seeker = base.GetComponent<Seeker>();
			if (this.seeker != null)
			{
				this.seeker.RegisterModifier(this);
			}
		}

		// Token: 0x060026AA RID: 9898 RVA: 0x001ABB06 File Offset: 0x001A9D06
		protected virtual void OnDisable()
		{
			if (this.seeker != null)
			{
				this.seeker.DeregisterModifier(this);
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x060026AB RID: 9899
		public abstract int Order { get; }

		// Token: 0x060026AC RID: 9900 RVA: 0x00002ACE File Offset: 0x00000CCE
		public virtual void PreProcess(Path path)
		{
		}

		// Token: 0x060026AD RID: 9901
		public abstract void Apply(Path path);

		// Token: 0x0400420F RID: 16911
		[NonSerialized]
		public Seeker seeker;
	}
}
