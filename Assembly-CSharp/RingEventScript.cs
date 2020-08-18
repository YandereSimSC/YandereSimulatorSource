using System;
using UnityEngine;

// Token: 0x02000391 RID: 913
public class RingEventScript : MonoBehaviour
{
	// Token: 0x060019A6 RID: 6566 RVA: 0x000FA9E9 File Offset: 0x000F8BE9
	private void Start()
	{
		this.HoldingPosition = new Vector3(0.0075f, -0.0355f, 0.0175f);
		this.HoldingRotation = new Vector3(15f, -70f, -135f);
	}

	// Token: 0x060019A7 RID: 6567 RVA: 0x000FAA20 File Offset: 0x000F8C20
	private void Update()
	{
		if (!this.Clock.StopTime && !this.EventActive && this.Clock.HourTime > this.EventTime)
		{
			this.EventStudent = this.StudentManager.Students[2];
			if (this.EventStudent != null && !this.EventStudent.Distracted && !this.EventStudent.Talking)
			{
				if (!this.EventStudent.WitnessedMurder && !this.EventStudent.Bullied)
				{
					if (this.EventStudent.Cosmetic.FemaleAccessories[3].activeInHierarchy)
					{
						if (SchemeGlobals.GetSchemeStage(2) < 100)
						{
							this.RingPrompt = this.EventStudent.Cosmetic.FemaleAccessories[3].GetComponent<PromptScript>();
							this.RingCollider = this.EventStudent.Cosmetic.FemaleAccessories[3].GetComponent<BoxCollider>();
							this.OriginalPosition = this.EventStudent.Cosmetic.FemaleAccessories[3].transform.localPosition;
							this.EventStudent.CurrentDestination = this.EventStudent.Destinations[this.EventStudent.Phase];
							this.EventStudent.Pathfinding.target = this.EventStudent.Destinations[this.EventStudent.Phase];
							this.EventStudent.Obstacle.checkTime = 99f;
							this.EventStudent.InEvent = true;
							this.EventStudent.Private = true;
							this.EventStudent.Prompt.Hide();
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
					else
					{
						base.enabled = false;
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
			if (this.EventStudent.DistanceToDestination < 0.5f)
			{
				this.EventStudent.Pathfinding.canSearch = false;
				this.EventStudent.Pathfinding.canMove = false;
			}
			if (this.EventStudent.Alarmed && this.Yandere.TheftTimer > 0f)
			{
				Debug.Log("Event ended because Sakyu saw theft.");
				this.EventStudent.Cosmetic.FemaleAccessories[3].transform.parent = this.EventStudent.LeftMiddleFinger;
				this.EventStudent.Cosmetic.FemaleAccessories[3].transform.localPosition = this.OriginalPosition;
				this.EventStudent.Cosmetic.FemaleAccessories[3].transform.localEulerAngles = new Vector3(0f, 0f, 0f);
				this.RingCollider.gameObject.SetActive(true);
				this.RingCollider.enabled = false;
				this.Yandere.Inventory.Ring = false;
				this.EndEvent();
				return;
			}
			if (this.Clock.HourTime > this.EventTime + 0.5f || this.EventStudent.WitnessedMurder || this.EventStudent.Splashed || this.EventStudent.Alarmed || this.EventStudent.Dying || !this.EventStudent.Alive)
			{
				this.EndEvent();
				return;
			}
			if (!this.EventStudent.Pathfinding.canMove)
			{
				if (this.EventPhase == 1)
				{
					this.Timer += Time.deltaTime;
					this.EventStudent.Character.GetComponent<Animation>().CrossFade(this.EventAnim[0]);
					this.EventPhase++;
				}
				else if (this.EventPhase == 2)
				{
					this.Timer += Time.deltaTime;
					if (this.Timer > this.EventStudent.Character.GetComponent<Animation>()[this.EventAnim[0]].length)
					{
						this.EventStudent.Character.GetComponent<Animation>().CrossFade(this.EventStudent.EatAnim);
						this.EventStudent.Bento.transform.localPosition = new Vector3(-0.025f, -0.105f, 0f);
						this.EventStudent.Bento.transform.localEulerAngles = new Vector3(0f, 165f, 82.5f);
						this.EventStudent.Chopsticks[0].SetActive(true);
						this.EventStudent.Chopsticks[1].SetActive(true);
						this.EventStudent.Bento.SetActive(true);
						this.EventStudent.Lid.SetActive(false);
						this.RingCollider.enabled = true;
						this.EventPhase++;
						this.Timer = 0f;
					}
					else if (this.Timer > 4f)
					{
						if (this.EventStudent.Cosmetic.FemaleAccessories[3] != null)
						{
							this.EventStudent.Cosmetic.FemaleAccessories[3].transform.parent = null;
							this.EventStudent.Cosmetic.FemaleAccessories[3].transform.position = new Vector3(-2.707666f, 12.4695f, -31.136f);
							this.EventStudent.Cosmetic.FemaleAccessories[3].transform.eulerAngles = new Vector3(-20f, 180f, 0f);
						}
					}
					else if (this.Timer > 2.5f)
					{
						this.EventStudent.Cosmetic.FemaleAccessories[3].transform.parent = this.EventStudent.RightHand;
						this.EventStudent.Cosmetic.FemaleAccessories[3].transform.localPosition = this.HoldingPosition;
						this.EventStudent.Cosmetic.FemaleAccessories[3].transform.localEulerAngles = this.HoldingRotation;
					}
				}
				else if (this.EventPhase == 3)
				{
					if (this.Clock.HourTime > 13.375f)
					{
						this.EventStudent.Bento.SetActive(false);
						this.EventStudent.Chopsticks[0].SetActive(false);
						this.EventStudent.Chopsticks[1].SetActive(false);
						if (this.RingCollider != null)
						{
							this.RingCollider.enabled = false;
						}
						if (this.RingPrompt != null)
						{
							this.RingPrompt.Hide();
							this.RingPrompt.enabled = false;
						}
						this.EventStudent.Character.GetComponent<Animation>()[this.EventAnim[0]].time = this.EventStudent.Character.GetComponent<Animation>()[this.EventAnim[0]].length;
						this.EventStudent.Character.GetComponent<Animation>()[this.EventAnim[0]].speed = -1f;
						this.EventStudent.Character.GetComponent<Animation>().CrossFade((this.EventStudent.Cosmetic.FemaleAccessories[3] != null) ? this.EventAnim[0] : this.EventAnim[1]);
						this.EventPhase++;
					}
				}
				else if (this.EventPhase == 4)
				{
					this.Timer += Time.deltaTime;
					if (this.EventStudent.Cosmetic.FemaleAccessories[3] != null)
					{
						if (this.Timer > 2f)
						{
							this.EventStudent.Cosmetic.FemaleAccessories[3].transform.parent = this.EventStudent.RightHand;
							this.EventStudent.Cosmetic.FemaleAccessories[3].transform.localPosition = this.HoldingPosition;
							this.EventStudent.Cosmetic.FemaleAccessories[3].transform.localEulerAngles = this.HoldingRotation;
						}
						if (this.Timer > 3f)
						{
							this.EventStudent.Cosmetic.FemaleAccessories[3].transform.parent = this.EventStudent.LeftMiddleFinger;
							this.EventStudent.Cosmetic.FemaleAccessories[3].transform.localPosition = this.OriginalPosition;
							this.RingCollider.enabled = false;
						}
						if (this.Timer > 6f)
						{
							this.EndEvent();
						}
					}
					else if (this.Timer > 1.5f && this.Yandere.transform.position.z < 0f)
					{
						this.EventSubtitle.text = this.EventSpeech[0];
						AudioClipPlayer.Play(this.EventClip[0], this.EventStudent.transform.position + Vector3.up, 5f, 10f, out this.VoiceClip, out this.CurrentClipLength);
						this.EventPhase++;
					}
				}
				else if (this.EventPhase == 5)
				{
					this.Timer += Time.deltaTime;
					if (this.Timer > 9.5f)
					{
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
	}

	// Token: 0x060019A8 RID: 6568 RVA: 0x000FB498 File Offset: 0x000F9698
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
			}
			this.EventStudent.Pathfinding.speed = 1f;
			this.EventStudent.TargetDistance = 0.5f;
			this.EventStudent.InEvent = false;
			this.EventStudent.Private = false;
			this.EventSubtitle.text = string.Empty;
			this.StudentManager.UpdateStudents(0);
		}
		this.EventActive = false;
		base.enabled = false;
	}

	// Token: 0x060019A9 RID: 6569 RVA: 0x000FB5AC File Offset: 0x000F97AC
	public void ReturnRing()
	{
		if (this.EventStudent.Cosmetic.FemaleAccessories[3] != null)
		{
			this.EventStudent.Cosmetic.FemaleAccessories[3].transform.parent = this.EventStudent.LeftMiddleFinger;
			this.EventStudent.Cosmetic.FemaleAccessories[3].transform.localPosition = this.OriginalPosition;
			this.RingCollider.enabled = false;
			this.RingPrompt.Hide();
			this.RingPrompt.enabled = false;
		}
	}

	// Token: 0x04002796 RID: 10134
	public StudentManagerScript StudentManager;

	// Token: 0x04002797 RID: 10135
	public YandereScript Yandere;

	// Token: 0x04002798 RID: 10136
	public ClockScript Clock;

	// Token: 0x04002799 RID: 10137
	public StudentScript EventStudent;

	// Token: 0x0400279A RID: 10138
	public UILabel EventSubtitle;

	// Token: 0x0400279B RID: 10139
	public AudioClip[] EventClip;

	// Token: 0x0400279C RID: 10140
	public string[] EventSpeech;

	// Token: 0x0400279D RID: 10141
	public string[] EventAnim;

	// Token: 0x0400279E RID: 10142
	public GameObject VoiceClip;

	// Token: 0x0400279F RID: 10143
	public bool EventActive;

	// Token: 0x040027A0 RID: 10144
	public bool EventOver;

	// Token: 0x040027A1 RID: 10145
	public float EventTime = 13.1f;

	// Token: 0x040027A2 RID: 10146
	public int EventPhase = 1;

	// Token: 0x040027A3 RID: 10147
	public Vector3 OriginalPosition;

	// Token: 0x040027A4 RID: 10148
	public Vector3 HoldingPosition;

	// Token: 0x040027A5 RID: 10149
	public Vector3 HoldingRotation;

	// Token: 0x040027A6 RID: 10150
	public float CurrentClipLength;

	// Token: 0x040027A7 RID: 10151
	public float Timer;

	// Token: 0x040027A8 RID: 10152
	public PromptScript RingPrompt;

	// Token: 0x040027A9 RID: 10153
	public Collider RingCollider;
}
