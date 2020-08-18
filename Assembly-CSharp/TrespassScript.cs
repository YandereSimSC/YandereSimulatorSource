using System;
using UnityEngine;

// Token: 0x02000431 RID: 1073
public class TrespassScript : MonoBehaviour
{
	// Token: 0x06001C6C RID: 7276 RVA: 0x00153C4C File Offset: 0x00151E4C
	private void OnTriggerEnter(Collider other)
	{
		if (base.enabled && other.gameObject.layer == 13)
		{
			this.YandereObject = other.gameObject;
			this.Yandere = other.gameObject.GetComponent<YandereScript>();
			if (this.Yandere != null)
			{
				if (!this.Yandere.Trespassing)
				{
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Intrude);
				}
				this.Yandere.Trespassing = true;
			}
		}
	}

	// Token: 0x06001C6D RID: 7277 RVA: 0x00153CC5 File Offset: 0x00151EC5
	private void OnTriggerExit(Collider other)
	{
		if (this.Yandere != null && other.gameObject == this.YandereObject)
		{
			this.Yandere.Trespassing = false;
		}
	}

	// Token: 0x0400355C RID: 13660
	public GameObject YandereObject;

	// Token: 0x0400355D RID: 13661
	public YandereScript Yandere;

	// Token: 0x0400355E RID: 13662
	public bool HideNotification;

	// Token: 0x0400355F RID: 13663
	public bool OffLimits;
}
