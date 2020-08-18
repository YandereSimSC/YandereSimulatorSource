using System;
using UnityEngine;

// Token: 0x02000472 RID: 1138
public class YandereShowerScript : MonoBehaviour
{
	// Token: 0x06001D96 RID: 7574 RVA: 0x00170234 File Offset: 0x0016E434
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			if (this.Yandere.Schoolwear > 0 || this.Yandere.Chased || this.Yandere.Chasers > 0 || this.Yandere.Bloodiness == 0f)
			{
				this.Prompt.Circle[0].fillAmount = 1f;
			}
			else
			{
				AudioSource.PlayClipAtPoint(this.CurtainOpen, base.transform.position);
				this.CensorSteam.SetActive(true);
				this.MyAudio.Play();
				this.Yandere.EmptyHands();
				this.Yandere.YandereShower = this;
				this.Yandere.CanMove = false;
				this.Yandere.Bathing = true;
				this.UpdateCurtain = true;
				this.Open = true;
				this.Timer = 6f;
			}
		}
		if (this.UpdateCurtain)
		{
			this.Timer = Mathf.MoveTowards(this.Timer, 0f, Time.deltaTime);
			if (this.Timer < 1f)
			{
				if (this.Open)
				{
					AudioSource.PlayClipAtPoint(this.CurtainClose, base.transform.position);
				}
				this.Open = false;
				if (this.Timer == 0f)
				{
					this.CensorSteam.SetActive(false);
					this.UpdateCurtain = false;
				}
			}
			if (this.Open)
			{
				this.OpenValue = Mathf.Lerp(this.OpenValue, 0f, Time.deltaTime * 10f);
				this.Curtain.SetBlendShapeWeight(0, this.OpenValue);
				return;
			}
			this.OpenValue = Mathf.Lerp(this.OpenValue, 100f, Time.deltaTime * 10f);
			this.Curtain.SetBlendShapeWeight(0, this.OpenValue);
		}
	}

	// Token: 0x04003A71 RID: 14961
	public SkinnedMeshRenderer Curtain;

	// Token: 0x04003A72 RID: 14962
	public GameObject CensorSteam;

	// Token: 0x04003A73 RID: 14963
	public YandereScript Yandere;

	// Token: 0x04003A74 RID: 14964
	public PromptScript Prompt;

	// Token: 0x04003A75 RID: 14965
	public Transform BatheSpot;

	// Token: 0x04003A76 RID: 14966
	public float OpenValue;

	// Token: 0x04003A77 RID: 14967
	public float Timer;

	// Token: 0x04003A78 RID: 14968
	public bool UpdateCurtain;

	// Token: 0x04003A79 RID: 14969
	public bool Open;

	// Token: 0x04003A7A RID: 14970
	public AudioSource MyAudio;

	// Token: 0x04003A7B RID: 14971
	public AudioClip CurtainClose;

	// Token: 0x04003A7C RID: 14972
	public AudioClip CurtainOpen;
}
