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
    //public Toggle FirstView, TriView;
    //public static bool DefaultView=true;
    //CameraChange CameraChange;
    

    private void Start()
    {
        //DefaultView = true;
    }

    public void ChangeQuality()
    {
        QualitySettings.SetQualityLevel(graphicsDropdown.value);
    }

    public void ChangeMasterVolume()
    {
        mainAudioMixer.SetFloat("Master",Mathf.Log10(masterVol.value)*20f);
    }

    public void ChangeBGMVolume()
    {
        mainAudioMixer.SetFloat("BGM", Mathf.Log10(BGMVol.value) * 20f);
    }

    public void ChangeSEVolume()
    {
        mainAudioMixer.SetFloat("SE", Mathf.Log10(SEVol.value) * 20f);
    }

    //public void ChangetoFirstView()
    //{
    //    DefaultView = true;
    //    CameraChange.Manager=0;
    //}

    //public void ChangetoTriView()
    //{
    //    DefaultView = false;
    //    CameraChange.Manager=1;
    //}
}
