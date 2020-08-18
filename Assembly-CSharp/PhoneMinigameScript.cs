using System;
using UnityEngine;

// Token: 0x0200035E RID: 862
public class PhoneMinigameScript : MonoBehaviour
{
	// Token: 0x060018C3 RID: 6339 RVA: 0x000E5214 File Offset: 0x000E3414
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.MainCamera.GetComponent<AudioListener>().enabled = true;
			this.Prompt.Yandere.Pickpocketing = true;
			this.Prompt.Yandere.CanMove = false;
			this.Prompt.Yandere.MainCamera.transform.eulerAngles = new Vector3(45f, 180f, 0f);
			this.Prompt.Yandere.MainCamera.transform.position = new Vector3(0.4f, 12.66666f, -29.2f);
			this.Prompt.Yandere.RPGCamera.enabled = false;
			this.SmartPhoneScreen = this.Event.Rival.SmartPhoneScreen;
			this.Smartphone = this.Event.Rival.SmartPhone.transform;
			this.PickpocketMinigame.StartingAlerts = this.Prompt.Yandere.Alerts;
			this.PickpocketMinigame.PickpocketSpot = null;
			this.PickpocketMinigame.Sabotage = true;
			this.PickpocketMinigame.Show = true;
			this.OriginalRotation = this.Smartphone.eulerAngles;
			this.OriginalPosition = this.Smartphone.position;
			this.Tampering = true;
		}
		if (this.Tampering)
		{
			this.Prompt.Yandere.MoveTowardsTarget(new Vector3(0f, 12f, -28.66666f));
			if (!this.PickpocketMinigame.Failure)
			{
				if (this.PickpocketMinigame.Progress == 1)
				{
					this.Smartphone.position = Vector3.Lerp(this.Smartphone.position, new Vector3(0.4f, this.Smartphone.position.y, this.Smartphone.position.z), Time.deltaTime * 10f);
					return;
				}
				if (this.PickpocketMinigame.Progress == 2)
				{
					this.Smartphone.eulerAngles = Vector3.Lerp(this.Smartphone.eulerAngles, new Vector3(0f, 180f, 0f), Time.deltaTime * 10f);
					return;
				}
				if (this.PickpocketMinigame.Progress == 3)
				{
					this.SmartPhoneScreen.material.mainTexture = this.AlarmOff;
					return;
				}
				if (this.PickpocketMinigame.Progress == 4)
				{
					this.Smartphone.eulerAngles = Vector3.Lerp(this.Smartphone.eulerAngles, new Vector3(this.OriginalRotation.x, this.OriginalRotation.y, this.OriginalRotation.z), Time.deltaTime * 10f);
					return;
				}
				if (!this.PickpocketMinigame.Show)
				{
					this.Smartphone.position = Vector3.Lerp(this.Smartphone.position, new Vector3(this.OriginalPosition.x, this.OriginalPosition.y, this.OriginalPosition.z), Time.deltaTime * 10f);
					this.Timer += Time.deltaTime;
					if ((double)this.Timer > 1.0)
					{
						this.Event.Sabotaged = true;
						this.End();
						return;
					}
				}
			}
			else
			{
				this.Prompt.Yandere.transform.position = new Vector3(0f, 12f, -28.5f);
				this.Event.Rival.transform.position = new Vector3(0f, 12f, -29.2f);
				this.Prompt.Yandere.Pickpocketing = true;
				this.Event.Rival.YandereVisible = true;
				this.Event.Rival.Distracted = false;
				this.Event.Rival.Alarm = 200f;
				this.End();
			}
		}
	}

	// Token: 0x060018C4 RID: 6340 RVA: 0x000E5624 File Offset: 0x000E3824
	private void End()
	{
		this.Prompt.Yandere.MainCamera.GetComponent<AudioListener>().enabled = false;
		this.Prompt.Yandere.RPGCamera.enabled = true;
		this.Prompt.Yandere.gameObject.SetActive(true);
		this.Prompt.Yandere.CanMove = true;
		this.Prompt.Hide();
		this.Prompt.enabled = false;
		this.Tampering = false;
		base.gameObject.SetActive(false);
	}

	// Token: 0x040024E9 RID: 9449
	public PickpocketMinigameScript PickpocketMinigame;

	// Token: 0x040024EA RID: 9450
	public OsanaThursdayAfterClassEventScript Event;

	// Token: 0x040024EB RID: 9451
	public Renderer SmartPhoneScreen;

	// Token: 0x040024EC RID: 9452
	public Transform Smartphone;

	// Token: 0x040024ED RID: 9453
	public PromptScript Prompt;

	// Token: 0x040024EE RID: 9454
	public Texture AlarmOff;

	// Token: 0x040024EF RID: 9455
	public bool Tampering;

	// Token: 0x040024F0 RID: 9456
	public float Timer;

	// Token: 0x040024F1 RID: 9457
	public Vector3 OriginalPosition;

	// Token: 0x040024F2 RID: 9458
	public Vector3 OriginalRotation;
}
