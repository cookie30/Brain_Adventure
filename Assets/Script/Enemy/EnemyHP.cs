using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
{

    NavMeshAgent navMeshAgent;                 // 宣告NavMeshAgent物件
    Animator anim;

    Enemy Enemy;

    [Header("數值設定")]
    public Image lifeBarImage;                 // 敵人血條的圖片
    public float maxLife = 10.0f;              // 敵人的最高生命值
    public float lifeAmount;                   // 敵人的目前生命值
    
    //玩家武器的攻擊力
    private float Gun1Attack=1.0f;
    private float Gun2Attack=5.0f;
    private float ShieldPush = 3.0f;

    [Header("掉落物設定")]
    public Transform dropspwan; //掉落位置
    public GameObject[] enemydrops; //掉落物列表
    
    public float itemindex; //掉落物標號
    public float dropRate; //掉落機率

    //Weapon_玩家的槍械武器子彈數量
    private int BulletSize;
    //Shield_玩家使用推擊技能、推擊技能的推動範圍
    private bool PlayUsePush;
    private float PushRange=-1000f;

    //盾牌
    private GameObject 盾牌;

    [Header("Buff設定")]
    [Tooltip("顯示Buff狀態的開關和對應圖片")]
    public bool Monster1_Buff;
    public Image Buff1;

    public bool Monster2_Buff;
    public Image Buff2;

    public bool Monster3_Buff;
    public Image Buff3;

    public bool Monster4_Buff;
    public Image Buff4;

    public bool Monster5_Buff;
    public Image Buff5;

    // Start is called before the first frame update
    void Start()
    {
        Enemy = GetComponent<Enemy>();
        navMeshAgent = GetComponent<NavMeshAgent>();                   // 接收NavMeshAgent
        anim = GetComponent<Animator>();

        lifeAmount = maxLife;
        
        Buff1=GameObject.Find("UI/Buff/BackGround/Buff1").GetComponent<Image>();
        Buff1.enabled = false;
        Monster1_Buff = false;
        Buff2=GameObject.Find("UI/Buff/BackGround/Buff2").GetComponent<Image>();
        Buff2.enabled = false;
        Monster2_Buff = false;
        Buff3=GameObject.Find("UI/Buff/BackGround/Buff3").GetComponent<Image>();
        Buff3.enabled = false;
        Monster3_Buff = false;
        Buff4=GameObject.Find("UI/Buff/BackGround/Buff4").GetComponent<Image>();
        Buff4.enabled = false;
        Monster4_Buff = false;
        Buff5=GameObject.Find("UI/Buff/BackGround/Buff5").GetComponent<Image>();
        Buff5.enabled = false;
        Monster5_Buff = false;

        dropspwan = this.gameObject.transform;

        //Playermovespeed = GameObject.Find("Player").GetComponent<FirstPersonController>().MoveSpeed;
        //BulletSize = GameObject.Find("Player/Camera/Weapon").GetComponent<Weapon>().magazineSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (盾牌!=null)
        {
            PlayUsePush = GameObject.Find("Player/1/mixamorig:Hips/mixamorig:Spine/Weapon/盾牌").GetComponent<Shield>().PlayerUsePush;
            //print("PlayUsePush"+PlayUsePush);
        }

        //PlayerMaxhp=GameObject.Find("Player").GetComponent<PlayerHP>().Maxhp;


        // 判斷式：如果生命數值低於0，則讓敵人消失
        if (lifeAmount <= 0.0f)
        {
            dropRate= Random.Range(1, 100);
            itemindex = dropRate % 3;
            
            navMeshAgent.enabled = false;

            switch (itemindex)
            {
                case 0:
                    Instantiate(enemydrops[0], dropspwan.position, Quaternion.identity);
                    break;
                case 1:
                    Instantiate(enemydrops[1],dropspwan.position, Quaternion.identity);
                    break;
                case 2:
                    break;
            }

            //Buff設定
            switch (this.gameObject.tag)
            {
                case "Monster1":

                    Monster1_Buff= true;

                    if (Monster1_Buff == true)
                    {
                        //取消其他Buff(關閉其他Buff的開關和圖片)
                        Monster2_Buff= false;
                        Buff2.enabled=false;
                        Monster3_Buff= false;
                        Buff3.enabled=false;
                        Monster4_Buff= false;
                        Buff4.enabled=false;
                        Monster5_Buff= false;
                        Buff5.enabled=false;

                        Buff1.enabled=true;

                        //提升移速
                        GameObject.Find("Player").GetComponent<FirstPersonController>().MoveSpeed=
                            GameObject.Find("Player").GetComponent<FirstPersonController>().MoveSpeed+1f;

                        //提升攻速(攻擊間隔)
                        Weapon.fireRate[0] = 0.1f;
                        Weapon.fireRate[1] = 0.4f;
                        //降低攻擊力
                        Gun1Attack = 0.5f;
                        Gun2Attack = 2.5f;
                        ShieldPush = 1.5f;
                    }
                    else
                    {
                        //移速
                        GameObject.Find("Player").GetComponent<FirstPersonController>().MoveSpeed =
                            GameObject.Find("Player").GetComponent<FirstPersonController>().MoveSpeed;

                        //攻速(攻擊間隔)
                        Weapon.fireRate[0] = 0.2f;
                        Weapon.fireRate[1] = 0.5f;
                        //攻擊力
                        Gun1Attack = 1f;
                        Gun2Attack = 5f;
                        ShieldPush = 3f;
                    }
                    break;
                case "Monster2":
                     Monster2_Buff = true;

                    if (Monster2_Buff == true)
                    {
                        //取消其他Buff
                        Monster1_Buff = false;
                        Buff1.enabled = false;
                        Monster3_Buff = false;
                        Buff3.enabled = false;
                        Monster4_Buff = false;
                        Buff4.enabled = false;
                        Monster5_Buff = false;
                        Buff5.enabled = false;

                        Buff2.enabled = true;

                        //降低移速
                        GameObject.Find("Player").GetComponent<FirstPersonController>().MoveSpeed = 3f;

                        //降低攻速(攻擊間隔)
                        Weapon.fireRate[0] = 0.5f;
                        Weapon.fireRate[1] = 0.7f;
                        //提升攻擊力
                        Gun1Attack = 2f;
                        Gun2Attack = 6f;
                        ShieldPush = 4f;
                    }
                    else {
                        //移速
                        GameObject.Find("Player").GetComponent<FirstPersonController>().MoveSpeed = 4f;

                        //攻速(攻擊間隔)
                        Weapon.fireRate[0] = 0.2f;
                        Weapon.fireRate[1] = 0.5f;
                        //攻擊力
                        Gun1Attack = 1f;
                        Gun2Attack = 5f;
                        ShieldPush = 3f;
                    }
                    break;
                case "Monster3":
                    Monster3_Buff= true;

                    if (Monster3_Buff)
                    {
                        //取消其他Buff
                        Monster1_Buff = false;
                        Buff1.enabled = false;
                        Monster2_Buff = false;
                        Buff2.enabled = false;
                        Monster4_Buff = false;
                        Buff4.enabled = false;
                        Monster5_Buff = false;
                        Buff5.enabled = false;

                        Buff3.enabled = true;


                        //降低目前子彈數量&上限
                        GameObject 機關槍 = GameObject.Find("Player/1/mixamorig:Hips/mixamorig:Spine/Weapon/機關槍");
                        GameObject 狙擊槍 = GameObject.Find("Player/1/mixamorig:Hips/mixamorig:Spine/Weapon/狙擊槍");
                        if (機關槍 != null&&機關槍.GetComponent<Weapon>().magazineSize==100)
                        {
                            
                            機關槍.GetComponent<Weapon>().magazineSize = 50;
                            機關槍.GetComponent<Weapon>().bulletsLeft -= 50;
                        }
                        else if(狙擊槍!= null && 狙擊槍.GetComponent<Weapon>().magazineSize == 20)
                        {
                            狙擊槍.GetComponent<Weapon>().magazineSize = 10;
                            狙擊槍.GetComponent<Weapon>().bulletsLeft -= 10;
                        }

                        //降低盾牌推動距離
                        PushRange = -500f;

                        //提高攻擊力
                        Gun1Attack = 3f;
                        Gun2Attack = 7f;
                        ShieldPush = 5f;
                    }
                    else
                    {
                        //降低目前子彈數量&上限
                        GameObject 機關槍 = GameObject.Find("Player/1/mixamorig:Hips/mixamorig:Spine/Weapon/機關槍");
                        GameObject 狙擊槍 = GameObject.Find("Player/1/mixamorig:Hips/mixamorig:Spine/Weapon/狙擊槍");
                        if (機關槍 != null && 機關槍.GetComponent<Weapon>().magazineSize == 100)
                        {

                            機關槍.GetComponent<Weapon>().magazineSize = 100;
                        }
                        else if (狙擊槍 != null && 狙擊槍.GetComponent<Weapon>().magazineSize == 20)
                        {
                            狙擊槍.GetComponent<Weapon>().magazineSize = 20;
                        }
                    }
                    break;
                case "Monster4":
                    Monster4_Buff= true;

                    if (Monster4_Buff == true)
                    {
                        //取消其他Buff
                        Monster1_Buff = false;
                        Buff1.enabled = false;
                        Monster2_Buff = false;
                        Buff2.enabled = false;
                        Monster3_Buff = false;
                        Buff3.enabled = false;
                        Monster5_Buff = false;
                        Buff5.enabled = false;

                        Buff4.enabled = true;

                        //受到傷害增加(提高怪物攻擊力)
                        Enemy.enemyattack[0] = 3f; // 第一種攻擊力增加為3
                        Enemy.enemyattack[1] = 5f; // 第二種攻擊力增加為5
                        Enemy.enemyattack[2] = 7f; // 第三種攻擊力增加為7
                        Enemy.enemyattack[3] = 9f; // 第四種攻擊力增加為9
                        Enemy.enemyattack[4] = 12f; // 第五種攻擊力增加為12
                        //提高攻擊力
                        Gun1Attack = 5f;
                        Gun2Attack = 9f;
                        ShieldPush = 7f;
                    }
                    else
                    {
                        //怪物攻擊力
                        Enemy.enemyattack[0] = 1f; 
                        Enemy.enemyattack[1] = 3f; 
                        Enemy.enemyattack[2] = 5f; 
                        Enemy.enemyattack[3] = 7f; 
                        Enemy.enemyattack[4] = 10f; 
                        //攻擊力
                        Gun1Attack = 1f;
                        Gun2Attack = 5f;
                        ShieldPush = 3f;
                    }
                    break;
                case "Monster5":
                    Monster5_Buff= true;

                    if (Monster5_Buff == true)
                    {
                        //取消其他Buff
                        Monster1_Buff = false;
                        Buff1.enabled = false;
                        Monster2_Buff = false;
                        Buff2.enabled = false;
                        Monster3_Buff = false;
                        Buff3.enabled = false;
                        Monster4_Buff = false;
                        Buff4.enabled = false;

                        Buff5.enabled = true;

                        //提高移速
                        GameObject.Find("Player").GetComponent<FirstPersonController>().MoveSpeed = 7f;
                        //提高攻速
                        Weapon.fireRate[0] = 0.05f;
                        Weapon.fireRate[1] = 0.2f;
                        //提高攻擊力
                        Gun1Attack = 7f;
                        Gun2Attack = 11f;
                        ShieldPush = 9f;
                        //降低生命值上限
                        GameObject.Find("Player").GetComponent<PlayerHP>().Maxhp = 70f;
                       
                    }
                    else
                    {
                        //移速
                        GameObject.Find("Player").GetComponent<FirstPersonController>().MoveSpeed = 4f;
                        //攻速
                        Weapon.fireRate[0] = 0.2f;
                        Weapon.fireRate[1] = 0.5f;
                        //攻擊力
                        Gun1Attack = 1f;
                        Gun2Attack = 5f;
                        ShieldPush = 3f;
                        //生命值上限
                        GameObject.Find("Player").GetComponent<PlayerHP>().Maxhp = 100f;
                    }
                    break;

            }

            GameObject.Find("/GameManager").GetComponent<GameManager>().ClearCount--;

            Destroy(this.gameObject);
        }
    }

    // 碰撞偵測：玩家碰到怪物
    private void OnTriggerEnter(Collider collision)
    {
        //在後台顯示怪物碰到的物件名稱
        //print(collision.transform.name);
        // 如果碰到帶有Bullet標籤的物件，就要扣血並更新血條狀態
        if (collision.gameObject.tag == "Gun1_Bullet")
        {
            lifeAmount -= Gun1Attack;
            lifeBarImage.fillAmount = lifeAmount / maxLife;
            anim.SetBool("Hurt", true);
            Enemy.isChasingPlayer= true;
            //print(Enemy.isChasingPlayer);
        }
        else if (collision.gameObject.tag == "Gun2_Bullet")
        {
            lifeAmount -= Gun2Attack;
            lifeBarImage.fillAmount = lifeAmount / maxLife;
            anim.SetBool("Hurt", true);
            Enemy.isChasingPlayer = true;
            //print(Enemy.isChasingPlayer);
        }
        else if (collision.gameObject.tag == "Shield" )
        {
            //print("我碰到盾牌了");
            盾牌 = collision.gameObject;

            lifeAmount -= ShieldPush;
            lifeBarImage.fillAmount = lifeAmount / maxLife;

            anim.SetBool("Hurt",true);

            this.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * PushRange,
                ForceMode.Force);
        }
        anim.SetBool("Hurt", false);

    }
    /*
    // 碰撞偵測
    private void OnCollisionEnter(Collision collision)
    {
        print(collision.transform.name);
        // 如果碰到帶有Bullet標籤的物件，就要扣血並更新血條狀態
        if (collision.gameObject.tag == "Gun1_Bullet")
        {
            lifeAmount -= Gun1Attack;
            lifeBarImage.fillAmount = lifeAmount / maxLife;
        }
        else if (collision.gameObject.tag == "Gun2_Bullet")
        {
            lifeAmount -= Gun2Attack;
            lifeBarImage.fillAmount = lifeAmount / maxLife;
        }
        else if (collision.gameObject.tag == "Shield" && PlayUsePush == true)
        {
            print("我碰到盾牌了");
            盾牌=collision.gameObject;
            
            lifeAmount -= ShieldPush;
            lifeBarImage.fillAmount= lifeAmount / maxLife;

            this.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * PushRange, 
                ForceMode.Force); 
        }
        

    }
    */
}
