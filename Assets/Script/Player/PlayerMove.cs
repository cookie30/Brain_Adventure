using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public StarterAssetsInputs StarterAssetsInputs;
    public AudioSource AudioSource;

    [Header("�P�w�}�B�n")]
    public AudioClip Road, Grass, Run; //����
    public RaycastHit WalkTypeHit; //�����a���g�u
    public Transform HitStart; //�g�u�_�I
    public float RayLength; //�g�u����
    public LayerMask layerMask; //�a���X��(���g�u�L�����a���H�~���F��)

    private void Start()
    {
        StarterAssetsInputs = GameObject.Find("Player").GetComponent<StarterAssetsInputs>();
        AudioSource=GetComponent<AudioSource>();
    }

    //�������a���}(�g�u)�I�쪺�a������/�O�_���U�Ĩ���Ӽ����������
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
        //���񭵮�
        AudioSource.PlayOneShot(audio);
    }
}
