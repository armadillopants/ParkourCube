using UnityEngine;
using System.Collections;
using Hack.States;

public class HangingState : ParkourState
{
	private Vector2 hangPos;
	private float hangTime;

	public HangingState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();

		hangTime = 0f;
		hangPos = owner.transform.position;
	}

	public override void Update()
	{
		base.Update();

		if (TryJump()) { return; }

		hangTime += Time.fixedDeltaTime;

		if (hangTime > 0.2f)
		{
			if (TryFall()) { return; }
		}

		owner.transform.position = hangPos;
	}

	public override void Exit()
	{
		base.Exit();
	}
}
