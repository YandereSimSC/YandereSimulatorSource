using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000489 RID: 1161
[RequireComponent(typeof(CharacterController))]
public class YanvaniaYanmontScript : MonoBehaviour
{
	// Token: 0x06001DDB RID: 7643 RVA: 0x001745B0 File Offset: 0x001727B0
	private void Awake()
	{
		Animation component = this.Character.GetComponent<Animation>();
		component["f02_yanvaniaDeath_00"].speed = 0.25f;
		component["f02_yanvaniaAttack_00"].speed = 2f;
		component["f02_yanvaniaCrouchAttack_00"].speed = 2f;
		component["f02_yanvaniaWalk_00"].speed = 0.6666667f;
		component["f02_yanvaniaWhip_Neutral"].speed = 0f;
		component["f02_yanvaniaWhip_Up"].speed = 0f;
		component["f02_yanvaniaWhip_Right"].speed = 0f;
		component["f02_yanvaniaWhip_Down"].speed = 0f;
		component["f02_yanvaniaWhip_Left"].speed = 0f;
		component["f02_yanvaniaCrouchPose_00"].layer = 1;
		component.Play("f02_yanvaniaCrouchPose_00");
		component["f02_yanvaniaCrouchPose_00"].weight = 0f;
		Physics.IgnoreLayerCollision(19, 13, true);
		Physics.IgnoreLayerCollision(19, 19, true);
	}

	// Token: 0x06001DDC RID: 7644 RVA: 0x001746CC File Offset: 0x001728CC
	private void Start()
	{
		this.WhipChain[0].transform.localScale = Vector3.zero;
		this.Character.GetComponent<Animation>().Play("f02_yanvaniaIdle_00");
		this.controller = base.GetComponent<CharacterController>();
		this.myTransform = base.transform;
		this.speed = this.walkSpeed;
		this.rayDistance = this.controller.height * 0.5f + this.controller.radius;
		this.slideLimit = this.controller.slopeLimit - 0.1f;
		this.jumpTimer = this.antiBunnyHopFactor;
		this.originalThreshold = this.fallingDamageThreshold;
	}

