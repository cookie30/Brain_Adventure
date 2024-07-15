using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class SettingMenu : MonoBehaviour
{
    //畫面品質按鈕
    public TMP_Dropdown graphicsDropdown;
    //全部、背景音樂、音效滑桿
    public Slider masterVol, BGMVol, SEVol;
    //混音器
    public AudioMixer mainAudioMixer;
    //切換視角按鈕和默認設定
    public Toggle FirstView, TriView;
    public static bool DefaultView=true;
    

    private void Start()
    {
        DefaultView = true;
    }

    public void ChangeQuality()
    {
        QualitySettings.SetQualityLevel(graphicsDropdown.value);
    }

    public void ChangeMasterVolume()
    {
        mainAudioMixer.SetFloat("Master",masterVol.value);
    }

    public void ChangeBGMVolume()
    {
        mainAudioMixer.SetFloat("BGM", BGMVol.value);
    }

    public void ChangeSEVolume()
    {
        mainAudioMixer.SetFloat("SE", SEVol.value);
    }

    public void ChangetoFirstView()
    {
        DefaultView = true;
    }

    public void ChangetoTriView()
    {
        DefaultView = false;
    }
}
