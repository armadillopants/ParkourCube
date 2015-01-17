using UnityEngine;

public class HoldingOnState : ParkourState
{
	private Vector2 hangPos;

	public HoldingOnState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();

		hangPos = owner.transform.position;
	}

	public override void Update()
	{
		base.Update();

		if (TryPullUp()) { return; }

		owner.transform.position = hangPos;

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
