using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    void UnlockLevel()
    {
        //���p�ثe��[���������s���p�󵥩��ܼ����d�s���A�h�������s���M�w�������d�s����+1(�i�C�����d�W��+1)
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("LevelIndex"))
        {
            PlayerPrefs.SetInt("LevelIndex",SceneManager.GetActiveScene().buildIndex+1);
            PlayerPrefs.SetInt("UnlockLevel", PlayerPrefs.GetInt("UnlockLevel") + 1);
            PlayerPrefs.Save();
        }
    }
}
