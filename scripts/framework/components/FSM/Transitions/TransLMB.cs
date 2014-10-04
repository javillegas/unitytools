using UnityEngine;
using System.Collections;

public class TransLMB : FSMTransition 
{
	public override bool Check()
	{
		if (Input.GetMouseButtonUp(0))
			return true;
		else
			return false;
	}
}
