using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{

    public void ReStart()
    {
        //Ū�����e�x�s�������W��
        string previousScene = PlayerPrefs.GetString("PreviousScene", "DefaultScene");

        SceneManager.LoadScene(previousScene);
    }

    public void GoBackMain()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
