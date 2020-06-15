using System;
using UnityEngine;

// Token: 0x02000380 RID: 896
public class PromptBarScript : MonoBehaviour
{
	// Token: 0x06001969 RID: 6505 RVA: 0x000F55B4 File Offset: 0x000F37B4
	private void Awake()
	{
		base.transform.localPosition = new Vector3(base.transform.localPosition.x, -627f, base.transform.localPosition.z);
		this.ID = 0;
		while (this.ID < this.Label.Length)
		{
			this.Label[this.ID].text = string.Empty;
			this.ID++;
		}
	}

	// Token: 0x0600196A RID: 6506 RVA: 0x000F5634 File Offset: 0x000F3834
	private void Start()
	{
		this.UpdateButtons();
	}

	// Token: 0x0600196B RID: 6507 RVA: 0x000F563C File Offset: 0x000F383C
	private void Update()
	{
		float t = Time.unscaledDeltaTime * 10f;
		if (!this.Show)
		{
			if (this.Panel.enabled)
			{
				base.transform.localPosition = new Vector3(base.transform.localPosition.x, Mathf.Lerp(base.transform.localPosition.y, -628f, t), base.transform.localPosition.z);
				if (base.transform.localPosition.y < -627f)
				{
					base.transform.localPosition = new Vector3(base.transform.localPosition.x, -628f, base.transform.localPosition.z);
					if (this.Panel != null)
					{
						this.Panel.enabled = false;
						return;
					}
				}
			}
		}
		else
		{
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, Mathf.Lerp(base.transform.localPosition.y, -528.5f, t), base.transform.localPosition.z);
		}
	}

	// Token: 0x0600196C RID: 6508 RVA: 0x000F5770 File Offset: 0x000F3970
	public void UpdateButtons()
	{
		if (this.Panel != null)
		{
			this.Panel.enabled = true;
		}
		this.ID = 0;
		while (this.ID < this.Label.Length)
		{
			this.Button[this.ID].enabled = (this.Label[this.ID].text.Length > 0);
			this.ID++;
		}
	}

	// Token: 0x0600196D RID: 6509 RVA: 0x000F57EC File Offset: 0x000F39EC
	public void ClearButtons()
	{
		this.ID = 0;
		while (this.ID < this.Label.Length)
		{
			this.Label[this.ID].text = string.Empty;
			this.Button[this.ID].enabled = false;
			this.ID++;
		}
	}

	// Token: 0x040026E0 RID: 9952
	public UISprite[] Button;

	// Token: 0x040026E1 RID: 9953
	public UILabel[] Label;

	// Token: 0x040026E2 RID: 9954
	public UIPanel Panel;

	// Token: 0x040026E3 RID: 9955
	public bool Show;

	// Token: 0x040026E4 RID: 9956
	public int ID;
}
