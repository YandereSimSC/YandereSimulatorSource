using System;
using UnityEngine;

// Token: 0x020000D4 RID: 212
public class AttackManagerScript : MonoBehaviour
{
	// Token: 0x06000A2A RID: 2602 RVA: 0x00051800 File Offset: 0x0004FA00
	private void Awake()
	{
		this.Yandere = base.GetComponent<YandereScript>();
	}

	// Token: 0x06000A2B RID: 2603 RVA: 0x0005180E File Offset: 0x0004FA0E
	private void Start()
	{
		this.OriginalBloodEffect = this.BloodEffect;
	}

	// Token: 0x06000A2C RID: 2604 RVA: 0x0005181C File Offset: 0x0004FA1C
	public bool IsAttacking()
	{
		return this.Victim != null;
	}

	// Token: 0x06000A2D RID: 2605 RVA: 0x0005182C File Offset: 0x0004FA2C
	private float GetReachDistance(WeaponType weaponType, SanityType sanityType)
	{
		if (weaponType == WeaponType.Knife)
		{
			if (this.Stealth)
			{
				return 0.75f;
			}
			if (sanityType == SanityType.High)
			{
				return 1f;
			}
			if (sanityType == SanityType.Medium)
			{
				return 0.75f;
			}
			return 0.5f;
		}
		else if (weaponType == WeaponType.Katana)
		{
			if (!this.Stealth)
			{
				return 1f;
			}
			return 0.5f;
		}
		else if (weaponType == WeaponType.Bat)
		{
			if (this.Stealth)
			{
				return 0.5f;
			}
			if (sanityType == SanityType.High)
			{
				return 0.75f;
			}
			return 1f;
		}
		else if (weaponType == WeaponType.Saw)
		{
			if (!this.Stealth)
			{
				return 1f;
			}
			return 0.7f;
		}
		else if (weaponType == WeaponType.Weight)
		{
			if (this.Stealth)
			{
				return 0.75f;
			}
			if (sanityType == SanityType.High)
			{
				return 0.75f;
			}
			return 0.75f;
		}
		else
		{
			if (weaponType == WeaponType.Syringe)
			{
				return 0.5f;
			}
			Debug.LogError("Weapon type \"" + weaponType.ToString() + "\" not implemented.");
			return 0f;
		}
	}

	// Token: 0x06000A2E RID: 2606 RVA: 0x00051910 File Offset: 0x0004FB10
	public void Attack(GameObject victim, WeaponScript weapon)
	{
		this.Victim = victim;
		this.Yandere.FollowHips = true;
		this.AttackTimer = 0f;
		this.EffectPhase = 0;
		this.Yandere.Sanity = Mathf.Clamp(this.Yandere.Sanity, 0f, 100f);
		SanityType sanityType = this.Yandere.SanityType;
		string sanityString = this.Yandere.GetSanityString(sanityType);
		string str = weapon.GetTypePrefix();
		string str2 = this.Yandere.TargetStudent.Male ? string.Empty : "f02_";
		if (!this.Stealth)
		{
			this.VictimAnimName = str2 + str + sanityString + "SanityB_00";
			if (weapon.WeaponID == 23)
			{
				str = "extin";
			}
			this.AnimName = "f02_" + str + sanityString + "SanityA_00";
		}
		else
		{
			this.VictimAnimName = str2 + str + "StealthB_00";
			if (weapon.WeaponID == 23)
			{
				str = "extin";
			}
			this.AnimName = "f02_" + str + "StealthA_00";
		}
		this.YandereAnim = this.Yandere.CharacterAnimation;
		this.YandereAnim[this.AnimName].time = 0f;
		this.YandereAnim.CrossFade(this.AnimName);
		this.VictimAnim = this.Yandere.TargetStudent.CharacterAnimation;
		this.VictimAnim[this.VictimAnimName].time = 0f;
		this.VictimAnim.CrossFade(this.VictimAnimName);
		AudioSource component = weapon.gameObject.GetComponent<AudioSource>();
		component.clip = weapon.GetClip(this.Yandere.Sanity / 100f, this.Stealth);
		component.time = 0f;
		component.Play();
		if (weapon.Type == WeaponType.Knife)
		{
			weapon.Flip = true;
		}
		this.Distance = this.GetReachDistance(weapon.Type, sanityType);
	}

