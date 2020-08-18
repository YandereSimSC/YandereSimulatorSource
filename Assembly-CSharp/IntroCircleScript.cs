using System;
using UnityEngine;

// Token: 0x02000307 RID: 775
public class IntroCircleScript : MonoBehaviour
{
	// Token: 0x06001778 RID: 6008 RVA: 0x000CB278 File Offset: 0x000C9478
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.ID < this.StartTime.Length && this.Timer > this.StartTime[this.ID])
		{
			this.CurrentTime = this.Duration[this.ID];
			this.LastTime = this.Duration[this.ID];
			this.Label.text = this.Text[this.ID];
			this.ID++;
		}
		if (this.CurrentTime > 0f)
		{
			this.CurrentTime -= Time.deltaTime;
		}
		if (this.Timer > 1f)
		{
			this.Sprite.fillAmount = this.CurrentTime / this.LastTime;
			if (this.Sprite.fillAmount == 0f)
			{
				this.Label.text = string.Empty;
			}
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			this.CurrentTime -= 5f;
			this.Timer += 5f;
		}
	}

	// Token: 0x040020CF RID: 8399
	public UISprite Sprite;

	// Token: 0x040020D0 RID: 8400
	public UILabel Label;

	// Token: 0x040020D1 RID: 8401
	public float[] StartTime;

	// Token: 0x040020D2 RID: 8402
	public float[] Duration;

	// Token: 0x040020D3 RID: 8403
	public string[] Text;

	// Token: 0x040020D4 RID: 8404
	public float CurrentTime;

	// Token: 0x040020D5 RID: 8405
	public float LastTime;

	// Token: 0x040020D6 RID: 8406
	public float Timer;

	// Token: 0x040020D7 RID: 8407
	public int ID;
}
