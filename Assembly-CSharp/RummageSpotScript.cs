using System;
using UnityEngine;

// Token: 0x020003A7 RID: 935
public class RummageSpotScript : MonoBehaviour
{
	// Token: 0x060019D3 RID: 6611 RVA: 0x000FC66C File Offset: 0x000FA86C
	private void Start()
	{
		if (this.ID == 1)
		{
			if (GameGlobals.AnswerSheetUnavailable)
			{
				Debug.Log("The answer sheet is no longer available, due to events on a previous day.");
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				base.gameObject.SetActive(false);
				return;
			}
			if (DateGlobals.Weekday == DayOfWeek.Friday && this.Clock.HourTime > 13.5f)
			{
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				base.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x060019D4 RID: 6612 RVA: 0x000FC6F4 File Offset: 0x000FA8F4
	private void Update()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
			{
				this.Yandere.EmptyHands();
				this.Yandere.CharacterAnimation.CrossFade("f02_rummage_00");
				this.Yandere.ProgressBar.transform.parent.gameObject.SetActive(true);
				this.Yandere.RummageSpot = this;
				this.Yandere.Rummaging = true;
				this.Yandere.CanMove = false;
				component.Play();
			}
		}
		if (this.Yandere.Rummaging)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.AlarmDisc, base.transform.position, Quaternion.identity);
			gameObject.GetComponent<AlarmDiscScript>().NoScream = true;
			gameObject.transform.localScale = new Vector3(750f, gameObject.transform.localScale.y, 750f);
		}
		if (this.Yandere.Noticed)
		{
			component.Stop();
		}
	}

	// Token: 0x060019D5 RID: 6613 RVA: 0x000FC834 File Offset: 0x000FAA34
	public void GetReward()
	{
		if (this.ID == 1)
		{
			if (this.Phase == 1)
			{
				SchemeGlobals.SetSchemeStage(5, 5);
				this.Schemes.UpdateInstructions();
				this.Yandere.Inventory.AnswerSheet = true;
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				this.DoorGap.Prompt.enabled = true;
				this.Phase++;
				return;
			}
			if (this.Phase == 2)
			{
				SchemeGlobals.SetSchemeStage(5, 8);
				this.Schemes.UpdateInstructions();
				this.Prompt.Yandere.Inventory.AnswerSheet = false;
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				base.gameObject.SetActive(false);
				this.Phase++;
			}
		}
	}

	// Token: 0x04002865 RID: 10341
	public GameObject AlarmDisc;

	// Token: 0x04002866 RID: 10342
	public DoorGapScript DoorGap;

	// Token: 0x04002867 RID: 10343
	public SchemesScript Schemes;

	// Token: 0x04002868 RID: 10344
	public YandereScript Yandere;

	// Token: 0x04002869 RID: 10345
	public PromptScript Prompt;

	// Token: 0x0400286A RID: 10346
	public ClockScript Clock;

	// Token: 0x0400286B RID: 10347
	public Transform Target;

	// Token: 0x0400286C RID: 10348
	public int Phase;

	// Token: 0x0400286D RID: 10349
	public int ID;
}
