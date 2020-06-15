using System;
using UnityEngine;

// Token: 0x020002AA RID: 682
public class FootstepScript : MonoBehaviour
{
	// Token: 0x06001426 RID: 5158 RVA: 0x000B19F8 File Offset: 0x000AFBF8
	private void Start()
	{
		if (!this.Student.Nemesis)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06001427 RID: 5159 RVA: 0x000B1A10 File Offset: 0x000AFC10
	private void Update()
	{
		if (!this.FootUp)
		{
			if (base.transform.position.y > this.Student.transform.position.y + this.UpThreshold)
			{
				this.FootUp = true;
				return;
			}
		}
		else if (base.transform.position.y < this.Student.transform.position.y + this.DownThreshold)
		{
			if (this.FootUp)
			{
				if (this.Student.Pathfinding.speed > 1f)
				{
					this.MyAudio.clip = this.RunFootsteps[UnityEngine.Random.Range(0, this.RunFootsteps.Length)];
					this.MyAudio.volume = 0.2f;
				}
				else
				{
					this.MyAudio.clip = this.WalkFootsteps[UnityEngine.Random.Range(0, this.WalkFootsteps.Length)];
					this.MyAudio.volume = 0.1f;
				}
				this.MyAudio.Play();
			}
			this.FootUp = false;
		}
	}

	// Token: 0x04001C95 RID: 7317
	public StudentScript Student;

	// Token: 0x04001C96 RID: 7318
	public AudioSource MyAudio;

	// Token: 0x04001C97 RID: 7319
	public AudioClip[] WalkFootsteps;

	// Token: 0x04001C98 RID: 7320
	public AudioClip[] RunFootsteps;

	// Token: 0x04001C99 RID: 7321
	public float DownThreshold = 0.02f;

	// Token: 0x04001C9A RID: 7322
	public float UpThreshold = 0.025f;

	// Token: 0x04001C9B RID: 7323
	public bool FootUp;
}
