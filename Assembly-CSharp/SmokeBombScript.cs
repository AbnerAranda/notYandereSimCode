using System;
using UnityEngine;

// Token: 0x020003E8 RID: 1000
public class SmokeBombScript : MonoBehaviour
{
	// Token: 0x06001AC6 RID: 6854 RVA: 0x0010C508 File Offset: 0x0010A708
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer > 15f)
		{
			if (!this.Stink)
			{
				foreach (StudentScript studentScript in this.Students)
				{
					if (studentScript != null)
					{
						studentScript.Blind = false;
					}
				}
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06001AC7 RID: 6855 RVA: 0x0010C570 File Offset: 0x0010A770
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 9)
		{
			StudentScript component = other.gameObject.GetComponent<StudentScript>();
			if (component != null)
			{
				if (this.Stink)
				{
					this.GoAway(component);
					return;
				}
				if (this.Amnesia && !component.Chasing)
				{
					component.ReturnToNormal();
				}
				this.Students[this.ID] = component;
				component.Blind = true;
				this.ID++;
			}
		}
	}

	// Token: 0x06001AC8 RID: 6856 RVA: 0x0010C5EC File Offset: 0x0010A7EC
	private void OnTriggerStay(Collider other)
	{
		if (this.Stink)
		{
			if (other.gameObject.layer == 9)
			{
				StudentScript component = other.gameObject.GetComponent<StudentScript>();
				if (component != null)
				{
					this.GoAway(component);
					return;
				}
			}
		}
		else if (this.Amnesia && other.gameObject.layer == 9)
		{
			StudentScript component2 = other.gameObject.GetComponent<StudentScript>();
			if (component2 != null && component2.Alarmed && !component2.Chasing)
			{
				component2.ReturnToNormal();
			}
		}
	}

	// Token: 0x06001AC9 RID: 6857 RVA: 0x0010C670 File Offset: 0x0010A870
	private void OnTriggerExit(Collider other)
	{
		if (!this.Stink && !this.Amnesia && other.gameObject.layer == 9)
		{
			StudentScript component = other.gameObject.GetComponent<StudentScript>();
			if (component != null)
			{
				Debug.Log(component.Name + " left a smoke cloud and stopped being blind.");
				component.Blind = false;
			}
		}
	}

	// Token: 0x06001ACA RID: 6858 RVA: 0x0010C6D0 File Offset: 0x0010A8D0
	private void GoAway(StudentScript Student)
	{
		if (!Student.Chasing)
		{
			if (Student.Following)
			{
				Student.Yandere.Followers--;
				Student.Hearts.emission.enabled = false;
				Student.FollowCountdown.gameObject.SetActive(false);
				Student.Following = false;
			}
			Student.BecomeAlarmed();
			Student.CurrentDestination = Student.StudentManager.GoAwaySpots.List[Student.StudentID];
			Student.Pathfinding.target = Student.StudentManager.GoAwaySpots.List[Student.StudentID];
			Student.Pathfinding.canSearch = true;
			Student.Pathfinding.canMove = true;
			Student.CharacterAnimation.CrossFade(Student.SprintAnim);
			Student.DistanceToDestination = 100f;
			Student.Pathfinding.speed = 4f;
			Student.AmnesiaTimer = 10f;
			Student.Distracted = true;
			Student.Alarmed = false;
			Student.Routine = false;
			Student.GoAway = true;
			Student.AlarmTimer = 0f;
		}
	}

	// Token: 0x04002B3B RID: 11067
	public StudentScript[] Students;

	// Token: 0x04002B3C RID: 11068
	public float Timer;

	// Token: 0x04002B3D RID: 11069
	public bool Amnesia;

	// Token: 0x04002B3E RID: 11070
	public bool Stink;

	// Token: 0x04002B3F RID: 11071
	public int ID;
}
