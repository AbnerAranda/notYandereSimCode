using System;
using UnityEngine;

// Token: 0x020003A7 RID: 935
public class RivalPhoneScript : MonoBehaviour
{
	// Token: 0x060019E5 RID: 6629 RVA: 0x000FE1F6 File Offset: 0x000FC3F6
	private void Start()
	{
		this.OriginalParent = base.transform.parent;
		this.OriginalPosition = base.transform.localPosition;
		this.OriginalRotation = base.transform.localRotation;
	}

	// Token: 0x060019E6 RID: 6630 RVA: 0x000FE22C File Offset: 0x000FC42C
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			if (this.StudentID == this.Prompt.Yandere.StudentManager.RivalID && SchemeGlobals.GetSchemeStage(1) == 4)
			{
				SchemeGlobals.SetSchemeStage(1, 5);
				this.Prompt.Yandere.PauseScreen.Schemes.UpdateInstructions();
			}
			this.Prompt.Yandere.RivalPhoneTexture = this.MyRenderer.material.mainTexture;
			this.Prompt.Yandere.Inventory.RivalPhone = true;
			this.Prompt.Yandere.Inventory.RivalPhoneID = this.StudentID;
			this.Prompt.enabled = false;
			base.enabled = false;
			this.StolenPhoneDropoff.Prompt.enabled = true;
			this.StolenPhoneDropoff.Phase = 1;
			this.StolenPhoneDropoff.Timer = 0f;
			this.StolenPhoneDropoff.Prompt.Label[0].text = "     Provide Stolen Phone";
			base.gameObject.SetActive(false);
			this.Stolen = true;
		}
	}

	// Token: 0x060019E7 RID: 6631 RVA: 0x000FE35C File Offset: 0x000FC55C
	public void ReturnToOrigin()
	{
		base.transform.parent = this.OriginalParent;
		base.transform.localPosition = this.OriginalPosition;
		base.transform.localRotation = this.OriginalRotation;
		base.gameObject.SetActive(false);
		this.Prompt.enabled = true;
		this.LewdPhotos = false;
		this.Stolen = false;
		base.enabled = true;
	}

	// Token: 0x040028A2 RID: 10402
	public DoorGapScript StolenPhoneDropoff;

	// Token: 0x040028A3 RID: 10403
	public Renderer MyRenderer;

	// Token: 0x040028A4 RID: 10404
	public PromptScript Prompt;

	// Token: 0x040028A5 RID: 10405
	public bool LewdPhotos;

	// Token: 0x040028A6 RID: 10406
	public bool Stolen;

	// Token: 0x040028A7 RID: 10407
	public int StudentID;

	// Token: 0x040028A8 RID: 10408
	public Vector3 OriginalPosition;

	// Token: 0x040028A9 RID: 10409
	public Quaternion OriginalRotation;

	// Token: 0x040028AA RID: 10410
	public Transform OriginalParent;
}
