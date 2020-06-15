using System;
using UnityEngine;

// Token: 0x02000254 RID: 596
public class CurtainScript : MonoBehaviour
{
	// Token: 0x060012CF RID: 4815 RVA: 0x00097224 File Offset: 0x00095424
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			this.MyAudio.Play();
			this.Animate = true;
			this.Open = !this.Open;
		}
		if (this.Animate)
		{
			if (!this.Open)
			{
				this.Weight = Mathf.Lerp(this.Weight, 0f, Time.deltaTime * 10f);
				if (this.Weight < 0.01f)
				{
					this.Animate = false;
					this.Weight = 0f;
				}
			}
			else
			{
				this.Weight = Mathf.Lerp(this.Weight, 100f, Time.deltaTime * 10f);
				if (this.Weight > 99.99f)
				{
					this.Animate = false;
					this.Weight = 100f;
				}
			}
			this.Curtains[0].SetBlendShapeWeight(0, this.Weight);
			this.Curtains[1].SetBlendShapeWeight(0, this.Weight);
		}
	}

	// Token: 0x060012D0 RID: 4816 RVA: 0x00097340 File Offset: 0x00095540
	private void OnTriggerEnter(Collider other)
	{
		if ((other.gameObject.layer == 13 || other.gameObject.layer == 9) && !this.Open)
		{
			this.MyAudio.Play();
			this.Animate = true;
			this.Open = true;
		}
	}

	// Token: 0x04001887 RID: 6279
	public SkinnedMeshRenderer[] Curtains;

	// Token: 0x04001888 RID: 6280
	public PromptScript Prompt;

	// Token: 0x04001889 RID: 6281
	public AudioSource MyAudio;

	// Token: 0x0400188A RID: 6282
	public bool Animate;

	// Token: 0x0400188B RID: 6283
	public bool Open;

	// Token: 0x0400188C RID: 6284
	public float Weight;
}
