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
	}

	public override void Update()
	{
		base.Update();

		hangTime += Time.fixedDeltaTime;

		if (hangTime > 0.5f)
		{
			owner.SetState(new FallState(owner));
		}

		owner.transform.position = hangPos;

		RaycastHit2D rightWallHit = Physics2D.Linecast(owner.transform.position, owner.transform.position + new Vector3(0.6f, 0, 0), owner.GetLayerMask());

		if (rightWallHit.collider != null)
		{
			LeanTween.rotateZ(owner.gameObject, 45, 5f * Time.fixedDeltaTime);
		}

		RaycastHit2D leftWallHit = Physics2D.Linecast(owner.transform.position, owner.transform.position + new Vector3(-0.6f, 0, 0), owner.GetLayerMask());

		if (leftWallHit.collider != null)
		{
			LeanTween.rotateZ(owner.gameObject, -45, 5f * Time.fixedDeltaTime);
		}

	}

	public override void Exit()
	{
		base.Exit();

		RaycastHit2D rightWallHit = Physics2D.Linecast(owner.transform.position, owner.transform.position + new Vector3(0.6f, 0, 0), owner.GetLayerMask());

		if (rightWallHit.collider != null)
		{
			owner.velocity = -Vector2.right;
		}

		RaycastHit2D leftWallHit = Physics2D.Linecast(owner.transform.position, owner.transform.position + new Vector3(-0.6f, 0, 0), owner.GetLayerMask());

		if (leftWallHit.collider != null)
		{
			owner.velocity = Vector2.right;
		}
	}
}
