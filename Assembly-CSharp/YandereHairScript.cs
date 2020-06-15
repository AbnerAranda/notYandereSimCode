using System;
using UnityEngine;

// Token: 0x0200046E RID: 1134
public class YandereHairScript : MonoBehaviour
{
	// Token: 0x06001D54 RID: 7508 RVA: 0x00160618 File Offset: 0x0015E818
	private void Start()
	{
		ScreenCapture.CaptureScreenshot(string.Concat(new object[]
		{
			Application.streamingAssetsPath,
			"/YandereHair/Hair_",
			this.Yandere.Hairstyle,
			".png"
		}));
		this.Limit = this.Yandere.Hairstyles.Length - 1;
	}

	// Token: 0x06001D55 RID: 7509 RVA: 0x00160678 File Offset: 0x0015E878
	private void Update()
	{
		if (this.Yandere.Hairstyle < this.Limit)
		{
			this.Frame++;
			if (this.Frame == 1)
			{
				this.Yandere.Hairstyle++;
				this.Yandere.UpdateHair();
			}
			if (this.Frame == 2)
			{
				ScreenCapture.CaptureScreenshot(string.Concat(new object[]
				{
					Application.streamingAssetsPath,
					"/YandereHair/Hair_",
					this.Yandere.Hairstyle,
					".png"
				}));
				this.Frame = 0;
			}
		}
	}

	// Token: 0x0400378D RID: 14221
	public YandereScript Yandere;

	// Token: 0x0400378E RID: 14222
	public int Frame;

	// Token: 0x0400378F RID: 14223
	public int Limit;
}
