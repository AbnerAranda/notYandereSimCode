using System;
using UnityEngine;

// Token: 0x020000C7 RID: 199
public class AnimatedTextureScript : MonoBehaviour
{
	// Token: 0x060009FC RID: 2556 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Awake()
	{
	}

	// Token: 0x17000203 RID: 515
	// (get) Token: 0x060009FD RID: 2557 RVA: 0x0004F8C3 File Offset: 0x0004DAC3
	private float SecondsPerFrame
	{
		get
		{
			return 1f / this.FramesPerSecond;
		}
	}

	// Token: 0x060009FE RID: 2558 RVA: 0x0004F8D4 File Offset: 0x0004DAD4
	private void Update()
	{
		this.CurrentSeconds += Time.unscaledDeltaTime;
		while (this.CurrentSeconds >= this.SecondsPerFrame)
		{
			this.CurrentSeconds -= this.SecondsPerFrame;
			this.Frame++;
			if (this.Frame > this.Limit)
			{
				this.Frame = this.Start;
			}
		}
		this.MyRenderer.material.mainTexture = this.Image[this.Frame];
	}

	// Token: 0x040009E3 RID: 2531
	[SerializeField]
	private Renderer MyRenderer;

	// Token: 0x040009E4 RID: 2532
	[SerializeField]
	private int Start;

	// Token: 0x040009E5 RID: 2533
	[SerializeField]
	private int Frame;

	// Token: 0x040009E6 RID: 2534
	[SerializeField]
	private int Limit;

	// Token: 0x040009E7 RID: 2535
	[SerializeField]
	private float FramesPerSecond;

	// Token: 0x040009E8 RID: 2536
	[SerializeField]
	private float CurrentSeconds;

	// Token: 0x040009E9 RID: 2537
	public Texture[] Image;
}
