using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

	public string[] levelNames;
	public int currentLevelID;

	void Start () 
	{
		DontDestroyOnLoad (this.gameObject);
	}

	public void LoadLevel (string name)
	{
		for (int i = 0; i < levelNames.Length; i++)
		{
			if (levelNames[i] == name)
			{
				currentLevelID = i;
				break;
			}
		}
		Application.LoadLevel (name);
	}

	public void LoadLevel (int id)
	{
		if (id < levelNames.Length)
		{
			currentLevelID = id;
			Application.LoadLevel (levelNames [id]);
		}
	}

	public void NextLevel ()
	{
		if (currentLevelID >= levelNames.Length)
			currentLevelID = 0;
		else
			Application.LoadLevel (levelNames [++currentLevelID]);
	}

	public void ResetGame ()
	{
		currentLevelID = 0;
	}

	public void ResetLevel ()
	{
		Application.LoadLevel (levelNames [currentLevelID]);
	}
}
