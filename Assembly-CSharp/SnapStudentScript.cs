using System;
using UnityEngine;

// Token: 0x020003E9 RID: 1001
public class SnapStudentScript : MonoBehaviour
{
	// Token: 0x06001ACC RID: 6860 RVA: 0x0010C7EC File Offset: 0x0010A9EC
	private void Start()
	{
		this.MyAnim.enabled = false;
		this.MyAnim[this.FearAnim].time = UnityEngine.Random.Range(0f, this.MyAnim[this.FearAnim].length);
		this.MyAnim.enabled = true;
	}

	// Token: 0x06001ACD RID: 6861 RVA: 0x0010C848 File Offset: 0x0010AA48
	private void Update()
	{
		if (Vector3.Distance(base.transform.position, this.Yandere.transform.position) < 1f)
		{
			if (this.Yandere.CanMove)
			{
				if (this.Student.StudentID == 1)
				{
					if (this.Yandere.Armed && !this.Yandere.KillingSenpai)
					{
						this.Yandere.Knife.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
						this.Yandere.MyAudio.clip = this.Yandere.EndSNAP;
						this.Yandere.MyAudio.loop = false;
						this.Yandere.MyAudio.volume = 1f;
						this.Yandere.MyAudio.pitch = 1f;
						this.Yandere.MyAudio.Play();
						this.Yandere.Speed = 0f;
						this.Yandere.MyAnim.CrossFade("f02_snapKill_00");
						this.MyAnim.CrossFade("snapDie_00");
						this.Yandere.TargetStudent = this;
						this.Yandere.KillingSenpai = true;
						this.Yandere.CanMove = false;
						base.enabled = false;
					}
				}
				else if (!this.Yandere.Attacking)
				{
					this.Yandere.transform.position = base.transform.position + base.transform.forward;
					this.Yandere.transform.LookAt(base.transform.position);
					this.Yandere.TargetStudent = this;
					this.Yandere.Attacking = true;
					this.Yandere.CanMove = false;
					this.Yandere.StaticNoise.volume = 0f;
					this.Yandere.Static.Fade = 0f;
					this.Yandere.HurryTimer = 0f;
					this.Yandere.ChooseAttack();
					base.enabled = false;
				}
			}
		}
		else if (Vector3.Distance(base.transform.position, this.Yandere.transform.position) < 5f)
		{
			if (!this.VoicedConcern && this.Yandere.CanMove && !this.Yandere.SnapVoice.isPlaying)
			{
				if (this.Student.StudentID == 1)
				{
					this.Yandere.SnapVoice.clip = this.SenpaiFear;
					this.Yandere.SnapVoice.Play();
					this.Yandere.ListenTimer = 10f;
				}
				else
				{
					int voiceID = this.Yandere.VoiceID;
					while (this.Yandere.VoiceID == voiceID)
					{
						this.Yandere.VoiceID = UnityEngine.Random.Range(0, 5);
					}
					this.Yandere.SnapVoice.clip = this.StudentFear[this.Yandere.VoiceID];
					this.Yandere.SnapVoice.Play();
					this.Yandere.ListenTimer = 1f;
				}
				this.VoicedConcern = true;
			}
			this.MyAnim.Play(this.FearAnim);
		}
		else
		{
			this.MyAnim.Play(this.FearAnim);
		}
		if (!this.Yandere.Attacking)
		{
			base.transform.LookAt(new Vector3(this.Yandere.transform.position.x, base.transform.position.y, this.Yandere.transform.position.z));
		}
	}

	// Token: 0x04002B40 RID: 11072
	public SnappedYandereScript Yandere;

	// Token: 0x04002B41 RID: 11073
	public Quaternion targetRotation;

	// Token: 0x04002B42 RID: 11074
	public StudentScript Student;

	// Token: 0x04002B43 RID: 11075
	public Animation MyAnim;

	// Token: 0x04002B44 RID: 11076
	public string FearAnim;

	// Token: 0x04002B45 RID: 11077
	public string[] AttackAnims;

	// Token: 0x04002B46 RID: 11078
	public bool VoicedConcern;

	// Token: 0x04002B47 RID: 11079
	public AudioClip[] StudentFear;

	// Token: 0x04002B48 RID: 11080
	public AudioClip SenpaiFear;
}
