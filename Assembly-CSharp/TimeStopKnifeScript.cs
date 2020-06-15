﻿using System;
using UnityEngine;

// Token: 0x02000426 RID: 1062
public class TimeStopKnifeScript : MonoBehaviour
{
	// Token: 0x06001C52 RID: 7250 RVA: 0x00153718 File Offset: 0x00151918
	private void Start()
	{
		base.transform.localScale = new Vector3(0f, 0f, 0f);
	}

	// Token: 0x06001C53 RID: 7251 RVA: 0x0015373C File Offset: 0x0015193C
	private void Update()
	{
		if (!this.Unfreeze)
		{
			this.Speed = Mathf.MoveTowards(this.Speed, 0f, Time.deltaTime);
			if (base.transform.localScale.x < 0.99f)
			{
				base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
			}
		}
		else
		{
			this.Speed = 10f;
			this.Timer += Time.deltaTime;
			if (this.Timer > 5f)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
		base.transform.Translate(Vector3.forward * this.Speed * Time.deltaTime, Space.Self);
	}

	// Token: 0x06001C54 RID: 7252 RVA: 0x0015381C File Offset: 0x00151A1C
	private void OnTriggerEnter(Collider other)
	{
		if (this.Unfreeze && other.gameObject.layer == 9)
		{
			StudentScript component = other.gameObject.GetComponent<StudentScript>();
			if (component != null && component.StudentID > 1)
			{
				if (component.Male)
				{
					UnityEngine.Object.Instantiate<GameObject>(this.MaleScream, base.transform.position, Quaternion.identity);
				}
				else
				{
					UnityEngine.Object.Instantiate<GameObject>(this.FemaleScream, base.transform.position, Quaternion.identity);
				}
				component.DeathType = DeathType.EasterEgg;
				component.BecomeRagdoll();
			}
		}
	}

	// Token: 0x04003527 RID: 13607
	public GameObject FemaleScream;

	// Token: 0x04003528 RID: 13608
	public GameObject MaleScream;

	// Token: 0x04003529 RID: 13609
	public bool Unfreeze;

	// Token: 0x0400352A RID: 13610
	public float Speed = 0.1f;

	// Token: 0x0400352B RID: 13611
	private float Timer;
}
