using System;
using UnityEngine;

// Token: 0x020002F5 RID: 757
public class HomeSleepScript : MonoBehaviour
{
	// Token: 0x06001742 RID: 5954 RVA: 0x000C829C File Offset: 0x000C649C
	private void Update()
	{
		if (!this.HomeYandere.CanMove && !this.HomeDarkness.FadeOut)
		{
			if (Input.GetButtonDown("A"))
			{
				this.HomeDarkness.Sprite.color = new Color(0f, 0f, 0f, 0f);
				this.HomeDarkness.Cyberstalking = true;
				this.HomeDarkness.FadeOut = true;
				this.HomeWindow.Show = false;
				base.enabled = false;
			}
			if (Input.GetButtonDown("B"))
			{
				this.HomeCamera.Destination = this.HomeCamera.Destinations[0];
				this.HomeCamera.Target = this.HomeCamera.Targets[0];
				this.HomeYandere.CanMove = true;
				this.HomeWindow.Show = false;
				base.enabled = false;
			}
		}
	}

	// Token: 0x04002028 RID: 8232
	public HomeDarknessScript HomeDarkness;

	// Token: 0x04002029 RID: 8233
	public HomeYandereScript HomeYandere;

	// Token: 0x0400202A RID: 8234
	public HomeCameraScript HomeCamera;

	// Token: 0x0400202B RID: 8235
	public HomeWindowScript HomeWindow;
}
