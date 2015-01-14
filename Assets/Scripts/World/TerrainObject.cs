using UnityEngine;

public class TerrainObject : WorldObject
{
	private bool wasVisible;

	public override void Update()
	{
		base.Update();

		if(!wasVisible && myRenderer.isVisible) { wasVisible = true; }
		if(wasVisible && !myRenderer.isVisible) { deletionFlag = true; }
	}

	public override void Reset()
	{
		base.Reset();
		wasVisible = false;
	}
}