using System;
using UnityEngine;

// Token: 0x020003E0 RID: 992
public class SimpleDetectClickScript : MonoBehaviour
{
	// Token: 0x06001AAF RID: 6831 RVA: 0x0010AF90 File Offset: 0x00109190
	private void Update()
	{
		RaycastHit raycastHit;
		if (Input.GetMouseButtonDown(0) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, 100f) && raycastHit.collider == this.MyCollider)
		{
			this.Clicked = true;
		}
	}

	// Token: 0x04002AFA RID: 11002
	public InventoryItemScript InventoryItem;

	// Token: 0x04002AFB RID: 11003
	public Collider MyCollider;

	// Token: 0x04002AFC RID: 11004
	public bool Clicked;
}
