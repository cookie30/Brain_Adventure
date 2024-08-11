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

    [Header("�P�w�}�B�n")]
    public AudioClip Road, Grass, Run; //����
    public RaycastHit WalkTypeHit; //�����a���g�u
    public Transform HitStart; //�g�u�_�I
    public float RayLength; //�g�u����
    public LayerMask layerMask; //�a���X��(���g�u�L�����a���H�~���F��)

    private void Start()
    {
        anim = GetComponent<Animator>();  
        gameManager=GameObject.Find("GameManager").GetComponent<GameManager>();
        StarterAssetsInputs=GetComponent<StarterAssetsInputs>();

        hpAmount = Maxhp;
    }

    //�����I��
    //���a�I��Ǫ�����ɡA�|�ھکǪ������O������������q
    void OnTriggerEnter(Collider hit)
    {
        //�b��x���Collider���쪺����W��
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
        // ||�O�Ϊ̡A&&�O�ӥB
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
        //���p�I�쪺�O��]
        else if (hit.gameObject.tag == "HPBag")
        {
            //�h�ɺ����aHP(�ثe��q=�̤j��q)
            hpAmount = Maxhp;
            gameManager.m_HPBar.fillAmount = Maxhp;
        }

        if (hpAmount <= 0)
        {
            StarterAssetsInputs.SetCursorState(true);
            anim.SetBool("Dead",true);
        }

        


        //�ª��I������
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

        //����1������changScene()
        Invoke("changeScene", 1f);
    }
    void changeScene()
    {
        print("change scene");
        SceneManager.LoadScene("GameOver Menu");
        
    }

    //�������a���}(�g�u)�I�쪺�a������/�O�_���U�Ĩ���Ӽ����������
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
        //���񭵮�
        AudioSource.PlayOneShot(audio);
    }

}

