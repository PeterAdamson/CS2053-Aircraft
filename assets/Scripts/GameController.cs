using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public Text timerText;
	public Text endText;
	public Text roundText;
	public Text remainingText;
	private int round;
	private int remaining;
	private int penalty;
	private int difficulty;
	private int count;
	private int timer;
	private int startTime;
	private int elapsedTime;
	private bool gameOver;
	public GameObject air;

	// Use this for initialization
	void Start () {
		remaining = 3;
		round = 0;
		penalty = 5 * round;
		timer = 30 - penalty;
		startTime = (int) Time.time;
		elapsedTime = 0;
		timerText.text = "Time Left: " + timer.ToString();
		roundText.text = "round " + round.ToString();
		remainingText.text = "Obstacles Remaining: " + remaining.ToString();
		endText.text = "";
		gameOver = false;
	}

	// Update is called once per frame
	void Update () {
		if(gameOver == false)
		{
			SetTimerText ();
		}
		SetTimerText();
		if(Input.GetKeyDown(KeyCode.R))
		{
			SceneManager.LoadScene( SceneManager.GetActiveScene().name );
			Time.timeScale = 1;
			gameOver = false;
		}
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	void SetTimerText ()
	{
		elapsedTime = (int) Time.time - startTime;
		timer = 30 - penalty - elapsedTime;
		timerText.text = "Time Left: " + timer.ToString();
		if (timer <= 0)
		{
			endText.text = "Time's Up! You Lose! Push 'R' to Restart.";
			gameOver = true;
			Time.timeScale = 0;
		}
	}

	public void SetRemaining()
	{
		remaining = remaining - 1;
		remainingText.text = "Obstacles Remaining: " + remaining.ToString();
	}

	public void NewRound()
	{
		round += 1;
		penalty = 5 * round;
		startTime = (int) Time.time;
		elapsedTime = 0;
		remaining = 3;
		remainingText.text = "Obstacles Remaining: " + remaining.ToString();
		SetTimerText();
		roundText.text = "round " + round.ToString();
	}

	public bool getGameOver()
	{
		return gameOver;
	}
}
