using System;
using UnityEngine;

// Token: 0x020000D8 RID: 216
public class BatheEventScript : MonoBehaviour
{
	// Token: 0x06000A3F RID: 2623 RVA: 0x00053FE0 File Offset: 0x000521E0
	private void Start()
	{
		this.RivalPhone.SetActive(false);
		if (DateGlobals.Weekday != this.EventDay)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06000A40 RID: 2624 RVA: 0x00054004 File Offset: 0x00052204
	private void Update()
	{
		if (!this.Clock.StopTime && !this.EventActive && this.Clock.HourTime > this.EventTime)
		{
			this.EventStudent = this.StudentManager.Students[30];
			if (this.EventStudent != null && !this.EventStudent.Distracted && !this.EventStudent.Talking && !this.EventStudent.Meeting && this.EventStudent.Indoors)
			{
				if (!this.EventStudent.WitnessedMurder)
				{
					this.OriginalPosition = this.EventStudent.Cosmetic.FemaleAccessories[3].transform.localPosition;
					this.EventStudent.CurrentDestination = this.StudentManager.FemaleStripSpot;
					this.EventStudent.Pathfinding.target = this.StudentManager.FemaleStripSpot;
					this.EventStudent.Character.GetComponent<Animation>().CrossFade(this.EventStudent.WalkAnim);
					this.EventStudent.Pathfinding.canSearch = true;
					this.EventStudent.Pathfinding.canMove = true;
					this.EventStudent.Pathfinding.speed = 1f;
					this.EventStudent.SpeechLines.Stop();
					this.EventStudent.DistanceToDestination = 100f;
					this.EventStudent.SmartPhone.SetActive(false);
					this.EventStudent.Obstacle.checkTime = 99f;
					this.EventStudent.InEvent = true;
					this.EventStudent.Private = true;
					this.EventStudent.Prompt.Hide();
					this.EventStudent.Hearts.Stop();
					this.EventActive = true;
					if (this.EventStudent.Following)
					{
						this.EventStudent.Pathfinding.canMove = true;
						this.EventStudent.Pathfinding.speed = 1f;
						this.EventStudent.Following = false;
						this.EventStudent.Routine = true;
						this.Yandere.Followers--;
						this.EventStudent.Subtitle.UpdateLabel(SubtitleType.StopFollowApology, 0, 3f);
						this.EventStudent.Prompt.Label[0].text = "     Talk";
					}
				}
				else
				{
					base.enabled = false;
				}
			}
		}
		if (this.EventActive)
		{
			if (this.Clock.HourTime > this.EventTime + 1f || this.EventStudent.WitnessedMurder || this.EventStudent.Splashed || this.EventStudent.Alarmed || this.EventStudent.Dying || !this.EventStudent.Alive)
			{
				this.EndEvent();
				return;
			}
			if (this.EventStudent.DistanceToDestination < 0.5f)
			{
				if (this.EventPhase == 1)
				{
					this.EventStudent.Routine = false;
					this.EventStudent.BathePhase = 1;
					this.EventStudent.Wet = true;
					this.EventPhase++;
				}
				else if (this.EventPhase == 2)
				{
					if (this.EventStudent.BathePhase == 4)
					{
						this.RivalPhone.SetActive(true);
						this.EventPhase++;
					}
				}
				else if (this.EventPhase == 3 && !this.EventStudent.Wet)
				{
					this.EndEvent();
				}
			}
			if (this.EventPhase == 4)
			{
				this.Timer += Time.deltaTime;
				if (this.Timer > this.CurrentClipLength + 1f)
				{
					this.EventStudent.Routine = true;
					this.EndEvent();
				}
			}
			float num = Vector3.Distance(this.Yandere.transform.position, this.EventStudent.transform.position);
			if (num < 11f)
			{
				if (num < 10f)
				{
					float num2 = Mathf.Abs((num - 10f) * 0.2f);
					if (num2 < 0f)
					{
						num2 = 0f;
					}
					if (num2 > 1f)
					{
						num2 = 1f;
					}
					this.EventSubtitle.transform.localScale = new Vector3(num2, num2, num2);
					return;
				}
				this.EventSubtitle.transform.localScale = Vector3.zero;
			}
		}
	}

	// Token: 0x06000A41 RID: 2625 RVA: 0x00054468 File Offset: 0x00052668
	private void EndEvent()
	{
		if (!this.EventOver)
		{
			if (this.VoiceClip != null)
			{
				UnityEngine.Object.Destroy(this.VoiceClip);
			}
			this.EventStudent.CurrentDestination = this.EventStudent.Destinations[this.EventStudent.Phase];
			this.EventStudent.Pathfinding.target = this.EventStudent.Destinations[this.EventStudent.Phase];
			this.EventStudent.Obstacle.checkTime = 1f;
			if (!this.EventStudent.Dying)
			{
				this.EventStudent.Prompt.enabled = true;
				this.EventStudent.Pathfinding.canSearch = true;
				this.EventStudent.Pathfinding.canMove = true;
				this.EventStudent.Pathfinding.speed = 1f;
				this.EventStudent.TargetDistance = 1f;
				this.EventStudent.Private = false;
			}
			this.EventStudent.InEvent = false;
			this.EventSubtitle.text = string.Empty;
			this.StudentManager.UpdateStudents(0);
		}
		this.EventActive = false;
		base.enabled = false;
	}

	// Token: 0x04000A6B RID: 2667
	public StudentManagerScript StudentManager;

	// Token: 0x04000A6C RID: 2668
	public YandereScript Yandere;

	// Token: 0x04000A6D RID: 2669
	public ClockScript Clock;

	// Token: 0x04000A6E RID: 2670
	public StudentScript EventStudent;

	// Token: 0x04000A6F RID: 2671
	public UILabel EventSubtitle;

	// Token: 0x04000A70 RID: 2672
	public AudioClip[] EventClip;

	// Token: 0x04000A71 RID: 2673
	public string[] EventSpeech;

	// Token: 0x04000A72 RID: 2674
	public string[] EventAnim;

	// Token: 0x04000A73 RID: 2675
	public GameObject RivalPhone;

	// Token: 0x04000A74 RID: 2676
	public GameObject VoiceClip;

	// Token: 0x04000A75 RID: 2677
	public bool EventActive;

	// Token: 0x04000A76 RID: 2678
	public bool EventOver;

	// Token: 0x04000A77 RID: 2679
	public float EventTime = 15.1f;

	// Token: 0x04000A78 RID: 2680
	public int EventPhase = 1;

	// Token: 0x04000A79 RID: 2681
	public DayOfWeek EventDay = DayOfWeek.Thursday;

	// Token: 0x04000A7A RID: 2682
	public Vector3 OriginalPosition;

	// Token: 0x04000A7B RID: 2683
	public float CurrentClipLength;

	// Token: 0x04000A7C RID: 2684
	public float Timer;
}
