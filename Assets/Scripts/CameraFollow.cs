﻿using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	private Transform target;
	private Vector3 velocity;
	private float damp;

	void Start()
	{
		target = GameObject.FindGameObjectWithTag("Player").transform;
		velocity = Vector3.zero;
		damp = 0.1f;
	}

	void FixedUpdate()
	{
		if (target)
		{
			Vector3 point = camera.WorldToViewportPoint(target.position);
			Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, damp);
		}
	}
}
