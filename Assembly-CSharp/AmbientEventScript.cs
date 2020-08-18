using System;
using UnityEngine;

// Token: 0x020000C3 RID: 195
public class AmbientEventScript : MonoBehaviour
{
	// Token: 0x060009EE RID: 2542 RVA: 0x0004E4D0 File Offset: 0x0004C6D0
	private void Start()
	{
		if (DateGlobals.Weekday != this.EventDay)
		{
			base.enabled = false;
		}
	}

	// Token: 0x060009EF RID: 2543 RVA: 0x0004E4E8 File Offset: 0x0004C6E8
	private void Update()
	{
		if (!this.EventOn)
		{
			for (int i = 1; i < 3; i++)
			{
				if (this.EventStudent[i] == null)
				{
					this.EventStudent[i] = this.StudentManager.Students[this.StudentID[i]];
				}
				else if (!this.EventStudent[i].Alive || this.EventStudent[i].Slave)
				{
					base.enabled = false;
				}
			}
			if (this.Clock.HourTime > 13.001f && this.EventStudent[1] != null && this.EventStudent[2] != null && this.EventStudent[1].Pathfinding.canMove && this.EventStudent[2].Pathfinding.canMove)
			{
				this.EventStudent[1].CharacterAnimation.CrossFade(this.EventStudent[1].WalkAnim);
				this.EventStudent[1].CurrentDestination = this.EventLocation[1];
				this.EventStudent[1].Pathfinding.target = this.EventLocation[1];
				this.EventStudent[1].InEvent = true;
				this.EventStudent[2].CharacterAnimation.CrossFade(this.EventStudent[2].WalkAnim);
				this.EventStudent[2].CurrentDestination = this.EventLocation[2];
				this.EventStudent[2].Pathfinding.target = this.EventLocation[2];
				this.EventStudent[2].InEvent = true;
				this.EventOn = true;
				return;
			}
		}
		else
		{
			float num = Vector3.Distance(this.Yandere.transform.position, this.EventLocation[1].parent.position);
			if (this.Clock.HourTime > 13.5f || this.EventStudent[1].WitnessedCorpse || this.EventStudent[2].WitnessedCorpse || this.EventStudent[1].Alarmed || this.EventStudent[2].Alarmed || this.EventStudent[1].Dying || this.EventStudent[2].Dying)
			{
				this.EndEvent();
				return;
			}
			for (int j = 1; j < 3; j++)
			{
				if (!this.EventStudent[j].Pathfinding.canMove && !this.EventStudent[j].Private)
				{
					this.EventStudent[j].Character.GetComponent<Animation>().CrossFade(this.EventStudent[j].IdleAnim);
					this.EventStudent[j].Private = true;
					this.StudentManager.UpdateStudents(0);
				}
			}
			if (!this.EventStudent[1].Pathfinding.canMove && !this.EventStudent[2].Pathfinding.canMove)
			{
				if (!this.Spoken)
				{
					this.EventStudent[this.EventSpeaker[1]].CharacterAnimation.CrossFade(this.EventStudent[1].IdleAnim);
					this.EventStudent[this.EventSpeaker[2]].CharacterAnimation.CrossFade(this.EventStudent[2].IdleAnim);
					this.EventStudent[this.EventSpeaker[this.EventPhase]].PickRandomAnim();
					this.EventStudent[this.EventSpeaker[this.EventPhase]].CharacterAnimation.CrossFade(this.EventStudent[this.EventSpeaker[this.EventPhase]].RandomAnim);
					if (DateGlobals.Weekday == DayOfWeek.Monday && this.EventPhase == 13)
					{
						this.EventStudent[this.EventSpeaker[this.EventPhase]].CharacterAnimation.CrossFade("jojoPose_00");
					}
					AudioClipPlayer.Play(this.EventClip[this.EventPhase], this.EventStudent[this.EventSpeaker[this.EventPhase]].transform.position + Vector3.up * 1.5f, 5f, 10f, out this.VoiceClip, this.Yandere.transform.position.y);
					this.Spoken = true;
					return;
				}
				int num2 = this.EventSpeaker[this.EventPhase];
				if (this.EventStudent[num2].CharacterAnimation[this.EventStudent[num2].RandomAnim].time >= this.EventStudent[num2].CharacterAnimation[this.EventStudent[num2].RandomAnim].length)
				{
					this.EventStudent[num2].PickRandomAnim();
					this.EventStudent[num2].CharacterAnimation.CrossFade(this.EventStudent[num2].RandomAnim);
				}
				this.Timer += Time.deltaTime;
				if (this.Yandere.transform.position.y > this.EventLocation[1].parent.position.y - 1f && this.Yandere.transform.position.y < this.EventLocation[1].parent.position.y + 1f)
				{
					if (this.VoiceClip != null)
					{
						this.VoiceClip.GetComponent<AudioSource>().volume = 1f;
					}
					if (num < 10f)
					{
						if (this.Timer > this.EventClip[this.EventPhase].length)
						{
							this.EventSubtitle.text = string.Empty;
						}
						else
						{
							this.EventSubtitle.text = this.EventSpeech[this.EventPhase];
						}
						this.Scale = Mathf.Abs((num - 10f) * 0.2f);
						if (this.Scale < 0f)
						{
							this.Scale = 0f;
						}
						if (this.Scale > 1f)
						{
							this.Scale = 1f;
						}
						this.EventSubtitle.transform.localScale = new Vector3(this.Scale, this.Scale, this.Scale);
					}
					else
					{
						this.EventSubtitle.transform.localScale = Vector3.zero;
						this.EventSubtitle.text = string.Empty;
					}
				}
				else if (this.VoiceClip != null)
				{
					this.VoiceClip.GetComponent<AudioSource>().volume = 0f;
				}
				if (this.Timer > this.EventClip[this.EventPhase].length + 0.5f)
				{
					this.Spoken = false;
					this.EventPhase++;
					this.Timer = 0f;
					if (this.EventPhase == this.EventSpeech.Length)
					{
						this.EndEvent();
					}
				}
			}
		}
	}