	// Token: 0x06001DDD RID: 7645 RVA: 0x0017477C File Offset: 0x0017297C
	private void FixedUpdate()
	{
		Animation component = this.Character.GetComponent<Animation>();
		if (this.CanMove)
		{
			if (!this.Injured)
			{
				if (!this.Cutscene)
				{
					if (this.grounded)
					{
						if (!this.Attacking)
						{
							if (!this.Crouching)
							{
								if (Input.GetAxis("VaniaHorizontal") > 0f)
								{
									this.inputX = 1f;
								}
								else if (Input.GetAxis("VaniaHorizontal") < 0f)
								{
									this.inputX = -1f;
								}
								else
								{
									this.inputX = 0f;
								}
							}
						}
						else if (this.grounded)
						{
							this.fallingDamageThreshold = 100f;
							this.moveDirection.x = 0f;
							this.inputX = 0f;
							this.speed = 0f;
						}
					}
					else if (Input.GetAxis("VaniaHorizontal") != 0f)
					{
						if (Input.GetAxis("VaniaHorizontal") > 0f)
						{
							this.inputX = 1f;
						}
						else if (Input.GetAxis("VaniaHorizontal") < 0f)
						{
							this.inputX = -1f;
						}
						else
						{
							this.inputX = 0f;
						}
					}
					else
					{
						this.inputX = Mathf.MoveTowards(this.inputX, 0f, Time.deltaTime * 10f);
					}
					float num = 0f;
					float num2 = (this.inputX != 0f && num != 0f && this.limitDiagonalSpeed) ? 0.70710677f : 1f;
					if (!this.Attacking)
					{
						if (Input.GetAxis("VaniaHorizontal") < 0f)
						{
							this.Character.transform.localEulerAngles = new Vector3(this.Character.transform.localEulerAngles.x, -90f, this.Character.transform.localEulerAngles.z);
							this.Character.transform.localScale = new Vector3(1f, this.Character.transform.localScale.y, this.Character.transform.localScale.z);
						}
						else if (Input.GetAxis("VaniaHorizontal") > 0f)
						{
							this.Character.transform.localEulerAngles = new Vector3(this.Character.transform.localEulerAngles.x, 90f, this.Character.transform.localEulerAngles.z);
							this.Character.transform.localScale = new Vector3(-1f, this.Character.transform.localScale.y, this.Character.transform.localScale.z);
						}
					}
					if (this.grounded)
					{
						if (!this.Attacking && !this.Dangling)
						{
							if (Input.GetAxis("VaniaVertical") < -0.5f)
							{
								this.MyController.center = new Vector3(this.MyController.center.x, 0.5f, this.MyController.center.z);
								this.MyController.height = 1f;
								this.Crouching = true;
								this.IdleTimer = 10f;
								this.inputX = 0f;
							}
							if (this.Crouching)
							{
								component.CrossFade("f02_yanvaniaCrouch_00", 0.1f);
								if (!this.Attacking)
								{
									if (!this.Dangling)
									{
										if (Input.GetAxis("VaniaVertical") > -0.5f)
										{
											component["f02_yanvaniaCrouchPose_00"].weight = 0f;
											this.MyController.center = new Vector3(this.MyController.center.x, 0.75f, this.MyController.center.z);
											this.MyController.height = 1.5f;
											this.Crouching = false;
										}
									}
									else if (Input.GetAxis("VaniaVertical") > -0.5f && Input.GetButton("X"))
									{
										component["f02_yanvaniaCrouchPose_00"].weight = 0f;
										this.MyController.center = new Vector3(this.MyController.center.x, 0.75f, this.MyController.center.z);
										this.MyController.height = 1.5f;
										this.Crouching = false;
									}
								}
							}
							else if (this.inputX == 0f)
							{
								if (this.IdleTimer > 0f)
								{
									component.CrossFade("f02_yanvaniaIdle_00", 0.1f);
									component["f02_yanvaniaIdle_00"].speed = this.IdleTimer / 10f;
								}
								else
								{
									component.CrossFade("f02_yanvaniaDramaticIdle_00", 1f);
								}
								this.IdleTimer -= Time.deltaTime;
							}
							else
							{
								this.IdleTimer = 10f;
								component.CrossFade((this.speed == this.walkSpeed) ? "f02_yanvaniaWalk_00" : "f02_yanvaniaRun_00", 0.1f);
							}
						}
						bool flag = false;
						if (Physics.Raycast(this.myTransform.position, -Vector3.up, out this.hit, this.rayDistance))
						{
							if (Vector3.Angle(this.hit.normal, Vector3.up) > this.slideLimit)
							{
								flag = true;
							}
						}
						else
						{
							Physics.Raycast(this.contactPoint + Vector3.up, -Vector3.up, out this.hit);
							if (Vector3.Angle(this.hit.normal, Vector3.up) > this.slideLimit)
							{
								flag = true;
							}
						}
						if (this.falling)
						{
							this.falling = false;
							if (this.myTransform.position.y < this.fallStartLevel - this.fallingDamageThreshold)
							{
								this.FallingDamageAlert(this.fallStartLevel - this.myTransform.position.y);
							}
							this.fallingDamageThreshold = this.originalThreshold;
						}
						if (!this.toggleRun)
						{
							this.speed = (Input.GetKey(KeyCode.LeftShift) ? this.runSpeed : this.walkSpeed);
						}
						if ((flag && this.slideWhenOverSlopeLimit) || (this.slideOnTaggedObjects && this.hit.collider.tag == "Slide"))
						{
							Vector3 normal = this.hit.normal;
							this.moveDirection = new Vector3(normal.x, -normal.y, normal.z);
							Vector3.OrthoNormalize(ref normal, ref this.moveDirection);
							this.moveDirection *= this.slideSpeed;
							this.playerControl = false;
						}
						else
						{
							this.moveDirection = new Vector3(this.inputX * num2, -this.antiBumpFactor, num * num2);
							this.moveDirection = this.myTransform.TransformDirection(this.moveDirection) * this.speed;
							this.playerControl = true;
						}
						if (!Input.GetButton("VaniaJump"))
						{
							this.jumpTimer++;
						}
						else if (this.jumpTimer >= this.antiBunnyHopFactor && !this.Attacking)
						{
							this.Crouching = false;
							this.fallingDamageThreshold = 0f;
							this.moveDirection.y = this.jumpSpeed;
							this.IdleTimer = 10f;
							this.jumpTimer = 0;
							AudioSource component2 = base.GetComponent<AudioSource>();
							component2.clip = this.Voices[UnityEngine.Random.Range(0, this.Voices.Length)];
							component2.Play();
						}
					}
					else
					{
						if (!this.Attacking)
						{
							component.CrossFade((base.transform.position.y > this.PreviousY) ? "f02_yanvaniaJump_00" : "f02_yanvaniaFall_00", 0.4f);
						}
						this.PreviousY = base.transform.position.y;
						if (!this.falling)
						{
							this.falling = true;
							this.fallStartLevel = this.myTransform.position.y;
						}
						if (this.airControl && this.playerControl)
						{
							this.moveDirection.x = this.inputX * this.speed * num2;
							this.moveDirection.z = num * this.speed * num2;
							this.moveDirection = this.myTransform.TransformDirection(this.moveDirection);
						}
					}
				}
				else
				{
					this.moveDirection.x = 0f;
					if (this.grounded)
					{
						if (base.transform.position.x > -34f)
						{
							this.Character.transform.localEulerAngles = new Vector3(this.Character.transform.localEulerAngles.x, -90f, this.Character.transform.localEulerAngles.z);
							this.Character.transform.localScale = new Vector3(1f, this.Character.transform.localScale.y, this.Character.transform.localScale.z);
							base.transform.position = new Vector3(Mathf.MoveTowards(base.transform.position.x, -34f, Time.deltaTime * this.walkSpeed), base.transform.position.y, base.transform.position.z);
							component.CrossFade("f02_yanvaniaWalk_00");
						}
						else if (base.transform.position.x < -34f)
						{
							this.Character.transform.localEulerAngles = new Vector3(this.Character.transform.localEulerAngles.x, 90f, this.Character.transform.localEulerAngles.z);
							this.Character.transform.localScale = new Vector3(-1f, this.Character.transform.localScale.y, this.Character.transform.localScale.z);
							base.transform.position = new Vector3(Mathf.MoveTowards(base.transform.position.x, -34f, Time.deltaTime * this.walkSpeed), base.transform.position.y, base.transform.position.z);
							component.CrossFade("f02_yanvaniaWalk_00");
						}
						else
						{
							component.CrossFade("f02_yanvaniaDramaticIdle_00", 1f);
							this.Character.transform.localEulerAngles = new Vector3(this.Character.transform.localEulerAngles.x, -90f, this.Character.transform.localEulerAngles.z);
							this.Character.transform.localScale = new Vector3(1f, this.Character.transform.localScale.y, this.Character.transform.localScale.z);
							this.WhipChain[0].transform.localScale = Vector3.zero;
							this.fallingDamageThreshold = 100f;
							this.TextBox.SetActive(true);
							this.Attacking = false;
							base.enabled = false;
						}
					}
					Physics.SyncTransforms();
				}
			}
			else
			{
				component.CrossFade("f02_damage_25");
				this.RecoveryTimer += Time.deltaTime;
				if (this.RecoveryTimer > 1f)
				{
					this.RecoveryTimer = 0f;
					this.Injured = false;
				}
			}
			this.moveDirection.y = this.moveDirection.y - this.gravity * Time.deltaTime;
			this.grounded = ((this.controller.Move(this.moveDirection * Time.deltaTime) & CollisionFlags.Below) > CollisionFlags.None);
			if (this.grounded && this.EnterCutscene)
			{
				this.YanvaniaCamera.Cutscene = true;
				this.Cutscene = true;
			}
			if ((this.controller.collisionFlags & CollisionFlags.Above) != CollisionFlags.None && this.moveDirection.y > 0f)
			{
				this.moveDirection.y = 0f;
				return;
			}
		}
		else if (this.Health == 0f)
		{
			this.DeathTimer += Time.deltaTime;
			if (this.DeathTimer > 5f)
			{
				this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, this.Darkness.color.a + Time.deltaTime * 0.2f);
				if (this.Darkness.color.a >= 1f)
				{
					if (this.Darkness.gameObject.activeInHierarchy)
					{
						this.HealthBar.parent.gameObject.SetActive(false);
						this.EXPBar.parent.gameObject.SetActive(false);
						this.Darkness.gameObject.SetActive(false);
						this.BossHealthBar.SetActive(false);
						this.BlackBG.SetActive(true);
					}
					this.TryAgainWindow.transform.localScale = Vector3.Lerp(this.TryAgainWindow.transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
				}
			}
		}
	}

