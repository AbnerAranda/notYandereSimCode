using System;
using UnityEngine;

// Token: 0x0200026E RID: 622
public class DoorBoxScript : MonoBehaviour
{
	// Token: 0x06001357 RID: 4951 RVA: 0x000A5510 File Offset: 0x000A3710
	private void Update()
	{
		float y = Mathf.Lerp(base.transform.localPosition.y, this.Show ? -530f : -630f, Time.deltaTime * 10f);
		base.transform.localPosition = new Vector3(base.transform.localPosition.x, y, base.transform.localPosition.z);
	}

	// Token: 0x04001A5A RID: 6746
	public UILabel Label;

	// Token: 0x04001A5B RID: 6747
	public bool Show;
}
