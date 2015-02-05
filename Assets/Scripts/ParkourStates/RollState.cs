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

		owner.SetParticleActive("Roll");

		LeanTween.delayedSound(owner.roll, owner.transform.position, 1f);
	}

	public override void Update()
	{
		base.Update();

		rollTime -= Time.fixedDeltaTime;

		if (rollTime > 0f)
		{
			LeanTween.rotateAround(owner.GetBody(), -Vector3.forward, 90f, 0.1f);
			LeanTween.scaleY(owner.GetBody(), 0.5f, 0.1f);
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

		LeanTween.scaleY(owner.GetBody(), 1f, 0.1f);
		owner.GetBody().transform.eulerAngles = Vector3.zero;

		owner.SetParticleActive("Roll", false);
	}
}
