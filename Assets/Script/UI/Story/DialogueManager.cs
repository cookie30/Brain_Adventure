using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class DialogueManager : MonoBehaviour
{
    public GameManager gameManager;

    public Hip Hip;
    private int dialoguePhase = 0;    // 追蹤對話的階段

    //是否達成結局的變數
    

    //放姓名、對話和頭像
    private Queue<string> sentences;
    private Queue<string> speakers;
    private Queue<Texture> portraits;
    private DialogueTrigger currentTrigger;
    public int SentenceIndex = 0; // 追蹤句子(在sentences的編號)
    public StoryController storyController;
    private string levelName; //現在啟用的DialogueTrigger物件名稱
    public bool DialogueisPlay;

    //儲存角色動作的字典
    private Dictionary<string, Dictionary<string, Dictionary<int, string>>> characterActions;
    public GameObject 劇情_本體;
    public GameObject 劇情_理性精神體;
    public GameObject 劇情_感性精神體;

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
    public RawImage PortraitImage;
    public Animator frameAnim;

    // Start is called before the first frame update
    void Start()
    {
        DialogueisPlay = false;

        speakers = new Queue<string>();
        sentences = new Queue<string>();
        portraits = new Queue<Texture>();

        if (Hip != null )
        {
            Hip.gameObject.SetActive( true );
        }
        else
        {
            return;
        }


        劇情_本體 = GameObject.Find("劇情_本體");
        if (劇情_本體 == null) return;
        劇情_感性精神體 = GameObject.Find("劇情_感性精神體");
        if (劇情_感性精神體 == null) return;
        劇情_理性精神體 = GameObject.Find("劇情_理性精神體");

        MessageFrame = GameObject.Find("UI/Story/對話框");

        if (MessageFrame != null)
        {
            Name = MessageFrame.transform.Find("Name").GetComponent<TextMeshProUGUI>();
            Story = MessageFrame.transform.Find("Story").GetComponent<TextMeshProUGUI>();
            PortraitImage = GameObject.Find("UI/Story/對話框/Portraits").GetComponent<RawImage>();
            frameAnim = MessageFrame.transform.GetComponent<Animator>();
        }



        storyController =GameObject.FindObjectOfType<StoryController>();
        if (storyController == null)
        {
            return;

        }

        characterActions = new Dictionary<string, Dictionary<string, Dictionary<int, string>>>()
        {
            {   "開始",new Dictionary<string, Dictionary<int, string>>
                {
                    {"本體",new Dictionary<int, string>
                        {
                            { 2,"Dying"},
                            { 3,"Sad"}

                        }
                    },
                    {"感性精神體", new Dictionary<int, string>
                        {
                            { 8, "Shock" }
                        }
                    },
                    {"理性精神體",new Dictionary<int, string>
                        {
                            {9,"Think" }
                        }
                    }
                }

            }
        };

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

    }

    //根據關卡進度撥放對應劇情
    public void PlayLevelStory()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Level1-1":
                Begin.TriggerDialogue();
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
            case "End Scene":
                if(gameManager.isLastLevel == true)
                    End.TriggerDialogue();
                break;

        }
    }

    //開始播放劇情
    public void StartDialogue(Dialogue dialogue,DialogueTrigger trigger)
    {

        DialogueisPlay = true;
        frameAnim.SetBool("isOpen", true);
        //儲存現在開啟的Trigger
        currentTrigger = trigger;
        SentenceIndex = 0;

        levelName=trigger.gameObject.name;

        ////清空放姓名、對話和頭像的序列(上一頁的內容)
        if (speakers == null)
        {
            speakers=new Queue<string>();
        }
        else
        {
            speakers.Clear();
        }

        if (sentences == null)
        {
            sentences=new Queue<string>();
        }
        else
        {
            sentences.Clear();
        }

        if (portraits == null)
        {
            portraits=new Queue<Texture>();
        }
        else
        {
            portraits.Clear();
        }

        //在序列中放入dialogue物件裡設定的名字、頭像
        foreach (string speaker in dialogue.名字)
        {
            speakers.Enqueue(speaker);
        }

        foreach (Texture portrait in dialogue.portraits)  // 將 Texture 存入隊列
        {
            portraits.Enqueue(portrait);
        }

        //在對話序列中放入dialogue物件裡設定的對話
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
        if (sentences == null && currentTrigger != null)
        {
            sentences = new Queue<string>();
            foreach (string 顯示內容 in currentTrigger.dialogue.劇情內容)
            {
                sentences.Enqueue(顯示內容);
            }

            return;
        }

        if (speakers == null && currentTrigger != null)
        {
            speakers = new Queue<string>();
            foreach (string 顯示內容 in currentTrigger.dialogue.劇情內容)
            {
                sentences.Enqueue(顯示內容);
            }
            return;
        }

        //Debug.Log("Button clicked, DisplayNextSentence called.");

        //當序列的元件數量為0(文字顯示完了)
        if (sentences.Count == 0||speakers.Count==0)
        {
            //Debug.Log("No more sentences to display.");
            //結束對話
            EndDialogue();
            DialogueisPlay=false;
            //print("End:" + DialogueisPlay);
            return;
        }

        //讓字元變數sentence等於序列
        string spk = speakers.Dequeue();
        //if (speakers.Count == 0)
        //{
        //    spk = "No Name";
        //}

        string sen = sentences.Dequeue();
        //if (sentences.Count == 0)
        //{
        //    sen = "...";
        //}

        //讓PortraitImage的貼圖等於portrait序列的貼圖(沒有的話就=null)
        Texture portrait = portraits.Count>0 ? portraits.Dequeue() : null;
        PortraitImage.texture = portrait;

        //根據句子編號來切換模型動作
        SwitchCharacterModel(levelName, spk, SentenceIndex);
        SentenceIndex++;

        //在啟動逐字效果前
        StopAllCoroutines();
        //讓文本逐字顯示
        StartCoroutine(TypeSentences(spk,sen,portrait));

    }

    //逐字顯示設定
    IEnumerator TypeSentences(string speaker,string sentence,Texture portrait)
    {
        if (speaker != null)
        {
            Name.text = speaker;
            Name.gameObject.SetActive(true);
        }
        else
        {
            Name.gameObject.SetActive(false);
        }

        // 如果 portrait 不是 null，則顯示頭像，否則隱藏頭像
        if (portrait != null)
        {
            PortraitImage.texture = portrait;
            PortraitImage.gameObject.SetActive(true);  // 顯示頭像
        }
        else
        {
            PortraitImage.gameObject.SetActive(false); // 隱藏頭像
        }

        Story.text = "";
        PortraitImage.texture = portrait;
        foreach (char letter in sentence.ToCharArray())
        {
            Story.text += letter;
            yield return null;
        }

    }

    void SwitchCharacterModel(string levelName, string speaker, int sentenceIndex)
    {
        // 確保 storyController 不為 null
        if (storyController == null)
        {
            Debug.LogError("StoryController is not initialized.");
            return;
        }

        // 確保 characterModels 不為 null
        if (storyController.characterModels == null)
        {
            //Debug.Log("CharacterModels is null.");
            return;
        }

        // 確保 characterActions 不為 null
        if (characterActions == null)
        {
            Debug.LogError("CharacterActions is null.");
            return;
        }

        // 隱藏所有角色模型
        foreach (var character in storyController.characterModels)
        {
            character.Value.SetActive(false);
        }

        // 顯示並執行特定角色的動作
        if (storyController.characterModels.TryGetValue(speaker, out GameObject characterModel))
        {
            characterModel.SetActive(true);

            if (characterActions.TryGetValue(levelName, out Dictionary<string, Dictionary<int, string>> levelAction))
            {
                if (levelAction.TryGetValue(speaker, out Dictionary<int, string> actionsForSpecificLines))
                {
                    if (actionsForSpecificLines.TryGetValue(sentenceIndex, out string specificAction))
                    {
                        storyController.SetCharacterAction(speaker, specificAction);
                        return;
                    }
                }
            }

            // 如果找不到特定的動作，則使用默認的Idle動作
            storyController.SetCharacterAction(speaker, "Idle");

            if (storyController.characterModels.TryGetValue(speaker, out GameObject characterModels))
            {
                characterModel.SetActive(true);
            }
        }
    }

    void EndDialogue()
    {
        //print("劇情對話放完了！");

        if (frameAnim != null)
        {
            frameAnim.SetBool("isOpen", false);
        }
        else
        {
            Debug.LogError("frameAnim 引用為空，請在 Inspector 中指定");
        }

        if (sentences != null) sentences.Clear();
        if (speakers != null) speakers.Clear();
        if (portraits != null) portraits.Clear();

        //在1-1關接續撥放劇情和打開操作介面
        if (SceneManager.GetActiveScene().name == "Level1-1")
        {
            dialoguePhase++;
            Debug.Log($"對話階段:{dialoguePhase}");

            if (dialoguePhase == 1)
            {
                // 如果是第一段對話結束，開始第二段對話
                currentTrigger.TriggerNextDialogue();
            }
            else if (dialoguePhase == 2)
            {
                // 如果是第二段對話結束，顯示操作說明介面
                //instructionUI.SetActive(true);

                Hip.OpenHip();
            }
        }

        DialogueisPlay = false;
    }

}
