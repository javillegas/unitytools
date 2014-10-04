using UnityEngine;
using System.Collections;

public class BaseInputController : MonoBehaviour {

	public bool up;
	public bool down;
	public bool left;
	public bool right;

	public float hAxis;
	public float vAxis;

	public Vector3 tmpVector;

	private Vector3 origin = new Vector3(0,0,0);

	public virtual void CheckInput ()
	{
		hAxis = Input.GetAxis ("Horizontal");
		vAxis = Input.GetAxis ("Vertical");
	}

	public virtual float GetHorizontal ()
	{
		return hAxis;
	}

	public virtual float GetVertical ()
	{
		return vAxis;
	}

	public virtual Vector3 GetMovementDirection ()
	{
		tmpVector = origin;
		if (up || down)
			tmpVector.y = vAxis;
		if (left || right)
			tmpVector.x = hAxis;
		return tmpVector;
	}
}
