using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//設定劇情UI的內容
[System.Serializable]
public class Dialogue
{
    public string[] 名字;

    //頭像
    public Texture[] portraits;

    [TextArea(3,5)]
    public string[] 劇情內容;

}
