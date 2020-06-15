using System;
using UnityEngine;

// Token: 0x0200031D RID: 797
[RequireComponent(typeof(Camera))]
public class Letterboxing : MonoBehaviour
{
	// Token: 0x060017FE RID: 6142 RVA: 0x000D4098 File Offset: 0x000D2298
	private void Start()
	{
		float num = (float)Screen.width / (float)Screen.height;
		float num2 = 1f - num / 1.77777779f;
		base.GetComponent<Camera>().rect = new Rect(0f, num2 / 2f, 1f, 1f - num2);
	}

	// Token: 0x04002275 RID: 8821
	private const float KEEP_ASPECT = 1.77777779f;
}
