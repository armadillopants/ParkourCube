using UnityEngine;

public class PullUpState : ParkourState
{

	private float lerpTime = 0.3f;
	private float travelDistance = 2f;
	private Vector3 startPos;
	private Vector3 endPos;
	private float currentLerpTime;

	public PullUpState(Player player) : base(player) { }

	public override void Enter()
	{
		base.Enter();

		currentLerpTime = 0f;
		startPos = owner.transform.position;
		endPos = owner.transform.position + owner.transform.up + owner.transform.right * travelDistance;

		LeanTween.delayedSound(owner.pullup, Vector3.zero, 1f);
	}

	public override void Update()
	{
		base.Update();

		currentLerpTime += Time.fixedDeltaTime;

		float percentage = currentLerpTime / lerpTime;
		owner.transform.position = Vector3.Lerp(startPos, endPos, percentage);

		if (currentLerpTime > lerpTime)
		{
			currentLerpTime = lerpTime;
			owner.SetState(new RunState(owner));
			return;
		}

		LeanTween.rotateZ(owner.GetBody(), -25, 0.1f);
		LeanTween.scaleY(owner.GetBody(), 0.8f, 0.1f);
	}

	public override void Exit()
	{
		base.Exit();
	}
}
