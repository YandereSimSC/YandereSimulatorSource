using System;
using UnityEngine;

// Token: 0x0200032F RID: 815
public class MetalDetectorScript : MonoBehaviour
{
	// Token: 0x0600181E RID: 6174 RVA: 0x000D7473 File Offset: 0x000D5673
	private void Start()
	{
		this.MyAudio = base.GetComponent<AudioSource>();
	}

	// Token: 0x0600181F RID: 6175 RVA: 0x000D7484 File Offset: 0x000D5684
	private void Update()
	{
		if (this.Yandere.Armed)
		{
			if (this.Yandere.EquippedWeapon.WeaponID == 6)
			{
				this.Prompt.enabled = true;
				if (this.Prompt.Circle[0].fillAmount == 0f)
				{
					this.MyAudio.Play();
					this.MyCollider.enabled = false;
					this.Prompt.Hide();
					this.Prompt.enabled = false;
					base.enabled = false;
				}
			}
			else if (this.Prompt.enabled)
			{
				this.Prompt.Hide();
				this.Prompt.enabled = false;
			}
		}
		else if (this.Prompt.enabled)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
		}
		if (this.Spraying)
		{
			this.SprayTimer += Time.deltaTime;
			if ((double)this.SprayTimer > 0.66666)
			{
				if (this.Yandere.Armed)
				{
					this.Yandere.EquippedWeapon.Drop();
				}
				this.Yandere.EmptyHands();
				this.PepperSprayEffect.Play();
				this.Spraying = false;
			}
		}
		this.MyAudio.volume -= Time.deltaTime * 0.01f;
	}

	// Token: 0x06001820 RID: 6176 RVA: 0x000D75E0 File Offset: 0x000D57E0
	private void OnTriggerStay(Collider other)
	{
		if (this.Yandere.enabled)
		{
			bool flag = false;
			if (this.MissionMode.GameOverID == 0 && other.gameObject.layer == 13)
			{
				for (int i = 1; i < 4; i++)
				{
					WeaponScript weaponScript = this.Yandere.Weapon[i];
					flag |= (weaponScript != null && weaponScript.Metal);
					if (!flag)
					{
						if (this.Yandere.Container != null && this.Yandere.Container.Weapon != null)
						{
							weaponScript = this.Yandere.Container.Weapon;
							flag = weaponScript.Metal;
						}
						if (this.Yandere.PickUp != null)
						{
							if (this.Yandere.PickUp.TrashCan != null && this.Yandere.PickUp.TrashCan.Weapon)
							{
								weaponScript = this.Yandere.PickUp.TrashCan.Item.GetComponent<WeaponScript>();
								flag = weaponScript.Metal;
							}
							if (this.Yandere.PickUp.StuckBoxCutter != null)
							{
								weaponScript = this.Yandere.PickUp.StuckBoxCutter;
								flag = true;
							}
						}
					}
				}
				if (flag && !this.Yandere.Inventory.IDCard)
				{
					if (this.MissionMode.enabled)
					{
						this.MissionMode.GameOverID = 16;
						this.MissionMode.GameOver();
						this.MissionMode.Phase = 4;
						base.enabled = false;
						return;
					}
					if (!this.Yandere.Sprayed)
					{
						this.MyAudio.clip = this.Alarm;
						this.MyAudio.loop = true;
						this.MyAudio.Play();
						this.MyAudio.volume = 0.1f;
						AudioSource.PlayClipAtPoint(this.PepperSpraySFX, base.transform.position);
						if (this.Yandere.Aiming)
						{
							this.Yandere.StopAiming();
						}
						this.PepperSprayEffect.transform.position = new Vector3(base.transform.position.x, this.Yandere.transform.position.y + 1.8f, this.Yandere.transform.position.z);
						this.Spraying = true;
						this.Yandere.CharacterAnimation.CrossFade("f02_sprayed_00");
						this.Yandere.FollowHips = true;
						this.Yandere.Punching = false;
						this.Yandere.CanMove = false;
						this.Yandere.Sprayed = true;
						this.Yandere.StudentManager.YandereDying = true;
						this.Yandere.StudentManager.StopMoving();
						this.Yandere.Blur.blurIterations = 1;
						this.Yandere.Jukebox.Volume = 0f;
						Time.timeScale = 1f;
					}
				}
			}
		}
	}

	// Token: 0x040022F1 RID: 8945
	public MissionModeScript MissionMode;

	// Token: 0x040022F2 RID: 8946
	public YandereScript Yandere;

	// Token: 0x040022F3 RID: 8947
	public PromptScript Prompt;

	// Token: 0x040022F4 RID: 8948
	public ParticleSystem PepperSprayEffect;

	// Token: 0x040022F5 RID: 8949
	public AudioSource MyAudio;

	// Token: 0x040022F6 RID: 8950
	public AudioClip PepperSpraySFX;

	// Token: 0x040022F7 RID: 8951
	public AudioClip Alarm;

	// Token: 0x040022F8 RID: 8952
	public Collider MyCollider;

	// Token: 0x040022F9 RID: 8953
	public float SprayTimer;

	// Token: 0x040022FA RID: 8954
	public bool Spraying;
}
