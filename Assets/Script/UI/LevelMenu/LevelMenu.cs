using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;
    //public GameObject levelbuttons;

    public SwipeController SwipeController;

    private void Awake()
    {
        //預設Unlocklevel為1(自動解鎖1-1)
        //ButtonsToArray();
        // int unlockstage = PlayerPrefs.GetInt("UnlockStage",1);
        int unlocklevel = PlayerPrefs.GetInt("Unlocklevel",1);
        //先禁用所有後續關卡
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        //根據unlocklevel
        for (int s = 0; s < unlocklevel; s++)
        {
            buttons[s].interactable = true;
        }
    }

    public void OpenLevel(int levelID)
    {
        int stageID = SwipeController.CurrentPage;

        string LevelName = "Level" + stageID + "-" +levelID;
        //print(LevelName);
        SceneManager.LoadSceneAsync(LevelName);
    }

    //void ButtonsToArray()
    //{
    //    int childCount=levelbuttons.transform.childCount;
    //    buttons=new Button[childCount];
    //    for(int i = 0;i < childCount;i++)
    //    {
    //        buttons[i] =levelbuttons.transform.GetChild(i).gameObject.GetComponent<Button>();
    //    }
    //}
}
