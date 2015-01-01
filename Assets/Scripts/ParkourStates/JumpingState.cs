using UnityEngine;

public class JumpingState : ParkourState
{
	private float gravity = 6f;
	private float jumpStrength = 6f;
	private float jumpTime;

	private float castLineTime;

	public JumpingState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();

		jumpTime = 0f;
		castLineTime = 0f;
	}

	public override void Update()
	{
		base.Update();

		castLineTime += Time.fixedDeltaTime;

		if (castLineTime > 0.1f)
		{
			RaycastHit2D vaultHit = Physics2D.Linecast(owner.transform.position, owner.transform.position + new Vector3(0.5f, -0.2f, 0), owner.GetLayerMask());

			if (vaultHit.collider != null && vaultHit.transform.position.y < owner.transform.position.y)
			{
				if (TryVault()) { return; }
			}

			RaycastHit2D rightWallHit = Physics2D.Linecast(owner.transform.position, owner.transform.position + new Vector3(0.5f, 0.2f, 0), owner.GetLayerMask());

			if (rightWallHit.collider != null)
			{
				if (TryWallRun()) { return; }
			}

			RaycastHit2D leftWallHit = Physics2D.Linecast(owner.transform.position, owner.transform.position + new Vector3(-0.5f, 0.2f, 0), owner.GetLayerMask());

			if (leftWallHit.collider != null)
			{
				if (TryWallRun()) { return; }
			}
		}

		jumpTime += Time.fixedDeltaTime;

		if (jumpTime > 0.2f)
		{
			owner.velocity.y -= gravity * Time.fixedDeltaTime;

			RaycastHit2D hit = Physics2D.Linecast(owner.transform.position, owner.transform.position - new Vector3(0, 0.4f, 0), owner.GetLayerMask());

			if(hit.collider != null)
			{
				owner.SetState(new RunningState(owner));
				return;
			}
		}
		else
		{
			owner.velocity.y += jumpStrength * Time.fixedDeltaTime;
		}

		owner.Move(owner.velocity);

		if (owner.transform.eulerAngles != Vector3.zero)
		{
			LeanTween.rotateZ(owner.gameObject, 0, 5f * Time.fixedDeltaTime);
		}
	}

	public override void Exit()
	{
		base.Exit();
	}
}
