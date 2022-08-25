using System.Collections;
using System.Collections.Generic;
using Unity.Transforms;
using UnityEngine;

public class SpriteAnimation : MonoBehaviour
{
    private void Update()
    {
        transform.position = transform.parent.position + new Vector3(0, Mathf.Sin(Time.time * 2f) * 0.5f, 0);
    }
}
