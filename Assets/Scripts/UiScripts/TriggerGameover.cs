using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGameover : MonoBehaviour
{
    public GameObject gameOver;
    public PauseMenu pauseMenu;
    int startingSprites = 2;
    private void Start()
    {
        pauseMenu.Resume();
        SpriteManager.red.Add(2);
        SpriteManager.blue.Add(2);
        SpriteManager.green.Add(2);
        SpriteManager.purple.Add(2);
        SpriteManager.yellow.Add(2);
    }
    void Update()
    {
        if (SpriteManager.IsDead)
        {
            pauseMenu.Pause();
            gameOver.SetActive(true);
        }
    }
    
}
