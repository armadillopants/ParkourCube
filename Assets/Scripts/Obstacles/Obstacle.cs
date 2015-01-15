using UnityEngine;

public class Obstacle : WorldObject
{
	public TriggerWatcher useBox;
	public TriggerWatcher perfectBox;
	
	public float size;

	public TriggerWatcher[] priorityList;

	public override void Update()
	{
		base.Update();

		if(useBox.IsPlayerTouching) { playerInteracted = true; }
	}

	public Transform GetRandomSpawnPoint()
	{
		throw new System.NotImplementedException();
	}

	public void TryInteract()
	{
		if(perfectBox.IsPlayerTouching) { World.ReportPerfectObstacleUse(this); }
		else if(useBox.IsPlayerTouching) { World.ReportNormalObstacleUse(this); }
	}

	public void TryInput(InputInfo info)
	{
		for(int i=0; i<priorityList.Length; ++i)
		{
			if(priorityList[i].IsPlayerTouching)
			{
				BehaviourActuator bActuator = priorityList[i].GetComponent<BehaviourActuator>();
				bActuator.ResolveInput(info);
			}
		}
	}
}
