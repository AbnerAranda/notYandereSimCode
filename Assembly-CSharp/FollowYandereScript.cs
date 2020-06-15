using System;
using UnityEngine;

// Token: 0x020002A7 RID: 679
public class FollowYandereScript : MonoBehaviour
{
	// Token: 0x0600141F RID: 5151 RVA: 0x000B13E8 File Offset: 0x000AF5E8
	private void Update()
	{
		base.transform.position = new Vector3(this.Yandere.position.x, base.transform.position.y, this.Yandere.position.z);
	}

	// Token: 0x04001C7E RID: 7294
	public Transform Yandere;
}
