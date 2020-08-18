using System;
using UnityEngine;

// Token: 0x020003F1 RID: 1009
public class StalkerYandereScript : MonoBehaviour
{
	// Token: 0x06001AD6 RID: 6870 RVA: 0x0010D20C File Offset: 0x0010B40C
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
		if (Input.GetKeyDown("m"))
		{
			PlayerGlobals.Money += 1f;
			if (this.Jukebox != null)
			{
				if (this.Jukebox.isPlaying)
				{
					this.Jukebox.Stop();
				}
				else
				{
					this.Jukebox.Play();
				}
			}
		}
		if (this.CanMove)
		{
			if (this.CameraTarget != null)
			{
				this.CameraTarget.localPosition = new Vector3(0f, 1f + (this.RPGCamera.distanceMax - this.RPGCamera.distance) * 0.2f, 0f);
			}
			this.UpdateMovement();
		}
		else if (this.CameraTarget != null && this.Climbing)
		{
			if (this.ClimbPhase == 1)
			{
				if (this.MyAnimation["f02_climbTrellis_00"].time < this.MyAnimation["f02_climbTrellis_00"].length - 1f)
				{
					this.CameraTarget.position = Vector3.MoveTowards(this.CameraTarget.position, this.Hips.position + new Vector3(0f, 0.103729f, 0.003539f), Time.deltaTime);
				}
				else
				{
					this.CameraTarget.position = Vector3.MoveTowards(this.CameraTarget.position, new Vector3(-9.5f, 5f, -2.5f), Time.deltaTime);
				}
				this.MoveTowardsTarget(this.TrellisClimbSpot.position);
				this.SpinTowardsTarget(this.TrellisClimbSpot.rotation);
				if (this.MyAnimation["f02_climbTrellis_00"].time > 7.5f)
				{
					this.RPGCamera.transform.position = this.EntryPOV.position;
					this.RPGCamera.transform.eulerAngles = this.EntryPOV.eulerAngles;
					this.RPGCamera.enabled = false;
					RenderSettings.ambientIntensity = 8f;
					this.ClimbPhase++;
				}
			}
			else
			{
				this.RPGCamera.transform.position = this.EntryPOV.position;
				this.RPGCamera.transform.eulerAngles = this.EntryPOV.eulerAngles;
				if (this.MyAnimation["f02_climbTrellis_00"].time > 11f)
				{
					base.transform.position = Vector3.MoveTowards(base.transform.position, this.TrellisClimbSpot.position + new Vector3(0.4f, 0f, 0f), Time.deltaTime * 0.5f);
				}
			}
			if (this.MyAnimation["f02_climbTrellis_00"].time > this.MyAnimation["f02_climbTrellis_00"].length)
			{
				this.MyAnimation.Play(this.IdleAnim);
				base.transform.position = new Vector3(-9.1f, 4f, -2.5f);
				this.CameraTarget.position = base.transform.position + new Vector3(0f, 1f, 0f);
				this.RPGCamera.enabled = true;
				this.Climbing = false;
				this.CanMove = true;
			}
		}
		if (this.Street && base.transform.position.x < -16f)
		{
			base.transform.position = new Vector3(-16f, 0f, base.transform.position.z);
		}
	}

	// Token: 0x06001AD7 RID: 6871 RVA: 0x0010D624 File Offset: 0x0010B824
	private void UpdateMovement()
	{
		if (!OptionGlobals.ToggleRun)
		{
			this.Running = false;
			if (Input.GetButton("LB"))
			{
				this.Running = true;
			}
		}
		else if (Input.GetButtonDown("LB"))
		{
			this.Running = !this.Running;
		}
		this.MyController.Move(Physics.gravity * Time.deltaTime);
		float axis = Input.GetAxis("Vertical");
		float axis2 = Input.GetAxis("Horizontal");
		Vector3 vector = this.MainCamera.transform.TransformDirection(Vector3.forward);
		vector.y = 0f;
		vector = vector.normalized;
		Vector3 a = new Vector3(vector.z, 0f, -vector.x);
		Vector3 vector2 = axis2 * a + axis * vector;
		Quaternion b = Quaternion.identity;
		if (vector2 != Vector3.zero)
		{
			b = Quaternion.LookRotation(vector2);
		}
		if (vector2 != Vector3.zero)
		{
			base.transform.rotation = Quaternion.Lerp(base.transform.rotation, b, Time.deltaTime * 10f);
		}
		else
		{
			b = new Quaternion(0f, 0f, 0f, 0f);
		}
		if (!this.Street)
		{
			if (this.Stance.Current == StanceType.Standing)
			{
				if (Input.GetButtonDown("RS"))
				{
					this.Stance.Current = StanceType.Crouching;
				}
			}
			else if (Input.GetButtonDown("RS"))
			{
				this.Stance.Current = StanceType.Standing;
			}
		}
		if (axis != 0f || axis2 != 0f)
		{
			if (this.Running)
			{
				if (this.Stance.Current == StanceType.Crouching)
				{
					this.MyAnimation.CrossFade(this.CrouchRunAnim);
					this.MyController.Move(base.transform.forward * this.CrouchRunSpeed * Time.deltaTime);
					return;
				}
				this.MyAnimation.CrossFade(this.RunAnim);
				this.MyController.Move(base.transform.forward * this.RunSpeed * Time.deltaTime);
				return;
			}
			else
			{
				if (this.Stance.Current == StanceType.Crouching)
				{
					this.MyAnimation.CrossFade(this.CrouchWalkAnim);
					this.MyController.Move(base.transform.forward * (this.CrouchWalkSpeed * Time.deltaTime));
					return;
				}
				this.MyAnimation.CrossFade(this.WalkAnim);
				this.MyController.Move(base.transform.forward * (this.WalkSpeed * Time.deltaTime));
				return;
			}
		}
		else
		{
			if (this.Stance.Current == StanceType.Crouching)
			{
				this.MyAnimation.CrossFade(this.CrouchIdleAnim);
				return;
			}
			this.MyAnimation.CrossFade(this.IdleAnim);
			return;
		}
	}

	// Token: 0x06001AD8 RID: 6872 RVA: 0x0010D90C File Offset: 0x0010BB0C
	private void MoveTowardsTarget(Vector3 target)
	{
		Vector3 a = target - base.transform.position;
		this.MyController.Move(a * (Time.deltaTime * 10f));
	}

	// Token: 0x06001AD9 RID: 6873 RVA: 0x0010BD54 File Offset: 0x00109F54
	private void SpinTowardsTarget(Quaternion target)
	{
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, target, Time.deltaTime * 10f);
	}

	// Token: 0x04002B6D RID: 11117
	public CharacterController MyController;

	// Token: 0x04002B6E RID: 11118
	public Transform TrellisClimbSpot;

	// Token: 0x04002B6F RID: 11119
	public Transform CameraTarget;

	// Token: 0x04002B70 RID: 11120
	public Transform EntryPOV;

	// Token: 0x04002B71 RID: 11121
	public Transform Hips;

	// Token: 0x04002B72 RID: 11122
	public RPG_Camera RPGCamera;

	// Token: 0x04002B73 RID: 11123
	public Animation MyAnimation;

	// Token: 0x04002B74 RID: 11124
	public AudioSource Jukebox;

	// Token: 0x04002B75 RID: 11125
	public Camera MainCamera;

	// Token: 0x04002B76 RID: 11126
	public bool Climbing;

	// Token: 0x04002B77 RID: 11127
	public bool Running;

	// Token: 0x04002B78 RID: 11128
	public bool CanMove;

	// Token: 0x04002B79 RID: 11129
	public bool Street;

	// Token: 0x04002B7A RID: 11130
	public Stance Stance = new Stance(StanceType.Standing);

	// Token: 0x04002B7B RID: 11131
	public string IdleAnim;

	// Token: 0x04002B7C RID: 11132
	public string WalkAnim;

	// Token: 0x04002B7D RID: 11133
	public string RunAnim;

	// Token: 0x04002B7E RID: 11134
	public string CrouchIdleAnim;

	// Token: 0x04002B7F RID: 11135
	public string CrouchWalkAnim;

	// Token: 0x04002B80 RID: 11136
	public string CrouchRunAnim;

	// Token: 0x04002B81 RID: 11137
	public float WalkSpeed;

	// Token: 0x04002B82 RID: 11138
	public float RunSpeed;

	// Token: 0x04002B83 RID: 11139
	public float CrouchWalkSpeed;

	// Token: 0x04002B84 RID: 11140
	public float CrouchRunSpeed;

	// Token: 0x04002B85 RID: 11141
	public int ClimbPhase;

	// Token: 0x04002B86 RID: 11142
	public int Frame;
}
