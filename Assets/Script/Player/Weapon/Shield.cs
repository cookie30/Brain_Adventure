using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shield : MonoBehaviour
{

    //�ޯ����ɶ�
    float PushDuration = 1.5f;
    float DefenseDuration = 1.0f;
    //�ޯબ�A
    private bool isSkillActive=false;
    public bool PlayerUsePush;
    public bool PlayerUseDefense;

    //�p��ޯ�ɶ����p�⾹
    private float PushTimer=0f;
    private float DefenseTimer=0f;

    //�޵P�ʵe�����
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

        // �˴����a�O�_���U�F�k��A�åB�ޯ�èS���B��}�Ҫ��A
        if (Input.GetMouseButtonDown(1) && !isSkillActive)
        {
            DefenseSkill();
        }

        // �p�G�޵P�ޯ�w�ҰʡA��s�p�ɾ�
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

    //�����ޯ�}�Үɰ��檺�ʧ@
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

    //���m�ޯ�}�Үɰ��檺�ʧ@
    void DefenseSkill()
    {
        isSkillActive = true;
        DefenseTimer = 0f;

        anim.SetTrigger("Defense");

        //�L�ĤƼĤH����(�N�ĤH�������O�k�s)
        for (int i = 0; i < Enemy.enemyattack.Length; i++)
        {
            Enemy.enemyattack[i] = 0f;
        }

        print("�}�l���m�I");

    }

    public void StartDefense()
    {
        PlayerUseDefense = true;
    }

    public void EndDefense()
    {
        PlayerUseDefense= false;
    }

    //���m�ޯ������ɰ��檺�ʧ@
    void DeactivateShieldSkill()
    {
        isSkillActive = false;
        PlayerUseDefense = false;
        PlayerUsePush=false;

        //�b�o�̸Ѱ��L�ĤƼĤH�������ĪG(��_�ĤH�������O)
        Enemy.enemyattack[0] = 1f; // �Ĥ@�ا����O��_��1
        Enemy.enemyattack[1] = 3f; // �ĤG�ا����O��_��3
        Enemy.enemyattack[2] = 5f; // �ĤT�ا����O��_��5
        Enemy.enemyattack[3] = 7f; // �ĥ|�ا����O��_��7
        Enemy.enemyattack[4] = 10f; // �Ĥ��ا����O��_��10

    }
}
