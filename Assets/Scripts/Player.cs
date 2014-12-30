using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

	public Transform groundCheck;
	public float currentSpeed;

	private ParkourState currentState;
	private Rigidbody2D rigid;


	void Start()
	{
		rigid = rigidbody2D;

		currentState = new RunningState(this);
		currentState.SetPreviousState(currentState);
		currentState.Enter();
	}

	void FixedUpdate()
	{
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
}
