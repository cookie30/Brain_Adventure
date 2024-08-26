using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    int unlocklevel;
    public void UnlockLevel()
    {
        //���p�ثe��[���������s���p�󵥩��ܼ����d�s���A�h�������s���M�w�������d�s����+1(�i�C�����d�W��+1)
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("LevelIndex"))
        {
            PlayerPrefs.SetInt("LevelIndex",SceneManager.GetActiveScene().buildIndex+1);
            PlayerPrefs.SetInt("UnlockLevel", PlayerPrefs.GetInt("Unlocklevel", 1) + 1);

            unlocklevel = PlayerPrefs.GetInt("UnlockLevel");
            print(unlocklevel);
            PlayerPrefs.Save();
        }
    }
}