	// Token: 0x06001DDE RID: 7646 RVA: 0x00175538 File Offset: 0x00173738
	private void Update()
	{
		Animation component = this.Character.GetComponent<Animation>();
		if (!this.Injured && this.CanMove && !this.Cutscene)
		{
			if (this.grounded)
			{
				if (this.InputManager.TappedRight || this.InputManager.TappedLeft)
				{
					this.TapTimer = 0.25f;
					this.Taps++;
				}
				if (this.Taps > 1)
				{
					this.speed = this.runSpeed;
				}
			}
			if (this.inputX == 0f)
			{
				this.speed = this.walkSpeed;
			}
			this.TapTimer -= Time.deltaTime;
			if (this.TapTimer < 0f)
			{
				this.Taps = 0;
			}
			if (Input.GetButtonDown("VaniaAttack") && !this.Attacking)
			{
				AudioSource.PlayClipAtPoint(this.WhipSound, base.transform.position);
				AudioSource component2 = base.GetComponent<AudioSource>();
				component2.clip = this.Voices[UnityEngine.Random.Range(0, this.Voices.Length)];
				component2.Play();
				this.WhipChain[0].transform.localScale = Vector3.zero;
				this.Attacking = true;
				this.IdleTimer = 10f;
				if (this.Crouching)
				{
					component["f02_yanvaniaCrouchAttack_00"].time = 0f;
					component.Play("f02_yanvaniaCrouchAttack_00");
				}
				else
				{
					component["f02_yanvaniaAttack_00"].time = 0f;
					component.Play("f02_yanvaniaAttack_00");
				}
				if (this.grounded)
				{
					this.moveDirection.x = 0f;
					this.inputX = 0f;
					this.speed = 0f;
				}
			}
			if (this.Attacking)
			{
				if (!this.Dangling)
				{
					this.WhipChain[0].transform.localScale = Vector3.MoveTowards(this.WhipChain[0].transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 5f);
					this.StraightenWhip();
				}
				else
				{
					this.LoosenWhip();
					if (Input.GetAxis("VaniaHorizontal") > -0.5f && Input.GetAxis("VaniaHorizontal") < 0.5f && Input.GetAxis("VaniaVertical") > -0.5f && Input.GetAxis("VaniaVertical") < 0.5f)
					{
						component.CrossFade("f02_yanvaniaWhip_Neutral");
						if (this.Crouching)
						{
							component["f02_yanvaniaCrouchPose_00"].weight = 1f;
						}
						this.SpunUp = false;
						this.SpunDown = false;
						this.SpunRight = false;
						this.SpunLeft = false;
					}
					else
					{
						if (Input.GetAxis("VaniaVertical") > 0.5f)
						{
							if (!this.SpunUp)
							{
								AudioClipPlayer.Play2D(this.WhipSound, base.transform.position, UnityEngine.Random.Range(0.9f, 1.1f));
								this.StraightenWhip();
								this.TargetRotation = -360f;
								this.Rotation = 0f;
								this.SpunUp = true;
							}
							component.CrossFade("f02_yanvaniaWhip_Up", 0.1f);
						}
						else
						{
							this.SpunUp = false;
						}
						if (Input.GetAxis("VaniaVertical") < -0.5f)
						{
							if (!this.SpunDown)
							{
								AudioClipPlayer.Play2D(this.WhipSound, base.transform.position, UnityEngine.Random.Range(0.9f, 1.1f));
								this.StraightenWhip();
								this.TargetRotation = 360f;
								this.Rotation = 0f;
								this.SpunDown = true;
							}
							component.CrossFade("f02_yanvaniaWhip_Down", 0.1f);
						}
						else
						{
							this.SpunDown = false;
						}
						if (Input.GetAxis("VaniaHorizontal") > 0.5f)
						{
							if (this.Character.transform.localScale.x == 1f)
							{
								this.SpinRight();
							}
							else
							{
								this.SpinLeft();
							}
						}
						else if (this.Character.transform.localScale.x == 1f)
						{
							this.SpunRight = false;
						}
						else
						{
							this.SpunLeft = false;
						}
						if (Input.GetAxis("VaniaHorizontal") < -0.5f)
						{
							if (this.Character.transform.localScale.x == 1f)
							{
								this.SpinLeft();
							}
							else
							{
								this.SpinRight();
							}
						}
						else if (this.Character.transform.localScale.x == 1f)
						{
							this.SpunLeft = false;
						}
						else
						{
							this.SpunRight = false;
						}
					}
					this.Rotation = Mathf.MoveTowards(this.Rotation, this.TargetRotation, Time.deltaTime * 3600f * 0.5f);
					this.WhipChain[1].transform.localEulerAngles = new Vector3(0f, 0f, this.Rotation);
					if (!Input.GetButton("VaniaAttack"))
					{
						this.StopAttacking();
					}
				}
			}
			else
			{
				if (this.WhipCollider[1].enabled)
				{
					for (int i = 1; i < this.WhipChain.Length; i++)
					{
						this.SphereCollider[i].enabled = false;
						this.WhipCollider[i].enabled = false;
					}
				}
				this.WhipChain[0].transform.localScale = Vector3.MoveTowards(this.WhipChain[0].transform.localScale, Vector3.zero, Time.deltaTime * 10f);
			}
			if ((!this.Crouching && component["f02_yanvaniaAttack_00"].time >= component["f02_yanvaniaAttack_00"].length) || (this.Crouching && component["f02_yanvaniaCrouchAttack_00"].time >= component["f02_yanvaniaCrouchAttack_00"].length))
			{
				if (Input.GetButton("VaniaAttack"))
				{
					if (this.Crouching)
					{
						component["f02_yanvaniaCrouchPose_00"].weight = 1f;
					}
					this.Dangling = true;
				}
				else
				{
					this.StopAttacking();
				}
			}
		}
		if (this.FlashTimer > 0f)
		{
			this.FlashTimer -= Time.deltaTime;
			if (!this.Red)
			{
				Material[] materials = this.MyRenderer.materials;
				for (int j = 0; j < materials.Length; j++)
				{
					materials[j].color = new Color(1f, 0f, 0f, 1f);
				}
				this.Frames++;
				if (this.Frames == 5)
				{
					this.Frames = 0;
					this.Red = true;
				}
			}
			else
			{
				Material[] materials = this.MyRenderer.materials;
				for (int j = 0; j < materials.Length; j++)
				{
					materials[j].color = new Color(1f, 1f, 1f, 1f);
				}
				this.Frames++;
				if (this.Frames == 5)
				{
					this.Frames = 0;
					this.Red = false;
				}
			}
		}
		else
		{
			this.FlashTimer = 0f;
			if (this.MyRenderer.materials[0].color != new Color(1f, 1f, 1f, 1f))
			{
				Material[] materials = this.MyRenderer.materials;
				for (int j = 0; j < materials.Length; j++)
				{
					materials[j].color = new Color(1f, 1f, 1f, 1f);
				}
			}
		}
		this.HealthBar.localScale = new Vector3(this.HealthBar.localScale.x, Mathf.Lerp(this.HealthBar.localScale.y, this.Health / this.MaxHealth, Time.deltaTime * 10f), this.HealthBar.localScale.z);
		if (this.Health > 0f)
		{
			if (this.EXP >= 100f)
			{
				this.Level++;
				if (this.Level >= 99)
				{
					this.Level = 99;
				}
				else
				{
					UnityEngine.Object.Instantiate<GameObject>(this.LevelUpEffect, this.LevelLabel.transform.position, Quaternion.identity).transform.parent = this.LevelLabel.transform;
					this.MaxHealth += 20f;
					this.Health = this.MaxHealth;
					this.EXP -= 100f;
				}
				this.LevelLabel.text = this.Level.ToString();
			}
			this.EXPBar.localScale = new Vector3(this.EXPBar.localScale.x, Mathf.Lerp(this.EXPBar.localScale.y, this.EXP / 100f, Time.deltaTime * 10f), this.EXPBar.localScale.z);
		}
		base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, 0f);
		if (Input.GetKeyDown(KeyCode.BackQuote))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			base.transform.position = new Vector3(-31.75f, 6.51f, 0f);
			Physics.SyncTransforms();
		}
		if (Input.GetKeyDown(KeyCode.Alpha5))
		{
			this.Level = 5;
			this.LevelLabel.text = this.Level.ToString();
		}
		if (Input.GetKeyDown(KeyCode.Equals))
		{
			Time.timeScale += 10f;
		}
		if (Input.GetKeyDown(KeyCode.Minus))
		{
			Time.timeScale -= 10f;
			if (Time.timeScale < 0f)
			{
				Time.timeScale = 1f;
			}
		}
	}

	// Token: 0x06001DDF RID: 7647 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void LateUpdate()
	{
	}

	// Token: 0x06001DE0 RID: 7648 RVA: 0x00175EF0 File Offset: 0x001740F0
	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		this.contactPoint = this.hit.point;
	}

	// Token: 0x06001DE1 RID: 7649 RVA: 0x00175F04 File Offset: 0x00174104
	private void FallingDamageAlert(float fallDistance)
	{
		AudioClipPlayer.Play2D(this.LandSound, base.transform.position, UnityEngine.Random.Range(0.9f, 1.1f));
		this.Character.GetComponent<Animation>().Play("f02_yanvaniaCrouch_00");
		this.fallingDamageThreshold = this.originalThreshold;
	}

	// Token: 0x06001DE2 RID: 7650 RVA: 0x00175F58 File Offset: 0x00174158
	private void SpinRight()
	{
		if (!this.SpunRight)
		{
			AudioClipPlayer.Play2D(this.WhipSound, base.transform.position, UnityEngine.Random.Range(0.9f, 1.1f));
			this.StraightenWhip();
			this.TargetRotation = 360f;
			this.Rotation = 0f;
			this.SpunRight = true;
		}
		this.Character.GetComponent<Animation>().CrossFade("f02_yanvaniaWhip_Right", 0.1f);
	}

	// Token: 0x06001DE3 RID: 7651 RVA: 0x00175FD0 File Offset: 0x001741D0
	private void SpinLeft()
	{
		if (!this.SpunLeft)
		{
			AudioClipPlayer.Play2D(this.WhipSound, base.transform.position, UnityEngine.Random.Range(0.9f, 1.1f));
			this.StraightenWhip();
			this.TargetRotation = -360f;
			this.Rotation = 0f;
			this.SpunLeft = true;
		}
		this.Character.GetComponent<Animation>().CrossFade("f02_yanvaniaWhip_Left", 0.1f);
	}

	// Token: 0x06001DE4 RID: 7652 RVA: 0x00176048 File Offset: 0x00174248
	private void StraightenWhip()
	{
		for (int i = 1; i < this.WhipChain.Length; i++)
		{
			this.WhipCollider[i].enabled = true;
			this.WhipChain[i].gameObject.GetComponent<Rigidbody>().isKinematic = true;
			Transform transform = this.WhipChain[i].transform;
			transform.localPosition = new Vector3(0f, -0.03f, 0f);
			transform.localEulerAngles = Vector3.zero;
		}
		this.WhipChain[1].transform.localPosition = new Vector3(0f, -0.1f, 0f);
		this.WhipTimer = 0f;
		this.Loose = false;
	}

	// Token: 0x06001DE5 RID: 7653 RVA: 0x001760F8 File Offset: 0x001742F8
	private void LoosenWhip()
	{
		if (!this.Loose)
		{
			this.WhipTimer += Time.deltaTime;
			if (this.WhipTimer > 0.25f)
			{
				for (int i = 1; i < this.WhipChain.Length; i++)
				{
					this.WhipChain[i].gameObject.GetComponent<Rigidbody>().isKinematic = false;
					this.SphereCollider[i].enabled = true;
				}
				this.Loose = true;
			}
		}
	}

	// Token: 0x06001DE6 RID: 7654 RVA: 0x0017616C File Offset: 0x0017436C
	private void StopAttacking()
	{
		this.Character.GetComponent<Animation>()["f02_yanvaniaCrouchPose_00"].weight = 0f;
		this.TargetRotation = 0f;
		this.Rotation = 0f;
		this.Attacking = false;
		this.Dangling = false;
		this.SpunUp = false;
		this.SpunDown = false;
		this.SpunRight = false;
		this.SpunLeft = false;
	}

	// Token: 0x06001DE7 RID: 7655 RVA: 0x001761D8 File Offset: 0x001743D8
	public void TakeDamage(int Damage)
	{
		if (this.WhipCollider[1].enabled)
		{
			for (int i = 1; i < this.WhipChain.Length; i++)
			{
				this.SphereCollider[i].enabled = false;
				this.WhipCollider[i].enabled = false;
			}
		}
		AudioSource component = base.GetComponent<AudioSource>();
		component.clip = this.Injuries[UnityEngine.Random.Range(0, this.Injuries.Length)];
		component.Play();
		this.WhipChain[0].transform.localScale = Vector3.zero;
		Animation component2 = this.Character.GetComponent<Animation>();
		component2["f02_damage_25"].time = 0f;
		this.fallingDamageThreshold = 100f;
		this.moveDirection.x = 0f;
		this.RecoveryTimer = 0f;
		this.FlashTimer = 2f;
		this.Injured = true;
		this.StopAttacking();
		this.Health -= (float)Damage;
		if (this.Dracula.Health <= 0f)
		{
			this.Health = 1f;
		}
		if (this.Dracula.Health > 0f && this.Health <= 0f)
		{
			if (this.NewBlood == null)
			{
				this.MyController.enabled = false;
				this.YanvaniaCamera.StopMusic = true;
				component.clip = this.DeathSound;
				component.Play();
				this.NewBlood = UnityEngine.Object.Instantiate<GameObject>(this.DeathBlood, base.transform.position, Quaternion.identity);
				this.NewBlood.transform.parent = this.Hips;
				this.NewBlood.transform.localPosition = Vector3.zero;
				component2.CrossFade("f02_yanvaniaDeath_00");
				this.CanMove = false;
			}
			this.Health = 0f;
		}
	}

	// Token: 0x04003B2E RID: 15150
	private GameObject NewBlood;

	// Token: 0x04003B2F RID: 15151
	public YanvaniaCameraScript YanvaniaCamera;

	// Token: 0x04003B30 RID: 15152
	public InputManagerScript InputManager;

	// Token: 0x04003B31 RID: 15153
	public YanvaniaDraculaScript Dracula;

	// Token: 0x04003B32 RID: 15154
	public CharacterController MyController;

	// Token: 0x04003B33 RID: 15155
	public GameObject BossHealthBar;

	// Token: 0x04003B34 RID: 15156
	public GameObject LevelUpEffect;

	// Token: 0x04003B35 RID: 15157
	public GameObject DeathBlood;

	// Token: 0x04003B36 RID: 15158
	public GameObject Character;

	// Token: 0x04003B37 RID: 15159
	public GameObject BlackBG;

	// Token: 0x04003B38 RID: 15160
	public GameObject TextBox;

	// Token: 0x04003B39 RID: 15161
	public Renderer MyRenderer;

	// Token: 0x04003B3A RID: 15162
	public Transform TryAgainWindow;

	// Token: 0x04003B3B RID: 15163
	public Transform HealthBar;

	// Token: 0x04003B3C RID: 15164
	public Transform EXPBar;

	// Token: 0x04003B3D RID: 15165
	public Transform Hips;

	// Token: 0x04003B3E RID: 15166
	public Transform TrailStart;

	// Token: 0x04003B3F RID: 15167
	public Transform TrailEnd;

	// Token: 0x04003B40 RID: 15168
	public UITexture Photograph;

	// Token: 0x04003B41 RID: 15169
	public UILabel LevelLabel;

	// Token: 0x04003B42 RID: 15170
	public UISprite Darkness;

	// Token: 0x04003B43 RID: 15171
	public Collider[] SphereCollider;

	// Token: 0x04003B44 RID: 15172
	public Collider[] WhipCollider;

	// Token: 0x04003B45 RID: 15173
	public Transform[] WhipChain;

	// Token: 0x04003B46 RID: 15174
	public AudioClip[] Voices;

	// Token: 0x04003B47 RID: 15175
	public AudioClip[] Injuries;

	// Token: 0x04003B48 RID: 15176
	public AudioClip DeathSound;

	// Token: 0x04003B49 RID: 15177
	public AudioClip LandSound;

	// Token: 0x04003B4A RID: 15178
	public AudioClip WhipSound;

	// Token: 0x04003B4B RID: 15179
	public bool Attacking;

	// Token: 0x04003B4C RID: 15180
	public bool Crouching;

	// Token: 0x04003B4D RID: 15181
	public bool Dangling;

	// Token: 0x04003B4E RID: 15182
	public bool EnterCutscene;

	// Token: 0x04003B4F RID: 15183
	public bool Cutscene;

	// Token: 0x04003B50 RID: 15184
	public bool CanMove;

	// Token: 0x04003B51 RID: 15185
	public bool Injured;

	// Token: 0x04003B52 RID: 15186
	public bool Loose;

	// Token: 0x04003B53 RID: 15187
	public bool Red;

	// Token: 0x04003B54 RID: 15188
	public bool SpunUp;

	// Token: 0x04003B55 RID: 15189
	public bool SpunDown;

	// Token: 0x04003B56 RID: 15190
	public bool SpunRight;

	// Token: 0x04003B57 RID: 15191
	public bool SpunLeft;

	// Token: 0x04003B58 RID: 15192
	public float TargetRotation;

	// Token: 0x04003B59 RID: 15193
	public float Rotation;

	// Token: 0x04003B5A RID: 15194
	public float RecoveryTimer;

	// Token: 0x04003B5B RID: 15195
	public float DeathTimer;

	// Token: 0x04003B5C RID: 15196
	public float FlashTimer;

	// Token: 0x04003B5D RID: 15197
	public float IdleTimer;

	// Token: 0x04003B5E RID: 15198
	public float WhipTimer;

	// Token: 0x04003B5F RID: 15199
	public float TapTimer;

	// Token: 0x04003B60 RID: 15200
	public float PreviousY;

	// Token: 0x04003B61 RID: 15201
	public float MaxHealth = 100f;

	// Token: 0x04003B62 RID: 15202
	public float Health = 100f;

	// Token: 0x04003B63 RID: 15203
	public float EXP;

	// Token: 0x04003B64 RID: 15204
	public int Frames;

	// Token: 0x04003B65 RID: 15205
	public int Level;

	// Token: 0x04003B66 RID: 15206
	public int Taps;

	// Token: 0x04003B67 RID: 15207
	public float walkSpeed = 6f;

	// Token: 0x04003B68 RID: 15208
	public float runSpeed = 11f;

	// Token: 0x04003B69 RID: 15209
	public bool limitDiagonalSpeed = true;

	// Token: 0x04003B6A RID: 15210
	public bool toggleRun;

	// Token: 0x04003B6B RID: 15211
	public float jumpSpeed = 8f;

	// Token: 0x04003B6C RID: 15212
	public float gravity = 20f;

	// Token: 0x04003B6D RID: 15213
	public float fallingDamageThreshold = 10f;

	// Token: 0x04003B6E RID: 15214
	public bool slideWhenOverSlopeLimit;

	// Token: 0x04003B6F RID: 15215
	public bool slideOnTaggedObjects;

	// Token: 0x04003B70 RID: 15216
	public float slideSpeed = 12f;

	// Token: 0x04003B71 RID: 15217
	public bool airControl;

	// Token: 0x04003B72 RID: 15218
	public float antiBumpFactor = 0.75f;

	// Token: 0x04003B73 RID: 15219
	public int antiBunnyHopFactor = 1;

	// Token: 0x04003B74 RID: 15220
	private Vector3 moveDirection = Vector3.zero;

	// Token: 0x04003B75 RID: 15221
	public bool grounded;

	// Token: 0x04003B76 RID: 15222
	private CharacterController controller;

	// Token: 0x04003B77 RID: 15223
	private Transform myTransform;

	// Token: 0x04003B78 RID: 15224
	private float speed;

	// Token: 0x04003B79 RID: 15225
	private RaycastHit hit;

	// Token: 0x04003B7A RID: 15226
	private float fallStartLevel;

	// Token: 0x04003B7B RID: 15227
	private bool falling;

	// Token: 0x04003B7C RID: 15228
	private float slideLimit;

	// Token: 0x04003B7D RID: 15229
	private float rayDistance;

	// Token: 0x04003B7E RID: 15230
	private Vector3 contactPoint;

	// Token: 0x04003B7F RID: 15231
	private bool playerControl;

	// Token: 0x04003B80 RID: 15232
	private int jumpTimer;

	// Token: 0x04003B81 RID: 15233
	private float originalThreshold;

	// Token: 0x04003B82 RID: 15234
	public float inputX;
}
