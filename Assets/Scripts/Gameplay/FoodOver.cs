using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FoodOver : MonoBehaviour
{
    public int foodCounter = 244;

    public void CheckIfFoodIsFinished()
    {
        if(foodCounter == 0)
        {
            GetComponent<Animator>().Play("Closed");

            GameObject.Find("Enemy").SetActive(false);
            GameObject.Find("Congratulations").GetComponent<SpriteRenderer>().enabled = true;

            Time.timeScale = 0;

            if (Score.score > PlayerPrefs.GetInt("HighScore", 0))
                PlayerPrefs.SetInt("HighScore", Score.score);

            LevelManager.level++;
            LevelManager levelManage = new LevelManager();
            levelManage.UpdateValues();

            FindObjectOfType<AudioManager>().Play("starting");

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
