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
        //ButtonsToArray();
        int unlockstage = PlayerPrefs.GetInt("UnlockStage",1);
        int unlocklevel = PlayerPrefs.GetInt("Unlocklevel",1);
        
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        for(int s = 0; s < unlocklevel; s++)
        {
            buttons[s].interactable = true;
        }
    }

    public void OpenLevel(int levelID)
    {
        int stageID;

        stageID = SwipeController.CurrentPage;

        string LevelName = "Level" + stageID + "-" +levelID;
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
