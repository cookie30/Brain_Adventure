using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [Header("�������d�]�w")]
    [Tooltip("������������Ǫ��ƶq")]
    public float LevelTarget; //���������ͪ��Ǫ��ƶq
    [Tooltip("�������a�ثe���ˤF�X����")]
    public float ClearCount; //���˪��Ǫ��ƶq
    [Tooltip("�O�_�M���Ǫ��ܼ�")]
    public bool ClearLevel; //�O�_�q��
    [Tooltip("�M���ǫ���X���q������")]
    public ClearLevelMenu ClearLevelMenu;

    public EnemyHP enemyhp;

    [Header("���a����Ϥ��]�w")]
    public Image m_HPBar;  //����Ϥ�

    [Header("�u�ĥ]�ƶq")]
    public TextMeshProUGUI bulletbag; //��ܼu�ĥ]�ƶq����r����
    public int BulletBag;

    

    // Start is called before the first frame update
    void Start()
    {
        
        //�O�s�����W��(���C�����������ϥ�)
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
            Debug.Log("�w���˳��W�Ҧ��Ǫ��I");
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
        //print("�ǳƸ������d...");
        //print("Scene:"+SceneManager.GetActiveScene().buildIndex);
        //print("ReachedIndex:" + PlayerPrefs.GetInt("ReachedIndex"));
        //print("UnlockLevel:" + PlayerPrefs.GetInt("Unlocklevel"));
        //���p�ثe��[���������s���j�󵥩��ܼ����d�s���A�h���w�������d�s��+1(�i�C�����d�W��+1)
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex+1);
            PlayerPrefs.SetInt("Unlocklevel", PlayerPrefs.GetInt("Unlocklevel", 1) + 1);

            print("���d���ꦨ�\�I");
            PlayerPrefs.Save();
        }
    }
}
