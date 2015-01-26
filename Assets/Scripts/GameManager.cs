using UnityEngine;
using UnityEngine.UI;

public class GameManager : MSingleton<GameManager>
{
	public Canvas menu;
	public Canvas score;
	public Canvas postGame;
	public Canvas pause;

	private bool isPaused;

	void Start()
	{
		menu.enabled = true;
		score.enabled = false;
		postGame.enabled = false;
		pause.enabled = false;
	}

	void Update()
	{
		// Temp
		if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
		{
			OnPause();
		}
	}

	public void OnPlayButton()
	{
		menu.enabled = false;
		score.enabled = true;
		World.CreateNewWorld();
	}

	void OnPause()
	{
		Time.timeScale = 0f;
		pause.enabled = true;
		isPaused = true;
	}

	public void OnResume()
	{
		Time.timeScale = 1f;
		pause.enabled = false;
		isPaused = false;
	}

	public void OnQuitButton()
	{
		Application.Quit();
	}

	public void OnGameOver()
	{
		score.enabled = false;
		postGame.enabled = true;
	}

	public void OnGameOverRestart()
	{
		score.enabled = true;
		postGame.enabled = false;
		ResetScore();
		World.Clear();
		World.CreateNewWorld();
	}

	public void OnGameOverBack()
	{
		postGame.enabled = false;
		menu.enabled = true;
		ResetScore();
		World.Clear();
	}

	public void UpdateScore(int playerScore)
	{
		score.transform.GetChild(1).GetComponentInChildren<Text>().text = playerScore.ToString();
	}

	public void DisplayFinalScore(int playerScore)
	{
		postGame.transform.GetChild(1).GetComponentInChildren<Text>().text = playerScore.ToString();
	}

	private void ResetScore()
	{
		score.transform.GetChild(1).GetComponentInChildren<Text>().text = "0";
	}
}
