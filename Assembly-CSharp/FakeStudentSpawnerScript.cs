using System;
using UnityEngine;

// Token: 0x0200029C RID: 668
public class FakeStudentSpawnerScript : MonoBehaviour
{
	// Token: 0x060013FE RID: 5118 RVA: 0x000AF318 File Offset: 0x000AD518
	public void Spawn()
	{
		if (!this.AlreadySpawned)
		{
			this.Student = this.FakeFemale;
			this.NESW = 1;
			while (this.Spawned < 100)
			{
				if (this.NESW == 1)
				{
					this.NewStudent = UnityEngine.Object.Instantiate<GameObject>(this.Student, new Vector3(UnityEngine.Random.Range(-21f, 21f), (float)this.Height, UnityEngine.Random.Range(21f, 19f)), Quaternion.identity);
				}
				else if (this.NESW == 2)
				{
					this.NewStudent = UnityEngine.Object.Instantiate<GameObject>(this.Student, new Vector3(UnityEngine.Random.Range(19f, 21f), (float)this.Height, UnityEngine.Random.Range(29f, -37f)), Quaternion.identity);
				}
				else if (this.NESW == 3)
				{
					this.NewStudent = UnityEngine.Object.Instantiate<GameObject>(this.Student, new Vector3(UnityEngine.Random.Range(-21f, 21f), (float)this.Height, UnityEngine.Random.Range(-21f, -19f)), Quaternion.identity);
				}
				else if (this.NESW == 4)
				{
					this.NewStudent = UnityEngine.Object.Instantiate<GameObject>(this.Student, new Vector3(UnityEngine.Random.Range(-19f, -21f), (float)this.Height, UnityEngine.Random.Range(29f, -37f)), Quaternion.identity);
				}
				this.StudentID++;
				this.NewStudent.GetComponent<PlaceholderStudentScript>().FakeStudentSpawner = this;
				this.NewStudent.GetComponent<PlaceholderStudentScript>().StudentID = this.StudentID;
				this.NewStudent.GetComponent<PlaceholderStudentScript>().NESW = this.NESW;
				this.NewStudent.transform.parent = this.FakeStudentParent;
				this.CurrentFloor++;
				this.CurrentRow++;
				this.Spawned++;
				if (this.CurrentFloor == this.FloorLimit)
				{
					this.CurrentFloor = 0;
					this.Height += 4;
				}
				if (this.CurrentRow == this.RowLimit)
				{
					this.CurrentRow = 0;
					this.NESW++;
					if (this.NESW > 4)
					{
						this.NESW = 1;
					}
				}
				this.Student = ((this.Student == this.FakeFemale) ? this.FakeMale : this.FakeFemale);
			}
			this.StudentIDLimit = this.StudentID;
			this.StudentID = 1;
			this.AlreadySpawned = true;
			return;
		}
		this.FakeStudentParent.gameObject.SetActive(!this.FakeStudentParent.gameObject.activeInHierarchy);
	}

	// Token: 0x04001C0A RID: 7178
	public Transform FakeStudentParent;

	// Token: 0x04001C0B RID: 7179
	public GameObject NewStudent;

	// Token: 0x04001C0C RID: 7180
	public GameObject FakeFemale;

	// Token: 0x04001C0D RID: 7181
	public GameObject FakeMale;

	// Token: 0x04001C0E RID: 7182
	public GameObject Student;

	// Token: 0x04001C0F RID: 7183
	public bool AlreadySpawned;

	// Token: 0x04001C10 RID: 7184
	public int CurrentFloor;

	// Token: 0x04001C11 RID: 7185
	public int CurrentRow;

	// Token: 0x04001C12 RID: 7186
	public int FloorLimit;

	// Token: 0x04001C13 RID: 7187
	public int RowLimit;

	// Token: 0x04001C14 RID: 7188
	public int StudentIDLimit;

	// Token: 0x04001C15 RID: 7189
	public int StudentID;

	// Token: 0x04001C16 RID: 7190
	public int Spawned;

	// Token: 0x04001C17 RID: 7191
	public int Height;

	// Token: 0x04001C18 RID: 7192
	public int NESW;

	// Token: 0x04001C19 RID: 7193
	public int ID;

	// Token: 0x04001C1A RID: 7194
	public GameObject[] SuspiciousObjects;
}
