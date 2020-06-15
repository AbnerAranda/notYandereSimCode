using System;
using System.Collections.Generic;

// Token: 0x0200006E RID: 110
[Serializable]
public class BMGlyph
{
	// Token: 0x0600035A RID: 858 RVA: 0x00020684 File Offset: 0x0001E884
	public int GetKerning(int previousChar)
	{
		if (this.kerning != null && previousChar != 0)
		{
			int i = 0;
			int count = this.kerning.Count;
			while (i < count)
			{
				if (this.kerning[i] == previousChar)
				{
					return this.kerning[i + 1];
				}
				i += 2;
			}
		}
		return 0;
	}

	// Token: 0x0600035B RID: 859 RVA: 0x000206D4 File Offset: 0x0001E8D4
	public void SetKerning(int previousChar, int amount)
	{
		if (this.kerning == null)
		{
			this.kerning = new List<int>();
		}
		for (int i = 0; i < this.kerning.Count; i += 2)
		{
			if (this.kerning[i] == previousChar)
			{
				this.kerning[i + 1] = amount;
				return;
			}
		}
		this.kerning.Add(previousChar);
		this.kerning.Add(amount);
	}

	// Token: 0x0600035C RID: 860 RVA: 0x00020744 File Offset: 0x0001E944
	public void Trim(int xMin, int yMin, int xMax, int yMax)
	{
		int num = this.x + this.width;
		int num2 = this.y + this.height;
		if (this.x < xMin)
		{
			int num3 = xMin - this.x;
			this.x += num3;
			this.width -= num3;
			this.offsetX += num3;
		}
		if (this.y < yMin)
		{
			int num4 = yMin - this.y;
			this.y += num4;
			this.height -= num4;
			this.offsetY += num4;
		}
		if (num > xMax)
		{
			this.width -= num - xMax;
		}
		if (num2 > yMax)
		{
			this.height -= num2 - yMax;
		}
	}

	// Token: 0x040004A0 RID: 1184
	public int index;

	// Token: 0x040004A1 RID: 1185
	public int x;

	// Token: 0x040004A2 RID: 1186
	public int y;

	// Token: 0x040004A3 RID: 1187
	public int width;

	// Token: 0x040004A4 RID: 1188
	public int height;

	// Token: 0x040004A5 RID: 1189
	public int offsetX;

	// Token: 0x040004A6 RID: 1190
	public int offsetY;

	// Token: 0x040004A7 RID: 1191
	public int advance;

	// Token: 0x040004A8 RID: 1192
	public int channel;

	// Token: 0x040004A9 RID: 1193
	public List<int> kerning;
}
