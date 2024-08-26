using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionManager : MonoBehaviour
{
    public GameObject instructionImage; // 操作說明圖片
    public GameObject closeButton; // 關閉按鈕

    // Start is called before the first frame update
    void Start()
    {

    }

    public void ShowInstruction()
    {
        // 顯示圖片
        instructionImage.SetActive(true);
        closeButton.SetActive(true);
    }

    public void CloseInstruction()
    {
        // 隱藏圖片
        instructionImage.SetActive(false);
        closeButton.SetActive(false);
    }
}
