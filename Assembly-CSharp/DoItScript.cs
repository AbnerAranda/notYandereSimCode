using System;
using UnityEngine;

// Token: 0x0200026C RID: 620
public class DoItScript : MonoBehaviour
{
	// Token: 0x06001352 RID: 4946 RVA: 0x000A516B File Offset: 0x000A336B
	private void Start()
	{
		this.MyLabel.fontSize = UnityEngine.Random.Range(50, 100);
	}

	// Token: 0x06001353 RID: 4947 RVA: 0x000A5184 File Offset: 0x000A3384
	private void Update()
	{
		base.transform.localScale += new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
		if (!this.Fade)
		{
			this.MyLabel.alpha += Time.deltaTime;
			if (this.MyLabel.alpha >= 1f)
			{
				this.Fade = true;
				return;
			}
		}
		else
		{
			this.MyLabel.alpha -= Time.deltaTime;
			if (this.MyLabel.alpha <= 0f)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	// Token: 0x04001A4E RID: 6734
	public UILabel MyLabel;

	// Token: 0x04001A4F RID: 6735
	public bool Fade;
}
