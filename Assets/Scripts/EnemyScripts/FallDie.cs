using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDie : MonoBehaviour
{
    void Update()
    {
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
        
    }
}
