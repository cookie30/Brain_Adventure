using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryController : MonoBehaviour
{

    private Animator anim;

    // �޲z����ҫ�������r��characterControllers
    private Dictionary<string, StoryController> characterControllers;
    // �޲z����W�ٻP�ҫ����r��characterModels
    public Dictionary<string, GameObject> characterModels; 

    public GameObject �@��_����;
    public GameObject �@��_�z�ʺ믫��;
    public GameObject �@��_�P�ʺ믫��;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        characterControllers = new Dictionary<string, StoryController>();
        characterModels = new Dictionary<string, GameObject>();

        if (�@��_���� != null) { 
            characterModels.Add("�֤k", �@��_����);
            //print("����w�Q�[�J�r��I");
        }
        if(�@��_�z�ʺ믫��!=null) characterModels.Add("�z�ʺ믫��", �@��_�z�ʺ믫��);
        if (�@��_�P�ʺ믫�� != null) characterModels.Add("�P�ʺ믫��", �@��_�P�ʺ믫��);

        AddCharacterController("�֤k", �@��_����);
        AddCharacterController("�P�ʺ믫��", �@��_�P�ʺ믫��);
        AddCharacterController("�z�ʺ믫��", �@��_�z�ʺ믫��);

        foreach (var contro in characterControllers) {
            if (contro.Key == null)
            {
                Debug.LogError("Character key is null");
            }
            else if (contro.Value == null)
            {
                Debug.LogError($"Character {contro.Key} has a null controller.");
            }
            else
            {
                // �������o name �ݩʡA������� null �� controllerName �� controller.name�A�_�h�� "Unknown"
                string controllerName = contro.Value.name;

                if (controllerName == null)
                {
                    Debug.Log($"Character {contro.Key} has a controller with a null name.");
                    return;
                }
                else
                {
                    Debug.Log($"Character {contro.Key} is set {controllerName}");
                }

            }
        }

    }

    // �K�[���ⱱ���r�夤
    public void AddCharacterController(string characterName, GameObject controller)
    {
        //�p�G�r��̪�����W�r/GameObject����O�Ū�
        if (string.IsNullOrEmpty(characterName))
        {
            Debug.LogError("Character name is null or empty.");
            return;
        }

        if (controller == null)
        {
            Debug.Log($"Controller for {characterName} is null.");
            return;
        }

        //�ˬd�r�夤�O�_�w�g�]�t�FcharacterName�A�p�G�S���h�K�[�i�r��
        if (!characterModels.ContainsKey(characterName))
        {
            characterModels.Add(characterName, controller);
            Debug.Log($"Added {characterName} to characterModels dictionary.");
        }
        else
        {
            //Debug.LogWarning($"{characterName} already exists in the dictionary.");
        }




    }

    // �ھڨ�������ʧ@
    public void SetCharacterAction(string characterName, string actionName)
    {
        //���p�bcharacterControllers���characterName�A�h�Ǧ^������StoryController(�R�W��controller)
        if (characterModels.TryGetValue(characterName, out GameObject character))
        {
            Animator charaim = character.GetComponent<Animator>();
            charaim.Play(actionName);
        }
    }

    //�����ʧ@
    public void SetAction(string actionName)
    {
        if (anim != null)
        {
            anim.Play(actionName);
        }
    }
}
