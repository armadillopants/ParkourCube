using UnityEngine;

public class VaultState : ParkourState
{
	private float speedBoost = 3f;
	private float vaultTime;

	public VaultState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();

		vaultTime = 0f;
	}

	public override void Update()
	{
		base.Update();

		vaultTime += Time.fixedDeltaTime;

		if (vaultTime > 0.2f)
		{
			owner.SetState(new RunState(owner));
		}

		owner.Move(Vector2.right * speedBoost);
	}

	public override void Exit()
	{
		base.Exit();
	}
}
