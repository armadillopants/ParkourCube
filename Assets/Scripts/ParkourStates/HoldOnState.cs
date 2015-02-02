using UnityEngine;

public class HoldOnState : ParkourState
{
	private Vector2 hangPos;

	public HoldOnState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();

		hangPos = owner.transform.position;
	}

	public override void Update()
	{
		base.Update();

		owner.transform.position = hangPos;

		if (owner.GetBody().transform.eulerAngles != Vector3.zero)
		{
			LeanTween.rotateZ(owner.GetBody(), 0f, 0.1f);
		}
	}

	public override void Exit()
	{
		base.Exit();
	}
}
