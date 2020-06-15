using System;
using UnityEngine;

// Token: 0x0200024E RID: 590
public class CountdownScript : MonoBehaviour
{
	// Token: 0x060012BF RID: 4799 RVA: 0x00096A6D File Offset: 0x00094C6D
	private void Update()
	{
		this.Sprite.fillAmount = Mathf.MoveTowards(this.Sprite.fillAmount, 0f, Time.deltaTime * this.Speed);
	}

	// Token: 0x0400186D RID: 6253
	public UISprite Sprite;

	// Token: 0x0400186E RID: 6254
	public float Speed = 0.05f;

	// Token: 0x0400186F RID: 6255
	public bool MaskedPhoto;
}
