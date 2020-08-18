﻿using System;
using UnityEngine;

// Token: 0x02000415 RID: 1045
public class TapePlayerScript : MonoBehaviour
{
	// Token: 0x06001C05 RID: 7173 RVA: 0x0014E4E2 File Offset: 0x0014C6E2
	private void Start()
	{
		this.Tape.SetActive(false);
	}

	// Token: 0x06001C06 RID: 7174 RVA: 0x0014E4F0 File Offset: 0x0014C6F0
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Yandere.HeartCamera.enabled = false;
			this.Yandere.RPGCamera.enabled = false;
			this.TapePlayerMenu.TimeBar.gameObject.SetActive(true);
			this.TapePlayerMenu.List.gameObject.SetActive(true);
			this.TapePlayerCamera.enabled = true;
			this.TapePlayerMenu.UpdateLabels();
			this.TapePlayerMenu.Show = true;
			this.NoteWindow.SetActive(false);
			this.Yandere.CanMove = false;
			this.Yandere.HUD.alpha = 0f;
			Time.timeScale = 0.0001f;
			this.PromptBar.ClearButtons();
			this.PromptBar.Label[1].text = "EXIT";
			this.PromptBar.Label[4].text = "CHOOSE";
			this.PromptBar.Label[5].text = "CATEGORY";
			this.TapePlayerMenu.CheckSelection();
			this.PromptBar.Show = true;
			this.Prompt.Hide();
			this.Prompt.enabled = false;
		}
		if (this.Spin)
		{
			Transform transform = this.Rolls[0];
			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + 0.016666668f * (360f * this.SpinSpeed), transform.localEulerAngles.z);
			Transform transform2 = this.Rolls[1];
			transform2.localEulerAngles = new Vector3(transform2.localEulerAngles.x, transform2.localEulerAngles.y + 0.016666668f * (360f * this.SpinSpeed), transform2.localEulerAngles.z);
		}
		if (this.FastForward)
		{
			this.FFButton.localEulerAngles = new Vector3(Mathf.MoveTowards(this.FFButton.localEulerAngles.x, 6.25f, 1.6666666f), this.FFButton.localEulerAngles.y, this.FFButton.localEulerAngles.z);
			this.SpinSpeed = 2f;
		}
		else
		{
			this.FFButton.localEulerAngles = new Vector3(Mathf.MoveTowards(this.FFButton.localEulerAngles.x, 0f, 1.6666666f), this.FFButton.localEulerAngles.y, this.FFButton.localEulerAngles.z);
			this.SpinSpeed = 1f;
		}
		if (this.Rewind)
		{
			this.RWButton.localEulerAngles = new Vector3(Mathf.MoveTowards(this.RWButton.localEulerAngles.x, 6.25f, 1.6666666f), this.RWButton.localEulerAngles.y, this.RWButton.localEulerAngles.z);
			this.SpinSpeed = -2f;
			return;
		}
		this.RWButton.localEulerAngles = new Vector3(Mathf.MoveTowards(this.RWButton.localEulerAngles.x, 0f, 1.6666666f), this.RWButton.localEulerAngles.y, this.RWButton.localEulerAngles.z);
	}

	// Token: 0x04003448 RID: 13384
	public TapePlayerMenuScript TapePlayerMenu;

	// Token: 0x04003449 RID: 13385
	public PromptBarScript PromptBar;

	// Token: 0x0400344A RID: 13386
	public YandereScript Yandere;

	// Token: 0x0400344B RID: 13387
	public PromptScript Prompt;

	// Token: 0x0400344C RID: 13388
	public Transform RWButton;

	// Token: 0x0400344D RID: 13389
	public Transform FFButton;

	// Token: 0x0400344E RID: 13390
	public Camera TapePlayerCamera;

	// Token: 0x0400344F RID: 13391
	public Transform[] Rolls;

	// Token: 0x04003450 RID: 13392
	public GameObject NoteWindow;

	// Token: 0x04003451 RID: 13393
	public GameObject Tape;

	// Token: 0x04003452 RID: 13394
	public bool FastForward;

	// Token: 0x04003453 RID: 13395
	public bool Rewind;

	// Token: 0x04003454 RID: 13396
	public bool Spin;

	// Token: 0x04003455 RID: 13397
	public float SpinSpeed;
}
