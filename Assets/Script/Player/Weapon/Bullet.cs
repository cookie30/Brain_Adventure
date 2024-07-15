using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 5); // 子彈預設十秒後會自動刪除自己
    }

    // 碰撞偵測：如果碰到帶有Collision的物件則刪除自己
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
