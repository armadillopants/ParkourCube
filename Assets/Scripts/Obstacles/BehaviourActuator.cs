using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(TriggerWatcher))]
public class BehaviourActuator : MonoBehaviour
{
	public InputType reads;
	public ParkourStateType enter;

	public bool ResolveInput(Player player)
	{
		bool result = GetRelevantInput();
		if(result)
		{
			ParkourState.CreateInstance(enter, player, true);
			return true;
		}
		return false;
	}
	
	private bool GetRelevantInput()
	{
		switch(reads)
		{
			case InputType.SwipeUp:
				return InputMonitor.Instance.Swipe(Vector2.up);
			case InputType.SwipeDown:
				return InputMonitor.Instance.Swipe(-Vector2.up);
			case InputType.SwipeLeft:
				return InputMonitor.Instance.Swipe(-Vector2.right);
			case InputType.SwipeRight:
				return InputMonitor.Instance.Swipe(Vector2.right);
			case InputType.Tap:
				return InputMonitor.Instance.DidTap();
			case InputType.Hold:
				return InputMonitor.Instance.Holding();
		}
		return false;
	}
}