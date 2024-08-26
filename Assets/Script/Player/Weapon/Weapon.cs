using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // �n���o�Ӥ~�౱���r��
using UnityEngine.InputSystem; //�s����J�t��
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [Header("�ѦҪ���")]
    public GameObject PlayerObejct;
    public Camera PlayerCamera;
    public Transform[] attackPoint;

    [Header("�w�m����")]
    public GameObject bullet;

    [Header("�Z���]�w")]
    public bool isGun;
    public int magazineSize;        // �]�w�u���i�H��h�����l�u�H
    public int bulletsLeft;         // �l�u�٦��h�����H(�p�G�S���n���աA�A�i�H�]�w�� Private)
    public int bulletcount;         //�C���i�H�g�X�h�֤l�u�H
    public float reloadTime;        // �]�w���u���һݭn���ɶ�
    public float recoilForce;       // �ϧ@�ΤO
    public static float[] fireRate = {0.2f,0.5f};          //�������j
    public static float nextFire;          //�U�������ɶ�
    public AudioClip fireSound;

    bool reloading;             // ���L�ܼơG�x�s�O���O���b���u�������A�HTrue�G���b���u���BFalse�G���u�����ʧ@�w����
    public int weaponNumber;

    [SerializeField]private LayerMask aimCoillderLayerMask=new LayerMask();

    [Header("UI����")]
    public TextMeshProUGUI ammunitionDisplay; // �u�q���
    public TextMeshProUGUI reloadingDisplay;  // ��ܬO���O���b���u���H

    private void Start()
    {
        ammunitionDisplay=GameObject.Find("UI/Ammo/ammunition").GetComponent<TextMeshProUGUI>();
        reloadingDisplay = GameObject.Find("UI/reloading").GetComponent<TextMeshProUGUI>();

        if (this.transform.name == "�����j")
        {
            magazineSize = 100;
            //string path = GetFullPath(this.transform);
            //print(path);
        }
        else if (this.transform.name == "�����j") 
        { 
            magazineSize = 20;
        }
        

        bulletsLeft = magazineSize;
        reloadingDisplay.enabled = false;  // �N��ܥ��b���u�����r�����ð_��

        ShowAmmoDisplay();                 // ��s�u�q���
    }

    // ��k�G�g���Z��
    public void Attack()
    {
        //�H���M�w�l�u�n�q���ӵo�g�I�X��
        //make random point from attackPoints length(1~limet)
        int Random_Points = Random.Range(1, attackPoint.Length);

        if (isGun && bulletsLeft > 0 && !reloading)
        {
            //Ray ray = PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));  // �q��v���g�X�@���g�u
            //Ray ray = new Ray(transform.position, transform.forward);  // �q��v���g�X�@���g�u

            Vector2 ScreenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Ray ray = Camera.main.ScreenPointToRay(ScreenCenterPoint);

            RaycastHit hit;  // �ŧi�@�Ӯg���I
            Vector3 targetPoint;  // �ŧi�@�Ӧ�m�I�ܼơA��ɭԦp�G������F��A�N�s��o���ܼ�


            // �p�G�g�u�������ƸI���骺����
            //if (Physics.Raycast(ray, out hit) == true)
            //if(Physics.Raycast(Camera.main.transform.position,Camera.main.transform.TransformDirection(Vector3.forward)*10,out hit,Mathf.Infinity))
            //{
            //    targetPoint = hit.point;         // �N���쪫�󪺦�m�I�s�i targetPoint
            //    //print(hit.transform.name);
            //}
            if(Physics.Raycast(ray,out hit, 999f, aimCoillderLayerMask))
            {
                targetPoint= hit.point;
            }
            else
            {
                targetPoint = ray.GetPoint(75);  // �p�G�S�����쪫��A�N�H����75�������I���o�@���I�A�s�i targetPoint
            }
            //Debug.DrawRay(ray.origin, targetPoint - ray.origin, Color.red, 10); // �e�X�o���g�u

            //�e�X�g�u(�l�u�o�g�I����m,�g�u���쪺�I-�g�u�����I,���w�u�e������)
            //Debug.DrawRay(attackPoint[Random_Points].transform.position, targetPoint - ray.origin, Color.red, 10);

            //�l�u�u���n���檺��V
            // �H�_�I�P���I�������I��m�A�p��X�g�u����V
            Vector3 shootingDirection = targetPoint - attackPoint[Random_Points].transform.position; 
            // �b�����I�W�����ͤ@�Ӥl�u
            GameObject currentBullet = Instantiate(bullet, attackPoint[Random_Points].transform.position, Quaternion.identity);
            
            // �N�l�u�����V�P�g�u��V�@�P
            currentBullet.transform.forward = shootingDirection.normalized; 

            //�]�w�l�u������(��V�M�O��)
            if (this.transform.name == "�����j")
            {

                currentBullet.GetComponent<Rigidbody>().AddForce(currentBullet.transform.forward * 40, ForceMode.Impulse);
                SoundEffectManager.Instance.PlaySound(fireSound, transform, 1f);

            }
            else if (this.transform.name == "�����j")
            {
                currentBullet.GetComponent<Rigidbody>().AddForce(currentBullet.transform.forward * 100, ForceMode.Impulse);
                SoundEffectManager.Instance.PlaySound(fireSound, transform, 1f);
            }

            bulletsLeft -=bulletcount;    // �N�u�������l�u��h�]�w�n���l�u�g�X�q�A�H�U���g�k���O�@�˪��N��
            // ��y�O����
            //PlayerObejct.GetComponent<Rigidbody>().AddForce(-shootingDirection.normalized * recoilForce, ForceMode.Impulse);
        }
        ShowAmmoDisplay();                 // ��s�u�q���

        // �ھڪZ����������ʵe
        if (this.transform.name == "�����j") 
        { 
            if (transform.GetChild(0).GetComponent<Animator>() != null)
                transform.GetChild(0).GetComponent<Animator>().SetTrigger("Attack");  
        }
        else if(this.transform.name == "�����j")
        {
            /*
            if (transform.GetChild(1).GetComponent<Animator>() != null)
                transform.GetChild(1).GetComponent<Animator>().SetTrigger("Attack");
            */
        }
        
    }

    private void OnEnable()
    {
        ShowAmmoDisplay();                 // ��s�u�q���
    }

    // ��k�G���u��������ɶ��]�w
    public void Reload()
    {
        if (bulletsLeft < magazineSize&& !reloading)
        {
            reloading = true;                      // �����N���u�����A�]�w���G���b���u��
            reloadingDisplay.enabled = true;       // �N���b���u�����r����ܥX��
            Invoke("ReloadFinished", reloadTime);  // �̷�reloadTime�ҳ]�w�����u���ɶ��˼ơA�ɶ���0�ɰ���ReloadFinished��k
        }
    }

    // ��k�G���u��
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;            // �N�l�u��
        reloading = false;                     // �N���u�����A�]�w���G�󴫼u������
        reloadingDisplay.enabled = false;      // �N���b���u�����r�����áA�������u�����ʧ@
        ShowAmmoDisplay();
    }

    // ��k�G��s�u�q���
    private void ShowAmmoDisplay()
    {
        if (ammunitionDisplay != null)
        { 
            ammunitionDisplay.SetText($"{bulletsLeft} / {magazineSize}");
        }
        else if (!isGun && ammunitionDisplay != null)
        {
            ammunitionDisplay.SetText("N/A");
        }
    }

    //��ܪ�����|
    string GetFullPath(Transform child)
    {
        string path = child.name;
        while (child.parent != null)
        {
            child = child.parent;
            path = child.name + "/" + path;
        }
        return path;
    }

}