using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void PlayCollectionGame()
    {
        SceneManager.LoadScene(0);
    }

    public void PlayExploreMode()
    {
        SceneManager.LoadScene(3);
    }

    public void OnApplicationQuit()
    {
        Debug.Log("QUIT!!");
        Application.Quit();
    }
}
