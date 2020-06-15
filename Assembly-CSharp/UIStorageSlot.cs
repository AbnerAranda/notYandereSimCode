using System;
using UnityEngine;

// Token: 0x02000026 RID: 38
[AddComponentMenu("NGUI/Examples/UI Storage Slot")]
public class UIStorageSlot : UIItemSlot
{
	// Token: 0x17000013 RID: 19
	// (get) Token: 0x060000EC RID: 236 RVA: 0x00011F49 File Offset: 0x00010149
	protected override InvGameItem observedItem
	{
		get
		{
			if (!(this.storage != null))
			{
				return null;
			}
			return this.storage.GetItem(this.slot);
		}
	}

	// Token: 0x060000ED RID: 237 RVA: 0x00011F6C File Offset: 0x0001016C
	protected override InvGameItem Replace(InvGameItem item)
	{
		if (!(this.storage != null))
		{
			return item;
		}
		return this.storage.Replace(this.slot, item);
	}

	// Token: 0x04000289 RID: 649
	public UIItemStorage storage;

	// Token: 0x0400028A RID: 650
	public int slot;
}
