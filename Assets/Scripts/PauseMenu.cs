using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject swapUI;

    private void Update()
    {
        if (Inp.inputs.UI.Pause.WasPressedThisFrame())
        {
            if (GameIsPaused)
            {
                AudioManager.Play("UnPauseSound");
                Resume();
            }
            else
            {
                AudioManager.Play("PauseSound");
                pauseMenuUI.SetActive(true);
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        swapUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Inp.inputs = new Inputs();
        Inp.inputs.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
        Inp.PlayerDisable();
        Cursor.lockState = CursorLockMode.None;
    }
}
