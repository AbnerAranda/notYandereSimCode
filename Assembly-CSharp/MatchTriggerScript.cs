using System;
using UnityEngine;

// Token: 0x0200032D RID: 813
public class MatchTriggerScript : MonoBehaviour
{
	// Token: 0x0600182E RID: 6190 RVA: 0x000D87CC File Offset: 0x000D69CC
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 9)
		{
			this.Student = other.gameObject.GetComponent<StudentScript>();
			if (this.Student == null)
			{
				GameObject gameObject = other.gameObject.transform.root.gameObject;
				this.Student = gameObject.GetComponent<StudentScript>();
			}
			if (this.Student != null && (this.Student.Gas || this.Fireball))
			{
				this.Student.Combust();
				if (this.PickUp != null && this.PickUp.Yandere.PickUp != null && this.PickUp.Yandere.PickUp == this.PickUp)
				{
					this.PickUp.Yandere.TargetStudent = this.Student;
					this.PickUp.Yandere.MurderousActionTimer = 1f;
				}
				if (this.Fireball)
				{
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
		}
	}

	// Token: 0x0400231D RID: 8989
	public PickUpScript PickUp;

	// Token: 0x0400231E RID: 8990
	public StudentScript Student;

	// Token: 0x0400231F RID: 8991
	public bool Fireball;
}
