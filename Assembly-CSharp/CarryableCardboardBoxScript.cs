using System;
using UnityEngine;

// Token: 0x0200022A RID: 554
public class CarryableCardboardBoxScript : MonoBehaviour
{
	// Token: 0x06001225 RID: 4645 RVA: 0x00080B70 File Offset: 0x0007ED70
	private void Update()
	{
		if (!this.Closed)
		{
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				this.Prompt.Label[0].text = "     Insert Box Cutter";
				this.MyRenderer.mesh = this.ClosedMesh;
				this.Prompt.HideButton[0] = true;
				this.Closed = true;
				return;
			}
		}
		else if (this.MyCutter == null)
		{
			this.Prompt.HideButton[0] = true;
			if (!this.Prompt.Yandere.Armed)
			{
				this.Prompt.HideButton[0] = true;
				return;
			}
			if (this.Prompt.Yandere.EquippedWeapon.WeaponID != 5 || this.Prompt.Yandere.EquippedWeapon.Blood.enabled)
			{
				this.Prompt.HideButton[0] = true;
				return;
			}
			this.Prompt.HideButton[0] = false;
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				this.MyCutter = this.Prompt.Yandere.EquippedWeapon;
				Physics.IgnoreCollision(base.GetComponent<Collider>(), this.MyCutter.MyCollider);
				this.Prompt.Yandere.DropTimer[this.Prompt.Yandere.Equipped] = 0.5f;
				this.Prompt.Yandere.DropWeapon(this.Prompt.Yandere.Equipped);
				this.MyCutter.MyRigidbody.useGravity = false;
				this.MyCutter.MyRigidbody.isKinematic = true;
				this.MyCutter.MyCollider.isTrigger = true;
				this.MyCutter.transform.parent = base.transform;
				this.MyCutter.transform.localPosition = new Vector3(0f, 0.24f, 0f);
				this.MyCutter.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
				this.MyCutter.Prompt.Hide();
				this.MyCutter.Prompt.enabled = false;
				this.MyCutter.enabled = false;
				this.MyCutter.gameObject.SetActive(true);
				this.Prompt.HideButton[0] = true;
				this.Prompt.HideButton[3] = false;
				this.PickUp.StuckBoxCutter = this.MyCutter;
				this.PickUp.enabled = true;
				return;
			}
		}
		else if (this.MyCutter.transform.parent != base.transform)
		{
			this.MyCutter = null;
		}
	}

	// Token: 0x04001562 RID: 5474
	public WeaponScript MyCutter;

	// Token: 0x04001563 RID: 5475
	public PickUpScript PickUp;

	// Token: 0x04001564 RID: 5476
	public PromptScript Prompt;

	// Token: 0x04001565 RID: 5477
	public MeshFilter MyRenderer;

	// Token: 0x04001566 RID: 5478
	public Mesh ClosedMesh;

	// Token: 0x04001567 RID: 5479
	public bool Closed;
}
