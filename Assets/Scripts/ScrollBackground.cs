using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{

	public float speed;
	private float leftSideOfScreen;
	private float rightSideOfScreen;

	private List<Transform> backgroundEffects = new List<Transform>();

	void Start()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			Transform child = transform.GetChild(i);
			backgroundEffects.Add(child);
		}
	}

	void LateUpdate()
	{
		float distance = Vector3.Dot(Camera.main.transform.forward, transform.position - Camera.main.transform.position);
		leftSideOfScreen = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance)).x;
		rightSideOfScreen = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance)).x;

		ScrollItems();
	}

	void ScrollItems()
	{
		foreach (Transform child in backgroundEffects)
		{
			float offset = Time.time * speed;

			transform.position = new Vector3(-offset, transform.position.y, transform.position.z);

			Vector3 currentPos = child.localPosition;

			if (currentPos.x < leftSideOfScreen)
			{
				currentPos.x = rightSideOfScreen;
			}

			child.localPosition = currentPos;
		}
	}
}
