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

    //���񭵮���(�ɮסA�����m�A���q)
    public void PlaySound(AudioClip AudioClip, Transform spawnTransform, float value)
    {
        //�ͦ��񭵮Ī��Ū���
        AudioSource audioSource = Instantiate(soundObject,spawnTransform.position,Quaternion.identity);

        //��������ɤ�������(�ɮ�&���q)
        audioSource.clip = AudioClip;
        audioSource.volume=value;

        audioSource.Play();

        //���Ī���
        float clipLength =audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }
}
