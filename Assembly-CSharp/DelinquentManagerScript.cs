using System;
using UnityEngine;

// Token: 0x0200025C RID: 604
public class DelinquentManagerScript : MonoBehaviour
{
	// Token: 0x06001319 RID: 4889 RVA: 0x0009E263 File Offset: 0x0009C463
	private void Start()
	{
		this.Delinquents.SetActive(false);
		this.TimerMax = 15f;
		this.Timer = 15f;
		this.Phase++;
	}

	// Token: 0x0600131A RID: 4890 RVA: 0x0009E298 File Offset: 0x0009C498
	private void Update()
	{
		this.SpeechTimer = Mathf.MoveTowards(this.SpeechTimer, 0f, Time.deltaTime);
		if (this.Attacker != null && !this.Attacker.Attacking && this.Attacker.ExpressedSurprise && this.Attacker.Run && !this.Aggro)
		{
			AudioSource component = base.GetComponent<AudioSource>();
			component.clip = this.Attacker.AggroClips[UnityEngine.Random.Range(0, this.Attacker.AggroClips.Length)];
			component.Play();
			this.Aggro = true;
		}
		if (this.Panel.activeInHierarchy && this.Clock.HourTime > this.NextTime[this.Phase])
		{
			if (this.Phase == 3 && this.Clock.HourTime > 7.25f)
			{
				this.TimerMax = 75f;
				this.Timer = 75f;
				this.Phase++;
			}
			else if (this.Phase == 5 && this.Clock.HourTime > 8.5f)
			{
				this.TimerMax = 285f;
				this.Timer = 285f;
				this.Phase++;
			}
			else if (this.Phase == 7 && this.Clock.HourTime > 13.25f)
			{
				this.TimerMax = 15f;
				this.Timer = 15f;
				this.Phase++;
			}
			else if (this.Phase == 9 && this.Clock.HourTime > 13.5f)
			{
				this.TimerMax = 135f;
				this.Timer = 135f;
				this.Phase++;
			}
			if (this.Attacker == null)
			{
				this.Timer -= Time.deltaTime * (this.Clock.TimeSpeed / 60f);
			}
			this.Circle.fillAmount = 1f - this.Timer / this.TimerMax;
			if (this.Timer <= 0f)
			{
				this.Delinquents.SetActive(!this.Delinquents.activeInHierarchy);
				if (this.Phase < 8)
				{
					this.Phase++;
					return;
				}
				this.Delinquents.SetActive(false);
				this.Panel.SetActive(false);
			}
		}
	}

	// Token: 0x0600131B RID: 4891 RVA: 0x0009E510 File Offset: 0x0009C710
	public void CheckTime()
	{
		if (this.Clock.HourTime < 13f)
		{
			this.Delinquents.SetActive(false);
			this.TimerMax = 15f;
			this.Timer = 15f;
			this.Phase = 6;
			return;
		}
		if (this.Clock.HourTime < 15.5f)
		{
			this.Delinquents.SetActive(false);
			this.TimerMax = 15f;
			this.Timer = 15f;
			this.Phase = 8;
		}
	}

	// Token: 0x0600131C RID: 4892 RVA: 0x0009E594 File Offset: 0x0009C794
	public void EasterEgg()
	{
		this.RapBeat.SetActive(true);
		this.Mirror.Limit++;
	}

	// Token: 0x0400196E RID: 6510
	public GameObject Delinquents;

	// Token: 0x0400196F RID: 6511
	public GameObject RapBeat;

	// Token: 0x04001970 RID: 6512
	public GameObject Panel;

	// Token: 0x04001971 RID: 6513
	public float[] NextTime;

	// Token: 0x04001972 RID: 6514
	public DelinquentScript Attacker;

	// Token: 0x04001973 RID: 6515
	public MirrorScript Mirror;

	// Token: 0x04001974 RID: 6516
	public UILabel TimeLabel;

	// Token: 0x04001975 RID: 6517
	public ClockScript Clock;

	// Token: 0x04001976 RID: 6518
	public UISprite Circle;

	// Token: 0x04001977 RID: 6519
	public float SpeechTimer;

	// Token: 0x04001978 RID: 6520
	public float TimerMax;

	// Token: 0x04001979 RID: 6521
	public float Timer;

	// Token: 0x0400197A RID: 6522
	public bool Aggro;

	// Token: 0x0400197B RID: 6523
	public int Phase = 1;
}
