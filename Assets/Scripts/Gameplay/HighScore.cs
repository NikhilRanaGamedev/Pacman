using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    private Text showScore;

    void Start()
    {
        showScore = GetComponent<Text>();
        showScore.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
}
