using UnityEngine;

public class Player : MonoBehaviour
{

	public float currentSpeed;

	private ParkourState currentState;
	private Rigidbody2D rigid;
	private LayerMask playerLayer;

	public KeyWatcher spaceKey;
	public KeyWatcher sKey;

	private Obstacle obstacle;
	private Obstacle curObstacle;

	void Start()
	{
		rigid = rigidbody2D;

		spaceKey = new KeyWatcher(KeyCode.Space);
		sKey = new KeyWatcher(KeyCode.S);

		playerLayer = ~(1 << LayerMask.NameToLayer("Player"));

		currentState = new RunState(this);
		currentState.Enter();
	}

	void FixedUpdate()
	{
		spaceKey.Update();
		sKey.Update();

		currentState.Update();

		if (obstacle != null)
		{
			obstacle.TryInteract();
			obstacle = null;
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
}
