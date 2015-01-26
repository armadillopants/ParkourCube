using UnityEngine;

public class HangState : ParkourState
{

	private Vector2 hangPos;
	private float hangTime;

	public HangState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();

		hangPos = owner.transform.position;
		hangTime = 0f;

		owner.velocity = Vector2.zero;

		RaycastHit2D rightWallHit = Physics2D.Linecast(owner.transform.position, owner.transform.position + new Vector3(0.6f, 0, 0), owner.GetLayerMask());

		if (rightWallHit.collider != null)
		{
			LeanTween.rotateZ(owner.GetBody(), 45, 5f * Time.fixedDeltaTime);
		}

		RaycastHit2D leftWallHit = Physics2D.Linecast(owner.transform.position, owner.transform.position + new Vector3(-0.6f, 0, 0), owner.GetLayerMask());

		if (leftWallHit.collider != null)
		{
			LeanTween.rotateZ(owner.GetBody(), -45, 5f * Time.fixedDeltaTime);
		}
	}

	public override void Update()
	{
		base.Update();

		hangTime += Time.fixedDeltaTime;

		if (hangTime > 0.3f)
		{
			owner.SetState(new FallState(owner));
			return;
		}

		owner.transform.position = hangPos;

	}

	public override void Exit()
	{
		base.Exit();
	}
}
