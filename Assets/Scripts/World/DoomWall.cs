using UnityEngine;

public class DoomWall : MonoBehaviour
{
	private Camera cam;

	public float creepSpeed;
	public float pushback;

	public float creepScoreModifier;
	public float maxModifier;

	private float offset;

	public bool canMove;

	void Start()
	{
		cam = Camera.main;
		canMove = true;
	}

	void Update()
	{
		if (canMove)
		{
			float val = 1f + Mathf.Clamp(World.Instance.playerScore / creepScoreModifier, 0f, maxModifier);
			offset += creepSpeed * val * Time.deltaTime;

			Vector3 newPosition = cam.ViewportToWorldPoint(new Vector3(offset, 0.5f, 0));
			newPosition.z = transform.position.z;
			newPosition.x += offset;
			transform.position = newPosition;
		}
	}

	public void PushBack()
	{
		offset = Mathf.Clamp(offset, 0f, offset - pushback);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			other.GetComponent<Player>().canMove = false;
			World.GameOver();
		}
	}
}
