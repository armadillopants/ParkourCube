using UnityEngine;

public class FallState : ParkourState
{
	private Vector3 lastPosition;

	public FallState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();

		lastPosition = owner.transform.position;
		owner.SetGravity(1f);
	}

	public override void Update()
	{
		base.Update();

		RaycastHit2D hit = Physics2D.Linecast(owner.transform.position, owner.transform.position - new Vector3(0, 0.6f, 0), owner.GetLayerMask());

		if (hit.collider != null)
		{
			if (Vector2.Distance(owner.transform.position, lastPosition) > 5f)
			{
				if (TryRoll()) { return; }
				else { Debug.Log("Dead"); return; }
			}

			owner.SetState(new RunState(owner));
			return;
		}

		owner.Move(Vector2.right);

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
