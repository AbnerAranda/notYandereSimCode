using System;
using UnityEngine;

// Token: 0x020002F3 RID: 755
public class HomePantiesScript : MonoBehaviour
{
	// Token: 0x0600174A RID: 5962 RVA: 0x000C83E4 File Offset: 0x000C65E4
	private void Start()
	{
		if (this.ID > 0 && !CollectibleGlobals.GetPantyPurchased(this.ID))
		{
			this.MyRenderer.material = this.Unselectable;
			this.MyRenderer.material.color = new Color(0f, 0f, 0f, 0.5f);
		}
	}

	// Token: 0x0600174B RID: 5963 RVA: 0x000C8444 File Offset: 0x000C6644
	private void Update()
	{
		float y = (this.PantyChanger.Selected == this.ID) ? (base.transform.eulerAngles.y + Time.deltaTime * this.RotationSpeed) : 0f;
		base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, y, base.transform.eulerAngles.z);
	}

	// Token: 0x04001FFA RID: 8186
	public HomePantyChangerScript PantyChanger;

	// Token: 0x04001FFB RID: 8187
	public float RotationSpeed;

	// Token: 0x04001FFC RID: 8188
	public Material Unselectable;

	// Token: 0x04001FFD RID: 8189
	public Renderer MyRenderer;

	// Token: 0x04001FFE RID: 8190
	public int ID;
}
