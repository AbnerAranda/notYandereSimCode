using System;
using UnityEngine;

// Token: 0x020003D3 RID: 979
public class ScrollingTexture : MonoBehaviour
{
	// Token: 0x06001A73 RID: 6771 RVA: 0x001040E7 File Offset: 0x001022E7
	private void Start()
	{
		this.MyRenderer = base.GetComponent<Renderer>();
	}

	// Token: 0x06001A74 RID: 6772 RVA: 0x001040F8 File Offset: 0x001022F8
	private void Update()
	{
		this.Offset += Time.deltaTime * this.Speed;
		this.MyRenderer.material.SetTextureOffset("_MainTex", new Vector2(this.Offset, this.Offset));
	}

	// Token: 0x04002A13 RID: 10771
	public Renderer MyRenderer;

	// Token: 0x04002A14 RID: 10772
	public float Offset;

	// Token: 0x04002A15 RID: 10773
	public float Speed;
}
