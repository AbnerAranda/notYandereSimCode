using System;
using UnityEngine;

// Token: 0x0200046C RID: 1132
public class WoodChipperScript : MonoBehaviour
{
	// Token: 0x06001D4E RID: 7502 RVA: 0x0015FE14 File Offset: 0x0015E014
	private void Update()
	{
		if (this.Yandere.PickUp != null)
		{
			if (this.Yandere.PickUp.Bucket != null)
			{
				if (!this.Yandere.PickUp.Bucket.Full)
				{
					this.BucketPrompt.HideButton[0] = false;
					if (this.BucketPrompt.Circle[0].fillAmount == 0f)
					{
						this.Bucket = this.Yandere.PickUp;
						this.Yandere.EmptyHands();
						this.Bucket.transform.eulerAngles = this.BucketPoint.eulerAngles;
						this.Bucket.transform.position = this.BucketPoint.position;
						this.Bucket.GetComponent<Rigidbody>().useGravity = false;
						this.Bucket.MyCollider.enabled = false;
					}
				}
				else
				{
					this.BucketPrompt.HideButton[0] = true;
				}
			}
			else
			{
				this.BucketPrompt.HideButton[0] = true;
			}
		}
		else
		{
			this.BucketPrompt.HideButton[0] = true;
		}
		AudioSource component = base.GetComponent<AudioSource>();
		if (!this.Open)
		{
			this.Rotation = Mathf.MoveTowards(this.Rotation, 0f, Time.deltaTime * 360f);
			if (this.Rotation > -36f)
			{
				if (this.Rotation < 0f)
				{
					component.clip = this.CloseAudio;
					component.Play();
				}
				this.Rotation = 0f;
			}
			this.Lid.transform.localEulerAngles = new Vector3(this.Rotation, this.Lid.transform.localEulerAngles.y, this.Lid.transform.localEulerAngles.z);
		}
		else
		{
			if (this.Lid.transform.localEulerAngles.x == 0f)
			{
				component.clip = this.OpenAudio;
				component.Play();
			}
			this.Rotation = Mathf.MoveTowards(this.Rotation, -90f, Time.deltaTime * 360f);
			this.Lid.transform.localEulerAngles = new Vector3(this.Rotation, this.Lid.transform.localEulerAngles.y, this.Lid.transform.localEulerAngles.z);
		}
		if (!this.BloodSpray.isPlaying)
		{
			if (!this.Occupied)
			{
				if (this.Yandere.Ragdoll == null)
				{
					this.Prompt.HideButton[3] = true;
				}
				else
				{
					this.Prompt.HideButton[3] = false;
				}
			}
			else if (this.Bucket == null)
			{
				this.Prompt.HideButton[0] = true;
			}
			else if (this.Bucket.Bucket.Full)
			{
				this.Prompt.HideButton[0] = true;
			}
			else
			{
				this.Prompt.HideButton[0] = false;
			}
		}
		if (this.Prompt.Circle[3].fillAmount == 0f)
		{
			Time.timeScale = 1f;
			if (this.Yandere.Ragdoll != null)
			{
				if (!this.Yandere.Carrying)
				{
					this.Yandere.Character.GetComponent<Animation>().CrossFade("f02_dragIdle_00");
				}
				else
				{
					this.Yandere.Character.GetComponent<Animation>().CrossFade("f02_carryIdleA_00");
				}
				this.Yandere.YandereVision = false;
				this.Yandere.Chipping = true;
				this.Yandere.CanMove = false;
				this.Victims++;
				this.VictimList[this.Victims] = this.Yandere.Ragdoll.GetComponent<RagdollScript>().StudentID;
				this.Open = true;
			}
		}
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			component.clip = this.ShredAudio;
			component.Play();
			this.Prompt.HideButton[3] = false;
			this.Prompt.HideButton[0] = true;
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			this.Yandere.Police.Corpses--;
			if (this.Yandere.Police.SuicideScene && this.Yandere.Police.Corpses == 1)
			{
				this.Yandere.Police.MurderScene = false;
			}
			if (this.Yandere.Police.Corpses == 0)
			{
				this.Yandere.Police.MurderScene = false;
			}
			if (this.Yandere.StudentManager.Students[this.VictimID].Drowned)
			{
				this.Yandere.Police.DrownVictims--;
			}
			this.Shredding = true;
			this.Yandere.StudentManager.Students[this.VictimID].Ragdoll.Disposed = true;
		}
		if (this.Shredding)
		{
			if (this.Bucket != null)
			{
				this.Bucket.Bucket.UpdateAppearance = true;
			}
			this.Timer += Time.deltaTime;
			if (this.Timer >= 10f)
			{
				this.Prompt.enabled = true;
				this.Shredding = false;
				this.Occupied = false;
				this.Timer = 0f;
				return;
			}
			if (this.Timer >= 9f)
			{
				if (this.Bucket != null)
				{
					this.Bucket.MyCollider.enabled = true;
					this.Bucket.Bucket.FillSpeed = 1f;
					this.Bucket = null;
					this.BloodSpray.Stop();
					return;
				}
			}
			else if (this.Timer >= 0.33333f && !this.Bucket.Bucket.Full)
			{
				this.BloodSpray.GetComponent<AudioSource>().Play();
				this.BloodSpray.Play();
				this.Bucket.Bucket.Bloodiness = 100f;
				this.Bucket.Bucket.FillSpeed = 0.05f;
				this.Bucket.Bucket.Full = true;
			}
		}
	}

	// Token: 0x06001D4F RID: 7503 RVA: 0x00160464 File Offset: 0x0015E664
	public void SetVictimsMissing()
	{
		int[] victimList = this.VictimList;
		for (int i = 0; i < victimList.Length; i++)
		{
			StudentGlobals.SetStudentMissing(victimList[i], true);
		}
	}

	// Token: 0x04003772 RID: 14194
	public ParticleSystem BloodSpray;

	// Token: 0x04003773 RID: 14195
	public PromptScript BucketPrompt;

	// Token: 0x04003774 RID: 14196
	public YandereScript Yandere;

	// Token: 0x04003775 RID: 14197
	public PickUpScript Bucket;

	// Token: 0x04003776 RID: 14198
	public PromptScript Prompt;

	// Token: 0x04003777 RID: 14199
	public AudioClip CloseAudio;

	// Token: 0x04003778 RID: 14200
	public AudioClip ShredAudio;

	// Token: 0x04003779 RID: 14201
	public AudioClip OpenAudio;

	// Token: 0x0400377A RID: 14202
	public Transform BucketPoint;

	// Token: 0x0400377B RID: 14203
	public Transform DumpPoint;

	// Token: 0x0400377C RID: 14204
	public Transform Lid;

	// Token: 0x0400377D RID: 14205
	public float Rotation;

	// Token: 0x0400377E RID: 14206
	public float Timer;

	// Token: 0x0400377F RID: 14207
	public bool Shredding;

	// Token: 0x04003780 RID: 14208
	public bool Occupied;

	// Token: 0x04003781 RID: 14209
	public bool Open;

	// Token: 0x04003782 RID: 14210
	public int VictimID;

	// Token: 0x04003783 RID: 14211
	public int Victims;

	// Token: 0x04003784 RID: 14212
	public int ID;

	// Token: 0x04003785 RID: 14213
	public int[] VictimList;
}
