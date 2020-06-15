using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.PostProcessing;

// Token: 0x02000401 RID: 1025
public class StreetShopInterfaceScript : MonoBehaviour
{
	// Token: 0x06001B1B RID: 6939 RVA: 0x00112128 File Offset: 0x00110328
	private void Start()
	{
		this.Shopkeeper.transform.localPosition = new Vector3(1485f, 0f, 0f);
		this.Interface.localPosition = new Vector3(-815.5f, 0f, 0f);
		this.SpeechBubbleParent.localScale = new Vector3(0f, 0f, 0f);
		this.UpdateFakeID();
	}

	// Token: 0x06001B1C RID: 6940 RVA: 0x001121A0 File Offset: 0x001103A0
	private void Update()
	{
		if (this.Show)
		{
			this.Shopkeeper.transform.localPosition = Vector3.Lerp(this.Shopkeeper.transform.localPosition, new Vector3((float)this.ShopkeeperPosition, 0f, 0f), Time.deltaTime * 10f);
			this.Interface.localPosition = Vector3.Lerp(this.Interface.localPosition, new Vector3(100f, 0f, 0f), Time.deltaTime * 10f);
			this.BlurAmount = Mathf.Lerp(this.BlurAmount, 0f, Time.deltaTime * 10f);
			if (Input.GetButtonUp("B"))
			{
				this.Yandere.RPGCamera.enabled = true;
				this.PromptBar.Show = false;
				this.Yandere.CanMove = true;
				this.Show = false;
			}
			if (this.Timer > 0.5f && Input.GetButtonUp("A") && this.Icons[this.Selected].spriteName != "Yes")
			{
				this.CheckStore();
				this.UpdateIcons();
			}
			if (this.InputManager.TappedDown)
			{
				this.Selected++;
				if (this.Selected > this.Limit)
				{
					this.Selected = 1;
				}
				this.UpdateHighlight();
			}
			else if (this.InputManager.TappedUp)
			{
				this.Selected--;
				if (this.Selected < 1)
				{
					this.Selected = this.Limit;
				}
				this.UpdateHighlight();
			}
			this.Timer += Time.deltaTime;
			if (this.Timer > 0.5f)
			{
				this.SpeechBubbleParent.localScale = Vector3.Lerp(this.SpeechBubbleParent.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
			}
			if (this.SpeechPhase == 0)
			{
				this.Shopkeeper.mainTexture = this.ShopkeeperPortraits[1];
				this.SpeechPhase++;
			}
			else if (this.Timer > 10f)
			{
				if (this.SpeechPhase == 1)
				{
					this.SpeechBubbleLabel.text = this.ShopkeeperSpeeches[2];
					this.Shopkeeper.mainTexture = this.ShopkeeperPortraits[2];
					this.SpeechBubbleParent.localScale = new Vector3(0f, 0f, 0f);
					this.SpeechPhase++;
				}
				else if (this.SpeechPhase == 2 && this.Timer > 10.1f)
				{
					int num = UnityEngine.Random.Range(2, 4);
					this.Shopkeeper.mainTexture = this.ShopkeeperPortraits[num];
					this.Timer = 10f;
				}
			}
		}
		else
		{
			this.SpeechBubbleParent.localScale = new Vector3(0f, 0f, 0f);
			this.Shopkeeper.transform.localPosition = Vector3.Lerp(this.Shopkeeper.transform.localPosition, new Vector3(1604f, 0f, 0f), Time.deltaTime * 10f);
			this.Interface.localPosition = Vector3.Lerp(this.Interface.localPosition, new Vector3(-815.5f, 0f, 0f), Time.deltaTime * 10f);
			if (this.ShowMaid)
			{
				this.BlurAmount = Mathf.Lerp(this.BlurAmount, 0f, Time.deltaTime * 10f);
				this.MaidWindow.localScale = Vector3.Lerp(this.MaidWindow.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
				if (Input.GetButtonDown("A"))
				{
					this.StreetManager.FadeOut = true;
					this.StreetManager.GoToCafe = true;
				}
				else if (Input.GetButtonDown("B"))
				{
					this.Yandere.RPGCamera.enabled = true;
					this.Yandere.CanMove = true;
					this.ShowMaid = false;
				}
			}
			else
			{
				this.BlurAmount = Mathf.Lerp(this.BlurAmount, 0.6f, Time.deltaTime * 10f);
				this.MaidWindow.localScale = Vector3.Lerp(this.MaidWindow.localScale, new Vector3(0f, 0f, 0f), Time.deltaTime * 10f);
			}
		}
		this.AdjustBlur();
	}

	// Token: 0x06001B1D RID: 6941 RVA: 0x00112648 File Offset: 0x00110848
	private void AdjustBlur()
	{
		DepthOfFieldModel.Settings settings = this.Profile.depthOfField.settings;
		settings.focusDistance = this.BlurAmount;
		this.Profile.depthOfField.settings = settings;
	}

	// Token: 0x06001B1E RID: 6942 RVA: 0x00112684 File Offset: 0x00110884
	public void UpdateHighlight()
	{
		this.Highlight.localPosition = new Vector3(-50f, (float)(50 - 50 * this.Selected), 0f);
	}

	// Token: 0x06001B1F RID: 6943 RVA: 0x001126B0 File Offset: 0x001108B0
	public void CheckStore()
	{
		if (this.AdultProducts[this.Selected] && !PlayerGlobals.FakeID)
		{
			this.SpeechBubbleLabel.text = this.ShopkeeperSpeeches[3];
			this.SpeechBubbleParent.localScale = new Vector3(0f, 0f, 0f);
			this.SpeechPhase = 0;
			this.Timer = 1f;
			return;
		}
		if (PlayerGlobals.Money < this.Costs[this.Selected])
		{
			this.StreetManager.Clock.MoneyFail();
			this.SpeechBubbleLabel.text = this.ShopkeeperSpeeches[4];
			this.SpeechBubbleParent.localScale = new Vector3(0f, 0f, 0f);
			this.SpeechPhase = 0;
			this.Timer = 1f;
			return;
		}
		ShopType currentStore = this.CurrentStore;
		switch (currentStore)
		{
		case ShopType.Nonfunctional:
			this.SpeechBubbleLabel.text = this.ShopkeeperSpeeches[6];
			this.SpeechBubbleParent.localScale = new Vector3(0f, 0f, 0f);
			this.SpeechPhase = 0;
			this.Timer = 1f;
			return;
		case ShopType.Hardware:
		case ShopType.Maid:
			break;
		case ShopType.Manga:
			this.PurchaseEffect();
			switch (this.Selected)
			{
			case 1:
				CollectibleGlobals.SetMangaCollected(6, true);
				return;
			case 2:
				CollectibleGlobals.SetMangaCollected(7, true);
				return;
			case 3:
				CollectibleGlobals.SetMangaCollected(8, true);
				return;
			case 4:
				CollectibleGlobals.SetMangaCollected(9, true);
				return;
			case 5:
				CollectibleGlobals.SetMangaCollected(10, true);
				return;
			case 6:
				CollectibleGlobals.SetMangaCollected(1, true);
				return;
			case 7:
				CollectibleGlobals.SetMangaCollected(2, true);
				return;
			case 8:
				CollectibleGlobals.SetMangaCollected(3, true);
				return;
			case 9:
				CollectibleGlobals.SetMangaCollected(4, true);
				return;
			case 10:
				CollectibleGlobals.SetMangaCollected(5, true);
				return;
			default:
				return;
			}
			break;
		case ShopType.Salon:
			this.SpeechBubbleLabel.text = this.ShopkeeperSpeeches[6];
			this.SpeechBubbleParent.localScale = new Vector3(0f, 0f, 0f);
			this.SpeechPhase = 0;
			this.Timer = 1f;
			break;
		case ShopType.Gift:
			this.PurchaseEffect();
			if (this.Selected < 6)
			{
				CollectibleGlobals.SenpaiGifts++;
			}
			else
			{
				CollectibleGlobals.MatchmakingGifts++;
			}
			CollectibleGlobals.SetGiftPurchased(this.Selected, true);
			return;
		default:
			if (currentStore != ShopType.Lingerie)
			{
				return;
			}
			this.PurchaseEffect();
			CollectibleGlobals.SetPantyPurchased(this.Selected, true);
			return;
		}
	}

	// Token: 0x06001B20 RID: 6944 RVA: 0x00112914 File Offset: 0x00110B14
	public void PurchaseEffect()
	{
		this.SpeechBubbleLabel.text = this.ShopkeeperSpeeches[5];
		this.SpeechBubbleParent.localScale = new Vector3(0f, 0f, 0f);
		this.SpeechPhase = 0;
		this.Timer = 1f;
		PlayerGlobals.Money -= this.Costs[this.Selected];
		this.MoneyLabel.text = "$" + PlayerGlobals.Money.ToString("F2", NumberFormatInfo.InvariantInfo);
		this.StreetManager.Clock.UpdateMoneyLabel();
		this.MyAudio.Play();
	}

	// Token: 0x06001B21 RID: 6945 RVA: 0x001129C4 File Offset: 0x00110BC4
	public void UpdateFakeID()
	{
		this.FakeIDBox.SetActive(PlayerGlobals.FakeID);
	}

	// Token: 0x06001B22 RID: 6946 RVA: 0x001129D8 File Offset: 0x00110BD8
	public void UpdateIcons()
	{
		for (int i = 1; i < 11; i++)
		{
			this.Icons[i].spriteName = "";
			this.Icons[i].gameObject.SetActive(false);
			this.ProductsLabel[i].color = new Color(1f, 1f, 1f, 1f);
		}
		for (int i = 1; i < 11; i++)
		{
			if (this.AdultProducts[i])
			{
				this.Icons[i].spriteName = "18+";
			}
		}
		ShopType currentStore = this.CurrentStore;
		if (currentStore != ShopType.Manga)
		{
			if (currentStore != ShopType.Gift)
			{
				if (currentStore == ShopType.Lingerie)
				{
					for (int i = 1; i < 11; i++)
					{
						if (CollectibleGlobals.GetPantyPurchased(i))
						{
							this.Icons[i].spriteName = "Yes";
							this.PricesLabel[i].text = "Owned";
						}
					}
				}
			}
			else
			{
				for (int i = 1; i < 11; i++)
				{
					if (CollectibleGlobals.GetGiftPurchased(i))
					{
						this.Icons[i].spriteName = "Yes";
						this.PricesLabel[i].text = "Owned";
					}
				}
			}
		}
		else
		{
			if (CollectibleGlobals.GetMangaCollected(1))
			{
				this.Icons[6].spriteName = "Yes";
				this.PricesLabel[6].text = "Owned";
			}
			if (CollectibleGlobals.GetMangaCollected(2))
			{
				this.Icons[7].spriteName = "Yes";
				this.PricesLabel[7].text = "Owned";
			}
			if (CollectibleGlobals.GetMangaCollected(3))
			{
				this.Icons[8].spriteName = "Yes";
				this.PricesLabel[8].text = "Owned";
			}
			if (CollectibleGlobals.GetMangaCollected(4))
			{
				this.Icons[9].spriteName = "Yes";
				this.PricesLabel[9].text = "Owned";
			}
			if (CollectibleGlobals.GetMangaCollected(5))
			{
				this.Icons[10].spriteName = "Yes";
				this.PricesLabel[10].text = "Owned";
			}
			if (CollectibleGlobals.GetMangaCollected(6))
			{
				this.Icons[1].spriteName = "Yes";
				this.PricesLabel[1].text = "Owned";
			}
			if (CollectibleGlobals.GetMangaCollected(7))
			{
				this.Icons[2].spriteName = "Yes";
				this.PricesLabel[2].text = "Owned";
			}
			if (CollectibleGlobals.GetMangaCollected(8))
			{
				this.Icons[3].spriteName = "Yes";
				this.PricesLabel[3].text = "Owned";
			}
			if (CollectibleGlobals.GetMangaCollected(9))
			{
				this.Icons[4].spriteName = "Yes";
				this.PricesLabel[4].text = "Owned";
			}
			if (CollectibleGlobals.GetMangaCollected(10))
			{
				this.Icons[5].spriteName = "Yes";
				this.PricesLabel[5].text = "Owned";
			}
		}
		for (int i = 1; i < 11; i++)
		{
			if (this.Icons[i].spriteName != "")
			{
				this.Icons[i].gameObject.SetActive(true);
				if (this.Icons[i].spriteName == "Yes")
				{
					this.ProductsLabel[i].color = new Color(1f, 1f, 1f, 0.5f);
				}
			}
		}
	}

	// Token: 0x04002C42 RID: 11330
	public StreetManagerScript StreetManager;

	// Token: 0x04002C43 RID: 11331
	public InputManagerScript InputManager;

	// Token: 0x04002C44 RID: 11332
	public PostProcessingProfile Profile;

	// Token: 0x04002C45 RID: 11333
	public StalkerYandereScript Yandere;

	// Token: 0x04002C46 RID: 11334
	public PromptBarScript PromptBar;

	// Token: 0x04002C47 RID: 11335
	public UILabel SpeechBubbleLabel;

	// Token: 0x04002C48 RID: 11336
	public UILabel StoreNameLabel;

	// Token: 0x04002C49 RID: 11337
	public UILabel MoneyLabel;

	// Token: 0x04002C4A RID: 11338
	public Texture[] ShopkeeperPortraits;

	// Token: 0x04002C4B RID: 11339
	public string[] ShopkeeperSpeeches;

	// Token: 0x04002C4C RID: 11340
	public UILabel[] ProductsLabel;

	// Token: 0x04002C4D RID: 11341
	public UILabel[] PricesLabel;

	// Token: 0x04002C4E RID: 11342
	public UISprite[] Icons;

	// Token: 0x04002C4F RID: 11343
	public bool[] AdultProducts;

	// Token: 0x04002C50 RID: 11344
	public float[] Costs;

	// Token: 0x04002C51 RID: 11345
	public UITexture Shopkeeper;

	// Token: 0x04002C52 RID: 11346
	public Transform SpeechBubbleParent;

	// Token: 0x04002C53 RID: 11347
	public Transform MaidWindow;

	// Token: 0x04002C54 RID: 11348
	public Transform Highlight;

	// Token: 0x04002C55 RID: 11349
	public Transform Interface;

	// Token: 0x04002C56 RID: 11350
	public GameObject FakeIDBox;

	// Token: 0x04002C57 RID: 11351
	public AudioSource MyAudio;

	// Token: 0x04002C58 RID: 11352
	public int ShopkeeperPosition;

	// Token: 0x04002C59 RID: 11353
	public int SpeechPhase;

	// Token: 0x04002C5A RID: 11354
	public int Selected;

	// Token: 0x04002C5B RID: 11355
	public int Limit;

	// Token: 0x04002C5C RID: 11356
	public float BlurAmount;

	// Token: 0x04002C5D RID: 11357
	public float Timer;

	// Token: 0x04002C5E RID: 11358
	public bool ShowMaid;

	// Token: 0x04002C5F RID: 11359
	public bool Show;

	// Token: 0x04002C60 RID: 11360
	public ShopType CurrentStore;
}
