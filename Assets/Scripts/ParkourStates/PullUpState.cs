using UnityEngine;
using System.Collections;

public class PullUpState : ParkourState
{

	public PullUpState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();
	}

	public override void Update()
	{
		base.Update();

		RaycastHit2D hit = Physics2D.Linecast(owner.transform.position, owner.transform.position + new Vector3(1f, 0f, 0f), owner.GetLayerMask());

		if (hit.collider != null)
		{
			Vector2 wallNormal = hit.normal;

			float newY = hit.collider.transform.lossyScale.y / 1.5f + hit.collider.transform.position.y; // divides to get the normal and adds the height to get top position
			newY -= owner.transform.position.y;
			newY += owner.transform.lossyScale.y / 2 + 0.1f;

			// find new position to move to
			Vector3 moveTo = -wallNormal * 1.5f; // flips normal of collider.z to face opposite of player.z
			moveTo.y = Mathf.Lerp(moveTo.y, newY, 100 * Time.fixedDeltaTime);

			owner.transform.position += moveTo;
		}
	}

	public override void Exit()
	{
		base.Exit();
	}
}
