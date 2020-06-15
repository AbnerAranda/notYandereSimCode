using System;
using UnityEngine;

// Token: 0x0200027F RID: 639
public class EmptyHuskScript : MonoBehaviour
{
	// Token: 0x0600139E RID: 5022 RVA: 0x000A95F8 File Offset: 0x000A77F8
	private void Update()
	{
		if (this.EatPhase < this.BloodTimes.Length && this.MyAnimation["f02_sixEat_00"].time > this.BloodTimes[this.EatPhase])
		{
			UnityEngine.Object.Instantiate<GameObject>(this.TargetStudent.StabBloodEffect, this.Mouth.position, Quaternion.identity).GetComponent<RandomStabScript>().Biting = true;
			this.EatPhase++;
		}
		if (this.MyAnimation["f02_sixEat_00"].time >= this.MyAnimation["f02_sixEat_00"].length)
		{
			if (this.DarkAura != null)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.DarkAura, base.transform.position + Vector3.up * 0.81f, Quaternion.identity);
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04001AE8 RID: 6888
	public StudentScript TargetStudent;

	// Token: 0x04001AE9 RID: 6889
	public Animation MyAnimation;

	// Token: 0x04001AEA RID: 6890
	public GameObject DarkAura;

	// Token: 0x04001AEB RID: 6891
	public Transform Mouth;

	// Token: 0x04001AEC RID: 6892
	public float[] BloodTimes;

	// Token: 0x04001AED RID: 6893
	public int EatPhase;
}
