using UnityEngine;
using UnityEngine.UI;

public class GameManager : MSingleton<GameManager>
{
	public GameObject menu;
	public GameObject score;
	public GameObject postGame;
	public GameObject pause;
	public GameObject pauseButton;

	public bool TutorialCompleted
	{
		get { return tutorialCompleted; }
		set { tutorialCompleted = value; }
	}
	private bool tutorialCompleted;

	public bool GameOver
	{
		get { return gameOver; }
	}
	private bool gameOver;

	void Start()
	{
		PlayerPrefs.DeleteAll();
		DontDestroyOnLoad(this);
		menu.SetActive(true);
		score.SetActive(false);
		postGame.SetActive(false);
		pause.SetActive(false);
		pauseButton.SetActive(false);
		tutorialCompleted = PlayerPrefs.GetInt("TutorialCompleted") == 1;
	}

	public void OnPlayButton()
	{
		menu.SetActive(false);
		score.SetActive(true);
		ResetScore();
		pauseButton.SetActive(true);
		Time.timeScale = 1f;
		Application.LoadLevel(1);
		//World.CreateNewWorld();
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
		gameOver = true;
		score.SetActive(false);
		pauseButton.SetActive(false);
		postGame.SetActive(true);
	}

	public void OnGameOverRestart()
	{
		gameOver = false;
		score.SetActive(true);
		pauseButton.SetActive(true);
		postGame.SetActive(false);
		OnPlayButton();
		//World.Clear();
		//World.CreateNewWorld();
	}

	public void OnGameOverBack()
	{
		World.player = null;
		Destroy(World.player);
		gameOver = false;
		postGame.SetActive(false);
		pause.SetActive(false);
		pauseButton.SetActive(false);
		score.SetActive(false);
		menu.SetActive(true);
		//World.Clear();
		Application.LoadLevel(0);
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
