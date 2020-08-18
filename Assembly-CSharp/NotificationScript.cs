using System;
using UnityEngine;

// Token: 0x02000344 RID: 836
public class NotificationScript : MonoBehaviour
{
	// Token: 0x06001876 RID: 6262 RVA: 0x000DFB38 File Offset: 0x000DDD38
	private void Start()
	{
		if (MissionModeGlobals.MissionMode)
		{
			this.Icon[0].color = new Color(1f, 1f, 1f, 1f);
			this.Icon[1].color = new Color(1f, 1f, 1f, 1f);
			this.Label.color = new Color(1f, 1f, 1f, 1f);
		}
	}

	// Token: 0x06001877 RID: 6263 RVA: 0x000DFBBC File Offset: 0x000DDDBC
	private void Update()
	{
		if (!this.Display)
		{
			this.Panel.alpha -= Time.deltaTime * ((this.NotificationManager.NotificationsSpawned > this.ID + 2) ? 3f : 1f);
			if (this.Panel.alpha <= 0f)
			{
				UnityEngine.Object.Destroy(base.gameObject);
				return;
			}
		}
		else
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > 4f)
			{
				this.Display = false;
			}
			if (this.NotificationManager.NotificationsSpawned > this.ID + 2)
			{
				this.Display = false;
			}
		}
	}

	// Token: 0x040023E9 RID: 9193
	public NotificationManagerScript NotificationManager;

	// Token: 0x040023EA RID: 9194
	public UISprite[] Icon;

	// Token: 0x040023EB RID: 9195
	public UIPanel Panel;

	// Token: 0x040023EC RID: 9196
	public UILabel Label;

	// Token: 0x040023ED RID: 9197
	public bool Display;

	// Token: 0x040023EE RID: 9198
	public float Timer;

	// Token: 0x040023EF RID: 9199
	public int ID;
}
