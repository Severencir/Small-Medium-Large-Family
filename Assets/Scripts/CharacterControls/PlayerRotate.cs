using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    private void LateUpdate()
    {
        //get horizontal mousedelta, rotate player
        Vector2 look = Inp.inputs.Player.Look.ReadValue<Vector2>();
        look /= Screen.width;
        Vector3 rotateValue = new(0, look.x, 0);

        if (!SpriteManager.IsDead)
            transform.Rotate(rotateValue * CameraController.rotateSpeed * Time.deltaTime);
    }
}
