using System;
using UnityEngine;

// Token: 0x020002F4 RID: 756
public class HomeSenpaiShrineScript : MonoBehaviour
{
	// Token: 0x0600173C RID: 5948 RVA: 0x000C7E70 File Offset: 0x000C6070
	private void Start()
	{
		this.UpdateText(this.GetCurrentIndex());
		for (int i = 1; i < 11; i++)
		{
			if (PlayerGlobals.GetShrineCollectible(i))
			{
				this.Collectibles[i].SetActive(true);
			}
		}
	}

	// Token: 0x0600173D RID: 5949 RVA: 0x000C7EAC File Offset: 0x000C60AC
	private bool InUpperHalf()
	{
		return this.Y < 2;
	}

	// Token: 0x0600173E RID: 5950 RVA: 0x000C7EB7 File Offset: 0x000C60B7
	private int GetCurrentIndex()
	{
		if (this.InUpperHalf())
		{
			return this.Y;
		}
		return 2 + (this.X + (this.Y - 2) * this.Columns);
	}

	// Token: 0x0600173F RID: 5951 RVA: 0x000C7EE0 File Offset: 0x000C60E0
	private void Update()
	{
		if (!this.HomeYandere.CanMove && !this.PauseScreen.Show)
		{
			if (this.HomeCamera.ID == 6)
			{
				this.Rotation = Mathf.Lerp(this.Rotation, 135f, Time.deltaTime * 10f);
				this.RightDoor.localEulerAngles = new Vector3(this.RightDoor.localEulerAngles.x, this.Rotation, this.RightDoor.localEulerAngles.z);
				this.LeftDoor.localEulerAngles = new Vector3(this.LeftDoor.localEulerAngles.x, -this.Rotation, this.LeftDoor.localEulerAngles.z);
				if (this.InputManager.TappedUp)
				{
					this.Y = ((this.Y > 0) ? (this.Y - 1) : (this.Rows - 1));
				}
				if (this.InputManager.TappedDown)
				{
					this.Y = ((this.Y < this.Rows - 1) ? (this.Y + 1) : 0);
				}
				if (this.InputManager.TappedRight && !this.InUpperHalf())
				{
					this.X = ((this.X < this.Columns - 1) ? (this.X + 1) : 0);
				}
				if (this.InputManager.TappedLeft && !this.InUpperHalf())
				{
					this.X = ((this.X > 0) ? (this.X - 1) : (this.Columns - 1));
				}
				if (this.InUpperHalf())
				{
					this.X = 1;
				}
				int currentIndex = this.GetCurrentIndex();
				this.HomeCamera.Destination = this.Destinations[currentIndex];
				this.HomeCamera.Target = this.Targets[currentIndex];
				if (this.InputManager.TappedUp || this.InputManager.TappedDown || this.InputManager.TappedRight || this.InputManager.TappedLeft)
				{
					this.UpdateText(currentIndex - 1);
				}
				if (Input.GetButtonDown("B"))
				{
					this.HomeCamera.Destination = this.HomeCamera.Destinations[0];
					this.HomeCamera.Target = this.HomeCamera.Targets[0];
					this.HomeYandere.CanMove = true;
					this.HomeYandere.gameObject.SetActive(true);
					this.HomeWindow.Show = false;
					return;
				}
			}
		}
		else
		{
			this.Rotation = Mathf.Lerp(this.Rotation, 0f, Time.deltaTime * 10f);
			this.RightDoor.localEulerAngles = new Vector3(this.RightDoor.localEulerAngles.x, this.Rotation, this.RightDoor.localEulerAngles.z);
			this.LeftDoor.localEulerAngles = new Vector3(this.LeftDoor.localEulerAngles.x, this.Rotation, this.LeftDoor.localEulerAngles.z);
		}
	}

	// Token: 0x06001740 RID: 5952 RVA: 0x000C81E8 File Offset: 0x000C63E8
	private void UpdateText(int newIndex)
	{
		if (newIndex == -1)
		{
			newIndex = 10;
		}
		if (newIndex == 0)
		{
			this.NameLabel.text = this.Names[newIndex];
			this.DescLabel.text = this.Descs[newIndex];
			return;
		}
		if (PlayerGlobals.GetShrineCollectible(newIndex))
		{
			this.NameLabel.text = this.Names[newIndex];
			this.DescLabel.text = this.Descs[newIndex];
			return;
		}
		this.NameLabel.text = "???";
		this.DescLabel.text = "I'd like to find something that Senpai touched and keep it here...";
	}

	// Token: 0x04002015 RID: 8213
	public InputManagerScript InputManager;

	// Token: 0x04002016 RID: 8214
	public PauseScreenScript PauseScreen;

	// Token: 0x04002017 RID: 8215
	public HomeYandereScript HomeYandere;

	// Token: 0x04002018 RID: 8216
	public HomeCameraScript HomeCamera;

	// Token: 0x04002019 RID: 8217
	public HomeWindowScript HomeWindow;

	// Token: 0x0400201A RID: 8218
	public GameObject[] Collectibles;

	// Token: 0x0400201B RID: 8219
	public Transform[] Destinations;

	// Token: 0x0400201C RID: 8220
	public Transform[] Targets;

	// Token: 0x0400201D RID: 8221
	public Transform RightDoor;

	// Token: 0x0400201E RID: 8222
	public Transform LeftDoor;

	// Token: 0x0400201F RID: 8223
	public UILabel NameLabel;

	// Token: 0x04002020 RID: 8224
	public UILabel DescLabel;

	// Token: 0x04002021 RID: 8225
	public string[] Names;

	// Token: 0x04002022 RID: 8226
	public string[] Descs;

	// Token: 0x04002023 RID: 8227
	public float Rotation;

	// Token: 0x04002024 RID: 8228
	private int Rows = 5;

	// Token: 0x04002025 RID: 8229
	private int Columns = 3;

	// Token: 0x04002026 RID: 8230
	private int X = 1;

	// Token: 0x04002027 RID: 8231
	private int Y = 3;
}
