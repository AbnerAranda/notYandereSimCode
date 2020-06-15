using System;
using UnityEngine;

// Token: 0x02000034 RID: 52
[AddComponentMenu("NGUI/Examples/Look At Target")]
public class LookAtTarget : MonoBehaviour
{
	// Token: 0x0600012A RID: 298 RVA: 0x00012E10 File Offset: 0x00011010
	private void Start()
	{
		this.mTrans = base.transform;
	}

	// Token: 0x0600012B RID: 299 RVA: 0x00012E20 File Offset: 0x00011020
	private void LateUpdate()
	{
		if (this.target != null)
		{
			Vector3 forward = this.target.position - this.mTrans.position;
			if (forward.magnitude > 0.001f)
			{
				Quaternion b = Quaternion.LookRotation(forward);
				this.mTrans.rotation = Quaternion.Slerp(this.mTrans.rotation, b, Mathf.Clamp01(this.speed * Time.deltaTime));
			}
		}
	}

	// Token: 0x040002BB RID: 699
	public int level;

	// Token: 0x040002BC RID: 700
	public Transform target;

	// Token: 0x040002BD RID: 701
	public float speed = 8f;

	// Token: 0x040002BE RID: 702
	private Transform mTrans;
}
