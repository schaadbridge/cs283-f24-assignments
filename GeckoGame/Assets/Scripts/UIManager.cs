using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI label;
    private LevelLoader _levelLoader;
    private string goalText = "";
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("LevelLoader")) {
            _levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
            goalText = "/" + _levelLoader.scoreGoal;
        }
        label.text = "0" + goalText;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateScore(int playerScore) {
        Debug.Log("Updating UI Score to " + playerScore);
        label.text = playerScore.ToString() + goalText;
    }
}
