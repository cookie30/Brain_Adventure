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
    public AudioSource AudioSource;

    public float Maxhp = 100f;
    public float hpAmount = 0f;
    public bool MonsterisAttack;

    private Animator anim;

    [Header("判定腳步聲")]
    public AudioClip Road, Grass, Run; //音檔
    public RaycastHit WalkTypeHit; //偵測地面射線
    public Transform HitStart; //射線起點
    public float RayLength; //射線長度
    public LayerMask layerMask; //地面蒙版(讓射線無視除地面以外的東西)

    private void Start()
    {
        anim = GetComponent<Animator>();  
        gameManager=GameObject.Find("GameManager").GetComponent<GameManager>();
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
            hpAmount -= EnemyAttack.enemyattack[0];
            gameManager.m_HPBar.fillAmount = hpAmount / Maxhp;
        }
        else if (hit.gameObject.tag == "Monster2")
        {
            hpAmount -= EnemyAttack.enemyattack[1];
            gameManager.m_HPBar.fillAmount = hpAmount/Maxhp;
        }
        // ||是或者，&&是而且
        else if (hit.gameObject.tag == "Monster3"|| 
            hit.gameObject.tag== "Monster3_Bullet")
        {
            hpAmount -= EnemyAttack.enemyattack[2];
            gameManager.m_HPBar.fillAmount = hpAmount/Maxhp;
        }
        else if (hit.gameObject.tag == "Monster4" ||
            hit.gameObject.tag == "Monster4_Bullet")
        {
            hpAmount -= EnemyAttack.enemyattack[3];
            gameManager.m_HPBar.fillAmount = hpAmount / Maxhp;
        }
        else if(hit.gameObject.tag == "Monster5")
        {
            hpAmount -= EnemyAttack.enemyattack[4];
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

    //偵測玩家的腳(射線)碰到的地面材質/是否按下衝刺鍵來撥放對應音效
    public void FootStep()
    {
        if(Physics.Raycast(HitStart.position,HitStart.transform.up*-1,
            out WalkTypeHit, RayLength, layerMask))
        {
            if (WalkTypeHit.collider.CompareTag("Road"))
            {
                PlayFootstepSound(Road);
            }
            else if (WalkTypeHit.collider.CompareTag("Garss"))
            {
                PlayFootstepSound(Grass);
            }
            else if(StarterAssetsInputs.sprint)
            {
                PlayFootstepSound(Run);
            }
        }
    }

    void PlayFootstepSound(AudioClip audio)
    {
        //播放音效
        AudioSource.PlayOneShot(audio);
    }

}

