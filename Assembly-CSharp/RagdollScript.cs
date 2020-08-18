using System;
using UnityEngine;

// Token: 0x02000384 RID: 900
public class RagdollScript : MonoBehaviour
{
	// Token: 0x06001973 RID: 6515 RVA: 0x000F65D0 File Offset: 0x000F47D0
	private void Start()
	{
		this.ElectrocutionAnimation = false;
		this.MurderSuicideAnimation = false;
		this.BurningAnimation = false;
		this.ChokingAnimation = false;
		this.Disturbing = false;
		Physics.IgnoreLayerCollision(11, 13, true);
		this.Zs.SetActive(this.Tranquil);
		if (!this.Tranquil && !this.Poisoned && !this.Drowned && !this.Electrocuted && !this.Burning && !this.NeckSnapped)
		{
			this.Student.StudentManager.TutorialWindow.ShowPoolMessage = true;
			this.BloodPoolSpawner.gameObject.SetActive(true);
			if (this.Pushed)
			{
				this.BloodPoolSpawner.Timer = 5f;
			}
		}
		for (int i = 0; i < this.AllRigidbodies.Length; i++)
		{
			this.AllRigidbodies[i].isKinematic = false;
			this.AllColliders[i].enabled = true;
			if (this.Yandere.StudentManager.NoGravity)
			{
				this.AllRigidbodies[i].useGravity = false;
			}
		}
		this.Prompt.enabled = true;
		if (ClassGlobals.PhysicalGrade + ClassGlobals.PhysicalBonus > 0 && !this.Tranquil)
		{
			this.Prompt.HideButton[3] = false;
		}
		if (this.Student.Yandere.BlackHole)
		{
			this.DisableRigidbodies();
		}
	}

