using System;
using UnityEngine;

// Token: 0x020000BD RID: 189
public class ARMiyukiScript : MonoBehaviour
{
	// Token: 0x060009DB RID: 2523 RVA: 0x0004CC81 File Offset: 0x0004AE81
	private void Start()
	{
		if (this.Enemy == null)
		{
			this.Enemy = this.MyStudent.StudentManager.MiyukiCat;
		}
	}

	// Token: 0x060009DC RID: 2524 RVA: 0x0004CCA7 File Offset: 0x0004AEA7
	private void Update()
	{
		if (!this.Student && this.Yandere.AR)
		{
			base.transform.LookAt(this.Enemy.position);
			if (Input.GetButtonDown("X"))
			{
				this.Shoot();
			}
		}
	}

	// Token: 0x060009DD RID: 2525 RVA: 0x0004CCE8 File Offset: 0x0004AEE8
	public void Shoot()
	{
		if (this.Enemy == null)
		{
			this.Enemy = this.MyStudent.StudentManager.MiyukiCat;
		}
		base.transform.LookAt(this.Enemy.position);
		UnityEngine.Object.Instantiate<GameObject>(this.Bullet, this.BulletSpawnPoint.position, base.transform.rotation);
	}

	// Token: 0x0400084E RID: 2126
	public Transform BulletSpawnPoint;

	// Token: 0x0400084F RID: 2127
	public StudentScript MyStudent;

	// Token: 0x04000850 RID: 2128
	public YandereScript Yandere;

	// Token: 0x04000851 RID: 2129
	public GameObject Bullet;

	// Token: 0x04000852 RID: 2130
	public Transform Enemy;

	// Token: 0x04000853 RID: 2131
	public GameObject MagicalGirl;

	// Token: 0x04000854 RID: 2132
	public bool Student;
}
