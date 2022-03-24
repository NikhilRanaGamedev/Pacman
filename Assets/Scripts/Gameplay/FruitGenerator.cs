using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitGenerator : MonoBehaviour
{
    public List<Object> fruitPrefab = new List<Object>();

    int fruitIndex;

    bool instantiated1 = false;
    bool instantiated2 = false;

    private void Start()
    {
        fruitIndex = LevelManager.fruit;
    }

    private void Update()
    {
        GenerateFruit();
    }

    void GenerateFruit()
    {
        int food = FindObjectOfType<FoodOver>().foodCounter;

        if (food == 170 && !instantiated1)
        {
            instantiated1 = true;
            Instantiate(fruitPrefab[fruitIndex]);
        }

        if (food == 70 && !instantiated2)
        {
            instantiated2 = true;
            Instantiate(fruitPrefab[fruitIndex]);
        }
    }
}
