using System;
using UnityEngine;

// Token: 0x020003F7 RID: 1015
public class StalkerIntroCameraScript : MonoBehaviour
{
	// Token: 0x06001AFD RID: 6909 RVA: 0x0011023C File Offset: 0x0010E43C
	private void Update()
	{
		if (this.YandereAnim["f02_wallJump_00"].time > this.YandereAnim["f02_wallJump_00"].length)
		{
			this.Speed += Time.deltaTime;
			this.Yandere.position = Vector3.Lerp(this.Yandere.position, new Vector3(14.33333f, 0f, 15f), Time.deltaTime * this.Speed);
			base.transform.position = Vector3.Lerp(base.transform.position, new Vector3(13.75f, 1.4f, 14.5f), Time.deltaTime * this.Speed);
			base.transform.eulerAngles = Vector3.Lerp(base.transform.eulerAngles, new Vector3(15f, 180f, 0f), Time.deltaTime * this.Speed);
		}
	}

	// Token: 0x04002BE9 RID: 11241
	public Animation YandereAnim;

	// Token: 0x04002BEA RID: 11242
	public Transform Yandere;

	// Token: 0x04002BEB RID: 11243
	public float Speed;
}
