using System;
using UnityEngine;

// Token: 0x02000463 RID: 1123
public class WeaponScript : MonoBehaviour
{
	// Token: 0x06001D0D RID: 7437 RVA: 0x00159BEC File Offset: 0x00157DEC
	private void Start()
	{
		this.Yandere = GameObject.Find("YandereChan").GetComponent<YandereScript>();
		this.StartingPosition = base.transform.position;
		this.StartingRotation = base.transform.eulerAngles;
		Physics.IgnoreCollision(this.Yandere.GetComponent<Collider>(), this.MyCollider);
		this.OriginalColor = this.Outline[0].color;
		if (this.StartLow)
		{
			this.OriginalOffset = this.Prompt.OffsetY[3];
			this.Prompt.OffsetY[3] = 0.2f;
		}
		if (this.DisableCollider)
		{
			this.MyCollider.enabled = false;
		}
		AudioSource component = base.GetComponent<AudioSource>();
		if (component != null)
		{
			this.OriginalClip = component.clip;
		}
		this.MyRigidbody = base.GetComponent<Rigidbody>();
		this.MyRigidbody.isKinematic = true;
		Transform transform = GameObject.Find("WeaponOriginParent").transform;
		this.Origin = UnityEngine.Object.Instantiate<GameObject>(this.Prompt.Yandere.StudentManager.EmptyObject, base.transform.position, Quaternion.identity).transform;
		this.Origin.parent = transform;
	}

	// Token: 0x06001D0E RID: 7438 RVA: 0x00159D20 File Offset: 0x00157F20
	public string GetTypePrefix()
	{
		if (this.Type == WeaponType.Knife)
		{
			return "knife";
		}
		if (this.Type == WeaponType.Katana)
		{
			return "katana";
		}
		if (this.Type == WeaponType.Bat)
		{
			return "bat";
		}
		if (this.Type == WeaponType.Saw)
		{
			return "saw";
		}
		if (this.Type == WeaponType.Syringe)
		{
			return "syringe";
		}
		if (this.Type == WeaponType.Weight)
		{
			return "weight";
		}
		Debug.LogError("Weapon type \"" + this.Type.ToString() + "\" not implemented.");
		return string.Empty;
	}

	// Token: 0x06001D0F RID: 7439 RVA: 0x00159DB4 File Offset: 0x00157FB4
	public AudioClip GetClip(float sanity, bool stealth)
	{
		AudioClip[] array;
		if (this.Clips2.Length == 0)
		{
			array = this.Clips;
		}
		else
		{
			array = ((UnityEngine.Random.Range(2, 4) == 2) ? this.Clips2 : this.Clips3);
		}
		if (stealth)
		{
			return array[0];
		}
		if (sanity > 0.6666667f)
		{
			return array[1];
		}
		if (sanity > 0.33333334f)
		{
			return array[2];
		}
		return array[3];
	}