	// Token: 0x06000A2F RID: 2607 RVA: 0x00051B04 File Offset: 0x0004FD04
	private void Update()
	{
		if (this.IsAttacking())
		{
			this.VictimAnim.CrossFade(this.VictimAnimName);
			if (this.Censor)
			{
				if (this.AttackTimer == 0f)
				{
					this.Yandere.Blur.enabled = true;
					this.Yandere.Blur.blurSize = 0f;
					this.Yandere.Blur.blurIterations = 0;
				}
				if (this.AttackTimer < this.YandereAnim[this.AnimName].length - 0.5f)
				{
					this.Yandere.Blur.blurSize = Mathf.MoveTowards(this.Yandere.Blur.blurSize, 10f, Time.deltaTime * 10f);
					if (this.Yandere.Blur.blurSize > (float)this.Yandere.Blur.blurIterations)
					{
						this.Yandere.Blur.blurIterations++;
					}
				}
				else
				{
					this.Yandere.Blur.blurSize = Mathf.Lerp(this.Yandere.Blur.blurSize, 0f, Time.deltaTime * 10f);
					if (this.Yandere.Blur.blurSize < (float)this.Yandere.Blur.blurIterations)
					{
						this.Yandere.Blur.blurIterations--;
					}
				}
			}
			this.AttackTimer += Time.deltaTime;
			WeaponScript equippedWeapon = this.Yandere.EquippedWeapon;
			SanityType sanityType = this.Yandere.SanityType;
			this.SpecialEffect(equippedWeapon, sanityType);
			if (sanityType == SanityType.Low)
			{
				this.LoopCheck(equippedWeapon);
			}
			this.SpecialEffect(equippedWeapon, sanityType);
			if (this.YandereAnim[this.AnimName].time > this.YandereAnim[this.AnimName].length - 0.33333334f)
			{
				this.YandereAnim.CrossFade("f02_idle_00");
				equippedWeapon.Flip = false;
			}
			if (this.AttackTimer > this.YandereAnim[this.AnimName].length)
			{
				if (this.Yandere.TargetStudent == this.Yandere.StudentManager.Reporter)
				{
					this.Yandere.StudentManager.Reporter = null;
				}
				if (!this.Yandere.CanTranq)
				{
					this.Yandere.TargetStudent.DeathType = DeathType.Weapon;
				}
				else
				{
					this.Yandere.TargetStudent.Tranquil = true;
					this.Yandere.NoStainGloves = true;
					this.Yandere.CanTranq = false;
					this.Yandere.StainWeapon();
					this.Yandere.Followers--;
					equippedWeapon.Type = WeaponType.Knife;
				}
				this.Yandere.TargetStudent.DeathCause = equippedWeapon.WeaponID;
				this.Yandere.TargetStudent.BecomeRagdoll();
				this.Yandere.Sanity -= ((PlayerGlobals.PantiesEquipped == 10) ? 10f : 20f) * this.Yandere.Numbness;
				this.Yandere.Attacking = false;
				this.Yandere.FollowHips = false;
				this.Yandere.HipCollider.enabled = false;
				bool flag = false;
				if (this.Yandere.EquippedWeapon.Type == WeaponType.Bat && this.Stealth)
				{
					flag = true;
				}
				if (!flag)
				{
					this.Yandere.EquippedWeapon.Evidence = true;
				}
				this.Victim = null;
				this.VictimAnimName = null;
				this.AnimName = null;
				this.Stealth = false;
				this.EffectPhase = 0;
				this.AttackTimer = 0f;
				this.Timer = 0f;
				this.CheckForSpecialCase(equippedWeapon);
				this.Yandere.Blur.enabled = false;
				this.Yandere.Blur.blurSize = 0f;
				if (!this.Yandere.Noticed)
				{
					this.Yandere.EquippedWeapon.MurderWeapon = true;
					this.Yandere.CanMove = true;
					return;
				}
				equippedWeapon.Drop();
			}
		}
	}

