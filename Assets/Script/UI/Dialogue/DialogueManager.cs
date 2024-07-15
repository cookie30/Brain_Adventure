using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{

    public GameManager gameManager;

    //放對話文字
    private Queue<string> sentences;

    //放DialogueTrigger的物件
    private DialogueTrigger Begin;
    private DialogueTrigger End;
    private DialogueTrigger Stage1;
    private DialogueTrigger Stage2;
    private DialogueTrigger Stage3;
    private DialogueTrigger Stage4;
    private DialogueTrigger Stage5;

    //對話框元件
    public GameObject MessageFrame;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Story;
    public Animator frameAnim;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //找到腳本掛的地方並觸發對話
        Begin=GameObject.Find("UI/Story/開始").GetComponent<DialogueTrigger>();
        End= GameObject.Find("UI/Story/結局").GetComponent<DialogueTrigger>();
        Stage1 =GameObject.Find("UI/Story/第一關").GetComponent<DialogueTrigger>();
        Stage2=GameObject.Find("UI/Story/第二關").GetComponent<DialogueTrigger>();
        Stage3=GameObject.Find("UI/Story/第三關").GetComponent<DialogueTrigger>();
        Stage4=GameObject.Find("UI/Story/第四關").GetComponent<DialogueTrigger>();
        Stage5=GameObject.Find("UI/Story/第五關").GetComponent<DialogueTrigger>();

        PlayLevelStory();

        MessageFrame = GameObject.Find("UI/Story/對話框");
        frameAnim = MessageFrame.GetComponent<Animator>();
    }

    public void PlayLevelStory()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Level1-1":
                Begin.TriggerDialogue();
                Stage1.TriggerDialogue();
                break;
            case "Level2-1":
                Stage2.TriggerDialogue();
                break;
            case "Level3-1":
                Stage3.TriggerDialogue();
                break;
            case "Level4-1":
                Stage4.TriggerDialogue();
                break;
            case "Level5-1":
                Stage5.TriggerDialogue();
                break;
            case "Level5-5":
                if(gameManager.ClearLevel==true)
                    End.TriggerDialogue();
                break;

        }
    }

    //開始劇情
    public void StartDialogue(Dialogue dialogue)
    {
        frameAnim.SetBool("isOpen", true);
        Name.text = dialogue.名字;

        //清空放對話文字的序列(上一頁的內容)
        sentences.Clear();
        

        //在序列中放入dialogue物件裡設定的對話文字
        foreach (string 顯示內容 in dialogue.劇情內容)
        {
            sentences.Enqueue(顯示內容);
        }

        //顯示放入sentences的劇情內容
        DisplayNextSentence();
    }

    //顯示下一段文字(下一頁按鈕)
    public void DisplayNextSentence()
    {
        //當序列的元件數量為0(文字顯示完了)
        if (sentences.Count == 0)
        {
            //結束對話
            EndDialogue();
            return;
        }

        //讓字元變數sentence等於序列
        string sen = sentences.Dequeue();

        StopAllCoroutines();
        //讓文本逐字顯示
        StartCoroutine(TypeSentences(sen));
    }

    //逐字顯示設定
    IEnumerator TypeSentences(string sentence)
    {
        Story.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            Story.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        print("劇情對話放完了！");
        frameAnim.SetBool("isOpen",false);
    }
}
