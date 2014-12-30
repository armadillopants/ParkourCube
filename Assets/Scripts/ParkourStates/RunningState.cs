using UnityEngine;
using System.Collections;
using Hack.States;

public class RunningState : ParkourState
{
	public RunningState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();
	}

	public override void Update()
	{
		base.Update();

		owner.Move(Vector2.right);

		if (TryJump()) { return; }
		if (TrySlide()) { return; }

		if (!Physics2D.Linecast(owner.transform.position, owner.groundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
		{
			if (TryFall()) { return; }
		}
	}

	public override void Exit()
	{
		base.Exit();
	}
}
