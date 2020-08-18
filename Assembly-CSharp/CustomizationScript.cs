using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000254 RID: 596
public class CustomizationScript : MonoBehaviour
{
	// Token: 0x060012CB RID: 4811 RVA: 0x00096450 File Offset: 0x00094650
	private void Awake()
	{
		this.Data = new CustomizationScript.CustomizationData();
		this.Data.skinColor = new global::RangeInt(3, this.MinSkinColor, this.MaxSkinColor);
		this.Data.hairstyle = new global::RangeInt(1, this.MinHairstyle, this.MaxHairstyle);
		this.Data.hairColor = new global::RangeInt(1, this.MinHairColor, this.MaxHairColor);
		this.Data.eyeColor = new global::RangeInt(1, this.MinEyeColor, this.MaxEyeColor);
		this.Data.eyewear = new global::RangeInt(0, this.MinEyewear, this.MaxEyewear);
		this.Data.facialHair = new global::RangeInt(0, this.MinFacialHair, this.MaxFacialHair);
		this.Data.maleUniform = new global::RangeInt(1, this.MinMaleUniform, this.MaxMaleUniform);
		this.Data.femaleUniform = new global::RangeInt(1, this.MinFemaleUniform, this.MaxFemaleUniform);
	}

	// Token: 0x060012CC RID: 4812 RVA: 0x00096550 File Offset: 0x00094750
	private void Start()
	{
		Time.timeScale = 1f;
		this.LoveSick = GameGlobals.LoveSick;
		this.ApologyWindow.localPosition = new Vector3(1360f, this.ApologyWindow.localPosition.y, this.ApologyWindow.localPosition.z);
		this.CustomizePanel.alpha = 0f;
		this.UniformPanel.alpha = 0f;
		this.FinishPanel.alpha = 0f;
		this.GenderPanel.alpha = 0f;
		this.WhitePanel.alpha = 1f;
		this.UpdateFacialHair(this.Data.facialHair.Value);
		this.UpdateHairStyle(this.Data.hairstyle.Value);
		this.UpdateEyes(this.Data.eyeColor.Value);
		this.UpdateSkin(this.Data.skinColor.Value);
		if (this.LoveSick)
		{
			this.LoveSickColorSwap();
			this.WhitePanel.alpha = 0f;
			this.Data.femaleUniform.Value = 5;
			this.Data.maleUniform.Value = 5;
			RenderSettings.fogColor = new Color(0f, 0f, 0f, 1f);
			this.LoveSickCamera.SetActive(true);
			this.Black.color = Color.black;
			this.MyAudio.loop = false;
			this.MyAudio.clip = this.LoveSickIntro;
			this.MyAudio.Play();
		}
		else
		{
			this.Data.femaleUniform.Value = this.MinFemaleUniform;
			this.Data.maleUniform.Value = this.MinMaleUniform;
			RenderSettings.fogColor = new Color(1f, 0.5f, 1f, 1f);
			this.Black.color = new Color(0f, 0f, 0f, 0f);
			this.LoveSickCamera.SetActive(false);
		}
		this.UpdateMaleUniform(this.Data.maleUniform.Value, this.Data.skinColor.Value);
		this.UpdateFemaleUniform(this.Data.femaleUniform.Value);
		this.Senpai.position = new Vector3(0f, -1f, 2f);
		this.Senpai.gameObject.SetActive(true);
		this.Senpai.GetComponent<Animation>().Play("newWalk_00");
		this.Yandere.position = new Vector3(1f, -1f, 4.5f);
		this.Yandere.gameObject.SetActive(true);
		this.Yandere.GetComponent<Animation>().Play("f02_newWalk_00");
		this.CensorCloud.SetActive(false);
		this.Hearts.SetActive(false);
	}

	// Token: 0x17000346 RID: 838
	// (get) Token: 0x060012CD RID: 4813 RVA: 0x0002291C File Offset: 0x00020B1C
	private int MinSkinColor
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x17000347 RID: 839
	// (get) Token: 0x060012CE RID: 4814 RVA: 0x00096850 File Offset: 0x00094A50
	private int MaxSkinColor
	{
		get
		{
			return 5;
		}
	}

	// Token: 0x17000348 RID: 840
	// (get) Token: 0x060012CF RID: 4815 RVA: 0x0002D171 File Offset: 0x0002B371
	private int MinHairstyle
	{
		get
		{
			return 0;
		}
	}

	// Token: 0x17000349 RID: 841
	// (get) Token: 0x060012D0 RID: 4816 RVA: 0x00096853 File Offset: 0x00094A53
	private int MaxHairstyle
	{
		get
		{
			return this.Hairstyles.Length - 1;
		}
	}

	// Token: 0x1700034A RID: 842
	// (get) Token: 0x060012D1 RID: 4817 RVA: 0x0002291C File Offset: 0x00020B1C
	private int MinHairColor
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x1700034B RID: 843
	// (get) Token: 0x060012D2 RID: 4818 RVA: 0x0009685F File Offset: 0x00094A5F
	private int MaxHairColor
	{
		get
		{
			return CustomizationScript.ColorPairs.Length - 1;
		}
	}

