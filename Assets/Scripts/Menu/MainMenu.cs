using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Time.timeScale = 0f;

        LevelManager levelManage = new LevelManager();
        levelManage.UpdateValues();

        FindObjectOfType<AudioManager>().Play("starting");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteKey("HighScore");
    }
}
