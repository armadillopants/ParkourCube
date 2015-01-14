using UnityEngine;

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

		if (TryJump()) { return; }

		slideTime += Time.fixedDeltaTime;

		if (slideTime > 0.3f)
		{
			owner.SetState(new RunningState(owner));
		}

		owner.Move(owner.velocity * slideSpeed);

		LeanTween.rotateZ(owner.gameObject, 65, 5f * Time.fixedDeltaTime);
	}

	public override void Exit()
	{
		base.Exit();
	}
}
