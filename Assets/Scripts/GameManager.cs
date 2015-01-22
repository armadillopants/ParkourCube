using UnityEngine;
using System.Collections;

public class GameManager : MSingleton<GameManager>
{
	public Canvas menu;
	public Canvas score;
	public Canvas postGame;

	void Start()
	{
		menu.enabled = true;
		score.enabled = false;
		postGame.enabled = false;
	}

	public void OnPlayButton()
	{
		menu.enabled = false;
		score.enabled = true;
		World.CreateNewWorld();
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
		World.Clear();
		World.CreateNewWorld();
	}

	public void OnGameOverBack()
	{
		postGame.enabled = false;
		menu.enabled = true;
		World.Clear();
	}
}
