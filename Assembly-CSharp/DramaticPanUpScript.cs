using System;
using UnityEngine;

// Token: 0x02000272 RID: 626
public class DramaticPanUpScript : MonoBehaviour
{
	// Token: 0x0600136B RID: 4971 RVA: 0x000A7854 File Offset: 0x000A5A54
	private void Update()
	{
		if (Input.GetKeyDown("space"))
		{
			this.Pan = true;
		}
		if (this.Pan)
		{
			this.Power += Time.deltaTime * 0.5f;
			this.Height = Mathf.Lerp(this.Height, 1.4f, this.Power * Time.deltaTime);
			base.transform.localPosition = new Vector3(0f, this.Height, 1f);
		}
	}

	// Token: 0x04001A8D RID: 6797
	public bool Pan;

	// Token: 0x04001A8E RID: 6798
	public float Height;

	// Token: 0x04001A8F RID: 6799
	public float Power;
}
