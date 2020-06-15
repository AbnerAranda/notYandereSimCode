using System;
using UnityEngine;

// Token: 0x0200030A RID: 778
public class IntroCircleScript : MonoBehaviour
{
	// Token: 0x06001795 RID: 6037 RVA: 0x000CD49C File Offset: 0x000CB69C
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.ID < this.StartTime.Length && this.Timer > this.StartTime[this.ID])
		{
			this.CurrentTime = this.Duration[this.ID];
			this.LastTime = this.Duration[this.ID];
			this.Label.text = this.Text[this.ID];
			this.ID++;
		}
		if (this.CurrentTime > 0f)
		{
			this.CurrentTime -= Time.deltaTime;
		}
		if (this.Timer > 1f)
		{
			this.Sprite.fillAmount = this.CurrentTime / this.LastTime;
			if (this.Sprite.fillAmount == 0f)
			{
				this.Label.text = string.Empty;
			}
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			this.CurrentTime -= 5f;
			this.Timer += 5f;
		}
	}

	// Token: 0x0400211C RID: 8476
	public UISprite Sprite;

	// Token: 0x0400211D RID: 8477
	public UILabel Label;

	// Token: 0x0400211E RID: 8478
	public float[] StartTime;

	// Token: 0x0400211F RID: 8479
	public float[] Duration;

	// Token: 0x04002120 RID: 8480
	public string[] Text;

	// Token: 0x04002121 RID: 8481
	public float CurrentTime;

	// Token: 0x04002122 RID: 8482
	public float LastTime;

	// Token: 0x04002123 RID: 8483
	public float Timer;

	// Token: 0x04002124 RID: 8484
	public int ID;
}
