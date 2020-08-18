using System;
using UnityEngine;

// Token: 0x02000274 RID: 628
public class DropsScript : MonoBehaviour
{
	// Token: 0x06001369 RID: 4969 RVA: 0x000A6A2C File Offset: 0x000A4C2C
	private void Start()
	{
		this.ID = 1;
		while (this.ID < this.DropNames.Length)
		{
			this.NameLabels[this.ID].text = this.DropNames[this.ID];
			this.ID++;
		}
	}

	// Token: 0x0600136A RID: 4970 RVA: 0x000A6A80 File Offset: 0x000A4C80
	private void Update()
	{
		if (this.InputManager.TappedUp)
		{
			this.Selected--;
			if (this.Selected < 1)
			{
				this.Selected = this.DropNames.Length - 1;
			}
			this.UpdateDesc();
		}
		if (this.InputManager.TappedDown)
		{
			this.Selected++;
			if (this.Selected > this.DropNames.Length - 1)
			{
				this.Selected = 1;
			}
			this.UpdateDesc();
		}
		if (Input.GetButtonDown("A"))
		{
			AudioSource component = base.GetComponent<AudioSource>();
			if (!this.Purchased[this.Selected])
			{
				if (this.PromptBar.Label[0].text != string.Empty)
				{
					if (this.Inventory.PantyShots >= this.DropCosts[this.Selected])
					{
						this.Inventory.PantyShots -= this.DropCosts[this.Selected];
						this.Purchased[this.Selected] = true;
						this.InfoChanWindow.Orders++;
						this.InfoChanWindow.ItemsToDrop[this.InfoChanWindow.Orders] = this.Selected;
						this.InfoChanWindow.DropObject();
						this.UpdateList();
						this.UpdateDesc();
						component.clip = this.InfoPurchase;
						component.Play();
						if (this.Selected == 2)
						{
							SchemeGlobals.SetSchemeStage(3, 2);
							this.Schemes.UpdateInstructions();
						}
					}
				}
				else if (this.Inventory.PantyShots < this.DropCosts[this.Selected])
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

	// Token: 0x0600136B RID: 4971 RVA: 0x000A6CE0 File Offset: 0x000A4EE0
	public void UpdateList()
	{
		this.ID = 1;
		while (this.ID < this.DropNames.Length)
		{
			UILabel uilabel = this.NameLabels[this.ID];
			if (!this.Purchased[this.ID])
			{
				this.CostLabels[this.ID].text = this.DropCosts[this.ID].ToString();
				uilabel.color = new Color(uilabel.color.r, uilabel.color.g, uilabel.color.b, 1f);
			}
			else
			{
				this.CostLabels[this.ID].text = string.Empty;
				uilabel.color = new Color(uilabel.color.r, uilabel.color.g, uilabel.color.b, 0.5f);
			}
			this.ID++;
		}
	}

	// Token: 0x0600136C RID: 4972 RVA: 0x000A6DDC File Offset: 0x000A4FDC
	public void UpdateDesc()
	{
		if (!this.Purchased[this.Selected])
		{
			if (this.Inventory.PantyShots >= this.DropCosts[this.Selected])
			{
				this.PromptBar.Label[0].text = "Purchase";
				this.PromptBar.UpdateButtons();
			}
			else
			{
				this.PromptBar.Label[0].text = string.Empty;
				this.PromptBar.UpdateButtons();
			}
		}
		else
		{
			this.PromptBar.Label[0].text = string.Empty;
			this.PromptBar.UpdateButtons();
		}
		this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 200f - 25f * (float)this.Selected, this.Highlight.localPosition.z);
		this.DropIcon.mainTexture = this.DropIcons[this.Selected];
		this.DropDesc.text = this.DropDescs[this.Selected];
		this.UpdatePantyCount();
	}

	// Token: 0x0600136D RID: 4973 RVA: 0x000A6EF5 File Offset: 0x000A50F5
	public void UpdatePantyCount()
	{
		this.PantyCount.text = this.Inventory.PantyShots.ToString();
	}

	// Token: 0x04001A78 RID: 6776
	public InfoChanWindowScript InfoChanWindow;

	// Token: 0x04001A79 RID: 6777
	public InputManagerScript InputManager;

	// Token: 0x04001A7A RID: 6778
	public InventoryScript Inventory;

	// Token: 0x04001A7B RID: 6779
	public PromptBarScript PromptBar;

	// Token: 0x04001A7C RID: 6780
	public SchemesScript Schemes;

	// Token: 0x04001A7D RID: 6781
	public GameObject FavorMenu;

	// Token: 0x04001A7E RID: 6782
	public Transform Highlight;

	// Token: 0x04001A7F RID: 6783
	public UILabel PantyCount;

	// Token: 0x04001A80 RID: 6784
	public UITexture DropIcon;

	// Token: 0x04001A81 RID: 6785
	public UILabel DropDesc;

	// Token: 0x04001A82 RID: 6786
	public UILabel[] CostLabels;

	// Token: 0x04001A83 RID: 6787
	public UILabel[] NameLabels;

	// Token: 0x04001A84 RID: 6788
	public bool[] Purchased;

	// Token: 0x04001A85 RID: 6789
	public Texture[] DropIcons;

	// Token: 0x04001A86 RID: 6790
	public int[] DropCosts;

	// Token: 0x04001A87 RID: 6791
	public string[] DropDescs;

	// Token: 0x04001A88 RID: 6792
	public string[] DropNames;

	// Token: 0x04001A89 RID: 6793
	public int Selected = 1;

	// Token: 0x04001A8A RID: 6794
	public int ID = 1;

	// Token: 0x04001A8B RID: 6795
	public AudioClip InfoUnavailable;

	// Token: 0x04001A8C RID: 6796
	public AudioClip InfoPurchase;

	// Token: 0x04001A8D RID: 6797
	public AudioClip InfoAfford;
}
