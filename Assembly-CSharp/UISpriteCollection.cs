using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000AB RID: 171
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Sprite Collection")]
public class UISpriteCollection : UIBasicSprite
{
	// Token: 0x170001D0 RID: 464
	// (get) Token: 0x060008D2 RID: 2258 RVA: 0x0004654C File Offset: 0x0004474C
	// (set) Token: 0x060008D3 RID: 2259 RVA: 0x0004056D File Offset: 0x0003E76D
	public override Texture mainTexture
	{
		get
		{
			Material material = null;
			INGUIAtlas atlas = this.atlas;
			if (atlas != null)
			{
				material = atlas.spriteMaterial;
			}
			if (!(material != null))
			{
				return null;
			}
			return material.mainTexture;
		}
		set
		{
			base.mainTexture = value;
		}
	}

	// Token: 0x170001D1 RID: 465
	// (get) Token: 0x060008D4 RID: 2260 RVA: 0x00046580 File Offset: 0x00044780
	// (set) Token: 0x060008D5 RID: 2261 RVA: 0x0004051A File Offset: 0x0003E71A
	public override Material material
	{
		get
		{
			Material material = base.material;
			if (material != null)
			{
				return material;
			}
			INGUIAtlas atlas = this.atlas;
			if (atlas != null)
			{
				return atlas.spriteMaterial;
			}
			return null;
		}
		set
		{
			base.material = value;
		}
	}

