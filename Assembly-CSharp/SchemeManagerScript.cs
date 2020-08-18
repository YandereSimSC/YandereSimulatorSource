using System;
using UnityEngine;

// Token: 0x020003C7 RID: 967
public class SchemeManagerScript : MonoBehaviour
{
	// Token: 0x06001A3E RID: 6718 RVA: 0x00100D78 File Offset: 0x000FEF78
	private void Update()
	{
		if (this.Clock.HourTime > 15.5f)
		{
			SchemeGlobals.SetSchemeStage(SchemeGlobals.CurrentScheme, 100);
			this.Clock.Yandere.NotificationManager.CustomText = "Scheme failed! You were too slow.";
			this.Clock.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
			this.Schemes.UpdateInstructions();
			base.enabled = false;
		}
		if (this.ClockCheck && this.Clock.HourTime > 8.25f)
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > 1f)
			{
				this.Timer = 0f;
				if (SchemeGlobals.GetSchemeStage(5) == 1)
				{
					Debug.Log("It's past 8:15 AM, so we're advancing to Stage 2 of Scheme 5.");
					SchemeGlobals.SetSchemeStage(5, 2);
					this.Schemes.UpdateInstructions();
					this.ClockCheck = false;
				}
			}
		}
	}

	// Token: 0x0400297B RID: 10619
	public SchemesScript Schemes;

	// Token: 0x0400297C RID: 10620
	public ClockScript Clock;

	// Token: 0x0400297D RID: 10621
	public bool ClockCheck;

	// Token: 0x0400297E RID: 10622
	public float Timer;
}
