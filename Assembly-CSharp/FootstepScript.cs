using System;
using UnityEngine;

// Token: 0x020002A8 RID: 680
public class FootstepScript : MonoBehaviour
{
	// Token: 0x06001417 RID: 5143 RVA: 0x000B0148 File Offset: 0x000AE348
	private void Start()
	{
		if (!this.Student.Nemesis)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06001418 RID: 5144 RVA: 0x000B0160 File Offset: 0x000AE360
	private void Update()
	{
		if (!this.FootUp)
		{
			if (base.transform.position.y > this.Student.transform.position.y + this.UpThreshold)
			{
				this.FootUp = true;
				return;
			}
		}
		else if (base.transform.position.y < this.Student.transform.position.y + this.DownThreshold)
		{
			if (this.FootUp)
			{
				if (this.Student.Pathfinding.speed > 1f)
				{
					this.MyAudio.clip = this.RunFootsteps[UnityEngine.Random.Range(0, this.RunFootsteps.Length)];
					this.MyAudio.volume = 0.2f;
				}
				else
				{
					this.MyAudio.clip = this.WalkFootsteps[UnityEngine.Random.Range(0, this.WalkFootsteps.Length)];
					this.MyAudio.volume = 0.1f;
				}
				this.MyAudio.Play();
			}
			this.FootUp = false;
		}
	}

	// Token: 0x04001C5D RID: 7261
	public StudentScript Student;

	// Token: 0x04001C5E RID: 7262
	public AudioSource MyAudio;

	// Token: 0x04001C5F RID: 7263
	public AudioClip[] WalkFootsteps;

	// Token: 0x04001C60 RID: 7264
	public AudioClip[] RunFootsteps;

	// Token: 0x04001C61 RID: 7265
	public float DownThreshold = 0.02f;

	// Token: 0x04001C62 RID: 7266
	public float UpThreshold = 0.025f;

	// Token: 0x04001C63 RID: 7267
	public bool FootUp;
}
