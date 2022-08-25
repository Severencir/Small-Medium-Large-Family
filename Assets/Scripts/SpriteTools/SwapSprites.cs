using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapSprites : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bool success = SpriteManager.red.Remove(5);
            if (success)
            {
                SpriteManager.blue.Add(5);
            }
        }
    }
}
