using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{

    public GameManager gameManager;

    //���ܤ�r
    private Queue<string> sentences;

    //��DialogueTrigger������
    private DialogueTrigger Begin;
    private DialogueTrigger End;
    private DialogueTrigger Stage1;
    private DialogueTrigger Stage2;
    private DialogueTrigger Stage3;
    private DialogueTrigger Stage4;
    private DialogueTrigger Stage5;

    //��ܮؤ���
    public GameObject MessageFrame;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Story;
    public Animator frameAnim;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //���}�������a���Ĳ�o���
        Begin=GameObject.Find("UI/Story/�}�l").GetComponent<DialogueTrigger>();
        End= GameObject.Find("UI/Story/����").GetComponent<DialogueTrigger>();
        Stage1 =GameObject.Find("UI/Story/�Ĥ@��").GetComponent<DialogueTrigger>();
        Stage2=GameObject.Find("UI/Story/�ĤG��").GetComponent<DialogueTrigger>();
        Stage3=GameObject.Find("UI/Story/�ĤT��").GetComponent<DialogueTrigger>();
        Stage4=GameObject.Find("UI/Story/�ĥ|��").GetComponent<DialogueTrigger>();
        Stage5=GameObject.Find("UI/Story/�Ĥ���").GetComponent<DialogueTrigger>();

        PlayLevelStory();

        MessageFrame = GameObject.Find("UI/Story/��ܮ�");
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

    //�}�l�@��
    public void StartDialogue(Dialogue dialogue)
    {
        frameAnim.SetBool("isOpen", true);
        Name.text = dialogue.�W�r;

        //�M�ũ��ܤ�r���ǦC(�W�@�������e)
        sentences.Clear();
        

        //�b�ǦC����Jdialogue����̳]�w����ܤ�r
        foreach (string ��ܤ��e in dialogue.�@�����e)
        {
            sentences.Enqueue(��ܤ��e);
        }

        //��ܩ�Jsentences���@�����e
        DisplayNextSentence();
    }

    //��ܤU�@�q��r(�U�@�����s)
    public void DisplayNextSentence()
    {
        //��ǦC������ƶq��0(��r��ܧ��F)
        if (sentences.Count == 0)
        {
            //�������
            EndDialogue();
            return;
        }

        //���r���ܼ�sentence����ǦC
        string sen = sentences.Dequeue();

        StopAllCoroutines();
        //���奻�v�r���
        StartCoroutine(TypeSentences(sen));
    }

    //�v�r��ܳ]�w
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
        print("�@����ܩ񧹤F�I");
        frameAnim.SetBool("isOpen",false);
    }
}
