using System;
using UnityEngine;

// Token: 0x02000227 RID: 551
public class CameraShake : MonoBehaviour
{
	// Token: 0x0600121D RID: 4637 RVA: 0x000808E5 File Offset: 0x0007EAE5
	private void Awake()
	{
		if (this.camTransform == null)
		{
			this.camTransform = base.GetComponent<Transform>();
		}
	}

	// Token: 0x0600121E RID: 4638 RVA: 0x00080901 File Offset: 0x0007EB01
	private void OnEnable()
	{
		this.originalPos = this.camTransform.localPosition;
	}

	// Token: 0x0600121F RID: 4639 RVA: 0x00080914 File Offset: 0x0007EB14
	private void Update()
	{
		if (this.shake > 0f)
		{
			this.camTransform.localPosition = this.originalPos + UnityEngine.Random.insideUnitSphere * this.shakeAmount;
			this.shake -= 0.0166666675f * this.decreaseFactor;
			return;
		}
		this.shake = 0f;
		this.camTransform.localPosition = this.originalPos;
	}

	// Token: 0x0400155B RID: 5467
	public Transform camTransform;

	// Token: 0x0400155C RID: 5468
	public float shake;

	// Token: 0x0400155D RID: 5469
	public float shakeAmount = 0.7f;

	// Token: 0x0400155E RID: 5470
	public float decreaseFactor = 1f;

	// Token: 0x0400155F RID: 5471
	private Vector3 originalPos;
}
