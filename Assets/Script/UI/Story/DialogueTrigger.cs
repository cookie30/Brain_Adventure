using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public DialogueTrigger nextDialogueTrigger;  //���V�U�@�q���
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


