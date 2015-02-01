using UnityEngine;

public class VaultState : ParkourState
{
	private float speedBoost = 3f;
	private float yMod = 1.5f;
	private float vaultTime;

	private GameObject vaultParticle;

	public VaultState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();

		vaultTime = 0f;
		owner.velocity = Vector2.right;

		LeanTween.rotateZ(owner.GetBody(), -45, 5f * Time.fixedDeltaTime);

		vaultParticle = owner.transform.GetChild(4).gameObject;
		vaultParticle.SetActive(true);
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

		owner.velocity.y += yMod * Time.fixedDeltaTime;

		owner.Move(owner.velocity * speedBoost);
	}

	public override void Exit()
	{
		base.Exit();

		vaultParticle.SetActive(false);
	}
}