	// Token: 0x06001D10 RID: 7440 RVA: 0x00159E10 File Offset: 0x00158010
	private void Update()
	{
		if (this.WeaponID == 16 && this.Yandere.EquippedWeapon == this && Input.GetButtonDown("RB"))
		{
			this.ExtraBlade.SetActive(!this.ExtraBlade.activeInHierarchy);
		}
		if (this.Dismembering)
		{
			AudioSource component = base.GetComponent<AudioSource>();
			if (this.DismemberPhase < 4)
			{
				if (component.time > 0.75f)
				{
					if (this.Speed < 36f)
					{
						this.Speed += Time.deltaTime + 10f;
					}
					this.Rotation += this.Speed;
					this.Blade.localEulerAngles = new Vector3(this.Rotation, this.Blade.localEulerAngles.y, this.Blade.localEulerAngles.z);
				}
				if (component.time > this.SoundTime[this.DismemberPhase])
				{
					this.Yandere.Sanity -= 5f * this.Yandere.Numbness;
					this.Yandere.Bloodiness += 25f;
					this.ShortBloodSpray[0].Play();
					this.ShortBloodSpray[1].Play();
					this.Blood.enabled = true;
					this.MurderWeapon = true;
					this.DismemberPhase++;
				}
			}
			else
			{
				this.Rotation = Mathf.Lerp(this.Rotation, 0f, Time.deltaTime * 2f);
				this.Blade.localEulerAngles = new Vector3(this.Rotation, this.Blade.localEulerAngles.y, this.Blade.localEulerAngles.z);
				if (!component.isPlaying)
				{
					component.clip = this.OriginalClip;
					this.Yandere.StainWeapon();
					this.Dismembering = false;
					this.DismemberPhase = 0;
					this.Rotation = 0f;
					this.Speed = 0f;
				}
			}
		}
		else if (this.Yandere.EquippedWeapon == this)
		{
			if (this.Yandere.AttackManager.IsAttacking())
			{
				if (this.Type == WeaponType.Knife)
				{
					base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, Mathf.Lerp(base.transform.localEulerAngles.y, this.Flip ? 180f : 0f, Time.deltaTime * 10f), base.transform.localEulerAngles.z);
				}
				else if (this.Type == WeaponType.Saw && this.Spin)
				{
					this.Blade.transform.localEulerAngles = new Vector3(this.Blade.transform.localEulerAngles.x + Time.deltaTime * 360f, this.Blade.transform.localEulerAngles.y, this.Blade.transform.localEulerAngles.z);
				}
			}
		}
		else if (!this.MyRigidbody.isKinematic)
		{
			this.KinematicTimer = Mathf.MoveTowards(this.KinematicTimer, 5f, Time.deltaTime);
			if (this.KinematicTimer == 5f)
			{
				this.MyRigidbody.isKinematic = true;
				this.KinematicTimer = 0f;
			}
			if (base.transform.position.x > -71f && base.transform.position.x < -61f && base.transform.position.z > -37.5f && base.transform.position.z < -27.5f)
			{
				base.transform.position = new Vector3(-63f, 1f, -26.5f);
				this.KinematicTimer = 0f;
			}
			if (base.transform.position.x > -46f && base.transform.position.x < -18f && base.transform.position.z > 66f && base.transform.position.z < 78f)
			{
				base.transform.position = new Vector3(-16f, 5f, 72f);
				this.KinematicTimer = 0f;
			}
		}
		if (this.Rotate)
		{
			base.transform.Rotate(Vector3.forward * Time.deltaTime * 100f);
		}
	}

	// Token: 0x06001D11 RID: 7441 RVA: 0x0015A2CC File Offset: 0x001584CC
	private void LateUpdate()
	{
		if (this.Prompt.Circle[3].fillAmount == 0f)
		{
			if (this.WeaponID == 6 && SchemeGlobals.GetSchemeStage(4) == 1)
			{
				SchemeGlobals.SetSchemeStage(4, 2);
				this.Yandere.PauseScreen.Schemes.UpdateInstructions();
			}
			this.Prompt.Circle[3].fillAmount = 1f;
			if (this.Prompt.Suspicious)
			{
				this.Yandere.TheftTimer = 0.1f;
			}
			if (this.Dangerous || this.Suspicious)
			{
				this.Yandere.WeaponTimer = 0.1f;
			}
			if (!this.Yandere.Gloved)
			{
				this.FingerprintID = 100;
			}
			this.ID = 0;
			while (this.ID < this.Outline.Length)
			{
				this.Outline[this.ID].color = new Color(0f, 0f, 0f, 1f);
				this.ID++;
			}
			base.transform.parent = this.Yandere.ItemParent;
			base.transform.localPosition = Vector3.zero;
			if (this.Type == WeaponType.Bat)
			{
				base.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
			}
			else
			{
				base.transform.localEulerAngles = Vector3.zero;
			}
			this.MyCollider.enabled = false;
			this.MyRigidbody.constraints = RigidbodyConstraints.FreezeAll;
			if (this.Yandere.Equipped == 3)
			{
				this.Yandere.Weapon[3].Drop();
			}
			if (this.Yandere.PickUp != null)
			{
				this.Yandere.PickUp.Drop();
			}
			if (this.Yandere.Dragging)
			{
				this.Yandere.Ragdoll.GetComponent<RagdollScript>().StopDragging();
			}
			if (this.Yandere.Carrying)
			{
				this.Yandere.StopCarrying();
			}
			if (this.Concealable)
			{
				if (this.Yandere.Weapon[1] == null)
				{
					if (this.Yandere.Weapon[2] != null)
					{
						this.Yandere.Weapon[2].gameObject.SetActive(false);
					}
					this.Yandere.Equipped = 1;
					this.Yandere.EquippedWeapon = this;
				}
				else if (this.Yandere.Weapon[2] == null)
				{
					if (this.Yandere.Weapon[1] != null)
					{
						this.Yandere.Weapon[1].gameObject.SetActive(false);
					}
					this.Yandere.Equipped = 2;
					this.Yandere.EquippedWeapon = this;
				}
				else if (this.Yandere.Weapon[2].gameObject.activeInHierarchy)
				{
					this.Yandere.Weapon[2].Drop();
					this.Yandere.Equipped = 2;
					this.Yandere.EquippedWeapon = this;
				}
				else
				{
					this.Yandere.Weapon[1].Drop();
					this.Yandere.Equipped = 1;
					this.Yandere.EquippedWeapon = this;
				}
			}
			else
			{
				if (this.Yandere.Weapon[1] != null)
				{
					this.Yandere.Weapon[1].gameObject.SetActive(false);
				}
				if (this.Yandere.Weapon[2] != null)
				{
					this.Yandere.Weapon[2].gameObject.SetActive(false);
				}
				this.Yandere.Equipped = 3;
				this.Yandere.EquippedWeapon = this;
			}
			this.Yandere.StudentManager.UpdateStudents(0);
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			this.Yandere.NearestPrompt = null;
			if (this.WeaponID == 9 || this.WeaponID == 10 || this.WeaponID == 12 || this.WeaponID == 25)
			{
				this.SuspicionCheck();
			}
			if (this.Yandere.EquippedWeapon.Suspicious)
			{
				if (!this.Yandere.WeaponWarning)
				{
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Armed);
					this.Yandere.WeaponWarning = true;
				}
			}
			else
			{
				this.Yandere.WeaponWarning = false;
			}
			this.Yandere.WeaponMenu.UpdateSprites();
			this.Yandere.WeaponManager.UpdateLabels();
			if (this.Evidence)
			{
				this.Yandere.Police.BloodyWeapons--;
			}
			if (this.WeaponID == 11)
			{
				this.Yandere.IdleAnim = "CyborgNinja_Idle_Armed";
				this.Yandere.WalkAnim = "CyborgNinja_Walk_Armed";
				this.Yandere.RunAnim = "CyborgNinja_Run_Armed";
			}
			if (this.WeaponID == 26)
			{
				this.WeaponTrail.SetActive(true);
			}
			this.KinematicTimer = 0f;
			AudioSource.PlayClipAtPoint(this.EquipClip, this.Yandere.MainCamera.transform.position);
		}
		if (this.Yandere.EquippedWeapon == this && this.Yandere.Armed)
		{
			base.transform.localScale = new Vector3(1f, 1f, 1f);
			if (!this.Yandere.Struggling)
			{
				if (this.Yandere.CanMove)
				{
					base.transform.localPosition = Vector3.zero;
					if (this.Type == WeaponType.Bat)
					{
						base.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
					}
					else
					{
						base.transform.localEulerAngles = Vector3.zero;
					}
				}
			}
			else
			{
				base.transform.localPosition = new Vector3(-0.01f, 0.005f, -0.01f);
			}
		}
		if (this.Dumped)
		{
			this.DumpTimer += Time.deltaTime;
			if (this.DumpTimer > 1f)
			{
				this.Yandere.Incinerator.MurderWeapons++;
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
		if (base.transform.parent == this.Yandere.ItemParent && this.Concealable && this.Yandere.Weapon[1] != this && this.Yandere.Weapon[2] != this)
		{
			this.Drop();
		}
	}

	// Token: 0x06001D12 RID: 7442 RVA: 0x0015A954 File Offset: 0x00158B54
	public void Drop()
	{
		if (this.WeaponID == 6 && SchemeGlobals.GetSchemeStage(4) == 2)
		{
			SchemeGlobals.SetSchemeStage(4, 1);
			this.Yandere.PauseScreen.Schemes.UpdateInstructions();
		}
		Debug.Log("A " + base.gameObject.name + " has been dropped.");
		if (this.WeaponID == 11)
		{
			this.Yandere.IdleAnim = "CyborgNinja_Idle_Unarmed";
			this.Yandere.WalkAnim = this.Yandere.OriginalWalkAnim;
			this.Yandere.RunAnim = "CyborgNinja_Run_Unarmed";
		}
		if (this.StartLow)
		{
			this.Prompt.OffsetY[3] = this.OriginalOffset;
		}
		if (this.Yandere.EquippedWeapon == this)
		{
			this.Yandere.EquippedWeapon = null;
			this.Yandere.Equipped = 0;
			this.Yandere.StudentManager.UpdateStudents(0);
		}
		base.gameObject.SetActive(true);
		base.transform.parent = null;
		this.MyRigidbody.constraints = RigidbodyConstraints.None;
		this.MyRigidbody.isKinematic = false;
		this.MyRigidbody.useGravity = true;
		this.MyCollider.isTrigger = false;
		if (this.Dumped)
		{
			base.transform.position = this.Incinerator.DumpPoint.position;
		}
		else
		{
			this.Prompt.enabled = true;
			this.MyCollider.enabled = true;
			if (this.Yandere.GetComponent<Collider>().enabled)
			{
				Physics.IgnoreCollision(this.Yandere.GetComponent<Collider>(), this.MyCollider);
			}
		}
		if (this.Evidence)
		{
			this.Yandere.Police.BloodyWeapons++;
		}
		if (Vector3.Distance(this.StartingPosition, base.transform.position) > 5f && Vector3.Distance(base.transform.position, this.Yandere.StudentManager.WeaponBoxSpot.parent.position) > 1f)
		{
			if (!this.Misplaced)
			{
				this.Prompt.Yandere.WeaponManager.MisplacedWeapons++;
				this.Misplaced = true;
			}
		}
		else if (this.Misplaced)
		{
			this.Prompt.Yandere.WeaponManager.MisplacedWeapons--;
			this.Misplaced = false;
		}
		this.ID = 0;
		while (this.ID < this.Outline.Length)
		{
			this.Outline[this.ID].color = (this.Evidence ? this.EvidenceColor : this.OriginalColor);
			this.ID++;
		}
		if (base.transform.position.y > 1000f)
		{
			base.transform.position = new Vector3(12f, 0f, 28f);
		}
		if (this.WeaponID == 26)
		{
			base.transform.parent = this.Parent;
			base.transform.localEulerAngles = Vector3.zero;
			base.transform.localPosition = Vector3.zero;
			this.MyRigidbody.isKinematic = true;
			this.WeaponTrail.SetActive(false);
		}
	}

	// Token: 0x06001D13 RID: 7443 RVA: 0x0015AC94 File Offset: 0x00158E94
	public void UpdateLabel()
	{
		if (this != null && base.gameObject.activeInHierarchy)
		{
			if (this.Yandere.Weapon[1] != null && this.Yandere.Weapon[2] != null && this.Concealable)
			{
				if (this.Prompt.Label[3] != null)
				{
					if (!this.Yandere.Armed || this.Yandere.Equipped == 3)
					{
						this.Prompt.Label[3].text = "     Swap " + this.Yandere.Weapon[1].Name + " for " + this.Name;
						return;
					}
					this.Prompt.Label[3].text = "     Swap " + this.Yandere.EquippedWeapon.Name + " for " + this.Name;
					return;
				}
			}
			else if (this.Prompt.Label[3] != null)
			{
				this.Prompt.Label[3].text = "     " + this.Name;
			}
		}
	}

	// Token: 0x06001D14 RID: 7444 RVA: 0x0015ADD4 File Offset: 0x00158FD4
	public void Effect()
	{
		if (this.WeaponID == 7)
		{
			this.BloodSpray[0].Play();
			this.BloodSpray[1].Play();
			return;
		}
		if (this.WeaponID == 8)
		{
			base.gameObject.GetComponent<ParticleSystem>().Play();
			base.GetComponent<AudioSource>().clip = this.OriginalClip;
			base.GetComponent<AudioSource>().Play();
			return;
		}
		if (this.WeaponID == 2 || this.WeaponID == 9 || this.WeaponID == 10 || this.WeaponID == 12 || this.WeaponID == 13)
		{
			base.GetComponent<AudioSource>().Play();
			return;
		}
		if (this.WeaponID == 14)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.HeartBurst, this.Yandere.TargetStudent.Head.position, Quaternion.identity);
			base.GetComponent<AudioSource>().Play();
		}
	}

	// Token: 0x06001D15 RID: 7445 RVA: 0x0015AEB3 File Offset: 0x001590B3
	public void Dismember()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		component.clip = this.DismemberClip;
		component.Play();
		this.Dismembering = true;
	}

	// Token: 0x06001D16 RID: 7446 RVA: 0x0015AED4 File Offset: 0x001590D4
	public void SuspicionCheck()
	{
		Debug.Log("Suspicion Check!");
		if ((this.WeaponID == 9 && ClubGlobals.Club == ClubType.Sports) || (this.WeaponID == 10 && ClubGlobals.Club == ClubType.Gardening) || (this.WeaponID == 12 && ClubGlobals.Club == ClubType.Sports) || (this.WeaponID == 25 && ClubGlobals.Club == ClubType.LightMusic))
		{
			this.Suspicious = false;
		}
		else
		{
			this.Suspicious = true;
		}
		if (this.Bloody)
		{
			this.Suspicious = true;
		}
	}

	// Token: 0x0400368A RID: 13962
	public ParticleSystem[] ShortBloodSpray;

	// Token: 0x0400368B RID: 13963
	public ParticleSystem[] BloodSpray;

	// Token: 0x0400368C RID: 13964
	public OutlineScript[] Outline;

	// Token: 0x0400368D RID: 13965
	public float[] SoundTime;

	// Token: 0x0400368E RID: 13966
	public IncineratorScript Incinerator;

	// Token: 0x0400368F RID: 13967
	public StudentScript Returner;

	// Token: 0x04003690 RID: 13968
	public YandereScript Yandere;

	// Token: 0x04003691 RID: 13969
	public PromptScript Prompt;

	// Token: 0x04003692 RID: 13970
	public Transform Origin;

	// Token: 0x04003693 RID: 13971
	public Transform Parent;

	// Token: 0x04003694 RID: 13972
	public AudioClip[] Clips;

	// Token: 0x04003695 RID: 13973
	public AudioClip[] Clips2;

	// Token: 0x04003696 RID: 13974
	public AudioClip[] Clips3;

	// Token: 0x04003697 RID: 13975
	public AudioClip DismemberClip;

	// Token: 0x04003698 RID: 13976
	public AudioClip EquipClip;

	// Token: 0x04003699 RID: 13977
	public ParticleSystem FireEffect;

	// Token: 0x0400369A RID: 13978
	public GameObject WeaponTrail;

	// Token: 0x0400369B RID: 13979
	public GameObject ExtraBlade;

	// Token: 0x0400369C RID: 13980
	public AudioSource FireAudio;

	// Token: 0x0400369D RID: 13981
	public Rigidbody MyRigidbody;

	// Token: 0x0400369E RID: 13982
	public Collider MyCollider;

	// Token: 0x0400369F RID: 13983
	public Renderer MyRenderer;

	// Token: 0x040036A0 RID: 13984
	public Transform Blade;

	// Token: 0x040036A1 RID: 13985
	public Projector Blood;

	// Token: 0x040036A2 RID: 13986
	public Vector3 StartingPosition;

	// Token: 0x040036A3 RID: 13987
	public Vector3 StartingRotation;

	// Token: 0x040036A4 RID: 13988
	public bool AlreadyExamined;

	// Token: 0x040036A5 RID: 13989
	public bool DisableCollider;

	// Token: 0x040036A6 RID: 13990
	public bool Dismembering;

	// Token: 0x040036A7 RID: 13991
	public bool MurderWeapon;

	// Token: 0x040036A8 RID: 13992
	public bool WeaponEffect;

	// Token: 0x040036A9 RID: 13993
	public bool Concealable;

	// Token: 0x040036AA RID: 13994
	public bool Suspicious;

	// Token: 0x040036AB RID: 13995
	public bool Dangerous;

	// Token: 0x040036AC RID: 13996
	public bool Misplaced;

	// Token: 0x040036AD RID: 13997
	public bool Evidence;

	// Token: 0x040036AE RID: 13998
	public bool StartLow;

	// Token: 0x040036AF RID: 13999
	public bool Flaming;

	// Token: 0x040036B0 RID: 14000
	public bool Bloody;

	// Token: 0x040036B1 RID: 14001
	public bool Dumped;

	// Token: 0x040036B2 RID: 14002
	public bool Heated;

	// Token: 0x040036B3 RID: 14003
	public bool Rotate;

	// Token: 0x040036B4 RID: 14004
	public bool Metal;

	// Token: 0x040036B5 RID: 14005
	public bool Flip;

	// Token: 0x040036B6 RID: 14006
	public bool Spin;

	// Token: 0x040036B7 RID: 14007
	public Color EvidenceColor;

	// Token: 0x040036B8 RID: 14008
	public Color OriginalColor;

	// Token: 0x040036B9 RID: 14009
	public float OriginalOffset;

	// Token: 0x040036BA RID: 14010
	public float KinematicTimer;

	// Token: 0x040036BB RID: 14011
	public float DumpTimer;

	// Token: 0x040036BC RID: 14012
	public float Rotation;

	// Token: 0x040036BD RID: 14013
	public float Speed;

	// Token: 0x040036BE RID: 14014
	public string SpriteName;

	// Token: 0x040036BF RID: 14015
	public string Name;

	// Token: 0x040036C0 RID: 14016
	public int DismemberPhase;

	// Token: 0x040036C1 RID: 14017
	public int FingerprintID;

	// Token: 0x040036C2 RID: 14018
	public int GlobalID;

	// Token: 0x040036C3 RID: 14019
	public int WeaponID;

	// Token: 0x040036C4 RID: 14020
	public int AnimID;

	// Token: 0x040036C5 RID: 14021
	public WeaponType Type = WeaponType.Knife;

	// Token: 0x040036C6 RID: 14022
	public bool[] Victims;

	// Token: 0x040036C7 RID: 14023
	private AudioClip OriginalClip;

	// Token: 0x040036C8 RID: 14024
	private int ID;

	// Token: 0x040036C9 RID: 14025
	public GameObject HeartBurst;
}
