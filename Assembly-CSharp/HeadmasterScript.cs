using System;
using UnityEngine;

// Token: 0x020002DD RID: 733
public class HeadmasterScript : MonoBehaviour
{
	// Token: 0x060016E2 RID: 5858 RVA: 0x000BD7F7 File Offset: 0x000BB9F7
	private void Start()
	{
		this.MyAnimation["HeadmasterRaiseTazer"].speed = 2f;
		this.Tazer.SetActive(false);
	}

	// Token: 0x060016E3 RID: 5859 RVA: 0x000BD820 File Offset: 0x000BBA20
	private void Update()
	{
		if (this.Yandere.transform.position.y > base.transform.position.y - 1f && this.Yandere.transform.position.y < base.transform.position.y + 1f && this.Yandere.transform.position.x < 6f && this.Yandere.transform.position.x > -6f)
		{
			this.Distance = Vector3.Distance(base.transform.position, this.Yandere.transform.position);
			if (this.Shooting)
			{
				this.targetRotation = Quaternion.LookRotation(base.transform.position - this.Yandere.transform.position);
				this.Yandere.transform.rotation = Quaternion.Slerp(this.Yandere.transform.rotation, this.targetRotation, Time.deltaTime * 10f);
				this.AimWeaponAtYandere();
				this.AimBodyAtYandere();
			}
			else if ((double)this.Distance < 1.2)
			{
				this.AimBodyAtYandere();
				if (this.Yandere.CanMove && !this.Yandere.Egg && !this.Shooting)
				{
					this.Shoot();
				}
			}
			else if ((double)this.Distance < 2.8)
			{
				this.PlayedSitSound = false;
				if (!this.StudentManager.Clock.StopTime)
				{
					this.PatienceTimer -= Time.deltaTime;
				}
				if (this.PatienceTimer < 0f && !this.Yandere.Egg)
				{
					this.LostPatience = true;
					this.PatienceTimer = 60f;
					this.Patience = 0;
					this.Shoot();
				}
				if (!this.LostPatience)
				{
					this.LostPatience = true;
					this.Patience--;
					if (this.Patience < 1 && !this.Yandere.Egg && !this.Shooting)
					{
						this.Shoot();
					}
				}
				this.AimBodyAtYandere();
				this.Threatened = true;
				this.AimWeaponAtYandere();
				this.ThreatTimer = Mathf.MoveTowards(this.ThreatTimer, 0f, Time.deltaTime);
				if (this.ThreatTimer == 0f)
				{
					this.ThreatID++;
					if (this.ThreatID < 5)
					{
						this.HeadmasterSubtitle.text = this.HeadmasterThreatText[this.ThreatID];
						this.MyAudio.clip = this.HeadmasterThreatClips[this.ThreatID];
						this.MyAudio.Play();
						this.ThreatTimer = this.HeadmasterThreatClips[this.ThreatID].length + 1f;
					}
				}
				this.CheckBehavior();
			}
			else if (this.Distance < 10f)
			{
				this.PlayedStandSound = false;
				this.LostPatience = false;
				this.targetRotation = Quaternion.LookRotation(new Vector3(0f, 8f, 0f) - base.transform.position);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, Time.deltaTime * 10f);
				this.Chair.localPosition = Vector3.Lerp(this.Chair.localPosition, new Vector3(this.Chair.localPosition.x, this.Chair.localPosition.y, -4.66666f), Time.deltaTime * 1f);
				this.LookAtPlayer = true;
				if (!this.Threatened)
				{
					this.MyAnimation.CrossFade("HeadmasterAttention", 1f);
					this.ScratchTimer = 0f;
					this.SpeechTimer = Mathf.MoveTowards(this.SpeechTimer, 0f, Time.deltaTime);
					if (this.SpeechTimer == 0f)
					{
						if (this.CardboardBox.parent == null && this.Yandere.Mask == null)
						{
							this.VoiceID++;
							if (this.VoiceID < 6)
							{
								this.HeadmasterSubtitle.text = this.HeadmasterSpeechText[this.VoiceID];
								this.MyAudio.clip = this.HeadmasterSpeechClips[this.VoiceID];
								this.MyAudio.Play();
								this.SpeechTimer = this.HeadmasterSpeechClips[this.VoiceID].length + 1f;
							}
						}
						else
						{
							this.BoxID++;
							if (this.BoxID < 6)
							{
								this.HeadmasterSubtitle.text = this.HeadmasterBoxText[this.BoxID];
								this.MyAudio.clip = this.HeadmasterBoxClips[this.BoxID];
								this.MyAudio.Play();
								this.SpeechTimer = this.HeadmasterBoxClips[this.BoxID].length + 1f;
							}
						}
					}
				}
				else if (!this.Relaxing)
				{
					this.HeadmasterSubtitle.text = this.HeadmasterRelaxText;
					this.MyAudio.clip = this.HeadmasterRelaxClip;
					this.MyAudio.Play();
					this.Relaxing = true;
				}
				else
				{
					if (!this.PlayedSitSound)
					{
						AudioSource.PlayClipAtPoint(this.SitDown, base.transform.position);
						this.PlayedSitSound = true;
					}
					this.MyAnimation.CrossFade("HeadmasterLowerTazer");
					this.Aiming = false;
					if ((double)this.MyAnimation["HeadmasterLowerTazer"].time > 1.33333)
					{
						this.Tazer.SetActive(false);
					}
					if (this.MyAnimation["HeadmasterLowerTazer"].time > this.MyAnimation["HeadmasterLowerTazer"].length)
					{
						this.Threatened = false;
						this.Relaxing = false;
					}
				}
				this.CheckBehavior();
			}
			else
			{
				if (this.LookAtPlayer)
				{
					this.MyAnimation.CrossFade("HeadmasterType");
					this.LookAtPlayer = false;
					this.Threatened = false;
					this.Relaxing = false;
					this.Aiming = false;
				}
				this.ScratchTimer += Time.deltaTime;
				if (this.ScratchTimer > 10f)
				{
					this.MyAnimation.CrossFade("HeadmasterScratch");
					if (this.MyAnimation["HeadmasterScratch"].time > this.MyAnimation["HeadmasterScratch"].length)
					{
						this.MyAnimation.CrossFade("HeadmasterType");
						this.ScratchTimer = 0f;
					}
				}
			}
			if (!this.MyAudio.isPlaying)
			{
				this.HeadmasterSubtitle.text = string.Empty;
				if (this.Shooting)
				{
					this.Taze();
				}
			}
			if (this.Yandere.Attacked && this.Yandere.Character.GetComponent<Animation>()["f02_swingB_00"].time >= this.Yandere.Character.GetComponent<Animation>()["f02_swingB_00"].length * 0.85f)
			{
				this.MyAudio.clip = this.Crumple;
				this.MyAudio.Play();
				base.enabled = false;
				return;
			}
		}
		else
		{
			this.HeadmasterSubtitle.text = string.Empty;
		}
	}

	// Token: 0x060016E4 RID: 5860 RVA: 0x000BDFB4 File Offset: 0x000BC1B4
	private void LateUpdate()
	{
		this.LookAtTarget = Vector3.Lerp(this.LookAtTarget, this.LookAtPlayer ? this.Yandere.Head.position : this.Default.position, Time.deltaTime * 10f);
		this.Head.LookAt(this.LookAtTarget);
	}

	// Token: 0x060016E5 RID: 5861 RVA: 0x000BE014 File Offset: 0x000BC214
	private void AimBodyAtYandere()
	{
		this.targetRotation = Quaternion.LookRotation(this.Yandere.transform.position - base.transform.position);
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, Time.deltaTime * 5f);
		this.Chair.localPosition = Vector3.Lerp(this.Chair.localPosition, new Vector3(this.Chair.localPosition.x, this.Chair.localPosition.y, -5.2f), Time.deltaTime * 1f);
	}

	// Token: 0x060016E6 RID: 5862 RVA: 0x000BE0C8 File Offset: 0x000BC2C8
	private void AimWeaponAtYandere()
	{
		if (!this.Aiming)
		{
			this.MyAnimation.CrossFade("HeadmasterRaiseTazer");
			if (!this.PlayedStandSound)
			{
				AudioSource.PlayClipAtPoint(this.StandUp, base.transform.position);
				this.PlayedStandSound = true;
			}
			if ((double)this.MyAnimation["HeadmasterRaiseTazer"].time > 1.166666)
			{
				this.Tazer.SetActive(true);
				this.Aiming = true;
				return;
			}
		}
		else if (this.MyAnimation["HeadmasterRaiseTazer"].time > this.MyAnimation["HeadmasterRaiseTazer"].length)
		{
			this.MyAnimation.CrossFade("HeadmasterAimTazer");
		}
	}

	// Token: 0x060016E7 RID: 5863 RVA: 0x000BE184 File Offset: 0x000BC384
	public void Shoot()
	{
		this.StudentManager.YandereDying = true;
		this.Yandere.StopAiming();
		this.Yandere.StopLaughing();
		this.Yandere.CharacterAnimation.CrossFade("f02_readyToFight_00");
		if (this.Patience < 1)
		{
			this.HeadmasterSubtitle.text = this.HeadmasterPatienceText;
			this.MyAudio.clip = this.HeadmasterPatienceClip;
		}
		else if (this.Yandere.Armed)
		{
			this.HeadmasterSubtitle.text = this.HeadmasterWeaponText;
			this.MyAudio.clip = this.HeadmasterWeaponClip;
		}
		else if (this.Yandere.Carrying || this.Yandere.Dragging || (this.Yandere.PickUp != null && this.Yandere.PickUp.BodyPart))
		{
			this.HeadmasterSubtitle.text = this.HeadmasterCorpseText;
			this.MyAudio.clip = this.HeadmasterCorpseClip;
		}
		else
		{
			this.HeadmasterSubtitle.text = this.HeadmasterAttackText;
			this.MyAudio.clip = this.HeadmasterAttackClip;
		}
		this.StudentManager.StopMoving();
		this.Yandere.EmptyHands();
		this.Yandere.CanMove = false;
		this.MyAudio.Play();
		this.Shooting = true;
	}

	// Token: 0x060016E8 RID: 5864 RVA: 0x000BE2EC File Offset: 0x000BC4EC
	private void CheckBehavior()
	{
		if (this.Yandere.CanMove && !this.Yandere.Egg)
		{
			if (this.Yandere.Chased || this.Yandere.Chasers > 0)
			{
				if (!this.Shooting)
				{
					this.Shoot();
					return;
				}
			}
			else if (this.Yandere.Armed)
			{
				if (!this.Shooting)
				{
					this.Shoot();
					return;
				}
			}
			else if ((this.Yandere.Carrying || this.Yandere.Dragging || (this.Yandere.PickUp != null && this.Yandere.PickUp.BodyPart)) && !this.Shooting)
			{
				this.Shoot();
			}
		}
	}

	// Token: 0x060016E9 RID: 5865 RVA: 0x000BE3B4 File Offset: 0x000BC5B4
	public void Taze()
	{
		if (this.Yandere.CanMove)
		{
			this.StudentManager.YandereDying = true;
			this.Yandere.StopAiming();
			this.Yandere.StopLaughing();
			this.StudentManager.StopMoving();
			this.Yandere.EmptyHands();
			this.Yandere.CanMove = false;
		}
		UnityEngine.Object.Instantiate<GameObject>(this.LightningEffect, this.TazerEffectTarget.position, Quaternion.identity);
		UnityEngine.Object.Instantiate<GameObject>(this.LightningEffect, this.Yandere.Spine[3].position, Quaternion.identity);
		this.MyAudio.clip = this.HeadmasterShockClip;
		this.MyAudio.Play();
		this.Yandere.CharacterAnimation.CrossFade("f02_swingB_00");
		this.Yandere.CharacterAnimation["f02_swingB_00"].time = 0.5f;
		this.Yandere.RPGCamera.enabled = false;
		this.Yandere.Attacked = true;
		this.Heartbroken.Headmaster = true;
		this.Jukebox.Volume = 0f;
		this.Shooting = false;
	}

	// Token: 0x04001E40 RID: 7744
	public StudentManagerScript StudentManager;

	// Token: 0x04001E41 RID: 7745
	public HeartbrokenScript Heartbroken;

	// Token: 0x04001E42 RID: 7746
	public YandereScript Yandere;

	// Token: 0x04001E43 RID: 7747
	public JukeboxScript Jukebox;

	// Token: 0x04001E44 RID: 7748
	public AudioClip[] HeadmasterSpeechClips;

	// Token: 0x04001E45 RID: 7749
	public AudioClip[] HeadmasterThreatClips;

	// Token: 0x04001E46 RID: 7750
	public AudioClip[] HeadmasterBoxClips;

	// Token: 0x04001E47 RID: 7751
	public AudioClip HeadmasterRelaxClip;

	// Token: 0x04001E48 RID: 7752
	public AudioClip HeadmasterAttackClip;

	// Token: 0x04001E49 RID: 7753
	public AudioClip HeadmasterCrypticClip;

	// Token: 0x04001E4A RID: 7754
	public AudioClip HeadmasterShockClip;

	// Token: 0x04001E4B RID: 7755
	public AudioClip HeadmasterPatienceClip;

	// Token: 0x04001E4C RID: 7756
	public AudioClip HeadmasterCorpseClip;

	// Token: 0x04001E4D RID: 7757
	public AudioClip HeadmasterWeaponClip;

	// Token: 0x04001E4E RID: 7758
	public AudioClip Crumple;

	// Token: 0x04001E4F RID: 7759
	public AudioClip StandUp;

	// Token: 0x04001E50 RID: 7760
	public AudioClip SitDown;

	// Token: 0x04001E51 RID: 7761
	public readonly string[] HeadmasterSpeechText = new string[]
	{
		"",
		"Ahh...! It's...it's you!",
		"No, that would be impossible...you must be...her daughter...",
		"I'll tolerate you in my school, but not in my office.",
		"Leave at once.",
		"There is nothing for you to achieve here. Just. Get. Out."
	};

	// Token: 0x04001E52 RID: 7762
	public readonly string[] HeadmasterThreatText = new string[]
	{
		"",
		"Not another step!",
		"You're up to no good! I know it!",
		"I'm not going to let you harm me!",
		"I'll use self-defense if I deem it necessary!",
		"This is your final warning. Get out of here...or else."
	};

	// Token: 0x04001E53 RID: 7763
	public readonly string[] HeadmasterBoxText = new string[]
	{
		"",
		"What...in...blazes are you doing?",
		"Are you trying to re-enact something you saw in a video game?",
		"Ugh, do you really think such a stupid ploy is going to work?",
		"I know who you are. It's obvious. You're not fooling anyone.",
		"I don't have time for this tomfoolery. Leave at once!"
	};

	// Token: 0x04001E54 RID: 7764
	public readonly string HeadmasterRelaxText = "Hmm...a wise decision.";

	// Token: 0x04001E55 RID: 7765
	public readonly string HeadmasterAttackText = "You asked for it!";

	// Token: 0x04001E56 RID: 7766
	public readonly string HeadmasterCrypticText = "Mr. Saikou...the deal is off.";

	// Token: 0x04001E57 RID: 7767
	public readonly string HeadmasterWeaponText = "How dare you raise a weapon in my office!";

	// Token: 0x04001E58 RID: 7768
	public readonly string HeadmasterPatienceText = "Enough of this nonsense!";

	// Token: 0x04001E59 RID: 7769
	public readonly string HeadmasterCorpseText = "You...you murderer!";

	// Token: 0x04001E5A RID: 7770
	public UILabel HeadmasterSubtitle;

	// Token: 0x04001E5B RID: 7771
	public Animation MyAnimation;

	// Token: 0x04001E5C RID: 7772
	public AudioSource MyAudio;

	// Token: 0x04001E5D RID: 7773
	public GameObject LightningEffect;

	// Token: 0x04001E5E RID: 7774
	public GameObject Tazer;

	// Token: 0x04001E5F RID: 7775
	public Transform TazerEffectTarget;

	// Token: 0x04001E60 RID: 7776
	public Transform CardboardBox;

	// Token: 0x04001E61 RID: 7777
	public Transform Chair;

	// Token: 0x04001E62 RID: 7778
	public Quaternion targetRotation;

	// Token: 0x04001E63 RID: 7779
	public float PatienceTimer;

	// Token: 0x04001E64 RID: 7780
	public float ScratchTimer;

	// Token: 0x04001E65 RID: 7781
	public float SpeechTimer;

	// Token: 0x04001E66 RID: 7782
	public float ThreatTimer;

	// Token: 0x04001E67 RID: 7783
	public float Distance;

	// Token: 0x04001E68 RID: 7784
	public int Patience = 10;

	// Token: 0x04001E69 RID: 7785
	public int ThreatID;

	// Token: 0x04001E6A RID: 7786
	public int VoiceID;

	// Token: 0x04001E6B RID: 7787
	public int BoxID;

	// Token: 0x04001E6C RID: 7788
	public bool PlayedStandSound;

	// Token: 0x04001E6D RID: 7789
	public bool PlayedSitSound;

	// Token: 0x04001E6E RID: 7790
	public bool LostPatience;

	// Token: 0x04001E6F RID: 7791
	public bool Threatened;

	// Token: 0x04001E70 RID: 7792
	public bool Relaxing;

	// Token: 0x04001E71 RID: 7793
	public bool Shooting;

	// Token: 0x04001E72 RID: 7794
	public bool Aiming;

	// Token: 0x04001E73 RID: 7795
	public Vector3 LookAtTarget;

	// Token: 0x04001E74 RID: 7796
	public bool LookAtPlayer;

	// Token: 0x04001E75 RID: 7797
	public Transform Default;

	// Token: 0x04001E76 RID: 7798
	public Transform Head;
}
