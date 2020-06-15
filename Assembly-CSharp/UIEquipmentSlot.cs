using System;
using UnityEngine;

// Token: 0x02000023 RID: 35
[AddComponentMenu("NGUI/Examples/UI Equipment Slot")]
public class UIEquipmentSlot : UIItemSlot
{
	// Token: 0x17000010 RID: 16
	// (get) Token: 0x060000DB RID: 219 RVA: 0x0001191C File Offset: 0x0000FB1C
	protected override InvGameItem observedItem
	{
		get
		{
			if (!(this.equipment != null))
			{
				return null;
			}
			return this.equipment.GetItem(this.slot);
		}
	}

	// Token: 0x060000DC RID: 220 RVA: 0x0001193F File Offset: 0x0000FB3F
	protected override InvGameItem Replace(InvGameItem item)
	{
		if (!(this.equipment != null))
		{
			return item;
		}
		return this.equipment.Replace(this.slot, item);
	}

	// Token: 0x04000276 RID: 630
	public InvEquipment equipment;

	// Token: 0x04000277 RID: 631
	public InvBaseItem.Slot slot;
}
