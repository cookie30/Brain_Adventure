using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shield : MonoBehaviour
{

    //技能持續時間
    float PushDuration = 1.5f;
    float DefenseDuration = 1.0f;
    //技能狀態
    private bool isSkillActive=false;
    public bool PlayerUsePush;
    public bool PlayerUseDefense;

    //計算技能時間的計算器
    private float PushTimer=0f;
    private float DefenseTimer=0f;

    //盾牌動畫的控制器
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isSkillActive)
        {
            PushSkill();
        }

        // 檢測玩家是否按下了右鍵，並且技能並沒有處於開啟狀態
        if (Input.GetMouseButtonDown(1) && !isSkillActive)
        {
            DefenseSkill();
        }

        // 如果盾牌技能已啟動，更新計時器
        if (isSkillActive)
        {
            PushTimer += Time.deltaTime;
            DefenseTimer += Time.deltaTime;
            if (PushTimer > PushDuration)
            {
                DeactivateShieldSkill();
            }
            else if (DefenseTimer > DefenseDuration)
            {
                DeactivateShieldSkill();
            }
        }
    }

    //推擊技能開啟時執行的動作
    void PushSkill()
    {
        isSkillActive = true;
        
        PushTimer = 0f;

        anim.SetTrigger("Push");

    }

    public void StartPush()
    {
        PlayerUsePush = true;
    }

    public void EndPush()
    {
        PlayerUsePush = false;
    }

    //防禦技能開啟時執行的動作
    void DefenseSkill()
    {
        isSkillActive = true;
        DefenseTimer = 0f;

        anim.SetTrigger("Defense");

        //無效化敵人攻擊(將敵人的攻擊力歸零)
        for (int i = 0; i < Enemy.enemyattack.Length; i++)
        {
            Enemy.enemyattack[i] = 0f;
        }

        print("開始防禦！");

    }

    public void StartDefense()
    {
        PlayerUseDefense = true;
    }

    public void EndDefense()
    {
        PlayerUseDefense= false;
    }

    //防禦技能關閉時執行的動作
    void DeactivateShieldSkill()
    {
        isSkillActive = false;
        PlayerUseDefense = false;
        PlayerUsePush=false;

        //在這裡解除無效化敵人攻擊的效果(恢復敵人的攻擊力)
        Enemy.enemyattack[0] = 1f; // 第一種攻擊力恢復為1
        Enemy.enemyattack[1] = 3f; // 第二種攻擊力恢復為3
        Enemy.enemyattack[2] = 5f; // 第三種攻擊力恢復為5
        Enemy.enemyattack[3] = 7f; // 第四種攻擊力恢復為7
        Enemy.enemyattack[4] = 10f; // 第五種攻擊力恢復為10

    }
}
