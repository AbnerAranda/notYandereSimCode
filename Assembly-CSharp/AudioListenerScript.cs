using System;
using UnityEngine;

// Token: 0x020000D5 RID: 213
public class AudioListenerScript : MonoBehaviour
{
	// Token: 0x06000A38 RID: 2616 RVA: 0x0005446E File Offset: 0x0005266E
	private void Start()
	{
		this.mainCamera = Camera.main;
	}

	// Token: 0x06000A39 RID: 2617 RVA: 0x0005447B File Offset: 0x0005267B
	private void Update()
	{
		base.transform.position = this.Target.position;
		base.transform.eulerAngles = this.mainCamera.transform.eulerAngles;
	}

	// Token: 0x04000A68 RID: 2664
	public Transform Target;

	// Token: 0x04000A69 RID: 2665
	public Camera mainCamera;
}
