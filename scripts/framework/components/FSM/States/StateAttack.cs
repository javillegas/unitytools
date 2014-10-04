using UnityEngine;
using System.Collections;

public class StateAttack : FSMState 
{
	public override void Act ()
	{
		Debug.Log("Attack state");
	}
}
