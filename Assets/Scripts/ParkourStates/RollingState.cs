using UnityEngine;
using System.Collections;
using Hack.States;

public class RollingState : ParkourState
{

	public RollingState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();
	}

	public override void Update()
	{
		base.Update();

		LeanTween.rotateZ(owner.gameObject, 360f, 5f * Time.fixedDeltaTime);
	}

	public override void Exit()
	{
		base.Exit();
	}
}
