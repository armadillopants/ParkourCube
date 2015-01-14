using UnityEngine;
using System;

public class Timer
{
	private float time;
	private float current;
	private bool realtime;
	private float realLastTime;
	private Action<Timer> callback;
	
	public Timer(float duration, bool realtime = false)
	{
		this.time = duration;
		this.current = 0f;
		this.realtime = realtime;
		this.callback = (Timer t) => { };
	}

	public Timer(float duration, Action<Timer> callback, bool realtime = false)
	{
		this.time = duration;
		this.current = 0f;
		this.realtime = realtime;
		this.callback = callback;
	}

	public bool Completed
	{
		get { return time >= current; }
	}

	public void Update()
	{
		if(realtime)
		{
			current += Time.realtimeSinceStartup - realLastTime;
			realLastTime = Time.realtimeSinceStartup;
		}
		else
		{
			current += Time.deltaTime;
		}

		if(Completed)
		{
			callback(this);
		}
	}
}