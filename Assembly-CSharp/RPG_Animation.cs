using System;
using UnityEngine;

// Token: 0x020000B9 RID: 185
public class RPG_Animation : MonoBehaviour
{
	// Token: 0x060009B4 RID: 2484 RVA: 0x0004B837 File Offset: 0x00049A37
	private void Awake()
	{
		RPG_Animation.instance = this;
	}

	// Token: 0x060009B5 RID: 2485 RVA: 0x0004B83F File Offset: 0x00049A3F
	private void Update()
	{
		this.SetCurrentState();
		this.StartAnimation();
	}

	// Token: 0x060009B6 RID: 2486 RVA: 0x0004B850 File Offset: 0x00049A50
	public void SetCurrentMoveDir(Vector3 playerDir)
	{
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		if (playerDir.z > 0f)
		{
			flag = true;
		}
		if (playerDir.z < 0f)
		{
			flag2 = true;
		}
		if (playerDir.x < 0f)
		{
			flag3 = true;
		}
		if (playerDir.x > 0f)
		{
			flag4 = true;
		}
		if (flag)
		{
			if (flag3)
			{
				this.currentMoveDir = RPG_Animation.CharacterMoveDirection.StrafeForwardLeft;
				return;
			}
			if (flag4)
			{
				this.currentMoveDir = RPG_Animation.CharacterMoveDirection.StrafeForwardRight;
				return;
			}
			this.currentMoveDir = RPG_Animation.CharacterMoveDirection.Forward;
			return;
		}
		else if (flag2)
		{
			if (flag3)
			{
				this.currentMoveDir = RPG_Animation.CharacterMoveDirection.StrafeBackLeft;
				return;
			}
			if (flag4)
			{
				this.currentMoveDir = RPG_Animation.CharacterMoveDirection.StrafeBackRight;
				return;
			}
			this.currentMoveDir = RPG_Animation.CharacterMoveDirection.Backward;
			return;
		}
		else
		{
			if (flag3)
			{
				this.currentMoveDir = RPG_Animation.CharacterMoveDirection.StrafeLeft;
				return;
			}
			if (flag4)
			{
				this.currentMoveDir = RPG_Animation.CharacterMoveDirection.StrafeRight;
				return;
			}
			this.currentMoveDir = RPG_Animation.CharacterMoveDirection.None;
			return;
		}
	}

	// Token: 0x060009B7 RID: 2487 RVA: 0x0004B900 File Offset: 0x00049B00
	public void SetCurrentState()
	{
		if (RPG_Controller.instance.characterController.isGrounded)
		{
			switch (this.currentMoveDir)
			{
			case RPG_Animation.CharacterMoveDirection.None:
				this.currentState = RPG_Animation.CharacterState.Idle;
				return;
			case RPG_Animation.CharacterMoveDirection.Forward:
				this.currentState = RPG_Animation.CharacterState.Walk;
				return;
			case RPG_Animation.CharacterMoveDirection.Backward:
				this.currentState = RPG_Animation.CharacterState.WalkBack;
				return;
			case RPG_Animation.CharacterMoveDirection.StrafeLeft:
				this.currentState = RPG_Animation.CharacterState.StrafeLeft;
				return;
			case RPG_Animation.CharacterMoveDirection.StrafeRight:
				this.currentState = RPG_Animation.CharacterState.StrafeRight;
				break;
			case RPG_Animation.CharacterMoveDirection.StrafeForwardLeft:
				this.currentState = RPG_Animation.CharacterState.Walk;
				return;
			case RPG_Animation.CharacterMoveDirection.StrafeForwardRight:
				this.currentState = RPG_Animation.CharacterState.Walk;
				return;
			case RPG_Animation.CharacterMoveDirection.StrafeBackLeft:
				this.currentState = RPG_Animation.CharacterState.WalkBack;
				return;
			case RPG_Animation.CharacterMoveDirection.StrafeBackRight:
				this.currentState = RPG_Animation.CharacterState.WalkBack;
				return;
			default:
				return;
			}
		}
	}

