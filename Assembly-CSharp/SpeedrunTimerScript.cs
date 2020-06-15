using System;
using UnityEngine;

// Token: 0x020003EE RID: 1006
public class SpeedrunTimerScript : MonoBehaviour
{
	// Token: 0x06001ADF RID: 6879 RVA: 0x0010EC2D File Offset: 0x0010CE2D
	private void Start()
	{
		this.Label.enabled = false;
	}

	// Token: 0x06001AE0 RID: 6880 RVA: 0x0010EC3C File Offset: 0x0010CE3C
	private void Update()
	{
		if (!this.Police.FadeOut)
		{
			this.Timer += Time.deltaTime;
			if (this.Label.enabled)
			{
				this.Label.text = (this.FormatTime(this.Timer) ?? "");
			}
			if (Input.GetKeyDown(KeyCode.Delete))
			{
				this.Label.enabled = !this.Label.enabled;
			}
		}
	}

	// Token: 0x06001AE1 RID: 6881 RVA: 0x0010ECB8 File Offset: 0x0010CEB8
	private string FormatTime(float time)
	{
		int num = (int)time;
		int num2 = num / 60;
		int num3 = num % 60;
		float num4 = time * 1000f;
		num4 %= 1000f;
		return string.Format("{0:00}:{1:00}:{2:000}", num2, num3, num4);
	}

	// Token: 0x04002B9D RID: 11165
	public PoliceScript Police;

	// Token: 0x04002B9E RID: 11166
	public UILabel Label;

	// Token: 0x04002B9F RID: 11167
	public float Timer;
}
