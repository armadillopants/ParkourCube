using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{

	public float currentSpeed;

	[HideInInspector]
	public Vector2 velocity;

	[HideInInspector]
	public bool playerTouching;

	[HideInInspector]
	public bool canMove = true;

	[SerializeField]
	private GameObject body;

	private ParkourState currentState;
	private Rigidbody2D rigid;
	private LayerMask playerLayer;

	private Obstacle obstacle;
	private GameObject obstacleRoot;

	private Dictionary<string, GameObject> particleDict = new Dictionary<string, GameObject>();

	public AudioClip jump;
	public AudioClip vault;
	public AudioClip rebound;

	void Start()
	{
		AddParticles();

		rigid = rigidbody2D;

		playerLayer = ~(1 << LayerMask.NameToLayer("Player"));

		currentState = new RunState(this);
		currentState.Enter();
	}

	void FixedUpdate()
	{
		currentState.Update();

		if (obstacle != null)
		{
			obstacle.TryUse(this);
		}
	}

	public void SetState(ParkourState newState)
	{
		ParkourState previous = currentState;
		previous.Exit();
		newState.Enter();
		currentState = newState;
	}

	public void Move(Vector2 direction)
	{
		if (canMove)
		{
			rigid.position += direction * currentSpeed * Time.fixedDeltaTime;
		}
	}

	public LayerMask GetLayerMask()
	{
		return playerLayer;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		GameObject root = null;
		Obstacle obs = null;

		if (!GameManager.Instance.TutorialCompleted)
		{
			root = other.transform.parent.gameObject;
			obs = root.GetComponent<Obstacle>();
		} 
		else 
		{
			root = other.RootGameObject();
			obs = root.GetComponent<Obstacle>();
		}

		if(obs)
		{
			obstacle = obs;
			obstacleRoot = root;
			playerTouching = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject == obstacleRoot)
		{
			obstacle = null;
			obstacleRoot = null;
			playerTouching = false;
		}
	}

	public GameObject GetBody()
	{
		return body;
	}

	private void AddParticles()
	{
		Transform particles = GameObject.Find("Particles").transform;

		foreach (Transform child in particles)
		{
			particleDict.Add(child.name, child.gameObject);
			child.gameObject.SetActive(false);
		}
	}

	public void SetParticleActive(string particle, bool active = true)
	{
		particleDict[particle].SetActive(active);
	}
}
