using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryController : MonoBehaviour
{

    private Animator anim;

    // 管理角色模型控制器的字典characterControllers
    private Dictionary<string, StoryController> characterControllers;
    // 管理角色名稱與模型的字典characterModels
    public Dictionary<string, GameObject> characterModels; 

    public GameObject 劇情_本體;
    public GameObject 劇情_理性精神體;
    public GameObject 劇情_感性精神體;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        characterControllers = new Dictionary<string, StoryController>();
        characterModels = new Dictionary<string, GameObject>();

        if (劇情_本體 != null) { 
            characterModels.Add("少女", 劇情_本體);
            //print("本體已被加入字典！");
        }
        if(劇情_理性精神體!=null) characterModels.Add("理性精神體", 劇情_理性精神體);
        if (劇情_感性精神體 != null) characterModels.Add("感性精神體", 劇情_感性精神體);

        AddCharacterController("少女", 劇情_本體);
        AddCharacterController("感性精神體", 劇情_感性精神體);
        AddCharacterController("理性精神體", 劇情_理性精神體);

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
                // 直接取得 name 屬性，控制器不為 null 時 controllerName 為 controller.name，否則為 "Unknown"
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

    // 添加角色控制器到字典中
    public void AddCharacterController(string characterName, GameObject controller)
    {
        //如果字典裡的角色名字/GameObject物件是空的
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

        //檢查字典中是否已經包含了characterName，如果沒有則添加進字典
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

    // 根據角色切換動作
    public void SetCharacterAction(string characterName, string actionName)
    {
        //假如在characterControllers找到characterName，則傳回對應的StoryController(命名成controller)
        if (characterModels.TryGetValue(characterName, out GameObject character))
        {
            Animator charaim = character.GetComponent<Animator>();
            charaim.Play(actionName);
        }
    }

    //切換動作
    public void SetAction(string actionName)
    {
        if (anim != null)
        {
            anim.Play(actionName);
        }
    }
}
