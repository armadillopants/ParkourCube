using UnityEngine;

public class ReboundState : ParkourState
{

	private Vector2 hangPos;
	private float hangTime;

	public ReboundState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();

		hangPos = owner.transform.position;
		hangTime = 0f;
		owner.SetGravity(0f);
	}

	public override void Update()
	{
		base.Update();

		if (TryJump()) { return; }

		hangTime += Time.fixedDeltaTime;

		if (hangTime > 0.2f)
		{
			owner.SetState(new FallState(owner));
		}

		owner.transform.position = hangPos;

	}

	public override void Exit()
	{
		base.Exit();
	}
}
