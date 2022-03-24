using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            Score.score += 10;
            GameObject.FindGameObjectWithTag("Player").GetComponent<FoodOver>().foodCounter--;
            GameObject.FindGameObjectWithTag("Player").GetComponent<FoodOver>().CheckIfFoodIsFinished();
            Destroy(gameObject);
        }
    }
}
