using System;
using UnityEngine;

// Token: 0x020003FA RID: 1018
public class StandWeaponScript : MonoBehaviour
{
	// Token: 0x06001B06 RID: 6918 RVA: 0x00110978 File Offset: 0x0010EB78
	private void Update()
	{
		if (this.Prompt.enabled)
		{
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				this.MoveToStand();
				return;
			}
		}
		else
		{
			base.transform.Rotate(Vector3.forward * (Time.deltaTime * this.RotationSpeed));
			base.transform.Rotate(Vector3.right * (Time.deltaTime * this.RotationSpeed));
			base.transform.Rotate(Vector3.up * (Time.deltaTime * this.RotationSpeed));
		}
	}

	// Token: 0x06001B07 RID: 6919 RVA: 0x00110A18 File Offset: 0x0010EC18
	private void MoveToStand()
	{
		this.Prompt.Hide();
		this.Prompt.enabled = false;
		this.Prompt.MyCollider.enabled = false;
		this.Stand.Weapons++;
		base.transform.parent = this.Stand.Hands[this.Stand.Weapons];
		base.transform.localPosition = new Vector3(-0.277f, 0f, 0f);
	}

	// Token: 0x04002BFD RID: 11261
	public PromptScript Prompt;

	// Token: 0x04002BFE RID: 11262
	public StandScript Stand;

	// Token: 0x04002BFF RID: 11263
	public float RotationSpeed;
}
