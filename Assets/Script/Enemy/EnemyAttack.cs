using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Unity.VisualScripting;

public class EnemyAttack : MonoBehaviour
{
    Animator anim;
    public float PlayerHPAmount;

    public bool MonsterisAttack;
    public static float[] enemyattack = { 1, 3, 5, 7, 10 };             //敵人攻擊力
    public float[] monsterattackRate = {0.3f,0.5f,0.7f,0.9f,1.0f};      //敵人攻擊間隔
    private float nextAttack;                                           //敵人下次攻擊時間
    private float timer;

    public bool IsShootAttack, aleadyAttack;
    public GameObject[] Bullet;
    public Transform BulletSpwan;
    public float[] BulletSpeed= {3.0f,5.0f};
    public int Index=0;


    private void Start()
    {
        anim = GetComponent<Animator>();
        timer += Time.deltaTime;
        MonsterisAttack = false;
    }

    //碰撞偵測
    private void OnCollisionEnter(Collision collision)
    {
        //如果碰到Player標籤的物件，就會攻擊玩家
        if (collision.gameObject.tag == "Player" && IsShootAttack==false&&
            EnemyMove.PlayerInShootRange==false)
        {

            Attack();

        }

    }
    void ShootPlayer()
    {
        if (IsShootAttack == true &&EnemyMove.PlayerInShootRange == true)
        {
            ShootAttack();
        }
    }

    private void Attack()
    {
        //計算攻擊間隔
        if (Time.time > nextAttack)
        {
            switch (this.gameObject.tag)
            {
                case "Monster1":
                    {

                        nextAttack = Time.time + monsterattackRate[0];
                        //anim.SetTrigger("Attack");

                         PlayerHPAmount -= enemyattack[0];

                        Debug.Log("怪物發動了攻擊！");
                        break;
                    }
                case "Monster2":
                    {

                        nextAttack = Time.time + monsterattackRate[1];

                        //anim.SetTrigger("Attack");

                        PlayerHPAmount -= enemyattack[1];
                        Debug.Log("怪物發動了攻擊！");
                        break;
                    }
                case "Monster3":
                    {

                        Index = 1;
                        nextAttack = Time.time + monsterattackRate[2];

                        PlayerHPAmount -= enemyattack[2];
                        break;
                    }
                case "Monster4":
                    {

                        Index = 2;
                        nextAttack = Time.time + monsterattackRate[3];

                        PlayerHPAmount -= enemyattack[3];
                        break;
                    }
                case "Monster5":
                    {

                        Index = 3;
                        nextAttack = Time.time + monsterattackRate[4];

                        PlayerHPAmount -= enemyattack[4];
                        break;
                    }
            }
        }

        if (timer >= nextAttack)
        {
            timer = 0;
        }
    }

    private void ShootAttack()
    {
        //anim.SetTrigger("ShootAttack");

        if (this.gameObject.tag == "Monster3")
        {
            GameObject monBullet = Instantiate(Bullet[0], BulletSpwan.transform.position,
                BulletSpwan.transform.rotation) as GameObject;
            Rigidbody monBulletrig = monBullet.GetComponent<Rigidbody>();
            monBulletrig.AddForce(monBulletrig.transform.forward * BulletSpeed[0]);
            Destroy(monBullet, 0.3f);
        }


        if (timer >= nextAttack)
        {
            timer = 0;
        }
    }
}
