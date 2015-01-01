using UnityEngine;
using System.Collections;
using Hack.States;

public class RunningState : ParkourState
{
	public RunningState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();

		owner.velocity = Vector2.right;
	}

	public override void Update()
	{
		base.Update();

		owner.Move(owner.velocity);

		if (TryJump()) { return; }
		if (TrySlide()) { return; }

		RaycastHit2D hit = Physics2D.Linecast(owner.transform.position, owner.transform.position - new Vector3(0, 0.4f, 0), owner.GetLayerMask());

		if (hit.collider == null)
		{
			if (TryFall()) { return; }
		}

		if (owner.transform.eulerAngles != Vector3.zero)
		{
			LeanTween.rotateZ(owner.gameObject, 0f, 5f * Time.fixedDeltaTime);
		}
	}

	public override void Exit()
	{
		base.Exit();
	}
}
