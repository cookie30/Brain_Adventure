using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;    //使用UnityEngine.AI

public class Enemy : MonoBehaviour
{
    [Header("追蹤目標設定")]
    public string targetName = "Player";       // 設定目標物件的標籤名稱
    public float minimunTraceDistance = 3.0f;  // 設定最短的追蹤距離
    public float maxmunTraceDistance = 5.0f;   //設定最長的追蹤距離
    public static float shootdistance = 4.0f; // 設定中間的追蹤距離(切換到遠程攻擊的距離)
    public static float distance;
    public static bool PlayerInShootRange;     //設定玩家是否在射擊範圍

    private Rigidbody Monsterrigidbody;         //怪物模型的鋼體
    private Animator anim;

    public NavMeshAgent navMeshAgent;                 // 宣告NavMeshAgent物件
    GameObject targetObject = null;            // 目標物件的變數
    public EnemyHP EnemyHP;

    [Header("巡邏模式設定")]
    public Transform[] points;
    public int targetPoint;

    public PlayerHP PlayerHP;

    [Header("攻擊設定")]
    //敵人攻擊力
    public static float[] enemyattack = { 1, 3, 5, 7, 10 };
    //敵人攻擊間隔    
    public static float[] monsterattackRate = { 0.3f, 0.5f, 0.7f, 0.9f, 1.0f };
    //敵人下次攻擊時間    
    public float nextAttack;
    //判斷是否在攻擊
    public bool isAttack;
    

    //是遠程攻擊，準備攻擊
    public bool IsShootAttack, aleadyAttack;
    //子彈元件
    public GameObject[] Bullet;
    //發射點
    public Transform BulletSpwan;
    //子彈射速
    public float[] BulletSpeed = { 3.0f, 5.0f };

    [SerializeField] private AudioClip attackClip,shootattackClip,damageClip,walkClip;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();                   // 接收NavMeshAgent
        Monsterrigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        EnemyHP=GetComponent<EnemyHP>();

        Monsterrigidbody.freezeRotation = true;
        isAttack= false;