	// Token: 0x06001974 RID: 6516 RVA: 0x000F6724 File Offset: 0x000F4924
	private void Update()
	{
		if (!this.Dragged && !this.Carried && !this.Settled && !this.Yandere.PK && !this.Yandere.StudentManager.NoGravity)
		{
			this.SettleTimer += Time.deltaTime;
			if (this.SettleTimer > 5f)
			{
				this.Settled = true;
				for (int i = 0; i < this.AllRigidbodies.Length; i++)
				{
					this.AllRigidbodies[i].isKinematic = true;
					this.AllColliders[i].enabled = false;
				}
			}
		}
		if (this.DetectionMarker != null)
		{
			if (this.DetectionMarker.Tex.color.a > 0.1f)
			{
				this.DetectionMarker.Tex.color = new Color(this.DetectionMarker.Tex.color.r, this.DetectionMarker.Tex.color.g, this.DetectionMarker.Tex.color.b, Mathf.MoveTowards(this.DetectionMarker.Tex.color.a, 0f, Time.deltaTime * 10f));
			}
			else
			{
				this.DetectionMarker.Tex.color = new Color(this.DetectionMarker.Tex.color.r, this.DetectionMarker.Tex.color.g, this.DetectionMarker.Tex.color.b, 0f);
				this.DetectionMarker = null;
			}
		}
		if (!this.Dumped)
		{
			if (this.StopAnimation && this.Student.CharacterAnimation.isPlaying)
			{
				this.Student.CharacterAnimation.Stop();
			}
			if (this.BloodPoolSpawner != null && this.BloodPoolSpawner.gameObject.activeInHierarchy && !this.Cauterized)
			{
				if (this.Yandere.PickUp != null)
				{
					if (this.Yandere.PickUp.Blowtorch)
					{
						if (!this.Cauterizable)
						{
							this.Prompt.Label[0].text = "     Cauterize";
							this.Cauterizable = true;
						}
					}
					else if (this.Cauterizable)
					{
						this.Prompt.Label[0].text = "     Dismember";
						this.Cauterizable = false;
					}
				}
				else if (this.Cauterizable)
				{
					this.Prompt.Label[0].text = "     Dismember";
					this.Cauterizable = false;
				}
			}
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				this.Prompt.Circle[0].fillAmount = 1f;
				if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
				{
					if (this.Cauterizable)
					{
						this.Prompt.Label[0].text = "     Dismember";
						this.BloodPoolSpawner.enabled = false;
						this.Cauterizable = false;
						this.Cauterized = true;
						this.Yandere.CharacterAnimation.CrossFade("f02_cauterize_00");
						this.Yandere.Cauterizing = true;
						this.Yandere.CanMove = false;
						this.Yandere.PickUp.GetComponent<BlowtorchScript>().enabled = true;
						this.Yandere.PickUp.GetComponent<AudioSource>().Play();
					}
					else if (this.Yandere.StudentManager.OriginalUniforms + this.Yandere.StudentManager.NewUniforms > 1)
					{
						this.Yandere.CharacterAnimation.CrossFade("f02_dismember_00");
						this.Yandere.transform.LookAt(base.transform);
						this.Yandere.RPGCamera.transform.position = this.Yandere.DismemberSpot.position;
						this.Yandere.RPGCamera.transform.eulerAngles = this.Yandere.DismemberSpot.eulerAngles;
						this.Yandere.EquippedWeapon.Dismember();
						this.Yandere.RPGCamera.enabled = false;
						this.Yandere.TargetStudent = this.Student;
						this.Yandere.Ragdoll = base.gameObject;
						this.Yandere.Dismembering = true;
						this.Yandere.CanMove = false;
					}
					else if (!this.Yandere.ClothingWarning)
					{
						this.Yandere.NotificationManager.DisplayNotification(NotificationType.Clothing);
						this.Yandere.StudentManager.TutorialWindow.ShowClothingMessage = true;
						this.Yandere.ClothingWarning = true;
					}
				}
			}
			if (this.Prompt.Circle[1].fillAmount == 0f)
			{
				this.Prompt.Circle[1].fillAmount = 1f;
				if (!this.Student.FireEmitters[1].isPlaying)
				{
					if (!this.Dragged)
					{
						this.Yandere.EmptyHands();
						this.Prompt.AcceptingInput[1] = false;
						this.Prompt.Label[1].text = "     Drop";
						this.PickNearestLimb();
						this.Yandere.RagdollDragger.connectedBody = this.Rigidbodies[this.LimbID];
						this.Yandere.RagdollDragger.connectedAnchor = this.LimbAnchor[this.LimbID];
						this.Yandere.Dragging = true;
						this.Yandere.Running = false;
						this.Yandere.DragState = 0;
						this.Yandere.Ragdoll = base.gameObject;
						this.Dragged = true;
						this.Yandere.StudentManager.UpdateStudents(0);
						if (this.MurderSuicide)
						{
							this.Police.MurderSuicideScene = false;
							this.MurderSuicide = false;
						}
						if (this.Suicide)
						{
							this.Police.Suicide = false;
							this.Suicide = false;
						}
						for (int j = 0; j < this.Student.Ragdoll.AllRigidbodies.Length; j++)
						{
							this.Student.Ragdoll.AllRigidbodies[j].drag = 2f;
						}
						for (int k = 0; k < this.AllRigidbodies.Length; k++)
						{
							this.AllRigidbodies[k].isKinematic = false;
							this.AllColliders[k].enabled = true;
							if (this.Yandere.StudentManager.NoGravity)
							{
								this.AllRigidbodies[k].useGravity = false;
							}
						}
					}
					else
					{
						this.StopDragging();
					}
				}
			}
			if (this.Prompt.Circle[3].fillAmount == 0f)
			{
				this.Prompt.Circle[3].fillAmount = 1f;
				if (!this.Student.FireEmitters[1].isPlaying)
				{
					this.Yandere.EmptyHands();
					this.Prompt.Label[1].text = "     Drop";
					this.Prompt.HideButton[1] = true;
					this.Prompt.HideButton[3] = true;
					this.Prompt.enabled = false;
					this.Prompt.Hide();
					for (int l = 0; l < this.AllRigidbodies.Length; l++)
					{
						this.AllRigidbodies[l].isKinematic = true;
						this.AllColliders[l].enabled = false;
					}
					if (this.Male)
					{
						Rigidbody rigidbody = this.AllRigidbodies[0];
						rigidbody.transform.parent.transform.localPosition = new Vector3(rigidbody.transform.parent.transform.localPosition.x, 0.2f, rigidbody.transform.parent.transform.localPosition.z);
					}
					this.Yandere.CharacterAnimation.Play("f02_carryLiftA_00");
					this.Student.CharacterAnimation.Play(this.LiftAnim);
					this.BloodSpawnerCollider.enabled = false;
					this.PelvisRoot.localEulerAngles = new Vector3(this.PelvisRoot.localEulerAngles.x, 0f, this.PelvisRoot.localEulerAngles.z);
					this.Prompt.MyCollider.enabled = false;
					this.PelvisRoot.localPosition = new Vector3(this.PelvisRoot.localPosition.x, this.PelvisRoot.localPosition.y, 0f);
					this.Yandere.Ragdoll = base.gameObject;
					this.Yandere.CurrentRagdoll = this;
					this.Yandere.CanMove = false;
					this.Yandere.Lifting = true;
					this.StopAnimation = false;
					this.Carried = true;
					this.Falling = false;
					this.FallTimer = 0f;
				}
			}
			if (this.Yandere.Running && this.Yandere.CanMove && this.Dragged)
			{
				this.StopDragging();
			}
			if (Vector3.Distance(this.Yandere.transform.position, this.Prompt.transform.position) < 2f)
			{
				if (!this.Suicide && !this.AddingToCount)
				{
					this.Yandere.NearestCorpseID = this.StudentID;
					this.Yandere.NearBodies++;
					this.AddingToCount = true;
				}
			}
			else if (this.AddingToCount)
			{
				this.Yandere.NearBodies--;
				this.AddingToCount = false;
			}
			if (!this.Prompt.AcceptingInput[1] && Input.GetButtonUp("B"))
			{
				this.Prompt.AcceptingInput[1] = true;
			}
			bool flag = false;
			if (this.Yandere.Armed && this.Yandere.EquippedWeapon.WeaponID == 7 && !this.Student.Nemesis)
			{
				flag = true;
			}
			if (!this.Cauterized && this.Yandere.PickUp != null && this.Yandere.PickUp.Blowtorch && this.BloodPoolSpawner.gameObject.activeInHierarchy)
			{
				flag = true;
			}
			this.Prompt.HideButton[0] = (this.Dragged || this.Carried || this.Tranquil || !flag);
		}
		else if (this.DumpType == RagdollDumpType.Incinerator)
		{
			if (this.DumpTimer == 0f && this.Yandere.Carrying)
			{
				this.Student.CharacterAnimation[this.DumpedAnim].time = 2.5f;
			}
			this.Student.CharacterAnimation.CrossFade(this.DumpedAnim);
			this.DumpTimer += Time.deltaTime;
			if (this.Student.CharacterAnimation[this.DumpedAnim].time >= this.Student.CharacterAnimation[this.DumpedAnim].length)
			{
				this.Incinerator.Corpses++;
				this.Incinerator.CorpseList[this.Incinerator.Corpses] = this.StudentID;
				this.Remove();
				base.enabled = false;
			}
		}
		else if (this.DumpType == RagdollDumpType.TranqCase)
		{
			if (this.DumpTimer == 0f && this.Yandere.Carrying)
			{
				this.Student.CharacterAnimation[this.DumpedAnim].time = 2.5f;
			}
			this.Student.CharacterAnimation.CrossFade(this.DumpedAnim);
			this.DumpTimer += Time.deltaTime;
			if (this.Student.CharacterAnimation[this.DumpedAnim].time >= this.Student.CharacterAnimation[this.DumpedAnim].length)
			{
				this.TranqCase.Open = false;
				if (this.AddingToCount)
				{
					this.Yandere.NearBodies--;
				}
				base.enabled = false;
			}
		}
		else if (this.DumpType == RagdollDumpType.WoodChipper)
		{
			if (this.DumpTimer == 0f && this.Yandere.Carrying)
			{
				this.Student.CharacterAnimation[this.DumpedAnim].time = 2.5f;
			}
			this.Student.CharacterAnimation.CrossFade(this.DumpedAnim);
			this.DumpTimer += Time.deltaTime;
			if (this.Student.CharacterAnimation[this.DumpedAnim].time >= this.Student.CharacterAnimation[this.DumpedAnim].length)
			{
				this.WoodChipper.VictimID = this.StudentID;
				this.Remove();
				base.enabled = false;
			}
		}
		if (this.Hidden && this.HideCollider == null)
		{
			this.Police.HiddenCorpses--;
			this.Hidden = false;
		}
		if (this.Falling)
		{
			this.FallTimer += Time.deltaTime;
			if (this.FallTimer > 2f)
			{
				this.BloodSpawnerCollider.enabled = true;
				this.FallTimer = 0f;
				this.Falling = false;
			}
		}
		if (this.Burning)
		{
			for (int m = 0; m < 3; m++)
			{
				Material material = this.MyRenderer.materials[m];
				material.color = Vector4.MoveTowards(material.color, new Vector4(0.1f, 0.1f, 0.1f, 1f), Time.deltaTime * 0.1f);
			}
			this.Student.Cosmetic.HairRenderer.material.color = Vector4.MoveTowards(this.Student.Cosmetic.HairRenderer.material.color, new Vector4(0.1f, 0.1f, 0.1f, 1f), Time.deltaTime * 0.1f);
			if (this.MyRenderer.materials[0].color == new Color(0.1f, 0.1f, 0.1f, 1f))
			{
				this.Burning = false;
				this.Burned = true;
			}
		}
		if (this.Burned)
		{
			this.Sacrifice = (Vector3.Distance(this.Prompt.transform.position, this.Yandere.StudentManager.SacrificeSpot.position) < 1.5f);
		}
	}

	// Token: 0x06001975 RID: 6517 RVA: 0x000F75E4 File Offset: 0x000F57E4
	private void LateUpdate()
	{
		if (!this.Male)
		{
			if (this.LeftEye != null)
			{
				this.LeftEye.localPosition = new Vector3(this.LeftEye.localPosition.x, this.LeftEye.localPosition.y, this.LeftEyeOrigin.z - this.EyeShrink * 0.01f);
				this.RightEye.localPosition = new Vector3(this.RightEye.localPosition.x, this.RightEye.localPosition.y, this.RightEyeOrigin.z + this.EyeShrink * 0.01f);
				this.LeftEye.localScale = new Vector3(1f - this.EyeShrink * 0.5f, 1f - this.EyeShrink * 0.5f, this.LeftEye.localScale.z);
				this.RightEye.localScale = new Vector3(1f - this.EyeShrink * 0.5f, 1f - this.EyeShrink * 0.5f, this.RightEye.localScale.z);
			}
			if (this.StudentID == 81)
			{
				for (int i = 0; i < 4; i++)
				{
					Transform transform = this.Student.Skirt[i];
					transform.transform.localScale = new Vector3(transform.transform.localScale.x, 0.6666667f, transform.transform.localScale.z);
				}
			}
		}
		if (this.Decapitated)
		{
			this.Head.localScale = Vector3.zero;
		}
		if (this.Yandere.Ragdoll == base.gameObject)
		{
			if (this.Yandere.DumpTimer < 1f)
			{
				if (this.Yandere.Lifting)
				{
					base.transform.position = this.Yandere.transform.position;
					base.transform.eulerAngles = this.Yandere.transform.eulerAngles;
				}
				else if (this.Carried)
				{
					base.transform.position = this.Yandere.transform.position;
					base.transform.eulerAngles = this.Yandere.transform.eulerAngles;
					float axis = Input.GetAxis("Vertical");
					float axis2 = Input.GetAxis("Horizontal");
					if (axis != 0f || axis2 != 0f)
					{
						this.Student.CharacterAnimation.CrossFade(this.Yandere.Running ? this.RunAnim : this.WalkAnim);
					}
					else
					{
						this.Student.CharacterAnimation.CrossFade(this.IdleAnim);
					}
					this.Student.CharacterAnimation[this.IdleAnim].time = this.Yandere.CharacterAnimation["f02_carryIdleA_00"].time;
					this.Student.CharacterAnimation[this.WalkAnim].time = this.Yandere.CharacterAnimation["f02_carryWalkA_00"].time;
					this.Student.CharacterAnimation[this.RunAnim].time = this.Yandere.CharacterAnimation["f02_carryRunA_00"].time;
				}
			}
			if (this.Carried)
			{
				if (this.Male)
				{
					Rigidbody rigidbody = this.AllRigidbodies[0];
					rigidbody.transform.parent.transform.localPosition = new Vector3(rigidbody.transform.parent.transform.localPosition.x, 0.2f, rigidbody.transform.parent.transform.localPosition.z);
				}
				if (this.Yandere.Struggling || this.Yandere.DelinquentFighting || this.Yandere.Sprayed)
				{
					this.Fall();
				}
			}
		}
	}

	// Token: 0x06001976 RID: 6518 RVA: 0x000F79F4 File Offset: 0x000F5BF4
	public void StopDragging()
	{
		Rigidbody[] allRigidbodies = this.Student.Ragdoll.AllRigidbodies;
		for (int i = 0; i < allRigidbodies.Length; i++)
		{
			allRigidbodies[i].drag = 0f;
		}
		if (ClassGlobals.PhysicalGrade + ClassGlobals.PhysicalBonus > 0 && !this.Tranquil)
		{
			this.Prompt.HideButton[3] = false;
		}
		this.Prompt.AcceptingInput[1] = true;
		this.Prompt.Circle[1].fillAmount = 1f;
		this.Prompt.Label[1].text = "     Drag";
		this.Yandere.RagdollDragger.connectedBody = null;
		this.Yandere.RagdollPK.connectedBody = null;
		this.Yandere.Dragging = false;
		this.Yandere.Ragdoll = null;
		this.Yandere.StudentManager.UpdateStudents(0);
		this.SettleTimer = 0f;
		this.Settled = false;
		this.Dragged = false;
	}

	// Token: 0x06001977 RID: 6519 RVA: 0x000F7AF4 File Offset: 0x000F5CF4
	private void PickNearestLimb()
	{
		this.NearestLimb = this.Limb[0];
		this.LimbID = 0;
		for (int i = 1; i < 4; i++)
		{
			Transform transform = this.Limb[i];
			if (Vector3.Distance(transform.position, this.Yandere.transform.position) < Vector3.Distance(this.NearestLimb.position, this.Yandere.transform.position))
			{
				this.NearestLimb = transform;
				this.LimbID = i;
			}
		}
	}

	// Token: 0x06001978 RID: 6520 RVA: 0x000F7B78 File Offset: 0x000F5D78
	public void Dump()
	{
		if (this.DumpType == RagdollDumpType.Incinerator)
		{
			base.transform.eulerAngles = this.Yandere.transform.eulerAngles;
			base.transform.position = this.Yandere.transform.position;
			this.Incinerator = this.Yandere.Incinerator;
			this.BloodPoolSpawner.enabled = false;
		}
		else if (this.DumpType == RagdollDumpType.TranqCase)
		{
			this.TranqCase = this.Yandere.TranqCase;
		}
		else if (this.DumpType == RagdollDumpType.WoodChipper)
		{
			this.WoodChipper = this.Yandere.WoodChipper;
		}
		this.Prompt.Hide();
		this.Prompt.enabled = false;
		this.Dumped = true;
		Rigidbody[] allRigidbodies = this.AllRigidbodies;
		for (int i = 0; i < allRigidbodies.Length; i++)
		{
			allRigidbodies[i].isKinematic = true;
		}
	}

	// Token: 0x06001979 RID: 6521 RVA: 0x000F7C58 File Offset: 0x000F5E58
	public void Fall()
	{
		base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + 0.0001f, base.transform.position.z);
		this.Prompt.Label[1].text = "     Drag";
		this.Prompt.HideButton[1] = false;
		this.Prompt.enabled = true;
		if (ClassGlobals.PhysicalGrade + ClassGlobals.PhysicalBonus > 0 && !this.Tranquil)
		{
			this.Prompt.HideButton[3] = false;
		}
		if (this.Dragged)
		{
			this.Yandere.RagdollDragger.connectedBody = null;
			this.Yandere.RagdollPK.connectedBody = null;
			this.Yandere.Dragging = false;
			this.Dragged = false;
		}
		this.Yandere.Ragdoll = null;
		this.Prompt.MyCollider.enabled = true;
		this.BloodPoolSpawner.NearbyBlood = 0;
		this.StopAnimation = true;
		this.SettleTimer = 0f;
		this.Carried = false;
		this.Settled = false;
		this.Falling = true;
		for (int i = 0; i < this.AllRigidbodies.Length; i++)
		{
			this.AllRigidbodies[i].isKinematic = false;
			this.AllColliders[i].enabled = true;
		}
	}

	// Token: 0x0600197A RID: 6522 RVA: 0x000F7DBC File Offset: 0x000F5FBC
	public void QuickDismember()
	{
		for (int i = 0; i < this.BodyParts.Length; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BodyParts[i], this.SpawnPoints[i].position, Quaternion.identity);
			gameObject.transform.eulerAngles = this.SpawnPoints[i].eulerAngles;
			gameObject.GetComponent<PromptScript>().enabled = false;
			gameObject.GetComponent<PickUpScript>().enabled = false;
			gameObject.GetComponent<OutlineScript>().enabled = false;
		}
		if (this.BloodPoolSpawner.BloodParent == null)
		{
			this.BloodPoolSpawner.Start();
		}
		Debug.Log("BloodPoolSpawner.transform.position is: " + this.BloodPoolSpawner.transform.position);
		Debug.Log("Student.StudentManager.SEStairs.bounds is: " + this.Student.StudentManager.SEStairs.bounds);
		if (!this.Student.StudentManager.NEStairs.bounds.Contains(this.BloodPoolSpawner.transform.position) && !this.Student.StudentManager.NWStairs.bounds.Contains(this.BloodPoolSpawner.transform.position) && !this.Student.StudentManager.SEStairs.bounds.Contains(this.BloodPoolSpawner.transform.position) && !this.Student.StudentManager.SWStairs.bounds.Contains(this.BloodPoolSpawner.transform.position))
		{
			this.BloodPoolSpawner.SpawnBigPool();
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600197B RID: 6523 RVA: 0x000F7F78 File Offset: 0x000F6178
	public void Dismember()
	{
		if (!this.Dismembered)
		{
			this.Student.LiquidProjector.material.mainTexture = this.Student.BloodTexture;
			for (int i = 0; i < this.BodyParts.Length; i++)
			{
				if (this.Decapitated)
				{
					i++;
					this.Decapitated = false;
				}
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BodyParts[i], this.SpawnPoints[i].position, Quaternion.identity);
				gameObject.transform.parent = this.Yandere.LimbParent;
				gameObject.transform.eulerAngles = this.SpawnPoints[i].eulerAngles;
				BodyPartScript component = gameObject.GetComponent<BodyPartScript>();
				component.StudentID = this.StudentID;
				component.Sacrifice = this.Sacrifice;
				if (this.Yandere.StudentManager.NoGravity)
				{
					gameObject.GetComponent<Rigidbody>().useGravity = false;
				}
				if (i == 0)
				{
					if (!this.Student.OriginallyTeacher)
					{
						if (!this.Male)
						{
							this.Student.Cosmetic.FemaleHair[this.Student.Cosmetic.Hairstyle].transform.parent = gameObject.transform;
							if (this.Student.Cosmetic.FemaleAccessories[this.Student.Cosmetic.Accessory] != null && this.Student.Cosmetic.Accessory != 3 && this.Student.Cosmetic.Accessory != 6)
							{
								this.Student.Cosmetic.FemaleAccessories[this.Student.Cosmetic.Accessory].transform.parent = gameObject.transform;
							}
						}
						else
						{
							Transform transform = this.Student.Cosmetic.MaleHair[this.Student.Cosmetic.Hairstyle].transform;
							transform.parent = gameObject.transform;
							transform.localScale *= 1.0638298f;
							if (transform.transform.localPosition.y < -1f)
							{
								transform.transform.localPosition = new Vector3(transform.transform.localPosition.x, transform.transform.localPosition.y - 0.095f, transform.transform.localPosition.z);
							}
							if (this.Student.Cosmetic.MaleAccessories[this.Student.Cosmetic.Accessory] != null)
							{
								this.Student.Cosmetic.MaleAccessories[this.Student.Cosmetic.Accessory].transform.parent = gameObject.transform;
							}
						}
					}
					else
					{
						this.Student.Cosmetic.TeacherHair[this.Student.Cosmetic.Hairstyle].transform.parent = gameObject.transform;
						if (this.Student.Cosmetic.TeacherAccessories[this.Student.Cosmetic.Accessory] != null)
						{
							this.Student.Cosmetic.TeacherAccessories[this.Student.Cosmetic.Accessory].transform.parent = gameObject.transform;
						}
					}
					if (this.Student.Club != ClubType.Photography && this.Student.Club < ClubType.Gaming && this.Student.Cosmetic.ClubAccessories[(int)this.Student.Club] != null)
					{
						this.Student.Cosmetic.ClubAccessories[(int)this.Student.Club].transform.parent = gameObject.transform;
						if (this.Student.Club == ClubType.Occult)
						{
							if (!this.Male)
							{
								this.Student.Cosmetic.ClubAccessories[(int)this.Student.Club].transform.localPosition = new Vector3(0f, -1.5f, 0.01f);
								this.Student.Cosmetic.ClubAccessories[(int)this.Student.Club].transform.localEulerAngles = Vector3.zero;
							}
							else
							{
								this.Student.Cosmetic.ClubAccessories[(int)this.Student.Club].transform.localPosition = new Vector3(0f, -1.42f, 0.005f);
								this.Student.Cosmetic.ClubAccessories[(int)this.Student.Club].transform.localEulerAngles = Vector3.zero;
							}
						}
					}
					gameObject.GetComponent<Renderer>().materials[0].mainTexture = this.Student.Cosmetic.FaceTexture;
					if (i == 0)
					{
						gameObject.transform.position += new Vector3(0f, 1f, 0f);
					}
				}
				else if (i == 1)
				{
					if (this.Student.Club == ClubType.Photography && this.Student.Cosmetic.ClubAccessories[(int)this.Student.Club] != null)
					{
						this.Student.Cosmetic.ClubAccessories[(int)this.Student.Club].transform.parent = gameObject.transform;
					}
				}
				else if (i == 2 && !this.Student.Male && this.Student.Cosmetic.Accessory == 6)
				{
					this.Student.Cosmetic.FemaleAccessories[this.Student.Cosmetic.Accessory].transform.parent = gameObject.transform;
				}
			}
			if (this.BloodPoolSpawner.BloodParent == null)
			{
				this.BloodPoolSpawner.Start();
			}
			Debug.Log("BloodPoolSpawner.transform.position is: " + this.BloodPoolSpawner.transform.position);
			Debug.Log("Student.StudentManager.SEStairs.bounds is: " + this.Student.StudentManager.SEStairs.bounds);
			Debug.Log("Student.StudentManager.SEStairs.bounds.Contains(BloodPoolSpawner.transform.position) is: " + this.Student.StudentManager.SEStairs.bounds.Contains(this.BloodPoolSpawner.transform.position).ToString());
			if (!this.Student.StudentManager.NEStairs.bounds.Contains(this.BloodPoolSpawner.transform.position) && !this.Student.StudentManager.NWStairs.bounds.Contains(this.BloodPoolSpawner.transform.position) && !this.Student.StudentManager.SEStairs.bounds.Contains(this.BloodPoolSpawner.transform.position) && !this.Student.StudentManager.SWStairs.bounds.Contains(this.BloodPoolSpawner.transform.position))
			{
				this.BloodPoolSpawner.SpawnBigPool();
			}
			this.Police.PartsIcon.gameObject.SetActive(true);
			this.Police.BodyParts += 6;
			this.Yandere.NearBodies--;
			this.Police.Corpses--;
			base.gameObject.SetActive(false);
			this.Dismembered = true;
		}
	}

	// Token: 0x0600197C RID: 6524 RVA: 0x000F8728 File Offset: 0x000F6928
	public void Remove()
	{
		this.BloodPoolSpawner.enabled = false;
		if (this.AddingToCount)
		{
			this.Yandere.NearBodies--;
		}
		if (this.Poisoned)
		{
			this.Police.PoisonScene = false;
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600197D RID: 6525 RVA: 0x000F877C File Offset: 0x000F697C
	public void DisableRigidbodies()
	{
		this.BloodPoolSpawner.gameObject.SetActive(false);
		for (int i = 0; i < this.AllRigidbodies.Length; i++)
		{
			if (this.AllRigidbodies[i].gameObject.GetComponent<CharacterJoint>() != null)
			{
				UnityEngine.Object.Destroy(this.AllRigidbodies[i].gameObject.GetComponent<CharacterJoint>());
			}
			UnityEngine.Object.Destroy(this.AllRigidbodies[i]);
			this.AllColliders[i].enabled = false;
		}
		this.Prompt.Hide();
		this.Prompt.enabled = false;
		base.enabled = false;
	}

	// Token: 0x04002702 RID: 9986
	public BloodPoolSpawnerScript BloodPoolSpawner;

	// Token: 0x04002703 RID: 9987
	public DetectionMarkerScript DetectionMarker;

	// Token: 0x04002704 RID: 9988
	public IncineratorScript Incinerator;

	// Token: 0x04002705 RID: 9989
	public WoodChipperScript WoodChipper;

	// Token: 0x04002706 RID: 9990
	public TranqCaseScript TranqCase;

	// Token: 0x04002707 RID: 9991
	public StudentScript Student;

	// Token: 0x04002708 RID: 9992
	public YandereScript Yandere;

	// Token: 0x04002709 RID: 9993
	public PoliceScript Police;

	// Token: 0x0400270A RID: 9994
	public PromptScript Prompt;

	// Token: 0x0400270B RID: 9995
	public SkinnedMeshRenderer MyRenderer;

	// Token: 0x0400270C RID: 9996
	public Collider BloodSpawnerCollider;

	// Token: 0x0400270D RID: 9997
	public Animation CharacterAnimation;

	// Token: 0x0400270E RID: 9998
	public Collider HideCollider;

	// Token: 0x0400270F RID: 9999
	public Rigidbody[] AllRigidbodies;

	// Token: 0x04002710 RID: 10000
	public Collider[] AllColliders;

	// Token: 0x04002711 RID: 10001
	public Rigidbody[] Rigidbodies;

	// Token: 0x04002712 RID: 10002
	public Transform[] SpawnPoints;

	// Token: 0x04002713 RID: 10003
	public GameObject[] BodyParts;

	// Token: 0x04002714 RID: 10004
	public Transform NearestLimb;

	// Token: 0x04002715 RID: 10005
	public Transform RightBreast;

	// Token: 0x04002716 RID: 10006
	public Transform LeftBreast;

	// Token: 0x04002717 RID: 10007
	public Transform PelvisRoot;

	// Token: 0x04002718 RID: 10008
	public Transform Ponytail;

	// Token: 0x04002719 RID: 10009
	public Transform RightEye;

	// Token: 0x0400271A RID: 10010
	public Transform LeftEye;

	// Token: 0x0400271B RID: 10011
	public Transform HairR;

	// Token: 0x0400271C RID: 10012
	public Transform HairL;

	// Token: 0x0400271D RID: 10013
	public Transform[] Limb;

	// Token: 0x0400271E RID: 10014
	public Transform Head;

	// Token: 0x0400271F RID: 10015
	public Vector3 RightEyeOrigin;

	// Token: 0x04002720 RID: 10016
	public Vector3 LeftEyeOrigin;

	// Token: 0x04002721 RID: 10017
	public Vector3[] LimbAnchor;

	// Token: 0x04002722 RID: 10018
	public GameObject Character;

	// Token: 0x04002723 RID: 10019
	public GameObject Zs;

	// Token: 0x04002724 RID: 10020
	public bool ElectrocutionAnimation;

	// Token: 0x04002725 RID: 10021
	public bool MurderSuicideAnimation;

	// Token: 0x04002726 RID: 10022
	public bool BurningAnimation;

	// Token: 0x04002727 RID: 10023
	public bool ChokingAnimation;

	// Token: 0x04002728 RID: 10024
	public bool AddingToCount;

	// Token: 0x04002729 RID: 10025
	public bool MurderSuicide;

	// Token: 0x0400272A RID: 10026
	public bool Cauterizable;

	// Token: 0x0400272B RID: 10027
	public bool Electrocuted;

	// Token: 0x0400272C RID: 10028
	public bool StopAnimation = true;

	// Token: 0x0400272D RID: 10029
	public bool Decapitated;

	// Token: 0x0400272E RID: 10030
	public bool Dismembered;

	// Token: 0x0400272F RID: 10031
	public bool NeckSnapped;

	// Token: 0x04002730 RID: 10032
	public bool Cauterized;

	// Token: 0x04002731 RID: 10033
	public bool Disturbing;

	// Token: 0x04002732 RID: 10034
	public bool Sacrifice;

	// Token: 0x04002733 RID: 10035
	public bool Disposed;

	// Token: 0x04002734 RID: 10036
	public bool Poisoned;

	// Token: 0x04002735 RID: 10037
	public bool Tranquil;

	// Token: 0x04002736 RID: 10038
	public bool Burning;

	// Token: 0x04002737 RID: 10039
	public bool Carried;

	// Token: 0x04002738 RID: 10040
	public bool Choking;

	// Token: 0x04002739 RID: 10041
	public bool Dragged;

	// Token: 0x0400273A RID: 10042
	public bool Drowned;

	// Token: 0x0400273B RID: 10043
	public bool Falling;

	// Token: 0x0400273C RID: 10044
	public bool Nemesis;

	// Token: 0x0400273D RID: 10045
	public bool Settled;

	// Token: 0x0400273E RID: 10046
	public bool Suicide;

	// Token: 0x0400273F RID: 10047
	public bool Burned;

	// Token: 0x04002740 RID: 10048
	public bool Dumped;

	// Token: 0x04002741 RID: 10049
	public bool Hidden;

	// Token: 0x04002742 RID: 10050
	public bool Pushed;

	// Token: 0x04002743 RID: 10051
	public bool Male;

	// Token: 0x04002744 RID: 10052
	public float AnimStartTime;

	// Token: 0x04002745 RID: 10053
	public float SettleTimer;

	// Token: 0x04002746 RID: 10054
	public float BreastSize;

	// Token: 0x04002747 RID: 10055
	public float DumpTimer;

	// Token: 0x04002748 RID: 10056
	public float EyeShrink;

	// Token: 0x04002749 RID: 10057
	public float FallTimer;

	// Token: 0x0400274A RID: 10058
	public int StudentID;

	// Token: 0x0400274B RID: 10059
	public RagdollDumpType DumpType;

	// Token: 0x0400274C RID: 10060
	public int LimbID;

	// Token: 0x0400274D RID: 10061
	public int Frame;

	// Token: 0x0400274E RID: 10062
	public string DumpedAnim = string.Empty;

	// Token: 0x0400274F RID: 10063
	public string LiftAnim = string.Empty;

	// Token: 0x04002750 RID: 10064
	public string IdleAnim = string.Empty;

	// Token: 0x04002751 RID: 10065
	public string WalkAnim = string.Empty;

	// Token: 0x04002752 RID: 10066
	public string RunAnim = string.Empty;
}
