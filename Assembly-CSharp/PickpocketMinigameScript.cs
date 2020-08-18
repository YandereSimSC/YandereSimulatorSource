using System;
using UnityEngine;

// Token: 0x02000364 RID: 868
public class PickpocketMinigameScript : MonoBehaviour
{
	// Token: 0x060018F0 RID: 6384 RVA: 0x000E905C File Offset: 0x000E725C
	private void Start()
	{
		base.transform.localScale = Vector3.zero;
		this.ButtonPrompts[1].enabled = false;
		this.ButtonPrompts[2].enabled = false;
		this.ButtonPrompts[3].enabled = false;
		this.ButtonPrompts[4].enabled = false;
		this.Circle.enabled = false;
		this.BG.enabled = false;
	}

	// Token: 0x060018F1 RID: 6385 RVA: 0x000E90CC File Offset: 0x000E72CC
	private void Update()
	{
		if (this.Show)
		{
			if (this.PickpocketSpot != null)
			{
				this.Yandere.MoveTowardsTarget(this.PickpocketSpot.position);
				this.Yandere.transform.rotation = Quaternion.Slerp(this.Yandere.transform.rotation, this.PickpocketSpot.rotation, Time.deltaTime * 10f);
			}
			base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
			this.Timer += Time.deltaTime;
			Debug.Log(string.Concat(new object[]
			{
				"Starting Alerts is: ",
				this.StartingAlerts,
				". Yandere's current Alerts are: ",
				this.Yandere.Alerts
			}));
			if (this.Timer > 1f)
			{
				if (this.ButtonID == 0 && this.Yandere.Alerts == this.StartingAlerts)
				{
					this.ChooseButton();
					this.Timer = 0f;
					return;
				}
				this.Yandere.Caught = true;
				this.Failure = true;
				this.End();
				return;
			}
			else if (this.ButtonID > 0)
			{
				this.Circle.fillAmount = 1f - this.Timer / 1f;
				if ((Input.GetButtonDown("A") && this.CurrentButton != "A") || (Input.GetButtonDown("B") && this.CurrentButton != "B") || (Input.GetButtonDown("X") && this.CurrentButton != "X") || (Input.GetButtonDown("Y") && this.CurrentButton != "Y"))
				{
					this.Yandere.Caught = true;
					this.Failure = true;
					this.End();
					return;
				}
				if (Input.GetButtonDown(this.CurrentButton))
				{
					this.ButtonPrompts[this.ButtonID].enabled = false;
					this.Circle.enabled = false;
					this.BG.enabled = false;
					this.ButtonID = 0;
					this.Timer = 0f;
					this.Progress++;
					if (this.Progress == 5)
					{
						if (this.Sabotage)
						{
							this.Yandere.NotificationManager.CustomText = "Sabotage Success";
							this.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
						}
						else
						{
							this.Yandere.NotificationManager.CustomText = "Pickpocket Success";
							this.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
						}
						this.Yandere.Pickpocketing = false;
						this.Yandere.CanMove = true;
						this.Success = true;
						this.End();
						return;
					}
				}
			}
		}
		else if (base.transform.localScale.x > 0.1f)
		{
			base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
			if (base.transform.localScale.x < 0.1f)
			{
				base.transform.localScale = Vector3.zero;
			}
		}
	}

	// Token: 0x060018F2 RID: 6386 RVA: 0x000E943C File Offset: 0x000E763C
	private void ChooseButton()
	{
		this.ButtonPrompts[1].enabled = false;
		this.ButtonPrompts[2].enabled = false;
		this.ButtonPrompts[3].enabled = false;
		this.ButtonPrompts[4].enabled = false;
		int buttonID = this.ButtonID;
		while (this.ButtonID == buttonID)
		{
			this.ButtonID = UnityEngine.Random.Range(1, 5);
		}
		if (this.ButtonID == 1)
		{
			this.CurrentButton = "A";
		}
		else if (this.ButtonID == 2)
		{
			this.CurrentButton = "B";
		}
		else if (this.ButtonID == 3)
		{
			this.CurrentButton = "X";
		}
		else if (this.ButtonID == 4)
		{
			this.CurrentButton = "Y";
		}
		this.ButtonPrompts[this.ButtonID].enabled = true;
		this.Circle.enabled = true;
		this.BG.enabled = true;
	}

	// Token: 0x060018F3 RID: 6387 RVA: 0x000E9524 File Offset: 0x000E7724
	public void End()
	{
		Debug.Log("Ending minigame.");
		this.ButtonPrompts[1].enabled = false;
		this.ButtonPrompts[2].enabled = false;
		this.ButtonPrompts[3].enabled = false;
		this.ButtonPrompts[4].enabled = false;
		this.Circle.enabled = false;
		this.BG.enabled = false;
		this.Yandere.CharacterAnimation.CrossFade("f02_readyToFight_00");
		this.Progress = 0;
		this.ButtonID = 0;
		this.Show = false;
		this.Timer = 0f;
	}

	// Token: 0x04002566 RID: 9574
	public Transform PickpocketSpot;

	// Token: 0x04002567 RID: 9575
	public UISprite[] ButtonPrompts;

	// Token: 0x04002568 RID: 9576
	public UISprite Circle;

	// Token: 0x04002569 RID: 9577
	public UISprite BG;

	// Token: 0x0400256A RID: 9578
	public YandereScript Yandere;

	// Token: 0x0400256B RID: 9579
	public string CurrentButton = string.Empty;

	// Token: 0x0400256C RID: 9580
	public bool NotNurse;

	// Token: 0x0400256D RID: 9581
	public bool Sabotage;

	// Token: 0x0400256E RID: 9582
	public bool Failure;

	// Token: 0x0400256F RID: 9583
	public bool Success;

	// Token: 0x04002570 RID: 9584
	public bool Show;

	// Token: 0x04002571 RID: 9585
	public int StartingAlerts;

	// Token: 0x04002572 RID: 9586
	public int ButtonID;

	// Token: 0x04002573 RID: 9587
	public int Progress;

	// Token: 0x04002574 RID: 9588
	public int ID;

	// Token: 0x04002575 RID: 9589
	public float Timer;
}
