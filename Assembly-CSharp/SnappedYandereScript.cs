using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020003E5 RID: 997
public class SnappedYandereScript : MonoBehaviour
{
	// Token: 0x06001AAE RID: 6830 RVA: 0x0010A374 File Offset: 0x00108574
	private void Start()
	{
		this.MyAnim[this.AttackAnims[1]].speed = 1.5f;
		this.MyAnim[this.AttackAnims[2]].speed = 1.5f;
		this.MyAnim[this.AttackAnims[3]].speed = 1.5f;
		this.MyAnim[this.AttackAnims[4]].speed = 1.5f;
		this.MyAnim[this.AttackAnims[5]].speed = 1.5f;
	}

	// Token: 0x06001AAF RID: 6831 RVA: 0x0010A414 File Offset: 0x00108614
	private void Update()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		if (Input.GetKeyDown("=") && Time.timeScale < 10f)
		{
			Time.timeScale += 1f;
		}
		if (Input.GetKeyDown("-") && Time.timeScale > 1f)
		{
			Time.timeScale -= 1f;
		}
		if (this.Glitch1.enabled)
		{
			if (this.Attacking)
			{
				this.GlitchTimer += Time.deltaTime * this.MyAnim[this.AttackAnims[this.AttackID]].speed;
			}
			else
			{
				this.GlitchTimer += Time.deltaTime;
			}
			if (this.GlitchTimer > this.GlitchTimeLimit)
			{
				this.SetGlitches(false);
				if (this.MyAudio.clip != this.EndSNAP)
				{
					this.MyAudio.Stop();
				}
				if (this.Attacking)
				{
					this.SnapAttackPivot.position = this.TargetStudent.Student.Head.position;
					this.SnapAttackPivot.localEulerAngles = new Vector3(0f, 0f, 0f);
					this.MainCamera.transform.parent = this.SnapAttackPivot;
					this.MainCamera.transform.localPosition = new Vector3(0f, 0f, -1f);
					this.MainCamera.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
					this.SnapAttackPivot.localEulerAngles = new Vector3(UnityEngine.Random.Range(-45f, 45f), UnityEngine.Random.Range(0f, 360f), 0f);
					while (this.MainCamera.transform.position.y < base.transform.position.y + 0.1f)
					{
						this.SnapAttackPivot.localEulerAngles = new Vector3(UnityEngine.Random.Range(-45f, 45f), UnityEngine.Random.Range(0f, 360f), 0f);
					}
					this.MyAnim[this.AttackAnims[this.AttackID]].time = 0f;
					this.MyAnim.Play(this.AttackAnims[this.AttackID]);
					this.MyAnim[this.AttackAnims[this.AttackID]].time = 0f;
					this.MyAnim[this.AttackAnims[this.AttackID]].speed += 0.1f;
					this.TargetStudent.MyAnim[this.TargetStudent.AttackAnims[this.AttackID]].time = 0f;
					this.TargetStudent.MyAnim.Play(this.TargetStudent.AttackAnims[this.AttackID]);
					this.TargetStudent.MyAnim[this.TargetStudent.AttackAnims[this.AttackID]].time = 0f;
					this.TargetStudent.MyAnim[this.TargetStudent.AttackAnims[this.AttackID]].speed = this.MyAnim[this.AttackAnims[this.AttackID]].speed;
					if (this.TargetStudent.Student.Male)
					{
						this.MyAudio.clip = this.MaleDeathScreams[UnityEngine.Random.Range(0, this.MaleDeathScreams.Length)];
						this.MyAudio.pitch = 1f;
						this.MyAudio.Play();
					}
					else
					{
						this.MyAudio.clip = this.FemaleDeathScreams[UnityEngine.Random.Range(0, this.FemaleDeathScreams.Length)];
						this.MyAudio.pitch = 1f;
						this.MyAudio.Play();
					}
					this.AttackAudio.clip = this.AttackSFX[this.AttackID];
					this.AttackAudio.pitch = this.MyAnim[this.AttackAnims[this.AttackID]].speed;
					this.AttackAudio.Play();
				}
			}
		}
		if (!this.Armed)
		{
			foreach (WeaponScript weaponScript in this.Weapons)
			{
				if (weaponScript != null && Vector3.Distance(base.transform.position, weaponScript.transform.position) < 1.5f)
				{
					weaponScript.Prompt.Circle[3].fillAmount = 0f;
					this.SNAPLabel.text = "Kill him.";
					this.StaticNoise.volume = 0f;
					this.Static.Fade = 0f;
					this.HurryTimer = 0f;
					this.Knife = weaponScript;
					this.Armed = true;
				}
			}
		}
		else
		{
			this.Knife.gameObject.SetActive(true);
		}
		if (this.CanMove)
		{
			this.SNAPLabel.alpha = Mathf.MoveTowards(this.SNAPLabel.alpha, 1f, Time.deltaTime * 0.2f);
			this.HurryTimer += Time.deltaTime;
			if (this.HurryTimer > 40f || base.transform.position.y < -0.1f || this.StudentManager.MaleLockerRoomArea.bounds.Contains(base.transform.position))
			{
				this.Teleport();
				this.HurryTimer = 0f;
				this.Static.Fade = 0f;
				this.StaticNoise.volume = 0f;
			}
			else if (this.HurryTimer > 30f)
			{
				this.StaticNoise.volume += Time.deltaTime * 0.1f;
				this.Static.Fade += Time.deltaTime * 0.1f;
			}
			this.UpdateMovement();
		}
		else if (this.Attacking)
		{
			this.SNAPLabel.alpha = 0f;
			if (this.MyAnim[this.AttackAnims[this.AttackID]].speed == 0f)
			{
				this.MyAnim[this.AttackAnims[this.AttackID]].speed = 1f;
			}
			if (this.MyAnim[this.AttackAnims[this.AttackID]].time >= this.MyAnim[this.AttackAnims[this.AttackID]].length)
			{
				if (this.Attacks < 5)
				{
					this.ChooseAttack();
				}
				else
				{
					this.MainCamera.transform.parent = base.transform;
					this.MainCamera.transform.localPosition = new Vector3(0.25f, 1.546664f, -0.5473595f);
					this.MainCamera.transform.localEulerAngles = new Vector3(15f, 0f, 0f);
					this.SetGlitches(true);
					this.GlitchTimeLimit = 0.5f;
					this.TargetStudent.Student.BecomeRagdoll();
					this.AttacksUsed[1] = false;
					this.AttacksUsed[2] = false;
					this.AttacksUsed[3] = false;
					this.AttacksUsed[4] = false;
					this.AttacksUsed[5] = false;
					this.Attacking = false;
					this.CanMove = true;
					this.Attacks = 0;
				}
			}
			else if (!this.Glitch1.enabled && this.BloodSpawned < 2)
			{
				if (this.AttackID == 1)
				{
					if (this.BloodSpawned == 0)
					{
						if (this.MyAnim[this.AttackAnims[this.AttackID]].time > 0.25f)
						{
							UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, this.RightHand.position, Quaternion.identity);
							this.MyAudio.Stop();
							this.BloodSpawned++;
						}
					}
					else if (this.MyAnim[this.AttackAnims[this.AttackID]].time > 1f)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, this.LeftHand.position, Quaternion.identity);
						this.BloodSpawned++;
					}
				}
				else if (this.AttackID == 2)
				{
					if (this.MyAnim[this.AttackAnims[this.AttackID]].time > 1f)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, this.RightHand.position, Quaternion.identity);
						this.BloodSpawned += 2;
						this.MyAudio.Stop();
					}
				}
				else if (this.AttackID == 3)
				{
					if (this.MyAnim[this.AttackAnims[this.AttackID]].time > 0.5f)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, this.RightHand.position, Quaternion.identity);
						this.BloodSpawned += 2;
						this.MyAudio.Stop();
					}
				}
				else if (this.AttackID == 4)
				{
					if (this.MyAnim[this.AttackAnims[this.AttackID]].time > 0.5f)
					{
						this.MyAudio.Stop();
					}
				}
				else if (this.AttackID == 5)
				{
					if (this.BloodSpawned == 0)
					{
						if (this.MyAnim[this.AttackAnims[this.AttackID]].time > 0.25f)
						{
							UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, this.RightFoot.position, Quaternion.identity);
							this.MyAudio.Stop();
							this.BloodSpawned++;
						}
					}
					else if (this.MyAnim[this.AttackAnims[this.AttackID]].time > 0.9f)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, this.RightFoot.position, Quaternion.identity);
						this.BloodSpawned++;
					}
				}
			}
		}
		else if (this.KillingSenpai)
		{
			this.CompressionFX.Parasite = Mathf.MoveTowards(this.CompressionFX.Parasite, 0f, Time.deltaTime);
			this.Distorted.Distortion = Mathf.MoveTowards(this.Distorted.Distortion, 0f, Time.deltaTime);
			this.StaticNoise.volume -= Time.deltaTime * 0.5f;
			this.Static.Fade = Mathf.MoveTowards(this.Static.Fade, 0f, Time.deltaTime * 0.5f);
			this.Jukebox.volume -= Time.deltaTime * 0.5f;
			this.SnapStatic.volume -= Time.deltaTime * 0.5f * 0.2f;
			this.SNAPLabel.alpha = Mathf.MoveTowards(this.SNAPLabel.alpha, 0f, Time.deltaTime * 0.5f);
			this.SnapVoice.volume -= Time.deltaTime;
			Quaternion b = Quaternion.LookRotation(this.TargetStudent.transform.position - base.transform.position);
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, b, Time.deltaTime);
			base.transform.position = Vector3.MoveTowards(base.transform.position, this.TargetStudent.transform.position + this.TargetStudent.transform.forward * 1f, Time.deltaTime);
			this.Speed += Time.deltaTime;
			if (this.AttackPhase < 3)
			{
				this.MainCamera.transform.position = Vector3.Lerp(this.MainCamera.transform.position, this.FinalSnapPOV.position, Time.deltaTime * this.Speed * 0.33333f);
				this.MainCamera.transform.rotation = Quaternion.Slerp(this.MainCamera.transform.rotation, this.FinalSnapPOV.rotation, Time.deltaTime * this.Speed * 0.33333f);
			}
			else
			{
				this.MainCamera.transform.position = Vector3.Lerp(this.MainCamera.transform.position, this.SuicidePOV.position, Time.deltaTime * this.Speed * 0.1f);
				this.MainCamera.transform.rotation = Quaternion.Slerp(this.MainCamera.transform.rotation, this.SuicidePOV.rotation, Time.deltaTime * this.Speed * 0.1f);
				if (this.Whisper)
				{
					this.Rumble.volume = Mathf.MoveTowards(this.Rumble.volume, 0.5f, Time.deltaTime * 0.05f);
					this.WhisperTimer += Time.deltaTime;
					if (this.WhisperTimer > 0.5f)
					{
						this.WhisperTimer = 0f;
						int num = UnityEngine.Random.Range(1, this.Whispers.Length);
						AudioSource.PlayClipAtPoint(this.Whispers[num], this.MainCamera.transform.position + new Vector3(11f - 10f * this.Rumble.volume * 2f, 11f - 10f * this.Rumble.volume * 2f, 11f - 10f * this.Rumble.volume * 2f));
						this.NewDoIt = UnityEngine.Object.Instantiate<GameObject>(this.DoIt, this.SNAPLabel.parent.transform.position, Quaternion.identity);
						this.NewDoIt.transform.parent = this.SNAPLabel.parent.transform;
						this.NewDoIt.transform.localScale = new Vector3(1f, 1f, 1f);
						this.NewDoIt.transform.localPosition = new Vector3(UnityEngine.Random.Range(-700f, 700f), UnityEngine.Random.Range(-450f, 450f), 0f);
						this.NewDoIt.transform.localEulerAngles = new Vector3(UnityEngine.Random.Range(-15f, 15f), UnityEngine.Random.Range(-15f, 15f), UnityEngine.Random.Range(-15f, 15f));
					}
				}
			}
			if (this.AttackPhase == 0)
			{
				if (this.MyAnim["f02_snapKill_00"].time > this.MyAnim["f02_snapKill_00"].length * 0.2f)
				{
					UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, this.Knife.transform.position, Quaternion.identity);
					this.AttackPhase++;
				}
			}
			else if (this.AttackPhase == 1)
			{
				if (this.MyAnim["f02_snapKill_00"].time > this.MyAnim["f02_snapKill_00"].length * 0.36f)
				{
					UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, this.Knife.transform.position, Quaternion.identity);
					this.AttackPhase++;
				}
			}
			else if (this.AttackPhase == 2)
			{
				if (this.MyAnim["f02_snapKill_00"].time > 13f)
				{
					this.MyAnim["f02_stareAtKnife_00"].layer = 100;
					this.MyAnim.Play("f02_stareAtKnife_00");
					this.MyAnim["f02_stareAtKnife_00"].weight = 0f;
					this.Whisper = true;
					this.Rumble.Play();
					this.Speed = 0f;
					this.AttackPhase++;
				}
			}
			else if (this.AttackPhase == 3)
			{
				this.Knife.transform.localEulerAngles = Vector3.Lerp(this.Knife.transform.localEulerAngles, new Vector3(0f, 0f, 0f), Time.deltaTime * this.Speed);
				this.MyAnim["f02_stareAtKnife_00"].weight = Mathf.Lerp(this.MyAnim["f02_stareAtKnife_00"].weight, 1f, Time.deltaTime * this.Speed);
				if (this.MyAnim["f02_stareAtKnife_00"].weight > 0.999f)
				{
					this.SuicidePrompt.alpha += Time.deltaTime;
					this.ImpatienceTimer += Time.deltaTime;
					if (Input.GetButtonDown("X") || this.ImpatienceTimer > this.ImpatienceLimit)
					{
						this.MyAnim["f02_suicide_00"].layer = 101;
						this.MyAnim.Play("f02_suicide_00");
						this.MyAnim["f02_suicide_00"].weight = 0f;
						this.MyAnim["f02_suicide_00"].time = 2f;
						this.MyAnim["f02_suicide_00"].speed = 0f;
						this.AttackPhase++;
						if (this.ImpatienceTimer > this.ImpatienceLimit)
						{
							this.ImpatienceLimit = 2f;
							this.ImpatienceTimer = 0f;
						}
						this.Taps++;
					}
				}
			}
			else if (this.AttackPhase == 4)
			{
				this.SuicidePrompt.alpha += Time.deltaTime;
				this.ImpatienceTimer += Time.deltaTime;
				if (Input.GetButtonDown("X") || this.ImpatienceTimer > this.ImpatienceLimit)
				{
					this.Target += 0.1f;
					this.SpeedUp = true;
					if (this.ImpatienceTimer > this.ImpatienceLimit)
					{
						this.ImpatienceLimit = 2f;
						this.ImpatienceTimer = 0f;
					}
					this.Taps++;
				}
				if (this.SpeedUp)
				{
					this.AnimSpeed += Time.deltaTime;
					if (this.AnimSpeed > 1f)
					{
						this.SpeedUp = false;
					}
				}
				else
				{
					this.AnimSpeed = Mathf.MoveTowards(this.AnimSpeed, 0f, Time.deltaTime);
				}
				this.MyAnim["f02_suicide_00"].weight = Mathf.Lerp(this.MyAnim["f02_suicide_00"].weight, this.Target, this.AnimSpeed * Time.deltaTime);
				if (this.MyAnim["f02_suicide_00"].weight >= 1f)
				{
					this.SpeedUp = false;
					this.AnimSpeed = 0f;
					this.Target = 2f;
					this.AttackPhase++;
				}
			}
			else if (this.AttackPhase == 5)
			{
				this.ImpatienceTimer += Time.deltaTime;
				if (Input.GetButtonDown("X") || this.ImpatienceTimer > this.ImpatienceLimit)
				{
					this.Target += 0.1f;
					this.SpeedUp = true;
					if (this.ImpatienceTimer > this.ImpatienceLimit)
					{
						this.ImpatienceLimit = 2f;
						this.ImpatienceTimer = 0f;
					}
					this.Taps++;
				}
				if (this.SpeedUp)
				{
					this.AnimSpeed += Time.deltaTime;
					if (this.AnimSpeed > 1f)
					{
						this.SpeedUp = false;
					}
				}
				else
				{
					this.AnimSpeed = Mathf.MoveTowards(this.AnimSpeed, 0f, Time.deltaTime);
				}
				this.MyAnim["f02_suicide_00"].time = Mathf.Lerp(this.MyAnim["f02_suicide_00"].time, this.Target, this.AnimSpeed * Time.deltaTime);
				if (this.MyAnim["f02_suicide_00"].time >= 3.66666f)
				{
					this.MyAnim["f02_suicide_00"].speed = 1f;
					this.SuicidePrompt.alpha = 0f;
					this.Rumble.volume = 0f;
					UnityEngine.Object.Destroy(this.NewDoIt);
					this.Whisper = false;
					this.AttackPhase++;
				}
			}
			else if (this.AttackPhase == 6)
			{
				if (this.MyAnim["f02_suicide_00"].time >= this.MyAnim["f02_suicide_00"].length * 0.355f)
				{
					UnityEngine.Object.Instantiate<GameObject>(this.StabBloodEffect, this.Knife.transform.position, Quaternion.identity);
					this.AttackPhase++;
				}
			}
			else if (this.MyAnim["f02_suicide_00"].time >= this.MyAnim["f02_suicide_00"].length * 0.475f)
			{
				this.MyListener.enabled = false;
				this.MainCamera.transform.parent = null;
				this.MainCamera.transform.position = new Vector3(0f, 2025f, -11f);
				this.MainCamera.transform.eulerAngles = new Vector3(0f, 0f, 0f);
				if (this.MyAnim["f02_suicide_00"].time >= this.MyAnim["f02_suicide_00"].length)
				{
					SceneManager.LoadScene("LoadingScene");
				}
			}
		}
		if (this.InputDevice.Type == InputDeviceType.MouseAndKeyboard)
		{
			this.SuicidePrompt.text = "F";
			this.SuicideSprite.enabled = false;
		}
		else
		{
			this.SuicidePrompt.text = "";
			this.SuicideSprite.enabled = true;
		}
		if (this.ListenTimer > 0f)
		{
			this.ListenTimer = Mathf.MoveTowards(this.ListenTimer, 0f, Time.deltaTime);
		}
	}

	// Token: 0x06001AB0 RID: 6832 RVA: 0x0010BB30 File Offset: 0x00109D30
	private void UpdateMovement()
	{
		this.MyController.Move(Physics.gravity * Time.deltaTime);
		float axis = Input.GetAxis("Vertical");
		float axis2 = Input.GetAxis("Horizontal");
		Vector3 vector = base.transform.TransformDirection(Vector3.forward);
		vector.y = 0f;
		vector = vector.normalized;
		Vector3 a = new Vector3(vector.z, 0f, -vector.x);
		Vector3 a2 = axis2 * a + axis * vector;
		if (Mathf.Abs(axis) > 0.5f || Mathf.Abs(axis2) > 0.5f)
		{
			this.MyAnim[this.WalkAnim].speed = Mathf.Abs(axis) + Mathf.Abs(axis2);
			if (this.MyAnim[this.WalkAnim].speed > 1f)
			{
				this.MyAnim[this.WalkAnim].speed = 1f;
			}
			this.MyAnim.CrossFade(this.WalkAnim);
			this.MyController.Move(a2 * Time.deltaTime);
		}
		else
		{
			this.MyAnim.CrossFade(this.IdleAnim);
		}
		float num = Input.GetAxis("Mouse X") * (float)OptionGlobals.Sensitivity;
		if (num != 0f)
		{
			base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, base.transform.eulerAngles.y + num * 36f * Time.deltaTime, base.transform.eulerAngles.z);
		}
		if (Input.GetButtonDown("LB"))
		{
			this.MyController.Move(a2 * 4f);
			this.SetGlitches(true);
			this.GlitchTimeLimit = 0.1f;
		}
	}

	// Token: 0x06001AB1 RID: 6833 RVA: 0x0010BD18 File Offset: 0x00109F18
	private void MoveTowardsTarget(Vector3 target)
	{
		Vector3 a = target - base.transform.position;
		this.MyController.Move(a * (Time.deltaTime * 10f));
	}

	// Token: 0x06001AB2 RID: 6834 RVA: 0x0010BD54 File Offset: 0x00109F54
	private void RotateTowardsTarget(Quaternion target)
	{
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, target, Time.deltaTime * 10f);
	}

	// Token: 0x06001AB3 RID: 6835 RVA: 0x0010BD80 File Offset: 0x00109F80
	private void SetGlitches(bool State)
	{
		this.GlitchTimer = 0f;
		this.Glitch1.enabled = State;
		this.Glitch2.enabled = State;
		this.Glitch4.enabled = State;
		this.Glitch5.enabled = State;
		this.Glitch6.enabled = State;
		this.Glitch7.enabled = State;
		this.Glitch10.enabled = State;
		this.Glitch11.enabled = State;
		if (State)
		{
			this.MyAudio.clip = this.Buzz;
			this.MyAudio.volume = 0.5f;
			this.MyAudio.pitch = UnityEngine.Random.Range(0.5f, 2f);
			this.MyAudio.Play();
		}
	}

	// Token: 0x06001AB4 RID: 6836 RVA: 0x0010BE44 File Offset: 0x0010A044
	public void ChooseAttack()
	{
		this.BloodSpawned = 0;
		this.SetGlitches(true);
		this.GlitchTimeLimit = 0.5f;
		this.AttackID = UnityEngine.Random.Range(1, 6);
		while (this.AttacksUsed[this.AttackID])
		{
			this.AttackID = UnityEngine.Random.Range(1, 6);
		}
		this.AttacksUsed[this.AttackID] = true;
		this.Attacks++;
		if (this.AttackID == 1)
		{
			this.TargetStudent.transform.position = base.transform.position + base.transform.forward * 0.0001f;
			this.TargetStudent.transform.LookAt(base.transform.position);
		}
		else if (this.AttackID == 2)
		{
			this.TargetStudent.transform.position = base.transform.position + base.transform.forward * 0.5f;
			this.TargetStudent.transform.LookAt(base.transform.position);
		}
		else if (this.AttackID == 3)
		{
			this.TargetStudent.transform.position = base.transform.position + base.transform.forward * 0.3f;
			this.TargetStudent.transform.LookAt(base.transform.position);
		}
		else if (this.AttackID == 4)
		{
			this.TargetStudent.transform.position = base.transform.position + base.transform.forward * 0.3f;
			this.TargetStudent.transform.rotation = base.transform.rotation;
		}
		else if (this.AttackID == 5)
		{
			this.TargetStudent.transform.position = base.transform.position + base.transform.forward * 0.66666f;
			this.TargetStudent.transform.rotation = base.transform.rotation;
		}
		Physics.SyncTransforms();
		this.MyAnim.Play(this.AttackAnims[this.AttackID]);
		this.MyAnim[this.AttackAnims[this.AttackID]].time = 0f;
		this.TargetStudent.MyAnim.Play(this.TargetStudent.AttackAnims[this.AttackID]);
		this.TargetStudent.MyAnim[this.TargetStudent.AttackAnims[this.AttackID]].time = 0f;
	}

	// Token: 0x06001AB5 RID: 6837 RVA: 0x0010C110 File Offset: 0x0010A310
	public void Teleport()
	{
		if (!this.Armed)
		{
			bool flag = false;
			while (!flag)
			{
				foreach (WeaponScript weaponScript in this.Weapons)
				{
					if (weaponScript != null)
					{
						this.SetGlitches(true);
						this.GlitchTimeLimit = 1f;
						base.transform.position = weaponScript.transform.position;
						flag = true;
					}
				}
			}
		}
		else
		{
			this.Teleports++;
			this.SetGlitches(true);
			this.GlitchTimeLimit = 1f;
			if (this.Teleports == 1)
			{
				base.transform.position = this.StudentManager.Students[1].transform.position + this.StudentManager.Students[1].transform.forward * 2f;
				base.transform.LookAt(this.StudentManager.Students[1].transform.position);
			}
			else
			{
				base.transform.position = this.StudentManager.Students[1].transform.position + this.StudentManager.Students[1].transform.forward * 0.9f;
				base.transform.LookAt(this.StudentManager.Students[1].transform.position);
			}
		}
		Physics.SyncTransforms();
	}

	// Token: 0x04002AE8 RID: 10984
	public CharacterController MyController;

	// Token: 0x04002AE9 RID: 10985
	public CameraFilterPack_FX_Glitch1 Glitch1;

	// Token: 0x04002AEA RID: 10986
	public CameraFilterPack_FX_Glitch2 Glitch2;

	// Token: 0x04002AEB RID: 10987
	public CameraFilterPack_FX_Glitch3 Glitch3;

	// Token: 0x04002AEC RID: 10988
	public CameraFilterPack_Glitch_Mozaic Glitch4;

	// Token: 0x04002AED RID: 10989
	public CameraFilterPack_NewGlitch1 Glitch5;

	// Token: 0x04002AEE RID: 10990
	public CameraFilterPack_NewGlitch2 Glitch6;

	// Token: 0x04002AEF RID: 10991
	public CameraFilterPack_NewGlitch3 Glitch7;

	// Token: 0x04002AF0 RID: 10992
	public CameraFilterPack_NewGlitch4 Glitch8;

	// Token: 0x04002AF1 RID: 10993
	public CameraFilterPack_NewGlitch5 Glitch9;

	// Token: 0x04002AF2 RID: 10994
	public CameraFilterPack_NewGlitch6 Glitch10;

	// Token: 0x04002AF3 RID: 10995
	public CameraFilterPack_NewGlitch7 Glitch11;

	// Token: 0x04002AF4 RID: 10996
	public CameraFilterPack_TV_CompressionFX CompressionFX;

	// Token: 0x04002AF5 RID: 10997
	public CameraFilterPack_TV_Distorted Distorted;

	// Token: 0x04002AF6 RID: 10998
	public CameraFilterPack_Blur_Tilt_Shift TiltShift;

	// Token: 0x04002AF7 RID: 10999
	public CameraFilterPack_Blur_Tilt_Shift_V TiltShiftV;

	// Token: 0x04002AF8 RID: 11000
	public CameraFilterPack_Noise_TV Static;

	// Token: 0x04002AF9 RID: 11001
	public StudentManagerScript StudentManager;

	// Token: 0x04002AFA RID: 11002
	public SnapStudentScript TargetStudent;

	// Token: 0x04002AFB RID: 11003
	public InputDeviceScript InputDevice;

	// Token: 0x04002AFC RID: 11004
	public GameObject StabBloodEffect;

	// Token: 0x04002AFD RID: 11005
	public GameObject BloodEffect;

	// Token: 0x04002AFE RID: 11006
	public GameObject NewDoIt;

	// Token: 0x04002AFF RID: 11007
	public WeaponScript Knife;

	// Token: 0x04002B00 RID: 11008
	public AudioListener MyListener;

	// Token: 0x04002B01 RID: 11009
	public Transform SnapAttackPivot;

	// Token: 0x04002B02 RID: 11010
	public Transform FinalSnapPOV;

	// Token: 0x04002B03 RID: 11011
	public Transform SuicidePOV;

	// Token: 0x04002B04 RID: 11012
	public Transform RightFoot;

	// Token: 0x04002B05 RID: 11013
	public Transform RightHand;

	// Token: 0x04002B06 RID: 11014
	public Transform LeftHand;

	// Token: 0x04002B07 RID: 11015
	public Transform Spine;

	// Token: 0x04002B08 RID: 11016
	public AudioSource StaticNoise;

	// Token: 0x04002B09 RID: 11017
	public AudioSource AttackAudio;

	// Token: 0x04002B0A RID: 11018
	public AudioSource SnapStatic;

	// Token: 0x04002B0B RID: 11019
	public AudioSource SnapVoice;

	// Token: 0x04002B0C RID: 11020
	public AudioSource Jukebox;

	// Token: 0x04002B0D RID: 11021
	public AudioSource MyAudio;

	// Token: 0x04002B0E RID: 11022
	public AudioSource Rumble;

	// Token: 0x04002B0F RID: 11023
	public AudioClip EndSNAP;

	// Token: 0x04002B10 RID: 11024
	public UILabel SNAPLabel;

	// Token: 0x04002B11 RID: 11025
	public Camera MainCamera;

	// Token: 0x04002B12 RID: 11026
	public Animation MyAnim;

	// Token: 0x04002B13 RID: 11027
	public AudioClip Buzz;

	// Token: 0x04002B14 RID: 11028
	public AudioClip[] Whispers;

	// Token: 0x04002B15 RID: 11029
	public AudioClip[] FemaleDeathScreams;

	// Token: 0x04002B16 RID: 11030
	public AudioClip[] MaleDeathScreams;

	// Token: 0x04002B17 RID: 11031
	public AudioClip[] AttackSFX;

	// Token: 0x04002B18 RID: 11032
	public GameObject DoIt;

	// Token: 0x04002B19 RID: 11033
	public UISprite SuicideSprite;

	// Token: 0x04002B1A RID: 11034
	public UILabel SuicidePrompt;

	// Token: 0x04002B1B RID: 11035
	public bool KillingSenpai;

	// Token: 0x04002B1C RID: 11036
	public bool Attacking;

	// Token: 0x04002B1D RID: 11037
	public bool CanMove;

	// Token: 0x04002B1E RID: 11038
	public bool SpeedUp;

	// Token: 0x04002B1F RID: 11039
	public bool Whisper;

	// Token: 0x04002B20 RID: 11040
	public bool Armed;

	// Token: 0x04002B21 RID: 11041
	public string IdleAnim;

	// Token: 0x04002B22 RID: 11042
	public string WalkAnim;

	// Token: 0x04002B23 RID: 11043
	public float ImpatienceLimit;

	// Token: 0x04002B24 RID: 11044
	public float GlitchTimeLimit;

	// Token: 0x04002B25 RID: 11045
	public float WhisperTimer;

	// Token: 0x04002B26 RID: 11046
	public float AttackTimer;

	// Token: 0x04002B27 RID: 11047
	public float GlitchTimer;

	// Token: 0x04002B28 RID: 11048
	public float ImpatienceTimer;

	// Token: 0x04002B29 RID: 11049
	public float ListenTimer;

	// Token: 0x04002B2A RID: 11050
	public float HurryTimer;

	// Token: 0x04002B2B RID: 11051
	public float AnimSpeed;

	// Token: 0x04002B2C RID: 11052
	public float Target;

	// Token: 0x04002B2D RID: 11053
	public float Speed;

	// Token: 0x04002B2E RID: 11054
	public int BloodSpawned;

	// Token: 0x04002B2F RID: 11055
	public int AttackPhase;

	// Token: 0x04002B30 RID: 11056
	public int Teleports;

	// Token: 0x04002B31 RID: 11057
	public int AttackID;

	// Token: 0x04002B32 RID: 11058
	public int VoiceID;

	// Token: 0x04002B33 RID: 11059
	public int Attacks;

	// Token: 0x04002B34 RID: 11060
	public int Taps;

	// Token: 0x04002B35 RID: 11061
	public string[] AttackAnims;

	// Token: 0x04002B36 RID: 11062
	public WeaponScript[] Weapons;

	// Token: 0x04002B37 RID: 11063
	public bool[] AttacksUsed;
}
