using System;
using UnityEngine;

// Token: 0x020002D8 RID: 728
public class GradingPaperScript : MonoBehaviour
{
	// Token: 0x060016E7 RID: 5863 RVA: 0x000BE37C File Offset: 0x000BC57C
	private void Start()
	{
		this.OriginalPosition = this.Chair.position;
	}

	// Token: 0x060016E8 RID: 5864 RVA: 0x000BE390 File Offset: 0x000BC590
	private void Update()
	{
		if (!this.Writing)
		{
			if (Vector3.Distance(this.Chair.position, this.OriginalPosition) > 0.01f)
			{
				this.Chair.position = Vector3.Lerp(this.Chair.position, this.OriginalPosition, Time.deltaTime * 10f);
				return;
			}
		}
		else if (this.Character != null)
		{
			if (Vector3.Distance(this.Chair.position, this.Character.transform.position + this.Character.transform.forward * 0.1f) > 0.01f)
			{
				this.Chair.position = Vector3.Lerp(this.Chair.position, this.Character.transform.position + this.Character.transform.forward * 0.1f, Time.deltaTime * 10f);
			}
			if (this.Phase == 1)
			{
				if (this.Teacher.CharacterAnimation["f02_deskWrite"].time > this.PickUpTime1)
				{
					this.Teacher.CharacterAnimation["f02_deskWrite"].speed = this.Speed;
					this.Paper.parent = this.LeftHand;
					this.Paper.localPosition = this.PickUpPosition1;
					this.Paper.localEulerAngles = this.PickUpRotation1;
					this.Paper.localScale = new Vector3(0.9090909f, 0.9090909f, 0.9090909f);
					this.Phase++;
				}
			}
			else if (this.Phase == 2)
			{
				if (this.Teacher.CharacterAnimation["f02_deskWrite"].time > this.SetDownTime1)
				{
					this.Paper.parent = this.Character.transform;
					this.Paper.localPosition = this.SetDownPosition1;
					this.Paper.localEulerAngles = this.SetDownRotation1;
					this.Phase++;
				}
			}
			else if (this.Phase == 3)
			{
				if (this.Teacher.CharacterAnimation["f02_deskWrite"].time > this.PickUpTime2)
				{
					this.Paper.parent = this.LeftHand;
					this.Paper.localPosition = this.PickUpPosition2;
					this.Paper.localEulerAngles = this.PickUpRotation2;
					this.Phase++;
				}
			}
			else if (this.Phase == 4)
			{
				if (this.Teacher.CharacterAnimation["f02_deskWrite"].time > this.SetDownTime2)
				{
					this.Paper.parent = this.Character.transform;
					this.Paper.localScale = Vector3.zero;
					this.Phase++;
				}
			}
			else if (this.Phase == 5 && this.Teacher.CharacterAnimation["f02_deskWrite"].time >= this.Teacher.CharacterAnimation["f02_deskWrite"].length)
			{
				this.Teacher.CharacterAnimation["f02_deskWrite"].time = 0f;
				this.Teacher.CharacterAnimation.Play("f02_deskWrite");
				this.Phase = 1;
			}
			if (this.Teacher.Actions[this.Teacher.Phase] != StudentActionType.GradePapers || !this.Teacher.Routine || this.Teacher.Stop)
			{
				this.Paper.localScale = Vector3.zero;
				this.Teacher.Obstacle.enabled = false;
				this.Teacher.Pen.SetActive(false);
				this.Writing = false;
				this.Phase = 1;
			}
		}
	}

	// Token: 0x04001E43 RID: 7747
	public StudentScript Teacher;

	// Token: 0x04001E44 RID: 7748
	public GameObject Character;

	// Token: 0x04001E45 RID: 7749
	public Transform LeftHand;

	// Token: 0x04001E46 RID: 7750
	public Transform Chair;

	// Token: 0x04001E47 RID: 7751
	public Transform Paper;

	// Token: 0x04001E48 RID: 7752
	public float PickUpTime1;

	// Token: 0x04001E49 RID: 7753
	public float SetDownTime1;

	// Token: 0x04001E4A RID: 7754
	public float PickUpTime2;

	// Token: 0x04001E4B RID: 7755
	public float SetDownTime2;

	// Token: 0x04001E4C RID: 7756
	public Vector3 OriginalPosition;

	// Token: 0x04001E4D RID: 7757
	public Vector3 PickUpPosition1;

	// Token: 0x04001E4E RID: 7758
	public Vector3 SetDownPosition1;

	// Token: 0x04001E4F RID: 7759
	public Vector3 PickUpPosition2;

	// Token: 0x04001E50 RID: 7760
	public Vector3 PickUpRotation1;

	// Token: 0x04001E51 RID: 7761
	public Vector3 SetDownRotation1;

	// Token: 0x04001E52 RID: 7762
	public Vector3 PickUpRotation2;

	// Token: 0x04001E53 RID: 7763
	public int Phase = 1;

	// Token: 0x04001E54 RID: 7764
	public float Speed = 1f;

	// Token: 0x04001E55 RID: 7765
	public bool Writing;
}
