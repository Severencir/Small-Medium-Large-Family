using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapSprites : MonoBehaviour
{
    public GameObject menu;
    public PauseMenu pauseMenu;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pauseMenu.Pause();
            menu.SetActive(true);
        }
    }
}
