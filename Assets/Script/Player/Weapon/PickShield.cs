using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickShield : MonoBehaviour
{
    /*
    public GameObject[] weaponObjects;

    public Weapon WeaponScript; //�Z���]�w�}��
    public Rigidbody rb;
    public BoxCollider coll;
    */
    public Transform Player;

    public float PickRange; //�B���d��
    
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
        //���p���a�a��޵P�ë��U��LE��
        
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
        //�}�Ҭ޵P���I������
        rb.isKinematic = true;
        coll.isTrigger = true;
        */

        /*
        //�N�޵P���쪱�a���W��Z�����Ū���(Weaponpoint=Player2.0/Camera/Weapon)
        //�]�m�޵P����m�M����ƭȬ�0
        transform.SetParent(Weaponpoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation=Quaternion.Euler(Vector3.zero);
        //transform.localScale= Vector3.one;
        

        //�ϥ�Weapon
        WeaponScript.enabled = true;
        */
    }
}
