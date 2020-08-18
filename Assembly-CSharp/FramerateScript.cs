using System;
using UnityEngine;

// Token: 0x020002AA RID: 682
public class FramerateScript : MonoBehaviour
{
	// Token: 0x0600141D RID: 5149 RVA: 0x000B041B File Offset: 0x000AE61B
	private void Start()
	{
		this.timeleft = this.updateInterval;
	}

	// Token: 0x0600141E RID: 5150 RVA: 0x000B042C File Offset: 0x000AE62C
	private void Update()
	{
		this.timeleft -= Time.deltaTime;
		this.accum += Time.timeScale / Time.deltaTime;
		this.frames++;
		if (this.timeleft <= 0f)
		{
			this.FPS = this.accum / (float)this.frames;
			int num = Mathf.Clamp((int)this.FPS, 0, Application.targetFrameRate);
			if (num > 0)
			{
				this.FPSLabel.text = "FPS: " + num.ToString();
			}
			this.timeleft = this.updateInterval;
			this.accum = 0f;
			this.frames = 0;
		}
	}

	// Token: 0x04001C6C RID: 7276
	public float updateInterval = 0.5f;

	// Token: 0x04001C6D RID: 7277
	private float accum;

	// Token: 0x04001C6E RID: 7278
	private int frames;

	// Token: 0x04001C6F RID: 7279
	private float timeleft;

	// Token: 0x04001C70 RID: 7280
	public float FPS;

	// Token: 0x04001C71 RID: 7281
	public UILabel FPSLabel;
}
