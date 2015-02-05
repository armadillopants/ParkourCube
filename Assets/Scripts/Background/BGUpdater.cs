using UnityEngine;
using System.Collections.Generic;

public class BGUpdater : MonoBehaviour
{
	public int initialAmount;
	public List<GameObject> bgPrefabs;
	public float zDepth;

	public Transform cam;
	private Vector3 lastCamPosition;
	private GameObject container;

	private List<ParallaxObject> bgObjects = new List<ParallaxObject>();

	void Start()
	{
		if(container) { DestroyImmediate(container); }
		bgObjects.Clear();

		container = new GameObject("BG Container");
		container.transform.position = cam.position;
		container.transform.parent = cam.transform;

		lastCamPosition = cam.position;
		PopulateScene();
	}

	void Update()
	{
		Vector3 delta = cam.position - lastCamPosition;
		delta.x *= -1f;
		delta.z = 0f;

		foreach(ParallaxObject bgObj in bgObjects)
		{
			bgObj.UpdatePosition(delta);
		}

		lastCamPosition = cam.position;
	}

	void PopulateScene()
	{
		System.Random random = new System.Random();
		for(int i=0; i<initialAmount; ++i)
		{
			Vector3 camPos = new Vector3(Random.value, Random.value, zDepth);
			Vector3 worldPos = cam.camera.ViewportToWorldPoint(camPos);

			int val = random.Next(0, bgPrefabs.Count);
			GameObject instance = Instantiate(bgPrefabs[val], worldPos, Quaternion.identity) as GameObject;

			//if(!instance) { continue; }
			
			ParallaxObject pObj = instance.GetComponent<ParallaxObject>();
			pObj.RollRandomLevel();

			instance.transform.parent = container.transform;

			bgObjects.Add(pObj);
		}
	}
}
