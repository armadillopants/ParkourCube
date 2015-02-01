using UnityEngine;

public class RunState : ParkourState
{
	private GameObject runParticle;

	public RunState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();

		owner.velocity = Vector2.right;
		LeanTween.scaleY(owner.GetBody(), 1f, 5f * Time.fixedDeltaTime);

		runParticle = owner.transform.GetChild(0).gameObject;
		runParticle.SetActive(true);
	}

	public override void Update()
	{
		base.Update();

		owner.Move(owner.velocity);

		if (TryFall()) { return; }

		if (owner.GetBody().transform.eulerAngles != Vector3.zero)
		{
			LeanTween.rotateZ(owner.GetBody(), 0f, 5f * Time.fixedDeltaTime);
		}
	}

	public override void Exit()
	{
		base.Exit();

		runParticle.SetActive(false);
	}
}
