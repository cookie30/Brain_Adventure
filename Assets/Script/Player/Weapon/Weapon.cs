using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // 要有這個才能控制文字框
using UnityEngine.InputSystem; //新的輸入系統
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [Header("參考物件")]
    public GameObject PlayerObejct;
    public Camera PlayerCamera;
    public Transform[] attackPoint;

    [Header("預置物件")]
    public GameObject bullet;

    [Header("武器設定")]
    public bool isGun;
    public int magazineSize;        // 設定彈夾可以放多少顆子彈？
    public int bulletsLeft;         // 子彈還有多少顆？(如果沒有要測試，你可以設定成 Private)
    public int bulletcount;         //每次可以射出多少子彈？
    public float reloadTime;        // 設定換彈夾所需要的時間
    public float recoilForce;       // 反作用力
    public static float[] fireRate = {0.2f,0.5f};          //攻擊間隔
    public static float nextFire;          //下次攻擊時間
    public AudioClip fireSound;

    bool reloading;             // 布林變數：儲存是不是正在換彈夾的狀態？True：正在換彈夾、False：換彈夾的動作已結束
    public int weaponNumber;

    [SerializeField]private LayerMask aimCoillderLayerMask=new LayerMask();

    [Header("UI物件")]
    public TextMeshProUGUI ammunitionDisplay; // 彈量顯示
    public TextMeshProUGUI reloadingDisplay;  // 顯示是不是正在換彈夾？

    private void Start()
    {
        ammunitionDisplay=GameObject.Find("UI/Ammo/ammunition").GetComponent<TextMeshProUGUI>();
        reloadingDisplay = GameObject.Find("UI/reloading").GetComponent<TextMeshProUGUI>();

        if (this.transform.name == "機關槍")
        {
            magazineSize = 100;
            //string path = GetFullPath(this.transform);
            //print(path);
        }
        else if (this.transform.name == "狙擊槍") 
        { 
            magazineSize = 20;
        }
        

        bulletsLeft = magazineSize;
        reloadingDisplay.enabled = false;  // 將顯示正在換彈夾的字幕隱藏起來

        ShowAmmoDisplay();                 // 更新彈量顯示
    }

    // 方法：射擊武器
    public void Attack()
    {
        //隨機決定子彈要從哪個發射點出來
        //make random point from attackPoints length(1~limet)
        int Random_Points = Random.Range(1, attackPoint.Length);

        if (isGun && bulletsLeft > 0 && !reloading)
        {
            //Ray ray = PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));  // 從攝影機射出一條射線
            //Ray ray = new Ray(transform.position, transform.forward);  // 從攝影機射出一條射線

            Vector2 ScreenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Ray ray = Camera.main.ScreenPointToRay(ScreenCenterPoint);

            RaycastHit hit;  // 宣告一個射擊點
            Vector3 targetPoint;  // 宣告一個位置點變數，到時候如果有打到東西，就存到這個變數


            // 如果射線有打到具備碰撞體的物件
            //if (Physics.Raycast(ray, out hit) == true)
            //if(Physics.Raycast(Camera.main.transform.position,Camera.main.transform.TransformDirection(Vector3.forward)*10,out hit,Mathf.Infinity))
            //{
            //    targetPoint = hit.point;         // 將打到物件的位置點存進 targetPoint
            //    //print(hit.transform.name);
            //}
            if(Physics.Raycast(ray,out hit, 999f, aimCoillderLayerMask))
            {
                targetPoint= hit.point;
            }
            else
            {
                targetPoint = ray.GetPoint(75);  // 如果沒有打到物件，就以長度75的末端點取得一個點，存進 targetPoint
            }
            //Debug.DrawRay(ray.origin, targetPoint - ray.origin, Color.red, 10); // 畫出這條射線

            //畫出射線(子彈發射點的位置,射線打到的點-射線的原點,指定線畫成紅色)
            //Debug.DrawRay(attackPoint[Random_Points].transform.position, targetPoint - ray.origin, Color.red, 10);

            //子彈真正要飛行的方向
            // 以起點與終點之間兩點位置，計算出射線的方向
            Vector3 shootingDirection = targetPoint - attackPoint[Random_Points].transform.position; 
            // 在攻擊點上面產生一個子彈
            GameObject currentBullet = Instantiate(bullet, attackPoint[Random_Points].transform.position, Quaternion.identity);
            
            // 將子彈飛行方向與射線方向一致
            currentBullet.transform.forward = shootingDirection.normalized; 

            //設定子彈的飛行(方向和力度)
            if (this.transform.name == "機關槍")
            {

                currentBullet.GetComponent<Rigidbody>().AddForce(currentBullet.transform.forward * 40, ForceMode.Impulse);
                SoundEffectManager.Instance.PlaySound(fireSound, transform, 1f);

            }
            else if (this.transform.name == "狙擊槍")
            {
                currentBullet.GetComponent<Rigidbody>().AddForce(currentBullet.transform.forward * 100, ForceMode.Impulse);
                SoundEffectManager.Instance.PlaySound(fireSound, transform, 1f);
            }

            bulletsLeft -=bulletcount;    // 將彈夾中的子彈減去設定好的子彈射出量，以下的寫法都是一樣的意思
            // 後座力模擬
            //PlayerObejct.GetComponent<Rigidbody>().AddForce(-shootingDirection.normalized * recoilForce, ForceMode.Impulse);
        }
        ShowAmmoDisplay();                 // 更新彈量顯示

        // 根據武器播放攻擊動畫
        if (this.transform.name == "機關槍") 
        { 
            if (transform.GetChild(0).GetComponent<Animator>() != null)
                transform.GetChild(0).GetComponent<Animator>().SetTrigger("Attack");  
        }
        else if(this.transform.name == "狙擊槍")
        {
            /*
            if (transform.GetChild(1).GetComponent<Animator>() != null)
                transform.GetChild(1).GetComponent<Animator>().SetTrigger("Attack");
            */
        }
        
    }

    private void OnEnable()
    {
        ShowAmmoDisplay();                 // 更新彈量顯示
    }

    // 方法：換彈夾的延遲時間設定
    public void Reload()
    {
        if (bulletsLeft < magazineSize&& !reloading)
        {
            reloading = true;                      // 首先將換彈夾狀態設定為：正在換彈夾
            reloadingDisplay.enabled = true;       // 將正在換彈夾的字幕顯示出來
            Invoke("ReloadFinished", reloadTime);  // 依照reloadTime所設定的換彈夾時間倒數，時間為0時執行ReloadFinished方法
        }
    }

    // 方法：換彈夾
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;            // 將子彈填滿
        reloading = false;                     // 將換彈夾狀態設定為：更換彈夾結束
        reloadingDisplay.enabled = false;      // 將正在換彈夾的字幕隱藏，結束換彈夾的動作
        ShowAmmoDisplay();
    }

    // 方法：更新彈量顯示
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

    //顯示物件路徑
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