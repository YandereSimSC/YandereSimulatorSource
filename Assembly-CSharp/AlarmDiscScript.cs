using System;
using UnityEngine;

// Token: 0x020000C1 RID: 193
public class AlarmDiscScript : MonoBehaviour
{
	// Token: 0x060009E5 RID: 2533 RVA: 0x0004D0F0 File Offset: 0x0004B2F0
	private void Start()
	{
		Vector3 localScale = base.transform.localScale;
		localScale.x *= 2f - SchoolGlobals.SchoolAtmosphere;
		localScale.z = localScale.x;
		base.transform.localScale = localScale;
	}

	// Token: 0x060009E6 RID: 2534 RVA: 0x0004D138 File Offset: 0x0004B338
	private void Update()
	{
		if (this.Frame > 0)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		else if (!this.NoScream)
		{
			if (!this.Long)
			{
				if (this.Originator != null)
				{
					this.Male = this.Originator.Male;
				}
				if (!this.Male)
				{
					this.PlayClip(this.FemaleScreams[UnityEngine.Random.Range(0, this.FemaleScreams.Length)], base.transform.position);
				}
				else if (this.Delinquent)
				{
					this.PlayClip(this.DelinquentScreams[UnityEngine.Random.Range(0, this.DelinquentScreams.Length)], base.transform.position);
				}
				else
				{
					this.PlayClip(this.MaleScreams[UnityEngine.Random.Range(0, this.MaleScreams.Length)], base.transform.position);
				}
			}
			else if (!this.Male)
			{
				this.PlayClip(this.LongFemaleScreams[UnityEngine.Random.Range(0, this.LongFemaleScreams.Length)], base.transform.position);
			}
			else
			{
				this.PlayClip(this.LongMaleScreams[UnityEngine.Random.Range(0, this.LongMaleScreams.Length)], base.transform.position);
			}
		}
		this.Frame++;
	}

