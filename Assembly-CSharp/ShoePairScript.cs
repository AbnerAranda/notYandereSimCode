using System;
using UnityEngine;

// Token: 0x020003DC RID: 988
public class ShoePairScript : MonoBehaviour
{
	// Token: 0x06001A92 RID: 6802 RVA: 0x00105F33 File Offset: 0x00104133
	private void Start()
	{
		this.Police = GameObject.Find("Police").GetComponent<PoliceScript>();
		if (ClassGlobals.LanguageGrade + ClassGlobals.LanguageBonus < 1)
		{
			this.Prompt.enabled = false;
		}
		this.Note.SetActive(false);
	}

	// Token: 0x06001A93 RID: 6803 RVA: 0x00105F70 File Offset: 0x00104170
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			this.Police.Suicide = true;
			this.Note.SetActive(true);
		}
	}

	// Token: 0x04002A69 RID: 10857
	public PoliceScript Police;

	// Token: 0x04002A6A RID: 10858
	public PromptScript Prompt;

	// Token: 0x04002A6B RID: 10859
	public GameObject Note;
}
