using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x020000AE RID: 174
[AddComponentMenu("NGUI/UI/Text List")]
public class UITextList : MonoBehaviour
{
	// Token: 0x170001D9 RID: 473
	// (get) Token: 0x06000901 RID: 2305 RVA: 0x00047C48 File Offset: 0x00045E48
	protected BetterList<UITextList.Paragraph> paragraphs
	{
		get
		{
			if (this.mParagraphs == null && !UITextList.mHistory.TryGetValue(base.name, out this.mParagraphs))
			{
				this.mParagraphs = new BetterList<UITextList.Paragraph>();
				UITextList.mHistory.Add(base.name, this.mParagraphs);
			}
			return this.mParagraphs;
		}
	}

	// Token: 0x170001DA RID: 474
	// (get) Token: 0x06000902 RID: 2306 RVA: 0x00047C9C File Offset: 0x00045E9C
	public int paragraphCount
	{
		get
		{
			return this.paragraphs.size;
		}
	}

	// Token: 0x170001DB RID: 475
	// (get) Token: 0x06000903 RID: 2307 RVA: 0x00047CA9 File Offset: 0x00045EA9
	public bool isValid
	{
		get
		{
			return this.textLabel != null && this.textLabel.ambigiousFont != null;
		}
	}

	// Token: 0x170001DC RID: 476
	// (get) Token: 0x06000904 RID: 2308 RVA: 0x00047CCC File Offset: 0x00045ECC
	// (set) Token: 0x06000905 RID: 2309 RVA: 0x00047CD4 File Offset: 0x00045ED4
	public float scrollValue
	{
		get
		{
			return this.mScroll;
		}
		set
		{
			value = Mathf.Clamp01(value);
			if (this.isValid && this.mScroll != value)
			{
				if (this.scrollBar != null)
				{
					this.scrollBar.value = value;
					return;
				}
				this.mScroll = value;
				this.UpdateVisibleText();
			}
		}
	}

	// Token: 0x170001DD RID: 477
	// (get) Token: 0x06000906 RID: 2310 RVA: 0x00047D22 File Offset: 0x00045F22
	protected float lineHeight
	{
		get
		{
			if (!(this.textLabel != null))
			{
				return 20f;
			}
			return (float)this.textLabel.fontSize + this.textLabel.effectiveSpacingY;
		}
	}

	// Token: 0x170001DE RID: 478
	// (get) Token: 0x06000907 RID: 2311 RVA: 0x00047D50 File Offset: 0x00045F50
	protected int scrollHeight
	{
		get
		{
			if (!this.isValid)
			{
				return 0;
			}
			int num = Mathf.FloorToInt((float)this.textLabel.height / this.lineHeight);
			return Mathf.Max(0, this.mTotalLines - num);
		}
	}

	// Token: 0x06000908 RID: 2312 RVA: 0x00047D8E File Offset: 0x00045F8E
	public void Clear()
	{
		this.paragraphs.Clear();
		this.UpdateVisibleText();
	}

	// Token: 0x06000909 RID: 2313 RVA: 0x00047DA4 File Offset: 0x00045FA4
	private void Start()
	{
		if (this.textLabel == null)
		{
			this.textLabel = base.GetComponentInChildren<UILabel>();
		}
		if (this.scrollBar != null)
		{
			EventDelegate.Add(this.scrollBar.onChange, new EventDelegate.Callback(this.OnScrollBar));
		}
		this.textLabel.overflowMethod = UILabel.Overflow.ClampContent;
		if (this.style == UITextList.Style.Chat)
		{
			this.textLabel.pivot = UIWidget.Pivot.BottomLeft;
			this.scrollValue = 1f;
			return;
		}
		this.textLabel.pivot = UIWidget.Pivot.TopLeft;
		this.scrollValue = 0f;
	}

	// Token: 0x0600090A RID: 2314 RVA: 0x00047E3A File Offset: 0x0004603A
	private void Update()
	{
		if (this.isValid && (this.textLabel.width != this.mLastWidth || this.textLabel.height != this.mLastHeight))
		{
			this.Rebuild();
		}
	}

	// Token: 0x0600090B RID: 2315 RVA: 0x00047E70 File Offset: 0x00046070
	public void OnScroll(float val)
	{
		int scrollHeight = this.scrollHeight;
		if (scrollHeight != 0)
		{
			val *= this.lineHeight;
			this.scrollValue = this.mScroll - val / (float)scrollHeight;
		}
	}

	// Token: 0x0600090C RID: 2316 RVA: 0x00047EA4 File Offset: 0x000460A4
	public void OnDrag(Vector2 delta)
	{
		int scrollHeight = this.scrollHeight;
		if (scrollHeight != 0)
		{
			float num = delta.y / this.lineHeight;
			this.scrollValue = this.mScroll + num / (float)scrollHeight;
		}
	}

	// Token: 0x0600090D RID: 2317 RVA: 0x00047EDA File Offset: 0x000460DA
	private void OnScrollBar()
	{
		this.mScroll = UIProgressBar.current.value;
		this.UpdateVisibleText();
	}

