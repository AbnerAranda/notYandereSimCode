using System;
using UnityEngine;

// Token: 0x0200036C RID: 876
public class PodScript : MonoBehaviour
{
	// Token: 0x0600191F RID: 6431 RVA: 0x000EC7C0 File Offset: 0x000EA9C0
	private void Start()
	{
		this.Timer = 1f;
	}

	// Token: 0x06001920 RID: 6432 RVA: 0x000EC7D0 File Offset: 0x000EA9D0
	private void LateUpdate()
	{
		this.PodTarget.transform.parent.eulerAngles = new Vector3(0f, this.AimTarget.parent.eulerAngles.y, 0f);
		base.transform.position = Vector3.Lerp(base.transform.position, this.PodTarget.position, Time.deltaTime * 100f);
		base.transform.rotation = this.AimTarget.parent.rotation;
		base.transform.eulerAngles += new Vector3(-15f, 7.5f, 0f);
		if (Input.GetButton("RB"))
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > this.FireRate)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.Projectile, this.SpawnPoint.position, base.transform.rotation);
				this.Timer = 0f;
			}
		}
	}

	// Token: 0x040025E1 RID: 9697
	public GameObject Projectile;

	// Token: 0x040025E2 RID: 9698
	public Transform SpawnPoint;

	// Token: 0x040025E3 RID: 9699
	public Transform PodTarget;

	// Token: 0x040025E4 RID: 9700
	public Transform AimTarget;

	// Token: 0x040025E5 RID: 9701
	public float FireRate;

	// Token: 0x040025E6 RID: 9702
	public float Timer;
}
