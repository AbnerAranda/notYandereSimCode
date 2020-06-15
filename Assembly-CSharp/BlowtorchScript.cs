using System;
using UnityEngine;

// Token: 0x020000E3 RID: 227
public class BlowtorchScript : MonoBehaviour
{
	// Token: 0x06000A68 RID: 2664 RVA: 0x0005630F File Offset: 0x0005450F
	private void Start()
	{
		this.Flame.localScale = Vector3.zero;
		base.enabled = false;
	}

	// Token: 0x06000A69 RID: 2665 RVA: 0x00056328 File Offset: 0x00054528
	private void Update()
	{
		this.Timer = Mathf.MoveTowards(this.Timer, 5f, Time.deltaTime);
		float num = UnityEngine.Random.Range(0.9f, 1f);
		this.Flame.localScale = new Vector3(num, num, num);
		if (this.Timer == 5f)
		{
			this.Flame.localScale = Vector3.zero;
			this.Yandere.Cauterizing = false;
			this.Yandere.CanMove = true;
			base.enabled = false;
			base.GetComponent<AudioSource>().Stop();
			this.Timer = 0f;
		}
	}

	// Token: 0x04000ACA RID: 2762
	public YandereScript Yandere;

	// Token: 0x04000ACB RID: 2763
	public RagdollScript Corpse;

	// Token: 0x04000ACC RID: 2764
	public PickUpScript PickUp;

	// Token: 0x04000ACD RID: 2765
	public PromptScript Prompt;

	// Token: 0x04000ACE RID: 2766
	public Transform Flame;

	// Token: 0x04000ACF RID: 2767
	public float Timer;
}