	// Token: 0x060009B8 RID: 2488 RVA: 0x0004B998 File Offset: 0x00049B98
	public void StartAnimation()
	{
		switch (this.currentState)
		{
		case RPG_Animation.CharacterState.Idle:
			this.Idle();
			return;
		case RPG_Animation.CharacterState.Walk:
			if (this.currentMoveDir == RPG_Animation.CharacterMoveDirection.StrafeForwardLeft)
			{
				this.StrafeForwardLeft();
				return;
			}
			if (this.currentMoveDir == RPG_Animation.CharacterMoveDirection.StrafeForwardRight)
			{
				this.StrafeForwardRight();
				return;
			}
			this.Walk();
			return;
		case RPG_Animation.CharacterState.WalkBack:
			if (this.currentMoveDir == RPG_Animation.CharacterMoveDirection.StrafeBackLeft)
			{
				this.StrafeBackLeft();
				return;
			}
			if (this.currentMoveDir == RPG_Animation.CharacterMoveDirection.StrafeBackRight)
			{
				this.StrafeBackRight();
				return;
			}
			this.WalkBack();
			return;
		case RPG_Animation.CharacterState.StrafeLeft:
			this.StrafeLeft();
			return;
		case RPG_Animation.CharacterState.StrafeRight:
			this.StrafeRight();
			return;
		default:
			return;
		}
	}

	// Token: 0x060009B9 RID: 2489 RVA: 0x0004BA29 File Offset: 0x00049C29
	private void Idle()
	{
		base.GetComponent<Animation>().CrossFade("idle");
	}

	// Token: 0x060009BA RID: 2490 RVA: 0x0004BA3B File Offset: 0x00049C3B
	private void Walk()
	{
		base.GetComponent<Animation>().CrossFade("walk");
	}

	// Token: 0x060009BB RID: 2491 RVA: 0x0004BA4D File Offset: 0x00049C4D
	private void StrafeForwardLeft()
	{
		base.GetComponent<Animation>().CrossFade("strafeforwardleft");
	}

	// Token: 0x060009BC RID: 2492 RVA: 0x0004BA5F File Offset: 0x00049C5F
	private void StrafeForwardRight()
	{
		base.GetComponent<Animation>().CrossFade("strafeforwardright");
	}

	// Token: 0x060009BD RID: 2493 RVA: 0x0004BA71 File Offset: 0x00049C71
	private void WalkBack()
	{
		base.GetComponent<Animation>().CrossFade("walkback");
	}

	// Token: 0x060009BE RID: 2494 RVA: 0x0004BA83 File Offset: 0x00049C83
	private void StrafeBackLeft()
	{
		base.GetComponent<Animation>().CrossFade("strafebackleft");
	}

	// Token: 0x060009BF RID: 2495 RVA: 0x0004BA95 File Offset: 0x00049C95
	private void StrafeBackRight()
	{
		base.GetComponent<Animation>().CrossFade("strafebackright");
	}

	// Token: 0x060009C0 RID: 2496 RVA: 0x0004BAA7 File Offset: 0x00049CA7
	private void StrafeLeft()
	{
		base.GetComponent<Animation>().CrossFade("strafeleft");
	}

	// Token: 0x060009C1 RID: 2497 RVA: 0x0004BAB9 File Offset: 0x00049CB9
	private void StrafeRight()
	{
		base.GetComponent<Animation>().CrossFade("straferight");
	}

	// Token: 0x060009C2 RID: 2498 RVA: 0x0004BACB File Offset: 0x00049CCB
	public void Jump()
	{
		this.currentState = RPG_Animation.CharacterState.Jump;
		if (base.GetComponent<Animation>().IsPlaying("jump"))
		{
			base.GetComponent<Animation>().Stop("jump");
		}
		base.GetComponent<Animation>().CrossFade("jump");
	}

	// Token: 0x0400081C RID: 2076
	public static RPG_Animation instance;

	// Token: 0x0400081D RID: 2077
	public RPG_Animation.CharacterMoveDirection currentMoveDir;

	// Token: 0x0400081E RID: 2078
	public RPG_Animation.CharacterState currentState;

	// Token: 0x02000693 RID: 1683
	public enum CharacterMoveDirection
	{
		// Token: 0x04004602 RID: 17922
		None,
		// Token: 0x04004603 RID: 17923
		Forward,
		// Token: 0x04004604 RID: 17924
		Backward,
		// Token: 0x04004605 RID: 17925
		StrafeLeft,
		// Token: 0x04004606 RID: 17926
		StrafeRight,
		// Token: 0x04004607 RID: 17927
		StrafeForwardLeft,
		// Token: 0x04004608 RID: 17928
		StrafeForwardRight,
		// Token: 0x04004609 RID: 17929
		StrafeBackLeft,
		// Token: 0x0400460A RID: 17930
		StrafeBackRight
	}

	// Token: 0x02000694 RID: 1684
	public enum CharacterState
	{
		// Token: 0x0400460C RID: 17932
		Idle,
		// Token: 0x0400460D RID: 17933
		Walk,
		// Token: 0x0400460E RID: 17934
		WalkBack,
		// Token: 0x0400460F RID: 17935
		StrafeLeft,
		// Token: 0x04004610 RID: 17936
		StrafeRight,
		// Token: 0x04004611 RID: 17937
		Jump
	}
}
