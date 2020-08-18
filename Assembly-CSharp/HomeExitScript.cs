using System;
using UnityEngine;

// Token: 0x020002EC RID: 748
public class HomeExitScript : MonoBehaviour
{
	// Token: 0x06001719 RID: 5913 RVA: 0x000C3534 File Offset: 0x000C1734
	private void Start()
	{
		UILabel uilabel = this.Labels[2];
		uilabel.color = new Color(uilabel.color.r, uilabel.color.g, uilabel.color.b, 0.5f);
		if (HomeGlobals.Night)
		{
			UILabel uilabel2 = this.Labels[1];
			uilabel2.color = new Color(uilabel2.color.r, uilabel2.color.g, uilabel2.color.b, 0.5f);
			uilabel.color = new Color(uilabel.color.r, uilabel.color.g, uilabel.color.b, 1f);
		}
	}

	// Token: 0x0600171A RID: 5914 RVA: 0x000C35F0 File Offset: 0x000C17F0
	private void Update()
	{
		if (!this.HomeYandere.CanMove && !this.HomeDarkness.FadeOut)
		{
			if (this.InputManager.TappedDown)
			{
				this.ID++;
				if (this.ID > 3)
				{
					this.ID = 1;
				}
				this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 50f - (float)this.ID * 50f, this.Highlight.localPosition.z);
			}
			if (this.InputManager.TappedUp)
			{
				this.ID--;
				if (this.ID < 1)
				{
					this.ID = 3;
				}
				this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 50f - (float)this.ID * 50f, this.Highlight.localPosition.z);
			}
			if (Input.GetButtonDown("A") && this.Labels[this.ID].color.a == 1f)
			{
				if (this.ID == 1)
				{
					if (SchoolGlobals.SchoolAtmosphere < 0.5f || GameGlobals.LoveSick)
					{
						this.HomeDarkness.Sprite.color = new Color(0f, 0f, 0f, 0f);
					}
					else
					{
						this.HomeDarkness.Sprite.color = new Color(1f, 1f, 1f, 0f);
					}
				}
				else if (this.ID == 2)
				{
					this.HomeDarkness.Sprite.color = new Color(1f, 1f, 1f, 0f);
				}
				else
				{
					this.HomeDarkness.Sprite.color = new Color(0f, 0f, 0f, 0f);
				}
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

	// Token: 0x04001F44 RID: 8004
	public InputManagerScript InputManager;

	// Token: 0x04001F45 RID: 8005
	public HomeDarknessScript HomeDarkness;

	// Token: 0x04001F46 RID: 8006
	public HomeYandereScript HomeYandere;

	// Token: 0x04001F47 RID: 8007
	public HomeCameraScript HomeCamera;

	// Token: 0x04001F48 RID: 8008
	public HomeWindowScript HomeWindow;

	// Token: 0x04001F49 RID: 8009
	public Transform Highlight;

	// Token: 0x04001F4A RID: 8010
	public UILabel[] Labels;

	// Token: 0x04001F4B RID: 8011
	public int ID = 1;
}
