using UnityEngine;
using System.Collections;

public class StateIdle : FSMState 
{
	public override void Act()
	{
		Debug.Log("Idle state");
	}
}
