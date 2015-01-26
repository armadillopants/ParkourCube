using UnityEngine;

public class FallState : ParkourState
{
	private Vector3 lastPosition;

	private Vector2 velocity;
	private float gravity = 6f;

	public FallState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();

		lastPosition = owner.transform.position;
	}

	public override void Update()
	{
		base.Update();

		owner.velocity.y -= gravity * Time.fixedDeltaTime;

		RaycastHit2D hit = Physics2D.Linecast(owner.transform.position, owner.transform.position - new Vector3(0, 0.6f, 0), owner.GetLayerMask());

		if (hit.collider != null)
		{
			if (Vector2.Distance(owner.transform.position, lastPosition) > 3f)
			{
				if (owner.canMove && TryRoll()) { return; }
				else { World.GameOver(); owner.canMove = false; return; }
			}

			owner.SetState(new RunState(owner));
			return;
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
