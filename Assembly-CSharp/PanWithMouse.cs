using System;
using UnityEngine;

// Token: 0x02000036 RID: 54
[AddComponentMenu("NGUI/Examples/Pan With Mouse")]
public class PanWithMouse : MonoBehaviour
{
	// Token: 0x0600012F RID: 303 RVA: 0x00012EE3 File Offset: 0x000110E3
	private void Start()
	{
		this.mTrans = base.transform;
		this.mStart = this.mTrans.localRotation;
	}

	// Token: 0x06000130 RID: 304 RVA: 0x00012F04 File Offset: 0x00011104
	private void Update()
	{
		float deltaTime = RealTime.deltaTime;
		Vector3 vector = UICamera.lastEventPosition;
		float num = (float)Screen.width * 0.5f;
		float num2 = (float)Screen.height * 0.5f;
		if (this.range < 0.1f)
		{
			this.range = 0.1f;
		}
		float x = Mathf.Clamp((vector.x - num) / num / this.range, -1f, 1f);
		float y = Mathf.Clamp((vector.y - num2) / num2 / this.range, -1f, 1f);
		this.mRot = Vector2.Lerp(this.mRot, new Vector2(x, y), deltaTime * 5f);
		this.mTrans.localRotation = this.mStart * Quaternion.Euler(-this.mRot.y * this.degrees.y, this.mRot.x * this.degrees.x, 0f);
	}

	// Token: 0x040002BF RID: 703
	public Vector2 degrees = new Vector2(5f, 3f);

	// Token: 0x040002C0 RID: 704
	public float range = 1f;

	// Token: 0x040002C1 RID: 705
	private Transform mTrans;

	// Token: 0x040002C2 RID: 706
	private Quaternion mStart;

	// Token: 0x040002C3 RID: 707
	private Vector2 mRot = Vector2.zero;
}
