using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public GameObject camera1;
    public GameObject camera2;
    public int Manager;

    public void ManageCamera()
    {
        if (Manager == 0 && SettingMenu.DefaultView == true)
        {
            Cam1();
            Manager = 1;
        }
        else
        {
            Cam2();
            Manager = 0;
        }
    }

    void Cam1()
    {
        camera1.SetActive(true);
        camera2.SetActive(false);
    }

    void Cam2()
    {
        camera2.SetActive(true);
        camera1.SetActive(false);
    }
}
