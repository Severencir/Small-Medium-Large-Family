using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGameover : MonoBehaviour
{
    public GameObject gameOver;
    public PauseMenu pauseMenu;
    int startingSprites = 2;
    bool didDamage = false;
    private void Start()
    {
        pauseMenu.Resume();
        SpriteManager.red.Add(startingSprites);
        SpriteManager.blue.Add(startingSprites);
        SpriteManager.green.Add(startingSprites);
        SpriteManager.purple.Add(startingSprites);
        SpriteManager.yellow.Add(startingSprites);
    }
    void Update()
    {
        if (SpriteManager.IsDead)
        {
            if (!didDamage)
            {
                SpriteManager.Damage(SpriteManager.spriteSum);
                didDamage = true;
                StartCoroutine(DeathTimer());
            }
        }
    }
    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(5f);
        pauseMenu.Pause();
        gameOver.SetActive(true);
    }
    
}
