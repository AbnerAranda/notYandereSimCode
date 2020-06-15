using System;
using UnityEngine;

// Token: 0x02000270 RID: 624
public class DoorOpenerScript : MonoBehaviour
{
	// Token: 0x0600135D RID: 4957 RVA: 0x000A5AE8 File Offset: 0x000A3CE8
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 9)
		{
			this.Student = other.gameObject.GetComponent<StudentScript>();
			if (this.Student != null && !this.Student.Dying && !this.Door.Open && !this.Door.Locked)
			{
				this.Door.Student = this.Student;
				this.Door.OpenDoor();
			}
		}
	}

	// Token: 0x0600135E RID: 4958 RVA: 0x000A5B68 File Offset: 0x000A3D68
	private void OnTriggerStay(Collider other)
	{
		if (!this.Door.Open && other.gameObject.layer == 9)
		{
			this.Student = other.gameObject.GetComponent<StudentScript>();
			if (this.Student != null && !this.Student.Dying && !this.Door.Open && !this.Door.Locked)
			{
				this.Door.Student = this.Student;
				this.Door.OpenDoor();
			}
		}
	}

	// Token: 0x04001A64 RID: 6756
	public StudentScript Student;

	// Token: 0x04001A65 RID: 6757
	public DoorScript Door;
}
