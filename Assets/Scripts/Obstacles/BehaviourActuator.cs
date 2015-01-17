using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(TriggerWatcher))]
public class BehaviourActuator : MonoBehaviour
{
	public InputType reads;
	public ParkourStateType enter;

	public bool ResolveInput(InputInfo info)
	{
		bool result = GetRelevantInput(reads, info);
		if(result)
		{
			ParkourState.CreateInstance(enter, info.Player, true);
			return true;
		}
		return false;
	}
	
	private bool GetRelevantInput(InputType type, InputInfo info)
	{
		switch(type)
		{
			case InputType.SwipeUp:
				return info.SwipeUp;
			case InputType.SwipeDown:
				return info.SwipeDown;
			case InputType.SwipeLeft:
				return info.SwipeLeft;
			case InputType.SwipeRight:
				return info.SwipeRight;
			case InputType.Tap:
				return info.Tap;
			case InputType.Hold:
				return info.Hold;
		}
		return false;
	}
}