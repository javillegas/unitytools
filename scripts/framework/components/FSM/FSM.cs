using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FSM : MonoBehaviour 
{
	protected List<FSMState> states;
	protected List<FSMTransition> transitions;
	protected FSMState currentState;

	private FSMState tmp;

	private FSMState FindByName (string name)
	{
		foreach (FSMState state in states)
		{
			if (state.name == name)
				return state;
		}
		Debug.LogWarning("State non found. Null returned");
		return null;
	}

	void Awake ()
	{
		states = new List<FSMState> ();
	}

	void Update ()
	{
		tmp = currentState.CheckTransitions ();
		if (tmp != null)
		{
			currentState = tmp;
			currentState.Act();
		}
	}

	void Init (int id)
	{
		if (currentState == null)
			currentState = states[id];
		else
			Debug.LogWarning("Initializing state machine: state machine is already initialized.");
	}

	void Init (string name)
	{
		if (currentState == null)
			currentState = FindByName(name);
		else
			Debug.LogWarning("Initializing state machine: state machine is already initialized.");
	}

	public void AddState<T>(string name) where T : Component
	{
		FSMState state = gameObject.AddComponent<T> () as FSMState;
		state.name = name;
		states.Add (state);
	}

	public void RemoveState(string name)
	{
		int pos;
		for (int i = 0; i < states.Count; i++)
		{
			for (int j = 0; j < states[i].ConnectedStates.Count; j++)
			{
				if (states[i].ConnectedStates[j].name == name)
				{
					states[i].ConnectedStates.RemoveAt(j);
					states[i].Transitions.RemoveAt(j);
				}
			}
			if (states[i].name == name)
				pos = i;
		}
		states.RemoveAt (pos);
	}

	public void ConnectStates<T>(string fromName, string toName) where T : Component
	{
		FSMState source = null;
		FSMState destination = null;

		for (int i = 0; i < states.Count; i++)
		{
			if (states[i].name == fromName)
				source = states[i];

			if (states[i].name == toName)
				destination = states[i];
		}

		if (source == null || destination == null)
		{
			Debug.LogWarning("Connecting states: state not found.");
			return;
		}

		source.ConnectState<T> (destination);
	}
}
