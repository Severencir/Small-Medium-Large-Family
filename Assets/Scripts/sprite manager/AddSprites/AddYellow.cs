using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddYellow : MonoBehaviour
{
    int count = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SpriteManager.yellow.Add(count);
            Destroy(transform.parent.gameObject);
        }
    }
}
