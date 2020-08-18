using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000360 RID: 864
public class PhotoGalleryScript : MonoBehaviour
{
	// Token: 0x060018C9 RID: 6345 RVA: 0x000E582A File Offset: 0x000E3A2A
	private void Start()
	{
		if (this.Cursor != null)
		{
			this.Cursor.gameObject.SetActive(false);
			base.enabled = false;
		}
		if (this.Corkboard)
		{
			base.StartCoroutine(this.GetPhotos());
		}
	}

	// Token: 0x17000457 RID: 1111
	// (get) Token: 0x060018CA RID: 6346 RVA: 0x000E5867 File Offset: 0x000E3A67
	private int CurrentIndex
	{
		get
		{
			return this.Column + (this.Row - 1) * 5;
		}
	}

	// Token: 0x17000458 RID: 1112
	// (get) Token: 0x060018CB RID: 6347 RVA: 0x000E587A File Offset: 0x000E3A7A
	private float LerpSpeed
	{
		get
		{
			return Time.unscaledDeltaTime * 10f;
		}
	}

	// Token: 0x17000459 RID: 1113
	// (get) Token: 0x060018CC RID: 6348 RVA: 0x000E5887 File Offset: 0x000E3A87
	private float HighlightX
	{
		get
		{
			return -450f + 150f * (float)this.Column;
		}
	}

	// Token: 0x1700045A RID: 1114
	// (get) Token: 0x060018CD RID: 6349 RVA: 0x000E589C File Offset: 0x000E3A9C
	private float HighlightY
	{
		get
		{
			return 225f - 75f * (float)this.Row;
		}
	}

	// Token: 0x1700045B RID: 1115
	// (get) Token: 0x060018CE RID: 6350 RVA: 0x000E58B4 File Offset: 0x000E3AB4
	// (set) Token: 0x060018CF RID: 6351 RVA: 0x000E58EC File Offset: 0x000E3AEC
	private float MovingPhotoXPercent
	{
		get
		{
			float num = -this.MaxPhotoX;
			float maxPhotoX = this.MaxPhotoX;
			return (this.MovingPhotograph.transform.localPosition.x - num) / (maxPhotoX - num);
		}
		set
		{
			this.MovingPhotograph.transform.localPosition = new Vector3(-this.MaxPhotoX + 2f * (this.MaxPhotoX * Mathf.Clamp01(value)), this.MovingPhotograph.transform.localPosition.y, this.MovingPhotograph.transform.localPosition.z);
		}
	}

	// Token: 0x1700045C RID: 1116
	// (get) Token: 0x060018D0 RID: 6352 RVA: 0x000E5954 File Offset: 0x000E3B54
	// (set) Token: 0x060018D1 RID: 6353 RVA: 0x000E598C File Offset: 0x000E3B8C
	private float MovingPhotoYPercent
	{
		get
		{
			float num = -this.MaxPhotoY;
			float maxPhotoY = this.MaxPhotoY;
			return (this.MovingPhotograph.transform.localPosition.y - num) / (maxPhotoY - num);
		}
		set
		{
			this.MovingPhotograph.transform.localPosition = new Vector3(this.MovingPhotograph.transform.localPosition.x, -this.MaxPhotoY + 2f * (this.MaxPhotoY * Mathf.Clamp01(value)), this.MovingPhotograph.transform.localPosition.z);
		}
	}

	// Token: 0x1700045D RID: 1117
	// (get) Token: 0x060018D2 RID: 6354 RVA: 0x000E59F3 File Offset: 0x000E3BF3
	// (set) Token: 0x060018D3 RID: 6355 RVA: 0x000E5A0C File Offset: 0x000E3C0C
	private float MovingPhotoRotation
	{
		get
		{
			return this.MovingPhotograph.transform.localEulerAngles.z;
		}
		set
		{
			this.MovingPhotograph.transform.localEulerAngles = new Vector3(this.MovingPhotograph.transform.localEulerAngles.x, this.MovingPhotograph.transform.localEulerAngles.y, value);
		}
	}

	// Token: 0x1700045E RID: 1118
	// (get) Token: 0x060018D4 RID: 6356 RVA: 0x000E5A5C File Offset: 0x000E3C5C
	// (set) Token: 0x060018D5 RID: 6357 RVA: 0x000E5A90 File Offset: 0x000E3C90
	private float CursorXPercent
	{
		get
		{
			float num = -4788f;
			float num2 = 4788f;
			return (this.Cursor.transform.localPosition.x - num) / (num2 - num);
		}
		set
		{
			this.Cursor.transform.localPosition = new Vector3(-4788f + 2f * (4788f * Mathf.Clamp01(value)), this.Cursor.transform.localPosition.y, this.Cursor.transform.localPosition.z);
		}
	}

