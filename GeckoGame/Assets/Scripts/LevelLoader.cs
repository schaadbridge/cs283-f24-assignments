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
    private HeadsUpDisplays _headsUpDisplayScript;
    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        levelToChange = SceneManager.GetActiveScene().buildIndex + 1;
        _motionController = GameObject.Find("Gecko_A07").GetComponent<PlayerMotionController>();
        _timer = GameObject.Find("LevelTimer").GetComponent<TimerScript>();
        _headsUpDisplayScript = GameObject.Find("HeadsUpDisplays").GetComponent<HeadsUpDisplays>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.Play();
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
            _headsUpDisplayScript.GameOver();
        }
    }
}
