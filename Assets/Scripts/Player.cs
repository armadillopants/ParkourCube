using UnityEngine;

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
	private Obstacle lastObstacle;
	private GameObject obstacleRoot;

	void Start()
	{
		rigid = rigidbody2D;

		playerLayer = ~(1 << LayerMask.NameToLayer("Player"));

		currentState = new RunState(this);
		currentState.Enter();

		lastObstacle = null;
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
		GameObject root = other.RootGameObject();
		Obstacle obs = root.GetComponent<Obstacle>();
		if(obs)
		{
			obstacle = obs;
			lastObstacle = obstacle;
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

	public Obstacle GetObstacle()
	{
		return lastObstacle;
	}

	public GameObject GetBody()
	{
		return body;
	}
}
