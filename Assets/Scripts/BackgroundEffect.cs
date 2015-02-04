using UnityEngine;

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

		newScale = new Vector3(Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f));
		transform.localScale = newScale;

		newColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
		renderer.material.color = newColor;

		InvokeRepeating("SwitchEffect", 3f, 3f);
	}

	void LateUpdate()
	{
		transform.localScale = Vector3.Lerp(transform.localScale, newScale, 0.5f * Time.deltaTime);
		renderer.material.color = Color.Lerp(renderer.material.color, newColor, 0.5f * Time.deltaTime);

		transform.Rotate(Vector3.right, rotateSpeed * Time.deltaTime);
		transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
	}

	void SwitchEffect()
	{
		newScale = new Vector3(Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f));
		newColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
	}
}
