﻿using System;
using UnityEngine;

// Token: 0x02000020 RID: 32
[AddComponentMenu("NGUI/Examples/Equip Items")]
public class EquipItems : MonoBehaviour
{
	// Token: 0x060000D0 RID: 208 RVA: 0x00011568 File Offset: 0x0000F768
	private void Start()
	{
		if (this.itemIDs != null && this.itemIDs.Length != 0)
		{
			InvEquipment invEquipment = base.GetComponent<InvEquipment>();
			if (invEquipment == null)
			{
				invEquipment = base.gameObject.AddComponent<InvEquipment>();
			}
			int max = 12;
			int i = 0;
			int num = this.itemIDs.Length;
			while (i < num)
			{
				int num2 = this.itemIDs[i];
				InvBaseItem invBaseItem = InvDatabase.FindByID(num2);
				if (invBaseItem != null)
				{
					invEquipment.Equip(new InvGameItem(num2, invBaseItem)
					{
						quality = (InvGameItem.Quality)UnityEngine.Random.Range(0, max),
						itemLevel = NGUITools.RandomRange(invBaseItem.minItemLevel, invBaseItem.maxItemLevel)
					});
				}
				else
				{
					Debug.LogWarning("Can't resolve the item ID of " + num2);
				}
				i++;
			}
		}
		UnityEngine.Object.Destroy(this);
	}

	// Token: 0x0400026E RID: 622
	public int[] itemIDs;
}
