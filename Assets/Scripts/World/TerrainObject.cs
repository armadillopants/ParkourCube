using UnityEngine;

public class TerrainObject : WorldObject
{
	private const float DELETE_LEFT_DIST = 20f;
	//private bool wasVisible;

	public override void Update()
	{
		base.Update();

		if (World.player)
		{
			if (transform.position.x < World.player.transform.position.x - DELETE_LEFT_DIST) { deletionFlag = true; }
		}

		//if(!wasVisible && myRenderer.isVisible) { wasVisible = true; }
		//if(wasVisible && !myRenderer.isVisible) { deletionFlag = true; }
	}

	public override void Reset()
	{
		base.Reset();
		//wasVisible = false;
	}
}