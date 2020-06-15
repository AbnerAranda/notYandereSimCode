using System;
using UnityEngine;

// Token: 0x0200007C RID: 124
public class RealTime : MonoBehaviour
{
	// Token: 0x1700007B RID: 123
	// (get) Token: 0x060004B6 RID: 1206 RVA: 0x0002CD8B File Offset: 0x0002AF8B
	public static float time
	{
		get
		{
			return Time.unscaledTime;
		}
	}

	// Token: 0x1700007C RID: 124
	// (get) Token: 0x060004B7 RID: 1207 RVA: 0x0002CD92 File Offset: 0x0002AF92
	public static float deltaTime
	{
		get
		{
			return Time.unscaledDeltaTime;
		}
	}
}
