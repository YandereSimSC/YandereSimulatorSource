using System;
using UnityEngine;

// Token: 0x02000433 RID: 1075
public class TutorialWindowScript : MonoBehaviour
{
	// Token: 0x06001C72 RID: 7282 RVA: 0x00153F2C File Offset: 0x0015212C
	private void Start()
	{
		base.transform.localScale = new Vector3(0f, 0f, 0f);
		if (OptionGlobals.TutorialsOff)
		{
			base.enabled = false;
			return;
		}
		this.IgnoreClothing = TutorialGlobals.IgnoreClothing;
		this.IgnoreCouncil = TutorialGlobals.IgnoreCouncil;
		this.IgnoreTeacher = TutorialGlobals.IgnoreTeacher;
		this.IgnoreLocker = TutorialGlobals.IgnoreLocker;
		this.IgnorePolice = TutorialGlobals.IgnorePolice;
		this.IgnoreSanity = TutorialGlobals.IgnoreSanity;
		this.IgnoreSenpai = TutorialGlobals.IgnoreSenpai;
		this.IgnoreVision = TutorialGlobals.IgnoreVision;
		this.IgnoreWeapon = TutorialGlobals.IgnoreWeapon;
		this.IgnoreBlood = TutorialGlobals.IgnoreBlood;
		this.IgnoreClass = TutorialGlobals.IgnoreClass;
		this.IgnorePhoto = TutorialGlobals.IgnorePhoto;
		this.IgnoreClub = TutorialGlobals.IgnoreClub;
		this.IgnoreInfo = TutorialGlobals.IgnoreInfo;
		this.IgnorePool = TutorialGlobals.IgnorePool;
		this.IgnoreRep = TutorialGlobals.IgnoreRep;
	}

