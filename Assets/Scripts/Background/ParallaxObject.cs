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
	private float parallaxAmount;
	private bool wasOnScreen;

	public void RollRandomLevel()
	{
		System.Random random = new System.Random();
		parallaxAmount = parallaxValues[random.Next(0, 3)];
	}

	public void UpdatePosition(Vector3 cDelta)
	{
		Vector3 newPos = transform.localPosition;
		newPos += cDelta * parallaxAmount;
		transform.localPosition = newPos;

		MoveIfOffScreen();
	}

	private void MoveIfOffScreen()
	{
		if(!renderer.isVisible && wasOnScreen)
		{
			wasOnScreen = false;
			Vector3 newPos = transform.position;
			newPos.x = Camera.main.ViewportToWorldPoint(Vector2.right).x + collider.bounds.extents.x + 1f;
			transform.position = newPos;
		}
		
		if(renderer.isVisible)
		{
			wasOnScreen = true;
		}
	}
}
