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


    [Header("���a����Ϥ��]�w")]
    public Image m_HPBar;  //����Ϥ�

    [Header("�u�ĥ]�ƶq")]
    public TextMeshProUGUI bulletbag; //��ܼu�ĥ]�ƶq����r����
    public int BulletBag;

    

    // Start is called before the first frame update
    void Start()
    {

        BulletBag = 0;

        LevelTarget = GameObject.FindGameObjectsWithTag("Monster1").Length+
            GameObject.FindGameObjectsWithTag("Monster2").Length+
            GameObject.FindGameObjectsWithTag("Monster3").Length+
            GameObject.FindGameObjectsWithTag("Monster4").Length+
            GameObject.FindGameObjectsWithTag("Monster5").Length;

        ClearCount = LevelTarget;
    }

    // Update is called once per frame
    void Update()
    {

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
                ShowClearLevel();
            }

        }
        else
        {
            ClearLevel = false;
        }

        void ShowClearLevel(){
            ClearLevelMenu.SetUP();
        }

    }
}
