using System;
using UnityEngine;

// Token: 0x0200029B RID: 667
public class FakeStudentScript : MonoBehaviour
{
	// Token: 0x060013FB RID: 5115 RVA: 0x000AF10D File Offset: 0x000AD30D
	private void Start()
	{
		this.targetRotation = base.transform.rotation;
		this.Student.Club = this.Club;
	}

	// Token: 0x060013FC RID: 5116 RVA: 0x000AF134 File Offset: 0x000AD334
	private void Update()
	{
		if (!this.Student.Talking)
		{
			if (this.LeaderAnim != "")
			{
				base.GetComponent<Animation>().CrossFade(this.LeaderAnim);
			}
			if (this.Rotate)
			{
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
				this.RotationTimer += Time.deltaTime;
				if (this.RotationTimer > 1f)
				{
					this.RotationTimer = 0f;
					this.Rotate = false;
				}
			}
		}
		if (this.Prompt.Circle[0].fillAmount == 0f && !this.Yandere.Chased && this.Yandere.Chasers == 0)
		{
			this.Yandere.TargetStudent = this.Student;
			this.Subtitle.UpdateLabel(SubtitleType.ClubGreeting, (int)this.Student.Club, 4f);
			this.DialogueWheel.ClubLeader = true;
			this.StudentManager.DisablePrompts();
			this.DialogueWheel.HideShadows();
			this.DialogueWheel.Show = true;
			this.DialogueWheel.Panel.enabled = true;
			this.Student.Talking = true;
			this.Student.TalkTimer = 0f;
			this.Yandere.ShoulderCamera.OverShoulder = true;
			this.Yandere.WeaponMenu.KeyboardShow = false;
			this.Yandere.Obscurance.enabled = false;
			this.Yandere.WeaponMenu.Show = false;
			this.Yandere.YandereVision = false;
			this.Yandere.CanMove = false;
			this.Yandere.Talking = true;
			this.RotationTimer = 0f;
			this.Rotate = true;
		}
	}

	// Token: 0x04001BFF RID: 7167
	public StudentManagerScript StudentManager;

	// Token: 0x04001C00 RID: 7168
	public DialogueWheelScript DialogueWheel;

	// Token: 0x04001C01 RID: 7169
	public SubtitleScript Subtitle;

	// Token: 0x04001C02 RID: 7170
	public YandereScript Yandere;

	// Token: 0x04001C03 RID: 7171
	public StudentScript Student;

	// Token: 0x04001C04 RID: 7172
	public PromptScript Prompt;

	// Token: 0x04001C05 RID: 7173
	public Quaternion targetRotation;

	// Token: 0x04001C06 RID: 7174
	public float RotationTimer;

	// Token: 0x04001C07 RID: 7175
	public bool Rotate;

	// Token: 0x04001C08 RID: 7176
	public ClubType Club;

	// Token: 0x04001C09 RID: 7177
	public string LeaderAnim;
}
