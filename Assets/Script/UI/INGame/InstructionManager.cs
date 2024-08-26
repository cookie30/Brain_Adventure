using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionManager : MonoBehaviour
{
    public GameObject instructionImage; // �ާ@�����Ϥ�
    public GameObject closeButton; // �������s

    // Start is called before the first frame update
    void Start()
    {

    }

    public void ShowInstruction()
    {
        // ��ܹϤ�
        instructionImage.SetActive(true);
        closeButton.SetActive(true);
    }

    public void CloseInstruction()
    {
        // ���ùϤ�
        instructionImage.SetActive(false);
        closeButton.SetActive(false);
    }
}
