using UnityEngine;
using System.Collections;
using Hack.States;

public class JumpingState : ParkourState
{
	private Vector2 velocity;
	private float gravity = 6f;
	private float jumpStrength = 6f;
	private float jumpTime;

	private float castLineTime;

	public JumpingState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();

		jumpTime = 0f;
		castLineTime = 0f;
		velocity = Vector2.right;

		if (Physics2D.Linecast(owner.transform.position, owner.transform.position + new Vector3(0.5f, 0, 0), 1 << LayerMask.NameToLayer("Wall")))
		{
			velocity = -Vector2.right;
		}

		if (Physics2D.Linecast(owner.transform.position, owner.transform.position - new Vector3(0.5f, 0, 0), 1 << LayerMask.NameToLayer("Wall")))
		{
			velocity = Vector2.right;
		}
	}

	public override void Update()
	{
		base.Update();

		castLineTime += Time.fixedDeltaTime;

		if (castLineTime > 0.1f)
		{
			if (Physics2D.Linecast(owner.transform.position, owner.transform.position + new Vector3(0.5f, -0.2f, 0), 1 << LayerMask.NameToLayer("Vault")))
			{
				if (TryVault()) { return; }
			}

			if (Physics2D.Linecast(owner.transform.position, owner.transform.position + new Vector3(0.5f, 0, 0), 1 << LayerMask.NameToLayer("Wall")))
			{
				if (TryWallRun()) { return; }
			}

			if (Physics2D.Linecast(owner.transform.position, owner.transform.position - new Vector3(0.5f, 0, 0), 1 << LayerMask.NameToLayer("Wall")))
			{
				if (TryWallRun()) { return; }
			}
		}

		jumpTime += Time.fixedDeltaTime;

		if (jumpTime > 0.2f)
		{
			velocity.y -= gravity * Time.fixedDeltaTime;

			if(Physics2D.Linecast(owner.transform.position, owner.transform.position - new Vector3(0, 0.4f, 0), 1 << LayerMask.NameToLayer("Ground")))
			{
				owner.SetState(new RunningState(owner));
				return;
			}
		}
		else
		{
			velocity.y += jumpStrength * Time.fixedDeltaTime;
		}

		owner.Move(velocity);

		if (owner.transform.eulerAngles != Vector3.zero)
		{
			LeanTween.rotateZ(owner.gameObject, 0, 5f * Time.fixedDeltaTime);
		}
	}

	public override void Exit()
	{
		base.Exit();
	}
}
