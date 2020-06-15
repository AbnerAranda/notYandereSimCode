using System;
using UnityEngine;

// Token: 0x0200003E RID: 62
[AddComponentMenu("NGUI/Examples/Window Auto-Yaw")]
public class WindowAutoYaw : MonoBehaviour
{
	// Token: 0x06000146 RID: 326 RVA: 0x0001365A File Offset: 0x0001185A
	private void OnDisable()
	{
		this.mTrans.localRotation = Quaternion.identity;
	}

	// Token: 0x06000147 RID: 327 RVA: 0x0001366C File Offset: 0x0001186C
	private void OnEnable()
	{
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		this.mTrans = base.transform;
	}

	// Token: 0x06000148 RID: 328 RVA: 0x000136A0 File Offset: 0x000118A0
	private void Update()
	{
		if (this.uiCamera != null)
		{
			Vector3 vector = this.uiCamera.WorldToViewportPoint(this.mTrans.position);
			this.mTrans.localRotation = Quaternion.Euler(0f, (vector.x * 2f - 1f) * this.yawAmount, 0f);
		}
	}

	// Token: 0x040002D6 RID: 726
	public int updateOrder;

	// Token: 0x040002D7 RID: 727
	public Camera uiCamera;

	// Token: 0x040002D8 RID: 728
	public float yawAmount = 20f;

	// Token: 0x040002D9 RID: 729
	private Transform mTrans;
}
