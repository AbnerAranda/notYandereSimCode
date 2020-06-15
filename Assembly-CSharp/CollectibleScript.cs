using System;
using UnityEngine;

// Token: 0x02000242 RID: 578
public class CollectibleScript : MonoBehaviour
{
	// Token: 0x06001273 RID: 4723 RVA: 0x00087740 File Offset: 0x00085940
	private void Start()
	{
		if ((this.CollectibleType == CollectibleType.BasementTape && CollectibleGlobals.GetBasementTapeCollected(this.ID)) || (this.CollectibleType == CollectibleType.Manga && CollectibleGlobals.GetMangaCollected(this.ID)) || (this.CollectibleType == CollectibleType.Tape && CollectibleGlobals.GetTapeCollected(this.ID)) || (this.CollectibleType == CollectibleType.Panty && CollectibleGlobals.GetPantyPurchased(11)))
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		if (GameGlobals.LoveSick || MissionModeGlobals.MissionMode)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x17000344 RID: 836
	// (get) Token: 0x06001274 RID: 4724 RVA: 0x000877CC File Offset: 0x000859CC
	public CollectibleType CollectibleType
	{
		get
		{
			if (this.Name == "HeadmasterTape")
			{
				return CollectibleType.HeadmasterTape;
			}
			if (this.Name == "BasementTape")
			{
				return CollectibleType.BasementTape;
			}
			if (this.Name == "Manga")
			{
				return CollectibleType.Manga;
			}
			if (this.Name == "Tape")
			{
				return CollectibleType.Tape;
			}
			if (this.Type == 5)
			{
				return CollectibleType.Key;
			}
			if (this.Type == 6)
			{
				return CollectibleType.Panty;
			}
			Debug.LogError("Unrecognized collectible \"" + this.Name + "\".", base.gameObject);
			return CollectibleType.Tape;
		}
	}

	// Token: 0x06001275 RID: 4725 RVA: 0x00087860 File Offset: 0x00085A60
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			if (this.CollectibleType == CollectibleType.HeadmasterTape)
			{
				CollectibleGlobals.SetHeadmasterTapeCollected(this.ID, true);
			}
			else if (this.CollectibleType == CollectibleType.BasementTape)
			{
				CollectibleGlobals.SetBasementTapeCollected(this.ID, true);
			}
			else if (this.CollectibleType == CollectibleType.Manga)
			{
				CollectibleGlobals.SetMangaCollected(this.ID, true);
			}
			else if (this.CollectibleType == CollectibleType.Tape)
			{
				CollectibleGlobals.SetTapeCollected(this.ID, true);
			}
			else if (this.CollectibleType == CollectibleType.Key)
			{
				this.Prompt.Yandere.Inventory.MysteriousKeys++;
			}
			else if (this.CollectibleType == CollectibleType.Panty)
			{
				CollectibleGlobals.SetPantyPurchased(11, true);
			}
			else
			{
				Debug.LogError("Collectible \"" + this.Name + "\" not implemented.", base.gameObject);
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04001679 RID: 5753
	public PromptScript Prompt;

	// Token: 0x0400167A RID: 5754
	public string Name = string.Empty;

	// Token: 0x0400167B RID: 5755
	public int Type;

	// Token: 0x0400167C RID: 5756
	public int ID;
}
