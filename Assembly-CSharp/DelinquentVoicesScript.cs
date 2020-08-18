using System;
using UnityEngine;

// Token: 0x0200025F RID: 607
public class DelinquentVoicesScript : MonoBehaviour
{
	// Token: 0x06001326 RID: 4902 RVA: 0x0009F7F6 File Offset: 0x0009D9F6
	private void Start()
	{
		this.Timer = 5f;
	}

	// Token: 0x06001327 RID: 4903 RVA: 0x0009F804 File Offset: 0x0009DA04
	private void Update()
	{
		if (this.Radio.MyAudio.isPlaying && this.Yandere.CanMove && Vector3.Distance(this.Yandere.transform.position, base.transform.position) < 5f)
		{
			this.Timer = Mathf.MoveTowards(this.Timer, 0f, Time.deltaTime);
			if (this.Timer == 0f && ClubGlobals.Club != ClubType.Delinquent)
			{
				if (this.Yandere.Container == null)
				{
					while (this.RandomID == this.LastID)
					{
						this.RandomID = UnityEngine.Random.Range(0, this.Subtitle.DelinquentAnnoyClips.Length);
					}
					this.LastID = this.RandomID;
					this.Subtitle.UpdateLabel(SubtitleType.DelinquentAnnoy, this.RandomID, 3f);
				}
				else
				{
					while (this.RandomID == this.LastID)
					{
						this.RandomID = UnityEngine.Random.Range(0, this.Subtitle.DelinquentCaseClips.Length);
					}
					this.LastID = this.RandomID;
					this.Subtitle.UpdateLabel(SubtitleType.DelinquentCase, this.RandomID, 3f);
				}
				this.Timer = 5f;
			}
		}
	}

	// Token: 0x040019BD RID: 6589
	public YandereScript Yandere;

	// Token: 0x040019BE RID: 6590
	public RadioScript Radio;

	// Token: 0x040019BF RID: 6591
	public SubtitleScript Subtitle;

	// Token: 0x040019C0 RID: 6592
	public float Timer;

	// Token: 0x040019C1 RID: 6593
	public int RandomID;

	// Token: 0x040019C2 RID: 6594
	public int LastID;
}
