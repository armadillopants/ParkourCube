using UnityEngine;

public enum ParallaxLevel
{
	Near,
	Middle,
	Far
}

public class ParallaxObject : MonoBehaviour
{
	private readonly float[] parallaxValues = { 0.6f, 0.3f, 0.1f };
	private readonly float[] zDistances = { 1f, 8f, 15f };

	private float zDistance;
	private float parallaxAmount;
	private bool wasOnScreen;

	public void RollRandomLevel()
	{
		System.Random random = new System.Random();
		parallaxAmount = parallaxValues[random.Next(0, 3)];
		zDistance = zDistances[random.Next(0, 3)];
	}

	public void UpdatePosition(Vector3 cDelta)
	{
		Vector3 newPos = transform.localPosition;
		newPos += cDelta * parallaxAmount;
		newPos.z = zDistance;
		transform.localPosition = newPos;

		MoveIfOffScreen();
	}

	private void MoveIfOffScreen()
	{
		if(!renderer.isVisible && wasOnScreen)
		{
			wasOnScreen = false;
			Vector3 newPos = transform.position;
			newPos.x = Camera.main.ViewportToWorldPoint(Vector2.right).x + renderer.bounds.extents.x + 1f;

			float vertExtent = Camera.main.camera.orthographicSize;

			float bottomBound = (float)(vertExtent - renderer.bounds.size.y / 2.0f);
			float topBound = (float)(renderer.bounds.size.y / 2.0f - vertExtent);

			if (newPos.y < bottomBound || newPos.y > topBound)
			{
				newPos.y = Camera.main.ViewportToWorldPoint(new Vector3(0, Random.value, 0)).y;
			}
			transform.position = newPos;
		}
		
		if(renderer.isVisible)
		{
			wasOnScreen = true;
		}
	}
}