	// Token: 0x060009F0 RID: 2544 RVA: 0x0004EB94 File Offset: 0x0004CD94
	public void EndEvent()
	{
		if (this.VoiceClip != null)
		{
			UnityEngine.Object.Destroy(this.VoiceClip);
		}
		for (int i = 1; i < 3; i++)
		{
			this.EventStudent[i].CurrentDestination = this.EventStudent[i].Destinations[this.EventStudent[i].Phase];
			this.EventStudent[i].Pathfinding.target = this.EventStudent[i].Destinations[this.EventStudent[i].Phase];
			this.EventStudent[i].InEvent = false;
			this.EventStudent[i].Private = false;
		}
		if (!this.StudentManager.Stop)
		{
			this.StudentManager.UpdateStudents(0);
		}
		this.EventSubtitle.text = string.Empty;
		base.enabled = false;
	}

	// Token: 0x04000888 RID: 2184
	public StudentManagerScript StudentManager;

	// Token: 0x04000889 RID: 2185
	public UILabel EventSubtitle;

	// Token: 0x0400088A RID: 2186
	public YandereScript Yandere;

	// Token: 0x0400088B RID: 2187
	public ClockScript Clock;

	// Token: 0x0400088C RID: 2188
	public StudentScript[] EventStudent;

	// Token: 0x0400088D RID: 2189
	public Transform[] EventLocation;

	// Token: 0x0400088E RID: 2190
	public AudioClip[] EventClip;

	// Token: 0x0400088F RID: 2191
	public string[] EventSpeech;

	// Token: 0x04000890 RID: 2192
	public string[] EventAnim;

	// Token: 0x04000891 RID: 2193
	public int[] EventSpeaker;

	// Token: 0x04000892 RID: 2194
	public GameObject VoiceClip;

	// Token: 0x04000893 RID: 2195
	public bool EventOn;

	// Token: 0x04000894 RID: 2196
	public bool Spoken;

	// Token: 0x04000895 RID: 2197
	public int EventPhase;

	// Token: 0x04000896 RID: 2198
	public float Timer;

	// Token: 0x04000897 RID: 2199
	public float Scale;

	// Token: 0x04000898 RID: 2200
	public int[] StudentID;

	// Token: 0x04000899 RID: 2201
	public DayOfWeek EventDay;
}
