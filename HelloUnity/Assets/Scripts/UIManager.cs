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
    private float goal;
    // Start is called before the first frame update
    void Start()
    {
        _levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
        goal = _levelLoader.scoreGoal;
        label.text = "Mushrooms: " + 0 + "/" + goal;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateScore(int playerScore) {
        Debug.Log("Updating UI Score to " + playerScore);
        label.text = "Mushrooms: " + playerScore.ToString() + "/" + goal;
    }
}
