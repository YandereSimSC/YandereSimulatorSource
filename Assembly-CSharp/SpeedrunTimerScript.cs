using System;
using UnityEngine;

// Token: 0x020003E9 RID: 1001
public class SpeedrunTimerScript : MonoBehaviour
{
	// Token: 0x06001ABE RID: 6846 RVA: 0x0010C38D File Offset: 0x0010A58D
	private void Start()
	{
		this.Label.enabled = false;
	}

	// Token: 0x06001ABF RID: 6847 RVA: 0x0010C39C File Offset: 0x0010A59C
	private void Update()
	{
		if (!this.Police.FadeOut)
		{
			this.Timer += Time.deltaTime;
			if (this.Label.enabled)
			{
				this.Label.text = (this.FormatTime(this.Timer) ?? "");
			}
			if (Input.GetKeyDown(KeyCode.Delete))
			{
				this.Label.enabled = !this.Label.enabled;
			}
		}
	}

	// Token: 0x06001AC0 RID: 6848 RVA: 0x0010C418 File Offset: 0x0010A618
	private string FormatTime(float time)
	{
		int num = (int)time;
		int num2 = num / 60;
		int num3 = num % 60;
		float num4 = time * 1000f;
		num4 %= 1000f;
		return string.Format("{0:00}:{1:00}:{2:000}", num2, num3, num4);
	}

	// Token: 0x04002B3C RID: 11068
	public PoliceScript Police;

	// Token: 0x04002B3D RID: 11069
	public UILabel Label;

	// Token: 0x04002B3E RID: 11070
	public float Timer;
}
