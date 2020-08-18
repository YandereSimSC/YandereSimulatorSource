using System;
using UnityEngine;

// Token: 0x020003D3 RID: 979
public class ServicesScript : MonoBehaviour
{
	// Token: 0x06001A60 RID: 6752 RVA: 0x00101F94 File Offset: 0x00100194
	private void Start()
	{
		for (int i = 1; i < this.ServiceNames.Length; i++)
		{
			SchemeGlobals.SetServicePurchased(i, false);
			this.NameLabels[i].text = this.ServiceNames[i];
		}
	}

	// Token: 0x06001A61 RID: 6753 RVA: 0x00101FD0 File Offset: 0x001001D0
	private void Update()
	{
		if (this.InputManager.TappedUp)
		{
			this.Selected--;
			if (this.Selected < 1)
			{
				this.Selected = this.ServiceNames.Length - 1;
			}
			this.UpdateDesc();
		}
		if (this.InputManager.TappedDown)
		{
			this.Selected++;
			if (this.Selected > this.ServiceNames.Length - 1)
			{
				this.Selected = 1;
			}
			this.UpdateDesc();
		}
		AudioSource component = base.GetComponent<AudioSource>();
		if (Input.GetButtonDown("A"))
		{
			if (!SchemeGlobals.GetServicePurchased(this.Selected) && (double)this.NameLabels[this.Selected].color.a == 1.0)
			{
				if (this.PromptBar.Label[0].text != string.Empty)
				{
					if (this.Inventory.PantyShots >= this.ServiceCosts[this.Selected])
					{
						if (this.Selected == 1)
						{
							this.Yandere.PauseScreen.StudentInfoMenu.GettingInfo = true;
							this.Yandere.PauseScreen.StudentInfoMenu.gameObject.SetActive(true);
							base.StartCoroutine(this.Yandere.PauseScreen.StudentInfoMenu.UpdatePortraits());
							this.Yandere.PauseScreen.StudentInfoMenu.Column = 0;
							this.Yandere.PauseScreen.StudentInfoMenu.Row = 0;
							this.Yandere.PauseScreen.StudentInfoMenu.UpdateHighlight();
							this.Yandere.PauseScreen.Sideways = true;
							this.Yandere.PromptBar.ClearButtons();
							this.Yandere.PromptBar.Label[1].text = "Cancel";
							this.Yandere.PromptBar.UpdateButtons();
							this.Yandere.PromptBar.Show = true;
							base.gameObject.SetActive(false);
						}
						if (this.Selected == 2)
						{
							this.Reputation.PendingRep += 5f;
							this.Purchase();
						}
						else if (this.Selected == 3)
						{
							StudentGlobals.SetStudentReputation(this.StudentManager.RivalID, StudentGlobals.GetStudentReputation(this.StudentManager.RivalID) - 5);
							this.Purchase();
						}
						else if (this.Selected == 4)
						{
							SchemeGlobals.SetServicePurchased(this.Selected, true);
							SchemeGlobals.DarkSecret = true;
							this.Purchase();
						}
						else if (this.Selected == 5)
						{
							this.Yandere.PauseScreen.StudentInfoMenu.SendingHome = true;
							this.Yandere.PauseScreen.StudentInfoMenu.gameObject.SetActive(true);
							base.StartCoroutine(this.Yandere.PauseScreen.StudentInfoMenu.UpdatePortraits());
							this.Yandere.PauseScreen.StudentInfoMenu.Column = 0;
							this.Yandere.PauseScreen.StudentInfoMenu.Row = 0;
							this.Yandere.PauseScreen.StudentInfoMenu.UpdateHighlight();
							this.Yandere.PauseScreen.Sideways = true;
							this.Yandere.PromptBar.ClearButtons();
							this.Yandere.PromptBar.Label[1].text = "Cancel";
							this.Yandere.PromptBar.UpdateButtons();
							this.Yandere.PromptBar.Show = true;
							base.gameObject.SetActive(false);
						}
						else if (this.Selected == 6)
						{
							this.Police.Timer += 300f;
							this.Police.Delayed = true;
							this.Purchase();
						}
						else if (this.Selected == 7)
						{
							SchemeGlobals.SetServicePurchased(this.Selected, true);
							CounselorGlobals.CounselorTape = 1;
							this.Purchase();
						}
						else if (this.Selected == 8)
						{
							SchemeGlobals.SetServicePurchased(this.Selected, true);
							for (int i = 1; i < 26; i++)
							{
								ConversationGlobals.SetTopicLearnedByStudent(i, 11, true);
							}
							this.Purchase();
						}
					}
				}
				else if (this.Inventory.PantyShots < this.ServiceCosts[this.Selected])
				{
					component.clip = this.InfoAfford;
					component.Play();
				}
				else
				{
					component.clip = this.InfoUnavailable;
					component.Play();
				}
			}
			else
			{
				component.clip = this.InfoUnavailable;
				component.Play();
			}
		}
		if (Input.GetButtonDown("B"))
		{
			this.PromptBar.ClearButtons();
			this.PromptBar.Label[0].text = "Accept";
			this.PromptBar.Label[1].text = "Exit";
			this.PromptBar.Label[5].text = "Choose";
			this.PromptBar.UpdateButtons();
			this.FavorMenu.SetActive(true);
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001A62 RID: 6754 RVA: 0x001024D4 File Offset: 0x001006D4
	public void UpdateList()
	{
		this.ID = 1;
		while (this.ID < this.ServiceNames.Length)
		{
			this.CostLabels[this.ID].text = this.ServiceCosts[this.ID].ToString();
			bool servicePurchased = SchemeGlobals.GetServicePurchased(this.ID);
			this.ServiceAvailable[this.ID] = false;
			if (this.ID == 1 || this.ID == 2 || this.ID == 3)
			{
				this.ServiceAvailable[this.ID] = true;
			}
			else if (this.ID == 4)
			{
				if (!SchemeGlobals.DarkSecret)
				{
					this.ServiceAvailable[this.ID] = true;
				}
			}
			else if (this.ID == 5)
			{
				if (!this.ServicePurchased[this.ID])
				{
					this.ServiceAvailable[this.ID] = true;
				}
			}
			else if (this.ID == 6)
			{
				if (this.Police.Show && !this.Police.Delayed)
				{
					this.ServiceAvailable[this.ID] = true;
				}
			}
			else if (this.ID == 7)
			{
				if (CounselorGlobals.CounselorTape == 0)
				{
					this.ServiceAvailable[this.ID] = true;
				}
			}
			else if (this.ID == 8 && !SchemeGlobals.GetServicePurchased(8))
			{
				this.ServiceAvailable[this.ID] = true;
			}
			UILabel uilabel = this.NameLabels[this.ID];
			uilabel.color = new Color(uilabel.color.r, uilabel.color.g, uilabel.color.b, (this.ServiceAvailable[this.ID] && !servicePurchased) ? 1f : 0.5f);
			this.ID++;
		}
	}

	// Token: 0x06001A63 RID: 6755 RVA: 0x0010269C File Offset: 0x0010089C
	public void UpdateDesc()
	{
		if (this.ServiceAvailable[this.Selected] && !SchemeGlobals.GetServicePurchased(this.Selected))
		{
			this.PromptBar.Label[0].text = ((this.Inventory.PantyShots >= this.ServiceCosts[this.Selected]) ? "Purchase" : string.Empty);
			this.PromptBar.UpdateButtons();
		}
		else
		{
			this.PromptBar.Label[0].text = string.Empty;
			this.PromptBar.UpdateButtons();
		}
		this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 200f - 25f * (float)this.Selected, this.Highlight.localPosition.z);
		this.ServiceIcon.mainTexture = this.ServiceIcons[this.Selected];
		this.ServiceLimit.text = this.ServiceLimits[this.Selected];
		this.ServiceDesc.text = this.ServiceDescs[this.Selected];
		if (this.Selected == 5)
		{
			this.ServiceDesc.text = this.ServiceDescs[this.Selected] + "\n\nIf student portraits don't appear, back out of the menu, load the Student Info menu, then return to this screen.";
		}
		this.UpdatePantyCount();
	}

	// Token: 0x06001A64 RID: 6756 RVA: 0x001027E8 File Offset: 0x001009E8
	public void UpdatePantyCount()
	{
		this.PantyCount.text = this.Inventory.PantyShots.ToString();
	}

	// Token: 0x06001A65 RID: 6757 RVA: 0x00102808 File Offset: 0x00100A08
	public void Purchase()
	{
		this.ServicePurchased[this.Selected] = true;
		this.TextMessageManager.SpawnMessage(this.Selected);
		this.Inventory.PantyShots -= this.ServiceCosts[this.Selected];
		AudioSource.PlayClipAtPoint(this.InfoPurchase, base.transform.position);
		this.UpdateList();
		this.UpdateDesc();
		this.PromptBar.Label[0].text = string.Empty;
		this.PromptBar.Label[1].text = "Back";
		this.PromptBar.UpdateButtons();
	}

	// Token: 0x040029C4 RID: 10692
	public TextMessageManagerScript TextMessageManager;

	// Token: 0x040029C5 RID: 10693
	public StudentManagerScript StudentManager;

	// Token: 0x040029C6 RID: 10694
	public InputManagerScript InputManager;

	// Token: 0x040029C7 RID: 10695
	public ReputationScript Reputation;

	// Token: 0x040029C8 RID: 10696
	public InventoryScript Inventory;

	// Token: 0x040029C9 RID: 10697
	public PromptBarScript PromptBar;

	// Token: 0x040029CA RID: 10698
	public SchemesScript Schemes;

	// Token: 0x040029CB RID: 10699
	public YandereScript Yandere;

	// Token: 0x040029CC RID: 10700
	public GameObject FavorMenu;

	// Token: 0x040029CD RID: 10701
	public Transform Highlight;

	// Token: 0x040029CE RID: 10702
	public PoliceScript Police;

	// Token: 0x040029CF RID: 10703
	public UITexture ServiceIcon;

	// Token: 0x040029D0 RID: 10704
	public UILabel ServiceLimit;

	// Token: 0x040029D1 RID: 10705
	public UILabel ServiceDesc;

	// Token: 0x040029D2 RID: 10706
	public UILabel PantyCount;

	// Token: 0x040029D3 RID: 10707
	public UILabel[] CostLabels;

	// Token: 0x040029D4 RID: 10708
	public UILabel[] NameLabels;

	// Token: 0x040029D5 RID: 10709
	public Texture[] ServiceIcons;

	// Token: 0x040029D6 RID: 10710
	public string[] ServiceLimits;

	// Token: 0x040029D7 RID: 10711
	public string[] ServiceDescs;

	// Token: 0x040029D8 RID: 10712
	public string[] ServiceNames;

	// Token: 0x040029D9 RID: 10713
	public bool[] ServiceAvailable;

	// Token: 0x040029DA RID: 10714
	public bool[] ServicePurchased;

	// Token: 0x040029DB RID: 10715
	public int[] ServiceCosts;

	// Token: 0x040029DC RID: 10716
	public int Selected = 1;

	// Token: 0x040029DD RID: 10717
	public int ID = 1;

	// Token: 0x040029DE RID: 10718
	public AudioClip InfoUnavailable;

	// Token: 0x040029DF RID: 10719
	public AudioClip InfoPurchase;

	// Token: 0x040029E0 RID: 10720
	public AudioClip InfoAfford;
}