	// Token: 0x170001D2 RID: 466
	// (get) Token: 0x060008D6 RID: 2262 RVA: 0x000465B1 File Offset: 0x000447B1
	// (set) Token: 0x060008D7 RID: 2263 RVA: 0x000465BE File Offset: 0x000447BE
	public INGUIAtlas atlas
	{
		get
		{
			return this.mAtlas as INGUIAtlas;
		}
		set
		{
			if (this.mAtlas as INGUIAtlas != value)
			{
				base.RemoveFromPanel();
				this.mAtlas = (value as UnityEngine.Object);
				this.mSprites.Clear();
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x170001D3 RID: 467
	// (get) Token: 0x060008D8 RID: 2264 RVA: 0x000465F4 File Offset: 0x000447F4
	public override float pixelSize
	{
		get
		{
			INGUIAtlas atlas = this.atlas;
			if (atlas != null)
			{
				return atlas.pixelSize;
			}
			return 1f;
		}
	}

	// Token: 0x170001D4 RID: 468
	// (get) Token: 0x060008D9 RID: 2265 RVA: 0x00046618 File Offset: 0x00044818
	public override bool premultipliedAlpha
	{
		get
		{
			INGUIAtlas atlas = this.atlas;
			return atlas != null && atlas.premultipliedAlpha;
		}
	}

	// Token: 0x170001D5 RID: 469
	// (get) Token: 0x060008DA RID: 2266 RVA: 0x00046638 File Offset: 0x00044838
	public override Vector4 border
	{
		get
		{
			if (this.mSprite == null)
			{
				return base.border;
			}
			return new Vector4((float)this.mSprite.borderLeft, (float)this.mSprite.borderBottom, (float)this.mSprite.borderRight, (float)this.mSprite.borderTop);
		}
	}

	// Token: 0x170001D6 RID: 470
	// (get) Token: 0x060008DB RID: 2267 RVA: 0x0004668C File Offset: 0x0004488C
	protected override Vector4 padding
	{
		get
		{
			Vector4 result = new Vector4(0f, 0f, 0f, 0f);
			if (this.mSprite != null)
			{
				result.x = (float)this.mSprite.paddingLeft;
				result.y = (float)this.mSprite.paddingBottom;
				result.z = (float)this.mSprite.paddingRight;
				result.w = (float)this.mSprite.paddingTop;
			}
			return result;
		}
	}

	// Token: 0x060008DC RID: 2268 RVA: 0x0004670C File Offset: 0x0004490C
	public override void OnFill(List<Vector3> verts, List<Vector2> uvs, List<Color> cols)
	{
		Texture mainTexture = this.mainTexture;
		if (mainTexture == null)
		{
			return;
		}
		int count = verts.Count;
		Vector4 drawRegion = base.drawRegion;
		foreach (KeyValuePair<object, UISpriteCollection.Sprite> keyValuePair in this.mSprites)
		{
			UISpriteCollection.Sprite value = keyValuePair.Value;
			if (value.enabled)
			{
				this.mSprite = value.sprite;
				if (this.mSprite != null)
				{
					Color color = value.color;
					color.a = this.finalAlpha;
					if (color.a != 0f)
					{
						Rect rect = new Rect((float)this.mSprite.x, (float)this.mSprite.y, (float)this.mSprite.width, (float)this.mSprite.height);
						Rect rect2 = new Rect((float)(this.mSprite.x + this.mSprite.borderLeft), (float)(this.mSprite.y + this.mSprite.borderTop), (float)(this.mSprite.width - this.mSprite.borderLeft - this.mSprite.borderRight), (float)(this.mSprite.height - this.mSprite.borderBottom - this.mSprite.borderTop));
						this.mOuterUV = NGUIMath.ConvertToTexCoords(rect, mainTexture.width, mainTexture.height);
						this.mInnerUV = NGUIMath.ConvertToTexCoords(rect2, mainTexture.width, mainTexture.height);
						this.mFlip = value.flip;
						Vector4 drawingDimensions = value.GetDrawingDimensions(this.pixelSize);
						Vector4 drawingUVs = base.drawingUVs;
						if (this.premultipliedAlpha)
						{
							color = NGUITools.ApplyPMA(color);
						}
						int count2 = verts.Count;
						switch (value.type)
						{
						case UIBasicSprite.Type.Simple:
							base.SimpleFill(verts, uvs, cols, ref drawingDimensions, ref drawingUVs, ref color);
							break;
						case UIBasicSprite.Type.Sliced:
							base.SlicedFill(verts, uvs, cols, ref drawingDimensions, ref drawingUVs, ref color);
							break;
						case UIBasicSprite.Type.Tiled:
							base.TiledFill(verts, uvs, cols, ref drawingDimensions, ref color);
							break;
						case UIBasicSprite.Type.Filled:
							base.FilledFill(verts, uvs, cols, ref drawingDimensions, ref drawingUVs, ref color);
							break;
						case UIBasicSprite.Type.Advanced:
							base.AdvancedFill(verts, uvs, cols, ref drawingDimensions, ref drawingUVs, ref color);
							break;
						}
						if (value.rot != 0f)
						{
							float f = value.rot * 0.0174532924f * 0.5f;
							float num = Mathf.Sin(f);
							float num2 = Mathf.Cos(f);
							float num3 = num * 2f;
							float num4 = num * num3;
							float num5 = num2 * num3;
							int i = count2;
							int count3 = verts.Count;
							while (i < count3)
							{
								Vector3 vector = verts[i];
								vector = new Vector3((1f - num4) * vector.x - num5 * vector.y, num5 * vector.x + (1f - num4) * vector.y, vector.z);
								vector.x += value.pos.x;
								vector.y += value.pos.y;
								verts[i] = vector;
								i++;
							}
						}
						else
						{
							int j = count2;
							int count4 = verts.Count;
							while (j < count4)
							{
								Vector3 value2 = verts[j];
								value2.x += value.pos.x;
								value2.y += value.pos.y;
								verts[j] = value2;
								j++;
							}
						}
					}
				}
			}
		}
		this.mSprite = null;
		if (this.onPostFill != null)
		{
			this.onPostFill(this, count, verts, uvs, cols);
		}
	}

	// Token: 0x060008DD RID: 2269 RVA: 0x00046AE8 File Offset: 0x00044CE8
	public void Add(object obj, string spriteName, Vector2 pos, float width, float height)
	{
		this.AddSprite(obj, spriteName, pos, width, height, new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue), new Vector2(0.5f, 0.5f), 0f, UIBasicSprite.Type.Simple, UIBasicSprite.Flip.Nothing, true);
	}

	// Token: 0x060008DE RID: 2270 RVA: 0x00046B34 File Offset: 0x00044D34
	public void Add(object obj, string spriteName, Vector2 pos, float width, float height, Color32 color)
	{
		this.AddSprite(obj, spriteName, pos, width, height, color, new Vector2(0.5f, 0.5f), 0f, UIBasicSprite.Type.Simple, UIBasicSprite.Flip.Nothing, true);
	}

	// Token: 0x060008DF RID: 2271 RVA: 0x00046B68 File Offset: 0x00044D68
	public void AddSprite(object id, string spriteName, Vector2 pos, float width, float height, Color32 color, Vector2 pivot, float rot = 0f, UIBasicSprite.Type type = UIBasicSprite.Type.Simple, UIBasicSprite.Flip flip = UIBasicSprite.Flip.Nothing, bool enabled = true)
	{
		if (this.mAtlas == null)
		{
			Debug.LogError("Atlas must be assigned first");
			return;
		}
		UISpriteCollection.Sprite sprite = default(UISpriteCollection.Sprite);
		INGUIAtlas atlas = this.atlas;
		if (atlas != null)
		{
			sprite.sprite = atlas.GetSprite(spriteName);
		}
		if (sprite.sprite == null)
		{
			return;
		}
		sprite.pos = pos;
		sprite.rot = rot;
		sprite.width = width;
		sprite.height = height;
		sprite.color = color;
		sprite.pivot = pivot;
		sprite.type = type;
		sprite.flip = flip;
		sprite.enabled = enabled;
		this.mSprites[id] = sprite;
		if (enabled && !this.mChanged)
		{
			this.MarkAsChanged();
		}
	}

	// Token: 0x060008E0 RID: 2272 RVA: 0x00046C28 File Offset: 0x00044E28
	public UISpriteCollection.Sprite? GetSprite(object id)
	{
		UISpriteCollection.Sprite value;
		if (this.mSprites.TryGetValue(id, out value))
		{
			return new UISpriteCollection.Sprite?(value);
		}
		return null;
	}

	// Token: 0x060008E1 RID: 2273 RVA: 0x00046C55 File Offset: 0x00044E55
	public bool RemoveSprite(object id)
	{
		if (this.mSprites.Remove(id))
		{
			if (!this.mChanged)
			{
				this.MarkAsChanged();
			}
			return true;
		}
		return false;
	}

	// Token: 0x060008E2 RID: 2274 RVA: 0x00046C76 File Offset: 0x00044E76
	public bool SetSprite(object id, UISpriteCollection.Sprite sp)
	{
		this.mSprites[id] = sp;
		if (!this.mChanged)
		{
			this.MarkAsChanged();
		}
		return true;
	}

	// Token: 0x060008E3 RID: 2275 RVA: 0x00046C94 File Offset: 0x00044E94
	[ContextMenu("Clear")]
	public void Clear()
	{
		if (this.mSprites.Count != 0)
		{
			this.mSprites.Clear();
			this.MarkAsChanged();
		}
	}

	// Token: 0x060008E4 RID: 2276 RVA: 0x00046CB4 File Offset: 0x00044EB4
	public bool IsActive(object id)
	{
		UISpriteCollection.Sprite sprite;
		return this.mSprites.TryGetValue(id, out sprite) && sprite.enabled;
	}

	// Token: 0x060008E5 RID: 2277 RVA: 0x00046CDC File Offset: 0x00044EDC
	public bool SetActive(object id, bool visible)
	{
		UISpriteCollection.Sprite sprite;
		if (this.mSprites.TryGetValue(id, out sprite))
		{
			if (sprite.enabled != visible)
			{
				sprite.enabled = visible;
				this.mSprites[id] = sprite;
				if (!this.mChanged)
				{
					this.MarkAsChanged();
				}
			}
			return true;
		}
		return false;
	}

	// Token: 0x060008E6 RID: 2278 RVA: 0x00046D28 File Offset: 0x00044F28
	public bool SetPosition(object id, Vector2 pos, bool visible = true)
	{
		UISpriteCollection.Sprite sprite;
		if (this.mSprites.TryGetValue(id, out sprite))
		{
			if (sprite.pos != pos)
			{
				sprite.pos = pos;
				sprite.enabled = visible;
				this.mSprites[id] = sprite;
				if (!this.mChanged)
				{
					this.MarkAsChanged();
				}
			}
			else if (sprite.enabled != visible)
			{
				sprite.enabled = visible;
				this.mSprites[id] = sprite;
				if (!this.mChanged)
				{
					this.MarkAsChanged();
				}
			}
			return true;
		}
		return false;
	}

	// Token: 0x060008E7 RID: 2279 RVA: 0x00046DB0 File Offset: 0x00044FB0
	private static Vector2 Rotate(Vector2 pos, float rot)
	{
		float f = rot * 0.0174532924f * 0.5f;
		float num = Mathf.Sin(f);
		float num2 = Mathf.Cos(f);
		float num3 = num * 2f;
		float num4 = num * num3;
		float num5 = num2 * num3;
		return new Vector2((1f - num4) * pos.x - num5 * pos.y, num5 * pos.x + (1f - num4) * pos.y);
	}

	// Token: 0x060008E8 RID: 2280 RVA: 0x00046E18 File Offset: 0x00045018
	public object GetCurrentSpriteID()
	{
		return this.GetCurrentSpriteID(UICamera.lastWorldPosition);
	}

	// Token: 0x060008E9 RID: 2281 RVA: 0x00046E25 File Offset: 0x00045025
	public UISpriteCollection.Sprite? GetCurrentSprite()
	{
		return this.GetCurrentSprite(UICamera.lastWorldPosition);
	}

	// Token: 0x060008EA RID: 2282 RVA: 0x00046E34 File Offset: 0x00045034
	public object GetCurrentSpriteID(Vector3 worldPos)
	{
		Vector2 a = this.mTrans.InverseTransformPoint(worldPos);
		foreach (KeyValuePair<object, UISpriteCollection.Sprite> keyValuePair in this.mSprites)
		{
			UISpriteCollection.Sprite value = keyValuePair.Value;
			Vector2 vector = a - value.pos;
			if (value.rot != 0f)
			{
				vector = UISpriteCollection.Rotate(vector, -value.rot);
			}
			Vector4 drawingDimensions = value.GetDrawingDimensions(this.pixelSize);
			if (vector.x >= drawingDimensions.x && vector.y >= drawingDimensions.y && vector.x <= drawingDimensions.z && vector.y <= drawingDimensions.w)
			{
				return keyValuePair.Key;
			}
		}
		return null;
	}

	// Token: 0x060008EB RID: 2283 RVA: 0x00046F2C File Offset: 0x0004512C
	public UISpriteCollection.Sprite? GetCurrentSprite(Vector3 worldPos)
	{
		Vector2 a = this.mTrans.InverseTransformPoint(worldPos);
		foreach (KeyValuePair<object, UISpriteCollection.Sprite> keyValuePair in this.mSprites)
		{
			UISpriteCollection.Sprite value = keyValuePair.Value;
			Vector2 vector = a - value.pos;
			if (value.rot != 0f)
			{
				vector = UISpriteCollection.Rotate(vector, -value.rot);
			}
			Vector4 drawingDimensions = value.GetDrawingDimensions(this.pixelSize);
			if (vector.x >= drawingDimensions.x && vector.y >= drawingDimensions.y && vector.x <= drawingDimensions.z && vector.y <= drawingDimensions.w)
			{
				return new UISpriteCollection.Sprite?(keyValuePair.Value);
			}
		}
		return null;
	}

	// Token: 0x060008EC RID: 2284 RVA: 0x00047030 File Offset: 0x00045230
	protected void OnClick()
	{
		if (this.onClick != null)
		{
			object currentSpriteID = this.GetCurrentSpriteID();
			if (currentSpriteID != null)
			{
				this.onClick(currentSpriteID);
			}
		}
	}

	// Token: 0x060008ED RID: 2285 RVA: 0x0004705C File Offset: 0x0004525C
	protected void OnPress(bool isPressed)
	{
		if (this.onPress != null)
		{
			if (isPressed && this.mLastPress != null)
			{
				return;
			}
			if (isPressed)
			{
				this.mLastPress = this.GetCurrentSpriteID();
				if (this.mLastPress != null)
				{
					this.onPress(this.mLastPress, true);
					return;
				}
			}
			else if (this.mLastPress != null)
			{
				this.onPress(this.mLastPress, false);
				this.mLastPress = null;
			}
		}
	}

	// Token: 0x060008EE RID: 2286 RVA: 0x000470C8 File Offset: 0x000452C8
	protected void OnHover(bool isOver)
	{
		if (this.onHover != null)
		{
			if (isOver)
			{
				UICamera.onMouseMove = (UICamera.MoveDelegate)Delegate.Combine(UICamera.onMouseMove, new UICamera.MoveDelegate(this.OnMove));
				this.OnMove(Vector2.zero);
				return;
			}
			UICamera.onMouseMove = (UICamera.MoveDelegate)Delegate.Remove(UICamera.onMouseMove, new UICamera.MoveDelegate(this.OnMove));
		}
	}

	// Token: 0x060008EF RID: 2287 RVA: 0x0004712C File Offset: 0x0004532C
	protected void OnMove(Vector2 delta)
	{
		if (!this || this.onHover == null)
		{
			return;
		}
		object currentSpriteID = this.GetCurrentSpriteID();
		if (this.mLastHover != currentSpriteID)
		{
			if (this.mLastHover != null)
			{
				this.onHover(this.mLastHover, false);
			}
			this.mLastHover = currentSpriteID;
			if (this.mLastHover != null)
			{
				this.onHover(this.mLastHover, true);
			}
		}
	}

	// Token: 0x060008F0 RID: 2288 RVA: 0x00047195 File Offset: 0x00045395
	protected void OnDrag(Vector2 delta)
	{
		if (this.onDrag != null && this.mLastPress != null)
		{
			this.onDrag(this.mLastPress, delta);
		}
	}

	// Token: 0x060008F1 RID: 2289 RVA: 0x000471BC File Offset: 0x000453BC
	protected void OnTooltip(bool show)
	{
		if (this.onTooltip != null)
		{
			if (show)
			{
				if (this.mLastTooltip != null)
				{
					this.onTooltip(this.mLastTooltip, false);
				}
				this.mLastTooltip = this.GetCurrentSpriteID();
				if (this.mLastTooltip != null)
				{
					this.onTooltip(this.mLastTooltip, true);
					return;
				}
			}
			else
			{
				this.onTooltip(this.mLastTooltip, false);
				this.mLastTooltip = null;
			}
		}
	}

	// Token: 0x0400077B RID: 1915
	[HideInInspector]
	[SerializeField]
	private UnityEngine.Object mAtlas;

	// Token: 0x0400077C RID: 1916
	[NonSerialized]
	private Dictionary<object, UISpriteCollection.Sprite> mSprites = new Dictionary<object, UISpriteCollection.Sprite>();

	// Token: 0x0400077D RID: 1917
	[NonSerialized]
	private UISpriteData mSprite;

	// Token: 0x0400077E RID: 1918
	public UISpriteCollection.OnHoverCB onHover;

	// Token: 0x0400077F RID: 1919
	public UISpriteCollection.OnPressCB onPress;

	// Token: 0x04000780 RID: 1920
	public UISpriteCollection.OnClickCB onClick;

	// Token: 0x04000781 RID: 1921
	public UISpriteCollection.OnDragCB onDrag;

	// Token: 0x04000782 RID: 1922
	public UISpriteCollection.OnTooltipCB onTooltip;

	// Token: 0x04000783 RID: 1923
	[NonSerialized]
	private object mLastHover;

	// Token: 0x04000784 RID: 1924
	[NonSerialized]
	private object mLastPress;

	// Token: 0x04000785 RID: 1925
	[NonSerialized]
	private object mLastTooltip;

	// Token: 0x0200069E RID: 1694
	public struct Sprite
	{
		// Token: 0x06002BAF RID: 11183 RVA: 0x001CA284 File Offset: 0x001C8484
		public Vector4 GetDrawingDimensions(float pixelSize)
		{
			float num = -this.pivot.x * this.width;
			float num2 = -this.pivot.y * this.height;
			float num3 = num + this.width;
			float num4 = num2 + this.height;
			if (this.sprite != null && this.type != UIBasicSprite.Type.Tiled)
			{
				int num5 = this.sprite.paddingLeft;
				int num6 = this.sprite.paddingBottom;
				int num7 = this.sprite.paddingRight;
				int num8 = this.sprite.paddingTop;
				if (this.type != UIBasicSprite.Type.Simple && pixelSize != 1f)
				{
					num5 = Mathf.RoundToInt(pixelSize * (float)num5);
					num6 = Mathf.RoundToInt(pixelSize * (float)num6);
					num7 = Mathf.RoundToInt(pixelSize * (float)num7);
					num8 = Mathf.RoundToInt(pixelSize * (float)num8);
				}
				int num9 = this.sprite.width + num5 + num7;
				int num10 = this.sprite.height + num6 + num8;
				float num11 = 1f;
				float num12 = 1f;
				if (num9 > 0 && num10 > 0 && (this.type == UIBasicSprite.Type.Simple || this.type == UIBasicSprite.Type.Filled))
				{
					if ((num9 & 1) != 0)
					{
						num7++;
					}
					if ((num10 & 1) != 0)
					{
						num8++;
					}
					num11 = 1f / (float)num9 * this.width;
					num12 = 1f / (float)num10 * this.height;
				}
				if (this.flip == UIBasicSprite.Flip.Horizontally || this.flip == UIBasicSprite.Flip.Both)
				{
					num += (float)num7 * num11;
					num3 -= (float)num5 * num11;
				}
				else
				{
					num += (float)num5 * num11;
					num3 -= (float)num7 * num11;
				}
				if (this.flip == UIBasicSprite.Flip.Vertically || this.flip == UIBasicSprite.Flip.Both)
				{
					num2 += (float)num8 * num12;
					num4 -= (float)num6 * num12;
				}
				else
				{
					num2 += (float)num6 * num12;
					num4 -= (float)num8 * num12;
				}
			}
			return new Vector4(num, num2, num3, num4);
		}

		// Token: 0x040046C2 RID: 18114
		public UISpriteData sprite;

		// Token: 0x040046C3 RID: 18115
		public Vector2 pos;

		// Token: 0x040046C4 RID: 18116
		public float rot;

		// Token: 0x040046C5 RID: 18117
		public float width;

		// Token: 0x040046C6 RID: 18118
		public float height;

		// Token: 0x040046C7 RID: 18119
		public Color32 color;

		// Token: 0x040046C8 RID: 18120
		public Vector2 pivot;

		// Token: 0x040046C9 RID: 18121
		public UIBasicSprite.Type type;

		// Token: 0x040046CA RID: 18122
		public UIBasicSprite.Flip flip;

		// Token: 0x040046CB RID: 18123
		public bool enabled;
	}

	// Token: 0x0200069F RID: 1695
	// (Invoke) Token: 0x06002BB1 RID: 11185
	public delegate void OnHoverCB(object obj, bool isOver);

	// Token: 0x020006A0 RID: 1696
	// (Invoke) Token: 0x06002BB5 RID: 11189
	public delegate void OnPressCB(object obj, bool isPressed);

	// Token: 0x020006A1 RID: 1697
	// (Invoke) Token: 0x06002BB9 RID: 11193
	public delegate void OnClickCB(object obj);

	// Token: 0x020006A2 RID: 1698
	// (Invoke) Token: 0x06002BBD RID: 11197
	public delegate void OnDragCB(object obj, Vector2 delta);

	// Token: 0x020006A3 RID: 1699
	// (Invoke) Token: 0x06002BC1 RID: 11201
	public delegate void OnTooltipCB(object obj, bool show);
}
