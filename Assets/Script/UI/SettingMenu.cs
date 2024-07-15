using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class SettingMenu : MonoBehaviour
{
    //�e���~����s
    public TMP_Dropdown graphicsDropdown;
    //�����B�I�����֡B���ķƱ�
    public Slider masterVol, BGMVol, SEVol;
    //�V����
    public AudioMixer mainAudioMixer;
    //�����������s�M�q�{�]�w
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
