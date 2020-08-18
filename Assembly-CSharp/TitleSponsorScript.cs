using System;
using UnityEngine;

// Token: 0x02000426 RID: 1062
public class TitleSponsorScript : MonoBehaviour
{
	// Token: 0x06001C43 RID: 7235 RVA: 0x00151E08 File Offset: 0x00150008
	private void Start()
	{
		base.transform.localPosition = new Vector3(1050f, base.transform.localPosition.y, base.transform.localPosition.z);
		this.UpdateHighlight();
		if (GameGlobals.LoveSick)
		{
			this.TurnLoveSick();
		}
	}

	// Token: 0x06001C44 RID: 7236 RVA: 0x00151E5D File Offset: 0x0015005D
	public int GetSponsorIndex()
	{
		return this.Column + this.Row * this.Columns;
	}

	// Token: 0x06001C45 RID: 7237 RVA: 0x00151E73 File Offset: 0x00150073
	public bool SponsorHasWebsite(int index)
	{
		return !string.IsNullOrEmpty(this.SponsorURLs[index]);
	}

	// Token: 0x06001C46 RID: 7238 RVA: 0x00151E88 File Offset: 0x00150088
	private void Update()
	{
		if (!this.Show)
		{
			base.transform.localPosition = new Vector3(Mathf.Lerp(base.transform.localPosition.x, 1050f, Time.deltaTime * 10f), base.transform.localPosition.y, base.transform.localPosition.z);
			return;
		}
		base.transform.localPosition = new Vector3(Mathf.Lerp(base.transform.localPosition.x, 0f, Time.deltaTime * 10f), base.transform.localPosition.y, base.transform.localPosition.z);
		if (this.InputManager.TappedUp)
		{
			this.Row = ((this.Row > 0) ? (this.Row - 1) : (this.Rows - 1));
		}
		if (this.InputManager.TappedDown)
		{
			this.Row = ((this.Row < this.Rows - 1) ? (this.Row + 1) : 0);
		}
		if (this.InputManager.TappedRight)
		{
			this.Column = ((this.Column < this.Columns - 1) ? (this.Column + 1) : 0);
		}
		if (this.InputManager.TappedLeft)
		{
			this.Column = ((this.Column > 0) ? (this.Column - 1) : (this.Columns - 1));
		}
		if (this.InputManager.TappedUp || this.InputManager.TappedDown || this.InputManager.TappedRight || this.InputManager.TappedLeft)
		{
			this.UpdateHighlight();
		}
		if (Input.GetButtonDown("A"))
		{
			int sponsorIndex = this.GetSponsorIndex();
			if (this.SponsorHasWebsite(sponsorIndex))
			{
				Application.OpenURL(this.SponsorURLs[sponsorIndex]);
			}
		}
	}

	// Token: 0x06001C47 RID: 7239 RVA: 0x00152068 File Offset: 0x00150268
	private void UpdateHighlight()
	{
		this.Highlight.localPosition = new Vector3(-384f + (float)this.Column * 256f, 128f - (float)this.Row * 256f, this.Highlight.localPosition.z);
		this.SponsorName.text = this.Sponsors[this.GetSponsorIndex()];
	}

	// Token: 0x06001C48 RID: 7240 RVA: 0x001520D4 File Offset: 0x001502D4
	private void TurnLoveSick()
	{
		this.BlackSprite.color = Color.black;
		foreach (UISprite uisprite in this.RedSprites)
		{
			uisprite.color = new Color(1f, 0f, 0f, uisprite.color.a);
		}
		foreach (UILabel uilabel in this.Labels)
		{
			uilabel.color = new Color(1f, 0f, 0f, uilabel.color.a);
		}
	}

	// Token: 0x04003505 RID: 13573
	public InputManagerScript InputManager;

	// Token: 0x04003506 RID: 13574
	public string[] SponsorURLs;

	// Token: 0x04003507 RID: 13575
	public string[] Sponsors;

	// Token: 0x04003508 RID: 13576
	public UILabel SponsorName;

	// Token: 0x04003509 RID: 13577
	public Transform Highlight;

	// Token: 0x0400350A RID: 13578
	public bool Show;

	// Token: 0x0400350B RID: 13579
	public int Columns;

	// Token: 0x0400350C RID: 13580
	public int Rows;

	// Token: 0x0400350D RID: 13581
	private int Column;

	// Token: 0x0400350E RID: 13582
	private int Row;

	// Token: 0x0400350F RID: 13583
	public UISprite BlackSprite;

	// Token: 0x04003510 RID: 13584
	public UISprite[] RedSprites;

	// Token: 0x04003511 RID: 13585
	public UILabel[] Labels;
}
