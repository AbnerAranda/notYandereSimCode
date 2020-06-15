using System;
using UnityEngine;

// Token: 0x02000266 RID: 614
public class DetectionCameraScript : MonoBehaviour
{
	// Token: 0x06001341 RID: 4929 RVA: 0x000A1720 File Offset: 0x0009F920
	private void Update()
	{
		base.transform.position = this.YandereChan.transform.position + Vector3.up * 100f;
		base.transform.eulerAngles = new Vector3(90f, base.transform.eulerAngles.y, base.transform.eulerAngles.z);
	}

	// Token: 0x04001A16 RID: 6678
	public Transform YandereChan;
}
