using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class KillBox : MonoBehaviour
{
	public Obstacle obstacle;

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player" && !obstacle.SuccessfulInteraction)
		{
			World.GameOver();
		}
	}
}
