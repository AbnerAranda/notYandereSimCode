using System;
using UnityEngine;

// Token: 0x0200023D RID: 573
public class ClothingScript : MonoBehaviour
{
	// Token: 0x06001261 RID: 4705 RVA: 0x00084AC0 File Offset: 0x00082CC0
	private void Start()
	{
		this.Yandere = GameObject.Find("YandereChan").GetComponent<YandereScript>();
	}

	// Token: 0x06001262 RID: 4706 RVA: 0x00084AD8 File Offset: 0x00082CD8
	private void Update()
	{
		if (this.CanPickUp)
		{
			if (this.Yandere.Bloodiness == 0f)
			{
				this.CanPickUp = false;
				this.Prompt.Hide();
				this.Prompt.enabled = false;
			}
		}
		else if (this.Yandere.Bloodiness > 0f)
		{
			this.CanPickUp = true;
			this.Prompt.enabled = true;
		}
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.Bloodiness = 0f;
			UnityEngine.Object.Instantiate<GameObject>(this.FoldedUniform, base.transform.position + Vector3.up, Quaternion.identity);
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04001608 RID: 5640
	public YandereScript Yandere;

	// Token: 0x04001609 RID: 5641
	public PromptScript Prompt;

	// Token: 0x0400160A RID: 5642
	public GameObject FoldedUniform;

	// Token: 0x0400160B RID: 5643
	public bool CanPickUp;
}
