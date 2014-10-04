using UnityEngine;
using System.Collections;

public class BaseGameController : MonoBehaviour {

	private bool paused;

	public virtual void PlayerDied () {	}

	public virtual void PlayerDamaged () { }

	public virtual void SpawnPlayer () { }

	public virtual void Respawn () { }

	public virtual void PauseGame () { }

	public virtual void ResumeGame () { }

	public virtual void RestartGame () { }

	public virtual void StopGame () { }

	public bool Paused
	{
		get 
		{
			return paused;
		}

		set
		{
			paused = value;
			if (paused)
				PauseGame ();
			else
				ResumeGame ();
		}
	}


}
