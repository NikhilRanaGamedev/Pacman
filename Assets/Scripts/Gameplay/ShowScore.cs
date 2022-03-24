using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour
{
    private Text showScore;

    void Start()
    {
        showScore = GetComponent<Text>();
    }

    void Update()
    {
        showScore.text = "Score: " + Score.score.ToString();
    }
}
