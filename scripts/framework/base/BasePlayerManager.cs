using UnityEngine;
using System.Collections;

public class BasePlayerManager : MonoBehaviour {

	public bool isInit;

	public BaseUserManager userManager;

	void Awake ()
	{
		isInit = false;
		Init ();
	}

	public virtual void Init ()
	{
		userManager = gameObject.GetComponent<BaseUserManager> ();

		if (userManager == null)
			userManager = gameObject.AddComponent<BaseUserManager> ();

		isInit = true;
	}

	public virtual void FinishGame ()
	{
		userManager.IsFinished = true;
	}

	public virtual void StartGame ()
	{
		userManager.IsFinished = false;
	}
}
