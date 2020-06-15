using System;
using UnityEngine;

// Token: 0x020002D7 RID: 727
public class GloveScript : MonoBehaviour
{
	// Token: 0x060016E4 RID: 5860 RVA: 0x000BE258 File Offset: 0x000BC458
	private void Start()
	{
		Physics.IgnoreCollision(GameObject.Find("YandereChan").GetComponent<YandereScript>().GetComponent<Collider>(), this.MyCollider);
		if (base.transform.position.y > 1000f)
		{
			base.transform.position = new Vector3(12f, 0f, 28f);
		}
	}

	// Token: 0x060016E5 RID: 5861 RVA: 0x000BE2BC File Offset: 0x000BC4BC
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			base.transform.parent = this.Prompt.Yandere.transform;
			base.transform.localPosition = new Vector3(0f, 1f, 0.25f);
			this.Prompt.Yandere.Gloves = this;
			this.Prompt.Yandere.WearGloves();
			base.gameObject.SetActive(false);
		}
		this.Prompt.HideButton[0] = (this.Prompt.Yandere.Schoolwear != 1 || this.Prompt.Yandere.ClubAttire);
	}

	// Token: 0x04001E3F RID: 7743
	public PromptScript Prompt;

	// Token: 0x04001E40 RID: 7744
	public PickUpScript PickUp;

	// Token: 0x04001E41 RID: 7745
	public Collider MyCollider;

	// Token: 0x04001E42 RID: 7746
	public Projector Blood;
}