	// Token: 0x1700034C RID: 844
	// (get) Token: 0x060012D3 RID: 4819 RVA: 0x0002291C File Offset: 0x00020B1C
	private int MinEyeColor
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x1700034D RID: 845
	// (get) Token: 0x060012D4 RID: 4820 RVA: 0x0009685F File Offset: 0x00094A5F
	private int MaxEyeColor
	{
		get
		{
			return CustomizationScript.ColorPairs.Length - 1;
		}
	}

	// Token: 0x1700034E RID: 846
	// (get) Token: 0x060012D5 RID: 4821 RVA: 0x0002D171 File Offset: 0x0002B371
	private int MinEyewear
	{
		get
		{
			return 0;
		}
	}

	// Token: 0x1700034F RID: 847
	// (get) Token: 0x060012D6 RID: 4822 RVA: 0x00096850 File Offset: 0x00094A50
	private int MaxEyewear
	{
		get
		{
			return 5;
		}
	}

	// Token: 0x17000350 RID: 848
	// (get) Token: 0x060012D7 RID: 4823 RVA: 0x0002D171 File Offset: 0x0002B371
	private int MinFacialHair
	{
		get
		{
			return 0;
		}
	}

	// Token: 0x17000351 RID: 849
	// (get) Token: 0x060012D8 RID: 4824 RVA: 0x0009686A File Offset: 0x00094A6A
	private int MaxFacialHair
	{
		get
		{
			return this.FacialHairstyles.Length - 1;
		}
	}

	// Token: 0x17000352 RID: 850
	// (get) Token: 0x060012D9 RID: 4825 RVA: 0x0002291C File Offset: 0x00020B1C
	private int MinMaleUniform
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x17000353 RID: 851
	// (get) Token: 0x060012DA RID: 4826 RVA: 0x00096876 File Offset: 0x00094A76
	private int MaxMaleUniform
	{
		get
		{
			return this.MaleUniforms.Length - 1;
		}
	}

	// Token: 0x17000354 RID: 852
	// (get) Token: 0x060012DB RID: 4827 RVA: 0x0002291C File Offset: 0x00020B1C
	private int MinFemaleUniform
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x17000355 RID: 853
	// (get) Token: 0x060012DC RID: 4828 RVA: 0x00096882 File Offset: 0x00094A82
	private int MaxFemaleUniform
	{
		get
		{
			return this.FemaleUniforms.Length - 1;
		}
	}

	// Token: 0x17000356 RID: 854
	// (get) Token: 0x060012DD RID: 4829 RVA: 0x0009688E File Offset: 0x00094A8E
	private float CameraSpeed
	{
		get
		{
			return Time.deltaTime * 10f;
		}
	}

