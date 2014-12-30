using UnityEngine;
using System.Collections;
using Hack.States;

public class RollingState : ParkourState
{

	private float rollTime;

	public RollingState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();

		rollTime = 0f;
	}

	public override void Update()
	{
		base.Update();

		LeanTween.rotateAround(owner.gameObject, Vector3.forward, -360f, 1.5f * Time.fixedDeltaTime);

		rollTime += Time.fixedDeltaTime;

		if (rollTime > 1f)
		{
			//owner.SetState(new RunningState(owner));
			//return;
		}
	}

	public override void Exit()
	{
		base.Exit();
	}
}
