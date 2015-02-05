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

		owner.SetParticleActive("Slide");

		LeanTween.delayedSound(owner.slide, Vector3.zero, 1f);
	}

	public override void Update()
	{
		base.Update();

		slideTime += Time.fixedDeltaTime;

		if (slideTime > 0.3f)
		{
			owner.SetState(new RunState(owner));
		}
		else
		{
			LeanTween.scaleY(owner.GetBody(), 0.8f, 0.3f);
		}

		owner.Move(owner.velocity * slideSpeed);

		LeanTween.rotateZ(owner.GetBody(), 85, 0.1f);
	}

	public override void Exit()
	{
		base.Exit();

		owner.SetParticleActive("Slide", false);
	}
}
