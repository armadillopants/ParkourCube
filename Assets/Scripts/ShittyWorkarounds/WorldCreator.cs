using UnityEngine;

public class WorldCreator : MonoBehaviour
{
	void Start()
	{
		WorldObject.Load();
		World.CreateNewWorld();
	}
}
