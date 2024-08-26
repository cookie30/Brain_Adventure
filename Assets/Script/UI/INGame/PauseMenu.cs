using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameisPause=false;

    public GameObject PauseGameUI;
    public GameObject SettingMenu;

    public StarterAssetsInputs StarterAssetsInputs;

    private void Awake()
    {
        StarterAssetsInputs=GameObject.Find("Player").GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            //設置游標狀態為true(鎖定游標移動)
            StarterAssetsInputs.SetCursorState(true);
            if (GameisPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseGameUI.SetActive(false);
        Time.timeScale = 1f;
        GameisPause = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        StarterAssetsInputs.SetCursorState(false);
        //print("GameisPause:"+GameisPause);
    }

    void Pause()
    {
        //顯示暫停頁面
        PauseGameUI.SetActive(true);
        //暫停遊戲內時間
        Time.timeScale = 0f;
        //將遊戲暫停的變數設為true
        GameisPause = true;
        //print("GameisPause:" + GameisPause); 
        Cursor.visible = true;
    }

    //呼叫設定頁面
    public void LoadMenu()
    {
        //print("menu");
        Time.timeScale = 1f;
        SettingMenu.SetActive(true);
    }

    //離開遊戲
    public void QuitGame()
    {
        print("quit");
        Application.Quit();
    }
}
