using System;
using UnityEngine;

// Token: 0x02000427 RID: 1063
public class TitleExtrasScript : MonoBehaviour
{
	// Token: 0x06001C56 RID: 7254 RVA: 0x001538C0 File Offset: 0x00151AC0
	private void Start()
	{
		base.transform.localPosition = new Vector3(1050f, base.transform.localPosition.y, base.transform.localPosition.z);
	}

	// Token: 0x06001C57 RID: 7255 RVA: 0x001538F8 File Offset: 0x00151AF8
	private void Update()
	{
		if (!this.Show)
		{
			base.transform.localPosition = new Vector3(Mathf.Lerp(base.transform.localPosition.x, 1050f, Time.deltaTime * 10f), base.transform.localPosition.y, base.transform.localPosition.z);
			return;
		}
		base.transform.localPosition = new Vector3(Mathf.Lerp(base.transform.localPosition.x, 0f, Time.deltaTime * 10f), base.transform.localPosition.y, base.transform.localPosition.z);
	}

	// Token: 0x0400352C RID: 13612
	public bool Show;
}
