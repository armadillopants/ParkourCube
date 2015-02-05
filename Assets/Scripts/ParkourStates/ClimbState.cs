using UnityEngine;

public class ClimbState : ParkourState
{
	private float wallRunTime;

	public ClimbState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();

		wallRunTime = 0f;
		owner.velocity = Vector2.up + Vector2.right;

		owner.SetParticleActive("Climb");

		LeanTween.delayedSound(owner.climb, owner.transform.position, 1f);
	}

	public override void Update()
	{
		base.Update();

		wallRunTime += Time.fixedDeltaTime;

		if (wallRunTime > 0.6f)
		{
			owner.SetState(new RunState(owner));
			return;
		}

		owner.Move(owner.velocity);

		RaycastHit2D rightWallHit = Physics2D.Linecast(owner.transform.position, owner.transform.position + new Vector3(0.6f, 0f, 0), owner.GetLayerMask());

		if (rightWallHit.collider != null)
		{
			LeanTween.rotateZ(owner.GetBody(), 45, 0.1f);
		}
	}

	public override void Exit()
	{
		base.Exit();

		owner.SetParticleActive("Climb", false);
	}
}
