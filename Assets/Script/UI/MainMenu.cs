using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayNewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadSceneAsync("Level1-1");

        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
