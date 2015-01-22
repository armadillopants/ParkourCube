using UnityEngine;

public class RollState : ParkourState
{

	private float rollTime;

	public RollState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();

		rollTime = 0.3f;

		owner.velocity = Vector2.right;
	}

	public override void Update()
	{
		base.Update();

		rollTime -= Time.fixedDeltaTime;

		if (rollTime > 0f)
		{
			owner.GetBody().transform.Rotate(-Vector3.forward, 600f * Time.fixedDeltaTime);
			LeanTween.scaleY(owner.GetBody(), 0.5f, 5f * Time.fixedDeltaTime);
		}
		else
		{
			owner.SetState(new RunState(owner));
			return;
		}

		owner.Move(owner.velocity);
	}

	public override void Exit()
	{
		base.Exit();

		LeanTween.scaleY(owner.GetBody(), 1f, 5f * Time.fixedDeltaTime);
		owner.GetBody().transform.eulerAngles = Vector3.zero;
	}
}
