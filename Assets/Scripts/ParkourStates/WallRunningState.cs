using UnityEngine;
using System.Collections;
using Hack.States;

public class WallRunningState : ParkourState
{
	private float wallRunTime;

	public WallRunningState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();

		wallRunTime = 0f;
	}

	public override void Update()
	{
		base.Update();

		if (TryHang()) { return; }

		wallRunTime += Time.fixedDeltaTime;

		if (wallRunTime > 0.5f)
		{
			owner.SetState(new HangingState(owner));
		}

		owner.Move(Vector2.up);

		if (Physics2D.Linecast(owner.transform.position, owner.rightWallCheck.position, 1 << LayerMask.NameToLayer("Wall")))
		{
			LeanTween.rotateZ(owner.gameObject, 45, 5f * Time.fixedDeltaTime);
		}

		if (Physics2D.Linecast(owner.transform.position, owner.leftWallCheck.position, 1 << LayerMask.NameToLayer("Wall")))
		{
			LeanTween.rotateZ(owner.gameObject, -45, 5f * Time.fixedDeltaTime);
		}
	}

	public override void Exit()
	{
		base.Exit();
	}
}
