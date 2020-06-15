using System;
using System.Globalization;
using UnityEngine;

// Token: 0x02000402 RID: 1026
public class StreetShopScript : MonoBehaviour
{
	// Token: 0x06001B24 RID: 6948 RVA: 0x00112D34 File Offset: 0x00110F34
	private void Start()
	{
		this.MyLabel.color = new Color(1f, 1f, 1f, 0f);
	}

	// Token: 0x06001B25 RID: 6949 RVA: 0x00112D5C File Offset: 0x00110F5C
	private void Update()
	{
		if (Vector3.Distance(this.Yandere.transform.position, base.transform.position) < 1f)
		{
			this.Alpha = Mathf.MoveTowards(this.Alpha, 1f, Time.deltaTime * 10f);
		}
		else
		{
			this.Alpha = Mathf.MoveTowards(this.Alpha, 0f, Time.deltaTime * 10f);
		}
		this.MyLabel.color = new Color(1f, 0.75f, 1f, this.Alpha);
		if (this.Alpha == 1f && Input.GetButtonDown("A"))
		{
			if (this.Exit)
			{
				this.StreetManager.FadeOut = true;
				this.Yandere.MyAnimation.CrossFade(this.Yandere.IdleAnim);
				this.Yandere.CanMove = false;
			}
			else if (this.MaidCafe)
			{
				this.StreetShopInterface.ShowMaid = true;
				this.Yandere.MyAnimation.CrossFade(this.Yandere.IdleAnim);
				this.Yandere.RPGCamera.enabled = false;
				this.Yandere.CanMove = false;
			}
			else if (!this.Binoculars)
			{
				if (!this.StreetShopInterface.Show)
				{
					this.StreetShopInterface.CurrentStore = this.StoreType;
					this.StreetShopInterface.Show = true;
					this.PromptBar.ClearButtons();
					this.PromptBar.Label[0].text = "Purchase";
					this.PromptBar.Label[1].text = "Exit";
					this.PromptBar.UpdateButtons();
					this.PromptBar.Show = true;
					this.Yandere.MyAnimation.CrossFade(this.Yandere.IdleAnim);
					this.Yandere.CanMove = false;
					this.UpdateShopInterface();
				}
			}
			else if (PlayerGlobals.Money >= 0.25f)
			{
				this.MyAudio.clip = this.InsertCoin;
				PlayerGlobals.Money -= 0.25f;
				this.HomeClock.UpdateMoneyLabel();
				this.BinocularCamera.gameObject.SetActive(true);
				this.BinocularRenderer.enabled = false;
				this.BinocularOverlay.SetActive(true);
				this.PromptBar.ClearButtons();
				this.PromptBar.Label[1].text = "Exit";
				this.PromptBar.UpdateButtons();
				this.PromptBar.Show = true;
				this.Yandere.MyAnimation.CrossFade(this.Yandere.IdleAnim);
				this.Yandere.transform.position = new Vector3(5f, 0f, 3f);
				this.Yandere.CanMove = false;
				this.MyAudio.Play();
			}
			else
			{
				this.HomeClock.MoneyFail();
			}
		}
		if (this.Binoculars && this.BinocularCamera.gameObject.activeInHierarchy)
		{
			if (this.InputDevice.Type == InputDeviceType.MouseAndKeyboard)
			{
				this.RotationX -= Input.GetAxis("Mouse Y") * (this.BinocularCamera.fieldOfView / 60f);
				this.RotationY += Input.GetAxis("Mouse X") * (this.BinocularCamera.fieldOfView / 60f);
			}
			else
			{
				this.RotationX -= Input.GetAxis("Mouse Y") * (this.BinocularCamera.fieldOfView / 60f);
				this.RotationY += Input.GetAxis("Mouse X") * (this.BinocularCamera.fieldOfView / 60f);
			}
			this.BinocularCamera.transform.eulerAngles = new Vector3(this.RotationX, this.RotationY + 90f, 0f);
			if (this.RotationX > 45f)
			{
				this.RotationX = 45f;
			}
			if (this.RotationX < -45f)
			{
				this.RotationX = -45f;
			}
			if (this.RotationY > 90f)
			{
				this.RotationY = 90f;
			}
			if (this.RotationY < -90f)
			{
				this.RotationY = -90f;
			}
			this.Zoom -= Input.GetAxis("Mouse ScrollWheel") * 10f;
			this.Zoom -= Input.GetAxis("Vertical") * 0.1f;
			if (this.Zoom > 60f)
			{
				this.Zoom = 60f;
			}
			else if (this.Zoom < 1f)
			{
				this.Zoom = 1f;
			}
			this.BinocularCamera.fieldOfView = Mathf.Lerp(this.BinocularCamera.fieldOfView, this.Zoom, Time.deltaTime * 10f);
			this.StreetManager.CurrentlyActiveJukebox.volume = this.BinocularCamera.fieldOfView / 60f * 0.5f;
			if (Input.GetButtonUp("B"))
			{
				this.BinocularCamera.gameObject.SetActive(false);
				this.BinocularRenderer.enabled = true;
				this.BinocularOverlay.SetActive(false);
				this.RotationX = 0f;
				this.RotationY = 0f;
				this.Zoom = 60f;
				this.StreetManager.CurrentlyActiveJukebox.volume = 0.5f;
				this.PromptBar.ClearButtons();
				this.PromptBar.Show = false;
				this.Yandere.CanMove = true;
			}
		}
	}

