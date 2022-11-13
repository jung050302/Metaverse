using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour
{
    public GameObject fadeOut;
    public GameObject setting;
    
    public void MainMenuButton()
    {
        fadeOut.GetComponent<FadeOut>()._scenename = "Lobby" ;
        fadeOut.SetActive(true);
    }

    public void OptionBtn()
    {
        if (setting.activeSelf == true)
        {
            setting.SetActive(false);
        }
        else
        {
            setting.SetActive(true);
        }
    }
}
