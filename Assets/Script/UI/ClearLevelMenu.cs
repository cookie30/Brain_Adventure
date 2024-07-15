using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearLevelMenu : MonoBehaviour
{
    public StarterAssetsInputs StarterAssetsInputs;
    public Enemy Enemy;

    public void SetUP()
    {
        gameObject.SetActive(true);
        Cursor.visible = true;
        StarterAssetsInputs.SetCursorState(true);
    }

    public void JumpNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        if (SceneManager.GetActiveScene().name == "Level5-5")
        {
            SceneManager.LoadScene("End Scene");
        }
    }

    public void GoBackMain()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
