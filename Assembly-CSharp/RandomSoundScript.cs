using System;
using UnityEngine;

// Token: 0x0200038D RID: 909
public class RandomSoundScript : MonoBehaviour
{
	// Token: 0x060019A9 RID: 6569 RVA: 0x000FB230 File Offset: 0x000F9430
	private void Start()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		component.clip = this.Clips[UnityEngine.Random.Range(1, this.Clips.Length)];
		component.Play();
	}

	// Token: 0x040027B7 RID: 10167
	public AudioClip[] Clips;
}
