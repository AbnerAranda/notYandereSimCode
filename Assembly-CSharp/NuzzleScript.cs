using System;
using UnityEngine;

// Token: 0x0200034A RID: 842
public class NuzzleScript : MonoBehaviour
{
	// Token: 0x0600189C RID: 6300 RVA: 0x000E20A5 File Offset: 0x000E02A5
	private void Start()
	{
		this.OriginalRotation = base.transform.localEulerAngles;
	}

	// Token: 0x0600189D RID: 6301 RVA: 0x000E20B8 File Offset: 0x000E02B8
	private void Update()
	{
		if (!this.Down)
		{
			this.Rotate += Time.deltaTime * this.Speed;
			if (this.Rotate > this.Limit)
			{
				this.Down = true;
			}
		}
		else
		{
			this.Rotate -= Time.deltaTime * this.Speed;
			if (this.Rotate < -1f * this.Limit)
			{
				this.Down = false;
			}
		}
		base.transform.localEulerAngles = this.OriginalRotation + new Vector3(this.Rotate, 0f, 0f);
	}

	// Token: 0x04002442 RID: 9282
	public Vector3 OriginalRotation;

	// Token: 0x04002443 RID: 9283
	public float Rotate;

	// Token: 0x04002444 RID: 9284
	public float Limit;

	// Token: 0x04002445 RID: 9285
	public float Speed;

	// Token: 0x04002446 RID: 9286
	private bool Down;
}
