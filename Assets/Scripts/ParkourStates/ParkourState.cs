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

		return false;
	}

	protected RunningState GetRunState()
	{
		return null;
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
		if (Input.GetKeyDown(KeyCode.Space))
		{
			return new JumpingState(owner);
		}

		return null;
	}
}
