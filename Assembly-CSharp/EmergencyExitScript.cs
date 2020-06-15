using System;
using UnityEngine;

// Token: 0x0200027E RID: 638
public class EmergencyExitScript : MonoBehaviour
{
	// Token: 0x0600139B RID: 5019 RVA: 0x000A94A4 File Offset: 0x000A76A4
	private void Update()
	{
		if (Vector3.Distance(this.Yandere.position, base.transform.position) < 2f)
		{
			this.Open = true;
		}
		else if (this.Timer == 0f)
		{
			this.Student = null;
			this.Open = false;
		}
		if (!this.Open)
		{
			this.Pivot.localEulerAngles = new Vector3(this.Pivot.localEulerAngles.x, Mathf.Lerp(this.Pivot.localEulerAngles.y, 0f, Time.deltaTime * 10f), this.Pivot.localEulerAngles.z);
			return;
		}
		this.Pivot.localEulerAngles = new Vector3(this.Pivot.localEulerAngles.x, Mathf.Lerp(this.Pivot.localEulerAngles.y, 90f, Time.deltaTime * 10f), this.Pivot.localEulerAngles.z);
		this.Timer = Mathf.MoveTowards(this.Timer, 0f, Time.deltaTime);
	}

	// Token: 0x0600139C RID: 5020 RVA: 0x000A95C5 File Offset: 0x000A77C5
	private void OnTriggerStay(Collider other)
	{
		this.Student = other.gameObject.GetComponent<StudentScript>();
		if (this.Student != null)
		{
			this.Timer = 1f;
			this.Open = true;
		}
	}

	// Token: 0x04001AE3 RID: 6883
	public StudentScript Student;

	// Token: 0x04001AE4 RID: 6884
	public Transform Yandere;

	// Token: 0x04001AE5 RID: 6885
	public Transform Pivot;

	// Token: 0x04001AE6 RID: 6886
	public float Timer;

	// Token: 0x04001AE7 RID: 6887
	public bool Open;
}
