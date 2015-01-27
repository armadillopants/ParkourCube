using UnityEngine;
using System.Collections;

public class Initializer : MonoBehaviour {

	public GameObject prefab;

	// Use this for initialization
	void Start () {
		if(!GameObject.Find("Game Manager(Clone)"))
		{
			Instantiate(prefab);
		}
		Destroy(this);
	}


}
