using UnityEngine;
using System.Collections;

public class Timer
{
	public bool isRunning = false;

	private float elapsedTime = 0.0f;
	private float currentTime = 0.0f;
	private float lastTime = 0.0f;
	private float scaleFactor = 1.0f;

	private string timeString;
	private string hours;
	private string minutes;
	private string seconds;
	private string millisec;

	private int hour;
	private int minute;
	private int second;
	private int msec;
	private int tmp;
	private int time;

	private GameObject callback;

	public void Update ()
	{
		elapsedTime = Mathf.Abs (Time.realtimeSinceStartup - lastTime);

		if (isRunning)
			currentTime += elapsedTime * scaleFactor;

		lastTime = Time.realtimeSinceStartup;
	}

	public void Start ()
	{
		isRunning = true;
		lastTime = Time.realtimeSinceStartup;
	}

	public void Stop ()
	{
		isRunning = false;
	}

	public void Reset ()
	{
		elapsedTime = 0.0f;
		currentTime = 0.0f;
		lastTime = Time.realtimeSinceStartup;
	}

	public float TimeSinceLastUpdate ()
	{
		elapsedTime = Mathf.Abs (Time.realtimeSinceStartup - lastTime);

		return elapsedTime;
	}

	public float GetTime ()
	{
		return currentTime;
	}

	public string GetFormattedTime ()
	{
		Update ();

		hour = (int)(currentTime / 3600);
		minute = (int)(currentTime / 60);
		second = (int)(currentTime % 60);
		msec = (int)(currentTime * 1000) % 1000;

		hours = hour.ToString ();
		minutes = minute.ToString ();
		seconds = second.ToString ();
		millisec = msec.ToString ();

		if (hours.Length < 2)
			hours = "0" + hours;

		if (minutes.Length < 2)
			minutes = "0" + minutes;

		if (seconds.Length < 2)
			seconds = "0" + seconds;

		if (millisec.Length < 3)
			if (millisec.Length < 2)
				millisec = "00" + millisec;
			else
				millisec = "0" + millisec;

		timeString = hours + ":" + minutes + ":" + seconds + "." + millisec;

		return timeString;
	}
}
