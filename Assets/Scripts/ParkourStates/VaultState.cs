using UnityEngine;

public class VaultState : ParkourState
{
	private float speedBoost = 3f;
	private float yMod = 1.5f;
	private float vaultTime;

	public VaultState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();

		vaultTime = 0f;
		owner.velocity = Vector2.right;

		owner.SetParticleActive("Vault");
	}

	public override void Update()
	{
		base.Update();

		vaultTime += Time.fixedDeltaTime;

		if (vaultTime > 0.3f)
		{
			owner.SetState(new RunState(owner));
			return;
		}
		else
		{
			LeanTween.scaleY(owner.GetBody(), 0.5f, 0.3f);
		}

		owner.velocity.y += yMod * Time.fixedDeltaTime;

		owner.Move(owner.velocity * speedBoost);

		LeanTween.rotateZ(owner.GetBody(), -45, 0.1f);
	}

	public override void Exit()
	{
		base.Exit();

		owner.SetParticleActive("Vault", false);
	}
}
