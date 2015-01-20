using UnityEngine;

public class SlideState : ParkourState
{
	private float slideTime;
	private float maxSlideTime = 0.3f;
	private float slideSpeed = 3f;

	public SlideState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();

		slideTime = 0f;
	}

	public override void Update()
	{
		base.Update();

		if (TryJump()) { return; }

		slideTime += Time.fixedDeltaTime;

		if (slideTime > maxSlideTime)
		{
			owner.SetState(new RunState(owner));
		}

		owner.Move(owner.velocity * slideSpeed);

		LeanTween.rotateZ(owner.GetBody(), 85, 5f * Time.fixedDeltaTime);
	}

	public override void Exit()
	{
		base.Exit();
	}
}
