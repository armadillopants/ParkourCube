using UnityEngine;

[RequireComponent(typeof(PooledObject))]
public class Obstacle : WorldObject
{
	public TriggerWatcher perfectBox;
	
	public float size;

	public TriggerWatcher[] priorityList;

	public bool SuccessfulInteraction
	{ get { return successfulInteraction; } }
	private bool successfulInteraction;

	public override void Update()
	{
		base.Update();
	}

	public Transform GetRandomSpawnPoint()
	{
		throw new System.NotImplementedException();
	}

	public void TryUse(Player player)
	{
		for(int i=0; i<priorityList.Length; ++i)
		{
			if(priorityList[i].IsPlayerTouching)
			{
				BehaviourActuator bActuator = priorityList[i].GetComponent<BehaviourActuator>();
				bool success = bActuator.ResolveInput(player);
				if(success)
				{
					successfulInteraction = true;
					World.ReportNormalObstacleUse(this);

					PerfectBoxDisabler pBox = perfectBox.GetComponent<PerfectBoxDisabler>();
					if(!pBox.Disabled)
					{
						perfectInteraction = true;
						World.ReportPerfectObstacleUse(this);
						Debug.Log("Perfect!");
					}
				}
			}
		}
	}

	public override void Reset()
	{
		base.Reset();
		successfulInteraction = false;
		perfectInteraction = false;
	}

	public bool IsPerfectInteraction()
	{
		return perfectInteraction;
	}
}
