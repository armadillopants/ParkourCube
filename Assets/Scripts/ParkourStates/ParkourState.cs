using System;
using Hack.States;
using UnityEngine;

public abstract class ParkourState : State
{

	protected Type previousStateType;

	protected Player owner;

	public ParkourState(Player player)
	{
		owner = player;
	}

	public override void Enter()
	{
		base.Enter();
	}

	public override void Update()
	{
		base.Update();
	}

	public override void Exit()
	{
		base.Exit();
	}

	public void SetPreviousState(ParkourState state)
	{
		previousStateType = state.GetType();
	}

	protected bool TryRun()
	{
		RunningState state = GetRunState();

		if (state != null)
		{
			owner.SetState(state);
			return true;
		}

		return false;
	}

	protected RunningState GetRunState()
	{
		return new RunningState(owner);
	}

	protected bool TryJump()
	{
		JumpingState state = GetJumpState();

		if (state != null)
		{
			owner.SetState(state);
			return true;
		}

		return false;
	}

	protected JumpingState GetJumpState()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			return new JumpingState(owner);
		}

		return null;
	}

	protected bool TryVault()
	{
		VaultingState state = GetVaultState();

		if (state != null)
		{
			owner.SetState(state);
			return true;
		}

		return false;
	}

	protected VaultingState GetVaultState()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			return new VaultingState(owner);
		}

		return null;
	}

	protected bool TryWallRun()
	{
		WallRunningState state = GetWallRunState();

		if (state != null)
		{
			owner.SetState(state);
			return true;
		}

		return false;
	}

	protected WallRunningState GetWallRunState()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			return new WallRunningState(owner);
		}

		return null;
	}

	protected bool TrySlide()
	{
		SlidingState state = GetSlideState();

		if (state != null)
		{
			owner.SetState(state);
			return true;
		}

		return false;
	}

	protected SlidingState GetSlideState()
	{
		if (Input.GetKey(KeyCode.S))
		{
			return new SlidingState(owner);
		}

		return null;
	}

	protected bool TryRoll()
	{
		RollingState state = GetRollState();

		if (state != null)
		{
			owner.SetState(state);
			return true;
		}

		return false;
	}

	protected RollingState GetRollState()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			return new RollingState(owner);
		}

		return null;
	}

	protected bool TryHang()
	{
		HangingState state = GetHangState();

		if (state != null)
		{
			owner.SetState(state);
			return true;
		}

		return false;
	}

	protected HangingState GetHangState()
	{
		if (Input.GetKeyUp(KeyCode.Space))
		{
			return new HangingState(owner);
		}

		return null;
	}

	protected bool TryFall()
	{
		FallingState state = GetFallState();

		if (state != null)
		{
			owner.SetState(state);
			return true;
		}

		return false;
	}

	protected FallingState GetFallState()
	{
		return new FallingState(owner);
	}

	protected bool TryTransitionToWall()
	{
		TransitionToWallState state = GetTransitionState();

		if (state != null)
		{
			owner.SetState(state);
			return true;
		}

		return false;
	}

	protected TransitionToWallState GetTransitionState()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			return new TransitionToWallState(owner);
		}

		return null;
	}
}
