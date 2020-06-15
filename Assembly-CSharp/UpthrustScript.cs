using System;
using UnityEngine;

// Token: 0x0200043B RID: 1083
public class UpthrustScript : MonoBehaviour
{
	// Token: 0x06001CA2 RID: 7330 RVA: 0x001583D4 File Offset: 0x001565D4
	private void Start()
	{
		this.startPosition = base.transform.localPosition;
	}

	// Token: 0x06001CA3 RID: 7331 RVA: 0x001583E8 File Offset: 0x001565E8
	private void Update()
	{
		float d = this.amplitude * Mathf.Sin(6.28318548f * this.frequency * Time.time);
		base.transform.localPosition = this.startPosition + this.evaluatePosition(Time.time);
		base.transform.Rotate(this.rotationAmplitude * d);
	}

	// Token: 0x06001CA4 RID: 7332 RVA: 0x0015844C File Offset: 0x0015664C
	private Vector3 evaluatePosition(float time)
	{
		float y = this.amplitude * Mathf.Sin(6.28318548f * this.frequency * time);
		return new Vector3(0f, y, 0f);
	}

	// Token: 0x0400364A RID: 13898
	[SerializeField]
	private float amplitude = 0.1f;

	// Token: 0x0400364B RID: 13899
	[SerializeField]
	private float frequency = 0.6f;

	// Token: 0x0400364C RID: 13900
	[SerializeField]
	private Vector3 rotationAmplitude = new Vector3(4.45f, 4.45f, 4.45f);

	// Token: 0x0400364D RID: 13901
	private Vector3 startPosition;
}
