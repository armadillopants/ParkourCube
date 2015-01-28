using UnityEngine;
using System.Collections.Generic;

public class WorldObject : MonoBehaviour, IPoolable
{
	#region Statics
	[SerializeField]
	private static List<ObjectPool> obstacles;
	[SerializeField]
	private static ObjectPool terrain;
	[SerializeField]
	private static GameObject noop;
	[SerializeField]
	private static int totalObstacles = -1;

	public const int PRELOAD = 10;

	public static bool Generated
	{ get { return obstacles != null; } }

	public static GameObject Terrain
	{ get { return terrain.Get(); } }

	public static GameObject NoObject
	{ get { return noop; } }

	public static int NumObstacles
	{ get { return totalObstacles; } }

	private const float DELETE_LEFT_DIST = 20f;

	public static void Load()
	{
		GameObject[] objects = Resources.LoadAll<GameObject>("Obstacles/");
		if(objects != null)
		{
			obstacles = new List<ObjectPool>();
			foreach(GameObject obj in objects)
			{
				ObjectPool pool = new ObjectPool(obj, 1, true);
				obstacles.Add(pool);
			}
			totalObstacles = obstacles.Count;
		}
		else
		{
			Debug.LogError("No obstacles found in Assets/Resources/Obstacles");
			Debug.Break();
		}
		terrain = new ObjectPool(Resources.Load<GameObject>("Terrain"), 1, true);
	}

	public static GameObject GetObstacle(int index)
	{
		return obstacles[index].Get();
	}

	public static GameObject GetTerrain()
	{
		return terrain.Get();
	}
	#endregion

	protected static System.Random spRandom;

	public Transform[] spawnPoints;

	protected List<GameObject> next = new List<GameObject>();

	protected bool playerInteracted;

	protected bool perfectInteraction;

	protected bool deletionFlag;

	public GameObject[] Tails
	{ get { return next.ToArray(); } }

	protected Renderer myRenderer;

	public virtual void Start()
	{
		if(spRandom == null) { spRandom = new System.Random(); }

		myRenderer = GetComponentInChildren<Renderer>();
	}

	public virtual void Update()
	{
		if (World.player)
		{
			if ((transform.position.x < World.player.transform.position.x - DELETE_LEFT_DIST) && deletionFlag && !myRenderer.isVisible)
			{
				if (GameManager.Instance.TutorialCompleted)
				{
					GetComponent<PooledObject>().ReturnToPool();
				}
			}
		}
	}

	public void AddTail(GameObject obj)
	{
		next.AddUnique(obj);
	}

	public void Discard()
	{
		foreach(GameObject obj in next)
		{
			WorldObject script = obj.GetComponent<WorldObject>();
			script.Discard();
		}
		Reset();
		gameObject.GetComponent<PooledObject>().ReturnToPool();
	}

	public virtual void Reset()
	{
		next.Clear();
		playerInteracted = false;
		deletionFlag = false;
	}

	public virtual bool HasPlayerInteracted()
	{
		return playerInteracted;
	}

	public virtual void FlagForDeletion() { deletionFlag = true; }

	public Vector3 GetTrailingPoint()
	{
		if(spRandom ==  null) { spRandom = new System.Random(); }

#if UNITY_EDITOR
		if(spawnPoints == null)
		{
			Debug.LogError("Piece has no Spawn Points: " + name);
			Debug.Break();
		}
#endif
		int roll = spRandom.Next(1, spawnPoints.Length + 1) - 1;
		return spawnPoints[roll].position;
	}
}
