using System;
using UnityEngine;

// Token: 0x02000017 RID: 23
public class MusicAudienceScript : MonoBehaviour
{
	// Token: 0x0600009B RID: 155 RVA: 0x00007FB0 File Offset: 0x000061B0
	private void Start()
	{
		this.JumpStrength += UnityEngine.Random.Range(-0.0001f, 0.0001f);
	}

	// Token: 0x0600009C RID: 156 RVA: 0x00007FD0 File Offset: 0x000061D0
	private void Update()
	{
		if (this.MusicMinigame.Health >= this.Threshold)
		{
			this.Minimum = Mathf.MoveTowards(this.Minimum, 0.2f, Time.deltaTime * 0.1f);
		}
		else
		{
			this.Minimum = Mathf.MoveTowards(this.Minimum, 0f, Time.deltaTime * 0.1f);
		}
		base.transform.localPosition += new Vector3(0f, this.Jump, 0f);
		this.Jump -= Time.deltaTime * 0.01f;
		if (base.transform.localPosition.y < this.Minimum)
		{
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, this.Minimum, 0f);
			this.Jump = this.JumpStrength;
		}
	}

	// Token: 0x0400012F RID: 303
	public MusicMinigameScript MusicMinigame;

	// Token: 0x04000130 RID: 304
	public float JumpStrength;

	// Token: 0x04000131 RID: 305
	public float Threshold;

	// Token: 0x04000132 RID: 306
	public float Minimum;

	// Token: 0x04000133 RID: 307
	public float Jump;
}
