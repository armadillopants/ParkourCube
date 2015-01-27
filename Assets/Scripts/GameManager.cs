using UnityEngine;
using UnityEngine.UI;

public class GameManager : MSingleton<GameManager>
{
	public GameObject menu;
	public GameObject score;
	public GameObject postGame;
	public GameObject pause;
	public GameObject pauseButton;

	private bool isPaused;

	void Start()
	{
		menu.SetActive(true);
		score.SetActive(false);
		postGame.SetActive(false);
		pause.SetActive(false);
		pauseButton.SetActive(false);
	}

	public void OnPlayButton()
	{
		menu.SetActive(false);
		score.SetActive(true);
		World.CreateNewWorld();
	}

	public void OnPause()
	{
		Time.timeScale = 0f;
		pause.SetActive(true);
		pauseButton.SetActive(false);
	}

	public void OnResume()
	{
		Time.timeScale = 1f;
		pause.SetActive(false);
		pauseButton.SetActive(true);
	}

	public void OnQuitButton()
	{
		Application.Quit();
	}

	public void OnGameOver()
	{
		score.SetActive(false);
		pauseButton.SetActive(false);
		postGame.SetActive(true);
	}

	public void OnGameOverRestart()
	{
		score.SetActive(true);
		pauseButton.SetActive(true);
		postGame.SetActive(false);
		ResetScore();
		World.Clear();
		World.CreateNewWorld();
	}

	public void OnGameOverBack()
	{
		postGame.SetActive(false);
		pauseButton.SetActive(false);
		menu.SetActive(true);
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
