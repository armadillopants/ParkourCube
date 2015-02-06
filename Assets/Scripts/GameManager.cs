using UnityEngine;
using UnityEngine.UI;

public class GameManager : MSingleton<GameManager>
{
	public GameObject menu;
	public GameObject score;
	public GameObject postGame;
	public GameObject pause;
	public GameObject pauseButton;

	public GameObject perfectObject;
	public GameObject normalObject;
	public GameObject comboObject;
	private GameObject combo;

	public static int multiplier;

	private string fellToDeath;
	private string[] causeOfDeath = new string[] { "The Doom Wall was too quick!", "You weren't fast enough.", "Epic Fail.", "Oops, you died.", 
														"You choked!", "You lost the battle, but not the war!", "Run faster next time.", "You lost." };

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
		multiplier = 0;
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
		multiplier = 0;
		//World.Clear();
		Application.LoadLevel(0);
	}

	public void UpdateScore(int playerScore)
	{
		score.transform.GetChild(1).GetComponentInChildren<Text>().text = playerScore.ToString();
	}

	public void DisplayFinalScore(int playerScore)
	{
		Text finalScore = postGame.transform.GetChild(1).GetComponentInChildren<Text>();
		finalScore.text = playerScore.ToString();
		int newScore = int.Parse(finalScore.text);

		Text highScore = postGame.transform.GetChild(3).GetComponentInChildren<Text>();
		int myScore = int.Parse(highScore.text);

		if (newScore > myScore)
		{
			highScore.text = playerScore.ToString();
			PlayerPrefs.SetString("HighScore", highScore.text);
		}

		Text causeOfDeathText = postGame.transform.GetChild(4).GetComponent<Text>();

		if (!fellToDeath.IsNullOrEmpty())
		{
			causeOfDeathText.text = fellToDeath;
			fellToDeath = "";
		}
		else
		{
			int rand = Random.Range(0, causeOfDeath.Length);
			causeOfDeathText.text = causeOfDeath[rand];
		}

		Text comboText = postGame.transform.GetChild(6).GetComponent<Text>();
		comboText.text = PlayerPrefs.GetInt("Combo").ToString();

	}

	public void DisplayCombo(Vector3 pos)
	{

		if (!GameObject.Find("Combo"))
		{
			GameObject obj = (GameObject)Instantiate(comboObject);
			obj.name = comboObject.name;
			obj.transform.position = pos;
			combo = obj;
		}

		combo.transform.position = pos;

		ComboNotice notice = combo.GetComponentInChildren<ComboNotice>();
		notice.SetActive(false);
		notice.SetActive(true);

		Text comboText = combo.transform.GetChild(0).GetChild(0).GetComponent<Text>();
		comboText.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
		comboText.text = "x" + multiplier;

		int currentCombo = PlayerPrefs.GetInt("Combo", 0);
		if(multiplier > currentCombo)
		{
			PlayerPrefs.SetInt("Combo", multiplier);
		}
	}

	public void ReportFall(string fallText)
	{
		fellToDeath = fallText;
	}

	private void ResetScore()
	{
		score.transform.GetChild(1).GetComponentInChildren<Text>().text = "0";
	}
}
