using System;
using UnityEngine;

// Token: 0x0200029A RID: 666
public class FakeStudentScript : MonoBehaviour
{
	// Token: 0x060013F3 RID: 5107 RVA: 0x000ADF09 File Offset: 0x000AC109
	private void Start()
	{
		this.targetRotation = base.transform.rotation;
		this.Student.Club = this.Club;
	}

	// Token: 0x060013F4 RID: 5108 RVA: 0x000ADF30 File Offset: 0x000AC130
	private void Update()
	{
		if (!this.Student.Talking)
		{
			if (this.LeaderAnim != "")
			{
				base.GetComponent<Animation>().CrossFade(this.LeaderAnim);
			}
			if (this.Rotate)
			{
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
				this.RotationTimer += Time.deltaTime;
				if (this.RotationTimer > 1f)
				{
					this.RotationTimer = 0f;
					this.Rotate = false;
				}
			}
		}
		if (this.Prompt.Circle[0].fillAmount == 0f && !this.Yandere.Chased && this.Yandere.Chasers == 0)
		{
			this.Yandere.TargetStudent = this.Student;
			this.Subtitle.UpdateLabel(SubtitleType.ClubGreeting, (int)this.Student.Club, 4f);
			this.DialogueWheel.ClubLeader = true;
			this.StudentManager.DisablePrompts();
			this.DialogueWheel.HideShadows();
			this.DialogueWheel.Show = true;
			this.DialogueWheel.Panel.enabled = true;
			this.Student.Talking = true;
			this.Student.TalkTimer = 0f;
			this.Yandere.ShoulderCamera.OverShoulder = true;
			this.Yandere.WeaponMenu.KeyboardShow = false;
			this.Yandere.Obscurance.enabled = false;
			this.Yandere.WeaponMenu.Show = false;
			this.Yandere.YandereVision = false;
			this.Yandere.CanMove = false;
			this.Yandere.Talking = true;
			this.RotationTimer = 0f;
			this.Rotate = true;
		}
	}

	// Token: 0x04001BDF RID: 7135
	public StudentManagerScript StudentManager;

	// Token: 0x04001BE0 RID: 7136
	public DialogueWheelScript DialogueWheel;

	// Token: 0x04001BE1 RID: 7137
	public SubtitleScript Subtitle;

	// Token: 0x04001BE2 RID: 7138
	public YandereScript Yandere;

	// Token: 0x04001BE3 RID: 7139
	public StudentScript Student;

	// Token: 0x04001BE4 RID: 7140
	public PromptScript Prompt;

	// Token: 0x04001BE5 RID: 7141
	public Quaternion targetRotation;

	// Token: 0x04001BE6 RID: 7142
	public float RotationTimer;

	// Token: 0x04001BE7 RID: 7143
	public bool Rotate;

	// Token: 0x04001BE8 RID: 7144
	public ClubType Club;

	// Token: 0x04001BE9 RID: 7145
	public string LeaderAnim;
}
