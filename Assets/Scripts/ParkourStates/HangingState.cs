using UnityEngine;

public class HangingState : ParkourState
{
	private Vector2 hangPos;
	private float hangTime;

	public HangingState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();

		hangTime = 0f;
		hangPos = owner.transform.position;

		if (!Physics2D.Linecast(owner.transform.position, owner.transform.position + new Vector3(0.5f, 0.2f, 0), owner.GetLayerMask()))
		{
			if (TryRun()) { return; }
		}
	}

	public override void Update()
	{
		base.Update();

		if (TryJump()) { return; }

		hangTime += Time.fixedDeltaTime;

		if (hangTime > 0.2f)
		{
			owner.SetState(new FallingState(owner));
		}

		owner.transform.position = hangPos;
	}

	public override void Exit()
	{
		base.Exit();

		if (Physics2D.Linecast(owner.transform.position, owner.transform.position + new Vector3(0.5f, 0.2f, 0), owner.GetLayerMask()))
		{
			owner.velocity = -Vector2.right;
		}

		if (Physics2D.Linecast(owner.transform.position, owner.transform.position + new Vector3(-0.5f, 0.2f, 0), owner.GetLayerMask()))
		{
			owner.velocity = Vector2.right;
		}
	}
}
