using UnityEngine;

public class WorldCreator : MonoBehaviour
{
	void Start()
	{
		World.CreateNewWorld();
		Destroy(gameObject);
	}
}
