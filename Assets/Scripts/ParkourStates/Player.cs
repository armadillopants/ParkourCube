using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{

	public float currentSpeed;

	private ParkourState currentState;
	private Rigidbody2D rigid;
	private LayerMask playerLayer;

	private Obstacle obstacle;
	private GameObject obstacleRoot;

	void Start()
	{
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
			//obstacle.TryInteract();
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
		rigid.position += direction * currentSpeed * Time.fixedDeltaTime;
	}

	public LayerMask GetLayerMask()
	{
		return playerLayer;
	}

	public void SetGravity(float gravity)
	{
		rigid.gravityScale = gravity;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		GameObject root = other.RootGameObject();
		Obstacle obs = root.GetComponent<Obstacle>();
		if(obs)
		{
			obstacle = obs;
			obstacleRoot = root;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject == obstacleRoot)
		{
			obstacle = null;
			obstacleRoot = null;
		}
	}
}
