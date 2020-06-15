using System;
using UnityEngine;

// Token: 0x02000267 RID: 615
[Serializable]
public class DetectionMarkerScript : MonoBehaviour
{
	// Token: 0x06001343 RID: 4931 RVA: 0x000A1794 File Offset: 0x0009F994
	private void Start()
	{
		base.transform.LookAt(new Vector3(this.Target.position.x, base.transform.position.y, this.Target.position.z));
		this.Tex.transform.localScale = new Vector3(1f, 0f, 1f);
		base.transform.localScale = new Vector3(1f, 1f, 1f);
		this.Tex.color = new Color(this.Tex.color.r, this.Tex.color.g, this.Tex.color.b, 0f);
	}

	// Token: 0x06001344 RID: 4932 RVA: 0x000A186C File Offset: 0x0009FA6C
	private void Update()
	{
		if (this.Tex.color.a > 0f && base.transform != null && this.Target != null)
		{
			base.transform.LookAt(new Vector3(this.Target.position.x, base.transform.position.y, this.Target.position.z));
		}
	}

	// Token: 0x04001A17 RID: 6679
	public Transform Target;

	// Token: 0x04001A18 RID: 6680
	public UITexture Tex;
}
