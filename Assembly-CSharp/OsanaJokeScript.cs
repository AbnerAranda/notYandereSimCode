using System;
using UnityEngine;

// Token: 0x0200034E RID: 846
public class OsanaJokeScript : MonoBehaviour
{
	// Token: 0x060018A9 RID: 6313 RVA: 0x000E2D90 File Offset: 0x000E0F90
	private void Update()
	{
		if (this.Advance)
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > 14f)
			{
				Application.Quit();
				return;
			}
			if (this.Timer > 3f)
			{
				this.Label.text = "Congratulations, you eliminated Osana!";
				if (!this.Jukebox.isPlaying)
				{
					this.Jukebox.clip = this.VictoryMusic;
					this.Jukebox.Play();
					return;
				}
			}
		}
		else if (Input.GetKeyDown("f"))
		{
			this.Rotation[0].enabled = false;
			this.Rotation[1].enabled = false;
			this.Rotation[2].enabled = false;
			this.Rotation[3].enabled = false;
			this.Rotation[4].enabled = false;
			this.Rotation[5].enabled = false;
			this.Rotation[6].enabled = false;
			this.Rotation[7].enabled = false;
			UnityEngine.Object.Instantiate<GameObject>(this.BloodSplatterEffect, this.Head.position, Quaternion.identity);
			this.Head.localScale = new Vector3(0f, 0f, 0f);
			this.Jukebox.clip = this.BloodSplatterSFX;
			this.Jukebox.Play();
			this.Label.text = "";
			this.Advance = true;
		}
	}

	// Token: 0x04002465 RID: 9317
	public ConstantRandomRotation[] Rotation;

	// Token: 0x04002466 RID: 9318
	public GameObject BloodSplatterEffect;

	// Token: 0x04002467 RID: 9319
	public AudioClip BloodSplatterSFX;

	// Token: 0x04002468 RID: 9320
	public AudioClip VictoryMusic;

	// Token: 0x04002469 RID: 9321
	public AudioSource Jukebox;

	// Token: 0x0400246A RID: 9322
	public Transform Head;

	// Token: 0x0400246B RID: 9323
	public UILabel Label;

	// Token: 0x0400246C RID: 9324
	public bool Advance;

	// Token: 0x0400246D RID: 9325
	public float Timer;

	// Token: 0x0400246E RID: 9326
	public int ID;
}
