using System;
using UnityEngine;

// Token: 0x0200032A RID: 810
public class MaskScript : MonoBehaviour
{
	// Token: 0x06001826 RID: 6182 RVA: 0x000D8454 File Offset: 0x000D6654
	private void Start()
	{
		if (GameGlobals.MasksBanned)
		{
			base.gameObject.SetActive(false);
		}
		else
		{
			this.MyFilter.mesh = this.Meshes[this.ID];
			this.MyRenderer.material.mainTexture = this.Textures[this.ID];
		}
		base.enabled = false;
	}

	// Token: 0x06001827 RID: 6183 RVA: 0x000D84B4 File Offset: 0x000D66B4
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			this.StudentManager.CanAnyoneSeeYandere();
			if (!this.StudentManager.YandereVisible && !this.Yandere.Chased && this.Yandere.Chasers == 0)
			{
				Rigidbody component = base.GetComponent<Rigidbody>();
				component.useGravity = false;
				component.isKinematic = true;
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				this.Prompt.MyCollider.enabled = false;
				base.transform.parent = this.Yandere.Head;
				base.transform.localPosition = new Vector3(0f, 0.033333f, 0.1f);
				base.transform.localEulerAngles = Vector3.zero;
				this.Yandere.Mask = this;
				this.ClubManager.UpdateMasks();
				this.StudentManager.UpdateStudents(0);
				return;
			}
			this.Yandere.NotificationManager.CustomText = "Not now. Too suspicious.";
			this.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
		}
	}

	// Token: 0x06001828 RID: 6184 RVA: 0x000D85FC File Offset: 0x000D67FC
	public void Drop()
	{
		this.Prompt.MyCollider.isTrigger = false;
		this.Prompt.MyCollider.enabled = true;
		Rigidbody component = base.GetComponent<Rigidbody>();
		component.useGravity = true;
		component.isKinematic = false;
		this.Prompt.enabled = true;
		base.transform.parent = null;
		this.Yandere.Mask = null;
		this.ClubManager.UpdateMasks();
		this.StudentManager.UpdateStudents(0);
	}

	// Token: 0x0400230E RID: 8974
	public StudentManagerScript StudentManager;

	// Token: 0x0400230F RID: 8975
	public ClubManagerScript ClubManager;

	// Token: 0x04002310 RID: 8976
	public YandereScript Yandere;

	// Token: 0x04002311 RID: 8977
	public PromptScript Prompt;

	// Token: 0x04002312 RID: 8978
	public PickUpScript PickUp;

	// Token: 0x04002313 RID: 8979
	public Projector Blood;

	// Token: 0x04002314 RID: 8980
	public Renderer MyRenderer;

	// Token: 0x04002315 RID: 8981
	public MeshFilter MyFilter;

	// Token: 0x04002316 RID: 8982
	public Texture[] Textures;

	// Token: 0x04002317 RID: 8983
	public Mesh[] Meshes;

	// Token: 0x04002318 RID: 8984
	public int ID;
}
