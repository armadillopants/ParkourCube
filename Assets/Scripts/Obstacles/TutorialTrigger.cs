using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class TutorialTrigger : MonoBehaviour
{

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			Destroy(World.tutorialObject);
			PlayerPrefs.SetInt("TutorialCompleted", 1);
			GameManager.Instance.TutorialCompleted = true;
		}
	}
}
