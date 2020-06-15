using System;
using UnityEngine;

// Token: 0x0200042B RID: 1067
public class TitleSponsorScript : MonoBehaviour
{
	// Token: 0x06001C67 RID: 7271 RVA: 0x00155280 File Offset: 0x00153480
	private void Start()
	{
		base.transform.localPosition = new Vector3(1050f, base.transform.localPosition.y, base.transform.localPosition.z);
		this.UpdateHighlight();
		if (GameGlobals.LoveSick)
		{
			this.TurnLoveSick();
		}
	}

	// Token: 0x06001C68 RID: 7272 RVA: 0x001552D5 File Offset: 0x001534D5
	public int GetSponsorIndex()
	{
		return this.Column + this.Row * this.Columns;
	}

	// Token: 0x06001C69 RID: 7273 RVA: 0x001552EB File Offset: 0x001534EB
	public bool SponsorHasWebsite(int index)
	{
		return !string.IsNullOrEmpty(this.SponsorURLs[index]);
	}

	// Token: 0x06001C6A RID: 7274 RVA: 0x00155300 File Offset: 0x00153500
	private void Update()
	{
		if (!this.Show)
		{
			base.transform.localPosition = new Vector3(Mathf.Lerp(base.transform.localPosition.x, 1050f, Time.deltaTime * 10f), base.transform.localPosition.y, base.transform.localPosition.z);
			return;
		}
		base.transform.localPosition = new Vector3(Mathf.Lerp(base.transform.localPosition.x, 0f, Time.deltaTime * 10f), base.transform.localPosition.y, base.transform.localPosition.z);
		if (this.InputManager.TappedUp)
		{
			this.Row = ((this.Row > 0) ? (this.Row - 1) : (this.Rows - 1));
		}
		if (this.InputManager.TappedDown)
		{
			this.Row = ((this.Row < this.Rows - 1) ? (this.Row + 1) : 0);
		}
		if (this.InputManager.TappedRight)
		{
			this.Column = ((this.Column < this.Columns - 1) ? (this.Column + 1) : 0);
		}
		if (this.InputManager.TappedLeft)
		{
			this.Column = ((this.Column > 0) ? (this.Column - 1) : (this.Columns - 1));
		}
		if (this.InputManager.TappedUp || this.InputManager.TappedDown || this.InputManager.TappedRight || this.InputManager.TappedLeft)
		{
			this.UpdateHighlight();
		}
		if (Input.GetButtonDown("A"))
		{
			int sponsorIndex = this.GetSponsorIndex();
			if (this.SponsorHasWebsite(sponsorIndex))
			{
				Application.OpenURL(this.SponsorURLs[sponsorIndex]);
			}
		}
	}

	// Token: 0x06001C6B RID: 7275 RVA: 0x001554E0 File Offset: 0x001536E0
	private void UpdateHighlight()
	{
		this.Highlight.localPosition = new Vector3(-384f + (float)this.Column * 256f, 128f - (float)this.Row * 256f, this.Highlight.localPosition.z);
		this.SponsorName.text = this.Sponsors[this.GetSponsorIndex()];
	}

	// Token: 0x06001C6C RID: 7276 RVA: 0x0015554C File Offset: 0x0015374C
	private void TurnLoveSick()
	{
		this.BlackSprite.color = Color.black;
		foreach (UISprite uisprite in this.RedSprites)
		{
			uisprite.color = new Color(1f, 0f, 0f, uisprite.color.a);
		}
		foreach (UILabel uilabel in this.Labels)
		{
			uilabel.color = new Color(1f, 0f, 0f, uilabel.color.a);
		}
	}

	// Token: 0x0400357A RID: 13690
	public InputManagerScript InputManager;

	// Token: 0x0400357B RID: 13691
	public string[] SponsorURLs;

	// Token: 0x0400357C RID: 13692
	public string[] Sponsors;

	// Token: 0x0400357D RID: 13693
	public UILabel SponsorName;

	// Token: 0x0400357E RID: 13694
	public Transform Highlight;

	// Token: 0x0400357F RID: 13695
	public bool Show;

	// Token: 0x04003580 RID: 13696
	public int Columns;

	// Token: 0x04003581 RID: 13697
	public int Rows;

	// Token: 0x04003582 RID: 13698
	private int Column;

	// Token: 0x04003583 RID: 13699
	private int Row;

	// Token: 0x04003584 RID: 13700
	public UISprite BlackSprite;

	// Token: 0x04003585 RID: 13701
	public UISprite[] RedSprites;

	// Token: 0x04003586 RID: 13702
	public UILabel[] Labels;
}
