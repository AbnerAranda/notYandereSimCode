using System;
using UnityEngine;

// Token: 0x02000460 RID: 1120
public class VibrateScript : MonoBehaviour
{
	// Token: 0x06001D10 RID: 7440 RVA: 0x001594B2 File Offset: 0x001576B2
	private void Start()
	{
		this.Origin = base.transform.localPosition;
	}

	// Token: 0x06001D11 RID: 7441 RVA: 0x001594C8 File Offset: 0x001576C8
	private void Update()
	{
		base.transform.localPosition = new Vector3(this.Origin.x + UnityEngine.Random.Range(-5f, 5f), this.Origin.y + UnityEngine.Random.Range(-5f, 5f), base.transform.localPosition.z);
	}

	// Token: 0x04003679 RID: 13945
	public Vector3 Origin;
}
