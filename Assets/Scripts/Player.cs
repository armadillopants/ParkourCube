﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

	public Transform groundCheck;
	public Transform leftWallCheck;
	public Transform rightWallCheck;
	public Transform vaultCheck;
	public float currentSpeed;

	private ParkourState currentState;
	private Rigidbody2D rigid;

	public KeyWatcher spaceKey;
	public KeyWatcher sKey;

	void Start()
	{
		rigid = rigidbody2D;

		spaceKey = new KeyWatcher(KeyCode.Space);
		sKey = new KeyWatcher(KeyCode.S);

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
}
