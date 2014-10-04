using UnityEngine;
using System.Collections;

public class BaseUserManager : MonoBehaviour {

	private int score;
	private int highScore;
	private int health;
	private int maxHealth;
	private int experience;
	private bool isFinished;

	public string playerName = "Player";

	public virtual void SetDefaultData ()
	{
		score = 0;
		highScore = 0;
		health = 1;
		experience = 0;
		isFinished = false;
		playerName = "Player";
	}

	public void AddScore (int amount)
	{
		score += amount;
		if (score > highScore)
			highScore = score;
	}

	public void ReduceScore (int amount)
	{
		score -= amount;
	}

	public void AddExp (int amount)
	{
		experience += amount;
	}

	public virtual int GetLevel { get; set; }

	public virtual void SetLevel () { }

	public string PlayerName
	{
		get { return playerName; }
		set { playerName = value; }
	}

	public int Score
	{
		get { return score; }
		set { score = value; }
	}

	public int HighScore
	{
		get { return highScore; }
		set { HighScore = value; }
	}

	public int Health
	{
		get { return health; }
		set 
		{ 
			if (value <= maxHealth) 
				health = value;
			else
				health = maxHealth; 
		}
	}

	public int MaxHealth
	{
		get { return maxHealth; }
		set { maxHealth = value; }
	}

	public int Experience
	{
		get { return experience; }
		set { experience = value; }
	}

	public bool IsFinished
	{
		get { return isFinished; }
		set { isFinished = value; }
	}
}
