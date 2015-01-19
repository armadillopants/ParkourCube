using UnityEngine;
using System.Collections.Generic;

public class InputMonitor : MSingleton<InputMonitor>
{
#if UNITY_ANDROID
	private const float HOLD_MAG = 0.2f;
#else
	private const float HOLD_MAG = 60f;
#endif
	private const int HOLD_DUR = 20;

	[SerializeField]
	private bool wasTouching;
	[SerializeField]
	private bool isTouching;

	[SerializeField]
	private bool swipeConsumed;

	[SerializeField]
	private Vector2 deltaSum;
	[SerializeField]
	private Vector2 deltaDirection;
	[SerializeField]
	private Vector2 previousMousePosition;
	[SerializeField]
	private int numFrames;

	[SerializeField]
	private bool didTap;

	void Start()
	{
		wasTouching = false;
	}

	void FixedUpdate()
	{
#if UNITY_ANDROID
		isTouching = Input.touchCount > 0;
#elif UNITY_EDITOR || UNITY_STANDALONE
		isTouching = Input.GetMouseButton(0);
#endif

		if(!isTouching && wasTouching)
		{
			if(numFrames < HOLD_DUR)
			{
				didTap = true;
			}
		}
		else
		{
			didTap = false;
		}

		if(!isTouching) 
		{
			deltaSum = Vector2.zero;
			deltaDirection = Vector2.zero;
			numFrames = 0;
			swipeConsumed = false;
		}

		if(!wasTouching && isTouching)
		{
#if !UNITY_ANDROID
			previousMousePosition = Input.mousePosition;
#endif
		}

		if(isTouching)
		{
			numFrames++;
#if UNITY_ANDROID
			deltaSum += Input.touches[0].deltaPosition;
#elif UNITY_EDITOR || UNITY_STANDALONE
			Vector2 mousePos = Input.mousePosition;
			Vector2 mouseDelta = mousePos - previousMousePosition;
			deltaSum += mouseDelta;
			previousMousePosition = mousePos;
#endif
			
			deltaDirection = (deltaSum / numFrames).normalized;
		}

		wasTouching = isTouching;
	}

	public bool Swipe(Vector2 direction, float similarityThreshold = 0.7f)
	{
		if(swipeConsumed) { return false; }

		if(deltaSum.magnitude > HOLD_MAG && Vector2.Dot(deltaDirection, direction) > similarityThreshold)
		{
			swipeConsumed = true;
			return true;
		}
		return false;
	}

	public bool Holding()
	{
		if(!isTouching || swipeConsumed) { return false; }
		return numFrames > HOLD_DUR &&
		       (deltaSum / numFrames).magnitude < HOLD_MAG;
	}

	public bool DidTap()
	{
		return didTap;
	}
}
