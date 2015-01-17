using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PerfectBoxDisabler : MonoBehaviour, IPoolable
{
	public bool Disabled
	{ get { return disabled; } }
	private bool disabled;

	public float destroyTime;
	private bool hasCollided;
	private float collideTime;

	void Update()
	{
		if(disabled) { return; }
		if(hasCollided)
		{
			collideTime += Time.deltaTime;
			if(collideTime > destroyTime)
			{
				disabled = true;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(disabled) { return; }
		if(other.tag == "Player")
		{
			hasCollided = true;
			collideTime = 0f;
		}
	}

	void IPoolable.Reset()
	{
		hasCollided = false;
		collideTime = 0f;
		disabled = false;
	}
}
