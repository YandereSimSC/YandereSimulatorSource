using System;
using UnityEngine;

// Token: 0x02000273 RID: 627
public class DrinkingFountainScript : MonoBehaviour
{
	// Token: 0x06001367 RID: 4967 RVA: 0x000A6864 File Offset: 0x000A4A64
	private void Update()
	{
		if (this.Prompt.Yandere.EquippedWeapon != null)
		{
			if (this.Prompt.Yandere.EquippedWeapon.Blood.enabled)
			{
				this.Prompt.HideButton[0] = false;
				this.Prompt.enabled = true;
			}
			else
			{
				this.Prompt.HideButton[0] = true;
			}
			if (!this.Leak.activeInHierarchy)
			{
				if (this.Prompt.Yandere.EquippedWeapon.WeaponID == 24)
				{
					this.Prompt.HideButton[1] = false;
					this.Prompt.enabled = true;
				}
				else
				{
					this.Prompt.HideButton[1] = true;
				}
			}
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				this.Prompt.Circle[0].fillAmount = 1f;
				this.Prompt.Yandere.CharacterAnimation.CrossFade("f02_cleaningWeapon_00");
				this.Prompt.Yandere.Target = this.DrinkPosition;
				this.Prompt.Yandere.CleaningWeapon = true;
				this.Prompt.Yandere.CanMove = false;
				this.WaterStream.Play();
			}
			if (this.Prompt.Circle[1].fillAmount == 0f)
			{
				this.Prompt.HideButton[1] = true;
				this.Puddle.SetActive(true);
				this.Leak.SetActive(true);
				this.MyAudio.Play();
				this.PowerSwitch.CheckPuddle();
				return;
			}
		}
		else if (this.Prompt.enabled)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
		}
	}

	// Token: 0x04001A70 RID: 6768
	public PowerSwitchScript PowerSwitch;

	// Token: 0x04001A71 RID: 6769
	public ParticleSystem WaterStream;

	// Token: 0x04001A72 RID: 6770
	public Transform DrinkPosition;

	// Token: 0x04001A73 RID: 6771
	public GameObject Puddle;

	// Token: 0x04001A74 RID: 6772
	public GameObject Leak;

	// Token: 0x04001A75 RID: 6773
	public PromptScript Prompt;

	// Token: 0x04001A76 RID: 6774
	public AudioSource MyAudio;

	// Token: 0x04001A77 RID: 6775
	public bool Occupied;
}
