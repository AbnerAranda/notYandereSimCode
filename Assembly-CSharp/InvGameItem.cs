using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200002B RID: 43
[Serializable]
public class InvGameItem
{
	// Token: 0x17000016 RID: 22
	// (get) Token: 0x06000105 RID: 261 RVA: 0x00012431 File Offset: 0x00010631
	public int baseItemID
	{
		get
		{
			return this.mBaseItemID;
		}
	}

	// Token: 0x17000017 RID: 23
	// (get) Token: 0x06000106 RID: 262 RVA: 0x00012439 File Offset: 0x00010639
	public InvBaseItem baseItem
	{
		get
		{
			if (this.mBaseItem == null)
			{
				this.mBaseItem = InvDatabase.FindByID(this.baseItemID);
			}
			return this.mBaseItem;
		}
	}

	// Token: 0x17000018 RID: 24
	// (get) Token: 0x06000107 RID: 263 RVA: 0x0001245A File Offset: 0x0001065A
	public string name
	{
		get
		{
			if (this.baseItem == null)
			{
				return null;
			}
			return this.quality.ToString() + " " + this.baseItem.name;
		}
	}

	// Token: 0x17000019 RID: 25
	// (get) Token: 0x06000108 RID: 264 RVA: 0x0001248C File Offset: 0x0001068C
	public float statMultiplier
	{
		get
		{
			float num = 0f;
			switch (this.quality)
			{
			case InvGameItem.Quality.Broken:
				num = 0f;
				break;
			case InvGameItem.Quality.Cursed:
				num = -1f;
				break;
			case InvGameItem.Quality.Damaged:
				num = 0.25f;
				break;
			case InvGameItem.Quality.Worn:
				num = 0.9f;
				break;
			case InvGameItem.Quality.Sturdy:
				num = 1f;
				break;
			case InvGameItem.Quality.Polished:
				num = 1.1f;
				break;
			case InvGameItem.Quality.Improved:
				num = 1.25f;
				break;
			case InvGameItem.Quality.Crafted:
				num = 1.5f;
				break;
			case InvGameItem.Quality.Superior:
				num = 1.75f;
				break;
			case InvGameItem.Quality.Enchanted:
				num = 2f;
				break;
			case InvGameItem.Quality.Epic:
				num = 2.5f;
				break;
			case InvGameItem.Quality.Legendary:
				num = 3f;
				break;
			}
			float num2 = (float)this.itemLevel / 50f;
			return num * Mathf.Lerp(num2, num2 * num2, 0.5f);
		}
	}

	// Token: 0x1700001A RID: 26
	// (get) Token: 0x06000109 RID: 265 RVA: 0x0001255C File Offset: 0x0001075C
	public Color color
	{
		get
		{
			Color result = Color.white;
			switch (this.quality)
			{
			case InvGameItem.Quality.Broken:
				result = new Color(0.4f, 0.2f, 0.2f);
				break;
			case InvGameItem.Quality.Cursed:
				result = Color.red;
				break;
			case InvGameItem.Quality.Damaged:
				result = new Color(0.4f, 0.4f, 0.4f);
				break;
			case InvGameItem.Quality.Worn:
				result = new Color(0.7f, 0.7f, 0.7f);
				break;
			case InvGameItem.Quality.Sturdy:
				result = new Color(1f, 1f, 1f);
				break;
			case InvGameItem.Quality.Polished:
				result = NGUIMath.HexToColor(3774856959U);
				break;
			case InvGameItem.Quality.Improved:
				result = NGUIMath.HexToColor(2480359935U);
				break;
			case InvGameItem.Quality.Crafted:
				result = NGUIMath.HexToColor(1325334783U);
				break;
			case InvGameItem.Quality.Superior:
				result = NGUIMath.HexToColor(12255231U);
				break;
			case InvGameItem.Quality.Enchanted:
				result = NGUIMath.HexToColor(1937178111U);
				break;
			case InvGameItem.Quality.Epic:
				result = NGUIMath.HexToColor(2516647935U);
				break;
			case InvGameItem.Quality.Legendary:
				result = NGUIMath.HexToColor(4287627519U);
				break;
			}
			return result;
		}
	}

	// Token: 0x0600010A RID: 266 RVA: 0x0001267C File Offset: 0x0001087C
	public InvGameItem(int id)
	{
		this.mBaseItemID = id;
	}

	// Token: 0x0600010B RID: 267 RVA: 0x00012699 File Offset: 0x00010899
	public InvGameItem(int id, InvBaseItem bi)
	{
		this.mBaseItemID = id;
		this.mBaseItem = bi;
	}

	// Token: 0x0600010C RID: 268 RVA: 0x000126C0 File Offset: 0x000108C0
	public List<InvStat> CalculateStats()
	{
		List<InvStat> list = new List<InvStat>();
		if (this.baseItem != null)
		{
			float statMultiplier = this.statMultiplier;
			List<InvStat> stats = this.baseItem.stats;
			int i = 0;
			int count = stats.Count;
			while (i < count)
			{
				InvStat invStat = stats[i];
				int num = Mathf.RoundToInt(statMultiplier * (float)invStat.amount);
				if (num != 0)
				{
					bool flag = false;
					int j = 0;
					int count2 = list.Count;
					while (j < count2)
					{
						InvStat invStat2 = list[j];
						if (invStat2.id == invStat.id && invStat2.modifier == invStat.modifier)
						{
							invStat2.amount += num;
							flag = true;
							break;
						}
						j++;
					}
					if (!flag)
					{
						list.Add(new InvStat
						{
							id = invStat.id,
							amount = num,
							modifier = invStat.modifier
						});
					}
				}
				i++;
			}
			list.Sort(new Comparison<InvStat>(InvStat.CompareArmor));
		}
		return list;
	}

	// Token: 0x040002A0 RID: 672
	[SerializeField]
	private int mBaseItemID;

	// Token: 0x040002A1 RID: 673
	public InvGameItem.Quality quality = InvGameItem.Quality.Sturdy;

	// Token: 0x040002A2 RID: 674
	public int itemLevel = 1;

	// Token: 0x040002A3 RID: 675
	private InvBaseItem mBaseItem;

	// Token: 0x02000620 RID: 1568
	public enum Quality
	{
		// Token: 0x04004538 RID: 17720
		Broken,
		// Token: 0x04004539 RID: 17721
		Cursed,
		// Token: 0x0400453A RID: 17722
		Damaged,
		// Token: 0x0400453B RID: 17723
		Worn,
		// Token: 0x0400453C RID: 17724
		Sturdy,
		// Token: 0x0400453D RID: 17725
		Polished,
		// Token: 0x0400453E RID: 17726
		Improved,
		// Token: 0x0400453F RID: 17727
		Crafted,
		// Token: 0x04004540 RID: 17728
		Superior,
		// Token: 0x04004541 RID: 17729
		Enchanted,
		// Token: 0x04004542 RID: 17730
		Epic,
		// Token: 0x04004543 RID: 17731
		Legendary,
		// Token: 0x04004544 RID: 17732
		_LastDoNotUse
	}
}