	// Token: 0x1700045F RID: 1119
	// (get) Token: 0x060018D6 RID: 6358 RVA: 0x000E5AF4 File Offset: 0x000E3CF4
	// (set) Token: 0x060018D7 RID: 6359 RVA: 0x000E5B28 File Offset: 0x000E3D28
	private float CursorYPercent
	{
		get
		{
			float num = -3122f;
			float num2 = 3122f;
			return (this.Cursor.transform.localPosition.y - num) / (num2 - num);
		}
		set
		{
			this.Cursor.transform.localPosition = new Vector3(this.Cursor.transform.localPosition.x, -3122f + 2f * (3122f * Mathf.Clamp01(value)), this.Cursor.transform.localPosition.z);
		}
	}

	// Token: 0x060018D8 RID: 6360 RVA: 0x000E5B8C File Offset: 0x000E3D8C
	private void UpdatePhotoSelection()
	{
		if (Input.GetButtonDown("A"))
		{
			if (!this.NamingBully)
			{
				UITexture uitexture = this.Photographs[this.CurrentIndex];
				if (uitexture.mainTexture != this.NoPhoto)
				{
					this.ViewPhoto.mainTexture = uitexture.mainTexture;
					this.ViewPhoto.transform.position = uitexture.transform.position;
					this.ViewPhoto.transform.localScale = uitexture.transform.localScale;
					this.Destination.position = uitexture.transform.position;
					this.Viewing = true;
					if (!this.Corkboard)
					{
						for (int i = 1; i < 26; i++)
						{
							this.Hearts[i].gameObject.SetActive(false);
						}
					}
					this.CanAdjust = false;
				}
				this.UpdateButtonPrompts();
			}
			else if (this.Photographs[this.CurrentIndex].mainTexture != this.NoPhoto && PlayerGlobals.GetBullyPhoto(this.CurrentIndex) > 0)
			{
				this.Yandere.Police.EndOfDay.FragileTarget = PlayerGlobals.GetBullyPhoto(this.CurrentIndex);
				this.Yandere.StudentManager.FragileOfferHelp.Continue();
				this.PauseScreen.MainMenu.SetActive(true);
				this.Yandere.RPGCamera.enabled = true;
				base.gameObject.SetActive(false);
				this.PauseScreen.Show = false;
				this.PromptBar.Show = false;
				this.NamingBully = false;
				Time.timeScale = 1f;
			}
		}
		if (!this.NamingBully && Input.GetButtonDown("B"))
		{
			this.PromptBar.ClearButtons();
			this.PromptBar.Label[0].text = "Accept";
			this.PromptBar.Label[1].text = "Exit";
			this.PromptBar.Label[4].text = "Choose";
			this.PromptBar.Label[5].text = "Choose";
			this.PromptBar.UpdateButtons();
			this.PauseScreen.MainMenu.SetActive(true);
			this.PauseScreen.Sideways = false;
			this.PauseScreen.PressedB = true;
			base.gameObject.SetActive(false);
			this.UpdateButtonPrompts();
		}
		if (Input.GetButtonDown("X"))
		{
			this.ViewPhoto.mainTexture = null;
			int currentIndex = this.CurrentIndex;
			if (this.Photographs[currentIndex].mainTexture != this.NoPhoto)
			{
				this.Photographs[currentIndex].mainTexture = this.NoPhoto;
				PlayerGlobals.SetPhoto(currentIndex, false);
				PlayerGlobals.SetSenpaiPhoto(currentIndex, false);
				TaskGlobals.SetGuitarPhoto(currentIndex, false);
				TaskGlobals.SetKittenPhoto(currentIndex, false);
				this.Hearts[currentIndex].gameObject.SetActive(false);
				this.TaskManager.UpdateTaskStatus();
			}
			this.UpdateButtonPrompts();
		}
		if (this.Corkboard)
		{
			if (Input.GetButtonDown("Y"))
			{
				this.CanAdjust = false;
				this.Cursor.gameObject.SetActive(true);
				this.Adjusting = true;
				this.UpdateButtonPrompts();
			}
		}
		else if (Input.GetButtonDown("Y") && PlayerGlobals.GetSenpaiPhoto(this.CurrentIndex))
		{
			int currentIndex2 = this.CurrentIndex;
			PlayerGlobals.SetSenpaiPhoto(currentIndex2, false);
			this.Hearts[currentIndex2].gameObject.SetActive(false);
			this.CanAdjust = false;
			this.Yandere.Sanity += 20f;
			this.UpdateButtonPrompts();
			AudioSource.PlayClipAtPoint(this.Sighs[UnityEngine.Random.Range(0, this.Sighs.Length)], this.Yandere.Head.position);
		}
		if (this.InputManager.TappedRight)
		{
			this.Column = ((this.Column < 5) ? (this.Column + 1) : 1);
		}
		if (this.InputManager.TappedLeft)
		{
			this.Column = ((this.Column > 1) ? (this.Column - 1) : 5);
		}
		if (this.InputManager.TappedUp)
		{
			this.Row = ((this.Row > 1) ? (this.Row - 1) : 5);
		}
		if (this.InputManager.TappedDown)
		{
			this.Row = ((this.Row < 5) ? (this.Row + 1) : 1);
		}
		bool flag = this.InputManager.TappedRight || this.InputManager.TappedLeft;
		bool flag2 = this.InputManager.TappedUp || this.InputManager.TappedDown;
		if (flag || flag2)
		{
			this.Highlight.transform.localPosition = new Vector3(this.HighlightX, this.HighlightY, this.Highlight.transform.localPosition.z);
			this.UpdateButtonPrompts();
		}
		this.ViewPhoto.transform.localScale = Vector3.Lerp(this.ViewPhoto.transform.localScale, new Vector3(1f, 1f, 1f), this.LerpSpeed);
		this.ViewPhoto.transform.position = Vector3.Lerp(this.ViewPhoto.transform.position, this.Destination.position, this.LerpSpeed);
		if (this.Corkboard)
		{
			this.Gallery.transform.localPosition = new Vector3(this.Gallery.transform.localPosition.x, Mathf.Lerp(this.Gallery.transform.localPosition.y, 0f, Time.deltaTime * 10f), this.Gallery.transform.localPosition.z);
		}
	}

