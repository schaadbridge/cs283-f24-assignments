using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public Color TeamColor; // new variable declared
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("QUIT!!!");
            Application.Quit();
        }

    }
}
