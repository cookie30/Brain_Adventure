using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;

public class PlayerHP : MonoBehaviour
{
    public GameManager gameManager;
    public StarterAssetsInputs StarterAssetsInputs;

    public float Maxhp = 100f;
    public float hpAmount = 0f;
    public bool MonsterisAttack;

    private Animator anim;

    private void Start()
    {
        anim = transform.Find("1").gameObject.GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StarterAssetsInputs=GetComponent<StarterAssetsInputs>();

        hpAmount = Maxhp;
    }

    //偵測碰撞
    //玩家碰到怪物本體時，會根據怪物攻擊力扣除相應的血量
    void OnTriggerEnter(Collider hit)
    {
        //在後台顯示Collider撞到的物件名稱
        //Debug.Log("PlayerHP_hit.gameObject.tag:"+hit.gameObject.tag +" HP:"+ hpAmount.ToString());

        if (hit.gameObject.tag == "Monster1")
        {
            hpAmount -= Enemy.enemyattack[0];
            gameManager.m_HPBar.fillAmount = hpAmount / Maxhp;
        }
        else if (hit.gameObject.tag == "Monster2")
        {
            hpAmount -= Enemy.enemyattack[1];
            gameManager.m_HPBar.fillAmount = hpAmount / Maxhp;
        }
        // ||是或者，&&是而且
        else if (hit.gameObject.tag == "Monster3" ||
            hit.gameObject.tag == "Monster3_Bullet")
        {
            hpAmount -= Enemy.enemyattack[2];
            gameManager.m_HPBar.fillAmount = hpAmount / Maxhp;
        }
        else if (hit.gameObject.tag == "Monster4" ||
            hit.gameObject.tag == "Monster4_Bullet")
        {
            hpAmount -= Enemy.enemyattack[3];
            gameManager.m_HPBar.fillAmount = hpAmount / Maxhp;
        }
        else if (hit.gameObject.tag == "Monster5")
        {
            hpAmount -= Enemy.enemyattack[4];
            gameManager.m_HPBar.fillAmount = hpAmount / Maxhp;
        }
        else if (hit.gameObject.tag == "Boss")
        {
            hpAmount -= Enemy.enemyattack[5];
            gameManager.m_HPBar.fillAmount = hpAmount / Maxhp;
        }
        //假如碰到的是血包
        else if (hit.gameObject.tag == "HPBag")
        {
            //則補滿玩家HP(目前血量=最大血量)
            hpAmount = Maxhp;
            gameManager.m_HPBar.fillAmount = Maxhp;
        }

        if (hpAmount <= 0)
        {
            StarterAssetsInputs.SetCursorState(true);
            anim.SetBool("Dead",true);

            Die();
        }

        


        //舊版碰撞偵測
        /*
        switch (hit.gameObject.tag)
        {
            case "Monster1":
                {
                    hpAmount -= EnemyAttack.enemyattack[0];
                    //Debug.Log(hpAmount);
                    gameManager.m_HPBar.fillAmount -= hpAmount / Maxhp;
                    break;
                }
            case "Monster2":
                {
                    hpAmount -= EnemyAttack.enemyattack[1];
                    gameManager.m_HPBar.fillAmount -= hpAmount / Maxhp;
                    break;
                }
            case "Monster3":
                {
                    hpAmount -= EnemyAttack.enemyattack[2];
                    gameManager.m_HPBar.fillAmount -= hpAmount / Maxhp;
                    break;
                }
            case "Monster3_Bullet":
                {
                    hpAmount -= EnemyAttack.enemyattack[2];
                    gameManager.m_HPBar.fillAmount -= hpAmount / Maxhp;
                    break;
                }
            case "Monster4":
                {
                    hpAmount -= EnemyAttack.enemyattack[3];
                    gameManager.m_HPBar.fillAmount -= hpAmount / Maxhp;
                    break;
                }
            case "Monster4_Bullet":
                {
                    hpAmount -= EnemyAttack.enemyattack[3];
                    gameManager.m_HPBar.fillAmount -= hpAmount / Maxhp;
                    break;
                }
            case "Monster5":
                {
                    hpAmount -= EnemyAttack.enemyattack[4];
                    gameManager.m_HPBar.fillAmount -= hpAmount / Maxhp;
                    break;
                }
        }
        */
    }
    void Die()
    {
        
        print("DIE");
        this.GetComponent<CharacterController>().enabled = false;
        this.GetComponent<PlayerInput>().enabled = false;

        //等待1秒後執行changScene()
        Invoke("changeScene", 1f);
    }
    void changeScene()
    {
        print("change scene");
        SceneManager.LoadScene("GameOver Menu");
        
    }



}

