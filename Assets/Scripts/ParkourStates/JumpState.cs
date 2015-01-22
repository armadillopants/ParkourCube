using UnityEngine;

public class JumpState : ParkourState
{
	private float jumpStrength = 4f;
	private float jumpTime;
	private Vector3 lastPosition;
	private Vector2 velocity;

	private float gravity = 6f;

	public JumpState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();

		jumpTime = 0f;
	}

	public override void Update()
	{
		base.Update();

		jumpTime += Time.fixedDeltaTime;

		if (jumpTime > 0.3f)
		{
			owner.velocity.y -= gravity * Time.fixedDeltaTime;

			RaycastHit2D hit = Physics2D.Linecast(owner.transform.position, owner.transform.position - new Vector3(0, 0.6f, 0), owner.GetLayerMask());

			if (hit.collider != null)
			{
				if (Vector2.Distance(owner.transform.position, lastPosition) > 5f)
				{
					if (TryRoll()) { return; }
					else { World.GameOver(); return; }
				}

				owner.SetState(new RunState(owner));
				return;
			}
		}
		else
		{
			owner.velocity.y += jumpStrength * Time.fixedDeltaTime;
			lastPosition = owner.transform.position;
		}

		owner.Move(owner.velocity);

		if (owner.GetBody().transform.eulerAngles != Vector3.zero)
		{
			LeanTween.rotateZ(owner.GetBody(), 0, 5f * Time.fixedDeltaTime);
		}
	}

	public override void Exit()
	{
		base.Exit();
	}
}
