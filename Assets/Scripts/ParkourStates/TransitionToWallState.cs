using UnityEngine;
using System.Collections;
using Hack.States;

public class TransitionToWallState : ParkourState
{
	private Vector2 velocity;
	private float gravity = 1f;

	public TransitionToWallState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();

		if (Physics2D.Linecast(owner.transform.position, owner.rightWallCheck.position, 1 << LayerMask.NameToLayer("Wall")))
		{
			velocity = -Vector2.right;
		}

		if (Physics2D.Linecast(owner.transform.position, owner.leftWallCheck.position, 1 << LayerMask.NameToLayer("Wall")))
		{
			velocity = Vector2.right;
		}
	}

	public override void Update()
	{
		base.Update();

		if (Physics2D.Linecast(owner.transform.position, owner.leftWallCheck.position, 1 << LayerMask.NameToLayer("Wall")))
		{
			if (TryWallRun()) { return; }
		}

		velocity.y -= gravity * Time.fixedDeltaTime;

		owner.Move(velocity);
	}

	public override void Exit()
	{
		base.Exit();
	}
}
