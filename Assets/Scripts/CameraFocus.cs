using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    public GameObject focus;
    void LateUpdate()
    {
        //focus on camera focus
        transform.LookAt(focus.transform.position);
    }
}