	// Token: 0x060009E7 RID: 2535 RVA: 0x0004D284 File Offset: 0x0004B484
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 9)
		{
			this.Student = other.gameObject.GetComponent<StudentScript>();
			if (this.Student != null && this.Student.enabled && this.Student.DistractionSpot != new Vector3(base.transform.position.x, this.Student.transform.position.y, base.transform.position.z))
			{
				UnityEngine.Object.Destroy(this.Student.Giggle);
				this.Student.InvestigationTimer = 0f;
				this.Student.InvestigationPhase = 0;
				this.Student.Investigating = false;
				this.Student.DiscCheck = false;
				this.Student.VisionDistance += 1f;
				this.Student.ChalkDust.Stop();
				this.Student.CleanTimer = 0f;
				if (!this.Radio)
				{
					if (this.Student != this.Originator)
					{
						if (this.Student.Clock.Period == 3 && this.Student.BusyAtLunch)
						{
							this.StudentIsBusy = true;
						}
						if ((this.Student.StudentID == 47 || this.Student.StudentID == 49) && this.Student.StudentManager.ConvoManager.Confirmed)
						{
							this.StudentIsBusy = true;
						}
						if (this.Student.StudentID == this.Student.StudentManager.RivalID || this.Student.StudentID == 1)
						{
							StudentActionType currentAction = this.Student.CurrentAction;
						}
						if ((!this.Student.TurnOffRadio && this.Student.Alive && !this.Student.Pushed && !this.Student.Dying && !this.Student.Alarmed && !this.Student.Guarding && !this.Student.Wet && !this.Student.Slave && !this.Student.CheckingNote && !this.Student.WitnessedMurder && !this.Student.WitnessedCorpse && !this.StudentIsBusy && !this.Student.FocusOnYandere && !this.Student.Fleeing && !this.Student.Shoving && !this.Student.SentHome && this.Student.ClubActivityPhase < 16 && !this.Student.Vomiting && !this.Student.Lethal && !this.Student.Headache && !this.Student.Sedated && !this.Student.SenpaiWitnessingRivalDie) || (this.Student.Persona == PersonaType.Protective && this.Originator.StudentID == 11))
						{
							bool male = this.Student.Male;
							if (!this.Student.Struggling)
							{
								this.Student.Character.GetComponent<Animation>().CrossFade(this.Student.LeanAnim);
							}
							if (this.Originator != null)
							{
								if (this.Originator.WitnessedMurder)
								{
									this.Student.DistractionSpot = new Vector3(base.transform.position.x, this.Student.Yandere.transform.position.y, base.transform.position.z);
								}
								else if (this.Originator.Corpse == null)
								{
									this.Student.DistractionSpot = new Vector3(base.transform.position.x, this.Student.transform.position.y, base.transform.position.z);
								}
								else
								{
									this.Student.DistractionSpot = new Vector3(this.Originator.Corpse.transform.position.x, this.Student.transform.position.y, this.Originator.Corpse.transform.position.z);
								}
							}
							else
							{
								this.Student.DistractionSpot = new Vector3(base.transform.position.x, this.Student.transform.position.y, base.transform.position.z);
							}
							this.Student.DiscCheck = true;
							if (this.Shocking)
							{
								this.Student.Hesitation = 0.5f;
							}
							this.Student.Alarm = 200f;
							if (!this.NoScream)
							{
								this.Student.Giggle = null;
								this.InvestigateScream();
							}
							if (this.FocusOnYandere)
							{
								this.Student.FocusOnYandere = true;
							}
							if (this.Hesitation != 1f)
							{
								this.Student.Hesitation = this.Hesitation;
							}
						}
					}
				}
				else
				{
					Debug.Log("A student just heard a radio...");
					if (this.Student.Giggle != null)
					{
						this.Student.StopInvestigating();
					}
					if (!this.Student.Nemesis && this.Student.Alive && !this.Student.Dying && !this.Student.Guarding && !this.Student.Alarmed && !this.Student.Wet && !this.Student.Slave && !this.Student.Headache && !this.Student.WitnessedMurder && !this.Student.WitnessedCorpse && !this.Student.InEvent && !this.Student.Following && !this.Student.Distracting && this.Student.Actions[this.Student.Phase] != StudentActionType.Teaching && this.Student.Actions[this.Student.Phase] != StudentActionType.SitAndTakeNotes && !this.Student.GoAway && this.Student.Routine && !this.Student.CheckingNote && !this.Student.SentHome && this.Student.Persona != PersonaType.Protective && this.Student.CharacterAnimation != null && this.SourceRadio.Victim == null)
					{
						Debug.Log(this.Student.Name + " has just been alarmed by a radio!");
						this.Student.CharacterAnimation.CrossFade(this.Student.LeanAnim);
						this.Student.Pathfinding.canSearch = false;
						this.Student.Pathfinding.canMove = false;
						this.Student.EatingSnack = false;
						this.Student.Radio = this.SourceRadio;
						this.Student.TurnOffRadio = true;
						this.Student.Routine = false;
						this.Student.GoAway = false;
						bool flag = false;
						if (this.Student.Bento.activeInHierarchy && this.Student.StudentID > 1)
						{
							flag = true;
						}
						this.Student.EmptyHands();
						if (flag)
						{
							GenericBentoScript component = this.Student.Bento.GetComponent<GenericBentoScript>();
							component.enabled = true;
							component.Prompt.enabled = true;
							this.Student.Bento.SetActive(true);
							this.Student.Bento.transform.parent = this.Student.transform;
							if (this.Student.Male)
							{
								this.Student.Bento.transform.localPosition = new Vector3(0f, 0.4266666f, -0.075f);
							}
							else
							{
								this.Student.Bento.transform.localPosition = new Vector3(0f, 0.461f, -0.075f);
							}
							this.Student.Bento.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
							this.Student.Bento.transform.parent = null;
						}
						this.Student.SpeechLines.Stop();
						this.Student.ChalkDust.Stop();
						this.Student.CleanTimer = 0f;
						this.Student.RadioTimer = 0f;
						this.Student.ReadPhase = 0;
						this.SourceRadio.Victim = this.Student;
						if (this.Student.StudentID == 97 && SchemeGlobals.GetSchemeStage(5) == 3)
						{
							SchemeGlobals.SetSchemeStage(5, 4);
							this.Student.Yandere.PauseScreen.Schemes.UpdateInstructions();
							base.enabled = false;
						}
						this.Student.Yandere.NotificationManager.CustomText = this.Student.Name + " hears the radio.";
						this.Student.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
					}
				}
			}
		}
		this.Student = null;
	}

	// Token: 0x060009E8 RID: 2536 RVA: 0x0004DC44 File Offset: 0x0004BE44
	private void PlayClip(AudioClip clip, Vector3 pos)
	{
		GameObject gameObject = new GameObject("TempAudio");
		gameObject.transform.position = pos;
		AudioSource audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.clip = clip;
		audioSource.Play();
		UnityEngine.Object.Destroy(gameObject, clip.length);
		audioSource.rolloffMode = AudioRolloffMode.Linear;
		audioSource.minDistance = 5f;
		audioSource.maxDistance = 10f;
		audioSource.spatialBlend = 1f;
		audioSource.volume = 0.5f;
		if (this.Student != null)
		{
			this.Student.DeathScream = gameObject;
		}
	}

	// Token: 0x060009E9 RID: 2537 RVA: 0x0004DCD4 File Offset: 0x0004BED4
	private void InvestigateScream()
	{
		if (this.Student.Clock.Period == 3 && this.Student.BusyAtLunch)
		{
			this.StudentIsBusy = true;
		}
		if (!this.Student.YandereVisible && !this.Student.Alarmed && !this.Student.Distracted && !this.Student.Wet && !this.Student.Slave && !this.Student.WitnessedMurder && !this.Student.WitnessedCorpse && !this.Student.InEvent && !this.Student.Following && !this.Student.Confessing && !this.Student.Meeting && !this.Student.TurnOffRadio && !this.Student.Fleeing && !this.Student.Distracting && !this.Student.GoAway && !this.Student.FocusOnYandere && !this.StudentIsBusy && this.Student.Actions[this.Student.Phase] != StudentActionType.Teaching && this.Student.Actions[this.Student.Phase] != StudentActionType.SitAndTakeNotes && this.Student.Actions[this.Student.Phase] != StudentActionType.Graffiti && this.Student.Actions[this.Student.Phase] != StudentActionType.Bully && !this.Student.Headache)
		{
			this.Student.Character.GetComponent<Animation>().CrossFade(this.Student.IdleAnim);
			GameObject giggle = UnityEngine.Object.Instantiate<GameObject>(this.Student.EmptyGameObject, new Vector3(base.transform.position.x, this.Student.transform.position.y, base.transform.position.z), Quaternion.identity);
			this.Student.Giggle = giggle;
			if (this.Student.Pathfinding != null && !this.Student.Nemesis)
			{
				this.Student.Pathfinding.canSearch = false;
				this.Student.Pathfinding.canMove = false;
				this.Student.InvestigationPhase = 0;
				this.Student.InvestigationTimer = 0f;
				this.Student.Investigating = true;
				this.Student.EatingSnack = false;
				this.Student.SpeechLines.Stop();
				this.Student.ChalkDust.Stop();
				this.Student.DiscCheck = true;
				this.Student.Routine = false;
				this.Student.CleanTimer = 0f;
				this.Student.ReadPhase = 0;
				this.Student.StopPairing();
				this.Student.EmptyHands();
				this.Student.HeardScream = true;
			}
		}
	}

	// Token: 0x04000862 RID: 2146
	public AudioClip[] LongFemaleScreams;

	// Token: 0x04000863 RID: 2147
	public AudioClip[] LongMaleScreams;

	// Token: 0x04000864 RID: 2148
	public AudioClip[] FemaleScreams;

	// Token: 0x04000865 RID: 2149
	public AudioClip[] MaleScreams;

	// Token: 0x04000866 RID: 2150
	public AudioClip[] DelinquentScreams;

	// Token: 0x04000867 RID: 2151
	public StudentScript Originator;

	// Token: 0x04000868 RID: 2152
	public RadioScript SourceRadio;

	// Token: 0x04000869 RID: 2153
	public StudentScript Student;

	// Token: 0x0400086A RID: 2154
	public bool FocusOnYandere;

	// Token: 0x0400086B RID: 2155
	public bool StudentIsBusy;

	// Token: 0x0400086C RID: 2156
	public bool Delinquent;

	// Token: 0x0400086D RID: 2157
	public bool NoScream;

	// Token: 0x0400086E RID: 2158
	public bool Shocking;

	// Token: 0x0400086F RID: 2159
	public bool Radio;

	// Token: 0x04000870 RID: 2160
	public bool Male;

	// Token: 0x04000871 RID: 2161
	public bool Long;

	// Token: 0x04000872 RID: 2162
	public float Hesitation = 1f;

	// Token: 0x04000873 RID: 2163
	public int Frame;
}
