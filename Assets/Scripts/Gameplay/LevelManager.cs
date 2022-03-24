using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager
{
    public static int level = 1;

    const float pacmanBaseSpeed = 10f;
    const float ghostBaseSpeed = 10f;

    public static float pacmanSpeed;
    public static float pacmanAfterBigFoodSpeed;

    public static float ghostSpeed;
    public static float ghostFrightSpeed;
    public static float ghostTunnelSpeed;

    public static float ghostFrightTime;
    public static float ghostFlashes;

    public static int elroyDotsLeft1;
    public static int elroyDotsLeft2;

    public static float elroySpeed1;
    public static float elroySpeed2;

    public static int fruit;

    public static int[] chase = new int[4];
    public static int[] scatter = new int[4];

    void ChangeSpeedValues()
    {
        if (level == 1)
        {
            pacmanSpeed = Calculate(pacmanBaseSpeed, 80);
            pacmanAfterBigFoodSpeed = Calculate(pacmanBaseSpeed, 90);

            ghostSpeed = Calculate(ghostBaseSpeed, 75);
            ghostFrightSpeed = Calculate(ghostBaseSpeed, 50);
            ghostTunnelSpeed = Calculate(ghostBaseSpeed, 40);

            elroySpeed1 = Calculate(ghostBaseSpeed, 80);
            elroySpeed2 = Calculate(ghostBaseSpeed, 85);
        }
        else if (level >= 2 && level <= 4)
        {
            pacmanSpeed = Calculate(pacmanBaseSpeed, 90);
            pacmanAfterBigFoodSpeed = Calculate(pacmanBaseSpeed, 95);

            ghostSpeed = Calculate(ghostBaseSpeed, 85);
            ghostFrightSpeed = Calculate(ghostBaseSpeed, 55);
            ghostTunnelSpeed = Calculate(ghostBaseSpeed, 45);

            elroySpeed1 = Calculate(ghostBaseSpeed, 90);
            elroySpeed2 = Calculate(ghostBaseSpeed, 95);
        }
        else if (level >= 5 && level <= 20)
        {
            pacmanSpeed = pacmanBaseSpeed;
            pacmanAfterBigFoodSpeed = pacmanBaseSpeed;

            ghostSpeed = Calculate(ghostBaseSpeed, 95);
            ghostFrightSpeed = Calculate(ghostBaseSpeed, 60);
            ghostTunnelSpeed = Calculate(ghostBaseSpeed, 50);

            elroySpeed1 = Calculate(ghostBaseSpeed, 100);
            elroySpeed2 = Calculate(ghostBaseSpeed, 105);
        }
        else if (level >= 21)
        {
            pacmanSpeed = Calculate(pacmanBaseSpeed, 90);
        }
    }

    void ChangeFrightValues()
    {
        if(level == 1)
        {
            ghostFrightTime = 6f;
        }
        else if (level == 2 || level == 6 || level == 10)
        {
            ghostFrightTime = 5f;
        }
        else if (level == 3)
        {
            ghostFrightTime = 4f;
        }
        else if (level == 4 || level == 14)
        {
            ghostFrightTime = 3f;
        }
        else if (level == 5 || level == 8 || level == 9 || level == 11)
        {
            ghostFrightTime = 2f;
        }
        else if (level == 9 || level == 12 || level == 13 || level == 15 || level == 16 || level == 18)
        {
            ghostFrightTime = 1f;
        }
    }

    void ChangeFrightFlash()
    {
        if (level >= 1 && level <= 8 || level == 10 || level == 11 || level == 14)
        {
            ghostFlashes = 5;
        }
        else if (level == 9 || level == 12 || level == 13 || level == 15 || level == 16 || level == 18)
        {
            ghostFlashes = 3;
        }
    }

    void ChangeElroyDots()
    {
        if (level == 1)
        {
            elroyDotsLeft1 = 20;
            elroyDotsLeft2 = 10;
        }
        else if (level == 2)
        {
            elroyDotsLeft1 = 30;
            elroyDotsLeft2 = 15;
        }
        else if (level >= 3 && level <= 5)
        {
            elroyDotsLeft1 = 40;
            elroyDotsLeft2 = 20;
        }
        else if (level >= 6 && level <= 8)
        {
            elroyDotsLeft1 = 50;
            elroyDotsLeft2 = 25;
        }
        else if (level >= 9 && level <= 11)
        {
            elroyDotsLeft1 = 60;
            elroyDotsLeft2 = 30;
        }
        else if (level >= 12 && level <= 14)
        {
            elroyDotsLeft1 = 80;
            elroyDotsLeft2 = 40;
        }
        else if (level >= 15 && level <= 18)
        {
            elroyDotsLeft1 = 100;
            elroyDotsLeft2 = 50;
        }
        else if (level >= 19)
        {
            elroyDotsLeft1 = 120;
            elroyDotsLeft2 = 60;
        }
    }

    void ChangeFruitValues()
    {
        if (level == 1)
        {
            fruit = 0;
        }
        else if (level == 2)
        {
            fruit = 1;
        }
        else if (level == 3 || level == 4)
        {
            fruit = 2;
        }
        else if (level == 5 || level == 6)
        {
            fruit = 3;
        }
        else if (level == 7 || level == 8)
        {
            fruit = 4;
        }
        else if (level == 9 || level == 10)
        {
            fruit = 5;
        }
        else if (level == 11 || level == 12)
        {
            fruit = 6;
        }
        else if (level >= 13)
        {
            fruit = 7;
        }
    }

    void ChangeChaseScatter()
    {
        if(level == 1)
        {
            scatter[0] = 7;
            scatter[1] = 7;
            scatter[2] = 5;
            scatter[3] = 5;

            chase[0] = 20;
            chase[1] = 20;
            chase[2] = 20;
            chase[3] = -1;
        }
        else if (level > 1 && level < 5)
        {
            scatter[3] = 1/60;

            chase[2] = 1033;
        }
        else if (level > 4)
        {
            scatter[0] = 5;
            scatter[1] = 5;

            chase[2] = 1037;
        }
    }

    public void UpdateValues()
    {
        ChangeSpeedValues();
        ChangeFrightValues();
        ChangeFrightFlash();
        ChangeElroyDots();
        ChangeFruitValues();
        ChangeChaseScatter();
    }

    float Calculate(float value, int percentage)
    {
        return value / 100 * percentage;
    }
}
