using System;
using UnityEngine;

// Token: 0x020002EA RID: 746
public class HomeCyberstalkScript : MonoBehaviour
{
	// Token: 0x06001713 RID: 5907 RVA: 0x000C2EC0 File Offset: 0x000C10C0
	private void Update()
	{
		if (Input.GetButtonDown("A"))
		{
			this.HomeDarkness.Sprite.color = new Color(0f, 0f, 0f, 0f);
			this.HomeDarkness.Cyberstalking = true;
			this.HomeDarkness.FadeOut = true;
			base.gameObject.SetActive(false);
			for (int i = 1; i < 26; i++)
			{
				ConversationGlobals.SetTopicLearnedByStudent(i, this.HomeDarkness.HomeCamera.HomeInternet.Student, true);
				ConversationGlobals.SetTopicDiscovered(i, true);
			}
		}
		if (Input.GetButtonDown("B"))
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x04001F39 RID: 7993
	public HomeDarknessScript HomeDarkness;
}
