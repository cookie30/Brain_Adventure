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
            //�]�m��Ъ��A��true(��w��в���)
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
        //��ܼȰ�����
        PauseGameUI.SetActive(true);
        //�Ȱ��C�����ɶ�
        Time.timeScale = 0f;
        //�N�C���Ȱ����ܼƳ]��true
        GameisPause = true;
        //print("GameisPause:" + GameisPause); 
        Cursor.visible = true;
    }

    //�I�s�]�w����
    public void LoadMenu()
    {
        //print("menu");
        Time.timeScale = 1f;
        SettingMenu.SetActive(true);
    }

    //���}�C��
    public void QuitGame()
    {
        print("quit");
        Application.Quit();
    }
}
