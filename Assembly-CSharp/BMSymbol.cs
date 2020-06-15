using System;
using UnityEngine;

// Token: 0x0200006F RID: 111
[Serializable]
public class BMSymbol
{
	// Token: 0x1700005F RID: 95
	// (get) Token: 0x0600035E RID: 862 RVA: 0x0002080F File Offset: 0x0001EA0F
	public int length
	{
		get
		{
			if (this.mLength == 0)
			{
				this.mLength = this.sequence.Length;
			}
			return this.mLength;
		}
	}

	// Token: 0x17000060 RID: 96
	// (get) Token: 0x0600035F RID: 863 RVA: 0x00020830 File Offset: 0x0001EA30
	public int offsetX
	{
		get
		{
			return this.mOffsetX;
		}
	}

	// Token: 0x17000061 RID: 97
	// (get) Token: 0x06000360 RID: 864 RVA: 0x00020838 File Offset: 0x0001EA38
	public int offsetY
	{
		get
		{
			return this.mOffsetY;
		}
	}

	// Token: 0x17000062 RID: 98
	// (get) Token: 0x06000361 RID: 865 RVA: 0x00020840 File Offset: 0x0001EA40
	public int width
	{
		get
		{
			return this.mWidth;
		}
	}

	// Token: 0x17000063 RID: 99
	// (get) Token: 0x06000362 RID: 866 RVA: 0x00020848 File Offset: 0x0001EA48
	public int height
	{
		get
		{
			return this.mHeight;
		}
	}

	// Token: 0x17000064 RID: 100
	// (get) Token: 0x06000363 RID: 867 RVA: 0x00020850 File Offset: 0x0001EA50
	public int advance
	{
		get
		{
			return this.mAdvance;
		}
	}

	// Token: 0x17000065 RID: 101
	// (get) Token: 0x06000364 RID: 868 RVA: 0x00020858 File Offset: 0x0001EA58
	public Rect uvRect
	{
		get
		{
			return this.mUV;
		}
	}

	// Token: 0x06000365 RID: 869 RVA: 0x00020860 File Offset: 0x0001EA60
	public void MarkAsChanged()
	{
		this.mIsValid = false;
	}

	// Token: 0x06000366 RID: 870 RVA: 0x0002086C File Offset: 0x0001EA6C
	public bool Validate(INGUIAtlas atlas)
	{
		if (atlas == null)
		{
			return false;
		}
		if (!this.mIsValid)
		{
			if (string.IsNullOrEmpty(this.spriteName))
			{
				return false;
			}
			this.mSprite = atlas.GetSprite(this.spriteName);
			Texture texture = atlas.texture;
			if (this.mSprite != null)
			{
				if (texture == null)
				{
					this.mSprite = null;
				}
				else
				{
					this.mUV = new Rect((float)this.mSprite.x, (float)this.mSprite.y, (float)this.mSprite.width, (float)this.mSprite.height);
					this.mUV = NGUIMath.ConvertToTexCoords(this.mUV, texture.width, texture.height);
					this.mOffsetX = this.mSprite.paddingLeft;
					this.mOffsetY = this.mSprite.paddingTop;
					this.mWidth = this.mSprite.width;
					this.mHeight = this.mSprite.height;
					this.mAdvance = this.mSprite.width + (this.mSprite.paddingLeft + this.mSprite.paddingRight);
					this.mIsValid = true;
				}
			}
		}
		return this.mSprite != null;
	}

	// Token: 0x040004AA RID: 1194
	public string sequence;

	// Token: 0x040004AB RID: 1195
	public string spriteName;

	// Token: 0x040004AC RID: 1196
	private UISpriteData mSprite;

	// Token: 0x040004AD RID: 1197
	private bool mIsValid;

	// Token: 0x040004AE RID: 1198
	private int mLength;

	// Token: 0x040004AF RID: 1199
	private int mOffsetX;

	// Token: 0x040004B0 RID: 1200
	private int mOffsetY;

	// Token: 0x040004B1 RID: 1201
	private int mWidth;

	// Token: 0x040004B2 RID: 1202
	private int mHeight;

	// Token: 0x040004B3 RID: 1203
	private int mAdvance;

	// Token: 0x040004B4 RID: 1204
	private Rect mUV;
}
