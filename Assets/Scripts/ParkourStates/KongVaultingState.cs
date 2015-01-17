using UnityEngine;

public class KongVaultingState : ParkourState
{
	private float speedBoost = 3f;
	private float yMod = 1.5f;
	private float vaultTime;

	public KongVaultingState(Player player) : base(player) { }

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
			owner.SetState(new RunningState(owner));
			return;
		}

		owner.velocity.y += yMod * Time.fixedDeltaTime;
		owner.Move(owner.velocity * speedBoost);

		LeanTween.rotateZ(owner.gameObject, -45, 5f * Time.fixedDeltaTime);
	}

	public override void Exit()
	{
		base.Exit();
	}
}
