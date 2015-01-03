using UnityEngine;

public class WallRunningState : ParkourState
{
	private float wallRunTime;

	public WallRunningState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();

		wallRunTime = 0f;
		owner.velocity = Vector2.up;
	}

	public override void Update()
	{
		base.Update();

		if (TryHang()) { return; }

		wallRunTime += Time.fixedDeltaTime;

		if (wallRunTime > 0.5f)
		{
			if (!Physics2D.Linecast(owner.transform.position, owner.transform.position + new Vector3(0.5f, 0.2f, 0), owner.GetLayerMask()))
			{
				owner.SetState(new HoldingOnState(owner));
				return;
			}

			if (!Physics2D.Linecast(owner.transform.position, owner.transform.position + new Vector3(-0.5f, 0.2f, 0), owner.GetLayerMask()))
			{
				owner.SetState(new HoldingOnState(owner));
				return;
			}

			owner.SetState(new HangingState(owner));
			return;
		}

		owner.Move(owner.velocity);

		RaycastHit2D rightWallHit = Physics2D.Linecast(owner.transform.position, owner.transform.position + new Vector3(0.5f, 0.2f, 0), owner.GetLayerMask());

		if (rightWallHit.collider != null)
		{
			LeanTween.rotateZ(owner.gameObject, 45, 5f * Time.fixedDeltaTime);
		}

		RaycastHit2D leftWallHit = Physics2D.Linecast(owner.transform.position, owner.transform.position + new Vector3(-0.5f, 0.2f, 0), owner.GetLayerMask());

		if (leftWallHit.collider != null)
		{
			LeanTween.rotateZ(owner.gameObject, -45, 5f * Time.fixedDeltaTime);
		}
	}

	public override void Exit()
	{
		base.Exit();
	}
}
