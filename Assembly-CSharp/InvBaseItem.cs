using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000028 RID: 40
[Serializable]
public class InvBaseItem
{
	// Token: 0x0400028E RID: 654
	public int id16;

	// Token: 0x0400028F RID: 655
	public string name;

	// Token: 0x04000290 RID: 656
	public string description;

	// Token: 0x04000291 RID: 657
	public InvBaseItem.Slot slot;

	// Token: 0x04000292 RID: 658
	public int minItemLevel = 1;

	// Token: 0x04000293 RID: 659
	public int maxItemLevel = 50;

	// Token: 0x04000294 RID: 660
	public List<InvStat> stats = new List<InvStat>();

	// Token: 0x04000295 RID: 661
	public GameObject attachment;

	// Token: 0x04000296 RID: 662
	public Color color = Color.white;

	// Token: 0x04000297 RID: 663
	public UnityEngine.Object iconAtlas;

	// Token: 0x04000298 RID: 664
	public string iconName = "";

	// Token: 0x0200061F RID: 1567
	public enum Slot
	{
		// Token: 0x0400452E RID: 17710
		None,
		// Token: 0x0400452F RID: 17711
		Weapon,
		// Token: 0x04004530 RID: 17712
		Shield,
		// Token: 0x04004531 RID: 17713
		Body,
		// Token: 0x04004532 RID: 17714
		Shoulders,
		// Token: 0x04004533 RID: 17715
		Bracers,
		// Token: 0x04004534 RID: 17716
		Boots,
		// Token: 0x04004535 RID: 17717
		Trinket,
		// Token: 0x04004536 RID: 17718
		_LastDoNotUse
	}
}
