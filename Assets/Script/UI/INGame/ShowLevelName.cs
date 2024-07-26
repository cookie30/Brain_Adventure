using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowLevelName : MonoBehaviour
{
    public TextMeshProUGUI SceneName;

    private void Start()
    {
        SceneName=GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        SceneName.text = SceneManager.GetActiveScene().name;
    }
}
