using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeScript: MonoBehaviour 
{
    public int tradeFrom = -1;
    public int tradeTo = -1;
    public int tradeAmount = 0;
    public GameObject menu;
    public PauseMenu pauseMenu;

    public void set(int input)
    {
        if (tradeFrom >= 0)
        {
            tradeTo = input;
            swap();
        }
        else
        {
            tradeFrom = input;
        }
        tradeAmount = SpriteManager.spriteMin;
    }

    public void swap()
    {
        if (tradeFrom > 4 || tradeFrom < 0 || tradeTo > 4 || tradeTo < 0)
            return;
        bool success = false;
        if (tradeFrom == 0)
            success = SpriteManager.red.Remove(tradeAmount);
        if (tradeFrom == 1)
            success = SpriteManager.blue.Remove(tradeAmount);
        if (tradeFrom == 2)
            success = SpriteManager.yellow.Remove(tradeAmount);
        if (tradeFrom == 3)
            success = SpriteManager.green.Remove(tradeAmount);
        if (tradeFrom == 4)
            success = SpriteManager.purple.Remove(tradeAmount);

        if (success)
        {
            if (tradeTo == 0)
                SpriteManager.red.Add(tradeAmount);
            if (tradeTo == 1)
                SpriteManager.blue.Add(tradeAmount);
            if (tradeTo == 2)
                SpriteManager.yellow.Add(tradeAmount);
            if (tradeTo == 3)
                SpriteManager.green.Add(tradeAmount);
            if (tradeTo == 4)
                SpriteManager.purple.Add(tradeAmount);
        }

        tradeFrom = -1;
        tradeTo = -1;
        menu.SetActive(false);
        pauseMenu.Resume();
    }
    public void Reset()
    {
        tradeFrom = -1;
        tradeTo = -1;
        menu.SetActive(false);
    }
}
