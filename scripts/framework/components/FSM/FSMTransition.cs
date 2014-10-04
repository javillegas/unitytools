using UnityEngine;
using System.Collections;

public abstract class FSMTransition : MonoBehaviour
{
	public new string name = "default";

	public abstract bool Check ();
}
