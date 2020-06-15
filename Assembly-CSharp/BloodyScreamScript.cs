using System;
using UnityEngine;

// Token: 0x020000E2 RID: 226
public class BloodyScreamScript : MonoBehaviour
{
	// Token: 0x06000A66 RID: 2662 RVA: 0x000562E7 File Offset: 0x000544E7
	private void Start()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		component.clip = this.Screams[UnityEngine.Random.Range(0, this.Screams.Length)];
		component.Play();
	}

	// Token: 0x04000AC9 RID: 2761
	public AudioClip[] Screams;
}
