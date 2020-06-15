using System;
using UnityEngine;

// Token: 0x020000B9 RID: 185
public class RPG_Animation : MonoBehaviour
{
	// Token: 0x060009B5 RID: 2485 RVA: 0x0004B85F File Offset: 0x00049A5F
	private void Awake()
	{
		RPG_Animation.instance = this;
	}

	// Token: 0x060009B6 RID: 2486 RVA: 0x0004B867 File Offset: 0x00049A67
	private void Update()
	{
		this.SetCurrentState();
		this.StartAnimation();
	}

	// Token: 0x060009B7 RID: 2487 RVA: 0x0004B878 File Offset: 0x00049A78
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

	// Token: 0x060009B8 RID: 2488 RVA: 0x0004B928 File Offset: 0x00049B28
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

	// Token: 0x060009B9 RID: 2489 RVA: 0x0004B9C0 File Offset: 0x00049BC0
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

	// Token: 0x060009BA RID: 2490 RVA: 0x0004BA51 File Offset: 0x00049C51
	private void Idle()
	{
		base.GetComponent<Animation>().CrossFade("idle");
	}

	// Token: 0x060009BB RID: 2491 RVA: 0x0004BA63 File Offset: 0x00049C63
	private void Walk()
	{
		base.GetComponent<Animation>().CrossFade("walk");
	}

	// Token: 0x060009BC RID: 2492 RVA: 0x0004BA75 File Offset: 0x00049C75
	private void StrafeForwardLeft()
	{
		base.GetComponent<Animation>().CrossFade("strafeforwardleft");
	}

	// Token: 0x060009BD RID: 2493 RVA: 0x0004BA87 File Offset: 0x00049C87
	private void StrafeForwardRight()
	{
		base.GetComponent<Animation>().CrossFade("strafeforwardright");
	}

	// Token: 0x060009BE RID: 2494 RVA: 0x0004BA99 File Offset: 0x00049C99
	private void WalkBack()
	{
		base.GetComponent<Animation>().CrossFade("walkback");
	}

	// Token: 0x060009BF RID: 2495 RVA: 0x0004BAAB File Offset: 0x00049CAB
	private void StrafeBackLeft()
	{
		base.GetComponent<Animation>().CrossFade("strafebackleft");
	}

	// Token: 0x060009C0 RID: 2496 RVA: 0x0004BABD File Offset: 0x00049CBD
	private void StrafeBackRight()
	{
		base.GetComponent<Animation>().CrossFade("strafebackright");
	}

	// Token: 0x060009C1 RID: 2497 RVA: 0x0004BACF File Offset: 0x00049CCF
	private void StrafeLeft()
	{
		base.GetComponent<Animation>().CrossFade("strafeleft");
	}

	// Token: 0x060009C2 RID: 2498 RVA: 0x0004BAE1 File Offset: 0x00049CE1
	private void StrafeRight()
	{
		base.GetComponent<Animation>().CrossFade("straferight");
	}

	// Token: 0x060009C3 RID: 2499 RVA: 0x0004BAF3 File Offset: 0x00049CF3
	public void Jump()
	{
		this.currentState = RPG_Animation.CharacterState.Jump;
		if (base.GetComponent<Animation>().IsPlaying("jump"))
		{
			base.GetComponent<Animation>().Stop("jump");
		}
		base.GetComponent<Animation>().CrossFade("jump");
	}

	// Token: 0x0400081E RID: 2078
	public static RPG_Animation instance;

	// Token: 0x0400081F RID: 2079
	public RPG_Animation.CharacterMoveDirection currentMoveDir;

	// Token: 0x04000820 RID: 2080
	public RPG_Animation.CharacterState currentState;

	// Token: 0x020006A9 RID: 1705
	public enum CharacterMoveDirection
	{
		// Token: 0x040046E0 RID: 18144
		None,
		// Token: 0x040046E1 RID: 18145
		Forward,
		// Token: 0x040046E2 RID: 18146
		Backward,
		// Token: 0x040046E3 RID: 18147
		StrafeLeft,
		// Token: 0x040046E4 RID: 18148
		StrafeRight,
		// Token: 0x040046E5 RID: 18149
		StrafeForwardLeft,
		// Token: 0x040046E6 RID: 18150
		StrafeForwardRight,
		// Token: 0x040046E7 RID: 18151
		StrafeBackLeft,
		// Token: 0x040046E8 RID: 18152
		StrafeBackRight
	}

	// Token: 0x020006AA RID: 1706
	public enum CharacterState
	{
		// Token: 0x040046EA RID: 18154
		Idle,
		// Token: 0x040046EB RID: 18155
		Walk,
		// Token: 0x040046EC RID: 18156
		WalkBack,
		// Token: 0x040046ED RID: 18157
		StrafeLeft,
		// Token: 0x040046EE RID: 18158
		StrafeRight,
		// Token: 0x040046EF RID: 18159
		Jump
	}
}
