using System;
using UnityEngine;

// Token: 0x020000C6 RID: 198
public class AnimatedGifScript : MonoBehaviour
{
	// Token: 0x060009F8 RID: 2552 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Awake()
	{
	}

	// Token: 0x17000202 RID: 514
	// (get) Token: 0x060009F9 RID: 2553 RVA: 0x0004F82A File Offset: 0x0004DA2A
	private float SecondsPerFrame
	{
		get
		{
			return 1f / this.FramesPerSecond;
		}
	}

	// Token: 0x060009FA RID: 2554 RVA: 0x0004F838 File Offset: 0x0004DA38
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
		this.Sprite.spriteName = this.SpriteName + this.Frame.ToString();
	}

	// Token: 0x040009DC RID: 2524
	[SerializeField]
	private UISprite Sprite;

	// Token: 0x040009DD RID: 2525
	[SerializeField]
	private string SpriteName;

	// Token: 0x040009DE RID: 2526
	[SerializeField]
	private int Start;

	// Token: 0x040009DF RID: 2527
	[SerializeField]
	private int Frame;

	// Token: 0x040009E0 RID: 2528
	[SerializeField]
	private int Limit;

	// Token: 0x040009E1 RID: 2529
	[SerializeField]
	private float FramesPerSecond;

	// Token: 0x040009E2 RID: 2530
	[SerializeField]
	private float CurrentSeconds;
}
