using UnityEngine;

[RequireComponent(typeof(PooledObject))]
public class Obstacle : WorldObject
{
	public TriggerWatcher perfectBox;
	
	public float size;

	public Transform startingPoint;

	public Transform follower;

	public TriggerWatcher[] priorityList;

	public bool SuccessfulInteraction
	{
		get { return successfulInteraction; }
		set { successfulInteraction = value; }
	}
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
			if(!GameManager.Instance.GameOver && priorityList[i].IsPlayerTouching)
			{
				BehaviourActuator bActuator = priorityList[i].GetComponent<BehaviourActuator>();
				bool success = bActuator.ResolveInput(player);
				if(success)
				{
					successfulInteraction = true;

					PerfectBoxDisabler pBox = perfectBox.GetComponent<PerfectBoxDisabler>();
					if(!pBox.Disabled)
					{
						perfectInteraction = true;
						//World.ReportPerfectObstacleUse(this);
						World.ReportNormalObstacleUse(this);
					}
					else
					{
						World.ReportNormalObstacleUse(this);
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

	public Vector3 ResizeFollowerTerrain(float size)
	{
		follower.localScale = new Vector3(size, 1, 1);

		Vector3 newPosition = follower.position;
		newPosition.x += size;
		return newPosition;
	}
}
