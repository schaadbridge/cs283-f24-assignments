using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameOverScreen;

    public void EscapeToMain()
    {
        SceneManager.LoadScene(0);
    }

    public void PlayCollectionGame()
    {
        SceneManager.LoadScene(3);
    }

    public void PlayExploreMode()
    {
        SceneManager.LoadScene(2);
    }

    public void PickColor()
    {
        SceneManager.LoadScene(1);
    }

    public void PauseGame()
    {
        Time.timeScale = 0.0f;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
    }

    public void GameOver()
    {
        PauseGame();
        _gameOverScreen.SetActive(true);
    }

    public void OnApplicationQuit()
    {
        Debug.Log("QUIT!!");
        Application.Quit();
    }
}
