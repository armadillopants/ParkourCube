using Hack.Core;
using UnityEngine;
using System.Collections.Generic;

//using WorldTree = NBTree<System.Collections.Generic.KeyValuePair<int, UnityEngine.Transform>>;
//using WorldNode = NBTreeNode<System.Collections.Generic.KeyValuePair<int, UnityEngine.Transform>>;

public class World : Singleton<World>
{
	public const int SPAWN_AHEAD = 2;
	public const int WATCH_TIME = 1;
	private const float REPEAT_MOD =  0.6f;

	private const float MIN_DISTANCE = 3f;
	private const float MAX_DISTANCE = 10f;
	private const float DISTANCE_RANGE = MAX_DISTANCE - MIN_DISTANCE;
	private const float TERRAIN_SCORE_DIVISOR = 25f;

	private Dictionary<int, int> counts;

	private bool firstRoll;
	public static GameObject tutorialObject;
	private GameObject lastSpawned;
	private int[] spawnQueue;
	private int totalSpawned;
	private Vector3 nextPiecePosition;

	public int playerScore;

	private static List<GameObject> spawned;

	public static GameObject player;

	public static DoomWall doomWall;

	public static World CreateNewWorld()
	{
		//if(!WorldObject.Generated) { WorldObject.Load(); }
		instance = new World();

		Vector3 location = new Vector3();

		if (!GameManager.Instance.TutorialCompleted)
		{
			location = new Vector3(-68f, -2, 0f);
		}
		else
		{
			location = new Vector3(-9f, 0.5f, 0f);
		}

		if (!GameManager.Instance.TutorialCompleted)
		{
			GameObject spawnObj = Resources.Load("Tutorial") as GameObject;
			GameObject tutInst = GameObject.Instantiate(spawnObj, Vector3.zero, Quaternion.identity) as GameObject;
			tutorialObject = tutInst;
		}

		// Create new player
		GameObject playerObject = Resources.Load("Player") as GameObject;
		GameObject pInst = GameObject.Instantiate(playerObject, location, Quaternion.identity) as GameObject;
		player = pInst;
		spawned.Add(pInst);

		GameObject dWall = Resources.Load("DoomWall") as GameObject;
		if(!dWall)
		{
			Debug.LogError("Move the DoomWall prefab to Resources folder, Matt.");
			Debug.Break();
		}
		GameObject dInst = GameObject.Instantiate(dWall, new Vector3(-10000f, 0f, -1f), Quaternion.identity) as GameObject;
		doomWall = dInst.GetComponent<DoomWall>();

		spawned.Add(dInst);

		return instance;
	}

	public World()
	{
		spawned = new List<GameObject>();
		firstRoll = true;
		nextPiecePosition = Vector3.zero;
		counts = new Dictionary<int, int>();
		spawnQueue = new int[SPAWN_AHEAD];
		totalSpawned = 1; // no division by 0
		for(int i=0; i<WorldObject.NumObstacles; ++i)
		{
			counts.Add(i, 0);
		}
		for(int i=0; i<WATCH_TIME; ++i)
		{
			spawnQueue[i] = -1; // do not want this to be a valid index
		}

		for(int i=0; i<SPAWN_AHEAD; ++i)
		{
			BuildNext();
		}
	}


	public static void Update()
	{
		if(instance != null) { instance.InternalUpdate(); }
	}

	private void InternalUpdate()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			playerScore++;
			BuildNext();
		}
	}

	private void BuildNext()
	{
		// Create the obstacle.
		int roll = RollForNext();
		RecordRoll(roll);
		++totalSpawned;

		if (firstRoll)
		{
			GameObject initialTerrain = WorldObject.GetTerrain();
			initialTerrain.transform.position = new Vector3(-10f, 0, 0);
			Vector3 initialScale = initialTerrain.transform.localScale;
			initialScale.x = 10f;
			initialTerrain.transform.localScale = initialScale;
			firstRoll = false;
		}

		// Create the new obstacle.
		GameObject toSpawn = WorldObject.GetObstacle(roll);
		toSpawn.transform.position = nextPiecePosition;
		lastSpawned = toSpawn;
		Obstacle obstacle = lastSpawned.GetComponent<Obstacle>();

		spawned.Add(toSpawn);

		nextPiecePosition = obstacle.ResizeFollowerTerrain(GetTerrainLength());
	}

	private float GetTerrainLength()
	{
		float result = Mathf.Lerp(MIN_DISTANCE, MAX_DISTANCE, 1 - Mathf.Clamp(playerScore, 0, TERRAIN_SCORE_DIVISOR) / TERRAIN_SCORE_DIVISOR);
		return result;
	}

	private void RecordRoll(int roll)
	{
		++counts[roll];

		for(int i=0; i<WATCH_TIME-1; ++i)
		{
			spawnQueue[i] = spawnQueue[i+1];
		}
		spawnQueue[WATCH_TIME-1] = roll;
	}

	private int RollForNext()
	{
		int hIndex = 0;
		float hRoll = 0f;
		int[] rolls = new int[WorldObject.NumObstacles];
		for(int i=0; i<spawnQueue.Length; ++i) 
		{
			if(spawnQueue[i] > -1)
				++rolls[spawnQueue[i]];
		}
		for(int i=0; i<WorldObject.NumObstacles; ++i)
		{
			float rollMod = Mathf.Pow(REPEAT_MOD, rolls[i]);
			float roll = Random.value * rollMod * (1 - (counts[i] / totalSpawned));
			if(roll > hRoll)
			{
				hIndex = i;
				hRoll = roll;
			}
		}
		return hIndex;
	}

	public static void ReportNormalObstacleUse(Obstacle obstacle)
	{
		Debug.Log("Normal");
		if (GameManager.Instance.TutorialCompleted)
		{
			instance.playerScore++;
			GameManager.Instance.UpdateScore(instance.playerScore);
		}
		instance.BuildNext();
		obstacle.FlagForDeletion();
	}

	public static void ReportPerfectObstacleUse(Obstacle obstacle)
	{
		Debug.Log("Perfect");
		if (GameManager.Instance.TutorialCompleted)
		{
			instance.playerScore += 2;
			GameManager.Instance.UpdateScore(instance.playerScore);
			GameManager.multiplier += 1;
			GameManager.Instance.DisplayCombo(player.transform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(2.5f, 3f), 0f));
		}
		instance.BuildNext();
		obstacle.FlagForDeletion();
		doomWall.PushBack();
	}

	public static void GameOver()
	{
		GameManager.Instance.OnGameOver();
		GameManager.Instance.DisplayFinalScore(instance.playerScore);
		doomWall.canMove = false;
	}

	public static void Clear()
	{
		foreach(GameObject obj in spawned)
		{
			PooledObject pObj = obj.GetComponent<PooledObject>();
			if(pObj)
			{
				pObj.ReturnToPool();
			}
			else
			{
				GameObject.Destroy(obj);
			}
		}
		spawned.TrimExcess();
	}
}