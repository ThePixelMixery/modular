using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
Button exitButton;

  void Start()
    {
    exitButton = GetComponent<Button>();
    exitButton.onClick.AddListener(ExitFunction);
    }

    void ExitFunction()
    {
    Application.Quit();
    }
}