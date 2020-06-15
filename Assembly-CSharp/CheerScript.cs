using System;
using UnityEngine;

// Token: 0x02000233 RID: 563
public class CheerScript : MonoBehaviour
{
	// Token: 0x0600123B RID: 4667 RVA: 0x00081A70 File Offset: 0x0007FC70
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer > 5f)
		{
			this.MyAudio.clip = this.Cheers[UnityEngine.Random.Range(1, this.Cheers.Length)];
			this.MyAudio.Play();
			this.Timer = 0f;
		}
	}

	// Token: 0x04001594 RID: 5524
	public AudioSource MyAudio;

	// Token: 0x04001595 RID: 5525
	public AudioClip[] Cheers;

	// Token: 0x04001596 RID: 5526
	public float Timer;
}
