using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	private Transform target;
	private float followSpeed;

	void Start()
	{
		target = GameObject.FindGameObjectWithTag("Player").transform;
		followSpeed = 10f;
	}

	void FixedUpdate()
	{
		if (target)
		{
			transform.position = Vector2.Lerp(transform.position, target.position, followSpeed * Time.fixedDeltaTime);
			transform.position -= new Vector3(0, 0, 10);
		}
	}
}
