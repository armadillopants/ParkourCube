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

		rollTime = 0.3f;
	}

	public override void Update()
	{
		base.Update();

		rollTime -= Time.fixedDeltaTime;

		if (rollTime > 0f)
		{
			owner.transform.Rotate(-Vector3.forward, 600f * Time.fixedDeltaTime);
			LeanTween.scaleY(owner.gameObject, 0.5f, 5f * Time.fixedDeltaTime);
		}
		else
		{
			owner.SetState(new RunningState(owner));
			return;
		}

		owner.Move(Vector2.right);
	}

	public override void Exit()
	{
		base.Exit();

		LeanTween.scaleY(owner.gameObject, 0.7f, 5f * Time.fixedDeltaTime);
		owner.transform.eulerAngles = Vector3.zero;
	}
}
