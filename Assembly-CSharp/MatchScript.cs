using System;
using UnityEngine;

// Token: 0x0200032C RID: 812
public class MatchScript : MonoBehaviour
{
	// Token: 0x0600182C RID: 6188 RVA: 0x000D86E0 File Offset: 0x000D68E0
	private void Update()
	{
		if (base.GetComponent<Rigidbody>().useGravity)
		{
			base.transform.Rotate(Vector3.right * (Time.deltaTime * 360f));
			if (this.Timer > 0f && this.MyCollider.isTrigger)
			{
				this.MyCollider.isTrigger = false;
			}
			this.Timer += Time.deltaTime;
			if (this.Timer > 5f)
			{
				base.transform.localScale = new Vector3(base.transform.localScale.x, base.transform.localScale.y, base.transform.localScale.z - Time.deltaTime);
				if (base.transform.localScale.z < 0f)
				{
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
		}
	}

	// Token: 0x0400231B RID: 8987
	public float Timer;

	// Token: 0x0400231C RID: 8988
	public Collider MyCollider;
}
