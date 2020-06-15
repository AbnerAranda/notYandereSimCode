using System;
using UnityEngine;

// Token: 0x02000288 RID: 648
[Serializable]
public abstract class Entity
{
	// Token: 0x060013C3 RID: 5059 RVA: 0x000AD5DE File Offset: 0x000AB7DE
	public Entity(GenderType gender)
	{
		this.gender = gender;
		this.deathType = DeathType.None;
	}

	// Token: 0x17000370 RID: 880
	// (get) Token: 0x060013C4 RID: 5060 RVA: 0x000AD5F4 File Offset: 0x000AB7F4
	public GenderType Gender
	{
		get
		{
			return this.gender;
		}
	}

	// Token: 0x17000371 RID: 881
	// (get) Token: 0x060013C5 RID: 5061 RVA: 0x000AD5FC File Offset: 0x000AB7FC
	// (set) Token: 0x060013C6 RID: 5062 RVA: 0x000AD604 File Offset: 0x000AB804
	public DeathType DeathType
	{
		get
		{
			return this.deathType;
		}
		set
		{
			this.deathType = value;
		}
	}

	// Token: 0x17000372 RID: 882
	// (get) Token: 0x060013C7 RID: 5063
	public abstract EntityType EntityType { get; }

	// Token: 0x04001B8B RID: 7051
	[SerializeField]
	private GenderType gender;

	// Token: 0x04001B8C RID: 7052
	[SerializeField]
	private DeathType deathType;
}
