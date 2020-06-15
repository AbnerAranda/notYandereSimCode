using System;
using UnityEngine;

// Token: 0x020000F8 RID: 248
public class ChallengeIconScript : MonoBehaviour
{
	// Token: 0x06000AAB RID: 2731 RVA: 0x00059750 File Offset: 0x00057950
	private void Start()
	{
		if (GameGlobals.LoveSick)
		{
			this.R = 1f;
			this.G = 0f;
			this.B = 0f;
			return;
		}
		this.R = 1f;
		this.G = 1f;
		this.B = 1f;
	}

	// Token: 0x06000AAC RID: 2732 RVA: 0x000597A8 File Offset: 0x000579A8
	private void Update()
	{
		if (base.transform.position.x > -0.125f && base.transform.position.x < 0.125f)
		{
			if (this.Icon != null)
			{
				this.LargeIcon.mainTexture = this.Icon.mainTexture;
			}
			this.Dark -= Time.deltaTime * 10f;
			if (this.Dark < 0f)
			{
				this.Dark = 0f;
			}
		}
		else
		{
			this.Dark += Time.deltaTime * 10f;
			if (this.Dark > 1f)
			{
				this.Dark = 1f;
			}
		}
		this.IconFrame.color = new Color(this.Dark * this.R, this.Dark * this.G, this.Dark * this.B, 1f);
		this.NameFrame.color = new Color(this.Dark * this.R, this.Dark * this.G, this.Dark * this.B, 1f);
		this.Name.color = new Color(this.Dark * this.R, this.Dark * this.G, this.Dark * this.B, 1f);
		if (GameGlobals.LoveSick)
		{
			if (base.transform.position.x > -0.125f && base.transform.position.x < 0.125f)
			{
				this.IconFrame.color = Color.white;
				this.NameFrame.color = Color.white;
				this.Name.color = Color.white;
				return;
			}
			this.IconFrame.color = new Color(this.R, this.G, this.B, 1f);
			this.NameFrame.color = new Color(this.R, this.G, this.B, 1f);
			this.Name.color = new Color(this.R, this.G, this.B, 1f);
		}
	}

	// Token: 0x04000B71 RID: 2929
	public UITexture LargeIcon;

	// Token: 0x04000B72 RID: 2930
	public UISprite IconFrame;

	// Token: 0x04000B73 RID: 2931
	public UISprite NameFrame;

	// Token: 0x04000B74 RID: 2932
	public UITexture Icon;

	// Token: 0x04000B75 RID: 2933
	public UILabel Name;

	// Token: 0x04000B76 RID: 2934
	public float Dark;

	// Token: 0x04000B77 RID: 2935
	private float R;

	// Token: 0x04000B78 RID: 2936
	private float G;

	// Token: 0x04000B79 RID: 2937
	private float B;
}
