using System;
using UnityEngine;
using XInputDotNetPure;

// Token: 0x0200045C RID: 1116
public class VibrationScript : MonoBehaviour
{
	// Token: 0x06001CEE RID: 7406 RVA: 0x00156027 File Offset: 0x00154227
	private void Start()
	{
		GamePad.SetVibration(PlayerIndex.One, this.Strength1, this.Strength2);
	}

	// Token: 0x06001CEF RID: 7407 RVA: 0x0015603B File Offset: 0x0015423B
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer > this.TimeLimit)
		{
			GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
			base.enabled = false;
		}
	}

	// Token: 0x04003605 RID: 13829
	public float Strength1;

	// Token: 0x04003606 RID: 13830
	public float Strength2;

	// Token: 0x04003607 RID: 13831
	public float TimeLimit;

	// Token: 0x04003608 RID: 13832
	public float Timer;
}
