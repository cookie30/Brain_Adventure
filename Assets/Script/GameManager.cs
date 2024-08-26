using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [Header("結束關卡設定")]
    [Tooltip("抓取場景中的怪物數量")]
    public float LevelTarget; //場景中產生的怪物數量
    [Tooltip("偵測玩家目前打倒了幾隻怪")]
    public float ClearCount; //打倒的怪物數量
    [Tooltip("是否清完怪的變數")]
    public bool ClearLevel; //是否通關
    [Tooltip("清完怪後跳出的通關頁面")]
    public ClearLevelMenu ClearLevelMenu;

    public EnemyHP enemyhp;

    [Header("玩家血條圖片設定")]
    public Image m_HPBar;  //血條圖片

    [Header("彈藥包數量")]
    public TextMeshProUGUI bulletbag; //顯示彈藥包數量的文字元件
    public int BulletBag;

    

    // Start is called before the first frame update
    void Start()
    {
        
        //保存場景名稱(給遊戲結束的選單使用)
        string currentScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("PreviousScene", currentScene);
        //print(currentScene);

        BulletBag = 0;

        LevelTarget = GameObject.FindGameObjectsWithTag("Monster1").Length +
        GameObject.FindGameObjectsWithTag("Monster2").Length +
        GameObject.FindGameObjectsWithTag("Monster3").Length +
        GameObject.FindGameObjectsWithTag("Monster4").Length +
        GameObject.FindGameObjectsWithTag("Monster5").Length +
        GameObject.FindGameObjectsWithTag("Boss").Length;

        ClearCount = LevelTarget;

        enemyhp=GameObject.FindObjectOfType<EnemyHP>();

    }

    // Update is called once per frame
    void Update()
    {

        LevelTarget = GameObject.FindGameObjectsWithTag("Monster1").Length +
        GameObject.FindGameObjectsWithTag("Monster2").Length +
        GameObject.FindGameObjectsWithTag("Monster3").Length +
        GameObject.FindGameObjectsWithTag("Monster4").Length +
        GameObject.FindGameObjectsWithTag("Monster5").Length +
        GameObject.FindGameObjectsWithTag("Boss").Length;

        ClearCount = LevelTarget;

        if (bulletbag != null)
        {
            bulletbag.SetText($"BulletBag {BulletBag}");
        }

        if (ClearCount == 0)
        {
            ClearLevel = true;
            Debug.Log("已打倒場上所有怪物！");
            if (ClearLevel)
            {
                UnlockLevel();
                ShowClearLevel();
            }

        }
        else
        {
            ClearLevel = false;
        }

    }

    void ShowClearLevel()
    {
        enemyhp.Monster1_Buff = false;
        enemyhp.Monster2_Buff = false;
        enemyhp.Monster3_Buff = false;
        enemyhp.Monster4_Buff = false;
        enemyhp.Monster5_Buff = false;
        ClearLevelMenu.SetUP();
    }

    public void UnlockLevel()
    {
        //print("準備解鎖關卡...");
        //print("Scene:"+SceneManager.GetActiveScene().buildIndex);
        //print("ReachedIndex:" + PlayerPrefs.GetInt("ReachedIndex"));
        //print("UnlockLevel:" + PlayerPrefs.GetInt("Unlocklevel"));
        //假如目前能加載的場景編號大於等於變數關卡編號，則讓已解鎖關卡編號+1(可遊玩關卡上限+1)
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex+1);
            PlayerPrefs.SetInt("Unlocklevel", PlayerPrefs.GetInt("Unlocklevel", 1) + 1);

            print("關卡解鎖成功！");
            PlayerPrefs.Save();
        }
    }
}
