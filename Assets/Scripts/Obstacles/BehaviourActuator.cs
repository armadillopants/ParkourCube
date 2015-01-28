//#define AUTOMODE
#undef AUTOMODE

using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(TriggerWatcher))]
public class BehaviourActuator : MonoBehaviour
{
	public InputType reads;
	public ParkourStateType onRead;

	public ParkourStateType onEnter;

	public bool ResolveInput(Player player)
	{
#if AUTOMODE
		ParkourState.CreateInstance(onRead, player, true);
		return true;
#else
		bool result = GetRelevantInput();
		if(result)
		{
			ParkourState.CreateInstance(onRead, player, true);
			return true;
		}
		return false;
#endif
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

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			Player player = other.GetComponent<Player>();
			ParkourState.CreateInstance(onEnter, player, true);
		}
	}
}