	// Token: 0x060018D9 RID: 6361 RVA: 0x000E6150 File Offset: 0x000E4350
	private void UpdatePhotoViewing()
	{
		this.ViewPhoto.transform.localScale = Vector3.Lerp(this.ViewPhoto.transform.localScale, this.Corkboard ? new Vector3(5.8f, 5.8f, 5.8f) : new Vector3(6.5f, 6.5f, 6.5f), this.LerpSpeed);
		this.ViewPhoto.transform.localPosition = Vector3.Lerp(this.ViewPhoto.transform.localPosition, Vector3.zero, this.LerpSpeed);
		if (this.Corkboard && Input.GetButtonDown("A"))
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Photograph, base.transform.position, Quaternion.identity);
			gameObject.transform.parent = this.CorkboardPanel;
			gameObject.transform.localEulerAngles = Vector3.zero;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			gameObject.GetComponent<UITexture>().mainTexture = this.Photographs[this.CurrentIndex].mainTexture;
			this.MovingPhotograph = gameObject;
			this.CanAdjust = false;
			this.Adjusting = true;
			this.Viewing = false;
			this.Moving = true;
			this.CorkboardPhotographs[this.Photos] = gameObject.GetComponent<HomeCorkboardPhotoScript>();
			this.CorkboardPhotographs[this.Photos].ID = this.CurrentIndex;
			this.CorkboardPhotographs[this.Photos].ArrayID = this.Photos;
			this.Photos++;
			this.UpdateButtonPrompts();
		}
		if (Input.GetButtonDown("B"))
		{
			this.Viewing = false;
			if (this.Corkboard)
			{
				this.Cursor.Highlight.transform.position = new Vector3(this.Cursor.Highlight.transform.position.x, 100f, this.Cursor.Highlight.transform.position.z);
				this.CanAdjust = true;
			}
			else
			{
				for (int i = 1; i < 26; i++)
				{
					if (PlayerGlobals.GetSenpaiPhoto(i))
					{
						this.Hearts[i].gameObject.SetActive(true);
						this.CanAdjust = true;
					}
				}
			}
			this.UpdateButtonPrompts();
		}
	}

	// Token: 0x060018DA RID: 6362 RVA: 0x000E63B8 File Offset: 0x000E45B8
	private void UpdateCorkboardPhoto()
	{
		if (Input.GetMouseButton(1))
		{
			this.MovingPhotoRotation += this.MouseDelta.x;
		}
		else
		{
			this.MovingPhotograph.transform.localPosition = new Vector3(this.MovingPhotograph.transform.localPosition.x + this.MouseDelta.x * 8.66666f, this.MovingPhotograph.transform.localPosition.y + this.MouseDelta.y * 8.66666f, 0f);
		}
		if (Input.GetButton("LB"))
		{
			this.MovingPhotoRotation += Time.deltaTime * 100f;
		}
		if (Input.GetButton("RB"))
		{
			this.MovingPhotoRotation -= Time.deltaTime * 100f;
		}
		if (Input.GetButton("Y"))
		{
			this.MovingPhotograph.transform.localScale += new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
			if (this.MovingPhotograph.transform.localScale.x > 2f)
			{
				this.MovingPhotograph.transform.localScale = new Vector3(2f, 2f, 2f);
			}
		}
		if (Input.GetButton("X"))
		{
			this.MovingPhotograph.transform.localScale -= new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
			if (this.MovingPhotograph.transform.localScale.x < 1f)
			{
				this.MovingPhotograph.transform.localScale = new Vector3(1f, 1f, 1f);
			}
		}
		Vector2 vector = new Vector2(this.MovingPhotograph.transform.localPosition.x, this.MovingPhotograph.transform.localPosition.y);
		Vector2 vector2 = new Vector2(Input.GetAxis("Horizontal") * 86.66666f * this.SpeedLimit, Input.GetAxis("Vertical") * 86.66666f * this.SpeedLimit);
		this.MovingPhotograph.transform.localPosition = new Vector3(Mathf.Clamp(vector.x + vector2.x, -this.MaxPhotoX, this.MaxPhotoX), Mathf.Clamp(vector.y + vector2.y, -this.MaxPhotoY, this.MaxPhotoY), this.MovingPhotograph.transform.localPosition.z);
		if (Input.GetButtonDown("A"))
		{
			this.Cursor.transform.localPosition = this.MovingPhotograph.transform.localPosition;
			this.Cursor.gameObject.SetActive(true);
			this.Moving = false;
			this.UpdateButtonPrompts();
			this.PhotoID++;
		}
	}

	// Token: 0x060018DB RID: 6363 RVA: 0x000E66B4 File Offset: 0x000E48B4
	private void UpdateString()
	{
		this.MouseDelta.x = this.MouseDelta.x + Input.GetAxis("Horizontal") * 8.66666f * this.SpeedLimit;
		this.MouseDelta.y = this.MouseDelta.y + Input.GetAxis("Vertical") * 8.66666f * this.SpeedLimit;
		Transform transform;
		if (this.StringPhase == 0)
		{
			transform = this.String.Origin;
			this.String.Target.position = this.String.Origin.position;
		}
		else
		{
			transform = this.String.Target;
		}
		transform.localPosition = new Vector3(transform.localPosition.x - this.MouseDelta.x * Time.deltaTime * 0.33333f, transform.localPosition.y + this.MouseDelta.y * Time.deltaTime * 0.33333f, 0f);
		if (transform.localPosition.x > 0.971f)
		{
			transform.localPosition = new Vector3(0.971f, transform.localPosition.y, transform.localPosition.z);
		}
		else if (transform.localPosition.x < -0.971f)
		{
			transform.localPosition = new Vector3(-0.971f, transform.localPosition.y, transform.localPosition.z);
		}
		if (transform.localPosition.y > 0.637f)
		{
			transform.localPosition = new Vector3(transform.localPosition.x, 0.637f, transform.localPosition.z);
		}
		else if (transform.localPosition.y < -0.637f)
		{
			transform.localPosition = new Vector3(transform.localPosition.x, -0.637f, transform.localPosition.z);
		}
		if (Input.GetButtonDown("A"))
		{
			if (this.StringPhase == 0)
			{
				this.StringPhase++;
				return;
			}
			if (this.StringPhase == 1)
			{
				this.Cursor.transform.localPosition = transform.localPosition;
				this.Cursor.gameObject.SetActive(true);
				this.MovingString = false;
				this.StringPhase = 0;
				this.UpdateButtonPrompts();
			}
		}
	}

	// Token: 0x060018DC RID: 6364 RVA: 0x000E68F4 File Offset: 0x000E4AF4
	private void UpdateCorkboardCursor()
	{
		Vector2 vector = new Vector2(this.Cursor.transform.localPosition.x, this.Cursor.transform.localPosition.y);
		Vector2 vector2 = new Vector2(this.MouseDelta.x * 8.66666f + Input.GetAxis("Horizontal") * 86.66666f * this.SpeedLimit, this.MouseDelta.y * 8.66666f + Input.GetAxis("Vertical") * 86.66666f * this.SpeedLimit);
		this.Cursor.transform.localPosition = new Vector3(Mathf.Clamp(vector.x + vector2.x, -4788f, 4788f), Mathf.Clamp(vector.y + vector2.y, -3122f, 3122f), this.Cursor.transform.localPosition.z);
		if (Input.GetButtonDown("A") && this.Cursor.Photograph != null)
		{
			this.Cursor.Highlight.transform.position = new Vector3(this.Cursor.Highlight.transform.position.x, 100f, this.Cursor.Highlight.transform.position.z);
			this.MovingPhotograph = this.Cursor.Photograph;
			this.Cursor.gameObject.SetActive(false);
			this.Moving = true;
			this.UpdateButtonPrompts();
		}
		if (Input.GetButtonDown("Y"))
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.StringSet, base.transform.position, Quaternion.identity);
			gameObject.transform.parent = this.StringParent;
			gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
			gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			this.String = gameObject.GetComponent<StringScript>();
			this.Cursor.gameObject.SetActive(false);
			this.MovingString = true;
			this.CorkboardStrings[this.Strings] = this.String.GetComponent<StringScript>();
			this.CorkboardStrings[this.Strings].ArrayID = this.Strings;
			this.Strings++;
			this.UpdateButtonPrompts();
		}
		if (Input.GetButtonDown("B"))
		{
			if (this.Cursor.Photograph != null)
			{
				this.Cursor.Photograph = null;
			}
			this.Cursor.transform.localPosition = new Vector3(0f, 0f, this.Cursor.transform.localPosition.z);
			this.Cursor.Highlight.transform.position = new Vector3(this.Cursor.Highlight.transform.position.x, 100f, this.Cursor.Highlight.transform.position.z);
			this.CanAdjust = true;
			this.Cursor.gameObject.SetActive(false);
			this.Adjusting = false;
			this.UpdateButtonPrompts();
		}
		if (Input.GetButtonDown("X"))
		{
			if (this.Cursor.Photograph != null)
			{
				this.Cursor.Highlight.transform.position = new Vector3(this.Cursor.Highlight.transform.position.x, 100f, this.Cursor.Highlight.transform.position.z);
				this.Shuffle(this.Cursor.Photograph.GetComponent<HomeCorkboardPhotoScript>().ArrayID);
				UnityEngine.Object.Destroy(this.Cursor.Photograph);
				this.Photos--;
				this.Cursor.Photograph = null;
				this.UpdateButtonPrompts();
			}
			if (this.Cursor.Tack != null)
			{
				this.Cursor.CircleHighlight.transform.position = new Vector3(this.Cursor.CircleHighlight.transform.position.x, 100f, this.Cursor.CircleHighlight.transform.position.z);
				this.ShuffleStrings(this.Cursor.Tack.transform.parent.GetComponent<StringScript>().ArrayID);
				UnityEngine.Object.Destroy(this.Cursor.Tack.transform.parent.gameObject);
				this.Strings--;
				this.Cursor.Tack = null;
				this.UpdateButtonPrompts();
			}
		}
	}

	// Token: 0x060018DD RID: 6365 RVA: 0x000E6DE0 File Offset: 0x000E4FE0
	private void Update()
	{
		if (this.GotPhotos && this.Corkboard && !this.SpawnedPhotos)
		{
			this.SpawnPhotographs();
			this.SpawnStrings();
			base.enabled = false;
			base.gameObject.SetActive(false);
			this.PromptBar.Label[0].text = string.Empty;
			this.PromptBar.Label[1].text = string.Empty;
			this.PromptBar.Label[2].text = string.Empty;
			this.PromptBar.Label[3].text = string.Empty;
			this.PromptBar.Label[4].text = string.Empty;
			this.PromptBar.Label[5].text = string.Empty;
			this.PromptBar.UpdateButtons();
			this.PromptBar.Show = false;
		}
		if (!this.Adjusting)
		{
			if (!this.Viewing)
			{
				this.UpdatePhotoSelection();
			}
			else
			{
				this.UpdatePhotoViewing();
			}
		}
		else
		{
			if (this.Corkboard)
			{
				this.Gallery.transform.localPosition = new Vector3(this.Gallery.transform.localPosition.x, Mathf.Lerp(this.Gallery.transform.localPosition.y, 1000f, Time.deltaTime * 10f), this.Gallery.transform.localPosition.z);
			}
			this.MouseDelta = new Vector2(Input.mousePosition.x - this.PreviousPosition.x, Input.mousePosition.y - this.PreviousPosition.y);
			if (this.InputDevice.Type == InputDeviceType.MouseAndKeyboard)
			{
				this.SpeedLimit = 0.1f;
			}
			else
			{
				this.SpeedLimit = 1f;
			}
			if (this.Moving)
			{
				this.UpdateCorkboardPhoto();
			}
			else if (this.MovingString)
			{
				this.UpdateString();
			}
			else
			{
				this.UpdateCorkboardCursor();
			}
		}
		this.PreviousPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
	}

	// Token: 0x060018DE RID: 6366 RVA: 0x000E7007 File Offset: 0x000E5207
	public IEnumerator GetPhotos()
	{
		if (!this.Corkboard)
		{
			for (int i = 1; i < 26; i++)
			{
				this.Hearts[i].gameObject.SetActive(false);
			}
		}
		int num;
		for (int ID = 1; ID < 26; ID = num + 1)
		{
			if (PlayerGlobals.GetPhoto(ID))
			{
				string url = string.Concat(new object[]
				{
					"file:///",
					Application.streamingAssetsPath,
					"/Photographs/Photo_",
					ID,
					".png"
				});
				WWW www = new WWW(url);
				yield return www;
				if (www.error == null)
				{
					this.Photographs[ID].mainTexture = www.texture;
					if (!this.Corkboard && PlayerGlobals.GetSenpaiPhoto(ID))
					{
						this.Hearts[ID].gameObject.SetActive(true);
					}
				}
				else
				{
					Debug.Log(string.Concat(new object[]
					{
						"Could not retrieve Photo ",
						ID,
						". Maybe it was deleted from Streaming Assets? Setting Photo ",
						ID,
						" to ''false''."
					}));
					PlayerGlobals.SetPhoto(ID, false);
				}
				www = null;
			}
			num = ID;
		}
		this.LoadingScreen.SetActive(false);
		if (!this.Corkboard)
		{
			this.PauseScreen.Sideways = true;
		}
		this.UpdateButtonPrompts();
		base.enabled = true;
		base.gameObject.SetActive(true);
		this.GotPhotos = true;
		yield break;
	}

	// Token: 0x060018DF RID: 6367 RVA: 0x000E7018 File Offset: 0x000E5218
	public void UpdateButtonPrompts()
	{
		if (this.NamingBully)
		{
			if (this.Photographs[this.CurrentIndex].mainTexture != this.NoPhoto && PlayerGlobals.GetBullyPhoto(this.CurrentIndex) > 0)
			{
				if (PlayerGlobals.GetBullyPhoto(this.CurrentIndex) > 0)
				{
					this.PromptBar.Label[0].text = "Name Bully";
				}
				else
				{
					this.PromptBar.Label[0].text = string.Empty;
				}
			}
			else
			{
				this.PromptBar.Label[0].text = string.Empty;
			}
			this.PromptBar.Label[1].text = string.Empty;
			this.PromptBar.Label[2].text = string.Empty;
			this.PromptBar.Label[3].text = string.Empty;
			this.PromptBar.Label[4].text = "Move";
			this.PromptBar.Label[5].text = "Move";
		}
		else if (this.Moving || this.MovingString)
		{
			this.PromptBar.Label[0].text = "Place";
			this.PromptBar.Label[1].text = string.Empty;
			this.PromptBar.Label[2].text = string.Empty;
			this.PromptBar.Label[3].text = string.Empty;
			this.PromptBar.Label[4].text = "Move";
			this.PromptBar.Label[5].text = "Move";
			if (!this.MovingString)
			{
				this.PromptBar.Label[2].text = "Resize";
				this.PromptBar.Label[3].text = "Resize";
			}
		}
		else if (this.Adjusting)
		{
			if (this.Cursor.Photograph != null)
			{
				this.PromptBar.Label[0].text = "Adjust";
				this.PromptBar.Label[1].text = string.Empty;
				this.PromptBar.Label[2].text = "Remove";
				this.PromptBar.Label[3].text = string.Empty;
			}
			else if (this.Cursor.Tack != null)
			{
				this.PromptBar.Label[2].text = "Remove";
			}
			else
			{
				this.PromptBar.Label[0].text = string.Empty;
				this.PromptBar.Label[2].text = string.Empty;
			}
			this.PromptBar.Label[1].text = "Back";
			this.PromptBar.Label[3].text = "Place Pin";
			this.PromptBar.Label[4].text = "Move";
			this.PromptBar.Label[5].text = "Move";
		}
		else if (!this.Viewing)
		{
			int currentIndex = this.CurrentIndex;
			if (this.Photographs[currentIndex].mainTexture != this.NoPhoto)
			{
				this.PromptBar.Label[0].text = "View";
				this.PromptBar.Label[2].text = "Delete";
			}
			else
			{
				this.PromptBar.Label[0].text = string.Empty;
				this.PromptBar.Label[2].text = string.Empty;
			}
			if (!this.Corkboard)
			{
				this.PromptBar.Label[3].text = (PlayerGlobals.GetSenpaiPhoto(currentIndex) ? "Use" : string.Empty);
			}
			else
			{
				this.PromptBar.Label[3].text = "Corkboard";
			}
			this.PromptBar.Label[1].text = "Back";
			this.PromptBar.Label[4].text = "Choose";
			this.PromptBar.Label[5].text = "Choose";
		}
		else
		{
			if (this.Corkboard)
			{
				this.PromptBar.Label[0].text = "Place";
			}
			else
			{
				this.PromptBar.Label[0].text = string.Empty;
			}
			this.PromptBar.Label[2].text = string.Empty;
			this.PromptBar.Label[3].text = string.Empty;
			this.PromptBar.Label[4].text = string.Empty;
			this.PromptBar.Label[5].text = string.Empty;
		}
		this.PromptBar.UpdateButtons();
		this.PromptBar.Show = true;
	}

	// Token: 0x060018E0 RID: 6368 RVA: 0x000E7504 File Offset: 0x000E5704
	private void Shuffle(int Start)
	{
		for (int i = Start; i < this.CorkboardPhotographs.Length - 1; i++)
		{
			if (this.CorkboardPhotographs[i] != null)
			{
				this.CorkboardPhotographs[i].ArrayID--;
				this.CorkboardPhotographs[i] = this.CorkboardPhotographs[i + 1];
			}
		}
	}

	// Token: 0x060018E1 RID: 6369 RVA: 0x000E7560 File Offset: 0x000E5760
	private void ShuffleStrings(int Start)
	{
		for (int i = Start; i < this.CorkboardPhotographs.Length - 1; i++)
		{
			if (this.CorkboardStrings[i] != null)
			{
				this.CorkboardStrings[i].ArrayID--;
				this.CorkboardStrings[i] = this.CorkboardStrings[i + 1];
			}
		}
	}

	// Token: 0x060018E2 RID: 6370 RVA: 0x000E75BC File Offset: 0x000E57BC
	public void SaveAllPhotographs()
	{
		for (int i = 0; i < 100; i++)
		{
			if (this.CorkboardPhotographs[i] != null)
			{
				PlayerPrefs.SetInt(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_Exists"
				}), 1);
				PlayerPrefs.SetInt(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_PhotoID"
				}), this.CorkboardPhotographs[i].ID);
				PlayerPrefs.SetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_PositionX"
				}), this.CorkboardPhotographs[i].transform.localPosition.x);
				PlayerPrefs.SetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_PositionY"
				}), this.CorkboardPhotographs[i].transform.localPosition.y);
				PlayerPrefs.SetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_PositionZ"
				}), this.CorkboardPhotographs[i].transform.localPosition.z);
				PlayerPrefs.SetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_RotationX"
				}), this.CorkboardPhotographs[i].transform.localEulerAngles.x);
				PlayerPrefs.SetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_RotationY"
				}), this.CorkboardPhotographs[i].transform.localEulerAngles.y);
				PlayerPrefs.SetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_RotationZ"
				}), this.CorkboardPhotographs[i].transform.localEulerAngles.z);
				PlayerPrefs.SetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_ScaleX"
				}), this.CorkboardPhotographs[i].transform.localScale.x);
				PlayerPrefs.SetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_ScaleY"
				}), this.CorkboardPhotographs[i].transform.localScale.y);
				PlayerPrefs.SetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_ScaleZ"
				}), this.CorkboardPhotographs[i].transform.localScale.z);
			}
			else
			{
				PlayerPrefs.SetInt(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_Exists"
				}), 0);
			}
		}
	}

	// Token: 0x060018E3 RID: 6371 RVA: 0x000E79B8 File Offset: 0x000E5BB8
	public void SpawnPhotographs()
	{
		for (int i = 0; i < 100; i++)
		{
			if (PlayerPrefs.GetInt(string.Concat(new object[]
			{
				"Profile_",
				GameGlobals.Profile,
				"_CorkboardPhoto_",
				i,
				"_Exists"
			})) == 1)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Photograph, base.transform.position, Quaternion.identity);
				gameObject.transform.parent = this.CorkboardPanel;
				gameObject.transform.localPosition = new Vector3(PlayerPrefs.GetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_PositionX"
				})), PlayerPrefs.GetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_PositionY"
				})), PlayerPrefs.GetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_PositionZ"
				})));
				gameObject.transform.localEulerAngles = new Vector3(PlayerPrefs.GetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_RotationX"
				})), PlayerPrefs.GetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_RotationY"
				})), PlayerPrefs.GetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_RotationZ"
				})));
				gameObject.transform.localScale = new Vector3(PlayerPrefs.GetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_ScaleX"
				})), PlayerPrefs.GetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_ScaleY"
				})), PlayerPrefs.GetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_ScaleZ"
				})));
				gameObject.GetComponent<UITexture>().mainTexture = this.Photographs[PlayerPrefs.GetInt(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_PhotoID"
				}))].mainTexture;
				this.CorkboardPhotographs[this.Photos] = gameObject.GetComponent<HomeCorkboardPhotoScript>();
				this.CorkboardPhotographs[this.Photos].ID = PlayerPrefs.GetInt(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_PhotoID"
				}));
				this.CorkboardPhotographs[this.Photos].ArrayID = this.Photos;
				this.Photos++;
			}
		}
		this.SpawnedPhotos = true;
	}

	// Token: 0x060018E4 RID: 6372 RVA: 0x000E7D8C File Offset: 0x000E5F8C
	public void SaveAllStrings()
	{
		Debug.Log("Saved strings.");
		for (int i = 0; i < 100; i++)
		{
			if (this.CorkboardStrings[i] != null)
			{
				PlayerPrefs.SetInt(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardString_",
					i,
					"_Exists"
				}), 1);
				PlayerPrefs.SetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardString_",
					i,
					"_PositionX"
				}), this.CorkboardStrings[i].Origin.localPosition.x);
				PlayerPrefs.SetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardString_",
					i,
					"_PositionY"
				}), this.CorkboardStrings[i].Origin.localPosition.y);
				PlayerPrefs.SetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardString_",
					i,
					"_PositionZ"
				}), this.CorkboardStrings[i].Origin.localPosition.z);
				PlayerPrefs.SetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardString2_",
					i,
					"_PositionX"
				}), this.CorkboardStrings[i].Target.localPosition.x);
				PlayerPrefs.SetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardString2_",
					i,
					"_PositionY"
				}), this.CorkboardStrings[i].Target.localPosition.y);
				PlayerPrefs.SetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardString2_",
					i,
					"_PositionZ"
				}), this.CorkboardStrings[i].Target.localPosition.z);
			}
			else
			{
				PlayerPrefs.SetInt(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardString_",
					i,
					"_Exists"
				}), 0);
			}
		}
	}

	// Token: 0x060018E5 RID: 6373 RVA: 0x000E8048 File Offset: 0x000E6248
	public void SpawnStrings()
	{
		for (int i = 0; i < 100; i++)
		{
			if (PlayerPrefs.GetInt(string.Concat(new object[]
			{
				"Profile_",
				GameGlobals.Profile,
				"_CorkboardString_",
				i,
				"_Exists"
			})) == 1)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.StringSet, base.transform.position, Quaternion.identity);
				gameObject.transform.parent = this.StringParent;
				gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
				gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				this.String = gameObject.GetComponent<StringScript>();
				this.String.Origin.localPosition = new Vector3(PlayerPrefs.GetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardString_",
					i,
					"_PositionX"
				})), PlayerPrefs.GetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardString_",
					i,
					"_PositionY"
				})), PlayerPrefs.GetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardString_",
					i,
					"_PositionZ"
				})));
				this.String.Target.localPosition = new Vector3(PlayerPrefs.GetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardString2_",
					i,
					"_PositionX"
				})), PlayerPrefs.GetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardString2_",
					i,
					"_PositionY"
				})), PlayerPrefs.GetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardString2_",
					i,
					"_PositionZ"
				})));
				this.CorkboardStrings[this.Strings] = this.String.GetComponent<StringScript>();
				this.CorkboardStrings[this.Strings].ArrayID = this.Strings;
				this.Strings++;
			}
			else
			{
				PlayerPrefs.SetInt(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardString_",
					i,
					"_Exists"
				}), 0);
			}
		}
	}

	// Token: 0x040024F6 RID: 9462
	public HomeCorkboardPhotoScript[] CorkboardPhotographs;

	// Token: 0x040024F7 RID: 9463
	public StringScript[] CorkboardStrings;

	// Token: 0x040024F8 RID: 9464
	public int PhotoID;

	// Token: 0x040024F9 RID: 9465
	public StringScript String;

	// Token: 0x040024FA RID: 9466
	public InputManagerScript InputManager;

	// Token: 0x040024FB RID: 9467
	public PauseScreenScript PauseScreen;

	// Token: 0x040024FC RID: 9468
	public TaskManagerScript TaskManager;

	// Token: 0x040024FD RID: 9469
	public InputDeviceScript InputDevice;

	// Token: 0x040024FE RID: 9470
	public PromptBarScript PromptBar;

	// Token: 0x040024FF RID: 9471
	public HomeCursorScript Cursor;

	// Token: 0x04002500 RID: 9472
	public YandereScript Yandere;

	// Token: 0x04002501 RID: 9473
	public GameObject MovingPhotograph;

	// Token: 0x04002502 RID: 9474
	public GameObject LoadingScreen;

	// Token: 0x04002503 RID: 9475
	public GameObject Photograph;

	// Token: 0x04002504 RID: 9476
	public GameObject StringSet;

	// Token: 0x04002505 RID: 9477
	public Transform CorkboardPanel;

	// Token: 0x04002506 RID: 9478
	public Transform Destination;

	// Token: 0x04002507 RID: 9479
	public Transform Highlight;

	// Token: 0x04002508 RID: 9480
	public Transform Gallery;

	// Token: 0x04002509 RID: 9481
	public Transform StringParent;

	// Token: 0x0400250A RID: 9482
	public UITexture[] Photographs;

	// Token: 0x0400250B RID: 9483
	public UISprite[] Hearts;

	// Token: 0x0400250C RID: 9484
	public AudioClip[] Sighs;

	// Token: 0x0400250D RID: 9485
	public UITexture ViewPhoto;

	// Token: 0x0400250E RID: 9486
	public Texture NoPhoto;

	// Token: 0x0400250F RID: 9487
	public Vector2 PreviousPosition;

	// Token: 0x04002510 RID: 9488
	public Vector2 MouseDelta;

	// Token: 0x04002511 RID: 9489
	public bool DoNotRaisePromptBar;

	// Token: 0x04002512 RID: 9490
	public bool SpawnedPhotos;

	// Token: 0x04002513 RID: 9491
	public bool MovingString;

	// Token: 0x04002514 RID: 9492
	public bool NamingBully;

	// Token: 0x04002515 RID: 9493
	public bool Adjusting;

	// Token: 0x04002516 RID: 9494
	public bool CanAdjust;

	// Token: 0x04002517 RID: 9495
	public bool Corkboard;

	// Token: 0x04002518 RID: 9496
	public bool GotPhotos;

	// Token: 0x04002519 RID: 9497
	public bool Viewing;

	// Token: 0x0400251A RID: 9498
	public bool Moving;

	// Token: 0x0400251B RID: 9499
	public bool Reset;

	// Token: 0x0400251C RID: 9500
	public int StringPhase;

	// Token: 0x0400251D RID: 9501
	public int Strings;

	// Token: 0x0400251E RID: 9502
	public int Photos;

	// Token: 0x0400251F RID: 9503
	public int Column;

	// Token: 0x04002520 RID: 9504
	public int Row;

	// Token: 0x04002521 RID: 9505
	public float MaxPhotoX = 4150f;

	// Token: 0x04002522 RID: 9506
	public float MaxPhotoY = 2500f;

	// Token: 0x04002523 RID: 9507
	private const float MaxCursorX = 4788f;

	// Token: 0x04002524 RID: 9508
	private const float MaxCursorY = 3122f;

	// Token: 0x04002525 RID: 9509
	private const float CorkboardAspectRatio = 1.5336323f;

	// Token: 0x04002526 RID: 9510
	public float SpeedLimit;
}
