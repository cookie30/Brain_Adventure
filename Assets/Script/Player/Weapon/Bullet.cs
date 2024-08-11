using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject HitVFX;
    void Start()
    {
        // 子彈預設五秒後會自動刪除自己
        Destroy(gameObject, 5); 
    }

    // 碰撞偵測：如果碰到帶有Collision的物件
    private void OnCollisionEnter(Collision collision)
    {
        //當撞到第一個東西後，生成打到的特效
        var hiteffact = Instantiate(HitVFX, 
            collision.contacts[0].point,Quaternion.identity) as GameObject;
        //刪除特效和自己
        Destroy(hiteffact, 1f);
        Destroy(gameObject);
    }
}
