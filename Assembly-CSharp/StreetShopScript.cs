using System;
using UnityEngine;

// Token: 0x020003FD RID: 1021
public class StreetShopScript : MonoBehaviour
{
	// Token: 0x06001B02 RID: 6914 RVA: 0x00110444 File Offset: 0x0010E644
	private void Start()
	{
		this.MyLabel.color = new Color(1f, 1f, 1f, 0f);
	}

	// Token: 0x06001B03 RID: 6915 RVA: 0x0011046C File Offset: 0x0010E66C
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

	// Token: 0x06001B04 RID: 6916 RVA: 0x00110A24 File Offset: 0x0010EC24
	private void UpdateShopInterface()
	{
		this.Yandere.MainCamera.GetComponent<RPG_Camera>().enabled = false;
		this.StreetShopInterface.StoreNameLabel.text = this.StoreName;
		this.StreetShopInterface.MoneyLabel.text = "$" + PlayerGlobals.Money.ToString("F2");
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

	// Token: 0x04002BFF RID: 11263
	public StreetShopInterfaceScript StreetShopInterface;

	// Token: 0x04002C00 RID: 11264
	public StreetManagerScript StreetManager;

	// Token: 0x04002C01 RID: 11265
	public InputDeviceScript InputDevice;

	// Token: 0x04002C02 RID: 11266
	public StalkerYandereScript Yandere;

	// Token: 0x04002C03 RID: 11267
	public PromptBarScript PromptBar;

	// Token: 0x04002C04 RID: 11268
	public HomeClockScript HomeClock;

	// Token: 0x04002C05 RID: 11269
	public GameObject BinocularOverlay;

	// Token: 0x04002C06 RID: 11270
	public Renderer BinocularRenderer;

	// Token: 0x04002C07 RID: 11271
	public Camera BinocularCamera;

	// Token: 0x04002C08 RID: 11272
	public AudioSource MyAudio;

	// Token: 0x04002C09 RID: 11273
	public AudioClip InsertCoin;

	// Token: 0x04002C0A RID: 11274
	public AudioClip Fail;

	// Token: 0x04002C0B RID: 11275
	public UILabel MyLabel;

	// Token: 0x04002C0C RID: 11276
	public Texture[] ShopkeeperPortraits;

	// Token: 0x04002C0D RID: 11277
	public string[] ShopkeeperSpeeches;

	// Token: 0x04002C0E RID: 11278
	public bool[] AdultProducts;

	// Token: 0x04002C0F RID: 11279
	public string[] Products;

	// Token: 0x04002C10 RID: 11280
	public float[] Costs;

	// Token: 0x04002C11 RID: 11281
	public float RotationX;

	// Token: 0x04002C12 RID: 11282
	public float RotationY;

	// Token: 0x04002C13 RID: 11283
	public float Alpha;

	// Token: 0x04002C14 RID: 11284
	public float Zoom;

	// Token: 0x04002C15 RID: 11285
	public int ShopkeeperPosition = 500;

	// Token: 0x04002C16 RID: 11286
	public int Limit;

	// Token: 0x04002C17 RID: 11287
	public bool Binoculars;

	// Token: 0x04002C18 RID: 11288
	public bool MaidCafe;

	// Token: 0x04002C19 RID: 11289
	public bool Exit;

	// Token: 0x04002C1A RID: 11290
	public string StoreName;

	// Token: 0x04002C1B RID: 11291
	public ShopType StoreType;
}
