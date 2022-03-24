using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public static Fruit instance;

    public Object fruitPointsPrefab;
    public int points;

    float randomTime;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        randomTime = Random.Range(9f, 10f);
    }

    private void Update()
    {
        StartCoroutine(DestroyFruit());
    }

    IEnumerator DestroyFruit()
    {
        yield return new WaitForSeconds(randomTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().Play("eatFruit");
            Score.score += points;
            Destroy(Instantiate(fruitPointsPrefab), 1f);
            Destroy(gameObject);
        }
    }
}
