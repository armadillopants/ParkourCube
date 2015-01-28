using UnityEngine;
using System.Collections.Generic;

public class InputMonitor : MSingleton<InputMonitor>
{
#if UNITY_ANDROID
	private const float HOLD_MAG = 20f;
#else
	private const float HOLD_MAG = 60f;
#endif
	private const int HOLD_DUR = 20;

	private const int WATCH_FOR = 20;

	private List<Vector2> inputs = new List<Vector2>();

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
			inputs.Clear();
		}

		if(isTouching)
		{
			numFrames++;

			if(inputs.Count == WATCH_FOR)
			{
				Debug.Log("Clearing earliest");
				deltaSum -= inputs[0];
				inputs.RemoveAt(0);
			}

#if UNITY_ANDROID
			Vector2 delta = Input.touches[0].deltaPosition;
			deltaSum += delta;
			inputs.Add(delta);
#elif UNITY_EDITOR || UNITY_STANDALONE
			if(!wasTouching)
			{
				previousMousePosition = Input.mousePosition;
			}

			Vector2 mousePos = Input.mousePosition;
			Vector2 mouseDelta = mousePos - previousMousePosition;

			inputs.Add(mouseDelta);
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
