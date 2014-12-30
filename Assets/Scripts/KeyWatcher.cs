using UnityEngine;

public class KeyWatcher
{
	private KeyCode key;
	private bool previous;
	private bool current;

	public KeyWatcher(KeyCode key)
	{
		this.key = key;
	}

	public void Update()
	{
		previous = current;
		current = Input.GetKey(key);
	}

	public bool Down()
	{
		return (!previous && current);
	}

	public bool Up()
	{
		return (previous && !current);
	}

	public bool Pressed()
	{
		return current;
	}
}