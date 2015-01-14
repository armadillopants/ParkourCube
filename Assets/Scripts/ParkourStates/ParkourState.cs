using Hack.States;
using System;
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
		if (owner.spaceKey.Down())
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
		if (owner.spaceKey.Pressed())
		{
			return new VaultingState(owner);
		}

		return null;
	}

	protected bool TryKongVault()
	{
		KongVaultingState state = GetKongVaultState();

		if (state != null)
		{
			owner.SetState(state);
			return true;
		}

		return false;
	}

	protected KongVaultingState GetKongVaultState()
	{
		if (owner.spaceKey.Pressed())
		{
			return new KongVaultingState(owner);
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
		if (owner.spaceKey.Pressed())
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
		if (owner.sKey.Down())
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
		if (owner.spaceKey.Pressed())
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
		if (owner.spaceKey.Up())
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
		RaycastHit2D hit = Physics2D.Linecast(owner.transform.position, owner.transform.position - new Vector3(0, 0.4f, 0), owner.GetLayerMask());

		if (hit.collider == null)
		{
			return new FallingState(owner);
		}

		return null;
	}

	protected bool TryPullUp()
	{
		PullUpState state = GetPullUpState();

		if (state != null)
		{
			owner.SetState(state);
			return true;
		}

		return false;
	}

	protected PullUpState GetPullUpState()
	{
		if (owner.spaceKey.Down())
		{
			return new PullUpState(owner);
		}

		return null;
	}
}