	// Token: 0x0600090E RID: 2318 RVA: 0x00047EF2 File Offset: 0x000460F2
	public void Add(string text)
	{
		this.Add(text, true);
	}

	// Token: 0x0600090F RID: 2319 RVA: 0x00047EFC File Offset: 0x000460FC
	protected void Add(string text, bool updateVisible)
	{
		UITextList.Paragraph paragraph;
		if (this.paragraphs.size < this.paragraphHistory)
		{
			paragraph = new UITextList.Paragraph();
		}
		else
		{
			paragraph = this.mParagraphs.buffer[0];
			this.mParagraphs.RemoveAt(0);
		}
		paragraph.text = text;
		this.mParagraphs.Add(paragraph);
		this.Rebuild();
	}

	// Token: 0x06000910 RID: 2320 RVA: 0x00047F5C File Offset: 0x0004615C
	protected void Rebuild()
	{
		if (this.isValid)
		{
			this.mLastWidth = this.textLabel.width;
			this.mLastHeight = this.textLabel.height;
			this.textLabel.UpdateNGUIText();
			NGUIText.rectHeight = 1000000;
			NGUIText.regionHeight = 1000000;
			this.mTotalLines = 0;
			for (int i = 0; i < this.paragraphs.size; i++)
			{
				UITextList.Paragraph paragraph = this.mParagraphs.buffer[i];
				string text;
				NGUIText.WrapText(paragraph.text, out text, false, true, false);
				paragraph.lines = text.Split(new char[]
				{
					'\n'
				});
				this.mTotalLines += paragraph.lines.Length;
			}
			this.mTotalLines = 0;
			int j = 0;
			int size = this.mParagraphs.size;
			while (j < size)
			{
				this.mTotalLines += this.mParagraphs.buffer[j].lines.Length;
				j++;
			}
			if (this.scrollBar != null)
			{
				UIScrollBar uiscrollBar = this.scrollBar as UIScrollBar;
				if (uiscrollBar != null)
				{
					uiscrollBar.barSize = ((this.mTotalLines == 0) ? 1f : (1f - (float)this.scrollHeight / (float)this.mTotalLines));
				}
			}
			this.UpdateVisibleText();
		}
	}

	// Token: 0x06000911 RID: 2321 RVA: 0x000480B8 File Offset: 0x000462B8
	protected void UpdateVisibleText()
	{
		if (this.isValid)
		{
			if (this.mTotalLines == 0)
			{
				this.textLabel.text = "";
				return;
			}
			int num = Mathf.FloorToInt((float)this.textLabel.height / this.lineHeight);
			int num2 = Mathf.Max(0, this.mTotalLines - num);
			int num3 = Mathf.RoundToInt(this.mScroll * (float)num2);
			if (num3 < 0)
			{
				num3 = 0;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int num4 = 0;
			int size = this.paragraphs.size;
			while (num > 0 && num4 < size)
			{
				UITextList.Paragraph paragraph = this.mParagraphs.buffer[num4];
				int num5 = 0;
				int num6 = paragraph.lines.Length;
				while (num > 0 && num5 < num6)
				{
					string value = paragraph.lines[num5];
					if (num3 > 0)
					{
						num3--;
					}
					else
					{
						if (stringBuilder.Length > 0)
						{
							stringBuilder.Append("\n");
						}
						stringBuilder.Append(value);
						num--;
					}
					num5++;
				}
				num4++;
			}
			this.textLabel.text = stringBuilder.ToString();
		}
	}

	// Token: 0x040007A3 RID: 1955
	public UILabel textLabel;

	// Token: 0x040007A4 RID: 1956
	public UIProgressBar scrollBar;

	// Token: 0x040007A5 RID: 1957
	public UITextList.Style style;

	// Token: 0x040007A6 RID: 1958
	public int paragraphHistory = 100;

	// Token: 0x040007A7 RID: 1959
	protected char[] mSeparator = new char[]
	{
		'\n'
	};

	// Token: 0x040007A8 RID: 1960
	protected float mScroll;

	// Token: 0x040007A9 RID: 1961
	protected int mTotalLines;

	// Token: 0x040007AA RID: 1962
	protected int mLastWidth;

	// Token: 0x040007AB RID: 1963
	protected int mLastHeight;

	// Token: 0x040007AC RID: 1964
	private BetterList<UITextList.Paragraph> mParagraphs;

	// Token: 0x040007AD RID: 1965
	private static Dictionary<string, BetterList<UITextList.Paragraph>> mHistory = new Dictionary<string, BetterList<UITextList.Paragraph>>();

	// Token: 0x020006A5 RID: 1701
	[DoNotObfuscateNGUI]
	public enum Style
	{
		// Token: 0x040046D5 RID: 18133
		Text,
		// Token: 0x040046D6 RID: 18134
		Chat
	}

	// Token: 0x020006A6 RID: 1702
	protected class Paragraph
	{
		// Token: 0x040046D7 RID: 18135
		public string text;

		// Token: 0x040046D8 RID: 18136
		public string[] lines;
	}
}
