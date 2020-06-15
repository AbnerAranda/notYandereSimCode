using System;
using UnityEngine;

// Token: 0x020002B1 RID: 689
public class GateScript : MonoBehaviour
{
	// Token: 0x06001441 RID: 5185 RVA: 0x000B361C File Offset: 0x000B181C
	private void Update()
	{
		if (!this.ManuallyAdjusted)
		{
			if (this.Clock.PresentTime / 60f > 8f && this.Clock.PresentTime / 60f < 15.5f)
			{
				if (!this.Closed)
				{
					this.PlayAudio();
					this.Closed = true;
					if (this.EmergencyDoor.enabled)
					{
						this.EmergencyDoor.enabled = false;
					}
				}
			}
			else if (this.Closed)
			{
				this.PlayAudio();
				this.Closed = false;
				if (!this.EmergencyDoor.enabled)
				{
					this.EmergencyDoor.enabled = true;
				}
			}
		}
		if (this.StudentManager.Students[97] != null)
		{
			if (this.StudentManager.Students[97].CurrentAction == StudentActionType.AtLocker && this.StudentManager.Students[97].Routine && this.StudentManager.Students[97].Alive)
			{
				if (Vector3.Distance(this.StudentManager.Students[97].transform.position, this.StudentManager.Podiums.List[0].position) < 0.1f)
				{
					if (this.ManuallyAdjusted)
					{
						this.ManuallyAdjusted = false;
					}
					this.Prompt.enabled = false;
					this.Prompt.Hide();
				}
				else
				{
					this.Prompt.enabled = true;
				}
			}
			else
			{
				this.Prompt.enabled = true;
			}
		}
		else
		{
			this.Prompt.enabled = true;
		}
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			this.PlayAudio();
			this.EmergencyDoor.enabled = !this.EmergencyDoor.enabled;
			this.ManuallyAdjusted = true;
			this.Closed = !this.Closed;
			if (this.StudentManager.Students[97] != null && this.StudentManager.Students[97].Investigating)
			{
				this.StudentManager.Students[97].StopInvestigating();
			}
		}
		if (!this.Closed)
		{
			if (this.RightGate.localPosition.x != 7f)
			{
				this.RightGate.localPosition = new Vector3(Mathf.MoveTowards(this.RightGate.localPosition.x, 7f, Time.deltaTime), this.RightGate.localPosition.y, this.RightGate.localPosition.z);
				this.LeftGate.localPosition = new Vector3(Mathf.MoveTowards(this.LeftGate.localPosition.x, -7f, Time.deltaTime), this.LeftGate.localPosition.y, this.LeftGate.localPosition.z);
				if (!this.AudioPlayed && this.RightGate.localPosition.x == 7f)
				{
					this.RightGateAudio.clip = this.StopOpen;
					this.LeftGateAudio.clip = this.StopOpen;
					this.RightGateAudio.Play();
					this.LeftGateAudio.Play();
					this.RightGateLoop.Stop();
					this.LeftGateLoop.Stop();
					this.AudioPlayed = true;
					return;
				}
			}
		}
		else if (this.RightGate.localPosition.x != 2.325f)
		{
			if (this.RightGate.localPosition.x < 2.4f)
			{
				this.Crushing = true;
			}
			this.RightGate.localPosition = new Vector3(Mathf.MoveTowards(this.RightGate.localPosition.x, 2.325f, Time.deltaTime), this.RightGate.localPosition.y, this.RightGate.localPosition.z);
			this.LeftGate.localPosition = new Vector3(Mathf.MoveTowards(this.LeftGate.localPosition.x, -2.325f, Time.deltaTime), this.LeftGate.localPosition.y, this.LeftGate.localPosition.z);
			if (!this.AudioPlayed && this.RightGate.localPosition.x == 2.325f)
			{
				this.RightGateAudio.clip = this.StopOpen;
				this.LeftGateAudio.clip = this.StopOpen;
				this.RightGateAudio.Play();
				this.LeftGateAudio.Play();
				this.RightGateLoop.Stop();
				this.LeftGateLoop.Stop();
				this.AudioPlayed = true;
				this.Crushing = false;
			}
		}
	}

	// Token: 0x06001442 RID: 5186 RVA: 0x000B3AE0 File Offset: 0x000B1CE0
	public void PlayAudio()
	{
		this.RightGateAudio.clip = this.Start;
		this.LeftGateAudio.clip = this.Start;
		this.RightGateAudio.Play();
		this.LeftGateAudio.Play();
		this.RightGateLoop.Play();
		this.LeftGateLoop.Play();
		this.AudioPlayed = false;
	}

	// Token: 0x04001CE9 RID: 7401
	public StudentManagerScript StudentManager;

	// Token: 0x04001CEA RID: 7402
	public PromptScript Prompt;

	// Token: 0x04001CEB RID: 7403
	public ClockScript Clock;

	// Token: 0x04001CEC RID: 7404
	public Collider EmergencyDoor;

	// Token: 0x04001CED RID: 7405
	public Collider GateCollider;

	// Token: 0x04001CEE RID: 7406
	public Transform RightGate;

	// Token: 0x04001CEF RID: 7407
	public Transform LeftGate;

	// Token: 0x04001CF0 RID: 7408
	public bool ManuallyAdjusted;

	// Token: 0x04001CF1 RID: 7409
	public bool AudioPlayed;

	// Token: 0x04001CF2 RID: 7410
	public bool UpdateGates;

	// Token: 0x04001CF3 RID: 7411
	public bool Crushing;

	// Token: 0x04001CF4 RID: 7412
	public bool Closed;

	// Token: 0x04001CF5 RID: 7413
	public AudioSource RightGateAudio;

	// Token: 0x04001CF6 RID: 7414
	public AudioSource LeftGateAudio;

	// Token: 0x04001CF7 RID: 7415
	public AudioSource RightGateLoop;

	// Token: 0x04001CF8 RID: 7416
	public AudioSource LeftGateLoop;

	// Token: 0x04001CF9 RID: 7417
	public AudioClip Start;

	// Token: 0x04001CFA RID: 7418
	public AudioClip StopOpen;

	// Token: 0x04001CFB RID: 7419
	public AudioClip StopClose;
}
