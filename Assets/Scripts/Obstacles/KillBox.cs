using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class KillBox : MonoBehaviour
{
	public Obstacle obstacle;

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player" && !obstacle.SuccessfulInteraction)
		{
			if (gameObject.RootGameObject().name == "Pull Up Flat")
			{
				GameManager.Instance.ReportFall("You didn't clear the gap, and fell to your death.");
			}
			else if (gameObject.RootGameObject().name == "Roll Flat")
			{
				GameManager.Instance.ReportFall("You didn't roll out in time, and fell to your death.");
			}
			other.GetComponent<Player>().canMove = false;
			World.GameOver();
		}
	}
}
