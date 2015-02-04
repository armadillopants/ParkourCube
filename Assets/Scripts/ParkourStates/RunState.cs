using UnityEngine;

public class RunState : ParkourState
{
	public RunState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();

		owner.velocity = Vector2.right;

		LeanTween.scale(owner.GetBody(), new Vector3(0.5f, 1f, 0f), 0.1f);

		owner.SetParticleActive("Run");
	}

	public override void Update()
	{
		base.Update();

		owner.Move(owner.velocity);

		if (TryFall()) { return; }

		if (owner.GetBody().transform.eulerAngles != Vector3.zero)
		{
			LeanTween.rotateZ(owner.GetBody(), 0f, 0.1f);
		}
	}

	public override void Exit()
	{
		base.Exit();

		owner.SetParticleActive("Run", false);
	}
}
