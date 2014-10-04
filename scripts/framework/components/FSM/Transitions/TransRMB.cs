using UnityEngine;
using System.Collections;

public class TransRMB : FSMTransition 
{
	public override bool Check()
	{
		if (Input.GetMouseButtonUp(1))
			return true;
		else 
			return false;
	}
}
