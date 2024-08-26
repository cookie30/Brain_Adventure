using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickShield : MonoBehaviour
{
    /*
    public GameObject[] weaponObjects;

    public Weapon WeaponScript; //武器設定腳本
    public Rigidbody rb;
    public BoxCollider coll;
    */
    public Transform Player;

    public float PickRange; //拾取範圍
    
    public bool equipped;
    public static bool slotFull = false;

    private void Start()
    {
        Player = GameObject.Find("Player").transform;
        /*
        if (!equipped)
        {
            WeaponScript.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
        }
        else if (equipped)
        {
            WeaponScript.enabled = true;
            rb.isKinematic = true;
            coll.isTrigger = true;
        }
        */
    }

    // Start is called before the first frame update
    private void Update()
    {
        Vector3 distanseToPlayer = Player.position - transform.position;
        //假如玩家靠近盾牌並按下鍵盤E鍵
        
        if (!equipped && distanseToPlayer.magnitude <= PickRange && Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }
        


    }
    private void PickUp()
    {

        equipped = true;
        slotFull=true;

        if(equipped==true)
        {
            Destroy(this.gameObject);
        }

        /*
        //開啟盾牌的碰撞偵測
        rb.isKinematic = true;
        coll.isTrigger = true;
        */

        /*
        //將盾牌移到玩家身上放武器的空物件(Weaponpoint=Player2.0/Camera/Weapon)
        //設置盾牌的位置和旋轉數值為0
        transform.SetParent(Weaponpoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation=Quaternion.Euler(Vector3.zero);
        //transform.localScale= Vector3.one;
        

        //使用Weapon
        WeaponScript.enabled = true;
        */
    }
}
