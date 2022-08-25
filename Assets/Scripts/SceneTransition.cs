using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public GameObject helpPanel;

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
        Time.timeScale = 1f;
    }

    public void LoadLv1()
    {
        SceneManager.LoadScene("Lv1Scene");
        Time.timeScale = 1f;
    }

    public void Help()
    {
        helpPanel.SetActive(true);
    }

    public void CloseHelp()
    {
        helpPanel.SetActive(false);        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}