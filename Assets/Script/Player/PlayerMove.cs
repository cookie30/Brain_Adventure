using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public StarterAssetsInputs StarterAssetsInputs;
    public AudioSource AudioSource;

    [Header("判定腳步聲")]
    public AudioClip Road, Grass, Run; //音檔
    public RaycastHit WalkTypeHit; //偵測地面射線
    public Transform HitStart; //射線起點
    public float RayLength; //射線長度
    public LayerMask layerMask; //地面蒙版(讓射線無視除地面以外的東西)

    private void Start()
    {
        StarterAssetsInputs = GameObject.Find("Player").GetComponent<StarterAssetsInputs>();
        AudioSource=GetComponent<AudioSource>();
    }

    //偵測玩家的腳(射線)碰到的地面材質/是否按下衝刺鍵來撥放對應音效
    public void FootStep()
    {
        if (Physics.Raycast(HitStart.position, HitStart.transform.up * -1,
            out WalkTypeHit, RayLength, layerMask))
        {
            if (WalkTypeHit.collider.CompareTag("Road"))
            {
                //print(WalkTypeHit.collider.name);
                PlayFootstepSound(Road);
            }
            else if (WalkTypeHit.collider.CompareTag("Garss"))
            {
                PlayFootstepSound(Grass);
            }
            else if (StarterAssetsInputs.sprint)
            {
                PlayFootstepSound(Run);
            }
        }
    }

    void PlayFootstepSound(AudioClip audio)
    {
        //播放音效
        AudioSource.PlayOneShot(audio);
    }
}
