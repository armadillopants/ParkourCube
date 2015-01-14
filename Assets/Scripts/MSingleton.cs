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
				GameObject obj = new GameObject(typeof(T).ToString());
				instance = obj.AddComponent<T>();
			}
			return instance;
		}
	}
}
