using UnityEngine;

public class RunState : ParkourState
{
	public RunState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();

		owner.SetGravity(1f);
	}

	public override void Update()
	{
		base.Update();

		owner.Move(Vector2.right);

		if (TryJump()) { return; }
		if (TrySlide()) { return; }
		if (TryFall()) { return; }

		if (owner.transform.eulerAngles != Vector3.zero)
		{
			LeanTween.rotateZ(owner.gameObject, 0f, 5f * Time.fixedDeltaTime);
		}
	}

	public override void Exit()
	{
		base.Exit();
	}
}