	// Token: 0x06001B26 RID: 6950 RVA: 0x00113314 File Offset: 0x00111514
	private void UpdateShopInterface()
	{
		this.Yandere.MainCamera.GetComponent<RPG_Camera>().enabled = false;
		this.StreetShopInterface.StoreNameLabel.text = this.StoreName;
		this.StreetShopInterface.MoneyLabel.text = "$" + PlayerGlobals.Money.ToString("F2", NumberFormatInfo.InvariantInfo);
		this.StreetShopInterface.Shopkeeper.mainTexture = this.ShopkeeperPortraits[1];
		this.StreetShopInterface.SpeechBubbleLabel.text = this.ShopkeeperSpeeches[1];
		this.StreetShopInterface.ShopkeeperPortraits = this.ShopkeeperPortraits;
		this.StreetShopInterface.ShopkeeperSpeeches = this.ShopkeeperSpeeches;
		this.StreetShopInterface.ShopkeeperPosition = this.ShopkeeperPosition;
		this.StreetShopInterface.AdultProducts = this.AdultProducts;
		this.StreetShopInterface.SpeechPhase = 0;
		this.StreetShopInterface.Costs = this.Costs;
		this.StreetShopInterface.Limit = this.Limit;
		this.StreetShopInterface.Selected = 1;
		this.StreetShopInterface.Timer = 0f;
		this.StreetShopInterface.UpdateHighlight();
		for (int i = 1; i < 11; i++)
		{
			this.StreetShopInterface.ProductsLabel[i].text = this.Products[i];
			this.StreetShopInterface.PricesLabel[i].text = "$" + this.Costs[i];
			if (this.StreetShopInterface.PricesLabel[i].text == "$0")
			{
				this.StreetShopInterface.PricesLabel[i].text = "";
			}
			if (this.StoreType == ShopType.Salon)
			{
				this.StreetShopInterface.PricesLabel[i].text = "Free";
			}
		}
		this.StreetShopInterface.UpdateIcons();
	}

	// Token: 0x04002C61 RID: 11361
	public StreetShopInterfaceScript StreetShopInterface;

	// Token: 0x04002C62 RID: 11362
	public StreetManagerScript StreetManager;

	// Token: 0x04002C63 RID: 11363
	public InputDeviceScript InputDevice;

	// Token: 0x04002C64 RID: 11364
	public StalkerYandereScript Yandere;

	// Token: 0x04002C65 RID: 11365
	public PromptBarScript PromptBar;

	// Token: 0x04002C66 RID: 11366
	public HomeClockScript HomeClock;

	// Token: 0x04002C67 RID: 11367
	public GameObject BinocularOverlay;

	// Token: 0x04002C68 RID: 11368
	public Renderer BinocularRenderer;

	// Token: 0x04002C69 RID: 11369
	public Camera BinocularCamera;

	// Token: 0x04002C6A RID: 11370
	public AudioSource MyAudio;

	// Token: 0x04002C6B RID: 11371
	public AudioClip InsertCoin;

	// Token: 0x04002C6C RID: 11372
	public AudioClip Fail;

	// Token: 0x04002C6D RID: 11373
	public UILabel MyLabel;

	// Token: 0x04002C6E RID: 11374
	public Texture[] ShopkeeperPortraits;

	// Token: 0x04002C6F RID: 11375
	public string[] ShopkeeperSpeeches;

	// Token: 0x04002C70 RID: 11376
	public bool[] AdultProducts;

	// Token: 0x04002C71 RID: 11377
	public string[] Products;

	// Token: 0x04002C72 RID: 11378
	public float[] Costs;

	// Token: 0x04002C73 RID: 11379
	public float RotationX;

	// Token: 0x04002C74 RID: 11380
	public float RotationY;

	// Token: 0x04002C75 RID: 11381
	public float Alpha;

	// Token: 0x04002C76 RID: 11382
	public float Zoom;

	// Token: 0x04002C77 RID: 11383
	public int ShopkeeperPosition = 500;

	// Token: 0x04002C78 RID: 11384
	public int Limit;

	// Token: 0x04002C79 RID: 11385
	public bool Binoculars;

	// Token: 0x04002C7A RID: 11386
	public bool MaidCafe;

	// Token: 0x04002C7B RID: 11387
	public bool Exit;

	// Token: 0x04002C7C RID: 11388
	public string StoreName;

	// Token: 0x04002C7D RID: 11389
	public ShopType StoreType;
}
