using System;
using UnityEngine;

// Token: 0x0200029F RID: 671
public class FanCoverScript : MonoBehaviour
{
	// Token: 0x06001401 RID: 5121 RVA: 0x000AE9F0 File Offset: 0x000ACBF0
	private void Start()
	{
		if (this.StudentManager.Students[this.RivalID] == null)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			base.enabled = false;
			return;
		}
		this.Rival = this.StudentManager.Students[this.RivalID];
	}

	// Token: 0x06001402 RID: 5122 RVA: 0x000AEA50 File Offset: 0x000ACC50
	private void Update()
	{
		if (Vector3.Distance(base.transform.position, this.Yandere.transform.position) < 2f)
		{
			if (this.Yandere.Armed)
			{
				this.Prompt.HideButton[0] = (this.Yandere.EquippedWeapon.WeaponID != 6 || !this.Rival.Meeting);
			}
			else
			{
				this.Prompt.HideButton[0] = true;
			}
		}
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Yandere.CharacterAnimation.CrossFade("f02_fanMurderA_00");
			this.Rival.CharacterAnimation.CrossFade("f02_fanMurderB_00");
			this.Rival.OsanaHair.GetComponent<Animation>().CrossFade("fanMurderHair");
			this.Yandere.EmptyHands();
			this.Rival.OsanaHair.transform.parent = this.Rival.transform;
			this.Rival.OsanaHair.transform.localEulerAngles = Vector3.zero;
			this.Rival.OsanaHair.transform.localPosition = Vector3.zero;
			this.Rival.OsanaHair.transform.localScale = new Vector3(1f, 1f, 1f);
			this.Rival.OsanaHairL.enabled = false;
			this.Rival.OsanaHairR.enabled = false;
			this.Rival.Distracted = true;
			this.Yandere.CanMove = false;
			this.Rival.Meeting = false;
			this.FanSFX.enabled = false;
			base.GetComponent<AudioSource>().Play();
			base.transform.localPosition = new Vector3(-1.733f, 0.465f, 0.952f);
			base.transform.localEulerAngles = new Vector3(-90f, 165f, 0f);
			Physics.SyncTransforms();
			Rigidbody component = base.GetComponent<Rigidbody>();
			component.isKinematic = false;
			component.useGravity = true;
			this.Prompt.enabled = false;
			this.Prompt.Hide();
			this.Phase++;
		}
		if (this.Phase > 0)
		{
			if (this.Phase == 1)
			{
				this.Yandere.transform.rotation = Quaternion.Slerp(this.Yandere.transform.rotation, this.MurderSpot.rotation, Time.deltaTime * 10f);
				this.Yandere.MoveTowardsTarget(this.MurderSpot.position);
				if (this.Yandere.CharacterAnimation["f02_fanMurderA_00"].time > 3.5f && !this.Reacted)
				{
					AudioSource.PlayClipAtPoint(this.RivalReaction, this.Rival.transform.position + new Vector3(0f, 1f, 0f));
					this.Yandere.MurderousActionTimer = this.Yandere.CharacterAnimation["f02_fanMurderA_00"].length - 3.5f;
					this.Reacted = true;
				}
				if (this.Yandere.CharacterAnimation["f02_fanMurderA_00"].time > 5f)
				{
					this.Rival.LiquidProjector.material.mainTexture = this.Rival.BloodTexture;
					this.Rival.LiquidProjector.enabled = true;
					this.Rival.EyeShrink = 1f;
					this.Yandere.BloodTextures = this.YandereBloodTextures;
					this.Yandere.Bloodiness += 20f;
					this.BloodProjector.gameObject.SetActive(true);
					this.BloodProjector.material.mainTexture = this.BloodTexture[1];
					this.BloodEffects.transform.parent = this.Rival.Head;
					this.BloodEffects.transform.localPosition = new Vector3(0f, 0.1f, 0f);
					this.BloodEffects.Play();
					this.Phase++;
					return;
				}
			}
			else if (this.Phase < 10)
			{
				if (this.Phase < 6)
				{
					this.Timer += Time.deltaTime;
					if (this.Timer > 1f)
					{
						this.Phase++;
						if (this.Phase - 1 < 5)
						{
							this.BloodProjector.material.mainTexture = this.BloodTexture[this.Phase - 1];
							this.Yandere.Bloodiness += 20f;
							this.Timer = 0f;
						}
					}
				}
				if (this.Rival.CharacterAnimation["f02_fanMurderB_00"].time >= this.Rival.CharacterAnimation["f02_fanMurderB_00"].length)
				{
					this.BloodProjector.material.mainTexture = this.BloodTexture[5];
					this.Yandere.Bloodiness += 20f;
					this.Rival.Ragdoll.Decapitated = true;
					this.Rival.OsanaHair.SetActive(false);
					this.Rival.DeathType = DeathType.Weapon;
					this.Rival.BecomeRagdoll();
					this.BloodEffects.Stop();
					this.Explosion.SetActive(true);
					this.Smoke.SetActive(true);
					this.Fan.enabled = false;
					this.Phase = 10;
					return;
				}
			}
			else if (this.Yandere.CharacterAnimation["f02_fanMurderA_00"].time >= this.Yandere.CharacterAnimation["f02_fanMurderA_00"].length)
			{
				this.OfferHelp.SetActive(false);
				this.Yandere.CanMove = true;
				base.enabled = false;
			}
		}
	}

	// Token: 0x04001C0D RID: 7181
	public StudentManagerScript StudentManager;

	// Token: 0x04001C0E RID: 7182
	public YandereScript Yandere;

	// Token: 0x04001C0F RID: 7183
	public PromptScript Prompt;

	// Token: 0x04001C10 RID: 7184
	public StudentScript Rival;

	// Token: 0x04001C11 RID: 7185
	public SM_rotateThis Fan;

	// Token: 0x04001C12 RID: 7186
	public ParticleSystem BloodEffects;

	// Token: 0x04001C13 RID: 7187
	public Projector BloodProjector;

	// Token: 0x04001C14 RID: 7188
	public Rigidbody MyRigidbody;

	// Token: 0x04001C15 RID: 7189
	public Transform MurderSpot;

	// Token: 0x04001C16 RID: 7190
	public GameObject Explosion;

	// Token: 0x04001C17 RID: 7191
	public GameObject OfferHelp;

	// Token: 0x04001C18 RID: 7192
	public GameObject Smoke;

	// Token: 0x04001C19 RID: 7193
	public AudioClip RivalReaction;

	// Token: 0x04001C1A RID: 7194
	public AudioSource FanSFX;

	// Token: 0x04001C1B RID: 7195
	public Texture[] YandereBloodTextures;

	// Token: 0x04001C1C RID: 7196
	public Texture[] BloodTexture;

	// Token: 0x04001C1D RID: 7197
	public bool Reacted;

	// Token: 0x04001C1E RID: 7198
	public float Timer;

	// Token: 0x04001C1F RID: 7199
	public int RivalID = 11;

	// Token: 0x04001C20 RID: 7200
	public int Phase;
}
