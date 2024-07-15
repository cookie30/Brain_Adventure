using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;    //使用UnityEngine.AI

public class EnemyMove : MonoBehaviour
{
    [Header("追蹤目標設定")]
    public string targetName = "Player";       // 設定目標物件的標籤名稱
    public float minimunTraceDistance = 5.0f;  // 設定最短的追蹤距離
    public static float shootdistance = 10f; // 設定中間的追蹤距離(切換到遠程攻擊的距離)
    public static float distance;
    public static bool PlayerInShootRange;     //設定玩家是否在射擊範圍

    private Rigidbody Monsterrigidbody;         //怪物模型的鋼體
    private Animator anim;

    public NavMeshAgent navMeshAgent;                 // 宣告NavMeshAgent物件
    GameObject targetObject = null;            // 目標物件的變數

    [Header("巡邏模式設定")]
    public Transform[] points;
    public int targetPoint;



    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();                   // 接收NavMeshAgent
        Monsterrigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        Monsterrigidbody.freezeRotation = true;

        targetObject = GameObject.FindGameObjectWithTag(targetName) ;   // 以帶有特定的標籤名稱為目標物件
        

    }

    void Update()
    {
        // 計算目標物件和自己的距離 distance(自己的位置,目標物件的位置)
        distance = Vector3.Distance(transform.position, targetObject.transform.position);

        // 判斷式：判斷距離是否低於最小追蹤距離，如果與目標的距離小於最小距離就追蹤，否則就不追蹤(切換成定點巡邏模式)
        if (distance <= minimunTraceDistance)
        {
            ChasePlayer();
        }
        //判斷距離是否小於射擊距離，如果與目標距離小於射擊距離就停止尋路(站在原地)，並將PlayerInShootRange設為true
        else if(distance <= shootdistance)
        {
            navMeshAgent.isStopped = true;
            PlayerInShootRange = true;
        }
        else
        {
            Patrolling();
        }
 
    }

    void FixedUpdate()
    {

    }

    // 函式：將敵人一直正面面對角色(也就是讓敵人的Z軸不斷的瞄準角色)
    void faceTarget()
    {
        //transform.LookAt(targetObject.transform.position);

        
        Vector3 targetDir = targetObject.transform.position - transform.position;                               // 計算敵人與角色之間的向量
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 0.1f * Time.deltaTime, 0.0F);      // 依照敵人Z軸向量與兩者間向量，可以計算出需要旋轉的角度
        transform.rotation = Quaternion.LookRotation(newDir);                                                   // 進行旋轉
        
    }


    private void Patrolling()
    {
        navMeshAgent.SetDestination(points[targetPoint].position);
     
        if (navMeshAgent.remainingDistance < 0.5f)
        {
            targetPoint=(targetPoint+1)%points.Length;
            
            navMeshAgent.SetDestination(points[targetPoint].position);
        }


    }

    private void ChasePlayer()
    {
        faceTarget(); // 將敵人一直正面面對角色，因為敵人和角色位置會變化，所以要不斷Update

        navMeshAgent.enabled = true;
        //anim.SetBool("Walk", true);

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

}
