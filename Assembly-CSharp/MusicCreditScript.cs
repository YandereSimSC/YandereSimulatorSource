using System;
using UnityEngine;

// Token: 0x02000339 RID: 825
public class MusicCreditScript : MonoBehaviour
{
	// Token: 0x06001843 RID: 6211 RVA: 0x000D9E08 File Offset: 0x000D8008
	private void Start()
	{
		base.transform.localPosition = new Vector3(400f, base.transform.localPosition.y, base.transform.localPosition.z);
		this.Panel.enabled = false;
	}

	// Token: 0x06001844 RID: 6212 RVA: 0x000D9E58 File Offset: 0x000D8058
	private void Update()
	{
		if (this.Slide)
		{
			this.Timer += Time.deltaTime;
			if (this.Timer < 5f)
			{
				base.transform.localPosition = new Vector3(Mathf.Lerp(base.transform.localPosition.x, 0f, Time.deltaTime * 10f), base.transform.localPosition.y, base.transform.localPosition.z);
				return;
			}
			base.transform.localPosition = new Vector3(base.transform.localPosition.x + Time.deltaTime, base.transform.localPosition.y, base.transform.localPosition.z);
			base.transform.localPosition = new Vector3(base.transform.localPosition.x + Mathf.Abs(base.transform.localPosition.x * 0.01f) * (Time.deltaTime * 1000f), base.transform.localPosition.y, base.transform.localPosition.z);
			if (base.transform.localPosition.x > 400f)
			{
				base.transform.localPosition = new Vector3(400f, base.transform.localPosition.y, base.transform.localPosition.z);
				this.Panel.enabled = false;
				this.Slide = false;
				this.Timer = 0f;
			}
		}
	}

	// Token: 0x0400233F RID: 9023
	public UILabel SongLabel;

	// Token: 0x04002340 RID: 9024
	public UILabel BandLabel;

	// Token: 0x04002341 RID: 9025
	public UIPanel Panel;

	// Token: 0x04002342 RID: 9026
	public bool Slide;

	// Token: 0x04002343 RID: 9027
	public float Timer;
}
