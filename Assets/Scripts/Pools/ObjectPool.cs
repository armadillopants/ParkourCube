using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

public class ObjectPool
{
	public GameObject prefab;

	public Stack<GameObject> pool;

	public ObjectPool(GameObject obj, int capacity, bool preload)
	{
		prefab = obj;
		pool = new Stack<GameObject>(capacity);

		if(preload)
		{
			for(int i=0; i<capacity; ++i)
			{
				CreateNewInstance();
			}
		}
	}

	public GameObject Get()
	{
		if(pool.Count == 0)
		{
			CreateNewInstance();
		}

		GameObject instance = pool.Pop();
		instance.SetActive(true);
		return instance;
	}

	public void Return(PooledObject obj)
	{
		obj.gameObject.SetActive(false);
		var scripts = obj.GetComponentsInChildren<MonoBehaviour>().Where(x => x is IPoolable);
		foreach(MonoBehaviour script in scripts)
		{
			((IPoolable)script).Reset();
		}
		pool.Push(obj.gameObject);
		Debug.Log("Object returned to pool: " + obj.name);
	}

	public void CreateNewInstance()
	{
		GameObject instance = (GameObject)GameObject.Instantiate(prefab);
		PooledObject pObj = instance.GetComponent<PooledObject>();

		if(!pObj)
		{
			pObj = instance.AddComponent<PooledObject>();
			Debug.LogWarning("No PooledObject script found on " + prefab.name);
		}
		pObj.SetPool(this);
		instance.SetActive(false);
		pool.Push(instance);
	}
}