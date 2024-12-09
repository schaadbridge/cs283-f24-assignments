using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    public int scoreGoal;
    private int levelToChange;

    private PlayerMotionController _motionController;
    private TimerScript _timer;
    private MainMenuScript _menuScript;
    // Start is called before the first frame update
    void Start()
    {
        levelToChange = SceneManager.GetActiveScene().buildIndex + 1;
        _motionController = GameObject.Find("Gecko_A07").GetComponent<PlayerMotionController>();
        _timer = GameObject.Find("LevelTimer").GetComponent<TimerScript>();
        _menuScript = GameObject.Find("HeadsUpDisplays").GetComponent<MainMenuScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // if score > scoregoal
        if (_motionController._score == scoreGoal)
        {
            SceneManager.LoadScene(levelToChange);
            Debug.Log(SceneManager.GetSceneByBuildIndex(levelToChange).name);
        }

        if (_timer.totalLevelTime <= 0)
        {
            _menuScript.GameOver();
        }
        // if out of time: show out of time!!
            // PAUSE
            // GAMEOVERSCREEN
    }
}
