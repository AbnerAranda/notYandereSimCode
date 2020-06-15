using System;
using UnityEngine;

// Token: 0x020003D6 RID: 982
public class SenpaiShrineCollectibleScript : MonoBehaviour
{
	// Token: 0x06001A7C RID: 6780 RVA: 0x0010455E File Offset: 0x0010275E
	private void Start()
	{
		if (PlayerGlobals.GetShrineCollectible(this.ID))
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06001A7D RID: 6781 RVA: 0x00104578 File Offset: 0x00102778
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.Inventory.ShrineCollectibles[this.ID] = true;
			this.Prompt.Hide();
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04002A20 RID: 10784
	public PromptScript Prompt;

	// Token: 0x04002A21 RID: 10785
	public int ID;
}
