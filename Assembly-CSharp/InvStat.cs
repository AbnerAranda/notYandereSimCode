using System;

// Token: 0x0200002C RID: 44
[Serializable]
public class InvStat
{
	// Token: 0x0600010D RID: 269 RVA: 0x000127D3 File Offset: 0x000109D3
	public static string GetName(InvStat.Identifier i)
	{
		return i.ToString();
	}

	// Token: 0x0600010E RID: 270 RVA: 0x000127E4 File Offset: 0x000109E4
	public static string GetDescription(InvStat.Identifier i)
	{
		switch (i)
		{
		case InvStat.Identifier.Strength:
			return "Strength increases melee damage";
		case InvStat.Identifier.Constitution:
			return "Constitution increases health";
		case InvStat.Identifier.Agility:
			return "Agility increases armor";
		case InvStat.Identifier.Intelligence:
			return "Intelligence increases mana";
		case InvStat.Identifier.Damage:
			return "Damage adds to the amount of damage done in combat";
		case InvStat.Identifier.Crit:
			return "Crit increases the chance of landing a critical strike";
		case InvStat.Identifier.Armor:
			return "Armor protects from damage";
		case InvStat.Identifier.Health:
			return "Health prolongs life";
		case InvStat.Identifier.Mana:
			return "Mana increases the number of spells that can be cast";
		default:
			return null;
		}
	}

	// Token: 0x0600010F RID: 271 RVA: 0x00012854 File Offset: 0x00010A54
	public static int CompareArmor(InvStat a, InvStat b)
	{
		int num = (int)a.id;
		int num2 = (int)b.id;
		if (a.id == InvStat.Identifier.Armor)
		{
			num -= 10000;
		}
		else if (a.id == InvStat.Identifier.Damage)
		{
			num -= 5000;
		}
		if (b.id == InvStat.Identifier.Armor)
		{
			num2 -= 10000;
		}
		else if (b.id == InvStat.Identifier.Damage)
		{
			num2 -= 5000;
		}
		if (a.amount < 0)
		{
			num += 1000;
		}
		if (b.amount < 0)
		{
			num2 += 1000;
		}
		if (a.modifier == InvStat.Modifier.Percent)
		{
			num += 100;
		}
		if (b.modifier == InvStat.Modifier.Percent)
		{
			num2 += 100;
		}
		if (num < num2)
		{
			return -1;
		}
		if (num > num2)
		{
			return 1;
		}
		return 0;
	}

	// Token: 0x06000110 RID: 272 RVA: 0x00012904 File Offset: 0x00010B04
	public static int CompareWeapon(InvStat a, InvStat b)
	{
		int num = (int)a.id;
		int num2 = (int)b.id;
		if (a.id == InvStat.Identifier.Damage)
		{
			num -= 10000;
		}
		else if (a.id == InvStat.Identifier.Armor)
		{
			num -= 5000;
		}
		if (b.id == InvStat.Identifier.Damage)
		{
			num2 -= 10000;
		}
		else if (b.id == InvStat.Identifier.Armor)
		{
			num2 -= 5000;
		}
		if (a.amount < 0)
		{
			num += 1000;
		}
		if (b.amount < 0)
		{
			num2 += 1000;
		}
		if (a.modifier == InvStat.Modifier.Percent)
		{
			num += 100;
		}
		if (b.modifier == InvStat.Modifier.Percent)
		{
			num2 += 100;
		}
		if (num < num2)
		{
			return -1;
		}
		if (num > num2)
		{
			return 1;
		}
		return 0;
	}

	// Token: 0x040002A4 RID: 676
	public InvStat.Identifier id;

	// Token: 0x040002A5 RID: 677
	public InvStat.Modifier modifier;

	// Token: 0x040002A6 RID: 678
	public int amount;

	// Token: 0x02000621 RID: 1569
	public enum Identifier
	{
		// Token: 0x04004546 RID: 17734
		Strength,
		// Token: 0x04004547 RID: 17735
		Constitution,
		// Token: 0x04004548 RID: 17736
		Agility,
		// Token: 0x04004549 RID: 17737
		Intelligence,
		// Token: 0x0400454A RID: 17738
		Damage,
		// Token: 0x0400454B RID: 17739
		Crit,
		// Token: 0x0400454C RID: 17740
		Armor,
		// Token: 0x0400454D RID: 17741
		Health,
		// Token: 0x0400454E RID: 17742
		Mana,
		// Token: 0x0400454F RID: 17743
		Other
	}

	// Token: 0x02000622 RID: 1570
	public enum Modifier
	{
		// Token: 0x04004551 RID: 17745
		Added,
		// Token: 0x04004552 RID: 17746
		Percent
	}
}
