using System;
using UnityEngine;

// Token: 0x020002AF RID: 687
public class GateScript : MonoBehaviour
{
	// Token: 0x06001432 RID: 5170 RVA: 0x000B1D6C File Offset: 0x000AFF6C
	private void Update()
	{
		if (!this.ManuallyAdjusted)
		{
			if (this.Clock.PresentTime / 60f > 8f && this.Clock.PresentTime / 60f < 15.5f)
			{
				if (!this.Closed)
				{
					this.PlayAudio();
					this.Closed = true;
					if (this.EmergencyDoor.enabled)
					{
						this.EmergencyDoor.enabled = false;
					}
				}
			}
			else if (this.Closed)
			{
				this.PlayAudio();
				this.Closed = false;
				if (!this.EmergencyDoor.enabled)
				{
					this.EmergencyDoor.enabled = true;
				}
			}
		}
		if (this.StudentManager.Students[97] != null)
		{
			if (this.StudentManager.Students[97].CurrentAction == StudentActionType.AtLocker && this.StudentManager.Students[97].Routine && this.StudentManager.Students[97].Alive)
			{
				if (Vector3.Distance(this.StudentManager.Students[97].transform.position, this.StudentManager.Podiums.List[0].position) < 0.1f)
				{
					if (this.ManuallyAdjusted)
					{
						this.ManuallyAdjusted = false;
					}
					this.Prompt.enabled = false;
					this.Prompt.Hide();
				}
				else
				{
					this.Prompt.enabled = true;
				}
			}
			else
			{
				this.Prompt.enabled = true;
			}
		}
		else
		{
			this.Prompt.enabled = true;
		}
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			this.PlayAudio();
			this.EmergencyDoor.enabled = !this.EmergencyDoor.enabled;
			this.ManuallyAdjusted = true;
			this.Closed = !this.Closed;
			if (this.StudentManager.Students[97] != null && this.StudentManager.Students[97].Investigating)
			{
				this.StudentManager.Students[97].StopInvestigating();
			}
		}
		if (!this.Closed)
		{
			if (this.RightGate.localPosition.x != 7f)
			{
				this.RightGate.localPosition = new Vector3(Mathf.MoveTowards(this.RightGate.localPosition.x, 7f, Time.deltaTime), this.RightGate.localPosition.y, this.RightGate.localPosition.z);
				this.LeftGate.localPosition = new Vector3(Mathf.MoveTowards(this.LeftGate.localPosition.x, -7f, Time.deltaTime), this.LeftGate.localPosition.y, this.LeftGate.localPosition.z);
				if (!this.AudioPlayed && this.RightGate.localPosition.x == 7f)
				{
					this.RightGateAudio.clip = this.StopOpen;
					this.LeftGateAudio.clip = this.StopOpen;
					this.RightGateAudio.Play();
					this.LeftGateAudio.Play();
					this.RightGateLoop.Stop();
					this.LeftGateLoop.Stop();
					this.AudioPlayed = true;
					return;
				}
			}
		}
		else if (this.RightGate.localPosition.x != 2.325f)
		{
			if (this.RightGate.localPosition.x < 2.4f)
			{
				this.Crushing = true;
			}
			this.RightGate.localPosition = new Vector3(Mathf.MoveTowards(this.RightGate.localPosition.x, 2.325f, Time.deltaTime), this.RightGate.localPosition.y, this.RightGate.localPosition.z);
			this.LeftGate.localPosition = new Vector3(Mathf.MoveTowards(this.LeftGate.localPosition.x, -2.325f, Time.deltaTime), this.LeftGate.localPosition.y, this.LeftGate.localPosition.z);
			if (!this.AudioPlayed && this.RightGate.localPosition.x == 2.325f)
			{
				this.RightGateAudio.clip = this.StopOpen;
				this.LeftGateAudio.clip = this.StopOpen;
				this.RightGateAudio.Play();
				this.LeftGateAudio.Play();
				this.RightGateLoop.Stop();
				this.LeftGateLoop.Stop();
				this.AudioPlayed = true;
				this.Crushing = false;
			}
		}
	}

	// Token: 0x06001433 RID: 5171 RVA: 0x000B2230 File Offset: 0x000B0430
	public void PlayAudio()
	{
		this.RightGateAudio.clip = this.Start;
		this.LeftGateAudio.clip = this.Start;
		this.RightGateAudio.Play();
		this.LeftGateAudio.Play();
		this.RightGateLoop.Play();
		this.LeftGateLoop.Play();
		this.AudioPlayed = false;
	}

	// Token: 0x04001CB1 RID: 7345
	public StudentManagerScript StudentManager;

	// Token: 0x04001CB2 RID: 7346
	public PromptScript Prompt;

	// Token: 0x04001CB3 RID: 7347
	public ClockScript Clock;

	// Token: 0x04001CB4 RID: 7348
	public Collider EmergencyDoor;

	// Token: 0x04001CB5 RID: 7349
	public Collider GateCollider;

	// Token: 0x04001CB6 RID: 7350
	public Transform RightGate;

	// Token: 0x04001CB7 RID: 7351
	public Transform LeftGate;

	// Token: 0x04001CB8 RID: 7352
	public bool ManuallyAdjusted;

	// Token: 0x04001CB9 RID: 7353
	public bool AudioPlayed;

	// Token: 0x04001CBA RID: 7354
	public bool UpdateGates;

	// Token: 0x04001CBB RID: 7355
	public bool Crushing;

	// Token: 0x04001CBC RID: 7356
	public bool Closed;

	// Token: 0x04001CBD RID: 7357
	public AudioSource RightGateAudio;

	// Token: 0x04001CBE RID: 7358
	public AudioSource LeftGateAudio;

	// Token: 0x04001CBF RID: 7359
	public AudioSource RightGateLoop;

	// Token: 0x04001CC0 RID: 7360
	public AudioSource LeftGateLoop;

	// Token: 0x04001CC1 RID: 7361
	public AudioClip Start;

	// Token: 0x04001CC2 RID: 7362
	public AudioClip StopOpen;

	// Token: 0x04001CC3 RID: 7363
	public AudioClip StopClose;
}
