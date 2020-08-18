using System;
using UnityEngine;

// Token: 0x02000377 RID: 887
public class PowerSwitchScript : MonoBehaviour
{
	// Token: 0x06001934 RID: 6452 RVA: 0x000F0EE0 File Offset: 0x000EF0E0
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			this.On = !this.On;
			if (this.On)
			{
				this.Prompt.Label[0].text = "     Turn Off";
				this.MyAudio.clip = this.Flick[1];
			}
			else
			{
				this.Prompt.Label[0].text = "     Turn On";
				this.MyAudio.clip = this.Flick[0];
			}
			if (this.BathroomLight != null)
			{
				this.BathroomLight.enabled = !this.BathroomLight.enabled;
			}
			this.CheckPuddle();
			this.MyAudio.Play();
		}
	}

	// Token: 0x06001935 RID: 6453 RVA: 0x000F0FC8 File Offset: 0x000EF1C8
	public void CheckPuddle()
	{
		if (this.On)
		{
			if (this.DrinkingFountain.Puddle != null && this.DrinkingFountain.Puddle.gameObject.activeInHierarchy && this.PowerOutlet.SabotagedOutlet.activeInHierarchy)
			{
				this.Electricity.SetActive(true);
				return;
			}
		}
		else
		{
			this.Electricity.SetActive(false);
		}
	}

	// Token: 0x04002641 RID: 9793
	public DrinkingFountainScript DrinkingFountain;

	// Token: 0x04002642 RID: 9794
	public PowerOutletScript PowerOutlet;

	// Token: 0x04002643 RID: 9795
	public GameObject Electricity;

	// Token: 0x04002644 RID: 9796
	public Light BathroomLight;

	// Token: 0x04002645 RID: 9797
	public PromptScript Prompt;

	// Token: 0x04002646 RID: 9798
	public AudioSource MyAudio;

	// Token: 0x04002647 RID: 9799
	public AudioClip[] Flick;

	// Token: 0x04002648 RID: 9800
	public bool On;
}
