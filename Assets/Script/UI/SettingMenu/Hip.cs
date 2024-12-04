using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Hip : MonoBehaviour
{
    //開啟介面
    public UnityEngine.UI.Button HipButton;
    //關閉介面
    private UnityEngine.UIElements.Button closeButton;
    //代表整個介面的元件
    VisualElement rootVisualElement;

    private void Awake()
    {
        rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

    }

    // Start is called before the first frame update
    void Start()
    {
        // 取得 UI Toolkit 的代表元件
        var uiDocumentComponent = GetComponent<UIDocument>();
        rootVisualElement = uiDocumentComponent.rootVisualElement;

        // 設定界面的按鈕點擊事件(開啟說明介面)
        HipButton.onClick.AddListener(OpenHip);

        // 從 UI Toolkit 中找到關閉按鈕
        closeButton = rootVisualElement.Q<UnityEngine.UIElements.Button>("closeButton");

        // 設定 UI Toolkit 按鈕的點擊事件來關閉 UI Toolkit
        closeButton.clicked += CloseUIToolkitUI;

        // 一開始隱藏 UI
        rootVisualElement.style.display = DisplayStyle.None;
    }

    public void OpenHip()
    {
        rootVisualElement.style.display = DisplayStyle.Flex;
    }

    // 當按下關閉按鈕時關閉 UI Toolkit 的界面
    private void CloseUIToolkitUI()
    {
        rootVisualElement.style.display = DisplayStyle.None;  // 顯示 UI Toolkit 元件
    }
}
