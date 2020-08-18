using System;
using UnityEngine;

// Token: 0x020003FF RID: 1023
public class StruggleBarScript : MonoBehaviour
{
	// Token: 0x06001B09 RID: 6921 RVA: 0x00110CC1 File Offset: 0x0010EEC1
	private void Start()
	{
		base.transform.localScale = Vector3.zero;
		this.ChooseButton();
	}

	// Token: 0x06001B0A RID: 6922 RVA: 0x00110CDC File Offset: 0x0010EEDC
	private void Update()
	{
		if (this.Struggling)
		{
			this.Intensity = Mathf.MoveTowards(this.Intensity, 1f, Time.deltaTime);
			base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
			this.Spikes.localEulerAngles = new Vector3(this.Spikes.localEulerAngles.x, this.Spikes.localEulerAngles.y, this.Spikes.localEulerAngles.z - Time.deltaTime * 360f);
			this.Victory -= Time.deltaTime * 10f * this.Strength * this.Intensity;
			if (ClubGlobals.Club == ClubType.MartialArts)
			{
				this.Victory = 100f;
			}
			if (Input.GetButtonDown(this.CurrentButton))
			{
				if (this.Invincible)
				{
					this.Victory += 100f;
				}
				this.Victory += Time.deltaTime * (500f + (float)(ClassGlobals.PhysicalGrade + ClassGlobals.PhysicalBonus) * 150f) * this.Intensity;
			}
			if (this.Victory >= 100f)
			{
				this.Victory = 100f;
			}
			if (this.Victory <= -100f)
			{
				this.Victory = -100f;
			}
			UISprite uisprite = this.ButtonPrompts[this.ButtonID];
			uisprite.transform.localPosition = new Vector3(Mathf.Lerp(uisprite.transform.localPosition.x, this.Victory * 6.5f, Time.deltaTime * 10f), uisprite.transform.localPosition.y, uisprite.transform.localPosition.z);
			this.Spikes.localPosition = new Vector3(uisprite.transform.localPosition.x, this.Spikes.localPosition.y, this.Spikes.localPosition.z);
			if (this.Victory == 100f)
			{
				Debug.Log("Yandere-chan just won a struggle against " + this.Student.Name + ".");
				this.Yandere.Won = true;
				this.Student.Lost = true;
				this.Struggling = false;
				this.Victory = 0f;
				return;
			}
			if (this.Victory == -100f)
			{
				if (!this.Invincible)
				{
					this.HeroWins();
					return;
				}
			}
			else
			{
				this.ButtonTimer += Time.deltaTime;
				if (this.ButtonTimer >= 1f)
				{
					this.ChooseButton();
					this.ButtonTimer = 0f;
					this.Intensity = 0f;
					return;
				}
			}
		}
		else
		{
			if (base.transform.localScale.x > 0.1f)
			{
				base.transform.localScale = Vector3.Lerp(base.transform.localScale, Vector3.zero, Time.deltaTime * 10f);
				return;
			}
			base.transform.localScale = Vector3.zero;
			if (!this.Yandere.AttackManager.Censor)
			{
				base.gameObject.SetActive(false);
				return;
			}
			if (this.AttackTimer == 0f)
			{
				this.Yandere.Blur.enabled = true;
				this.Yandere.Blur.blurSize = 0f;
				this.Yandere.Blur.blurIterations = 0;
			}
			this.AttackTimer += Time.deltaTime;
			if (this.AttackTimer < 2.5f)
			{
				this.Yandere.Blur.blurSize = Mathf.MoveTowards(this.Yandere.Blur.blurSize, 10f, Time.deltaTime * 10f);
				if (this.Yandere.Blur.blurSize > (float)this.Yandere.Blur.blurIterations)
				{
					this.Yandere.Blur.blurIterations++;
					return;
				}
			}
			else
			{
				this.Yandere.Blur.blurSize = Mathf.Lerp(this.Yandere.Blur.blurSize, 0f, Time.deltaTime * 10f);
				if (this.Yandere.Blur.blurSize < (float)this.Yandere.Blur.blurIterations)
				{
					this.Yandere.Blur.blurIterations--;
				}
				if (this.AttackTimer >= 3f)
				{
					base.gameObject.SetActive(false);
					this.Yandere.Blur.enabled = false;
					this.Yandere.Blur.blurSize = 0f;
					this.Yandere.Blur.blurIterations = 0;
					this.AttackTimer = 0f;
				}
			}
		}
	}

	// Token: 0x06001B0B RID: 6923 RVA: 0x001111CC File Offset: 0x0010F3CC
	public void HeroWins()
	{
		if (this.Yandere.Armed)
		{
			this.Yandere.EquippedWeapon.Drop();
		}
		this.Yandere.Lost = true;
		this.Student.Won = true;
		this.Struggling = false;
		this.Victory = 0f;
		this.Yandere.StudentManager.StopMoving();
	}

	// Token: 0x06001B0C RID: 6924 RVA: 0x00111230 File Offset: 0x0010F430
	private void ChooseButton()
	{
		int buttonID = this.ButtonID;
		for (int i = 1; i < 5; i++)
		{
			this.ButtonPrompts[i].enabled = false;
			this.ButtonPrompts[i].transform.localPosition = this.ButtonPrompts[buttonID].transform.localPosition;
		}
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
	}

	// Token: 0x04002C20 RID: 11296
	public ShoulderCameraScript ShoulderCamera;

	// Token: 0x04002C21 RID: 11297
	public PromptSwapScript ButtonPrompt;

	// Token: 0x04002C22 RID: 11298
	public UISprite[] ButtonPrompts;

	// Token: 0x04002C23 RID: 11299
	public YandereScript Yandere;

	// Token: 0x04002C24 RID: 11300
	public StudentScript Student;

	// Token: 0x04002C25 RID: 11301
	public Transform Spikes;

	// Token: 0x04002C26 RID: 11302
	public string CurrentButton = string.Empty;

	// Token: 0x04002C27 RID: 11303
	public bool Struggling;

	// Token: 0x04002C28 RID: 11304
	public bool Invincible;

	// Token: 0x04002C29 RID: 11305
	public float AttackTimer;

	// Token: 0x04002C2A RID: 11306
	public float ButtonTimer;

	// Token: 0x04002C2B RID: 11307
	public float Intensity;

	// Token: 0x04002C2C RID: 11308
	public float Strength = 1f;

	// Token: 0x04002C2D RID: 11309
	public float Struggle;

	// Token: 0x04002C2E RID: 11310
	public float Victory;

	// Token: 0x04002C2F RID: 11311
	public int ButtonID;
}
