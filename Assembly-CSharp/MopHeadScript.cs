using System;
using UnityEngine;

// Token: 0x02000339 RID: 825
public class MopHeadScript : MonoBehaviour
{
	// Token: 0x06001855 RID: 6229 RVA: 0x000DAA40 File Offset: 0x000D8C40
	private void OnTriggerStay(Collider other)
	{
		if (this.Mop.Bloodiness < 100f && other.tag == "Puddle")
		{
			this.BloodPool = other.gameObject.GetComponent<BloodPoolScript>();
			if (this.BloodPool != null)
			{
				this.BloodPool.Grow = false;
				other.transform.localScale -= new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
				if (this.BloodPool.Blood)
				{
					this.Mop.Bloodiness += Time.deltaTime * 10f;
					this.Mop.UpdateBlood();
				}
				if (other.transform.localScale.x < 0.1f)
				{
					UnityEngine.Object.Destroy(other.gameObject);
					return;
				}
			}
			else
			{
				UnityEngine.Object.Destroy(other.gameObject);
			}
		}
	}

	// Token: 0x04002367 RID: 9063
	public BloodPoolScript BloodPool;

	// Token: 0x04002368 RID: 9064
	public MopScript Mop;
}
