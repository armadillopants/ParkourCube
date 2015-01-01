using UnityEngine;
using System.Collections;
using Hack.States;

public class VaultingState : ParkourState
{
	private float speedBoost = 3f;
	private float vaultTime;

	public VaultingState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();

		vaultTime = 0f;
		owner.velocity = Vector2.right;
	}

	public override void Update()
	{
		base.Update();

		vaultTime += Time.fixedDeltaTime;

		if (vaultTime > 0.2f)
		{
			if (TryRun()) { return; }
		}

		owner.Move(owner.velocity * speedBoost);
	}

	public override void Exit()
	{
		base.Exit();
	}
}
