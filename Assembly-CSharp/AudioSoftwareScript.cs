using System;
using UnityEngine;

// Token: 0x020000D7 RID: 215
public class AudioSoftwareScript : MonoBehaviour
{
	// Token: 0x06000A3C RID: 2620 RVA: 0x00053D35 File Offset: 0x00051F35
	private void Start()
	{
		this.Screen.SetActive(false);
	}

	// Token: 0x06000A3D RID: 2621 RVA: 0x00053D44 File Offset: 0x00051F44
	private void Update()
	{
		if (this.ConversationRecorded && this.Yandere.Inventory.RivalPhone)
		{
			if (!this.Prompt.enabled)
			{
				this.Prompt.enabled = true;
			}
		}
		else if (this.Prompt.enabled)
		{
			this.Prompt.enabled = false;
		}
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Yandere.Character.GetComponent<Animation>().CrossFade("f02_playingGames_00");
			this.Yandere.MyController.radius = 0.1f;
			this.Yandere.CanMove = false;
			base.GetComponent<AudioSource>().Play();
			this.ChairCollider.enabled = false;
			this.Screen.SetActive(true);
			this.Editing = true;
		}
		if (this.Editing)
		{
			this.targetRotation = Quaternion.LookRotation(new Vector3(this.Screen.transform.position.x, this.Yandere.transform.position.y, this.Screen.transform.position.z) - this.Yandere.transform.position);
			this.Yandere.transform.rotation = Quaternion.Slerp(this.Yandere.transform.rotation, this.targetRotation, Time.deltaTime * 10f);
			this.Yandere.MoveTowardsTarget(this.SitSpot.position);
			this.Timer += Time.deltaTime;
			if (this.Timer > 1f)
			{
				this.EventSubtitle.text = "Okay, how 'bout that boy from class 3-2? What do you think of him?";
			}
			if (this.Timer > 7f)
			{
				this.EventSubtitle.text = "He's just my childhood friend.";
			}
			if (this.Timer > 9f)
			{
				this.EventSubtitle.text = "Is he your boyfriend?";
			}
			if (this.Timer > 11f)
			{
				this.EventSubtitle.text = "What? HIM? Ugh, no way! That guy's a total creep! I wouldn't date him if he was the last man alive on earth! He can go jump off a cliff for all I care!";
			}
			if (this.Timer > 22f)
			{
				this.Yandere.MyController.radius = 0.2f;
				this.Yandere.CanMove = true;
				this.ChairCollider.enabled = false;
				this.EventSubtitle.text = "";
				this.Screen.SetActive(false);
				this.AudioDoctored = true;
				this.Editing = false;
				this.Prompt.enabled = false;
				this.Prompt.Hide();
				base.enabled = false;
			}
		}
	}

	// Token: 0x04000A60 RID: 2656
	public YandereScript Yandere;

	// Token: 0x04000A61 RID: 2657
	public PromptScript Prompt;

	// Token: 0x04000A62 RID: 2658
	public Quaternion targetRotation;

	// Token: 0x04000A63 RID: 2659
	public Collider ChairCollider;

	// Token: 0x04000A64 RID: 2660
	public UILabel EventSubtitle;

	// Token: 0x04000A65 RID: 2661
	public GameObject Screen;

	// Token: 0x04000A66 RID: 2662
	public Transform SitSpot;

	// Token: 0x04000A67 RID: 2663
	public bool ConversationRecorded;

	// Token: 0x04000A68 RID: 2664
	public bool AudioDoctored;

	// Token: 0x04000A69 RID: 2665
	public bool Editing;

	// Token: 0x04000A6A RID: 2666
	public float Timer;
}
