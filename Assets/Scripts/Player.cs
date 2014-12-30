﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

	private ParkourState currentState;


	void Start()
	{

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
}
