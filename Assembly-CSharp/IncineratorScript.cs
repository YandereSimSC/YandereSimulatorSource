using System;
using UnityEngine;

// Token: 0x02000300 RID: 768
public class IncineratorScript : MonoBehaviour
{
	// Token: 0x06001769 RID: 5993 RVA: 0x000C9CB0 File Offset: 0x000C7EB0
	private void Start()
	{
		this.Panel.SetActive(false);
		this.Prompt.enabled = true;
	}

	// Token: 0x0600176A RID: 5994 RVA: 0x000C9CCC File Offset: 0x000C7ECC
	private void Update()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		if (!this.Open)
		{
			this.RightDoor.transform.localEulerAngles = new Vector3(this.RightDoor.transform.localEulerAngles.x, Mathf.MoveTowards(this.RightDoor.transform.localEulerAngles.y, 0f, Time.deltaTime * 360f), this.RightDoor.transform.localEulerAngles.z);
			this.LeftDoor.transform.localEulerAngles = new Vector3(this.LeftDoor.transform.localEulerAngles.x, Mathf.MoveTowards(this.LeftDoor.transform.localEulerAngles.y, 0f, Time.deltaTime * 360f), this.LeftDoor.transform.localEulerAngles.z);
			if (this.RightDoor.transform.localEulerAngles.y < 36f)
			{
				if (this.RightDoor.transform.localEulerAngles.y > 0f)
				{
					component.clip = this.IncineratorClose;
					component.Play();
				}
				this.RightDoor.transform.localEulerAngles = new Vector3(this.RightDoor.transform.localEulerAngles.x, 0f, this.RightDoor.transform.localEulerAngles.z);
			}
		}
		else
		{
			if (this.RightDoor.transform.localEulerAngles.y == 0f)
			{
				component.clip = this.IncineratorOpen;
				component.Play();
			}
			this.RightDoor.transform.localEulerAngles = new Vector3(this.RightDoor.transform.localEulerAngles.x, Mathf.Lerp(this.RightDoor.transform.localEulerAngles.y, 135f, Time.deltaTime * 10f), this.RightDoor.transform.localEulerAngles.z);
			this.LeftDoor.transform.localEulerAngles = new Vector3(this.LeftDoor.transform.localEulerAngles.x, Mathf.Lerp(this.LeftDoor.transform.localEulerAngles.y, 135f, Time.deltaTime * 10f), this.LeftDoor.transform.localEulerAngles.z);
			if (this.RightDoor.transform.localEulerAngles.y > 134f)
			{
				this.RightDoor.transform.localEulerAngles = new Vector3(this.RightDoor.transform.localEulerAngles.x, 135f, this.RightDoor.transform.localEulerAngles.z);
			}
		}
		if (this.OpenTimer > 0f)
		{
			this.OpenTimer -= Time.deltaTime;
			if (this.OpenTimer <= 1f)
			{
				this.Open = false;
			}
			if (this.OpenTimer <= 0f)
			{
				this.Prompt.enabled = true;
			}
		}
		else if (!this.Smoke.isPlaying)
		{
			this.YandereHoldingEvidence = (this.Yandere.Ragdoll != null);
			if (!this.YandereHoldingEvidence)
			{
				if (this.Yandere.PickUp != null)
				{
					this.YandereHoldingEvidence = (this.Yandere.PickUp.Evidence || this.Yandere.PickUp.Garbage);
				}
				else
				{
					this.YandereHoldingEvidence = false;
				}
			}
			if (!this.YandereHoldingEvidence)
			{
				if (this.Yandere.EquippedWeapon != null)
				{
					this.YandereHoldingEvidence = this.Yandere.EquippedWeapon.MurderWeapon;
				}
				else
				{
					this.YandereHoldingEvidence = false;
				}
			}
			if (!this.YandereHoldingEvidence)
			{
				if (!this.Prompt.HideButton[3])
				{
					this.Prompt.HideButton[3] = true;
				}
			}
			else if (this.Prompt.HideButton[3])
			{
				this.Prompt.HideButton[3] = false;
			}
			if ((this.Yandere.Chased || this.Yandere.Chasers > 0 || !this.YandereHoldingEvidence) && !this.Prompt.HideButton[3])
			{
				this.Prompt.HideButton[3] = true;
			}
			if (this.Ready)
			{
				if (!this.Smoke.isPlaying)
				{
					if (this.Prompt.HideButton[0])
					{
						this.Prompt.HideButton[0] = false;
					}
				}
				else if (!this.Prompt.HideButton[0])
				{
					this.Prompt.HideButton[0] = true;
				}
			}
		}
		if (this.Prompt.Circle[3].fillAmount == 0f)
		{
			Time.timeScale = 1f;
			if (this.Yandere.Ragdoll != null)
			{
				this.Yandere.Character.GetComponent<Animation>().CrossFade(this.Yandere.Carrying ? "f02_carryIdleA_00" : "f02_dragIdle_00");
				this.Yandere.YandereVision = false;
				this.Yandere.CanMove = false;
				this.Yandere.Dumping = true;
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				this.Victims++;
				this.VictimList[this.Victims] = this.Yandere.Ragdoll.GetComponent<RagdollScript>().StudentID;
				this.Open = true;
			}
			if (this.Yandere.PickUp != null)
			{
				if (this.Yandere.PickUp.BodyPart != null)
				{
					this.Limbs++;
					this.LimbList[this.Limbs] = this.Yandere.PickUp.GetComponent<BodyPartScript>().StudentID;
				}
				this.Yandere.PickUp.Incinerator = this;
				this.Yandere.PickUp.Dumped = true;
				this.Yandere.PickUp.Drop();
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				this.OpenTimer = 2f;
				this.Ready = true;
				this.Open = true;
			}
			WeaponScript equippedWeapon = this.Yandere.EquippedWeapon;
			if (equippedWeapon != null)
			{
				this.DestroyedEvidence++;
				this.EvidenceList[this.DestroyedEvidence] = equippedWeapon.WeaponID;
				equippedWeapon.Incinerator = this;
				equippedWeapon.Dumped = true;
				equippedWeapon.Drop();
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				this.OpenTimer = 2f;
				this.Ready = true;
				this.Open = true;
			}
		}
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			this.Panel.SetActive(true);
			this.Timer = 60f;
			component.clip = this.IncineratorActivate;
			component.Play();
			this.Flames.Play();
			this.Smoke.Play();
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			this.Yandere.Police.IncineratedWeapons += this.MurderWeapons;
			this.Yandere.Police.BloodyClothing -= this.BloodyClothing;
			this.Yandere.Police.BloodyWeapons -= this.MurderWeapons;
			this.Yandere.Police.BodyParts -= this.BodyParts;
			this.Yandere.Police.Corpses -= this.Corpses;
			if (this.Yandere.Police.SuicideScene && this.Yandere.Police.Corpses == 1)
			{
				this.Yandere.Police.MurderScene = false;
			}
			if (this.Yandere.Police.Corpses == 0)
			{
				this.Yandere.Police.MurderScene = false;
			}
			this.BloodyClothing = 0;
			this.MurderWeapons = 0;
			this.BodyParts = 0;
			this.Corpses = 0;
			this.ID = 0;
			while (this.ID < 101)
			{
				if (this.Yandere.StudentManager.Students[this.CorpseList[this.ID]] != null)
				{
					this.Yandere.StudentManager.Students[this.CorpseList[this.ID]].Ragdoll.Disposed = true;
					this.ConfirmedDead[this.ID] = this.CorpseList[this.ID];
					if (this.Yandere.StudentManager.Students[this.CorpseList[this.ID]].Ragdoll.Drowned)
					{
						this.Yandere.Police.DrownVictims--;
					}
				}
				this.ID++;
			}
		}
		if (this.Smoke.isPlaying)
		{
			this.Timer -= Time.deltaTime * (this.Clock.TimeSpeed / 60f);
			this.FlameSound.volume += Time.deltaTime;
			this.Circle.fillAmount = 1f - this.Timer / 60f;
			if (this.Timer <= 0f)
			{
				this.Prompt.HideButton[0] = true;
				this.Prompt.enabled = true;
				this.Panel.SetActive(false);
				this.Ready = false;
				this.Flames.Stop();
				this.Smoke.Stop();
			}
		}
		else
		{
			this.FlameSound.volume -= Time.deltaTime;
		}
		if (this.Panel.activeInHierarchy)
		{
			float num = (float)Mathf.CeilToInt(this.Timer * 60f);
			float num2 = Mathf.Floor(num / 60f);
			float num3 = (float)Mathf.RoundToInt(num % 60f);
			this.TimeLabel.text = string.Format("{0:00}:{1:00}", num2, num3);
		}
	}

	// Token: 0x0600176B RID: 5995 RVA: 0x000CA734 File Offset: 0x000C8934
	public void SetVictimsMissing()
	{
		int[] confirmedDead = this.ConfirmedDead;
		for (int i = 0; i < confirmedDead.Length; i++)
		{
			StudentGlobals.SetStudentMissing(confirmedDead[i], true);
		}
	}

	// Token: 0x04002075 RID: 8309
	public YandereScript Yandere;

	// Token: 0x04002076 RID: 8310
	public PromptScript Prompt;

	// Token: 0x04002077 RID: 8311
	public ClockScript Clock;

	// Token: 0x04002078 RID: 8312
	public AudioClip IncineratorActivate;

	// Token: 0x04002079 RID: 8313
	public AudioClip IncineratorClose;

	// Token: 0x0400207A RID: 8314
	public AudioClip IncineratorOpen;

	// Token: 0x0400207B RID: 8315
	public AudioSource FlameSound;

	// Token: 0x0400207C RID: 8316
	public ParticleSystem Flames;

	// Token: 0x0400207D RID: 8317
	public ParticleSystem Smoke;

	// Token: 0x0400207E RID: 8318
	public Transform DumpPoint;

	// Token: 0x0400207F RID: 8319
	public Transform RightDoor;

	// Token: 0x04002080 RID: 8320
	public Transform LeftDoor;

	// Token: 0x04002081 RID: 8321
	public GameObject Panel;

	// Token: 0x04002082 RID: 8322
	public UILabel TimeLabel;

	// Token: 0x04002083 RID: 8323
	public UISprite Circle;

	// Token: 0x04002084 RID: 8324
	public bool YandereHoldingEvidence;

	// Token: 0x04002085 RID: 8325
	public bool Ready;

	// Token: 0x04002086 RID: 8326
	public bool Open;

	// Token: 0x04002087 RID: 8327
	public int DestroyedEvidence;

	// Token: 0x04002088 RID: 8328
	public int BloodyClothing;

	// Token: 0x04002089 RID: 8329
	public int MurderWeapons;

	// Token: 0x0400208A RID: 8330
	public int BodyParts;

	// Token: 0x0400208B RID: 8331
	public int Corpses;

	// Token: 0x0400208C RID: 8332
	public int Victims;

	// Token: 0x0400208D RID: 8333
	public int Limbs;

	// Token: 0x0400208E RID: 8334
	public int ID;

	// Token: 0x0400208F RID: 8335
	public float OpenTimer;

	// Token: 0x04002090 RID: 8336
	public float Timer;

	// Token: 0x04002091 RID: 8337
	public int[] EvidenceList;

	// Token: 0x04002092 RID: 8338
	public int[] CorpseList;

	// Token: 0x04002093 RID: 8339
	public int[] VictimList;

	// Token: 0x04002094 RID: 8340
	public int[] LimbList;

	// Token: 0x04002095 RID: 8341
	public int[] ConfirmedDead;
}
