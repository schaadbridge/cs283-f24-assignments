using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Mushrooms: " + 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateScore(int playerScore) {
        Debug.Log("Updating UI Score to " + playerScore);
        _scoreText.text = "Mushrooms: " + playerScore.ToString();
    }
}
