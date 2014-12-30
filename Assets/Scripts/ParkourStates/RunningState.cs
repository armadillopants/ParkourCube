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

		if (!Physics2D.Linecast(owner.transform.position, owner.transform.position - new Vector3(0, 0.4f, 0), 1 << LayerMask.NameToLayer("Ground")))
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
