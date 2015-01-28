using UnityEngine;

public class TriggerWatcher : MonoBehaviour, IPoolable
{
	private bool playerTouching;
	public bool IsPlayerTouching
	{ get { return playerTouching; } }

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			playerTouching = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			playerTouching = false;
		}
	}

	public virtual void Reset()
	{
		playerTouching = false;
	}
}
