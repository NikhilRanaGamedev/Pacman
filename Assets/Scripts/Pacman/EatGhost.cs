using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatGhost : MonoBehaviour
{
    int ghostEatingStreak = 0;
    public bool frightened = false;

    public Object points200, points400, points800, points1600;

    List<Object> prefabPoints = new List<Object>();

    float timer = 0f;
    float pauseTimer = 0f;

    Collider2D colliderEnemy;

    private void Start()
    {
        prefabPoints.Add(points200);
        prefabPoints.Add(points400);
        prefabPoints.Add(points800);
        prefabPoints.Add(points1600);
    }

    private void Update()
    {
        if (frightened)
        {
            timer += Time.deltaTime;
            timer %= 60;

            if (timer >= 8f)
            {
                ghostEatingStreak = 0;
                timer = 0;
                frightened = false;
            }
        }

        if(GetComponent<SpriteRenderer>().enabled == false && frightened)
        {
            pauseTimer += Time.fixedDeltaTime;
            pauseTimer %= 60;

            if (pauseTimer >= 2f)
            {
                pauseTimer = 0f;
                Time.timeScale = 1f;

                GetComponent<SpriteRenderer>().enabled = true;
                colliderEnemy.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Enemy"))
        {
            if(collider.GetComponent<GhostAI>().mode == "scared")
            {
                FindObjectOfType<AudioManager>().Play("eatGhost");

                colliderEnemy = collider;

                Time.timeScale = 0f;
                GetComponent<SpriteRenderer>().enabled = false;
                collider.GetComponent<SpriteRenderer>().enabled = false;

                ghostEatingStreak++;

                if(ghostEatingStreak == 1)
                {
                    Score.score += 200;
                }
                else if (ghostEatingStreak == 2)
                {
                    Score.score += 400;
                }
                else if (ghostEatingStreak == 3)
                {
                    Score.score += 800;
                }
                else if (ghostEatingStreak == 4)
                {
                    Score.score += 1600;
                }

                Instantiate(prefabPoints[ghostEatingStreak - 1], collider.GetComponent<Transform>().position, Quaternion.identity);
            }
        }
    }
}
