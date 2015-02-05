using UnityEngine;
using System.Collections;

public class BackgroundEffect : MonoBehaviour
{

	private float rotateSpeed;

	private float newEffectTime;
	private bool switchingEffect;

	private Vector3 newScale;
	private Color newColor;

	void Awake()
	{
		rotateSpeed = Random.Range(5f, 20f);

		newScale = new Vector3(Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f), 1);
		transform.localScale = newScale;

		newColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
		renderer.material.color = newColor;

		InvokeRepeating("SwitchEffect", Random.Range(3f, 5f), Random.Range(1f, 3f));
	}

	void LateUpdate()
	{
		transform.localScale = Vector3.Lerp(transform.localScale, newScale, 1.5f * Time.deltaTime);
		renderer.material.color = Color.Lerp(renderer.material.color, newColor, 1.5f * Time.deltaTime);

		transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime, Space.Self);
	}

	void SwitchEffect()
	{
		newScale = new Vector3(Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f));
		newColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
	}
}
