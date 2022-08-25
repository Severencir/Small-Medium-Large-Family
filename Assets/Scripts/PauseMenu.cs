using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    private void Update()
    {
        if (Inp.inputs.UI.Pause.WasPressedThisFrame())
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        AudioManager.Play("UnPauseSound");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Inp.inputs = new Inputs();
        Inp.inputs.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Pause()
    {
        AudioManager.Play("PauseSound");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Inp.PlayerDisable();
        Cursor.lockState = CursorLockMode.None;
    }
}