	// Token: 0x06001C73 RID: 7283 RVA: 0x00154018 File Offset: 0x00152218
	private void Update()
	{
		if (this.Show)
		{
			base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1.2925f, 1.2925f, 1.2925f), Time.unscaledDeltaTime * 10f);
			if (base.transform.localScale.x > 1f)
			{
				if (Input.GetButtonDown("B"))
				{
					OptionGlobals.TutorialsOff = true;
					this.TitleLabel.text = "Tutorials Disabled";
					this.TutorialLabel.text = this.DisabledString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.DisabledTexture;
					this.ShadowLabel.text = this.TutorialLabel.text;
				}
				else if (Input.GetButtonDown("A"))
				{
					this.Yandere.RPGCamera.enabled = true;
					this.Yandere.Blur.enabled = false;
					Time.timeScale = 1f;
					this.Show = false;
					this.Hide = true;
				}
			}
		}
		else if (this.Hide)
		{
			base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(0f, 0f, 0f), Time.unscaledDeltaTime * 10f);
			if (base.transform.localScale.x < 0.1f)
			{
				base.transform.localScale = new Vector3(0f, 0f, 0f);
				this.Hide = false;
				if (OptionGlobals.TutorialsOff)
				{
					base.enabled = false;
				}
			}
		}
		if (this.Yandere.CanMove && !this.Yandere.Egg && !this.Yandere.Aiming && !this.Yandere.PauseScreen.Show && !this.Yandere.CinematicCamera.activeInHierarchy)
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > 5f)
			{
				if (!this.IgnoreClothing && this.ShowClothingMessage && !this.Show)
				{
					TutorialGlobals.IgnoreClothing = true;
					this.IgnoreClothing = true;
					this.TitleLabel.text = "No Spare Clothing";
					this.TutorialLabel.text = this.ClothingString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.ClothingTexture;
					this.SummonWindow();
				}
				if (!this.IgnoreCouncil && this.ShowCouncilMessage && !this.Show)
				{
					TutorialGlobals.IgnoreCouncil = true;
					this.IgnoreCouncil = true;
					this.TitleLabel.text = "Student Council";
					this.TutorialLabel.text = this.CouncilString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.CouncilTexture;
					this.SummonWindow();
				}
				if (!this.IgnoreTeacher && this.ShowTeacherMessage && !this.Show)
				{
					TutorialGlobals.IgnoreTeacher = true;
					this.IgnoreTeacher = true;
					this.TitleLabel.text = "Teachers";
					this.TutorialLabel.text = this.TeacherString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.TeacherTexture;
					this.SummonWindow();
				}
				if (!this.IgnoreLocker && this.ShowLockerMessage && !this.Show)
				{
					TutorialGlobals.IgnoreLocker = true;
					this.IgnoreLocker = true;
					this.TitleLabel.text = "Notes In Lockers";
					this.TutorialLabel.text = this.LockerString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.LockerTexture;
					this.SummonWindow();
				}
				if (!this.IgnorePolice && this.ShowPoliceMessage && !this.Show)
				{
					TutorialGlobals.IgnorePolice = true;
					this.IgnorePolice = true;
					this.TitleLabel.text = "Police";
					this.TutorialLabel.text = this.PoliceString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.PoliceTexture;
					this.SummonWindow();
				}
				if (!this.IgnoreSanity && this.ShowSanityMessage && !this.Show)
				{
					TutorialGlobals.IgnoreSanity = true;
					this.IgnoreSanity = true;
					this.TitleLabel.text = "Restoring Sanity";
					this.TutorialLabel.text = this.SanityString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.SanityTexture;
					this.SummonWindow();
				}
				if (!this.IgnoreSenpai && this.ShowSenpaiMessage && !this.Show)
				{
					TutorialGlobals.IgnoreSenpai = true;
					this.IgnoreSenpai = true;
					this.TitleLabel.text = "Your Senpai";
					this.TutorialLabel.text = this.SenpaiString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.SenpaiTexture;
					this.SummonWindow();
				}
				if (!this.IgnoreVision)
				{
					if (this.Yandere.StudentManager.WestBathroomArea.bounds.Contains(this.Yandere.transform.position) || this.Yandere.StudentManager.EastBathroomArea.bounds.Contains(this.Yandere.transform.position))
					{
						this.ShowVisionMessage = true;
					}
					if (this.ShowVisionMessage && !this.Show)
					{
						TutorialGlobals.IgnoreVision = true;
						this.IgnoreVision = true;
						this.TitleLabel.text = "Yandere Vision";
						this.TutorialLabel.text = this.VisionString;
						this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
						this.TutorialImage.mainTexture = this.VisionTexture;
						this.SummonWindow();
					}
				}
				if (!this.IgnoreWeapon)
				{
					if (this.Yandere.Armed)
					{
						this.ShowWeaponMessage = true;
					}
					if (this.ShowWeaponMessage && !this.Show)
					{
						TutorialGlobals.IgnoreWeapon = true;
						this.IgnoreWeapon = true;
						this.TitleLabel.text = "Weapons";
						this.TutorialLabel.text = this.WeaponString;
						this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
						this.TutorialImage.mainTexture = this.WeaponTexture;
						this.SummonWindow();
					}
				}
				if (!this.IgnoreBlood && this.ShowBloodMessage && !this.Show)
				{
					TutorialGlobals.IgnoreBlood = true;
					this.IgnoreBlood = true;
					this.TitleLabel.text = "Bloody Clothing";
					this.TutorialLabel.text = this.BloodString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.BloodTexture;
					this.SummonWindow();
				}
				if (!this.IgnoreClass && this.ShowClassMessage && !this.Show)
				{
					TutorialGlobals.IgnoreClass = true;
					this.IgnoreClass = true;
					this.TitleLabel.text = "Attending Class";
					this.TutorialLabel.text = this.ClassString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.ClassTexture;
					this.SummonWindow();
				}
				if (!this.IgnorePhoto)
				{
					if (this.Yandere.transform.position.z > -50f)
					{
						this.ShowPhotoMessage = true;
					}
					if (this.ShowPhotoMessage && !this.Show)
					{
						TutorialGlobals.IgnorePhoto = true;
						this.IgnorePhoto = true;
						this.TitleLabel.text = "Taking Photographs";
						this.TutorialLabel.text = this.PhotoString;
						this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
						this.TutorialImage.mainTexture = this.PhotoTexture;
						this.SummonWindow();
					}
				}
				if (!this.IgnoreClub && this.ShowClubMessage && !this.Show)
				{
					TutorialGlobals.IgnoreClub = true;
					this.IgnoreClub = true;
					this.TitleLabel.text = "Joining Clubs";
					this.TutorialLabel.text = this.ClubString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.ClubTexture;
					this.SummonWindow();
				}
				if (!this.IgnoreInfo && this.ShowInfoMessage && !this.Show)
				{
					TutorialGlobals.IgnoreInfo = true;
					this.IgnoreInfo = true;
					this.TitleLabel.text = "Info-chan's Services";
					this.TutorialLabel.text = this.InfoString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.InfoTexture;
					this.SummonWindow();
				}
				if (!this.IgnorePool && this.ShowPoolMessage && !this.Show)
				{
					TutorialGlobals.IgnorePool = true;
					this.IgnorePool = true;
					this.TitleLabel.text = "Cleaning Blood";
					this.TutorialLabel.text = this.PoolString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.PoolTexture;
					this.SummonWindow();
				}
				if (!this.IgnoreRep && this.ShowRepMessage && !this.Show)
				{
					TutorialGlobals.IgnoreRep = true;
					this.IgnoreRep = true;
					this.TitleLabel.text = "Reputation";
					this.TutorialLabel.text = this.RepString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.RepTexture;
					this.SummonWindow();
				}
			}
		}
	}

	// Token: 0x06001C74 RID: 7284 RVA: 0x00154AC4 File Offset: 0x00152CC4
	public void SummonWindow()
	{
		Debug.Log("Summoning tutorial window.");
		this.ShadowLabel.text = this.TutorialLabel.text;
		this.Yandere.RPGCamera.enabled = false;
		this.Yandere.Blur.enabled = true;
		Time.timeScale = 0f;
		this.Show = true;
		this.Timer = 0f;
	}

	// Token: 0x0400356E RID: 13678
	public YandereScript Yandere;

	// Token: 0x0400356F RID: 13679
	public bool ShowClothingMessage;

	// Token: 0x04003570 RID: 13680
	public bool ShowCouncilMessage;

	// Token: 0x04003571 RID: 13681
	public bool ShowTeacherMessage;

	// Token: 0x04003572 RID: 13682
	public bool ShowLockerMessage;

	// Token: 0x04003573 RID: 13683
	public bool ShowPoliceMessage;

	// Token: 0x04003574 RID: 13684
	public bool ShowSanityMessage;

	// Token: 0x04003575 RID: 13685
	public bool ShowSenpaiMessage;

	// Token: 0x04003576 RID: 13686
	public bool ShowVisionMessage;

	// Token: 0x04003577 RID: 13687
	public bool ShowWeaponMessage;

	// Token: 0x04003578 RID: 13688
	public bool ShowBloodMessage;

	// Token: 0x04003579 RID: 13689
	public bool ShowClassMessage;

	// Token: 0x0400357A RID: 13690
	public bool ShowPhotoMessage;

	// Token: 0x0400357B RID: 13691
	public bool ShowClubMessage;

	// Token: 0x0400357C RID: 13692
	public bool ShowInfoMessage;

	// Token: 0x0400357D RID: 13693
	public bool ShowPoolMessage;

	// Token: 0x0400357E RID: 13694
	public bool ShowRepMessage;

	// Token: 0x0400357F RID: 13695
	public bool IgnoreClothing;

	// Token: 0x04003580 RID: 13696
	public bool IgnoreCouncil;

	// Token: 0x04003581 RID: 13697
	public bool IgnoreTeacher;

	// Token: 0x04003582 RID: 13698
	public bool IgnoreLocker;

	// Token: 0x04003583 RID: 13699
	public bool IgnorePolice;

	// Token: 0x04003584 RID: 13700
	public bool IgnoreSanity;

	// Token: 0x04003585 RID: 13701
	public bool IgnoreSenpai;

	// Token: 0x04003586 RID: 13702
	public bool IgnoreVision;

	// Token: 0x04003587 RID: 13703
	public bool IgnoreWeapon;

	// Token: 0x04003588 RID: 13704
	public bool IgnoreBlood;

	// Token: 0x04003589 RID: 13705
	public bool IgnoreClass;

	// Token: 0x0400358A RID: 13706
	public bool IgnorePhoto;

	// Token: 0x0400358B RID: 13707
	public bool IgnoreClub;

	// Token: 0x0400358C RID: 13708
	public bool IgnoreInfo;

	// Token: 0x0400358D RID: 13709
	public bool IgnorePool;

	// Token: 0x0400358E RID: 13710
	public bool IgnoreRep;

	// Token: 0x0400358F RID: 13711
	public bool Hide;

	// Token: 0x04003590 RID: 13712
	public bool Show;

	// Token: 0x04003591 RID: 13713
	public UILabel TutorialLabel;

	// Token: 0x04003592 RID: 13714
	public UILabel ShadowLabel;

	// Token: 0x04003593 RID: 13715
	public UILabel TitleLabel;

	// Token: 0x04003594 RID: 13716
	public UITexture TutorialImage;

	// Token: 0x04003595 RID: 13717
	public string DisabledString;

	// Token: 0x04003596 RID: 13718
	public Texture DisabledTexture;

	// Token: 0x04003597 RID: 13719
	public string ClothingString;

	// Token: 0x04003598 RID: 13720
	public Texture ClothingTexture;

	// Token: 0x04003599 RID: 13721
	public string CouncilString;

	// Token: 0x0400359A RID: 13722
	public Texture CouncilTexture;

	// Token: 0x0400359B RID: 13723
	public string TeacherString;

	// Token: 0x0400359C RID: 13724
	public Texture TeacherTexture;

	// Token: 0x0400359D RID: 13725
	public string LockerString;

	// Token: 0x0400359E RID: 13726
	public Texture LockerTexture;

	// Token: 0x0400359F RID: 13727
	public string PoliceString;

	// Token: 0x040035A0 RID: 13728
	public Texture PoliceTexture;

	// Token: 0x040035A1 RID: 13729
	public string SanityString;

	// Token: 0x040035A2 RID: 13730
	public Texture SanityTexture;

	// Token: 0x040035A3 RID: 13731
	public string SenpaiString;

	// Token: 0x040035A4 RID: 13732
	public Texture SenpaiTexture;

	// Token: 0x040035A5 RID: 13733
	public string VisionString;

	// Token: 0x040035A6 RID: 13734
	public Texture VisionTexture;

	// Token: 0x040035A7 RID: 13735
	public string WeaponString;

	// Token: 0x040035A8 RID: 13736
	public Texture WeaponTexture;

	// Token: 0x040035A9 RID: 13737
	public string BloodString;

	// Token: 0x040035AA RID: 13738
	public Texture BloodTexture;

	// Token: 0x040035AB RID: 13739
	public string ClassString;

	// Token: 0x040035AC RID: 13740
	public Texture ClassTexture;

	// Token: 0x040035AD RID: 13741
	public string PhotoString;

	// Token: 0x040035AE RID: 13742
	public Texture PhotoTexture;

	// Token: 0x040035AF RID: 13743
	public string ClubString;

	// Token: 0x040035B0 RID: 13744
	public Texture ClubTexture;

	// Token: 0x040035B1 RID: 13745
	public string InfoString;

	// Token: 0x040035B2 RID: 13746
	public Texture InfoTexture;

	// Token: 0x040035B3 RID: 13747
	public string PoolString;

	// Token: 0x040035B4 RID: 13748
	public Texture PoolTexture;

	// Token: 0x040035B5 RID: 13749
	public string RepString;

	// Token: 0x040035B6 RID: 13750
	public Texture RepTexture;

	// Token: 0x040035B7 RID: 13751
	public string PointsString;

	// Token: 0x040035B8 RID: 13752
	public float Timer;
}
