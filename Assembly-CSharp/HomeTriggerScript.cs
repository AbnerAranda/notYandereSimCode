using System;
using UnityEngine;

// Token: 0x020002F9 RID: 761
public class HomeTriggerScript : MonoBehaviour
{
	// Token: 0x06001761 RID: 5985 RVA: 0x000CA578 File Offset: 0x000C8778
	private void Start()
	{
		this.Label.color = new Color(this.Label.color.r, this.Label.color.g, this.Label.color.b, 0f);
	}

	// Token: 0x06001762 RID: 5986 RVA: 0x000CA5CA File Offset: 0x000C87CA
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			this.HomeCamera.ID = this.ID;
			this.FadeIn = true;
		}
	}

	// Token: 0x06001763 RID: 5987 RVA: 0x000CA5F6 File Offset: 0x000C87F6
	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			this.HomeCamera.ID = 0;
			this.FadeIn = false;
		}
	}

	// Token: 0x06001764 RID: 5988 RVA: 0x000CA620 File Offset: 0x000C8820
	private void Update()
	{
		this.Label.color = new Color(this.Label.color.r, this.Label.color.g, this.Label.color.b, Mathf.MoveTowards(this.Label.color.a, this.FadeIn ? 1f : 0f, Time.deltaTime * 10f));
	}

	// Token: 0x06001765 RID: 5989 RVA: 0x000CA6A4 File Offset: 0x000C88A4
	public void Disable()
	{
		this.Label.color = new Color(this.Label.color.r, this.Label.color.g, this.Label.color.b, 0f);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04002079 RID: 8313
	public HomeCameraScript HomeCamera;

	// Token: 0x0400207A RID: 8314
	public UILabel Label;

	// Token: 0x0400207B RID: 8315
	public bool FadeIn;

	// Token: 0x0400207C RID: 8316
	public int ID;
}
