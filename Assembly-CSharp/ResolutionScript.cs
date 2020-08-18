using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200038E RID: 910
public class ResolutionScript : MonoBehaviour
{
	// Token: 0x06001998 RID: 6552 RVA: 0x000F9E74 File Offset: 0x000F8074
	private void Start()
	{
		this.Darkness.color = new Color(1f, 1f, 1f, 1f);
		Screen.fullScreen = false;
		Screen.SetResolution(1280, 720, false);
		this.ResolutionLabel.text = Screen.width + " x " + Screen.height;
		this.QualityLabel.text = (this.Qualities[QualitySettings.GetQualityLevel()] ?? "");
		this.FullScreenLabel.text = "No";
	}

	// Token: 0x06001999 RID: 6553 RVA: 0x000F9F14 File Offset: 0x000F8114
	private void Update()
	{
		if (this.FadeOut)
		{
			this.Alpha = Mathf.MoveTowards(this.Alpha, 1f, Time.deltaTime);
			if (this.Alpha == 1f)
			{
				SceneManager.LoadScene("WelcomeScene");
			}
		}
		else
		{
			this.Alpha = Mathf.MoveTowards(this.Alpha, 0f, Time.deltaTime);
		}
		this.Darkness.color = new Color(1f, 1f, 1f, this.Alpha);
		if (this.Alpha == 0f)
		{
			if (this.InputManager.TappedDown)
			{
				this.ID++;
				this.UpdateHighlight();
			}
			if (this.InputManager.TappedUp)
			{
				this.ID--;
				this.UpdateHighlight();
			}
			if (this.ID == 1)
			{
				if (this.InputManager.TappedRight)
				{
					this.ResID++;
					if (this.ResID == this.Widths.Length)
					{
						this.ResID = 0;
					}
					this.UpdateRes();
				}
				else if (this.InputManager.TappedLeft)
				{
					this.ResID--;
					if (this.ResID < 0)
					{
						this.ResID = this.Widths.Length - 1;
					}
					this.UpdateRes();
				}
			}
			else if (this.ID == 2)
			{
				if (this.InputManager.TappedRight || this.InputManager.TappedLeft)
				{
					this.FullScreen = !this.FullScreen;
					if (this.FullScreen)
					{
						this.FullScreenLabel.text = "Yes";
					}
					else
					{
						this.FullScreenLabel.text = "No";
					}
					Screen.SetResolution(Screen.width, Screen.height, this.FullScreen);
				}
			}
			else if (this.ID == 3)
			{
				if (this.InputManager.TappedRight)
				{
					this.QualityID++;
					if (this.QualityID == this.Qualities.Length)
					{
						this.QualityID = 0;
					}
					this.UpdateQuality();
				}
				else if (this.InputManager.TappedLeft)
				{
					this.QualityID--;
					if (this.QualityID < 0)
					{
						this.QualityID = this.Qualities.Length - 1;
					}
					this.UpdateQuality();
				}
			}
			else if (this.ID == 4 && Input.GetButtonUp("A"))
			{
				this.FadeOut = true;
			}
		}
		this.Highlight.localPosition = Vector3.Lerp(this.Highlight.localPosition, new Vector3(-307.5f, (float)(250 - this.ID * 100), 0f), Time.deltaTime * 10f);
	}

	// Token: 0x0600199A RID: 6554 RVA: 0x000FA1D0 File Offset: 0x000F83D0
	private void UpdateRes()
	{
		Screen.SetResolution(this.Widths[this.ResID], this.Heights[this.ResID], Screen.fullScreen);
		this.ResolutionLabel.text = this.Widths[this.ResID] + " x " + this.Heights[this.ResID];
	}

	// Token: 0x0600199B RID: 6555 RVA: 0x000FA23A File Offset: 0x000F843A
	private void UpdateQuality()
	{
		QualitySettings.SetQualityLevel(this.QualityID, true);
		this.QualityLabel.text = (this.Qualities[this.QualityID] ?? "");
	}

	// Token: 0x0600199C RID: 6556 RVA: 0x000FA269 File Offset: 0x000F8469
	private void UpdateHighlight()
	{
		if (this.ID < 1)
		{
			this.ID = 4;
			return;
		}
		if (this.ID > 4)
		{
			this.ID = 1;
		}
	}

	// Token: 0x04002780 RID: 10112
	public InputManagerScript InputManager;

	// Token: 0x04002781 RID: 10113
	public UILabel ResolutionLabel;

	// Token: 0x04002782 RID: 10114
	public UILabel FullScreenLabel;

	// Token: 0x04002783 RID: 10115
	public UILabel QualityLabel;

	// Token: 0x04002784 RID: 10116
	public Transform Highlight;

	// Token: 0x04002785 RID: 10117
	public UISprite Darkness;

	// Token: 0x04002786 RID: 10118
	public float Alpha = 1f;

	// Token: 0x04002787 RID: 10119
	public bool FullScreen;

	// Token: 0x04002788 RID: 10120
	public bool FadeOut;

	// Token: 0x04002789 RID: 10121
	public string[] Qualities;

	// Token: 0x0400278A RID: 10122
	public int[] Widths;

	// Token: 0x0400278B RID: 10123
	public int[] Heights;

	// Token: 0x0400278C RID: 10124
	public int QualityID;

	// Token: 0x0400278D RID: 10125
	public int ResID = 1;

	// Token: 0x0400278E RID: 10126
	public int ID = 1;
}
