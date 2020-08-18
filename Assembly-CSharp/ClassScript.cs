using System;
using UnityEngine;

// Token: 0x02000239 RID: 569
public class ClassScript : MonoBehaviour
{
	// Token: 0x06001244 RID: 4676 RVA: 0x00081464 File Offset: 0x0007F664
	private void Start()
	{
		this.GradeUpWindow.localScale = Vector3.zero;
		this.Subject[1] = ClassGlobals.Biology;
		this.Subject[2] = ClassGlobals.Chemistry;
		this.Subject[3] = ClassGlobals.Language;
		this.Subject[4] = ClassGlobals.Physical;
		this.Subject[5] = ClassGlobals.Psychology;
		this.DescLabel.text = this.Desc[this.Selected];
		this.UpdateSubjectLabels();
		this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, 1f);
		this.UpdateBars();
	}

	// Token: 0x06001245 RID: 4677 RVA: 0x0008152C File Offset: 0x0007F72C
	private void Update()
	{
		if (this.Show)
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, this.Darkness.color.a - Time.deltaTime);
			if (this.Darkness.color.a <= 0f)
			{
				if (Input.GetKeyDown(KeyCode.Backslash))
				{
					this.GivePoints();
				}
				if (Input.GetKeyDown(KeyCode.P))
				{
					this.MaxPhysical();
				}
				this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, 0f);
				if (this.InputManager.TappedDown)
				{
					this.Selected++;
					if (this.Selected > 5)
					{
						this.Selected = 1;
					}
					this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 375f - 125f * (float)this.Selected, this.Highlight.localPosition.z);
					this.DescLabel.text = this.Desc[this.Selected];
					this.UpdateSubjectLabels();
				}
				if (this.InputManager.TappedUp)
				{
					this.Selected--;
					if (this.Selected < 1)
					{
						this.Selected = 5;
					}
					this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 375f - 125f * (float)this.Selected, this.Highlight.localPosition.z);
					this.DescLabel.text = this.Desc[this.Selected];
					this.UpdateSubjectLabels();
				}
				if (this.InputManager.TappedRight && this.StudyPoints > 0 && this.Subject[this.Selected] + this.SubjectTemp[this.Selected] < 100)
				{
					this.SubjectTemp[this.Selected]++;
					this.StudyPoints--;
					this.UpdateLabel();
					this.UpdateBars();
				}
				if (this.InputManager.TappedLeft && this.SubjectTemp[this.Selected] > 0)
				{
					this.SubjectTemp[this.Selected]--;
					this.StudyPoints++;
					this.UpdateLabel();
					this.UpdateBars();
				}
				if (Input.GetButtonDown("A") && this.StudyPoints == 0)
				{
					this.Show = false;
					this.PromptBar.ClearButtons();
					this.PromptBar.Show = false;
					ClassGlobals.Biology = this.Subject[1] + this.SubjectTemp[1];
					ClassGlobals.Chemistry = this.Subject[2] + this.SubjectTemp[2];
					ClassGlobals.Language = this.Subject[3] + this.SubjectTemp[3];
					ClassGlobals.Physical = this.Subject[4] + this.SubjectTemp[4];
					ClassGlobals.Psychology = this.Subject[5] + this.SubjectTemp[5];
					for (int i = 0; i < 6; i++)
					{
						this.Subject[i] += this.SubjectTemp[i];
						this.SubjectTemp[i] = 0;
					}
					this.CheckForGradeUp();
					return;
				}
			}
		}
		else
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, this.Darkness.color.a + Time.deltaTime);
			if (this.Darkness.color.a >= 1f)
			{
				this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, 1f);
				if (!this.GradeUp)
				{
					if (this.GradeUpWindow.localScale.x > 0.1f)
					{
						this.GradeUpWindow.localScale = Vector3.Lerp(this.GradeUpWindow.localScale, Vector3.zero, Time.deltaTime * 10f);
					}
					else
					{
						this.GradeUpWindow.localScale = Vector3.zero;
					}
					if (this.GradeUpWindow.localScale.x < 0.01f)
					{
						this.GradeUpWindow.localScale = Vector3.zero;
						this.CheckForGradeUp();
						if (!this.GradeUp)
						{
							if (ClassGlobals.ChemistryGrade > 0 && this.Poison != null)
							{
								this.Poison.SetActive(true);
							}
							Debug.Log("CutsceneManager.Scheme is: " + this.CutsceneManager.Scheme);
							if (this.CutsceneManager.Scheme > 0)
							{
								SchemeGlobals.SetSchemeStage(this.CutsceneManager.Scheme, 100);
								this.PromptBar.ClearButtons();
								this.PromptBar.Label[0].text = "Continue";
								this.PromptBar.UpdateButtons();
								this.CutsceneManager.gameObject.SetActive(true);
								this.Schemes.UpdateInstructions();
								base.gameObject.SetActive(false);
								return;
							}
							if (!this.Portal.FadeOut)
							{
								this.Portal.Yandere.PhysicalGrade = ClassGlobals.PhysicalGrade;
								this.PromptBar.Show = false;
								this.Portal.Proceed = true;
								base.gameObject.SetActive(false);
								return;
							}
						}
					}
				}
				else
				{
					if (this.GradeUpWindow.localScale.x == 0f)
					{
						if (this.GradeUpSubject == 1)
						{
							this.GradeUpName.text = "BIOLOGY RANK UP";
							this.GradeUpDesc.text = this.Subject1GradeText[this.Grade];
						}
						else if (this.GradeUpSubject == 2)
						{
							this.GradeUpName.text = "CHEMISTRY RANK UP";
							this.GradeUpDesc.text = this.Subject2GradeText[this.Grade];
						}
						else if (this.GradeUpSubject == 3)
						{
							this.GradeUpName.text = "LANGUAGE RANK UP";
							this.GradeUpDesc.text = this.Subject3GradeText[this.Grade];
						}
						else if (this.GradeUpSubject == 4)
						{
							this.GradeUpName.text = "PHYSICAL RANK UP";
							this.GradeUpDesc.text = this.Subject4GradeText[this.Grade];
						}
						else if (this.GradeUpSubject == 5)
						{
							this.GradeUpName.text = "PSYCHOLOGY RANK UP";
							this.GradeUpDesc.text = this.Subject5GradeText[this.Grade];
						}
						this.PromptBar.ClearButtons();
						this.PromptBar.Label[0].text = "Continue";
						this.PromptBar.UpdateButtons();
						this.PromptBar.Show = true;
					}
					else if (this.GradeUpWindow.localScale.x > 0.99f && Input.GetButtonDown("A"))
					{
						this.PromptBar.ClearButtons();
						this.GradeUp = false;
					}
					this.GradeUpWindow.localScale = Vector3.Lerp(this.GradeUpWindow.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
				}
			}
		}
	}

	// Token: 0x06001246 RID: 4678 RVA: 0x00081CD4 File Offset: 0x0007FED4
	private void UpdateSubjectLabels()
	{
		for (int i = 1; i < 6; i++)
		{
			this.SubjectLabels[i].color = new Color(0f, 0f, 0f, 1f);
		}
		this.SubjectLabels[this.Selected].color = new Color(1f, 1f, 1f, 1f);
	}

	// Token: 0x06001247 RID: 4679 RVA: 0x00081D40 File Offset: 0x0007FF40
	public void UpdateLabel()
	{
		this.StudyPointsLabel.text = "STUDY POINTS: " + this.StudyPoints;
		if (this.StudyPoints == 0)
		{
			this.PromptBar.Label[0].text = "Confirm";
			this.PromptBar.UpdateButtons();
			return;
		}
		this.PromptBar.Label[0].text = string.Empty;
		this.PromptBar.UpdateButtons();
	}

	// Token: 0x06001248 RID: 4680 RVA: 0x00081DBC File Offset: 0x0007FFBC
	private void UpdateBars()
	{
		for (int i = 1; i < 6; i++)
		{
			Transform transform = this.Subject1Bars[i];
			if (this.Subject[1] + this.SubjectTemp[1] > (i - 1) * 20)
			{
				transform.localScale = new Vector3(-((float)((i - 1) * 20 - (this.Subject[1] + this.SubjectTemp[1])) / 20f), transform.localScale.y, transform.localScale.z);
				if (transform.localScale.x > 1f)
				{
					transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
				}
			}
			else
			{
				transform.localScale = new Vector3(0f, transform.localScale.y, transform.localScale.z);
			}
		}
		for (int j = 1; j < 6; j++)
		{
			Transform transform2 = this.Subject2Bars[j];
			if (this.Subject[2] + this.SubjectTemp[2] > (j - 1) * 20)
			{
				transform2.localScale = new Vector3(-((float)((j - 1) * 20 - (this.Subject[2] + this.SubjectTemp[2])) / 20f), transform2.localScale.y, transform2.localScale.z);
				if (transform2.localScale.x > 1f)
				{
					transform2.localScale = new Vector3(1f, transform2.localScale.y, transform2.localScale.z);
				}
			}
			else
			{
				transform2.localScale = new Vector3(0f, transform2.localScale.y, transform2.localScale.z);
			}
		}
		for (int k = 1; k < 6; k++)
		{
			Transform transform3 = this.Subject3Bars[k];
			if (this.Subject[3] + this.SubjectTemp[3] > (k - 1) * 20)
			{
				transform3.localScale = new Vector3(-((float)((k - 1) * 20 - (this.Subject[3] + this.SubjectTemp[3])) / 20f), transform3.localScale.y, transform3.localScale.z);
				if (transform3.localScale.x > 1f)
				{
					transform3.localScale = new Vector3(1f, transform3.localScale.y, transform3.localScale.z);
				}
			}
			else
			{
				transform3.localScale = new Vector3(0f, transform3.localScale.y, transform3.localScale.z);
			}
		}
		for (int l = 1; l < 6; l++)
		{
			Transform transform4 = this.Subject4Bars[l];
			if (this.Subject[4] + this.SubjectTemp[4] > (l - 1) * 20)
			{
				transform4.localScale = new Vector3(-((float)((l - 1) * 20 - (this.Subject[4] + this.SubjectTemp[4])) / 20f), transform4.localScale.y, transform4.localScale.z);
				if (transform4.localScale.x > 1f)
				{
					transform4.localScale = new Vector3(1f, transform4.localScale.y, transform4.localScale.z);
				}
			}
			else
			{
				transform4.localScale = new Vector3(0f, transform4.localScale.y, transform4.localScale.z);
			}
		}
		for (int m = 1; m < 6; m++)
		{
			Transform transform5 = this.Subject5Bars[m];
			if (this.Subject[5] + this.SubjectTemp[5] > (m - 1) * 20)
			{
				transform5.localScale = new Vector3(-((float)((m - 1) * 20 - (this.Subject[5] + this.SubjectTemp[5])) / 20f), transform5.localScale.y, transform5.localScale.z);
				if (transform5.localScale.x > 1f)
				{
					transform5.localScale = new Vector3(1f, transform5.localScale.y, transform5.localScale.z);
				}
			}
			else
			{
				transform5.localScale = new Vector3(0f, transform5.localScale.y, transform5.localScale.z);
			}
		}
	}

	// Token: 0x06001249 RID: 4681 RVA: 0x00082234 File Offset: 0x00080434
	private void CheckForGradeUp()
	{
		if (ClassGlobals.Biology >= 20 && ClassGlobals.BiologyGrade < 1)
		{
			ClassGlobals.BiologyGrade = 1;
			this.GradeUpSubject = 1;
			this.GradeUp = true;
			this.Grade = 1;
			return;
		}
		if (ClassGlobals.Chemistry >= 20 && ClassGlobals.ChemistryGrade < 1)
		{
			ClassGlobals.ChemistryGrade = 1;
			this.GradeUpSubject = 2;
			this.GradeUp = true;
			this.Grade = 1;
			return;
		}
		if (ClassGlobals.Language >= 20 && ClassGlobals.LanguageGrade < 1)
		{
			ClassGlobals.LanguageGrade = 1;
			this.GradeUpSubject = 3;
			this.GradeUp = true;
			this.Grade = 1;
			return;
		}
		if (ClassGlobals.Physical >= 20 && ClassGlobals.PhysicalGrade < 1)
		{
			ClassGlobals.PhysicalGrade = 1;
			this.GradeUpSubject = 4;
			this.GradeUp = true;
			this.Grade = 1;
			return;
		}
		if (ClassGlobals.Physical >= 40 && ClassGlobals.PhysicalGrade < 2)
		{
			ClassGlobals.PhysicalGrade = 2;
			this.GradeUpSubject = 4;
			this.GradeUp = true;
			this.Grade = 2;
			return;
		}
		if (ClassGlobals.Physical >= 60 && ClassGlobals.PhysicalGrade < 3)
		{
			ClassGlobals.PhysicalGrade = 3;
			this.GradeUpSubject = 4;
			this.GradeUp = true;
			this.Grade = 3;
			return;
		}
		if (ClassGlobals.Physical >= 80 && ClassGlobals.PhysicalGrade < 4)
		{
			ClassGlobals.PhysicalGrade = 4;
			this.GradeUpSubject = 4;
			this.GradeUp = true;
			this.Grade = 4;
			return;
		}
		if (ClassGlobals.Physical == 100 && ClassGlobals.PhysicalGrade < 5)
		{
			ClassGlobals.PhysicalGrade = 5;
			this.GradeUpSubject = 4;
			this.GradeUp = true;
			this.Grade = 5;
			return;
		}
		if (ClassGlobals.Psychology >= 20 && ClassGlobals.PsychologyGrade < 1)
		{
			ClassGlobals.PsychologyGrade = 1;
			this.GradeUpSubject = 5;
			this.GradeUp = true;
			this.Grade = 1;
		}
	}

	// Token: 0x0600124A RID: 4682 RVA: 0x000823D8 File Offset: 0x000805D8
	private void GivePoints()
	{
		ClassGlobals.BiologyGrade = 0;
		ClassGlobals.ChemistryGrade = 0;
		ClassGlobals.LanguageGrade = 0;
		ClassGlobals.PhysicalGrade = 0;
		ClassGlobals.PsychologyGrade = 0;
		ClassGlobals.Biology = 19;
		ClassGlobals.Chemistry = 19;
		ClassGlobals.Language = 19;
		ClassGlobals.Physical = 19;
		ClassGlobals.Psychology = 19;
		this.Subject[1] = ClassGlobals.Biology;
		this.Subject[2] = ClassGlobals.Chemistry;
		this.Subject[3] = ClassGlobals.Language;
		this.Subject[4] = ClassGlobals.Physical;
		this.Subject[5] = ClassGlobals.Psychology;
		this.UpdateBars();
	}

	// Token: 0x0600124B RID: 4683 RVA: 0x0008246D File Offset: 0x0008066D
	private void MaxPhysical()
	{
		ClassGlobals.PhysicalGrade = 0;
		ClassGlobals.Physical = 99;
		this.Subject[4] = ClassGlobals.Physical;
		this.UpdateBars();
	}

	// Token: 0x0400158E RID: 5518
	public CutsceneManagerScript CutsceneManager;

	// Token: 0x0400158F RID: 5519
	public InputManagerScript InputManager;

	// Token: 0x04001590 RID: 5520
	public PromptBarScript PromptBar;

	// Token: 0x04001591 RID: 5521
	public SchemesScript Schemes;

	// Token: 0x04001592 RID: 5522
	public PortalScript Portal;

	// Token: 0x04001593 RID: 5523
	public GameObject Poison;

	// Token: 0x04001594 RID: 5524
	public UILabel StudyPointsLabel;

	// Token: 0x04001595 RID: 5525
	public UILabel[] SubjectLabels;

	// Token: 0x04001596 RID: 5526
	public UILabel GradeUpDesc;

	// Token: 0x04001597 RID: 5527
	public UILabel GradeUpName;

	// Token: 0x04001598 RID: 5528
	public UILabel DescLabel;

	// Token: 0x04001599 RID: 5529
	public UISprite Darkness;

	// Token: 0x0400159A RID: 5530
	public Transform[] Subject1Bars;

	// Token: 0x0400159B RID: 5531
	public Transform[] Subject2Bars;

	// Token: 0x0400159C RID: 5532
	public Transform[] Subject3Bars;

	// Token: 0x0400159D RID: 5533
	public Transform[] Subject4Bars;

	// Token: 0x0400159E RID: 5534
	public Transform[] Subject5Bars;

	// Token: 0x0400159F RID: 5535
	public string[] Subject1GradeText;

	// Token: 0x040015A0 RID: 5536
	public string[] Subject2GradeText;

	// Token: 0x040015A1 RID: 5537
	public string[] Subject3GradeText;

	// Token: 0x040015A2 RID: 5538
	public string[] Subject4GradeText;

	// Token: 0x040015A3 RID: 5539
	public string[] Subject5GradeText;

	// Token: 0x040015A4 RID: 5540
	public Transform GradeUpWindow;

	// Token: 0x040015A5 RID: 5541
	public Transform Highlight;

	// Token: 0x040015A6 RID: 5542
	public int[] SubjectTemp;

	// Token: 0x040015A7 RID: 5543
	public int[] Subject;

	// Token: 0x040015A8 RID: 5544
	public string[] Desc;

	// Token: 0x040015A9 RID: 5545
	public int GradeUpSubject;

	// Token: 0x040015AA RID: 5546
	public int StudyPoints;

	// Token: 0x040015AB RID: 5547
	public int Selected;

	// Token: 0x040015AC RID: 5548
	public int Grade;

	// Token: 0x040015AD RID: 5549
	public bool GradeUp;

	// Token: 0x040015AE RID: 5550
	public bool Show;
}
