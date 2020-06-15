using System;
using UnityEngine;

// Token: 0x020002EA RID: 746
public class HomeCorkboardPhotoScript : MonoBehaviour
{
	// Token: 0x06001728 RID: 5928 RVA: 0x000C4C00 File Offset: 0x000C2E00
	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.layer == 4)
		{
			base.transform.localScale = new Vector3(Mathf.MoveTowards(base.transform.localScale.x, 1f, Time.deltaTime * 10f), Mathf.MoveTowards(base.transform.localScale.y, 1f, Time.deltaTime * 10f), Mathf.MoveTowards(base.transform.localScale.z, 1f, Time.deltaTime * 10f));
		}
	}

	// Token: 0x04001F79 RID: 8057
	public int ArrayID;

	// Token: 0x04001F7A RID: 8058
	public int ID;
}
