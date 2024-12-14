using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadsUpDisplays : MainMenuScript
{
    [SerializeField]
    private GameObject _gameOverScreen;

    public void GameOver()
    {
        PauseGame();
        _gameOverScreen.SetActive(true);
    }


    public void PauseGame()
    {
        Time.timeScale = 0.0f;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
    }
}
