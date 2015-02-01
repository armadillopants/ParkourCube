using UnityEngine;
using UnityEngine.UI;

public class InteractNotice : MonoBehaviour
{

	private float speed = 3f;
	private Color color;

	void Start()
	{
		color = gameObject.GetComponentInChildren<Text>().color;
		color.r = Random.Range(0f, 255f / 255f);
		color.g = Random.Range(0f, 255f / 255f);
		color.b = Random.Range(0f, 255f / 255f);
	}

	void LateUpdate()
	{
		speed -= 2f * Time.deltaTime;

		Vector3 pos = transform.position;
		pos.y += speed * Time.deltaTime;
		transform.position = pos;

		color.a -= Time.deltaTime;

		gameObject.GetComponentInChildren<Text>().color = color;

		if (color.a <= 0)
		{
			Destroy(gameObject);
		}
	}

}
