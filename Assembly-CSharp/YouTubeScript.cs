using System;
using UnityEngine;

// Token: 0x02000492 RID: 1170
public class YouTubeScript : MonoBehaviour
{
	// Token: 0x06001E1F RID: 7711 RVA: 0x0017AE6C File Offset: 0x0017906C
	private void Update()
	{
		if (Input.GetKeyDown("space"))
		{
			this.Begin = true;
		}
		if (this.Begin)
		{
			this.Strength += Time.deltaTime;
			base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, new Vector3(0f, 1.15f, 1f), Time.deltaTime * this.Strength);
		}
	}

	// Token: 0x04003C37 RID: 15415
	public float Strength;

	// Token: 0x04003C38 RID: 15416
	public bool Begin;
}
