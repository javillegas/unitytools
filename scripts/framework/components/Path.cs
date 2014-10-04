using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Path : MonoBehaviour 
{
	public bool lookForward = false;
	public bool closed = false;
	public Color color = Color.green;

	public List<Vector3> route;

	private float length;
	private int segmentNumber;
	private Vector3 currentSegment;
	private Vector3 currentPosition;

	public void AddWaypoint (Vector3 position)
	{
		if (route == null)
		{
			route = new List<Vector3> ();
			route.Add (position);
			currentPosition = position;
			segmentNumber = 0;
		}
		else
			route.Add (position);
	}

	public void InsertWaypoint (Vector3 position, int index)
	{
		route.Insert(index, position);
	}

	public void DeleteWaypoint (int index)
	{
		route.RemoveAt(index);
	}

	public void JumpToStart ()
	{
		transform.position = route [0];
		currentPosition = route [0];
		currentSegment = route [1] - route [0];
		segmentNumber = 0;
	}

	public void JumpToEnd ()
	{
		if (closed)
		{
			transform.position = route [0];
			currentPosition = route [0];
			currentSegment = route [0] - route [route.Count - 1];
			segmentNumber = route.Count - 1;
		}
		else
		{
			transform.position = route [route.Count - 1];
			currentPosition = route [route.Count - 1];
			currentSegment = route [route.Count - 1] - route [route.Count - 2];
			segmentNumber = route.Count - 2;
		}
	}

	public void JumpToWaypoint (int index)
	{
		transform.position = route [index];
		currentPosition = route [index];

		if (index < route.Count)
			segmentNumber = index;
		else if (index == route.Count - 1)
		{
			if (closed)
				segmentNumber = 0;
			else
				segmentNumber = index - 1;
		}

		currentSegment = route [(segmentNumber + 1) % route.Count] - route [segmentNumber];
	}

	public void JumpToLastPosition ()
	{
		transform.position = currentPosition;
	}

	public void StepForward (float velocity)
	{
		Vector3 newPosition = currentPosition + velocity * currentSegment.normalized * Time.deltaTime;
		Vector3 step = newPosition - currentPosition;
		Vector3 toTurn = route [(segmentNumber + 1) % route.Count] - currentPosition;

		if (step.magnitude > toTurn.magnitude)
		{
			if (closed && segmentNumber == route.Count-1) 
			{
				JumpToStart();
				return;
			}
			if (!closed && segmentNumber == route.Count-2)
				return;

			newPosition = route [segmentNumber + 1];
			currentSegment = route [(segmentNumber + 2) % route.Count] - route [segmentNumber + 1];
			segmentNumber++;
		}

		currentPosition = newPosition;
		transform.position = newPosition;

		if (lookForward)
			transform.LookAt(route[(segmentNumber + 1) % route.Count]);
	}

	public void StepBackward (float velocity)
	{
		Vector3 newPosition = currentPosition - velocity * currentSegment.normalized * Time.deltaTime;
		Vector3 step = newPosition - currentPosition;
		Vector3 toTurn = route [segmentNumber] - currentPosition;
		
		if (step.magnitude > toTurn.magnitude)
		{
			if (closed && segmentNumber == 0) 
			{
				JumpToEnd();
				return;
			}
			if (!closed && segmentNumber == 0)
			{
				return;
			}
			
			newPosition = route [segmentNumber];
			currentSegment = route [segmentNumber] - route [segmentNumber - 1];
			segmentNumber--;
		}
		
		currentPosition = newPosition;
		transform.position = newPosition;

		if (lookForward)
			transform.LookAt(-route[(segmentNumber + 1) % route.Count]);
	}

	public float Length
	{
		get { return length; }
	}

	public List<Vector3> Route
	{
		get { return route; }
	}

	private void CalculateLength ()
	{
		length = 0;
		for (int i = 0; i < route.Count-1; i++)
		{
			Vector3 segment = route[i+1] - route[i];
			length += segment.magnitude;
		}
	}
}
