using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//設定劇情UI的內容
[System.Serializable]
public class Dialogue
{
    public string 名字;

    [TextArea(3,5)]
    public string[] 劇情內容;

}
