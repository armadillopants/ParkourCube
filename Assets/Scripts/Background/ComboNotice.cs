using UnityEngine;

public class ComboNotice : MonoBehaviour
{

	private Vector3 newScale = Vector3.zero;
	private readonly float[] scales = new float[] { 0.8f, 1f, 1.2f, 1.5f, 1.8f, 2f };

	public void SetActive(bool setActive)
	{
		gameObject.SetActive(setActive);

		transform.localScale = new Vector3(0, 0, 1f);
		transform.eulerAngles = new Vector3(0, 0, Random.Range(-30f, 30f));

		if (setActive)
		{
			System.Random rand = new System.Random();

			newScale.x = scales[rand.Next(0, 6)];
			newScale.y = newScale.x;
			newScale.z = 1f;
		}
	}

	void Update()
	{
		LeanTween.scale(gameObject, newScale, 0.1f);
	}
}
