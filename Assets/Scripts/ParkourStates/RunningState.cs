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

		if (TryJump())
		{
			return;
		}
	}

	public override void Exit()
	{
		base.Exit();
	}
}
