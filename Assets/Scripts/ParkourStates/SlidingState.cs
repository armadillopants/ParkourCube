using UnityEngine;

public class SlidingState : ParkourState
{
	private float slideTime;
	private float slideSpeed = 2f;

	public SlidingState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();

		slideTime = 0f;
		owner.velocity = Vector2.right;
	}

	public override void Update()
	{
		base.Update();

		if (TryJump()) { return; }

		slideTime += Time.fixedDeltaTime;

		if (slideTime > 0.3f)
		{
			owner.SetState(new RunningState(owner));
		}

		RaycastHit2D hit = Physics2D.Linecast(owner.transform.position, owner.transform.position - new Vector3(0, 0.6f, 0), owner.GetLayerMask());

		if (hit.collider == null)
		{
			owner.SetState(new FallingState(owner));
		}

		owner.Move(owner.velocity * slideSpeed);
		LeanTween.rotateZ(owner.gameObject, 65, 5f * Time.fixedDeltaTime);
	}

	public override void Exit()
	{
		base.Exit();
	}
}
