using UnityEngine;
using System.Collections;
using Hack.States;

public class JumpingState : ParkourState
{
	private Vector2 velocity;
	private float gravity = 6f;
	private float jumpStrength = 5.5f;
	private float jumpTime;

	public JumpingState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();

		jumpTime = 0f;
		velocity = Vector2.right;

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

		if (Physics2D.Linecast(owner.transform.position, owner.groundCheck.position, 1 << LayerMask.NameToLayer("Vault")))
		{
			if (TryVault()) { return;  }
		}

		if (Physics2D.Linecast(owner.transform.position, owner.rightWallCheck.position, 1 << LayerMask.NameToLayer("Wall")))
		{
			if (TryWallRun()) { return; }
		}

		jumpTime += Time.fixedDeltaTime;

		if (jumpTime > 0.2f)
		{
			velocity.y -= gravity * Time.fixedDeltaTime;

			RaycastHit2D hit;
			if (Physics2D.Linecast(owner.transform.position, owner.groundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
			{
				owner.transform.position = hit.point;
				owner.SetState(new RunningState(owner));
				return;
			}
		}
		else
		{
			velocity.y += jumpStrength * Time.fixedDeltaTime;
		}

		owner.Move(velocity);
	}

	public override void Exit()
	{
		base.Exit();
	}
}
