using System;
using UnityEngine;

// Token: 0x02000031 RID: 49
public class LagPosition : MonoBehaviour
{
	// Token: 0x0600011B RID: 283 RVA: 0x00012B96 File Offset: 0x00010D96
	public void OnRepositionEnd()
	{
		this.Interpolate(1000f);
	}

	// Token: 0x0600011C RID: 284 RVA: 0x00012BA4 File Offset: 0x00010DA4
	private void Interpolate(float delta)
	{
		Transform parent = this.mTrans.parent;
		if (parent != null)
		{
			Vector3 vector = parent.position + parent.rotation * this.mRelative;
			this.mAbsolute.x = Mathf.Lerp(this.mAbsolute.x, vector.x, Mathf.Clamp01(delta * this.speed.x));
			this.mAbsolute.y = Mathf.Lerp(this.mAbsolute.y, vector.y, Mathf.Clamp01(delta * this.speed.y));
			this.mAbsolute.z = Mathf.Lerp(this.mAbsolute.z, vector.z, Mathf.Clamp01(delta * this.speed.z));
			this.mTrans.position = this.mAbsolute;
		}
	}

	// Token: 0x0600011D RID: 285 RVA: 0x00012C90 File Offset: 0x00010E90
	private void Awake()
	{
		this.mTrans = base.transform;
	}

	// Token: 0x0600011E RID: 286 RVA: 0x00012C9E File Offset: 0x00010E9E
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.ResetPosition();
		}
	}

	// Token: 0x0600011F RID: 287 RVA: 0x00012CAE File Offset: 0x00010EAE
	private void Start()
	{
		this.mStarted = true;
		this.ResetPosition();
	}

	// Token: 0x06000120 RID: 288 RVA: 0x00012CBD File Offset: 0x00010EBD
	public void ResetPosition()
	{
		this.mAbsolute = this.mTrans.position;
		this.mRelative = this.mTrans.localPosition;
	}

	// Token: 0x06000121 RID: 289 RVA: 0x00012CE1 File Offset: 0x00010EE1
	private void Update()
	{
		this.Interpolate(this.ignoreTimeScale ? RealTime.deltaTime : Time.deltaTime);
	}

	// Token: 0x040002AF RID: 687
	public Vector3 speed = new Vector3(10f, 10f, 10f);

	// Token: 0x040002B0 RID: 688
	public bool ignoreTimeScale;

	// Token: 0x040002B1 RID: 689
	private Transform mTrans;

	// Token: 0x040002B2 RID: 690
	private Vector3 mRelative;

	// Token: 0x040002B3 RID: 691
	private Vector3 mAbsolute;

	// Token: 0x040002B4 RID: 692
	private bool mStarted;
}