        targetObject = GameObject.FindGameObjectWithTag(targetName) ;   // 以帶有特定的標籤名稱為目標物件
        PlayerHP=GameObject.Find("Player").GetComponent<PlayerHP>();
    }

    void Update()
    {

        // 計算目標物件和自己的距離 distance(自己的位置,目標物件的位置)
        distance = Vector3.Distance(transform.position, targetObject.transform.position);

        // 判斷式：判斷距離是否低於最小追蹤距離，如果與目標的距離小於最小距離就追蹤，否則就不追蹤(切換成定點巡邏模式)
        //★★★判斷的方法應該從大到小，如traceDistance 為最大值(追蹤)，然後再判斷 shootDistance(攻擊)
        if (distance < maxmunTraceDistance)
        {
            //判斷距離是否小於射擊距離，如果與目標距離小於射擊距離就呼叫ShootPlayer方法：
            //將PlayerInShootRange設為true，並停止尋路(站在原地)+播放攻擊動畫
            if (distance < shootdistance|| EnemyHP.lifeAmount != EnemyHP.maxLife)
            {
                PlayerInShootRange = true;
                ShootPlayer();
            }
            else if (distance < minimunTraceDistance && IsShootAttack == false &&
                    PlayerInShootRange == false|| EnemyHP.lifeAmount != EnemyHP.maxLife)
            {
                    Attack();
            }
            else
            {
                PlayerInShootRange = false;
                ChasePlayer();
            }
        }
        else
        {
            //假如上述條件(距離>最小追蹤距離和攻擊距離)成立，則關閉玩家進入攻擊範圍的開關，並進入巡邏模式
            PlayerInShootRange = false;
            Patrolling();
        }

    }

    //將敵人一直正面面對角色(也就是讓敵人的Z軸不斷的瞄準角色)
    void faceTarget()
    {
        //transform.LookAt(targetObject.transform.position);

        
        Vector3 targetDir = targetObject.transform.position - transform.position;                               // 計算敵人與角色之間的向量
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 0.5f * Time.deltaTime, 0.0f);      // 依照敵人Z軸向量與兩者間向量，可以計算出需要旋轉的角度
        transform.rotation = Quaternion.LookRotation(newDir);                                                   // 進行旋轉
        
    }

    //巡邏模式：按照順序移動到陣列中的地點
    private void Patrolling()
    {
        navMeshAgent.isStopped = false;
        //SoundEffectManager.Instance.PlaySound(walkClip, transform, 1f);
        navMeshAgent.SetDestination(points[targetPoint].position);
        
     
        if (navMeshAgent.remainingDistance < 0.5f)
        {
            targetPoint=(targetPoint+1)%points.Length;
            
            navMeshAgent.SetDestination(points[targetPoint].position);
        }


    }

    //追逐玩家
    private void ChasePlayer()
    {
        faceTarget(); // 將敵人一直正面面對角色，因為敵人和角色位置會變化，所以要不斷Update

        navMeshAgent.enabled = true;
        anim.SetBool("Walk", true);
        //SoundEffectManager.Instance.PlaySound(walkClip, transform, 1f);
        //除了標籤為Monster2的物件以外，其他掛著這個腳本的物件都要朝玩家的座標移動
        if (this.gameObject.tag == "Monster2")
        {
            return;
        }
        else
        {
            if (navMeshAgent.enabled)
                navMeshAgent.SetDestination(targetObject.transform.position);    // 讓敵人往目標物的座標移動   
        }
    }

    //碰撞偵測
    private void OnColliderEnter(Collision collision)
    {
        //如果碰到Player標籤的物件，就會攻擊玩家
        if (collision.gameObject.tag == "Player" && IsShootAttack == false &&
            PlayerInShootRange == false)
        {
            Attack();
        }
    }

    bool ShootPlayer()
    {
        
        if (IsShootAttack == true && PlayerInShootRange == true)
        {
            navMeshAgent.isStopped = true;
            anim.SetBool("Walk", false);
            faceTarget();

            if (Time.time > nextAttack)
            {
                switch (this.gameObject.tag)
                {
                    case "Monster3":
                        {
                            anim.SetTrigger("Attack");
                            SoundEffectManager.Instance.PlaySound(shootattackClip, transform, nextAttack);
                            //產生子彈物件eb(調用Bullet物件陣列中的0號物件，從發射點產生)
                            GameObject eb = Instantiate(Bullet[0], BulletSpwan.position, Quaternion.identity);
                            
                            eb.transform.eulerAngles=this.transform.eulerAngles;
                            nextAttack = Time.time + monsterattackRate[2];
                            
                            break;
                        }
                    case "Monster4":
                        {
                            anim.SetTrigger("Attack");
                            SoundEffectManager.Instance.PlaySound(shootattackClip, transform, nextAttack);
                            GameObject eb2 = Instantiate(Bullet[1], BulletSpwan.position, Quaternion.identity);
                            
                            eb2.transform.eulerAngles = this.transform.eulerAngles;
                            nextAttack = Time.time + monsterattackRate[3];
                            break;
                        }
                }
            }

            return true;
        }
        navMeshAgent.isStopped = false;
        anim.SetBool("Walk", true);
        return false;
    }

    public void Attack()
    {
        faceTarget();

        //計算攻擊間隔
        if (Time.time > nextAttack)
        {
            isAttack = true;
            switch (this.gameObject.tag)
            {
                case "Monster1":
                    {
                        nextAttack = Time.time + monsterattackRate[0];
                        anim.SetTrigger("Attack");
                        SoundEffectManager.Instance.PlaySound(attackClip, transform, nextAttack);
                        PlayerHP.hpAmount -= enemyattack[0];

                        
                        break;
                    }
                case "Monster2":
                    {
                        nextAttack = Time.time + monsterattackRate[1];
                        anim.SetTrigger("Attack");

                        PlayerHP.hpAmount -= enemyattack[1];

                        
                        break;
                    }
                case "Monster3":
                    {
                        nextAttack = Time.time + monsterattackRate[2];
                        anim.SetTrigger("Attack");

                        PlayerHP.hpAmount -= enemyattack[2];

                        break;
                    }
                case "Monster4":
                    {
                        nextAttack = Time.time + monsterattackRate[3];
                        anim.SetTrigger("Attack");

                        PlayerHP.hpAmount -= enemyattack[3];
                        break;
                    }
                case "Monster5":
                    {

                        nextAttack = Time.time + monsterattackRate[4];
                        anim.SetTrigger("Attack");

                        PlayerHP.hpAmount -= enemyattack[4];
                        break;
                    }
            }
        }
    }
}
