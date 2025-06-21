using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public Transform Target;

    private void Start()
    {
        Target=GameObject.Find("Player").GetComponent<Transform>();
    }

    //LateUpdate:有啟用這個函式所掛著的腳本元件才會執行的Update
    private void LateUpdate()
    {
        //讓地圖icon抓取需要對齊的目標座標(newPoition=目標)
        Vector3 newPosition =Target.position;
        Debug.Log("Target Position: " + newPosition);
        newPosition.y=transform.position.y;
        transform.position = newPosition;

        //設定地圖icon的位置(目標
        transform.rotation=Quaternion.Euler(90f,Target.eulerAngles.y,0f);
    }
}
