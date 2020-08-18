using System;
using UnityEngine;

// Token: 0x0200023F RID: 575
public class ClubWindowScript : MonoBehaviour
{
	// Token: 0x06001268 RID: 4712 RVA: 0x00086490 File Offset: 0x00084690
	private void Start()
	{
		this.Window.SetActive(false);
		if (SchoolGlobals.SchoolAtmosphere <= 0.9f)
		{
			this.ActivityDescs[7] = this.LowAtmosphereDesc;
			return;
		}
		if (SchoolGlobals.SchoolAtmosphere <= 0.8f)
		{
			this.ActivityDescs[7] = this.MedAtmosphereDesc;
		}
	}

	// Token: 0x06001269 RID: 4713 RVA: 0x000864E0 File Offset: 0x000846E0
	private void Update()
	{
		if (this.Window.activeInHierarchy)
		{
			if (this.Timer > 0.5f)
			{
				if (Input.GetButtonDown("A"))
				{
					if (!this.Quitting && !this.Activity)
					{
						ClubGlobals.Club = this.Club;
						this.Yandere.ClubAccessory();
						this.Yandere.TargetStudent.Interaction = StudentInteractionType.ClubJoin;
						this.ClubManager.ActivateClubBenefit();
					}
					else if (this.Quitting)
					{
						this.ClubManager.DeactivateClubBenefit();
						ClubGlobals.SetQuitClub(this.Club, true);
						ClubGlobals.Club = ClubType.None;
						this.Yandere.ClubAccessory();
						this.Yandere.TargetStudent.Interaction = StudentInteractionType.ClubQuit;
						this.Quitting = false;
						this.Yandere.StudentManager.UpdateBooths();
					}
					else if (this.Activity)
					{
						this.Yandere.TargetStudent.Interaction = StudentInteractionType.ClubActivity;
					}
					this.Yandere.TargetStudent.TalkTimer = 100f;
					this.Yandere.TargetStudent.ClubPhase = 2;
					this.PromptBar.ClearButtons();
					this.PromptBar.Show = false;
					this.Window.SetActive(false);
				}
				if (Input.GetButtonDown("B"))
				{
					if (!this.Quitting && !this.Activity)
					{
						this.Yandere.TargetStudent.Interaction = StudentInteractionType.ClubJoin;
					}
					else if (this.Quitting)
					{
						this.Yandere.TargetStudent.Interaction = StudentInteractionType.ClubQuit;
						this.Quitting = false;
					}
					else if (this.Activity)
					{
						this.Yandere.TargetStudent.Interaction = StudentInteractionType.ClubActivity;
						this.Activity = false;
					}
					this.Yandere.TargetStudent.TalkTimer = 100f;
					this.Yandere.TargetStudent.ClubPhase = 3;
					this.PromptBar.ClearButtons();
					this.PromptBar.Show = false;
					this.Window.SetActive(false);
				}
				if (Input.GetButtonDown("X") && !this.Quitting && !this.Activity)
				{
					if (!this.Warning.activeInHierarchy)
					{
						this.ClubInfo.SetActive(false);
						this.Warning.SetActive(true);
					}
					else
					{
						this.ClubInfo.SetActive(true);
						this.Warning.SetActive(false);
					}
				}
			}
			this.Timer += Time.deltaTime;
		}
		if (this.PerformingActivity)
		{
			this.ActivityWindow.localScale = Vector3.Lerp(this.ActivityWindow.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
			return;
		}
		if (this.ActivityWindow.localScale.x > 0.1f)
		{
			this.ActivityWindow.localScale = Vector3.Lerp(this.ActivityWindow.localScale, Vector3.zero, Time.deltaTime * 10f);
			return;
		}
		if (this.ActivityWindow.localScale.x != 0f)
		{
			this.ActivityWindow.localScale = Vector3.zero;
		}
	}

	// Token: 0x0600126A RID: 4714 RVA: 0x000867FC File Offset: 0x000849FC
	public void UpdateWindow()
	{
		this.ClubName.text = this.ClubNames[(int)this.Club];
		if (!this.Quitting && !this.Activity)
		{
			this.ClubDesc.text = this.ClubDescs[(int)this.Club];
			this.PromptBar.ClearButtons();
			this.PromptBar.Label[0].text = "Accept";
			this.PromptBar.Label[1].text = "Refuse";
			this.PromptBar.Label[2].text = "More Info";
			this.PromptBar.UpdateButtons();
			this.PromptBar.Show = true;
			this.BottomLabel.text = "Will you join the " + this.ClubNames[(int)this.Club] + "?";
		}
		else if (this.Activity)
		{
			this.ClubDesc.text = "Club activities last until 6:00 PM. If you choose to participate in club activities now, the day will end.\n\nIf you don't join by 5:30 PM, you won't be able to participate in club activities today.\n\nIf you don't participate in club activities at least once a week, you will be removed from the club.";
			this.PromptBar.ClearButtons();
			this.PromptBar.Label[0].text = "Yes";
			this.PromptBar.Label[1].text = "No";
			this.PromptBar.UpdateButtons();
			this.PromptBar.Show = true;
			this.BottomLabel.text = "Will you participate in club activities?";
		}
		else if (this.Quitting)
		{
			this.ClubDesc.text = "Are you sure you want to quit this club? If you quit, you will never be allowed to return.";
			this.PromptBar.ClearButtons();
			this.PromptBar.Label[0].text = "Confirm";
			this.PromptBar.Label[1].text = "Deny";
			this.PromptBar.UpdateButtons();
			this.PromptBar.Show = true;
			this.BottomLabel.text = "Will you quit the " + this.ClubNames[(int)this.Club] + "?";
		}
		this.ClubInfo.SetActive(true);
		this.Warning.SetActive(false);
		this.Window.SetActive(true);
		this.Timer = 0f;
	}

	// Token: 0x0400163E RID: 5694
	public ClubManagerScript ClubManager;

	// Token: 0x0400163F RID: 5695
	public PromptBarScript PromptBar;

	// Token: 0x04001640 RID: 5696
	public YandereScript Yandere;

	// Token: 0x04001641 RID: 5697
	public Transform ActivityWindow;

	// Token: 0x04001642 RID: 5698
	public GameObject ClubInfo;

	// Token: 0x04001643 RID: 5699
	public GameObject Window;

	// Token: 0x04001644 RID: 5700
	public GameObject Warning;

	// Token: 0x04001645 RID: 5701
	public string[] ActivityDescs;

	// Token: 0x04001646 RID: 5702
	public string[] ClubNames;

	// Token: 0x04001647 RID: 5703
	public string[] ClubDescs;

	// Token: 0x04001648 RID: 5704
	public string MedAtmosphereDesc;

	// Token: 0x04001649 RID: 5705
	public string LowAtmosphereDesc;

	// Token: 0x0400164A RID: 5706
	public UILabel ActivityLabel;

	// Token: 0x0400164B RID: 5707
	public UILabel BottomLabel;

	// Token: 0x0400164C RID: 5708
	public UILabel ClubName;

	// Token: 0x0400164D RID: 5709
	public UILabel ClubDesc;

	// Token: 0x0400164E RID: 5710
	public bool PerformingActivity;

	// Token: 0x0400164F RID: 5711
	public bool Activity;

	// Token: 0x04001650 RID: 5712
	public bool Quitting;

	// Token: 0x04001651 RID: 5713
	public float Timer;

	// Token: 0x04001652 RID: 5714
	public ClubType Club;
}
