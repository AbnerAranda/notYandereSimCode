using System;
using UnityEngine;

// Token: 0x0200038A RID: 906
public class RainbowScript : MonoBehaviour
{
	// Token: 0x060019A0 RID: 6560 RVA: 0x000FAEF5 File Offset: 0x000F90F5
	private void Start()
	{
		this.MyRenderer.material.color = Color.red;
		this.cyclesPerSecond = 0.25f;
	}

	// Token: 0x060019A1 RID: 6561 RVA: 0x000FAF18 File Offset: 0x000F9118
	private void Update()
	{
		this.percent = (this.percent + Time.deltaTime * this.cyclesPerSecond) % 1f;
		this.MyRenderer.material.color = Color.HSVToRGB(this.percent, 1f, 1f);
	}

	// Token: 0x040027B0 RID: 10160
	[SerializeField]
	private Renderer MyRenderer;

	// Token: 0x040027B1 RID: 10161
	[SerializeField]
	private float cyclesPerSecond;

	// Token: 0x040027B2 RID: 10162
	[SerializeField]
	private float percent;
}
