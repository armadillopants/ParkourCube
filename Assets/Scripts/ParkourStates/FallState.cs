using UnityEngine;

public class FallState : ParkourState
{
	private float gravity = 6f;

	public FallState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();
	}

	public override void Update()
	{
		base.Update();

		owner.velocity.y -= gravity * Time.fixedDeltaTime;

		RaycastHit2D hit = Physics2D.Linecast(owner.transform.position, owner.transform.position - new Vector3(0, 0.6f, 0), owner.GetLayerMask());

		if (hit.collider != null)
		{
			owner.SetState(new RunState(owner));
			return;
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
	}
}
