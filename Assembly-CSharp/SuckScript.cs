using System;
using UnityEngine;

// Token: 0x02000412 RID: 1042
public class SuckScript : MonoBehaviour
{
	// Token: 0x06001C0C RID: 7180 RVA: 0x00149D44 File Offset: 0x00147F44
	private void Update()
	{
		this.Strength += Time.deltaTime;
		base.transform.position = Vector3.MoveTowards(base.transform.position, this.Student.Yandere.Hips.position + base.transform.up * 0.25f, Time.deltaTime * this.Strength);
		if (Vector3.Distance(base.transform.position, this.Student.Yandere.Hips.position + base.transform.up * 0.25f) < 1f)
		{
			base.transform.localScale = Vector3.MoveTowards(base.transform.localScale, Vector3.zero, Time.deltaTime);
			if (base.transform.localScale == Vector3.zero)
			{
				base.transform.parent.parent.parent.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x0400343F RID: 13375
	public StudentScript Student;

	// Token: 0x04003440 RID: 13376
	public float Strength;
}
