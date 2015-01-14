using UnityEngine;

public class Obstacle : WorldObject
{
	public TriggerWatcher useBox;
	public TriggerWatcher perfectBox;

	[SerializeField]
	private float size;

	public override void Update()
	{
		base.Update();

		if(useBox.IsPlayerTouching) { playerInteracted = true; }
	}

	public Transform GetRandomSpawnPoint()
	{
		throw new System.NotImplementedException();
	}

	public float GetSize()
	{
		return size;
	}

	public void TryInteract()
	{
		if(perfectBox.IsPlayerTouching) { World.ReportPerfectObstacleUse(this); }
		else if(useBox.IsPlayerTouching) { World.ReportNormalObstacleUse(this); }
	}
}
