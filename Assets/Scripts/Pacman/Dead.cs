using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dead : MonoBehaviour
{
    public int lives = 3;
    public bool dead = false;
    Animator anim;

    float timer = 0f;

    public Object lifePrefab;

    Vector3 lifePos = new Vector3(-22f, -14f, 0f);

    List<Object> lifeHolder = new List<Object>();

    void Awake()
    {
        anim = GetComponent<Animator>();

        for(int i = 0; i < lives - 1; i++)
        {
            lifeHolder.Add(Instantiate(lifePrefab, lifePos + new Vector3(i * 1.5f, 0f, 0f), Quaternion.identity));
        }
    }

    private void Update()
    {
        if(dead && lives > 1)
        {
            timer += Time.fixedDeltaTime;
            timer %= 60;

            if(timer > 6f && timer < 22f)
            {
                ReloadLevel();
                GameObject.Find("Ready").GetComponent<SpriteRenderer>().enabled = true;
                anim.Play("Closed");
                Time.timeScale = 0f;

                if(timer > 8f && timer < 8.05f)
                {
                    FindObjectOfType<AudioManager>().Play("starting");
                }
            } else if(timer > 22f)
            {
                lives--;
                Destroy(lifeHolder[lives - 1]);
                GameObject.Find("Ready").GetComponent<SpriteRenderer>().enabled = false;
                dead = false;
                timer = 0f;
                Time.timeScale = 1f;
            }
        } else if(dead && lives == 1)
        {
            lives--;
            anim.Play("Die");
            GameObject.Find("Enemy").SetActive(false);

            GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("Game Over Text1").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("Game Over Text2").GetComponent<SpriteRenderer>().enabled = true;

            //SceneManager.LoadScene("Main Menu");

            Time.timeScale = 0f;
        }
    }

    void ReloadLevel()
    {
        transform.position = new Vector3(0f, -8f, 0f);
        transform.localScale = Vector3.one;
        transform.eulerAngles = Vector3.zero;

        GetComponent<CharacterMovement>().moveDirection = Vector3.left;

        GetComponent<CharacterMovement>().playerNode = GetComponent<CharacterMovement>().grid.NodeFromWorldPoint(transform.position);
        GetComponent<CharacterMovement>().neighbours = GetComponent<CharacterMovement>().grid.GetNeighbours(GetComponent<CharacterMovement>().playerNode);

        GameObject.Find("Blinky").GetComponent<Transform>().position = new Vector3(0f, 4f, 0f);
        GameObject.Find("Inky").GetComponent<Transform>().position = new Vector3(-2f, 1f, 0f);
        GameObject.Find("Pinky").GetComponent<Transform>().position = new Vector3(0f, 1f, 0f);
        GameObject.Find("Clyde").GetComponent<Transform>().position = new Vector3(2f, 1f, 0f);

        for (int i = 0; i < 4; i++)
        {
            GameObject.Find("Enemy").transform.GetChild(i).gameObject.GetComponent<GhostAI>().Reset();

        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Enemy") && !dead)
        {
            if(collider.GetComponent<GhostAI>().mode != "scared" && collider.GetComponent<GhostAI>().mode != "eaten")
            {
                FindObjectOfType<AudioManager>().Play("die");

                dead = true;

                for(int i = 0; i < 4; i++)
                {
                    GameObject.Find("Enemy").transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().enabled = false;
                }

                if(Fruit.instance != null)
                {
                    Destroy(GameObject.FindGameObjectWithTag("Fruit"));
                }

                anim.Play("Die");
            }
        }
    }
}
