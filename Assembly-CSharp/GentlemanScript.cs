using System;
using UnityEngine;

// Token: 0x020002B6 RID: 694
public class GentlemanScript : MonoBehaviour
{
	// Token: 0x06001454 RID: 5204 RVA: 0x000B4D1C File Offset: 0x000B2F1C
	private void Update()
	{
		if (Input.GetButtonDown("RB"))
		{
			AudioSource component = base.GetComponent<AudioSource>();
			if (!component.isPlaying)
			{
				component.clip = this.Clips[UnityEngine.Random.Range(0, this.Clips.Length - 1)];
				component.Play();
				this.Yandere.Sanity += 10f;
			}
		}
	}

	// Token: 0x04001D27 RID: 7463
	public YandereScript Yandere;

	// Token: 0x04001D28 RID: 7464
	public AudioClip[] Clips;
}
