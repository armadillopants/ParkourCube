using UnityEngine;

public class SlideState : ParkourState
{
	private float slideTime;
	private float slideSpeed = 3f;

	public SlideState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();

		slideTime = 0f;

		owner.velocity = Vector2.right;
	}

	public override void Update()
	{
		base.Update();

		slideTime += Time.fixedDeltaTime;

		if (slideTime > 0.3f)
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
