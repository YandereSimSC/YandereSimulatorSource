using System;
using UnityEngine;

// Token: 0x02000357 RID: 855
public class PersonaSubtitleScript : MonoBehaviour
{
	// Token: 0x060018AF RID: 6319 RVA: 0x000E3768 File Offset: 0x000E1968
	public void UpdateLabel(PersonaType Persona, float Reputation, float Duration)
	{
		switch (Persona)
		{
		case PersonaType.Loner:
			this.SubtitleArray = this.LonerReactions;
			break;
		case PersonaType.TeachersPet:
			this.SubtitleArray = this.TeachersPetReactions;
			break;
		case PersonaType.Heroic:
			this.SubtitleArray = this.HeroicReactions;
			break;
		case PersonaType.Coward:
			this.SubtitleArray = this.CowardReactions;
			break;
		case PersonaType.Evil:
			this.SubtitleArray = this.EvilReactions;
			break;
		case PersonaType.SocialButterfly:
			this.SubtitleArray = this.SocialButterflyReactions;
			break;
		case PersonaType.Lovestruck:
			this.SubtitleArray = this.LovestruckReactions;
			break;
		case PersonaType.Dangerous:
			this.SubtitleArray = this.DangerousReactions;
			break;
		case PersonaType.Strict:
			this.SubtitleArray = this.StrictReactions;
			break;
		case PersonaType.PhoneAddict:
			this.SubtitleArray = this.PhoneAddictReactions;
			break;
		case PersonaType.Fragile:
			this.SubtitleArray = this.FragileReactions;
			break;
		case PersonaType.Spiteful:
			this.SubtitleArray = this.SpitefulReactions;
			break;
		case PersonaType.Sleuth:
			this.SubtitleArray = this.SleuthReactions;
			break;
		case PersonaType.Vengeful:
			this.SubtitleArray = this.VengefulReactions;
			break;
		case PersonaType.Protective:
			this.SubtitleArray = this.ProtectiveReactions;
			break;
		case PersonaType.Violent:
			this.SubtitleArray = this.ViolentReactions;
			break;
		default:
			if (Persona == PersonaType.Nemesis)
			{
				this.SubtitleArray = this.NemesisReactions;
			}
			break;
		}
		if (Reputation < -0.33333334f)
		{
			this.Subtitle.Label.text = this.SubtitleArray[1];
		}
		else if (Reputation > 0.33333334f)
		{
			this.Subtitle.Label.text = this.SubtitleArray[3];
		}
		else
		{
			this.Subtitle.Label.text = this.SubtitleArray[2];
		}
		this.Subtitle.Timer = Duration;
	}

	// Token: 0x0400248F RID: 9359
	public SubtitleScript Subtitle;

	// Token: 0x04002490 RID: 9360
	public string[] LonerReactions;

	// Token: 0x04002491 RID: 9361
	public string[] TeachersPetReactions;

	// Token: 0x04002492 RID: 9362
	public string[] HeroicReactions;

	// Token: 0x04002493 RID: 9363
	public string[] CowardReactions;

	// Token: 0x04002494 RID: 9364
	public string[] EvilReactions;

	// Token: 0x04002495 RID: 9365
	public string[] SocialButterflyReactions;

	// Token: 0x04002496 RID: 9366
	public string[] LovestruckReactions;

	// Token: 0x04002497 RID: 9367
	public string[] DangerousReactions;

	// Token: 0x04002498 RID: 9368
	public string[] StrictReactions;

	// Token: 0x04002499 RID: 9369
	public string[] PhoneAddictReactions;

	// Token: 0x0400249A RID: 9370
	public string[] FragileReactions;

	// Token: 0x0400249B RID: 9371
	public string[] SpitefulReactions;

	// Token: 0x0400249C RID: 9372
	public string[] SleuthReactions;

	// Token: 0x0400249D RID: 9373
	public string[] VengefulReactions;

	// Token: 0x0400249E RID: 9374
	public string[] ProtectiveReactions;

	// Token: 0x0400249F RID: 9375
	public string[] ViolentReactions;

	// Token: 0x040024A0 RID: 9376
	public string[] NemesisReactions;

	// Token: 0x040024A1 RID: 9377
	public string[] SubtitleArray;
}
