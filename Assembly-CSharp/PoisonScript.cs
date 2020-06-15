using System;
using UnityEngine;

// Token: 0x0200036E RID: 878
public class PoisonScript : MonoBehaviour
{
	// Token: 0x06001924 RID: 6436 RVA: 0x000ECA18 File Offset: 0x000EAC18
	public void Start()
	{
		base.gameObject.SetActive(ClassGlobals.ChemistryGrade + ClassGlobals.ChemistryBonus >= 1);
	}

	// Token: 0x06001925 RID: 6437 RVA: 0x000ECA38 File Offset: 0x000EAC38
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Yandere.Inventory.ChemicalPoison = true;
			this.Yandere.StudentManager.UpdateAllBentos();
			UnityEngine.Object.Destroy(base.gameObject);
			UnityEngine.Object.Destroy(this.Bottle);
		}
	}

	// Token: 0x040025EA RID: 9706
	public YandereScript Yandere;

	// Token: 0x040025EB RID: 9707
	public PromptScript Prompt;

	// Token: 0x040025EC RID: 9708
	public GameObject Bottle;
}
