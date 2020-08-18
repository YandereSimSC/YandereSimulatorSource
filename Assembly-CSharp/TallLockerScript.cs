using System;
using UnityEngine;

// Token: 0x02000413 RID: 1043
public class TallLockerScript : MonoBehaviour
{
	// Token: 0x06001BF8 RID: 7160 RVA: 0x0014BFA7 File Offset: 0x0014A1A7
	private void Start()
	{
		this.Prompt.HideButton[1] = true;
		this.Prompt.HideButton[2] = true;
		this.Prompt.HideButton[3] = true;
	}

	// Token: 0x06001BF9 RID: 7161 RVA: 0x0014BFD4 File Offset: 0x0014A1D4
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f && !this.Yandere.Chased && this.Yandere.Chasers == 0)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if (!this.Open)
			{
				this.Open = true;
				if (this.YandereLocker)
				{
					if (!this.Yandere.ClubAttire || (this.Yandere.ClubAttire && this.Yandere.Bloodiness > 0f))
					{
						if (this.Yandere.Bloodiness == 0f)
						{
							if (!this.Bloody[1])
							{
								this.Prompt.HideButton[1] = false;
							}
							if (!this.Bloody[2])
							{
								this.Prompt.HideButton[2] = false;
							}
							if (!this.Bloody[3])
							{
								this.Prompt.HideButton[3] = false;
							}
						}
						else if (this.Yandere.Schoolwear > 0)
						{
							if (!this.Yandere.ClubAttire)
							{
								this.Prompt.HideButton[this.Yandere.Schoolwear] = false;
							}
							else
							{
								this.Prompt.HideButton[1] = false;
							}
						}
					}
					else
					{
						this.Prompt.HideButton[1] = true;
						this.Prompt.HideButton[2] = true;
						this.Prompt.HideButton[3] = true;
					}
				}
				this.UpdateSchoolwear();
				this.Prompt.Label[0].text = "     Close";
			}
			else
			{
				this.Open = false;
				this.Prompt.HideButton[1] = true;
				this.Prompt.HideButton[2] = true;
				this.Prompt.HideButton[3] = true;
				this.Prompt.Label[0].text = "     Open";
			}
		}
		if (!this.Open)
		{
			if (this.YandereLocker)
			{
				this.Rotation = Mathf.Lerp(this.Rotation, 0f, Time.deltaTime * 10f);
			}
			this.Prompt.HideButton[1] = true;
			this.Prompt.HideButton[2] = true;
			this.Prompt.HideButton[3] = true;
		}
		else
		{
			if (this.YandereLocker)
			{
				this.Rotation = Mathf.Lerp(this.Rotation, -180f, Time.deltaTime * 10f);
			}
			if (this.Prompt.Circle[1].fillAmount == 0f)
			{
				this.Yandere.EmptyHands();
				if (this.Yandere.ClubAttire)
				{
					this.RemovingClubAttire = true;
				}
				this.Yandere.PreviousSchoolwear = this.Yandere.Schoolwear;
				if (this.Yandere.Schoolwear == 1)
				{
					this.Yandere.Schoolwear = 0;
					if (!this.Removed[1])
					{
						if (this.Yandere.Bloodiness == 0f)
						{
							this.DropCleanUniform = true;
						}
					}
					else
					{
						this.Removed[1] = false;
					}
				}
				else
				{
					this.Yandere.Schoolwear = 1;
					this.Removed[1] = true;
				}
				this.SpawnSteam();
				this.Yandere.CurrentUniformOrigin = 1;
			}
			else if (this.Prompt.Circle[2].fillAmount == 0f)
			{
				bool flag = false;
				if (this.Yandere.Schoolwear > 0)
				{
					Debug.Log("Checking to see if it's okay for the player to take off clothing.");
					this.CheckAvailableUniforms();
					if (this.AvailableUniforms > 0)
					{
						flag = true;
					}
				}
				else
				{
					flag = true;
				}
				if (flag)
				{
					this.Yandere.EmptyHands();
					if (this.Yandere.ClubAttire)
					{
						this.RemovingClubAttire = true;
					}
					this.Yandere.PreviousSchoolwear = this.Yandere.Schoolwear;
					if (this.Yandere.Schoolwear == 1 && !this.Removed[1])
					{
						this.DropCleanUniform = true;
					}
					if (this.Yandere.Schoolwear == 2)
					{
						this.Yandere.Schoolwear = 0;
						this.Removed[2] = false;
					}
					else
					{
						this.Yandere.Schoolwear = 2;
						this.Removed[2] = true;
					}
					this.SpawnSteam();
					this.Yandere.CurrentUniformOrigin = 1;
				}
				else
				{
					this.Prompt.Circle[2].fillAmount = 1f;
					Debug.Log("Error Message.");
				}
			}
			else if (this.Prompt.Circle[3].fillAmount == 0f)
			{
				this.Yandere.EmptyHands();
				if (this.Yandere.ClubAttire)
				{
					this.RemovingClubAttire = true;
				}
				this.Yandere.PreviousSchoolwear = this.Yandere.Schoolwear;
				if (this.Yandere.Schoolwear == 1 && !this.Removed[1])
				{
					this.DropCleanUniform = true;
				}
				if (this.Yandere.Schoolwear == 3)
				{
					this.Yandere.Schoolwear = 0;
					this.Removed[3] = false;
				}
				else
				{
					this.Yandere.Schoolwear = 3;
					this.Removed[3] = true;
				}
				this.SpawnSteam();
				this.Yandere.CurrentUniformOrigin = 1;
			}
		}
		if (this.YandereLocker)
		{
			this.Hinge.localEulerAngles = new Vector3(0f, this.Rotation, 0f);
		}
		if (this.SteamCountdown)
		{
			this.Timer += Time.deltaTime;
			if (this.Phase == 1)
			{
				if (this.Timer > 1.5f)
				{
					if (this.YandereLocker)
					{
						if (this.Yandere.Gloved)
						{
							this.Yandere.Gloves.GetComponent<PickUpScript>().MyRigidbody.isKinematic = false;
							this.Yandere.Gloves.transform.localPosition = new Vector3(0f, 1f, -1f);
							this.Yandere.Gloves.transform.parent = null;
							this.Yandere.GloveAttacher.newRenderer.enabled = false;
							this.Yandere.Gloves.gameObject.SetActive(true);
							this.Yandere.Gloved = false;
							this.Yandere.Gloves = null;
						}
						this.Yandere.ChangeSchoolwear();
						if (this.Yandere.Bloodiness > 0f)
						{
							PickUpScript component;
							if (this.RemovingClubAttire)
							{
								component = UnityEngine.Object.Instantiate<GameObject>(this.BloodyClubUniform[(int)ClubGlobals.Club], this.Yandere.transform.position + Vector3.forward * 0.5f + Vector3.up, Quaternion.identity).GetComponent<PickUpScript>();
								this.StudentManager.ChangingBooths[(int)ClubGlobals.Club].CannotChange = true;
								this.StudentManager.ChangingBooths[(int)ClubGlobals.Club].CheckYandereClub();
								this.Prompt.HideButton[1] = true;
								this.Prompt.HideButton[2] = true;
								this.Prompt.HideButton[3] = true;
								this.RemovingClubAttire = false;
							}
							else
							{
								component = UnityEngine.Object.Instantiate<GameObject>(this.BloodyUniform[this.Yandere.PreviousSchoolwear], this.Yandere.transform.position + Vector3.forward * 0.5f + Vector3.up, Quaternion.identity).GetComponent<PickUpScript>();
								this.Prompt.HideButton[this.Yandere.PreviousSchoolwear] = true;
								this.Bloody[this.Yandere.PreviousSchoolwear] = true;
							}
							if (this.Yandere.RedPaint)
							{
								component.RedPaint = true;
							}
						}
					}
					else
					{
						if (this.Student.Schoolwear == 0 && !this.RivalPhone.gameObject.activeInHierarchy && !this.Yandere.Inventory.RivalPhone)
						{
							this.RivalPhone.transform.parent = this.StudentManager.StrippingPositions[this.Student.GirlID];
							this.RivalPhone.transform.localPosition = new Vector3(0f, 0.92f, 0.2375f);
							this.RivalPhone.transform.localEulerAngles = new Vector3(-80f, 0f, 0f);
							this.RivalPhone.gameObject.SetActive(true);
							this.RivalPhone.StudentID = this.Student.StudentID;
							this.RivalPhone.MyRenderer.material.mainTexture = this.Student.SmartPhone.GetComponent<Renderer>().material.mainTexture;
						}
						this.Student.ChangeSchoolwear();
					}
					this.UpdateSchoolwear();
					this.Phase++;
					return;
				}
			}
			else if (this.Timer > 3f)
			{
				if (!this.YandereLocker)
				{
					this.Student.BathePhase++;
				}
				this.SteamCountdown = false;
				this.Phase = 1;
				this.Timer = 0f;
			}
		}
	}

	// Token: 0x06001BFA RID: 7162 RVA: 0x0014C8C8 File Offset: 0x0014AAC8
	public void SpawnSteam()
	{
		this.SteamCountdown = true;
		if (this.YandereLocker)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.SteamCloud, this.Yandere.transform.position + Vector3.up * 0.81f, Quaternion.identity);
			this.Yandere.Character.GetComponent<Animation>().CrossFade("f02_stripping_00");
			this.Yandere.Stripping = true;
			this.Yandere.CanMove = false;
			return;
		}
		UnityEngine.Object.Instantiate<GameObject>(this.SteamCloud, this.Student.transform.position + Vector3.up * 0.81f, Quaternion.identity).transform.parent = this.Student.transform;
		this.Student.CharacterAnimation.CrossFade(this.Student.StripAnim);
		this.Student.Pathfinding.canSearch = false;
		this.Student.Pathfinding.canMove = false;
	}

	// Token: 0x06001BFB RID: 7163 RVA: 0x0014C9D4 File Offset: 0x0014ABD4
	public void SpawnSteamNoSideEffects(StudentScript SteamStudent)
	{
		Debug.Log("Changing clothes, no strings attached.");
		UnityEngine.Object.Instantiate<GameObject>(this.SteamCloud, SteamStudent.transform.position + Vector3.up * 0.81f, Quaternion.identity).transform.parent = SteamStudent.transform;
		SteamStudent.CharacterAnimation.CrossFade(SteamStudent.StripAnim);
		SteamStudent.Pathfinding.canSearch = false;
		SteamStudent.Pathfinding.canMove = false;
		SteamStudent.MustChangeClothing = false;
		SteamStudent.Stripping = true;
		SteamStudent.Routine = false;
	}

	// Token: 0x06001BFC RID: 7164 RVA: 0x0014CA68 File Offset: 0x0014AC68
	public void UpdateSchoolwear()
	{
		if (this.DropCleanUniform)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.CleanUniform, this.Yandere.transform.position + Vector3.forward * -0.5f + Vector3.up, Quaternion.identity);
			this.DropCleanUniform = false;
		}
		if (!this.Bloody[1])
		{
			this.Schoolwear[1].SetActive(true);
		}
		if (!this.Bloody[2])
		{
			this.Schoolwear[2].SetActive(true);
		}
		if (!this.Bloody[3])
		{
			this.Schoolwear[3].SetActive(true);
		}
		this.Prompt.Label[1].text = "     School Uniform";
		this.Prompt.Label[2].text = "     School Swimsuit";
		this.Prompt.Label[3].text = "     Gym Uniform";
		if (this.YandereLocker)
		{
			if (this.Yandere.ClubAttire)
			{
				this.Prompt.Label[1].text = "     Towel";
				return;
			}
			if (this.Yandere.Schoolwear > 0)
			{
				this.Prompt.Label[this.Yandere.Schoolwear].text = "     Towel";
				if (this.Removed[this.Yandere.Schoolwear])
				{
					this.Schoolwear[this.Yandere.Schoolwear].SetActive(false);
					return;
				}
			}
		}
		else if (this.Student != null && this.Student.Schoolwear > 0)
		{
			this.Prompt.HideButton[this.Student.Schoolwear] = true;
			this.Schoolwear[this.Student.Schoolwear].SetActive(false);
			this.Student.Indoors = true;
		}
	}

	// Token: 0x06001BFD RID: 7165 RVA: 0x0014CC3C File Offset: 0x0014AE3C
	public void UpdateButtons()
	{
		if (!this.Yandere.ClubAttire || (this.Yandere.ClubAttire && this.Yandere.Bloodiness > 0f))
		{
			if (this.Open)
			{
				if (this.Yandere.Bloodiness > 0f)
				{
					this.Prompt.HideButton[1] = true;
					this.Prompt.HideButton[2] = true;
					this.Prompt.HideButton[3] = true;
					if (this.Yandere.Schoolwear > 0 && !this.Yandere.ClubAttire)
					{
						this.Prompt.HideButton[this.Yandere.Schoolwear] = false;
					}
					if (this.Yandere.ClubAttire)
					{
						Debug.Log("Don't hide Prompt 1!");
						this.Prompt.HideButton[1] = false;
						return;
					}
				}
				else
				{
					if (!this.Bloody[1])
					{
						this.Prompt.HideButton[1] = false;
					}
					if (!this.Bloody[2])
					{
						this.Prompt.HideButton[2] = false;
					}
					if (!this.Bloody[3])
					{
						this.Prompt.HideButton[3] = false;
						return;
					}
				}
			}
		}
		else
		{
			this.Prompt.HideButton[1] = true;
			this.Prompt.HideButton[2] = true;
			this.Prompt.HideButton[3] = true;
		}
	}

	// Token: 0x06001BFE RID: 7166 RVA: 0x0014CD94 File Offset: 0x0014AF94
	private void CheckAvailableUniforms()
	{
		this.AvailableUniforms = this.StudentManager.OriginalUniforms;
		Debug.Log(this.AvailableUniforms + " of the original uniforms are still clean.");
		Debug.Log("There are " + this.StudentManager.NewUniforms + " new uniforms in school.");
		if (this.StudentManager.NewUniforms > 0)
		{
			for (int i = 0; i < this.StudentManager.Uniforms.Length; i++)
			{
				Transform transform = this.StudentManager.Uniforms[i];
				if (transform != null && this.StudentManager.LockerRoomArea.bounds.Contains(transform.position))
				{
					Debug.Log("Cool, there's a uniform in the locker room.");
					this.AvailableUniforms++;
				}
			}
		}
	}

	// Token: 0x040033F4 RID: 13300
	public GameObject[] BloodyClubUniform;

	// Token: 0x040033F5 RID: 13301
	public GameObject[] BloodyUniform;

	// Token: 0x040033F6 RID: 13302
	public GameObject[] Schoolwear;

	// Token: 0x040033F7 RID: 13303
	public bool[] Removed;

	// Token: 0x040033F8 RID: 13304
	public bool[] Bloody;

	// Token: 0x040033F9 RID: 13305
	public GameObject CleanUniform;

	// Token: 0x040033FA RID: 13306
	public GameObject SteamCloud;

	// Token: 0x040033FB RID: 13307
	public StudentManagerScript StudentManager;

	// Token: 0x040033FC RID: 13308
	public RivalPhoneScript RivalPhone;

	// Token: 0x040033FD RID: 13309
	public StudentScript Student;

	// Token: 0x040033FE RID: 13310
	public YandereScript Yandere;

	// Token: 0x040033FF RID: 13311
	public PromptScript Prompt;

	// Token: 0x04003400 RID: 13312
	public Transform Hinge;

	// Token: 0x04003401 RID: 13313
	public bool RemovingClubAttire;

	// Token: 0x04003402 RID: 13314
	public bool DropCleanUniform;

	// Token: 0x04003403 RID: 13315
	public bool SteamCountdown;

	// Token: 0x04003404 RID: 13316
	public bool YandereLocker;

	// Token: 0x04003405 RID: 13317
	public bool Swapping;

	// Token: 0x04003406 RID: 13318
	public bool Open;

	// Token: 0x04003407 RID: 13319
	public float Rotation;

	// Token: 0x04003408 RID: 13320
	public float Timer;

	// Token: 0x04003409 RID: 13321
	public int AvailableUniforms = 2;

	// Token: 0x0400340A RID: 13322
	public int Phase = 1;
}
