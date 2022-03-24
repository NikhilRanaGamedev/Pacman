using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinky : GhostAI
{
    int elroyDots1;
    int elroyDots2;

    float elroySpeed1;
    float elroySpeed2;

    new void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        scatterPos = new Vector3(12.5f, 16f, 0.0f);
        ghostColor = Color.red;
        myMonsterPen = new Vector3(0f, 1f, 0f);
        waitTimer = 0f;

        elroyDots1 = LevelManager.elroyDotsLeft1;
        elroyDots2 = LevelManager.elroyDotsLeft2;

        elroySpeed1 = LevelManager.elroySpeed1;
        elroySpeed2 = LevelManager.elroySpeed2;

        base.Start();
    }

    new void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        CruiseElroy();
        base.Update();
    }

    void CruiseElroy()
    {
        int dotsLeft = GameObject.Find("Pacman").GetComponent<FoodOver>().foodCounter;

        if (dotsLeft <= elroyDots1 && dotsLeft > elroyDots2 && (mode == "chase" || mode == "scatter"))
        {
            if (mode == "chase" || mode == "scatter")
            {
                if (dotsLeft > elroyDots2)
                {
                    speed = elroySpeed1;
                }
                else if (dotsLeft <= elroyDots2)
                {
                    speed = elroySpeed2;
                }
            }
            else if (mode == "scared")
            {
                speed = LevelManager.ghostFrightSpeed;
            }
            else if (mode == "eaten")
            {
                speed = LevelManager.ghostSpeed * 2.5f;
            }
        }
    }
}
