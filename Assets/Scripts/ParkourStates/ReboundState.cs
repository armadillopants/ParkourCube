using UnityEngine;

public class ReboundState : ParkourState
{

	private float reboundTime;
	private float jumpStrength = 6f;

	public ReboundState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();

		reboundTime = 0f;
	}

	public override void Update()
	{
		base.Update();

		reboundTime += Time.fixedDeltaTime;

		if (reboundTime > 0.3f)
		{
			owner.SetState(new RunState(owner));
			return;
		}
		else
		{
			owner.velocity.y += jumpStrength * Time.fixedDeltaTime;
		}

		owner.Move(owner.velocity);
	}

	public override void Exit()
	{
		base.Exit();
	}
}
