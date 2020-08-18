﻿using System;
using UnityEngine;

// Token: 0x020000BC RID: 188
public class RPG_Controller : MonoBehaviour
{
	// Token: 0x060009D5 RID: 2517 RVA: 0x0004C973 File Offset: 0x0004AB73
	private void Awake()
	{
		RPG_Controller.instance = this;
		this.characterController = (base.GetComponent("CharacterController") as CharacterController);
		RPG_Camera.CameraSetup();
		this.MainCamera = Camera.main;
	}

	// Token: 0x060009D6 RID: 2518 RVA: 0x0004C9A1 File Offset: 0x0004ABA1
	private void Update()
	{
		if (this.MainCamera == null)
		{
			return;
		}
		if (this.characterController == null)
		{
			Debug.Log("Error: No Character Controller component found! Please add one to the GameObject which has this script attached.");
			return;
		}
		this.GetInput();
		this.StartMotor();
	}

	// Token: 0x060009D7 RID: 2519 RVA: 0x0004C9D8 File Offset: 0x0004ABD8
	private void GetInput()
	{
		float d = 0f;
		float d2 = 0f;
		if (Input.GetButton("Horizontal Strafe"))
		{
			d = ((Input.GetAxis("Horizontal Strafe") < 0f) ? -1f : ((Input.GetAxis("Horizontal Strafe") > 0f) ? 1f : 0f));
		}
		if (Input.GetButton("Vertical"))
		{
			d2 = ((Input.GetAxis("Vertical") < 0f) ? -1f : ((Input.GetAxis("Vertical") > 0f) ? 1f : 0f));
		}
		if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
		{
			d2 = 1f;
		}
		this.playerDir = d * Vector3.right + d2 * Vector3.forward;
		if (RPG_Animation.instance != null)
		{
			RPG_Animation.instance.SetCurrentMoveDir(this.playerDir);
		}
		if (this.characterController.isGrounded)
		{
			this.playerDirWorld = base.transform.TransformDirection(this.playerDir);
			if (Mathf.Abs(this.playerDir.x) + Mathf.Abs(this.playerDir.z) > 1f)
			{
				this.playerDirWorld.Normalize();
			}
			this.playerDirWorld *= this.walkSpeed;
			this.playerDirWorld.y = this.fallingThreshold;
			if (Input.GetButtonDown("Jump"))
			{
				this.playerDirWorld.y = this.jumpHeight;
				if (RPG_Animation.instance != null)
				{
					RPG_Animation.instance.Jump();
				}
			}
		}
		this.rotation.y = Input.GetAxis("Horizontal") * this.turnSpeed;
	}

	// Token: 0x060009D8 RID: 2520 RVA: 0x0004CB9C File Offset: 0x0004AD9C
	private void StartMotor()
	{
		this.playerDirWorld.y = this.playerDirWorld.y - this.gravity * Time.deltaTime;
		this.characterController.Move(this.playerDirWorld * Time.deltaTime);
		base.transform.Rotate(this.rotation);
		if (!Input.GetMouseButton(0))
		{
			RPG_Camera.instance.RotateWithCharacter();
		}
	}

	// Token: 0x04000841 RID: 2113
	public static RPG_Controller instance;

	// Token: 0x04000842 RID: 2114
	public CharacterController characterController;

	// Token: 0x04000843 RID: 2115
	public float walkSpeed = 10f;

	// Token: 0x04000844 RID: 2116
	public float turnSpeed = 2.5f;

	// Token: 0x04000845 RID: 2117
	public float jumpHeight = 10f;

	// Token: 0x04000846 RID: 2118
	public float gravity = 20f;

	// Token: 0x04000847 RID: 2119
	public float fallingThreshold = -6f;

	// Token: 0x04000848 RID: 2120
	private Vector3 playerDir;

	// Token: 0x04000849 RID: 2121
	private Vector3 playerDirWorld;

	// Token: 0x0400084A RID: 2122
	private Vector3 rotation = Vector3.zero;

	// Token: 0x0400084B RID: 2123
	private Camera MainCamera;
}
