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

    //�����i�ױ��]�w
    [SerializeField] Image[] barImage;
    [SerializeField] Sprite barClosed, barOpen;

    //�����������s�]�w
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

    //�b���ж}�l�즲�ɦ۰ʫe���U�@���ΤW�@��
    public void OnEndDrag(PointerEventData eventData)
    {
        //���p�즲���Ъ��d��(�����즲-�}�l�즲)>�۰ʩ즲����
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
