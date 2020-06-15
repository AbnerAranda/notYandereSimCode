using System;
using UnityEngine;

// Token: 0x020002AC RID: 684
public class FramerateScript : MonoBehaviour
{
	// Token: 0x0600142C RID: 5164 RVA: 0x000B1CCB File Offset: 0x000AFECB
	private void Start()
	{
		this.timeleft = this.updateInterval;
	}

	// Token: 0x0600142D RID: 5165 RVA: 0x000B1CDC File Offset: 0x000AFEDC
	private void Update()
	{
		this.timeleft -= Time.deltaTime;
		this.accum += Time.timeScale / Time.deltaTime;
		this.frames++;
		if (this.timeleft <= 0f)
		{
			this.FPS = this.accum / (float)this.frames;
			int num = Mathf.Clamp((int)this.FPS, 0, Application.targetFrameRate);
			if (num > 0)
			{
				this.FPSLabel.text = "FPS: " + num.ToString();
			}
			this.timeleft = this.updateInterval;
			this.accum = 0f;
			this.frames = 0;
		}
	}

	// Token: 0x04001CA4 RID: 7332
	public float updateInterval = 0.5f;

	// Token: 0x04001CA5 RID: 7333
	private float accum;

	// Token: 0x04001CA6 RID: 7334
	private int frames;

	// Token: 0x04001CA7 RID: 7335
	private float timeleft;

	// Token: 0x04001CA8 RID: 7336
	public float FPS;

	// Token: 0x04001CA9 RID: 7337
	public UILabel FPSLabel;
}
