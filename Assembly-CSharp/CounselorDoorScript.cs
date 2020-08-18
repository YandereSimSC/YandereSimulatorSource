using System;
using UnityEngine;

// Token: 0x0200024B RID: 587
public class CounselorDoorScript : MonoBehaviour
{
	// Token: 0x060012AC RID: 4780 RVA: 0x00091D41 File Offset: 0x0008FF41
	private void Start()
	{
		this.Prompt.enabled = false;
		this.Prompt.Hide();
	}

	// Token: 0x060012AD RID: 4781 RVA: 0x00091D5C File Offset: 0x0008FF5C
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if (!this.Prompt.Yandere.Chased && this.Prompt.Yandere.Chasers == 0 && !this.FadeIn && this.Prompt.Yandere.Bloodiness == 0f && this.Prompt.Yandere.Sanity > 66.66666f && !this.Prompt.Yandere.Carrying && !this.Prompt.Yandere.Dragging)
			{
				if (!this.Counselor.Busy)
				{
					this.Prompt.Yandere.CharacterAnimation.CrossFade(this.Prompt.Yandere.IdleAnim);
					this.Prompt.Yandere.Police.Darkness.enabled = true;
					this.Prompt.Yandere.CanMove = false;
					this.FadeOut = true;
				}
				else
				{
					this.Counselor.CounselorSubtitle.text = this.Counselor.CounselorBusyText;
					this.Counselor.MyAudio.clip = this.Counselor.CounselorBusyClip;
					this.Counselor.MyAudio.Play();
				}
			}
		}
		if (this.FadeOut)
		{
			float a = Mathf.MoveTowards(this.Darkness.color.a, 1f, Time.deltaTime);
			this.Darkness.color = new Color(0f, 0f, 0f, a);
			if (this.Darkness.color.a == 1f)
			{
				if (!this.Exit)
				{
					this.Prompt.Yandere.CharacterAnimation.Play("f02_sit_00");
					this.Prompt.Yandere.transform.position = new Vector3(-27.51f, 0f, 12f);
					this.Prompt.Yandere.transform.localEulerAngles = new Vector3(0f, -90f, 0f);
					this.Counselor.Talk();
					this.FadeOut = false;
					this.FadeIn = true;
				}
				else
				{
					this.Darkness.color = new Color(0f, 0f, 0f, 2f);
					this.Counselor.Quit();
					this.FadeOut = false;
					this.FadeIn = true;
					this.Exit = false;
				}
			}
		}
		if (this.FadeIn)
		{
			float a2 = Mathf.MoveTowards(this.Darkness.color.a, 0f, Time.deltaTime);
			this.Darkness.color = new Color(0f, 0f, 0f, a2);
			if (this.Darkness.color.a == 0f)
			{
				this.FadeIn = false;
			}
		}
	}

	// Token: 0x040017AC RID: 6060
	public CounselorScript Counselor;

	// Token: 0x040017AD RID: 6061
	public PromptScript Prompt;

	// Token: 0x040017AE RID: 6062
	public UISprite Darkness;

	// Token: 0x040017AF RID: 6063
	public bool FadeOut;

	// Token: 0x040017B0 RID: 6064
	public bool FadeIn;

	// Token: 0x040017B1 RID: 6065
	public bool Exit;
}
