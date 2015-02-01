using UnityEngine;

public class ReboundState : ParkourState
{

	private float reboundTime;
	private float jumpStrength = 4f;

	private GameObject reboundParticle;

	public ReboundState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();

		RaycastHit2D rightWallHit = Physics2D.Linecast(owner.transform.position, owner.transform.position + new Vector3(0.6f, 0, 0), owner.GetLayerMask());

		if (rightWallHit.collider != null)
		{
			owner.velocity = -Vector2.right;
			reboundParticle = owner.transform.GetChild(1).gameObject;
		}

		RaycastHit2D leftWallHit = Physics2D.Linecast(owner.transform.position, owner.transform.position + new Vector3(-0.6f, 0, 0), owner.GetLayerMask());

		if (leftWallHit.collider != null)
		{
			owner.velocity = Vector2.right;
			reboundParticle = owner.transform.GetChild(2).gameObject;
		}

		reboundParticle.SetActive(true);

		reboundTime = 0f;
	}

	public override void Update()
	{
		base.Update();

		reboundTime += Time.fixedDeltaTime;

		if (reboundTime > 0.3f)
		{
			owner.SetState(new FallState(owner));
			return;
		}
		else
		{
			owner.velocity.y += jumpStrength * Time.fixedDeltaTime;
		}

		owner.Move(owner.velocity);

		if (owner.GetBody().transform.eulerAngles != Vector3.zero)
		{
			LeanTween.rotateZ(owner.GetBody(), 0f, 5f * Time.fixedDeltaTime);
		}
	}

	public override void Exit()
	{
		base.Exit();

		reboundParticle.SetActive(false);
	}
}