	// Token: 0x060012DE RID: 4830 RVA: 0x0009689C File Offset: 0x00094A9C
	private void Update()
	{
		if (!this.MyAudio.loop && !this.MyAudio.isPlaying)
		{
			this.MyAudio.loop = true;
			this.MyAudio.clip = this.LoveSickLoop;
			this.MyAudio.Play();
		}
		for (int i = 1; i < 3; i++)
		{
			Transform transform = this.Corridor[i];
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + Time.deltaTime * this.ScrollSpeed);
			if (transform.position.z > 36f)
			{
				transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 72f);
			}
		}
		if (this.Phase == 1)
		{
			if (this.WhitePanel.alpha == 0f)
			{
				this.GenderPanel.alpha = Mathf.MoveTowards(this.GenderPanel.alpha, 1f, Time.deltaTime * 2f);
				if (this.GenderPanel.alpha == 1f)
				{
					if (Input.GetButtonDown("A"))
					{
						this.Phase++;
					}
					if (Input.GetButtonDown("B"))
					{
						this.Apologize = true;
					}
					if (Input.GetButtonDown("X"))
					{
						this.White.color = new Color(0f, 0f, 0f, 1f);
						this.FadeOut = true;
						this.Phase = 0;
					}
				}
			}
		}
		else if (this.Phase == 2)
		{
			this.GenderPanel.alpha = Mathf.MoveTowards(this.GenderPanel.alpha, 0f, Time.deltaTime * 2f);
			this.Black.color = new Color(0f, 0f, 0f, Mathf.MoveTowards(this.Black.color.a, 0f, Time.deltaTime * 2f));
			if (this.GenderPanel.alpha == 0f)
			{
				this.Senpai.gameObject.SetActive(true);
				this.Phase++;
			}
		}
		else if (this.Phase == 3)
		{
			this.Adjustment += Input.GetAxis("Mouse X") * Time.deltaTime * 10f;
			if (this.Adjustment > 3f)
			{
				this.Adjustment = 3f;
			}
			else if (this.Adjustment < 0f)
			{
				this.Adjustment = 0f;
			}
			this.CustomizePanel.alpha = Mathf.MoveTowards(this.CustomizePanel.alpha, 1f, Time.deltaTime * 2f);
			if (this.CustomizePanel.alpha == 1f)
			{
				if (Input.GetButtonDown("A"))
				{
					this.Senpai.localEulerAngles = new Vector3(this.Senpai.localEulerAngles.x, 180f, this.Senpai.localEulerAngles.z);
					this.Phase++;
				}
				bool tappedDown = this.InputManager.TappedDown;
				bool tappedUp = this.InputManager.TappedUp;
				if (tappedDown || tappedUp)
				{
					if (tappedDown)
					{
						this.Selected = ((this.Selected >= 6) ? 1 : (this.Selected + 1));
					}
					else if (tappedUp)
					{
						this.Selected = ((this.Selected <= 1) ? 6 : (this.Selected - 1));
					}
					this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 650f - (float)this.Selected * 150f, this.Highlight.localPosition.z);
				}
				if (this.InputManager.TappedRight)
				{
					if (this.Selected == 1)
					{
						this.Data.skinColor.Value = this.Data.skinColor.Next;
						this.UpdateSkin(this.Data.skinColor.Value);
					}
					else if (this.Selected == 2)
					{
						this.Data.hairstyle.Value = this.Data.hairstyle.Next;
						this.UpdateHairStyle(this.Data.hairstyle.Value);
					}
					else if (this.Selected == 3)
					{
						this.Data.hairColor.Value = this.Data.hairColor.Next;
						this.UpdateColor(this.Data.hairColor.Value);
					}
					else if (this.Selected == 4)
					{
						this.Data.eyeColor.Value = this.Data.eyeColor.Next;
						this.UpdateEyes(this.Data.eyeColor.Value);
					}
					else if (this.Selected == 5)
					{
						this.Data.eyewear.Value = this.Data.eyewear.Next;
						this.UpdateEyewear(this.Data.eyewear.Value);
					}
					else if (this.Selected == 6)
					{
						this.Data.facialHair.Value = this.Data.facialHair.Next;
						this.UpdateFacialHair(this.Data.facialHair.Value);
					}
				}
				if (this.InputManager.TappedLeft)
				{
					if (this.Selected == 1)
					{
						this.Data.skinColor.Value = this.Data.skinColor.Previous;
						this.UpdateSkin(this.Data.skinColor.Value);
					}
					else if (this.Selected == 2)
					{
						this.Data.hairstyle.Value = this.Data.hairstyle.Previous;
						this.UpdateHairStyle(this.Data.hairstyle.Value);
					}
					else if (this.Selected == 3)
					{
						this.Data.hairColor.Value = this.Data.hairColor.Previous;
						this.UpdateColor(this.Data.hairColor.Value);
					}
					else if (this.Selected == 4)
					{
						this.Data.eyeColor.Value = this.Data.eyeColor.Previous;
						this.UpdateEyes(this.Data.eyeColor.Value);
					}
					else if (this.Selected == 5)
					{
						this.Data.eyewear.Value = this.Data.eyewear.Previous;
						this.UpdateEyewear(this.Data.eyewear.Value);
					}
					else if (this.Selected == 6)
					{
						this.Data.facialHair.Value = this.Data.facialHair.Previous;
						this.UpdateFacialHair(this.Data.facialHair.Value);
					}
				}
			}
			this.Rotation = Mathf.Lerp(this.Rotation, 45f - this.Adjustment * 30f, Time.deltaTime * 10f);
			this.AbsoluteRotation = 45f - Mathf.Abs(this.Rotation);
			if (this.Selected == 1)
			{
				this.LoveSickCamera.transform.position = new Vector3(Mathf.Lerp(this.LoveSickCamera.transform.position.x, -1.5f + this.Adjustment, this.CameraSpeed), Mathf.Lerp(this.LoveSickCamera.transform.position.y, 0f, this.CameraSpeed), Mathf.Lerp(this.LoveSickCamera.transform.position.z, 0.5f - this.AbsoluteRotation * 0.015f, this.CameraSpeed));
			}
			else
			{
				this.LoveSickCamera.transform.position = new Vector3(Mathf.Lerp(this.LoveSickCamera.transform.position.x, -0.5f + this.Adjustment * 0.33333f, this.CameraSpeed), Mathf.Lerp(this.LoveSickCamera.transform.position.y, 0.5f, this.CameraSpeed), Mathf.Lerp(this.LoveSickCamera.transform.position.z, 1.5f - this.AbsoluteRotation * 0.015f * 0.33333f, this.CameraSpeed));
			}
			this.LoveSickCamera.transform.eulerAngles = new Vector3(0f, this.Rotation, 0f);
			base.transform.eulerAngles = this.LoveSickCamera.transform.eulerAngles;
			base.transform.position = this.LoveSickCamera.transform.position;
		}
		else if (this.Phase == 4)
		{
			this.Adjustment = Mathf.Lerp(this.Adjustment, 0f, Time.deltaTime * 10f);
			this.Rotation = Mathf.Lerp(this.Rotation, 45f, Time.deltaTime * 10f);
			this.AbsoluteRotation = 45f - Mathf.Abs(this.Rotation);
			this.LoveSickCamera.transform.position = new Vector3(Mathf.Lerp(this.LoveSickCamera.transform.position.x, -1.5f + this.Adjustment, this.CameraSpeed), Mathf.Lerp(this.LoveSickCamera.transform.position.y, 0f, this.CameraSpeed), Mathf.Lerp(this.LoveSickCamera.transform.position.z, 0.5f - this.AbsoluteRotation * 0.015f, this.CameraSpeed));
			this.LoveSickCamera.transform.eulerAngles = new Vector3(0f, this.Rotation, 0f);
			base.transform.eulerAngles = this.LoveSickCamera.transform.eulerAngles;
			base.transform.position = this.LoveSickCamera.transform.position;
			this.CustomizePanel.alpha = Mathf.MoveTowards(this.CustomizePanel.alpha, 0f, Time.deltaTime * 2f);
			if (this.CustomizePanel.alpha == 0f)
			{
				this.Phase++;
			}
		}
		else if (this.Phase == 5)
		{
			this.FinishPanel.alpha = Mathf.MoveTowards(this.FinishPanel.alpha, 1f, Time.deltaTime * 2f);
			if (this.FinishPanel.alpha == 1f)
			{
				if (Input.GetButtonDown("A"))
				{
					this.Phase++;
				}
				if (Input.GetButtonDown("B"))
				{
					this.Selected = 1;
					this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 650f - (float)this.Selected * 150f, this.Highlight.localPosition.z);
					this.Phase = 100;
				}
			}
		}
		else if (this.Phase == 6)
		{
			this.FinishPanel.alpha = Mathf.MoveTowards(this.FinishPanel.alpha, 0f, Time.deltaTime * 2f);
			if (this.FinishPanel.alpha == 0f)
			{
				this.UpdateFemaleUniform(this.Data.femaleUniform.Value);
				this.UpdateMaleUniform(this.Data.maleUniform.Value, this.Data.skinColor.Value);
				this.CensorCloud.SetActive(false);
				this.Yandere.gameObject.SetActive(true);
				this.Selected = 1;
				this.Phase++;
			}
		}
		else if (this.Phase == 7)
		{
			this.UniformPanel.alpha = Mathf.MoveTowards(this.UniformPanel.alpha, 1f, Time.deltaTime * 2f);
			if (this.UniformPanel.alpha == 1f)
			{
				if (Input.GetButtonDown("A"))
				{
					this.Yandere.localEulerAngles = new Vector3(this.Yandere.localEulerAngles.x, 180f, this.Yandere.localEulerAngles.z);
					this.Senpai.localEulerAngles = new Vector3(this.Senpai.localEulerAngles.x, 180f, this.Senpai.localEulerAngles.z);
					this.Phase++;
				}
				if (this.InputManager.TappedDown || this.InputManager.TappedUp)
				{
					this.Selected = ((this.Selected == 1) ? 2 : 1);
					this.UniformHighlight.localPosition = new Vector3(this.UniformHighlight.localPosition.x, 650f - (float)this.Selected * 150f, this.UniformHighlight.localPosition.z);
				}
				if (this.InputManager.TappedRight)
				{
					if (this.Selected == 1)
					{
						this.Data.maleUniform.Value = this.Data.maleUniform.Next;
						this.UpdateMaleUniform(this.Data.maleUniform.Value, this.Data.skinColor.Value);
					}
					else if (this.Selected == 2)
					{
						this.Data.femaleUniform.Value = this.Data.femaleUniform.Next;
						this.UpdateFemaleUniform(this.Data.femaleUniform.Value);
					}
				}
				if (this.InputManager.TappedLeft)
				{
					if (this.Selected == 1)
					{
						this.Data.maleUniform.Value = this.Data.maleUniform.Previous;
						this.UpdateMaleUniform(this.Data.maleUniform.Value, this.Data.skinColor.Value);
					}
					else if (this.Selected == 2)
					{
						this.Data.femaleUniform.Value = this.Data.femaleUniform.Previous;
						this.UpdateFemaleUniform(this.Data.femaleUniform.Value);
					}
				}
			}
		}
		else if (this.Phase == 8)
		{
			this.UniformPanel.alpha = Mathf.MoveTowards(this.UniformPanel.alpha, 0f, Time.deltaTime * 2f);
			if (this.UniformPanel.alpha == 0f)
			{
				this.Phase++;
			}
		}
		else if (this.Phase == 9)
		{
			this.FinishPanel.alpha = Mathf.MoveTowards(this.FinishPanel.alpha, 1f, Time.deltaTime * 2f);
			if (this.FinishPanel.alpha == 1f)
			{
				if (Input.GetButtonDown("A"))
				{
					this.Phase++;
				}
				if (Input.GetButtonDown("B"))
				{
					this.Selected = 1;
					this.UniformHighlight.localPosition = new Vector3(this.UniformHighlight.localPosition.x, 650f - (float)this.Selected * 150f, this.UniformHighlight.localPosition.z);
					this.Phase = 99;
				}
			}
		}
		else if (this.Phase == 10)
		{
			this.FinishPanel.alpha = Mathf.MoveTowards(this.FinishPanel.alpha, 0f, Time.deltaTime * 2f);
			if (this.FinishPanel.alpha == 0f)
			{
				this.White.color = new Color(0f, 0f, 0f, 1f);
				this.FadeOut = true;
				this.Phase = 0;
			}
		}
		else if (this.Phase == 99)
		{
			this.FinishPanel.alpha = Mathf.MoveTowards(this.FinishPanel.alpha, 0f, Time.deltaTime * 2f);
			if (this.FinishPanel.alpha == 0f)
			{
				this.Phase = 7;
			}
		}
		else if (this.Phase == 100)
		{
			this.FinishPanel.alpha = Mathf.MoveTowards(this.FinishPanel.alpha, 0f, Time.deltaTime * 2f);
			if (this.FinishPanel.alpha == 0f)
			{
				this.Phase = 3;
			}
		}
		if (this.Phase > 3 && this.Phase < 100)
		{
			if (this.Phase < 6)
			{
				this.LoveSickCamera.transform.position = new Vector3(Mathf.Lerp(this.LoveSickCamera.transform.position.x, -1.5f, this.CameraSpeed), Mathf.Lerp(this.LoveSickCamera.transform.position.y, 0f, this.CameraSpeed), Mathf.Lerp(this.LoveSickCamera.transform.position.z, 0.5f, this.CameraSpeed));
				base.transform.position = this.LoveSickCamera.transform.position;
			}
			else
			{
				this.LoveSickCamera.transform.position = new Vector3(Mathf.Lerp(this.LoveSickCamera.transform.position.x, 0f, this.CameraSpeed), Mathf.Lerp(this.LoveSickCamera.transform.position.y, 0.5f, this.CameraSpeed), Mathf.Lerp(this.LoveSickCamera.transform.position.z, 0f, this.CameraSpeed));
				this.LoveSickCamera.transform.eulerAngles = new Vector3(Mathf.Lerp(this.LoveSickCamera.transform.eulerAngles.x, 15f, this.CameraSpeed), Mathf.Lerp(this.LoveSickCamera.transform.eulerAngles.y, 0f, this.CameraSpeed), Mathf.Lerp(this.LoveSickCamera.transform.eulerAngles.z, 0f, this.CameraSpeed));
				base.transform.eulerAngles = this.LoveSickCamera.transform.eulerAngles;
				base.transform.position = this.LoveSickCamera.transform.position;
				this.Yandere.position = new Vector3(Mathf.Lerp(this.Yandere.position.x, 1f, Time.deltaTime * 10f), Mathf.Lerp(this.Yandere.position.y, -1f, Time.deltaTime * 10f), Mathf.Lerp(this.Yandere.position.z, 3.5f, Time.deltaTime * 10f));
			}
		}
		if (this.FadeOut)
		{
			this.WhitePanel.alpha = Mathf.MoveTowards(this.WhitePanel.alpha, 1f, Time.deltaTime);
			base.GetComponent<AudioSource>().volume = 1f - this.WhitePanel.alpha;
			if (this.WhitePanel.alpha == 1f)
			{
				SenpaiGlobals.CustomSenpai = true;
				SenpaiGlobals.SenpaiSkinColor = this.Data.skinColor.Value;
				SenpaiGlobals.SenpaiHairStyle = this.Data.hairstyle.Value;
				SenpaiGlobals.SenpaiHairColor = this.HairColorName;
				SenpaiGlobals.SenpaiEyeColor = this.EyeColorName;
				SenpaiGlobals.SenpaiEyeWear = this.Data.eyewear.Value;
				SenpaiGlobals.SenpaiFacialHair = this.Data.facialHair.Value;
				StudentGlobals.MaleUniform = this.Data.maleUniform.Value;
				StudentGlobals.FemaleUniform = this.Data.femaleUniform.Value;
				SceneManager.LoadScene("NewIntroScene");
			}
		}
		else
		{
			this.WhitePanel.alpha = Mathf.MoveTowards(this.WhitePanel.alpha, 0f, Time.deltaTime);
		}
		if (this.Apologize)
		{
			this.Timer += Time.deltaTime;
			if (this.Timer < 1f)
			{
				this.ApologyWindow.localPosition = new Vector3(Mathf.Lerp(this.ApologyWindow.localPosition.x, 0f, Time.deltaTime * 10f), this.ApologyWindow.localPosition.y, this.ApologyWindow.localPosition.z);
				return;
			}
			this.ApologyWindow.localPosition = new Vector3(Mathf.Abs((this.ApologyWindow.localPosition.x - Time.deltaTime) * 0.01f) * (Time.deltaTime * 1000f), this.ApologyWindow.localPosition.y, this.ApologyWindow.localPosition.z);
			if (this.ApologyWindow.localPosition.x < -1360f)
			{
				this.ApologyWindow.localPosition = new Vector3(1360f, this.ApologyWindow.localPosition.y, this.ApologyWindow.localPosition.z);
				this.Apologize = false;
				this.Timer = 0f;
			}
		}
	}

	// Token: 0x060012DF RID: 4831 RVA: 0x00097EBF File Offset: 0x000960BF
	private void LateUpdate()
	{
		this.YandereHead.LookAt(this.SenpaiHead.position);
	}

	// Token: 0x060012E0 RID: 4832 RVA: 0x00097ED7 File Offset: 0x000960D7
	private void UpdateSkin(int skinColor)
	{
		this.UpdateMaleUniform(this.Data.maleUniform.Value, skinColor);
		this.SkinColorLabel.text = "Skin Color " + skinColor.ToString();
	}

	// Token: 0x060012E1 RID: 4833 RVA: 0x00097F0C File Offset: 0x0009610C
	private void UpdateHairStyle(int hairstyle)
	{
		for (int i = 1; i < this.Hairstyles.Length; i++)
		{
			this.Hairstyles[i].SetActive(false);
		}
		if (hairstyle > 0)
		{
			this.HairRenderer = this.Hairstyles[hairstyle].GetComponent<Renderer>();
			this.Hairstyles[hairstyle].SetActive(true);
		}
		this.HairStyleLabel.text = "Hair Style " + hairstyle.ToString();
		this.UpdateColor(this.Data.hairColor.Value);
	}

	// Token: 0x060012E2 RID: 4834 RVA: 0x00097F94 File Offset: 0x00096194
	private void UpdateFacialHair(int facialHair)
	{
		for (int i = 1; i < this.FacialHairstyles.Length; i++)
		{
			this.FacialHairstyles[i].SetActive(false);
		}
		if (facialHair > 0)
		{
			this.FacialHairRenderer = this.FacialHairstyles[facialHair].GetComponent<Renderer>();
			this.FacialHairstyles[facialHair].SetActive(true);
		}
		this.FacialHairStyleLabel.text = "Facial Hair " + facialHair.ToString();
		this.UpdateColor(this.Data.hairColor.Value);
	}

	// Token: 0x060012E3 RID: 4835 RVA: 0x0009801C File Offset: 0x0009621C
	private void UpdateColor(int hairColor)
	{
		KeyValuePair<Color, string> keyValuePair = CustomizationScript.ColorPairs[hairColor];
		Color key = keyValuePair.Key;
		this.HairColorName = keyValuePair.Value;
		if (this.Data.hairstyle.Value > 0)
		{
			this.HairRenderer = this.Hairstyles[this.Data.hairstyle.Value].GetComponent<Renderer>();
			this.HairRenderer.material.color = key;
		}
		if (this.Data.facialHair.Value > 0)
		{
			this.FacialHairRenderer = this.FacialHairstyles[this.Data.facialHair.Value].GetComponent<Renderer>();
			this.FacialHairRenderer.material.color = key;
			if (this.FacialHairRenderer.materials.Length > 1)
			{
				this.FacialHairRenderer.materials[1].color = key;
			}
		}
		this.HairColorLabel.text = "Hair Color " + hairColor.ToString();
	}

	// Token: 0x060012E4 RID: 4836 RVA: 0x00098118 File Offset: 0x00096318
	private void UpdateEyes(int eyeColor)
	{
		KeyValuePair<Color, string> keyValuePair = CustomizationScript.ColorPairs[eyeColor];
		Color key = keyValuePair.Key;
		this.EyeColorName = keyValuePair.Value;
		this.EyeR.material.color = key;
		this.EyeL.material.color = key;
		this.EyeColorLabel.text = "Eye Color " + eyeColor.ToString();
	}

	// Token: 0x060012E5 RID: 4837 RVA: 0x00098184 File Offset: 0x00096384
	private void UpdateEyewear(int eyewear)
	{
		for (int i = 1; i < this.Eyewears.Length; i++)
		{
			this.Eyewears[i].SetActive(false);
		}
		if (eyewear > 0)
		{
			this.Eyewears[eyewear].SetActive(true);
		}
		this.EyeWearLabel.text = "Eye Wear " + eyewear.ToString();
	}

	// Token: 0x060012E6 RID: 4838 RVA: 0x000981E0 File Offset: 0x000963E0
	private void UpdateMaleUniform(int maleUniform, int skinColor)
	{
		this.SenpaiRenderer.sharedMesh = this.MaleUniforms[maleUniform];
		if (maleUniform == 1)
		{
			this.SenpaiRenderer.materials[0].mainTexture = this.SkinTextures[skinColor];
			this.SenpaiRenderer.materials[1].mainTexture = this.MaleUniformTextures[maleUniform];
			this.SenpaiRenderer.materials[2].mainTexture = this.FaceTextures[skinColor];
		}
		else if (maleUniform == 2)
		{
			this.SenpaiRenderer.materials[0].mainTexture = this.MaleUniformTextures[maleUniform];
			this.SenpaiRenderer.materials[1].mainTexture = this.FaceTextures[skinColor];
			this.SenpaiRenderer.materials[2].mainTexture = this.SkinTextures[skinColor];
		}
		else if (maleUniform == 3)
		{
			this.SenpaiRenderer.materials[0].mainTexture = this.MaleUniformTextures[maleUniform];
			this.SenpaiRenderer.materials[1].mainTexture = this.FaceTextures[skinColor];
			this.SenpaiRenderer.materials[2].mainTexture = this.SkinTextures[skinColor];
		}
		else if (maleUniform == 4)
		{
			this.SenpaiRenderer.materials[0].mainTexture = this.FaceTextures[skinColor];
			this.SenpaiRenderer.materials[1].mainTexture = this.SkinTextures[skinColor];
			this.SenpaiRenderer.materials[2].mainTexture = this.MaleUniformTextures[maleUniform];
		}
		else if (maleUniform == 5)
		{
			this.SenpaiRenderer.materials[0].mainTexture = this.FaceTextures[skinColor];
			this.SenpaiRenderer.materials[1].mainTexture = this.SkinTextures[skinColor];
			this.SenpaiRenderer.materials[2].mainTexture = this.MaleUniformTextures[maleUniform];
		}
		else if (maleUniform == 6)
		{
			this.SenpaiRenderer.materials[0].mainTexture = this.FaceTextures[skinColor];
			this.SenpaiRenderer.materials[1].mainTexture = this.SkinTextures[skinColor];
			this.SenpaiRenderer.materials[2].mainTexture = this.MaleUniformTextures[maleUniform];
		}
		this.MaleUniformLabel.text = "Male Uniform " + maleUniform.ToString();
	}

	// Token: 0x060012E7 RID: 4839 RVA: 0x00098420 File Offset: 0x00096620
	private void UpdateFemaleUniform(int femaleUniform)
	{
		this.YandereRenderer.sharedMesh = this.FemaleUniforms[femaleUniform];
		this.YandereRenderer.materials[0].mainTexture = this.FemaleUniformTextures[femaleUniform];
		this.YandereRenderer.materials[1].mainTexture = this.FemaleUniformTextures[femaleUniform];
		this.YandereRenderer.materials[2].mainTexture = this.FemaleFace;
		this.YandereRenderer.materials[3].mainTexture = this.FemaleFace;
		this.FemaleUniformLabel.text = "Female Uniform " + femaleUniform.ToString();
	}

	// Token: 0x060012E8 RID: 4840 RVA: 0x000984C0 File Offset: 0x000966C0
	private void LoveSickColorSwap()
	{
		foreach (GameObject gameObject in UnityEngine.Object.FindObjectsOfType<GameObject>())
		{
			UISprite component = gameObject.GetComponent<UISprite>();
			if (component != null && component.color != Color.black && component.transform.parent != this.Highlight && component.transform.parent != this.UniformHighlight)
			{
				component.color = new Color(1f, 0f, 0f, component.color.a);
			}
			UITexture component2 = gameObject.GetComponent<UITexture>();
			if (component2 != null)
			{
				component2.color = new Color(1f, 0f, 0f, component2.color.a);
			}
			UILabel component3 = gameObject.GetComponent<UILabel>();
			if (component3 != null && component3.color != Color.black)
			{
				component3.color = new Color(1f, 0f, 0f, component3.color.a);
			}
		}
	}

	// Token: 0x0400186E RID: 6254
	[SerializeField]
	private CustomizationScript.CustomizationData Data;

	// Token: 0x0400186F RID: 6255
	[SerializeField]
	private InputManagerScript InputManager;

	// Token: 0x04001870 RID: 6256
	[SerializeField]
	private Renderer FacialHairRenderer;

	// Token: 0x04001871 RID: 6257
	[SerializeField]
	private SkinnedMeshRenderer YandereRenderer;

	// Token: 0x04001872 RID: 6258
	[SerializeField]
	private SkinnedMeshRenderer SenpaiRenderer;

	// Token: 0x04001873 RID: 6259
	[SerializeField]
	private Renderer HairRenderer;

	// Token: 0x04001874 RID: 6260
	[SerializeField]
	private AudioSource MyAudio;

	// Token: 0x04001875 RID: 6261
	[SerializeField]
	private Renderer EyeR;

	// Token: 0x04001876 RID: 6262
	[SerializeField]
	private Renderer EyeL;

	// Token: 0x04001877 RID: 6263
	[SerializeField]
	private Transform UniformHighlight;

	// Token: 0x04001878 RID: 6264
	[SerializeField]
	private Transform ApologyWindow;

	// Token: 0x04001879 RID: 6265
	[SerializeField]
	private Transform YandereHead;

	// Token: 0x0400187A RID: 6266
	[SerializeField]
	private Transform YandereNeck;

	// Token: 0x0400187B RID: 6267
	[SerializeField]
	private Transform SenpaiHead;

	// Token: 0x0400187C RID: 6268
	[SerializeField]
	private Transform Highlight;

	// Token: 0x0400187D RID: 6269
	[SerializeField]
	private Transform Yandere;

	// Token: 0x0400187E RID: 6270
	[SerializeField]
	private Transform Senpai;

	// Token: 0x0400187F RID: 6271
	[SerializeField]
	private Transform[] Corridor;

	// Token: 0x04001880 RID: 6272
	[SerializeField]
	private UIPanel CustomizePanel;

	// Token: 0x04001881 RID: 6273
	[SerializeField]
	private UIPanel UniformPanel;

	// Token: 0x04001882 RID: 6274
	[SerializeField]
	private UIPanel FinishPanel;

	// Token: 0x04001883 RID: 6275
	[SerializeField]
	private UIPanel GenderPanel;

	// Token: 0x04001884 RID: 6276
	[SerializeField]
	private UIPanel WhitePanel;

	// Token: 0x04001885 RID: 6277
	[SerializeField]
	private UILabel FacialHairStyleLabel;

	// Token: 0x04001886 RID: 6278
	[SerializeField]
	private UILabel FemaleUniformLabel;

	// Token: 0x04001887 RID: 6279
	[SerializeField]
	private UILabel MaleUniformLabel;

	// Token: 0x04001888 RID: 6280
	[SerializeField]
	private UILabel SkinColorLabel;

	// Token: 0x04001889 RID: 6281
	[SerializeField]
	private UILabel HairStyleLabel;

	// Token: 0x0400188A RID: 6282
	[SerializeField]
	private UILabel HairColorLabel;

	// Token: 0x0400188B RID: 6283
	[SerializeField]
	private UILabel EyeColorLabel;

	// Token: 0x0400188C RID: 6284
	[SerializeField]
	private UILabel EyeWearLabel;

	// Token: 0x0400188D RID: 6285
	[SerializeField]
	private GameObject LoveSickCamera;

	// Token: 0x0400188E RID: 6286
	[SerializeField]
	private GameObject CensorCloud;

	// Token: 0x0400188F RID: 6287
	[SerializeField]
	private GameObject BigCloud;

	// Token: 0x04001890 RID: 6288
	[SerializeField]
	private GameObject Hearts;

	// Token: 0x04001891 RID: 6289
	[SerializeField]
	private GameObject Cloud;

	// Token: 0x04001892 RID: 6290
	[SerializeField]
	private UISprite Black;

	// Token: 0x04001893 RID: 6291
	[SerializeField]
	private UISprite White;

	// Token: 0x04001894 RID: 6292
	[SerializeField]
	private bool Apologize;

	// Token: 0x04001895 RID: 6293
	[SerializeField]
	private bool LoveSick;

	// Token: 0x04001896 RID: 6294
	[SerializeField]
	private bool FadeOut;

	// Token: 0x04001897 RID: 6295
	[SerializeField]
	private float ScrollSpeed;

	// Token: 0x04001898 RID: 6296
	[SerializeField]
	private float Timer;

	// Token: 0x04001899 RID: 6297
	[SerializeField]
	private int Selected = 1;

	// Token: 0x0400189A RID: 6298
	[SerializeField]
	private int Phase = 1;

	// Token: 0x0400189B RID: 6299
	[SerializeField]
	private Texture[] FemaleUniformTextures;

	// Token: 0x0400189C RID: 6300
	[SerializeField]
	private Texture[] MaleUniformTextures;

	// Token: 0x0400189D RID: 6301
	[SerializeField]
	private Texture[] FaceTextures;

	// Token: 0x0400189E RID: 6302
	[SerializeField]
	private Texture[] SkinTextures;

	// Token: 0x0400189F RID: 6303
	[SerializeField]
	private GameObject[] FacialHairstyles;

	// Token: 0x040018A0 RID: 6304
	[SerializeField]
	private GameObject[] Hairstyles;

	// Token: 0x040018A1 RID: 6305
	[SerializeField]
	private GameObject[] Eyewears;

	// Token: 0x040018A2 RID: 6306
	[SerializeField]
	private Mesh[] FemaleUniforms;

	// Token: 0x040018A3 RID: 6307
	[SerializeField]
	private Mesh[] MaleUniforms;

	// Token: 0x040018A4 RID: 6308
	[SerializeField]
	private Texture FemaleFace;

	// Token: 0x040018A5 RID: 6309
	[SerializeField]
	private string HairColorName = string.Empty;

	// Token: 0x040018A6 RID: 6310
	[SerializeField]
	private string EyeColorName = string.Empty;

	// Token: 0x040018A7 RID: 6311
	[SerializeField]
	private AudioClip LoveSickIntro;

	// Token: 0x040018A8 RID: 6312
	[SerializeField]
	private AudioClip LoveSickLoop;

	// Token: 0x040018A9 RID: 6313
	public float AbsoluteRotation;

	// Token: 0x040018AA RID: 6314
	public float Adjustment;

	// Token: 0x040018AB RID: 6315
	public float Rotation;

	// Token: 0x040018AC RID: 6316
	private static readonly KeyValuePair<Color, string>[] ColorPairs = new KeyValuePair<Color, string>[]
	{
		new KeyValuePair<Color, string>(default(Color), string.Empty),
		new KeyValuePair<Color, string>(new Color(0.5f, 0.5f, 0.5f), "Black"),
		new KeyValuePair<Color, string>(new Color(1f, 0f, 0f), "Red"),
		new KeyValuePair<Color, string>(new Color(1f, 1f, 0f), "Yellow"),
		new KeyValuePair<Color, string>(new Color(0f, 1f, 0f), "Green"),
		new KeyValuePair<Color, string>(new Color(0f, 1f, 1f), "Cyan"),
		new KeyValuePair<Color, string>(new Color(0f, 0f, 1f), "Blue"),
		new KeyValuePair<Color, string>(new Color(1f, 0f, 1f), "Purple"),
		new KeyValuePair<Color, string>(new Color(1f, 0.5f, 0f), "Orange"),
		new KeyValuePair<Color, string>(new Color(0.5f, 0.25f, 0f), "Brown"),
		new KeyValuePair<Color, string>(new Color(1f, 1f, 1f), "White")
	};

	// Token: 0x0200069C RID: 1692
	private class CustomizationData
	{
		// Token: 0x04004665 RID: 18021
		public global::RangeInt skinColor;

		// Token: 0x04004666 RID: 18022
		public global::RangeInt hairstyle;

		// Token: 0x04004667 RID: 18023
		public global::RangeInt hairColor;

		// Token: 0x04004668 RID: 18024
		public global::RangeInt eyeColor;

		// Token: 0x04004669 RID: 18025
		public global::RangeInt eyewear;

		// Token: 0x0400466A RID: 18026
		public global::RangeInt facialHair;

		// Token: 0x0400466B RID: 18027
		public global::RangeInt maleUniform;

		// Token: 0x0400466C RID: 18028
		public global::RangeInt femaleUniform;
	}
}
