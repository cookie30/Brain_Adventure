using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipeController : MonoBehaviour, IEndDragHandler
{
    [SerializeField] int MaxPage;
    public int CurrentPage;
    Vector3 targetPos;
    [SerializeField] Vector3 PageStep;
    [SerializeField] RectTransform levelPagesRect;

    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;
    float drag;

    //頁面進度條設定
    [SerializeField] Image[] barImage;
    [SerializeField] Sprite barClosed, barOpen;

    //切換頁面按鈕設定
    [SerializeField] Button PrevBtn, nextBtn;

    private void Awake()
    {
        CurrentPage = 1;
        targetPos = levelPagesRect.localPosition;
        drag = Screen.width / 50;
        UpdateBar();
        UpdateArrowButton();
    }

    public void Next()
    {
        if (CurrentPage < MaxPage)
        {
            CurrentPage++;
            targetPos += PageStep;
            MovePage();
        }
    }

    public void Previous()
    {
        if (CurrentPage > 1)
        {
            CurrentPage--;
            targetPos -= PageStep;
            MovePage();
        }
    }

    void MovePage()
    {
        levelPagesRect.LeanMoveLocal(targetPos,tweenTime).setEase(tweenType);
        UpdateBar();
        UpdateArrowButton();
    }

    //在鼠標開始拖曳時自動前往下一頁或上一頁
    public void OnEndDrag(PointerEventData eventData)
    {
        //假如拖曳鼠標的範圍(結束拖曳-開始拖曳)>自動拖曳的值
        if (Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > drag)
        {
            if (eventData.position.x > eventData.pressPosition.x)
            {
                Previous();
            }
            else
            {
                Next();
            }
        }
        else
        {
            MovePage();
        }
    }

    void UpdateBar()
    {
        foreach(var item in barImage)
        {
            item.sprite = barClosed;
        }
        barImage[CurrentPage-1].sprite = barOpen;
    }

    void UpdateArrowButton()
    {
        nextBtn.interactable = true;
        PrevBtn.interactable = true;

        if (CurrentPage == 1)
        {
            PrevBtn.interactable = false;
        }
        else if (CurrentPage==MaxPage)
        {
            nextBtn.interactable = false;
        }
        {
             
        }
    }
}
