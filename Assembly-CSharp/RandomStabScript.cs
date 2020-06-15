using System;
using UnityEngine;

// Token: 0x0200038E RID: 910
public class RandomStabScript : MonoBehaviour
{
	// Token: 0x060019AB RID: 6571 RVA: 0x000FB258 File Offset: 0x000F9458
	private void Start()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		if (!this.Biting)
		{
			component.clip = this.Stabs[UnityEngine.Random.Range(0, this.Stabs.Length)];
			component.Play();
			return;
		}
		component.clip = this.Bite;
		component.pitch = UnityEngine.Random.Range(0.5f, 1f);
		component.Play();
	}

	// Token: 0x040027B8 RID: 10168
	public AudioClip[] Stabs;

	// Token: 0x040027B9 RID: 10169
	public AudioClip Bite;

	// Token: 0x040027BA RID: 10170
	public bool Biting;
}
