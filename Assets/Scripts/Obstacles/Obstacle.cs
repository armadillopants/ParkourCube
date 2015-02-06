//#define AUTOMODE
#undef AUTOMODE

using UnityEngine;

[RequireComponent(typeof(PooledObject))]
public class Obstacle : WorldObject
{
	public TriggerWatcher perfectBox;
	
	public float size;

	public Transform startingPoint;

	public Transform follower;

	public TriggerWatcher[] priorityList;

#if AUTOMODE
	private BehaviourActuator lastUsed;
#endif

	public bool SuccessfulInteraction
	{
		get { return successfulInteraction; }
		set { successfulInteraction = value; }
	}
	private bool successfulInteraction;

	public override void Update()
	{
		base.Update();

#if AUTOMODE
		Player player = GameObject.FindObjectOfType<Player>();
		TryUse(player);
#endif
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

#if AUTOMODE
				if(bActuator != lastUsed)
				{
					lastUsed = bActuator;

					bool success = bActuator.ResolveInput(player);
					if(success)
					{
						successfulInteraction = true;

						PerfectBoxDisabler pBox = perfectBox.GetComponent<PerfectBoxDisabler>();
						if(!pBox.Disabled)
						{
							perfectInteraction = true;
							World.ReportPerfectObstacleUse(this);
						}
						else
						{
							World.ReportNormalObstacleUse(this);
						}
					}
				}
#else
				bool success = bActuator.ResolveInput(player);
				if(success)
				{
					successfulInteraction = true;

					PerfectBoxDisabler pBox = perfectBox.GetComponent<PerfectBoxDisabler>();
					if(!pBox.Disabled)
					{
						Instantiate(GameManager.Instance.perfectObject, perfectBox.transform.position, Quaternion.identity);
						perfectInteraction = true;
						World.ReportPerfectObstacleUse(this);
					}
					else
					{
						if (gameObject.name != "Rebound Wall Flat" && gameObject.name != "Rebound Wall Up")
						{
							GameManager.multiplier = 0;
						}
						Instantiate(GameManager.Instance.normalObject, priorityList[i].transform.position, Quaternion.identity);
						World.ReportNormalObstacleUse(this);
					}
				}
#endif
			}
		}
	}

	public override void Reset()
	{
		base.Reset();
		successfulInteraction = false;
		perfectInteraction = false;
#if AUTOMODE
		lastUsed = null;
#endif
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
