using System;
using UnityEngine;

// Token: 0x0200003F RID: 63
[AddComponentMenu("NGUI/Examples/Window Drag Tilt")]
public class WindowDragTilt : MonoBehaviour
{
	// Token: 0x0600014A RID: 330 RVA: 0x00013718 File Offset: 0x00011918
	private void OnEnable()
	{
		this.mTrans = base.transform;
		this.mLastPos = this.mTrans.position;
	}

	// Token: 0x0600014B RID: 331 RVA: 0x00013738 File Offset: 0x00011938
	private void Update()
	{
		Vector3 vector = this.mTrans.position - this.mLastPos;
		this.mLastPos = this.mTrans.position;
		this.mAngle += vector.x * this.degrees;
		this.mAngle = NGUIMath.SpringLerp(this.mAngle, 0f, 20f, Time.deltaTime);
		this.mTrans.localRotation = Quaternion.Euler(0f, 0f, -this.mAngle);
	}

	// Token: 0x040002DA RID: 730
	public int updateOrder;

	// Token: 0x040002DB RID: 731
	public float degrees = 30f;

	// Token: 0x040002DC RID: 732
	private Vector3 mLastPos;

	// Token: 0x040002DD RID: 733
	private Transform mTrans;

	// Token: 0x040002DE RID: 734
	private float mAngle;
}
