using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigFood : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            player.GetComponent<EatGhost>().frightened = true;

            Score.score += 50;
            GameObject.FindGameObjectWithTag("Player").GetComponent<FoodOver>().foodCounter--;
            GameObject.FindGameObjectWithTag("Player").GetComponent<FoodOver>().CheckIfFoodIsFinished();

            for(int i = 0; i < 4; i++)
            {
                GameObject childObject = GameObject.Find("Enemy").transform.GetChild(i).gameObject;

                if(LevelManager.level <= 16 || LevelManager.level == 18)
                {
                    if (childObject.GetComponent<GhostAI>().eaten &&
                   childObject.GetComponent<Transform>().position == childObject.GetComponent<GhostAI>().myMonsterPen)
                    {
                        childObject.GetComponent<Animator>().Play("Frightened");
                    }
                    else
                    {
                        childObject.GetComponent<GhostAI>().TurnCalculation("scared");
                    }
                } else
                {
                    childObject.GetComponent<GhostAI>().TurnCalculation("chase");
                }
            }

            Destroy(gameObject);
        }
    }
}
