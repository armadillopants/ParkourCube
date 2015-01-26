using Hack.Core;
using UnityEngine;
using System.Collections.Generic;

//using WorldTree = NBTree<System.Collections.Generic.KeyValuePair<int, UnityEngine.Transform>>;
//using WorldNode = NBTreeNode<System.Collections.Generic.KeyValuePair<int, UnityEngine.Transform>>;

public class World : Singleton<World>
{
	public const int SPAWN_AHEAD = 20;
	public const int WATCH_TIME = 3;
	private const float REPEAT_MOD =  0.6f;

	private const float MIN_DISTANCE = 2f;
	private const float MAX_DISTANCE = 10f;
	private const float DISTANCE_RANGE = MAX_DISTANCE - MIN_DISTANCE;
	private const float TERRAIN_SCORE_DIVISOR = 25f;

	private Dictionary<int, int> counts;

	private bool firstRoll;
	private GameObject lastSpawned;
	private int[] spawnQueue;
	private int totalSpawned;
	private Vector3 nextPiecePosition;

	private int playerScore;

	private static List<GameObject> spawned;

	public static GameObject player;

	public static World CreateNewWorld()
	{
		if(!WorldObject.Generated) { WorldObject.Load(); }
		instance = new World();

		// Create new player
		GameObject playerObject = Resources.Load("Player") as GameObject;
		GameObject pInst = GameObject.Instantiate(playerObject, new Vector3(-9f, 0, 0), Quaternion.identity) as GameObject;
		player = pInst;
		spawned.Add(pInst);

		// Create new doom wall

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

		// Create the terrain below it.
		GameObject uTerrain = WorldObject.GetTerrain();
		uTerrain.transform.position = lastSpawned.transform.position;
		Vector3 uTScale = uTerrain.transform.localScale;
		uTScale.x = obstacle.size;
		uTerrain.transform.localScale = uTScale;

		spawned.Add(uTerrain);

		// Create the terrain ahead of it.
		GameObject fTerrain = WorldObject.GetTerrain();
		WorldObject fTObj = fTerrain.GetComponent<WorldObject>();
		fTerrain.transform.position = obstacle.GetTrailingPoint();
		Vector3 fTScale = fTerrain.transform.localScale;
		fTScale.x = GetTerrainLength();
		fTerrain.transform.localScale = fTScale;
		nextPiecePosition = fTObj.GetTrailingPoint();

		spawned.Add(fTerrain);
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
		for(int i=0; i<WorldObject.NumObstacles; ++i)
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
		instance.playerScore++;
		GameManager.Instance.UpdateScore(instance.playerScore);
		instance.BuildNext();
		obstacle.FlagForDeletion();
	}

	public static void ReportPerfectObstacleUse(Obstacle obstacle)
	{
		Debug.Log("Perfect");
		instance.playerScore += 2;
		GameManager.Instance.UpdateScore(instance.playerScore);
		instance.BuildNext();
		obstacle.FlagForDeletion();
	}

	public static void GameOver()
	{
		Debug.Log("Game Over!");
		GameManager.Instance.OnGameOver();
		GameManager.Instance.DisplayFinalScore(instance.playerScore);
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