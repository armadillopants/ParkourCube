using UnityEngine;
using System.Collections;
using Hack.States;

public class SlidingState : ParkourState
{
	private float slideTime;
	private float slideSpeed = 2f;

	public SlidingState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();

		slideTime = 0f;
		owner.velocity = Vector2.right;
	}

	public override void Update()
	{
		base.Update();

		slideTime += Time.fixedDeltaTime;

		if (slideTime > 0.3f)
		{
			if (TryRun()) { return; }
		}

		owner.Move(owner.velocity * slideSpeed);
		LeanTween.rotateZ(owner.gameObject, 45, 5f * Time.fixedDeltaTime);
	}

	public override void Exit()
	{
		base.Exit();
	}
}
