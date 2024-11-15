using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI label;
    // Start is called before the first frame update
    void Start()
    {
        label.text = "Mushrooms: " + 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateScore(int playerScore) {
        Debug.Log("Updating UI Score to " + playerScore);
        label.text = "Mushrooms: " + playerScore.ToString();
    }
}
