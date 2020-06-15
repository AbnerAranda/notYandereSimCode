using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200009B RID: 155
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Unity2D Sprite")]
public class UI2DSprite : UIBasicSprite
{
	// Token: 0x1700011B RID: 283
	// (get) Token: 0x060006CC RID: 1740 RVA: 0x00037C6E File Offset: 0x00035E6E
	// (set) Token: 0x060006CD RID: 1741 RVA: 0x00037C76 File Offset: 0x00035E76
	public Sprite sprite2D
	{
		get
		{
			return this.mSprite;
		}
		set
		{
			if (this.mSprite != value)
			{
				base.RemoveFromPanel();
				this.mSprite = value;
				this.nextSprite = null;
				base.CreatePanel();
			}
		}
	}

	// Token: 0x1700011C RID: 284
	// (get) Token: 0x060006CE RID: 1742 RVA: 0x00032B23 File Offset: 0x00030D23
	// (set) Token: 0x060006CF RID: 1743 RVA: 0x00037CA1 File Offset: 0x00035EA1
	public override Material material
	{
		get
		{
			return this.mMat;
		}
		set
		{
			if (this.mMat != value)
			{
				base.RemoveFromPanel();
				this.mMat = value;
				this.mPMA = -1;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x1700011D RID: 285
	// (get) Token: 0x060006D0 RID: 1744 RVA: 0x00037CCB File Offset: 0x00035ECB
	// (set) Token: 0x060006D1 RID: 1745 RVA: 0x00037D0B File Offset: 0x00035F0B
	public override Shader shader
	{
		get
		{
			if (this.mMat != null)
			{
				return this.mMat.shader;
			}
			if (this.mShader == null)
			{
				this.mShader = Shader.Find("Unlit/Transparent Colored");
			}
			return this.mShader;
		}
		set
		{
			if (this.mShader != value)
			{
				base.RemoveFromPanel();
				this.mShader = value;
				if (this.mMat == null)
				{
					this.mPMA = -1;
					this.MarkAsChanged();
				}
			}
		}
	}

	// Token: 0x1700011E RID: 286
	// (get) Token: 0x060006D2 RID: 1746 RVA: 0x00037D43 File Offset: 0x00035F43
	public override Texture mainTexture
	{
		get
		{
			if (this.mSprite != null)
			{
				return this.mSprite.texture;
			}
			if (this.mMat != null)
			{
				return this.mMat.mainTexture;
			}
			return null;
		}
	}

	// Token: 0x1700011F RID: 287
	// (get) Token: 0x060006D3 RID: 1747 RVA: 0x00037D7A File Offset: 0x00035F7A
	// (set) Token: 0x060006D4 RID: 1748 RVA: 0x00037D82 File Offset: 0x00035F82
	public bool fixedAspect
	{
		get
		{
			return this.mFixedAspect;
		}
		set
		{
			if (this.mFixedAspect != value)
			{
				this.mFixedAspect = value;
				this.mDrawRegion = new Vector4(0f, 0f, 1f, 1f);
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000120 RID: 288
	// (get) Token: 0x060006D5 RID: 1749 RVA: 0x00037DBC File Offset: 0x00035FBC
	public override bool premultipliedAlpha
	{
		get
		{
			if (this.mPMA == -1)
			{
				Shader shader = this.shader;
				this.mPMA = ((shader != null && shader.name.Contains("Premultiplied")) ? 1 : 0);
			}
			return this.mPMA == 1;
		}
	}

	// Token: 0x17000121 RID: 289
	// (get) Token: 0x060006D6 RID: 1750 RVA: 0x00037E07 File Offset: 0x00036007
	public override float pixelSize
	{
		get
		{
			return this.mPixelSize;
		}
	}

	// Token: 0x17000122 RID: 290
	// (get) Token: 0x060006D7 RID: 1751 RVA: 0x00037E10 File Offset: 0x00036010
	public override Vector4 drawingDimensions
	{
		get
		{
			Vector2 pivotOffset = base.pivotOffset;
			float num = -pivotOffset.x * (float)this.mWidth;
			float num2 = -pivotOffset.y * (float)this.mHeight;
			float num3 = num + (float)this.mWidth;
			float num4 = num2 + (float)this.mHeight;
			if (this.mSprite != null && this.mType != UIBasicSprite.Type.Tiled)
			{
				int num5 = Mathf.RoundToInt(this.mSprite.rect.width);
				int num6 = Mathf.RoundToInt(this.mSprite.rect.height);
				int num7 = Mathf.RoundToInt(this.mSprite.textureRectOffset.x);
				int num8 = Mathf.RoundToInt(this.mSprite.textureRectOffset.y);
				int num9 = Mathf.RoundToInt(this.mSprite.rect.width - this.mSprite.textureRect.width - this.mSprite.textureRectOffset.x);
				int num10 = Mathf.RoundToInt(this.mSprite.rect.height - this.mSprite.textureRect.height - this.mSprite.textureRectOffset.y);
				float num11 = 1f;
				float num12 = 1f;
				if (num5 > 0 && num6 > 0 && (this.mType == UIBasicSprite.Type.Simple || this.mType == UIBasicSprite.Type.Filled))
				{
					if ((num5 & 1) != 0)
					{
						num9++;
					}
					if ((num6 & 1) != 0)
					{
						num10++;
					}
					num11 = 1f / (float)num5 * (float)this.mWidth;
					num12 = 1f / (float)num6 * (float)this.mHeight;
				}
				if (this.mFlip == UIBasicSprite.Flip.Horizontally || this.mFlip == UIBasicSprite.Flip.Both)
				{
					num += (float)num9 * num11;
					num3 -= (float)num7 * num11;
				}
				else
				{
					num += (float)num7 * num11;
					num3 -= (float)num9 * num11;
				}
				if (this.mFlip == UIBasicSprite.Flip.Vertically || this.mFlip == UIBasicSprite.Flip.Both)
				{
					num2 += (float)num10 * num12;
					num4 -= (float)num8 * num12;
				}
				else
				{
					num2 += (float)num8 * num12;
					num4 -= (float)num10 * num12;
				}
			}
			float num13;
			float num14;
			if (this.mFixedAspect)
			{
				num13 = 0f;
				num14 = 0f;
			}
			else
			{
				Vector4 vector = this.border * this.pixelSize;
				num13 = vector.x + vector.z;
				num14 = vector.y + vector.w;
			}
			float x = Mathf.Lerp(num, num3 - num13, this.mDrawRegion.x);
			float y = Mathf.Lerp(num2, num4 - num14, this.mDrawRegion.y);
			float z = Mathf.Lerp(num + num13, num3, this.mDrawRegion.z);
			float w = Mathf.Lerp(num2 + num14, num4, this.mDrawRegion.w);
			return new Vector4(x, y, z, w);
		}
	}

	// Token: 0x17000123 RID: 291
	// (get) Token: 0x060006D8 RID: 1752 RVA: 0x000380E2 File Offset: 0x000362E2
	// (set) Token: 0x060006D9 RID: 1753 RVA: 0x000380EA File Offset: 0x000362EA
	public override Vector4 border
	{
		get
		{
			return this.mBorder;
		}
		set
		{
			if (this.mBorder != value)
			{
				this.mBorder = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x060006DA RID: 1754 RVA: 0x00038108 File Offset: 0x00036308
	protected override void OnUpdate()
	{
		if (this.nextSprite != null)
		{
			if (this.nextSprite != this.mSprite)
			{
				this.sprite2D = this.nextSprite;
			}
			this.nextSprite = null;
		}
		base.OnUpdate();
		if (this.mFixedAspect && this.mainTexture != null)
		{
			float num = (float)Mathf.RoundToInt(this.mSprite.rect.width);
			int num2 = Mathf.RoundToInt(this.mSprite.rect.height);
			int num3 = Mathf.RoundToInt(this.mSprite.textureRectOffset.x);
			int num4 = Mathf.RoundToInt(this.mSprite.textureRectOffset.y);
			int num5 = Mathf.RoundToInt(this.mSprite.rect.width - this.mSprite.textureRect.width - this.mSprite.textureRectOffset.x);
			int num6 = Mathf.RoundToInt(this.mSprite.rect.height - this.mSprite.textureRect.height - this.mSprite.textureRectOffset.y);
			float num7 = num + (float)(num3 + num5);
			num2 += num6 + num4;
			float num8 = (float)this.mWidth;
			float num9 = (float)this.mHeight;
			float num10 = num8 / num9;
			float num11 = num7 / (float)num2;
			if (num11 < num10)
			{
				float num12 = (num8 - num9 * num11) / num8 * 0.5f;
				base.drawRegion = new Vector4(num12, 0f, 1f - num12, 1f);
				return;
			}
			float num13 = (num9 - num8 / num11) / num9 * 0.5f;
			base.drawRegion = new Vector4(0f, num13, 1f, 1f - num13);
		}
	}

	// Token: 0x060006DB RID: 1755 RVA: 0x000382E0 File Offset: 0x000364E0
	public override void MakePixelPerfect()
	{
		base.MakePixelPerfect();
		if (this.mType == UIBasicSprite.Type.Tiled)
		{
			return;
		}
		Texture mainTexture = this.mainTexture;
		if (mainTexture == null)
		{
			return;
		}
		if ((this.mType == UIBasicSprite.Type.Simple || this.mType == UIBasicSprite.Type.Filled || !base.hasBorder) && mainTexture != null)
		{
			Rect rect = this.mSprite.rect;
			int num = Mathf.RoundToInt(this.pixelSize * rect.width);
			int num2 = Mathf.RoundToInt(this.pixelSize * rect.height);
			if ((num & 1) == 1)
			{
				num++;
			}
			if ((num2 & 1) == 1)
			{
				num2++;
			}
			base.width = num;
			base.height = num2;
		}
	}

	// Token: 0x060006DC RID: 1756 RVA: 0x00038388 File Offset: 0x00036588
	public override void OnFill(List<Vector3> verts, List<Vector2> uvs, List<Color> cols)
	{
		Texture mainTexture = this.mainTexture;
		if (mainTexture == null)
		{
			return;
		}
		Rect rect = (this.mSprite != null) ? this.mSprite.textureRect : new Rect(0f, 0f, (float)mainTexture.width, (float)mainTexture.height);
		Rect inner = rect;
		Vector4 border = this.border;
		inner.xMin += border.x;
		inner.yMin += border.y;
		inner.xMax -= border.z;
		inner.yMax -= border.w;
		float num = 1f / (float)mainTexture.width;
		float num2 = 1f / (float)mainTexture.height;
		rect.xMin *= num;
		rect.xMax *= num;
		rect.yMin *= num2;
		rect.yMax *= num2;
		inner.xMin *= num;
		inner.xMax *= num;
		inner.yMin *= num2;
		inner.yMax *= num2;
		int count = verts.Count;
		base.Fill(verts, uvs, cols, rect, inner);
		if (this.onPostFill != null)
		{
			this.onPostFill(this, count, verts, uvs, cols);
		}
	}

	// Token: 0x0400062A RID: 1578
	[HideInInspector]
	[SerializeField]
	private Sprite mSprite;

	// Token: 0x0400062B RID: 1579
	[HideInInspector]
	[SerializeField]
	private Shader mShader;

	// Token: 0x0400062C RID: 1580
	[HideInInspector]
	[SerializeField]
	private Vector4 mBorder = Vector4.zero;

	// Token: 0x0400062D RID: 1581
	[HideInInspector]
	[SerializeField]
	private bool mFixedAspect;

	// Token: 0x0400062E RID: 1582
	[HideInInspector]
	[SerializeField]
	private float mPixelSize = 1f;

	// Token: 0x0400062F RID: 1583
	public Sprite nextSprite;

	// Token: 0x04000630 RID: 1584
	[NonSerialized]
	private int mPMA = -1;
}
