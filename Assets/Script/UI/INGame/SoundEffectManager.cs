using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager Instance;

    [SerializeField] private AudioSource soundObject;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    //播放音效檔(檔案，播放位置，音量)
    public void PlaySound(AudioClip AudioClip, Transform spawnTransform, float value)
    {
        //生成放音效的空物件
        AudioSource audioSource = Instantiate(soundObject,spawnTransform.position,Quaternion.identity);

        //抓取音效檔中的元件(檔案&音量)
        audioSource.clip = AudioClip;
        audioSource.volume=value;

        audioSource.Play();

        //音效長度
        float clipLength =audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }
}
