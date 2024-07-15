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
        //���۳o�Ӹ}��������A���檺�ʵe�ĪG�A����ɶ�(���s��j)
        LeanTween.scale(gameObject,UpScale,0.1f);
        //���s����0.1����Y�^�쪬
        LeanTween.scale(gameObject,Vector3.one,0.1f).setDelay(0.1f);
    }
}
