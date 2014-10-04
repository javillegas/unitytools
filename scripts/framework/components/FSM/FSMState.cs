using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class FSMState : MonoBehaviour
{
	public new string name = "default";
	
	protected List<FSMState> connectedStates;
	protected List<FSMTransition> transitions;

	public abstract void Act ();

	public FSMState()
	{
		connectedStates = new List<FSMState> ();
		transitions = new List<FSMTransition> ();
	}

	public List<FSMState> ConnectedStates
	{
		get { return connectedStates; }
	}

	public List<FSMTransition> Transitions
	{
		get { return transitions; }
	}

	public FSMState CheckTransitions ()
	{
		for (int i = 0; i < connectedStates.Count; i++)
		{
			if (transitions[i].Check())
				return connectedStates[i];
		}
		return null;
	}

	public void ConnectState<T> (FSMState state) where T : Component
	{
		FSMTransition transition = gameObject.GetComponent<T> () as FSMTransition;

		if (transition == null)
			transition = gameObject.AddComponent<T> () as FSMTransition;

		connectedStates.Add (state);
		transitions.Add (transition);
	}

	public void DisconnectState (string stateName)
	{
		for (int i = 0; i < connectedStates.Count; i++)
		{
			if (connectedStates[i].name == stateName)
			{
				connectedStates.RemoveAt(i);
				transitions.RemoveAt(i);
				return;
			}
		}
		Debug.LogWarning ("Desconnecting state: state not found.");
	}

	public void ChangeTransition (string stateName, FSMTransition transition)
	{
		for (int i = 0; i < connectedStates.Count; i++)
		{
			if (connectedStates[i].name == stateName)
			{
				transitions[i] = transition;
				return;
			}
		}
		Debug.LogWarning ("Changing transition: state not found.");		
	}
}
