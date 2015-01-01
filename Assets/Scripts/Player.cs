using UnityEngine;

public class Player : MonoBehaviour
{

	public float currentSpeed;
	[HideInInspector]
	public Vector2 velocity;

	private ParkourState currentState;
	private Rigidbody2D rigid;
	private LayerMask playerLayer;

	public KeyWatcher spaceKey;
	public KeyWatcher sKey;

	void Start()
	{
		rigid = rigidbody2D;

		spaceKey = new KeyWatcher(KeyCode.Space);
		sKey = new KeyWatcher(KeyCode.S);

		playerLayer = ~(1 << LayerMask.NameToLayer("Player"));

		currentState = new RunningState(this);
		currentState.SetPreviousState(currentState);
		currentState.Enter();
	}

	void FixedUpdate()
	{
		spaceKey.Update();
		sKey.Update();

		currentState.Update();
	}

	public void SetState(ParkourState newState)
	{
		ParkourState previous = currentState;
		previous.Exit();
		newState.SetPreviousState(previous);
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
}
