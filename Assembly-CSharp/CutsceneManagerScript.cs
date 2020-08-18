using System;
using UnityEngine;

// Token: 0x02000255 RID: 597
public class CutsceneManagerScript : MonoBehaviour
{
	// Token: 0x060012EB RID: 4843 RVA: 0x000987B8 File Offset: 0x000969B8
	private void Update()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		if (this.Phase == 1)
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 1f, Time.deltaTime));
			if (this.Darkness.color.a == 1f)
			{
				if (this.Scheme == 5)
				{
					this.Phase++;
					return;
				}
				this.Phase = 4;
				return;
			}
		}
		else
		{
			if (this.Phase == 2)
			{
				this.Subtitle.text = this.Text[this.Line];
				component.clip = this.Voice[this.Line];
				component.Play();
				this.Phase++;
				return;
			}
			if (this.Phase == 3)
			{
				if (!component.isPlaying || Input.GetButtonDown("A"))
				{
					if (this.Line < 2)
					{
						this.Phase--;
						this.Line++;
						return;
					}
					this.Subtitle.text = string.Empty;
					this.Phase++;
					return;
				}
			}
			else
			{
				if (this.Phase == 4)
				{
					Debug.Log("We're activating EndOfDay from CutsceneManager.");
					this.EndOfDay.gameObject.SetActive(true);
					this.EndOfDay.Phase = 14;
					if (this.Scheme == 5)
					{
						this.Counselor.LecturePhase = 5;
					}
					else
					{
						this.Counselor.LecturePhase = 1;
					}
					this.Phase++;
					return;
				}
				if (this.Phase == 6)
				{
					this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 0f, Time.deltaTime));
					if (this.Darkness.color.a == 0f)
					{
						this.Phase++;
						return;
					}
				}
				else if (this.Phase == 7)
				{
					if (this.Scheme == 5)
					{
						this.StudentManager.Students[this.StudentManager.RivalID] != null;
					}
					this.PromptBar.ClearButtons();
					this.PromptBar.Show = false;
					this.Portal.Proceed = true;
					base.gameObject.SetActive(false);
					this.Scheme = 0;
				}
			}
		}
	}

	// Token: 0x040018AD RID: 6317
	public StudentManagerScript StudentManager;

	// Token: 0x040018AE RID: 6318
	public CounselorScript Counselor;

	// Token: 0x040018AF RID: 6319
	public PromptBarScript PromptBar;

	// Token: 0x040018B0 RID: 6320
	public EndOfDayScript EndOfDay;

	// Token: 0x040018B1 RID: 6321
	public PortalScript Portal;

	// Token: 0x040018B2 RID: 6322
	public UISprite Darkness;

	// Token: 0x040018B3 RID: 6323
	public UILabel Subtitle;

	// Token: 0x040018B4 RID: 6324
	public AudioClip[] Voice;

	// Token: 0x040018B5 RID: 6325
	public string[] Text;

	// Token: 0x040018B6 RID: 6326
	public int Scheme;

	// Token: 0x040018B7 RID: 6327
	public int Phase = 1;

	// Token: 0x040018B8 RID: 6328
	public int Line = 1;
}
