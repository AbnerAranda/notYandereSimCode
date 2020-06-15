using System;
using UnityEngine;

// Token: 0x02000032 RID: 50
[AddComponentMenu("NGUI/Examples/Lag Rotation")]
public class LagRotation : MonoBehaviour
{
	// Token: 0x06000123 RID: 291 RVA: 0x00012D1F File Offset: 0x00010F1F
	public void OnRepositionEnd()
	{
		this.Interpolate(1000f);
	}

	// Token: 0x06000124 RID: 292 RVA: 0x00012D2C File Offset: 0x00010F2C
	private void Interpolate(float delta)
	{
		if (this.mTrans != null)
		{
			Transform parent = this.mTrans.parent;
			if (parent != null)
			{
				this.mAbsolute = Quaternion.Slerp(this.mAbsolute, parent.rotation * this.mRelative, delta * this.speed);
				this.mTrans.rotation = this.mAbsolute;
			}
		}
	}

	// Token: 0x06000125 RID: 293 RVA: 0x00012D97 File Offset: 0x00010F97
	private void Start()
	{
		this.mTrans = base.transform;
		this.mRelative = this.mTrans.localRotation;
		this.mAbsolute = this.mTrans.rotation;
	}

	// Token: 0x06000126 RID: 294 RVA: 0x00012DC7 File Offset: 0x00010FC7
	private void Update()
	{
		this.Interpolate(this.ignoreTimeScale ? RealTime.deltaTime : Time.deltaTime);
	}

	// Token: 0x040002B5 RID: 693
	public float speed = 10f;

	// Token: 0x040002B6 RID: 694
	public bool ignoreTimeScale;

	// Token: 0x040002B7 RID: 695
	private Transform mTrans;

	// Token: 0x040002B8 RID: 696
	private Quaternion mRelative;

	// Token: 0x040002B9 RID: 697
	private Quaternion mAbsolute;
}
