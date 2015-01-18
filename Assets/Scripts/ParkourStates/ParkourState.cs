﻿using Hack.States;
using UnityEngine;
using System;
using System.Collections.Generic;

public enum ParkourStateType
{
	None,
	Fall,
	Rebound,
	Hold,
	Jump,
	PullUp,
	Roll,
	Run,
	Slide,
	Vault,
	Climb
}

public abstract class ParkourState : State
{
	private static Dictionary<ParkourStateType, Func<Player, ParkourState>> stateCreators = new Dictionary<ParkourStateType, Func<Player, ParkourState>>()
	{
		{ ParkourStateType.None, (Player p) => { return null; } },
		{ ParkourStateType.Fall, (Player p) => { return new FallState(p); } },
		{ ParkourStateType.Rebound, (Player p) => { return new ReboundState(p); } },
		{ ParkourStateType.Hold, (Player p) => { return new HoldOnState(p); } },
		{ ParkourStateType.Jump, (Player p) => { return new JumpState(p); } },
		{ ParkourStateType.PullUp, (Player p) => { return new PullUpState(p); } },
		{ ParkourStateType.Roll, (Player p) => { return new RollState(p); } },
		{ ParkourStateType.Run, (Player p) => { return new RunState(p); } },
		{ ParkourStateType.Slide, (Player p) => { return new SlideState(p); } },
		{ ParkourStateType.Vault, (Player p) => { return new VaultState(p); } },
		{ ParkourStateType.Climb, (Player p) => { return new ClimbState(p); } },
	};

	public static ParkourState CreateInstance(ParkourStateType type, Player player, bool autoApply = false)
	{
		if(type == ParkourStateType.None) { return null; }
		ParkourState state = stateCreators[type](player);
		if(autoApply) { player.SetState(state); }
		return state;
	}

	protected Player owner;

	public ParkourState(Player player)
	{
		owner = player;
	}

	protected bool TryJump()
	{
		JumpState state = GetJumpState();

		if (state != null)
		{
			owner.SetState(state);
			return true;
		}

		return false;
	}

	protected JumpState GetJumpState()
	{
		if (InputMonitor.Instance.Swipe(Vector2.up))
		{
			return new JumpState(owner);
		}

		return null;
	}

	protected bool TrySlide()
	{
		SlideState state = GetSlideState();

		if (state != null)
		{
			owner.SetState(state);
			return true;
		}

		return false;
	}

	protected SlideState GetSlideState()
	{
		if (InputMonitor.Instance.Swipe(-Vector2.up))
		{
			return new SlideState(owner);
		}

		return null;
	}

	protected bool TryRoll()
	{
		RollState state = GetRollState();

		if (state != null)
		{
			owner.SetState(state);
			return true;
		}

		return false;
	}

	protected RollState GetRollState()
	{
		if (InputMonitor.Instance.Swipe(-Vector2.up))
		{
			return new RollState(owner);
		}

		return null;
	}

	protected bool TryFall()
	{
		FallState state = GetFallState();

		if (state != null)
		{
			owner.SetState(state);
			return true;
		}

		return false;
	}

	protected FallState GetFallState()
	{
		RaycastHit2D hit = Physics2D.Linecast(owner.transform.position, owner.transform.position - new Vector3(0, 0.6f, 0), owner.GetLayerMask());

		if (hit.collider == null)
		{
			return new FallState(owner);
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
		if (InputMonitor.Instance.DidTap())
		{
			return new PullUpState(owner);
		}

		return null;
	}
}
