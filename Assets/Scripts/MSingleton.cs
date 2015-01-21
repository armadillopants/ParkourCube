using UnityEngine;

public class MSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T instance;

	public static T Instance
	{
		get
		{
			if(!instance)
			{
				// Try to find one in the scene first
				T existing = GameObject.FindObjectOfType<T>();
				if(existing) { instance = existing; }
				else
				{
					// If not create a new one
					GameObject obj = new GameObject(typeof(T).ToString());
					instance = obj.AddComponent<T>();
				}
			}
			return instance;
		}
	}
}
