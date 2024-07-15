using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour
{
    Button btn;
    Vector3 UpScale = new Vector3(1.2f,1.2f,1);

    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(ClickAnimation);
    }

    void ClickAnimation()
    {
        //掛著這個腳本的物件，執行的動畫效果，持續時間(按鈕放大)
        LeanTween.scale(gameObject,UpScale,0.1f);
        //按鈕延遲0.1秒後縮回原狀
        LeanTween.scale(gameObject,Vector3.one,0.1f).setDelay(0.1f);
    }
}
