using System;
using UnityEngine;

// Token: 0x0200026B RID: 619
public class DoItScript : MonoBehaviour
{
	// Token: 0x0600134B RID: 4939 RVA: 0x000A41B7 File Offset: 0x000A23B7
	private void Start()
	{
		this.MyLabel.fontSize = UnityEngine.Random.Range(50, 100);
	}

	// Token: 0x0600134C RID: 4940 RVA: 0x000A41D0 File Offset: 0x000A23D0
	private void Update()
	{
		base.transform.localScale += new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
		if (!this.Fade)
		{
			this.MyLabel.alpha += Time.deltaTime;
			if (this.MyLabel.alpha >= 1f)
			{
				this.Fade = true;
				return;
			}
		}
		else
		{
			this.MyLabel.alpha -= Time.deltaTime;
			if (this.MyLabel.alpha <= 0f)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	// Token: 0x04001A2F RID: 6703
	public UILabel MyLabel;

	// Token: 0x04001A30 RID: 6704
	public bool Fade;
}
