using System;
using UnityEngine;

// Token: 0x0200033D RID: 829
public class MusicCreditScript : MonoBehaviour
{
	// Token: 0x06001862 RID: 6242 RVA: 0x000DC0A8 File Offset: 0x000DA2A8
	private void Start()
	{
		base.transform.localPosition = new Vector3(400f, base.transform.localPosition.y, base.transform.localPosition.z);
		this.Panel.enabled = false;
	}

	// Token: 0x06001863 RID: 6243 RVA: 0x000DC0F8 File Offset: 0x000DA2F8
	private void Update()
	{
		if (this.Slide)
		{
			this.Timer += Time.deltaTime;
			if (this.Timer < 5f)
			{
				base.transform.localPosition = new Vector3(Mathf.Lerp(base.transform.localPosition.x, 0f, Time.deltaTime * 10f), base.transform.localPosition.y, base.transform.localPosition.z);
				return;
			}
			base.transform.localPosition = new Vector3(base.transform.localPosition.x + Time.deltaTime, base.transform.localPosition.y, base.transform.localPosition.z);
			base.transform.localPosition = new Vector3(base.transform.localPosition.x + Mathf.Abs(base.transform.localPosition.x * 0.01f) * (Time.deltaTime * 1000f), base.transform.localPosition.y, base.transform.localPosition.z);
			if (base.transform.localPosition.x > 400f)
			{
				base.transform.localPosition = new Vector3(400f, base.transform.localPosition.y, base.transform.localPosition.z);
				this.Panel.enabled = false;
				this.Slide = false;
				this.Timer = 0f;
			}
		}
	}

	// Token: 0x0400238D RID: 9101
	public UILabel SongLabel;

	// Token: 0x0400238E RID: 9102
	public UILabel BandLabel;

	// Token: 0x0400238F RID: 9103
	public UIPanel Panel;

	// Token: 0x04002390 RID: 9104
	public bool Slide;

	// Token: 0x04002391 RID: 9105
	public float Timer;
}
