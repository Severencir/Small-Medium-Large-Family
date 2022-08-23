using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticPosition : MonoBehaviour
{
    public static Dictionary<string, Vector3> positions = new();


    private void Update()
    {
        if (!positions.ContainsKey(gameObject.name))
        {
            positions.Add(gameObject.name, transform.position);
        }

        positions[gameObject.name] = transform.position;
    }
}
