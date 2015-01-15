using UnityEngine;
using System.Collections.Generic;

public class TimerManager : MSingleton<TimerManager>
{
	List<Timer> timers;

	void Awake()
	{
		timers = new List<Timer>();
	}

	void Update()
	{
		for(int i=0; i<timers.Count; ++i)//foreach(Timer timer in timers)
		{
			timers[i].Update();
			if(timers[i].Completed) { timers.Remove(timers[i]); }
		}
	}

	public void AddTimer(Timer timer)
	{
		timers.Add(timer);
	}
}
