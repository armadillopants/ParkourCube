using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(TriggerWatcher))]
public class BehaviourActuator : MonoBehaviour
{
	public InputType reads;
	public ParkourStateType enter;

	private bool wasHolding;
	public ParkourStateType onReleaseEnter;

	public void ResolveInput(InputInfo info)
	{
		bool hold = GetRelevantInput(InputType.Hold, info);
		if(wasHolding && !hold)
		{
			ParkourState.CreateInstance(onReleaseEnter, info.Player, true);
		}
		wasHolding = hold;

		bool result = GetRelevantInput(reads, info);
		if(result)
		{
			ParkourState.CreateInstance(enter, info.Player, true);
		}
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