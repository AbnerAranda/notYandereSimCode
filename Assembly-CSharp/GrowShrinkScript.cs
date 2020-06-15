using System;
using UnityEngine;

// Token: 0x020002DD RID: 733
public class GrowShrinkScript : MonoBehaviour
{
	// Token: 0x060016F4 RID: 5876 RVA: 0x000BEAFC File Offset: 0x000BCCFC
	private void Start()
	{
		this.OriginalPosition = base.transform.localPosition;
		base.transform.localScale = Vector3.zero;
	}

	// Token: 0x060016F5 RID: 5877 RVA: 0x000BEB20 File Offset: 0x000BCD20
	private void Update()
	{
		this.Timer += Time.deltaTime;
		this.Scale += Time.deltaTime * (this.Strength * this.Speed);
		if (!this.Shrink)
		{
			this.Strength += Time.deltaTime * this.Speed;
			if (this.Strength > this.Threshold)
			{
				this.Strength = this.Threshold;
			}
			if (this.Scale > this.Target)
			{
				this.Threshold *= this.Slowdown;
				this.Shrink = true;
			}
		}
		else
		{
			this.Strength -= Time.deltaTime * this.Speed;
			float num = this.Threshold * -1f;
			if (this.Strength < num)
			{
				this.Strength = num;
			}
			if (this.Scale < this.Target)
			{
				this.Threshold *= this.Slowdown;
				this.Shrink = false;
			}
		}
		if (this.Timer > 3.33333f)
		{
			this.FallSpeed += Time.deltaTime * 10f;
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y - this.FallSpeed * this.FallSpeed, base.transform.localPosition.z);
		}
		base.transform.localScale = new Vector3(this.Scale, this.Scale, this.Scale);
	}

	// Token: 0x060016F6 RID: 5878 RVA: 0x000BECB8 File Offset: 0x000BCEB8
	public void Return()
	{
		base.transform.localPosition = this.OriginalPosition;
		base.transform.localScale = Vector3.zero;
		this.FallSpeed = 0f;
		this.Threshold = 1f;
		this.Slowdown = 0.5f;
		this.Strength = 1f;
		this.Target = 1f;
		this.Scale = 0f;
		this.Speed = 5f;
		this.Timer = 0f;
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001E6A RID: 7786
	public float FallSpeed;

	// Token: 0x04001E6B RID: 7787
	public float Threshold = 1f;

	// Token: 0x04001E6C RID: 7788
	public float Slowdown = 0.5f;

	// Token: 0x04001E6D RID: 7789
	public float Strength = 1f;

	// Token: 0x04001E6E RID: 7790
	public float Target = 1f;

	// Token: 0x04001E6F RID: 7791
	public float Scale;

	// Token: 0x04001E70 RID: 7792
	public float Speed = 5f;

	// Token: 0x04001E71 RID: 7793
	public float Timer;

	// Token: 0x04001E72 RID: 7794
	public bool Shrink;

	// Token: 0x04001E73 RID: 7795
	public Vector3 OriginalPosition;
}
