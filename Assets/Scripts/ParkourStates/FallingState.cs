using UnityEngine;
using System.Collections;
using Hack.States;

public class FallingState : ParkourState
{
	private Vector2 velocity;
	private float gravity = 6f;

	public FallingState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();

		velocity = Vector2.right;
	}

	public override void Update()
	{
		base.Update();

		velocity.y -= gravity * Time.fixedDeltaTime;

		if (Physics2D.Linecast(owner.transform.position, owner.groundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
		{
			owner.SetState(new RunningState(owner));
			return;
		}

		owner.Move(velocity);
	}

	public override void Exit()
	{
		base.Exit();
	}
}
