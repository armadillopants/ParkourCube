using UnityEngine;

public class JumpState : ParkourState
{
	private float jumpStrength = 4f;
	private float jumpTime;
	private Vector2 velocity;

	private float gravity = 6f;

	public JumpState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();

		jumpTime = 0f;

		owner.SetParticleActive("Jump");

		LeanTween.delayedSound(owner.jump, Vector3.zero, 1f);
	}

	public override void Update()
	{
		base.Update();

		jumpTime += Time.fixedDeltaTime;

		if (jumpTime > 0.3f)
		{
			LeanTween.scale(owner.GetBody(), new Vector3(0.5f, 1f, 0f), 0.1f);

			owner.velocity.y -= gravity * Time.fixedDeltaTime;

			RaycastHit2D hit = Physics2D.Linecast(owner.transform.position, owner.transform.position - new Vector3(0, 0.6f, 0), owner.GetLayerMask());

			if (hit.collider != null)
			{
				owner.SetState(new RunState(owner));
				return;
			}
		}
		else
		{
			LeanTween.scale(owner.GetBody(), new Vector3(0.3f, 1.2f, 0f), 0.1f);
			owner.velocity.y += jumpStrength * Time.fixedDeltaTime;
		}

		owner.Move(owner.velocity);

		if (owner.GetBody().transform.eulerAngles != Vector3.zero)
		{
			LeanTween.rotateZ(owner.GetBody(), 0, 0.1f);
		}
	}

	public override void Exit()
	{
		base.Exit();

		LeanTween.scale(owner.GetBody(), new Vector3(0.5f, 1f, 0f), 0.1f);
		owner.SetParticleActive("Jump", false);
	}
}
