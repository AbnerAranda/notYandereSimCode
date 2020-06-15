using System;
using UnityEngine;

// Token: 0x02000422 RID: 1058
public class TextureCycleScript : MonoBehaviour
{
	// Token: 0x06001C47 RID: 7239 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Awake()
	{
	}

	// Token: 0x17000473 RID: 1139
	// (get) Token: 0x06001C48 RID: 7240 RVA: 0x00152FF4 File Offset: 0x001511F4
	private float SecondsPerFrame
	{
		get
		{
			return 1f / this.FramesPerSecond;
		}
	}

	// Token: 0x06001C49 RID: 7241 RVA: 0x00153004 File Offset: 0x00151204
	private void Update()
	{
		this.ID++;
		if (this.ID > 1)
		{
			this.ID = 0;
			this.Frame++;
			if (this.Frame > this.Limit)
			{
				this.Frame = this.Start;
			}
		}
		this.Sprite.mainTexture = this.Textures[this.Frame];
	}

	// Token: 0x04003505 RID: 13573
	public UITexture Sprite;

	// Token: 0x04003506 RID: 13574
	[SerializeField]
	private int Start;

	// Token: 0x04003507 RID: 13575
	[SerializeField]
	private int Frame;

	// Token: 0x04003508 RID: 13576
	[SerializeField]
	private int Limit;

	// Token: 0x04003509 RID: 13577
	[SerializeField]
	private float FramesPerSecond;

	// Token: 0x0400350A RID: 13578
	[SerializeField]
	private float CurrentSeconds;

	// Token: 0x0400350B RID: 13579
	[SerializeField]
	private Texture[] Textures;

	// Token: 0x0400350C RID: 13580
	public int ID;
}
