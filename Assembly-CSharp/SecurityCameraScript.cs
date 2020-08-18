﻿using System;
using UnityEngine;

// Token: 0x020003CF RID: 975
public class SecurityCameraScript : MonoBehaviour
{
	// Token: 0x06001A55 RID: 6741 RVA: 0x00101AD0 File Offset: 0x000FFCD0
	private void Update()
	{
		this.Rotation += (float)this.Direction * 36f * Time.deltaTime;
		base.transform.parent.localEulerAngles = new Vector3(base.transform.parent.localEulerAngles.x, this.Rotation, base.transform.parent.localEulerAngles.z);
		if (this.Direction > 0)
		{
			if (this.Rotation > 86.5f)
			{
				this.Direction = -1;
				return;
			}
		}
		else if (this.Rotation < -86.5f)
		{
			this.Direction = 1;
		}
	}

	// Token: 0x06001A56 RID: 6742 RVA: 0x00101B74 File Offset: 0x000FFD74
	private void OnTriggerStay(Collider other)
	{
		if (this.MissionMode.GameOverID == 0)
		{
			if (other.gameObject.layer == 13)
			{
				if ((this.Yandere.Armed && this.Yandere.EquippedWeapon.Suspicious) || (this.Yandere.Bloodiness > 0f && !this.Yandere.RedPaint) || (this.Yandere.Sanity < 33.333f || this.Yandere.Attacking || this.Yandere.Struggling || this.Yandere.Dragging || this.Yandere.Lewd || this.Yandere.Dragging || this.Yandere.Carrying || (this.Yandere.Laughing && this.Yandere.LaughIntensity > 15f)) || (this.Yandere.PickUp != null && this.Yandere.PickUp.Clothing && this.Yandere.PickUp.Evidence && !this.Yandere.PickUp.RedPaint))
				{
					if (this.Yandere.Mask == null)
					{
						if (this.MissionMode.enabled)
						{
							this.MissionMode.GameOverID = 15;
							this.MissionMode.GameOver();
							this.MissionMode.Phase = 4;
							base.enabled = false;
							return;
						}
						if (!this.SecuritySystem.Evidence)
						{
							this.Yandere.NotificationManager.DisplayNotification(NotificationType.Evidence);
							this.SecuritySystem.Evidence = true;
							this.SecuritySystem.Masked = false;
							return;
						}
					}
					else if (!this.SecuritySystem.Masked)
					{
						this.Yandere.NotificationManager.DisplayNotification(NotificationType.Evidence);
						this.SecuritySystem.Evidence = true;
						this.SecuritySystem.Masked = true;
						return;
					}
				}
			}
			else if (other.gameObject.layer == 11 && this.MissionMode.enabled)
			{
				this.MissionMode.GameOverID = 15;
				this.MissionMode.GameOver();
				this.MissionMode.Phase = 4;
				base.enabled = false;
			}
		}
	}

	// Token: 0x040029B5 RID: 10677
	public SecuritySystemScript SecuritySystem;

	// Token: 0x040029B6 RID: 10678
	public MissionModeScript MissionMode;

	// Token: 0x040029B7 RID: 10679
	public YandereScript Yandere;

	// Token: 0x040029B8 RID: 10680
	public float Rotation;

	// Token: 0x040029B9 RID: 10681
	public int Direction = 1;
}
