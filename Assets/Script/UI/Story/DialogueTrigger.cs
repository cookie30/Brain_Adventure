using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public DialogueTrigger nextDialogueTrigger;  //指向下一段對話
    public bool firstplay;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue,this);
    }

    public void TriggerNextDialogue()
    {
        if (nextDialogueTrigger != null)
        {
            nextDialogueTrigger.TriggerDialogue();
        }
    }
}


