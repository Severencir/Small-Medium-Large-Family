using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPurple : MonoBehaviour
{
    int count = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SpriteManager.purple.Add(count);
            Destroy(transform.parent.gameObject);
        }
    }
}
