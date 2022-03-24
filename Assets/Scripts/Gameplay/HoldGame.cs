using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldGame : MonoBehaviour
{
    bool hold = true;

    float timer = 0f;

    private void Awake()
    {
        GameObject.Find("Ready").GetComponent<SpriteRenderer>().enabled = true;
    }

    private void Update()
    {
        if(hold)
        {
            Hold();
        }
    }

    void Hold()
    {
        timer += Time.fixedDeltaTime;
        timer %= 60f;

        if(timer > 16f)
        {
            timer = 0f;
            hold = false;
            GameObject.Find("Ready").GetComponent<SpriteRenderer>().enabled = false;
            Time.timeScale = 1f;
            FindObjectOfType<AudioManager>().Play("siren");
        }
    }
}
