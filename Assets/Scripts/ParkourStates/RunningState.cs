using UnityEngine;
using System.Collections;
using Hack.States;

public class RunningState : ParkourState
{

	private float moveSpeed;
	private Vector2 direction;

	public RunningState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();

		direction = Vector2.right;
		moveSpeed = 6f;
	}

	public override void Update()
	{
		base.Update();

		owner.rigidbody2D.position += (Vector2)owner.transform.TransformDirection(direction) * moveSpeed * Time.fixedDeltaTime;

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
