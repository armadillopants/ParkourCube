using UnityEngine;
using System.Collections;
using Hack.States;

public class FallingState : ParkourState
{
	private Vector2 velocity;
	private float gravity = 6f;

	private Vector3 lastPosition;

	public FallingState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();

		lastPosition = owner.transform.position;

		Debug.Log("Entered");

		velocity = Vector2.right;
	}

	public override void Update()
	{
		base.Update();

		velocity.y -= gravity * Time.fixedDeltaTime;

		if (Physics2D.Linecast(owner.transform.position, owner.transform.position - new Vector3(0, 0.4f, 0), 1 << LayerMask.NameToLayer("Ground")))
		{
			if (Vector2.Distance(owner.transform.position, lastPosition) > 3f)
			{
				if (TryRoll()) { return; } else { Debug.Log("Dead"); return; }
			}

			owner.SetState(new RunningState(owner));
			return;
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
