using System;
using UnityEngine;

// Token: 0x020000DE RID: 222
public class BloodParentScript : MonoBehaviour
{
	// Token: 0x06000A54 RID: 2644 RVA: 0x00055770 File Offset: 0x00053970
	public void RecordAllBlood()
	{
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			BloodPoolScript component = transform.GetComponent<BloodPoolScript>();
			if (component != null)
			{
				this.PoolID++;
				if (this.PoolID < 100)
				{
					this.BloodPositions[this.PoolID] = transform.position;
					this.BloodRotations[this.PoolID] = transform.eulerAngles;
					this.BloodSizes[this.PoolID] = component.TargetSize;
				}
			}
			else
			{
				this.FootprintID++;
				if (this.FootprintID < 100)
				{
					this.FootprintPositions[this.FootprintID] = transform.position;
					this.FootprintRotations[this.FootprintID] = transform.eulerAngles;
				}
			}
		}
	}

	// Token: 0x06000A55 RID: 2645 RVA: 0x0005587C File Offset: 0x00053A7C
	public void RestoreAllBlood()
	{
		while (this.PoolID > 0)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Bloodpool, this.BloodPositions[this.PoolID], Quaternion.identity);
			gameObject.GetComponent<BloodPoolScript>().TargetSize = this.BloodSizes[this.PoolID];
			gameObject.transform.eulerAngles = this.BloodRotations[this.PoolID];
			gameObject.transform.parent = base.transform;
			this.PoolID--;
		}
		while (this.FootprintID > 0)
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.Footprint, this.FootprintPositions[this.FootprintID], Quaternion.identity);
			gameObject2.transform.eulerAngles = this.FootprintRotations[this.FootprintID];
			gameObject2.transform.parent = base.transform;
			this.FootprintID--;
		}
	}

	// Token: 0x04000AA7 RID: 2727
	public GameObject Bloodpool;

	// Token: 0x04000AA8 RID: 2728
	public GameObject Footprint;

	// Token: 0x04000AA9 RID: 2729
	public Vector3[] FootprintPositions;

	// Token: 0x04000AAA RID: 2730
	public Vector3[] BloodPositions;

	// Token: 0x04000AAB RID: 2731
	public Vector3[] FootprintRotations;

	// Token: 0x04000AAC RID: 2732
	public Vector3[] BloodRotations;

	// Token: 0x04000AAD RID: 2733
	public float[] BloodSizes;

	// Token: 0x04000AAE RID: 2734
	public int FootprintID;

	// Token: 0x04000AAF RID: 2735
	public int PoolID;
}
