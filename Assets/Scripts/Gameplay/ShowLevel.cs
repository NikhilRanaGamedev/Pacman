using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowLevel : MonoBehaviour
{
    private Text showScore;

    void Start()
    {
        showScore = GetComponent<Text>();
        showScore.text = "Level:\n" + LevelManager.level;
    }
}