	// Token: 0x06000A30 RID: 2608 RVA: 0x00051F24 File Offset: 0x00050124
	private void SpecialEffect(WeaponScript weapon, SanityType sanityType)
	{
		this.BloodEffect = this.OriginalBloodEffect;
		if (weapon.WeaponID == 14)
		{
			this.BloodEffect = weapon.HeartBurst;
		}
		if (weapon.Type == WeaponType.Knife)
		{
			if (!this.Stealth)
			{
				if (sanityType == SanityType.High)
				{
					if (this.EffectPhase == 0 && this.YandereAnim[this.AnimName].time > 1.0666667f)
					{
						this.Yandere.Bloodiness += 20f;
						this.Yandere.StainWeapon();
						UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, weapon.transform.position + weapon.transform.forward * 0.1f, Quaternion.identity);
						this.EffectPhase++;
						return;
					}
				}
				else if (sanityType == SanityType.Medium)
				{
					if (this.EffectPhase == 0)
					{
						if (this.YandereAnim[this.AnimName].time > 2.1666667f)
						{
							this.Yandere.Bloodiness += 20f;
							this.Yandere.StainWeapon();
							UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, weapon.transform.position + weapon.transform.forward * 0.1f, Quaternion.identity);
							this.EffectPhase++;
							return;
						}
					}
					else if (this.EffectPhase == 1 && this.YandereAnim[this.AnimName].time > 3.0333333f)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, weapon.transform.position + weapon.transform.forward * 0.1f, Quaternion.identity);
						this.EffectPhase++;
						return;
					}
				}
				else if (this.EffectPhase == 0)
				{
					if (this.YandereAnim[this.AnimName].time > 2.7666667f)
					{
						this.Yandere.Bloodiness += 20f;
						this.Yandere.StainWeapon();
						UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, weapon.transform.position + weapon.transform.forward * 0.1f, Quaternion.identity);
						this.EffectPhase++;
						return;
					}
				}
				else if (this.EffectPhase == 1)
				{
					if (this.YandereAnim[this.AnimName].time > 3.5333333f)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, weapon.transform.position + weapon.transform.forward * 0.1f, Quaternion.identity);
						this.EffectPhase++;
						return;
					}
				}
				else if (this.EffectPhase == 2 && this.YandereAnim[this.AnimName].time > 4.1666665f)
				{
					UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, weapon.transform.position + weapon.transform.forward * 0.1f, Quaternion.identity);
					this.EffectPhase++;
					return;
				}
			}
			else if (this.EffectPhase == 0 && this.YandereAnim[this.AnimName].time > 0.96666664f)
			{
				this.Yandere.Bloodiness += 20f;
				this.Yandere.StainWeapon();
				UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, weapon.transform.position + weapon.transform.forward * 0.1f, Quaternion.identity);
				this.EffectPhase++;
				return;
			}
		}
		else if (weapon.Type == WeaponType.Katana)
		{
			if (!this.Stealth)
			{
				if (sanityType == SanityType.High)
				{
					if (this.EffectPhase == 0 && this.YandereAnim[this.AnimName].time > 0.48333332f)
					{
						this.Yandere.Bloodiness += 20f;
						this.Yandere.StainWeapon();
						UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, weapon.transform.position + weapon.transform.forward * 0.5f, Quaternion.identity);
						this.EffectPhase++;
						return;
					}
				}
				else if (sanityType == SanityType.Medium)
				{
					if (this.EffectPhase == 0)
					{
						if (this.YandereAnim[this.AnimName].time > 0.55f)
						{
							this.Yandere.Bloodiness += 20f;
							this.Yandere.StainWeapon();
							UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, weapon.transform.position + weapon.transform.forward * 0.5f, Quaternion.identity);
							this.EffectPhase++;
							return;
						}
					}
					else if (this.EffectPhase == 1 && this.YandereAnim[this.AnimName].time > 1.5166667f)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, weapon.transform.position + weapon.transform.forward * 0.5f, Quaternion.identity);
						this.EffectPhase++;
						return;
					}
				}
				else if (this.EffectPhase == 0)
				{
					if (this.YandereAnim[this.AnimName].time > 0.5f)
					{
						this.Yandere.Bloodiness += 20f;
						this.Yandere.StainWeapon();
						UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, weapon.transform.position + weapon.transform.forward * 0.6666667f, Quaternion.identity);
						this.EffectPhase++;
						return;
					}
				}
				else if (this.EffectPhase == 1)
				{
					if (this.YandereAnim[this.AnimName].time > 1f)
					{
						weapon.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
						this.EffectPhase++;
						return;
					}
				}
				else if (this.EffectPhase == 2)
				{
					if (this.YandereAnim[this.AnimName].time > 2.3333333f)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, weapon.transform.position + weapon.transform.forward * 0.6666667f, Quaternion.identity);
						this.EffectPhase++;
						return;
					}
				}
				else if (this.EffectPhase == 3)
				{
					if (this.YandereAnim[this.AnimName].time > 2.7333333f)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, weapon.transform.position + weapon.transform.forward * 0.6666667f, Quaternion.identity);
						this.EffectPhase++;
						return;
					}
				}
				else if (this.EffectPhase == 4)
				{
					if (this.YandereAnim[this.AnimName].time > 3.1333334f)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, weapon.transform.position + weapon.transform.forward * 0.6666667f, Quaternion.identity);
						this.EffectPhase++;
						return;
					}
				}
				else if (this.EffectPhase == 5)
				{
					if (this.YandereAnim[this.AnimName].time > 3.5333333f)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, weapon.transform.position + weapon.transform.forward * 0.6666667f, Quaternion.identity);
						this.EffectPhase++;
						return;
					}
				}
				else if (this.EffectPhase == 6)
				{
					if (this.YandereAnim[this.AnimName].time > 4.133333f)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, weapon.transform.position + weapon.transform.forward * 0.6666667f, Quaternion.identity);
						this.EffectPhase++;
						return;
					}
				}
				else if (this.EffectPhase == 8 && this.YandereAnim[this.AnimName].time > 5f)
				{
					weapon.transform.localEulerAngles = Vector3.zero;
					this.EffectPhase++;
					return;
				}
			}
			else if (this.EffectPhase == 0)
			{
				if (this.YandereAnim[this.AnimName].time > 0.36666667f)
				{
					this.Yandere.Bloodiness += 20f;
					this.Yandere.StainWeapon();
					UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, weapon.transform.position + weapon.transform.forward * 0.6666667f, Quaternion.identity);
					this.EffectPhase++;
					return;
				}
			}
			else if (this.EffectPhase == 1 && this.YandereAnim[this.AnimName].time > 1f)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, weapon.transform.position + weapon.transform.forward * 0.33333334f, Quaternion.identity);
				this.EffectPhase++;
				return;
			}
		}
		else if (weapon.Type == WeaponType.Bat)
		{
			if (this.Stealth)
			{
				this.Yandere.TargetStudent.Ragdoll.NeckSnapped = true;
				return;
			}
			if (sanityType == SanityType.High)
			{
				if (this.EffectPhase == 0 && this.YandereAnim[this.AnimName].time > 0.73333335f)
				{
					this.Yandere.Bloodiness += 20f;
					this.Yandere.StainWeapon();
					UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, weapon.transform.position + weapon.transform.forward * 0.5f, Quaternion.identity);
					this.EffectPhase++;
					return;
				}
			}
			else if (sanityType == SanityType.Medium)
			{
				if (this.EffectPhase == 0)
				{
					if (this.YandereAnim[this.AnimName].time > 1f)
					{
						this.Yandere.Bloodiness += 20f;
						this.Yandere.StainWeapon();
						UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, weapon.transform.position + weapon.transform.forward * 0.5f, Quaternion.identity);
						this.EffectPhase++;
						return;
					}
				}
				else if (this.EffectPhase == 1 && this.YandereAnim[this.AnimName].time > 2.9666667f)
				{
					UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, weapon.transform.position + weapon.transform.forward * 0.5f, Quaternion.identity);
					this.EffectPhase++;
					return;
				}
			}
			else if (this.EffectPhase == 0)
			{
				if (this.YandereAnim[this.AnimName].time > 0.7f)
				{
					this.Yandere.Bloodiness += 20f;
					this.Yandere.StainWeapon();
					UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, weapon.transform.position + weapon.transform.forward * 0.5f, Quaternion.identity);
					this.EffectPhase++;
					return;
				}
			}
			else if (this.EffectPhase == 1)
			{
				if (this.YandereAnim[this.AnimName].time > 3.1f)
				{
					UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, weapon.transform.position + weapon.transform.forward * 0.5f, Quaternion.identity);
					this.EffectPhase++;
					return;
				}
			}
			else if (this.EffectPhase == 2)
			{
				if (this.YandereAnim[this.AnimName].time > 3.7666667f)
				{
					UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, weapon.transform.position + weapon.transform.forward * 0.5f, Quaternion.identity);
					this.EffectPhase++;
					return;
				}
			}
			else if (this.EffectPhase == 3 && this.YandereAnim[this.AnimName].time > 4.4f)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, weapon.transform.position + weapon.transform.forward * 0.5f, Quaternion.identity);
				this.EffectPhase++;
				return;
			}
		}
		else if (weapon.Type == WeaponType.Saw)
		{
			if (!this.Stealth)
			{
				if (sanityType == SanityType.High)
				{
					if (this.EffectPhase == 0)
					{
						if (this.YandereAnim[this.AnimName].time > 0f)
						{
							weapon.Spin = true;
							this.EffectPhase++;
							return;
						}
					}
					else if (this.EffectPhase == 1)
					{
						if (this.YandereAnim[this.AnimName].time > 0.73333335f)
						{
							this.Yandere.Bloodiness += 20f;
							this.Yandere.StainWeapon();
							weapon.BloodSpray[0].Play();
							weapon.BloodSpray[1].Play();
							this.EffectPhase++;
							return;
						}
					}
					else if (this.EffectPhase == 2 && this.YandereAnim[this.AnimName].time > 1.4333333f)
					{
						weapon.Spin = false;
						weapon.BloodSpray[0].Stop();
						weapon.BloodSpray[1].Stop();
						this.EffectPhase++;
						return;
					}
				}
				else if (sanityType == SanityType.Medium)
				{
					if (this.EffectPhase == 0)
					{
						if (this.YandereAnim[this.AnimName].time > 0f)
						{
							weapon.Spin = true;
							this.EffectPhase++;
							return;
						}
					}
					else if (this.EffectPhase == 1)
					{
						if (this.YandereAnim[this.AnimName].time > 1.1f)
						{
							this.Yandere.Bloodiness += 20f;
							this.Yandere.StainWeapon();
							weapon.BloodSpray[0].Play();
							weapon.BloodSpray[1].Play();
							this.EffectPhase++;
							return;
						}
					}
					else if (this.EffectPhase == 2)
					{
						if (this.YandereAnim[this.AnimName].time > 1.4333333f)
						{
							weapon.BloodSpray[0].Stop();
							weapon.BloodSpray[1].Stop();
							this.EffectPhase++;
							return;
						}
					}
					else if (this.EffectPhase == 3)
					{
						if (this.YandereAnim[this.AnimName].time > 2.3666666f)
						{
							weapon.BloodSpray[0].Play();
							weapon.BloodSpray[1].Play();
							this.EffectPhase++;
							return;
						}
					}
					else if (this.EffectPhase == 4 && this.YandereAnim[this.AnimName].time > 2.4f)
					{
						weapon.Spin = true;
						weapon.BloodSpray[0].Stop();
						weapon.BloodSpray[1].Stop();
						this.EffectPhase++;
						return;
					}
				}
				else if (this.EffectPhase == 0)
				{
					if (this.YandereAnim[this.AnimName].time > 0f)
					{
						weapon.Spin = true;
						this.EffectPhase++;
						return;
					}
				}
				else if (this.EffectPhase == 1)
				{
					if (this.YandereAnim[this.AnimName].time > 0.6666667f)
					{
						this.Yandere.Bloodiness += 20f;
						this.Yandere.StainWeapon();
						weapon.BloodSpray[0].Play();
						weapon.BloodSpray[1].Play();
						this.EffectPhase++;
						return;
					}
				}
				else if (this.EffectPhase == 2)
				{
					if (this.YandereAnim[this.AnimName].time > 0.73333335f)
					{
						weapon.BloodSpray[0].Stop();
						weapon.BloodSpray[1].Stop();
						this.EffectPhase++;
						return;
					}
				}
				else if (this.EffectPhase == 3)
				{
					if (this.YandereAnim[this.AnimName].time > 3f)
					{
						weapon.BloodSpray[0].Play();
						weapon.BloodSpray[1].Play();
						this.EffectPhase++;
						return;
					}
				}
				else if (this.EffectPhase == 4 && this.YandereAnim[this.AnimName].time > 4.866667f)
				{
					weapon.Spin = false;
					weapon.BloodSpray[0].Stop();
					weapon.BloodSpray[1].Stop();
					this.EffectPhase++;
					return;
				}
			}
			else if (this.EffectPhase == 0 && this.YandereAnim[this.AnimName].time > 1f)
			{
				this.Yandere.Bloodiness += 20f;
				this.Yandere.StainWeapon();
				UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, weapon.transform.position + weapon.transform.right * 0.2f + weapon.transform.forward * -0.06666667f, Quaternion.identity);
				this.EffectPhase++;
				return;
			}
		}
		else if (weapon.Type == WeaponType.Weight)
		{
			if (!this.Stealth)
			{
				if (sanityType == SanityType.High)
				{
					if (this.EffectPhase == 0 && this.YandereAnim[this.AnimName].time > 0.6666667f)
					{
						this.Yandere.Bloodiness += 20f;
						this.Yandere.StainWeapon();
						UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, weapon.transform.position + weapon.transform.forward * 0.1f, Quaternion.identity);
						this.EffectPhase++;
						return;
					}
				}
				else if (sanityType == SanityType.Medium)
				{
					if (this.EffectPhase == 0)
					{
						if (this.YandereAnim[this.AnimName].time > 1f)
						{
							this.Yandere.Bloodiness += 20f;
							this.Yandere.StainWeapon();
							UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, weapon.transform.position + weapon.transform.forward * 0.1f, Quaternion.identity);
							this.EffectPhase++;
							return;
						}
					}
					else if (this.EffectPhase == 1 && this.YandereAnim[this.AnimName].time > 2.8333333f)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, weapon.transform.position + weapon.transform.forward * 0.1f, Quaternion.identity);
						this.EffectPhase++;
						return;
					}
				}
				else if (this.EffectPhase == 0)
				{
					if (this.YandereAnim[this.AnimName].time > 2.1666667f)
					{
						this.Yandere.Bloodiness += 20f;
						this.Yandere.StainWeapon();
						UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, weapon.transform.position + weapon.transform.forward * 0.1f, Quaternion.identity);
						this.EffectPhase++;
						return;
					}
				}
				else if (this.EffectPhase == 1 && this.YandereAnim[this.AnimName].time > 4.1666665f)
				{
					UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, weapon.transform.position + weapon.transform.forward * 0.1f, Quaternion.identity);
					this.EffectPhase++;
					return;
				}
			}
			else
			{
				this.Yandere.TargetStudent.Ragdoll.NeckSnapped = true;
			}
		}
	}

	// Token: 0x06000A31 RID: 2609 RVA: 0x000534E4 File Offset: 0x000516E4
	private void LoopCheck(WeaponScript weapon)
	{
		if (Input.GetButtonDown("X") && !this.Yandere.Chased && this.Yandere.Chasers == 0)
		{
			if (weapon.Type == WeaponType.Knife)
			{
				if (this.YandereAnim[this.AnimName].time > 3.5333333f && this.YandereAnim[this.AnimName].time < 4.1666665f)
				{
					this.LoopStart = 106f;
					this.LoopEnd = 125f;
					this.LoopPhase = 2;
					this.Loop = true;
				}
			}
			else if (weapon.Type == WeaponType.Katana)
			{
				if (this.YandereAnim[this.AnimName].time > 3.3666666f && this.YandereAnim[this.AnimName].time < 3.9f)
				{
					this.LoopStart = 101f;
					this.LoopEnd = 117f;
					this.LoopPhase = 5;
					this.Loop = true;
				}
			}
			else if (weapon.Type == WeaponType.Bat)
			{
				if (this.YandereAnim[this.AnimName].time > 3.7666667f && this.YandereAnim[this.AnimName].time < 4.4f)
				{
					this.LoopStart = 113f;
					this.LoopEnd = 132f;
					this.LoopPhase = 2;
					this.Loop = true;
				}
			}
			else if (weapon.Type == WeaponType.Saw)
			{
				if (this.YandereAnim[this.AnimName].time > 3.0333333f && this.YandereAnim[this.AnimName].time < 4.5666666f)
				{
					this.LoopStart = 91f;
					this.LoopEnd = 137f;
					this.LoopPhase = 3;
					this.PingPong = true;
				}
			}
			else if (weapon.Type == WeaponType.Weight && this.YandereAnim[this.AnimName].time > 3f && this.YandereAnim[this.AnimName].time < 4.5f)
			{
				this.LoopStart = 90f;
				this.LoopEnd = 135f;
				this.LoopPhase = 1;
				this.Loop = true;
			}
		}
		AudioSource component = weapon.gameObject.GetComponent<AudioSource>();
		if (this.PingPong)
		{
			if (this.YandereAnim[this.AnimName].time > this.LoopEnd / 30f)
			{
				component.pitch = 1f + UnityEngine.Random.Range(0.1f, -0.1f);
				component.time = this.LoopStart / 30f;
				this.VictimAnim[this.VictimAnimName].speed = -1f;
				this.YandereAnim[this.AnimName].speed = -1f;
				this.EffectPhase = this.LoopPhase;
				this.AttackTimer = 0f;
			}
			else if (this.YandereAnim[this.AnimName].time < this.LoopStart / 30f)
			{
				component.pitch = 1f + UnityEngine.Random.Range(0.1f, -0.1f);
				component.time = this.LoopStart / 30f;
				this.VictimAnim[this.VictimAnimName].speed = 1f;
				this.YandereAnim[this.AnimName].speed = 1f;
				this.EffectPhase = this.LoopPhase;
				this.AttackTimer = this.LoopStart / 30f;
				this.EffectPhase = this.LoopPhase;
				this.PingPong = false;
			}
		}
		if (this.Loop && this.YandereAnim[this.AnimName].time > this.LoopEnd / 30f)
		{
			component.pitch = 1f + UnityEngine.Random.Range(0.1f, -0.1f);
			component.time = this.LoopStart / 30f;
			this.VictimAnim[this.VictimAnimName].time = this.LoopStart / 30f;
			this.YandereAnim[this.AnimName].time = this.LoopStart / 30f;
			this.AttackTimer = this.LoopStart / 30f;
			this.EffectPhase = this.LoopPhase;
			this.Loop = false;
		}
	}

	// Token: 0x06000A32 RID: 2610 RVA: 0x00053989 File Offset: 0x00051B89
	private void CheckForSpecialCase(WeaponScript weapon)
	{
		if (weapon.WeaponID == 8 && GameGlobals.Paranormal)
		{
			this.Yandere.TargetStudent.Ragdoll.Sacrifice = true;
			weapon.Effect();
		}
	}

	// Token: 0x04000A40 RID: 2624
	public GameObject BloodEffect;

	// Token: 0x04000A41 RID: 2625
	private GameObject OriginalBloodEffect;

	// Token: 0x04000A42 RID: 2626
	private GameObject Victim;

	// Token: 0x04000A43 RID: 2627
	private YandereScript Yandere;

	// Token: 0x04000A44 RID: 2628
	private string VictimAnimName = string.Empty;

	// Token: 0x04000A45 RID: 2629
	private string AnimName = string.Empty;

	// Token: 0x04000A46 RID: 2630
	public bool PingPong;

	// Token: 0x04000A47 RID: 2631
	public bool Stealth;

	// Token: 0x04000A48 RID: 2632
	public bool Censor;

	// Token: 0x04000A49 RID: 2633
	public bool Loop;

	// Token: 0x04000A4A RID: 2634
	public int EffectPhase;

	// Token: 0x04000A4B RID: 2635
	public int LoopPhase;

	// Token: 0x04000A4C RID: 2636
	public float AttackTimer;

	// Token: 0x04000A4D RID: 2637
	public float Distance;

	// Token: 0x04000A4E RID: 2638
	public float Timer;

	// Token: 0x04000A4F RID: 2639
	public float LoopStart;

	// Token: 0x04000A50 RID: 2640
	public float LoopEnd;

	// Token: 0x04000A51 RID: 2641
	public Animation YandereAnim;

	// Token: 0x04000A52 RID: 2642
	public Animation VictimAnim;
}
