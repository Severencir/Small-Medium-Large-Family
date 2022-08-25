using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoCollide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(6, 10);
        Physics.IgnoreLayerCollision(7, 10);
        Physics.IgnoreLayerCollision(10, 10);

    }

}
