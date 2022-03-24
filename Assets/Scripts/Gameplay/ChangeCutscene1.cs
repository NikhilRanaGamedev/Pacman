using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeCutscene1 : MonoBehaviour
{
    void Update()
    {
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(10f);

        SceneManager.LoadScene("Gameplay");
    }
}
