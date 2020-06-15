using System;
using UnityEngine;

// Token: 0x02000083 RID: 131
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Internal/Snapshot Point")]
public class UISnapshotPoint : MonoBehaviour
{
	// Token: 0x0600054E RID: 1358 RVA: 0x00032144 File Offset: 0x00030344
	private void Start()
	{
		if (base.tag != "EditorOnly")
		{
			base.tag = "EditorOnly";
		}
	}

	// Token: 0x04000582 RID: 1410
	public bool isOrthographic = true;

	// Token: 0x04000583 RID: 1411
	public float nearClip = -100f;

	// Token: 0x04000584 RID: 1412
	public float farClip = 100f;

	// Token: 0x04000585 RID: 1413
	[Range(10f, 80f)]
	public int fieldOfView = 35;

	// Token: 0x04000586 RID: 1414
	public float orthoSize = 30f;

	// Token: 0x04000587 RID: 1415
	public Texture2D thumbnail;
}
