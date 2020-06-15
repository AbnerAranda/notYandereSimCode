using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000AA RID: 170
[ExecuteInEditMode]
[RequireComponent(typeof(UISprite))]
[AddComponentMenu("NGUI/UI/Sprite Animation")]
public class UISpriteAnimation : MonoBehaviour
{
	// Token: 0x170001CB RID: 459
	// (get) Token: 0x060008C3 RID: 2243 RVA: 0x00046280 File Offset: 0x00044480
	public int frames
	{
		get
		{
			return this.mSpriteNames.Count;
		}
	}

	// Token: 0x170001CC RID: 460
	// (get) Token: 0x060008C4 RID: 2244 RVA: 0x0004628D File Offset: 0x0004448D
	// (set) Token: 0x060008C5 RID: 2245 RVA: 0x00046295 File Offset: 0x00044495
	public int framesPerSecond
	{
		get
		{
			return this.mFPS;
		}
		set
		{
			this.mFPS = value;
		}
	}

	// Token: 0x170001CD RID: 461
	// (get) Token: 0x060008C6 RID: 2246 RVA: 0x0004629E File Offset: 0x0004449E
	// (set) Token: 0x060008C7 RID: 2247 RVA: 0x000462A6 File Offset: 0x000444A6
	public string namePrefix
	{
		get
		{
			return this.mPrefix;
		}
		set
		{
			if (this.mPrefix != value)
			{
				this.mPrefix = value;
				this.RebuildSpriteList();
			}
		}
	}

	// Token: 0x170001CE RID: 462
	// (get) Token: 0x060008C8 RID: 2248 RVA: 0x000462C3 File Offset: 0x000444C3
	// (set) Token: 0x060008C9 RID: 2249 RVA: 0x000462CB File Offset: 0x000444CB
	public bool loop
	{
		get
		{
			return this.mLoop;
		}
		set
		{
			this.mLoop = value;
		}
	}

	// Token: 0x170001CF RID: 463
	// (get) Token: 0x060008CA RID: 2250 RVA: 0x000462D4 File Offset: 0x000444D4
	public bool isPlaying
	{
		get
		{
			return this.mActive;
		}
	}

	// Token: 0x060008CB RID: 2251 RVA: 0x000462DC File Offset: 0x000444DC
	protected virtual void Start()
	{
		this.RebuildSpriteList();
	}

	// Token: 0x060008CC RID: 2252 RVA: 0x000462E4 File Offset: 0x000444E4
	protected virtual void Update()
	{
		if (this.mActive && this.mSpriteNames.Count > 1 && Application.isPlaying && this.mFPS > 0)
		{
			this.mDelta += Mathf.Min(1f, RealTime.deltaTime);
			float num = 1f / (float)this.mFPS;
			while (num < this.mDelta)
			{
				this.mDelta = ((num > 0f) ? (this.mDelta - num) : 0f);
				int num2 = this.frameIndex + 1;
				this.frameIndex = num2;
				if (num2 >= this.mSpriteNames.Count)
				{
					this.frameIndex = 0;
					this.mActive = this.mLoop;
				}
				if (this.mActive)
				{
					this.mSprite.spriteName = this.mSpriteNames[this.frameIndex];
					if (this.mSnap)
					{
						this.mSprite.MakePixelPerfect();
					}
				}
			}
		}
	}

	// Token: 0x060008CD RID: 2253 RVA: 0x000463E4 File Offset: 0x000445E4
	public void RebuildSpriteList()
	{
		if (this.mSprite == null)
		{
			this.mSprite = base.GetComponent<UISprite>();
		}
		this.mSpriteNames.Clear();
		if (this.mSprite != null)
		{
			INGUIAtlas atlas = this.mSprite.atlas;
			if (atlas != null)
			{
				List<UISpriteData> spriteList = atlas.spriteList;
				int i = 0;
				int count = spriteList.Count;
				while (i < count)
				{
					UISpriteData uispriteData = spriteList[i];
					if (string.IsNullOrEmpty(this.mPrefix) || uispriteData.name.StartsWith(this.mPrefix))
					{
						this.mSpriteNames.Add(uispriteData.name);
					}
					i++;
				}
				this.mSpriteNames.Sort();
			}
		}
	}

	// Token: 0x060008CE RID: 2254 RVA: 0x00046494 File Offset: 0x00044694
	public void Play()
	{
		this.mActive = true;
	}

	// Token: 0x060008CF RID: 2255 RVA: 0x0004649D File Offset: 0x0004469D
	public void Pause()
	{
		this.mActive = false;
	}

	// Token: 0x060008D0 RID: 2256 RVA: 0x000464A8 File Offset: 0x000446A8
	public void ResetToBeginning()
	{
		this.mActive = true;
		this.frameIndex = 0;
		if (this.mSprite != null && this.mSpriteNames.Count > 0)
		{
			this.mSprite.spriteName = this.mSpriteNames[this.frameIndex];
			if (this.mSnap)
			{
				this.mSprite.MakePixelPerfect();
			}
		}
	}

	// Token: 0x04000772 RID: 1906
	public int frameIndex;

	// Token: 0x04000773 RID: 1907
	[HideInInspector]
	[SerializeField]
	protected int mFPS = 30;

	// Token: 0x04000774 RID: 1908
	[HideInInspector]
	[SerializeField]
	protected string mPrefix = "";

	// Token: 0x04000775 RID: 1909
	[HideInInspector]
	[SerializeField]
	protected bool mLoop = true;

	// Token: 0x04000776 RID: 1910
	[HideInInspector]
	[SerializeField]
	protected bool mSnap = true;

	// Token: 0x04000777 RID: 1911
	protected UISprite mSprite;

	// Token: 0x04000778 RID: 1912
	protected float mDelta;

	// Token: 0x04000779 RID: 1913
	protected bool mActive = true;

	// Token: 0x0400077A RID: 1914
	protected List<string> mSpriteNames = new List<string>();
}
