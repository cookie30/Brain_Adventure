using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    void UnlockLevel()
    {
        //假如目前能加載的場景編號小於等於變數關卡編號，則讓場景編號和已解鎖關卡編號都+1(可遊玩關卡上限+1)
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("LevelIndex"))
        {
            PlayerPrefs.SetInt("LevelIndex",SceneManager.GetActiveScene().buildIndex+1);
            PlayerPrefs.SetInt("UnlockLevel", PlayerPrefs.GetInt("UnlockLevel") + 1);
            PlayerPrefs.Save();
        }
    }
}
