using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    Rigidbody rb;
    Vector3 velocity;
    public float smoothing = 40f;
    Vector3 targetVelocity;
    float timer = 0;
    Vector3 lastTargetVelocity;
    Vector3 startVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        lastTargetVelocity = rb.velocity;
        timer = smoothing;
    }
    private void Update()
    {
        bool escaped = Inp.inputs.MouseCapture.Escape.WasPressedThisFrame();
        bool enabled = Inp.inputs.Player.enabled;
        if (escaped)
            if (enabled)
            {
                Inp.inputs.Player.Disable();
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Inp.inputs.Player.Enable();
                Cursor.lockState = CursorLockMode.Locked;
            }

        Vector2 wasd = Inp.inputs.Player.Move.ReadValue<Vector2>();

        targetVelocity = (transform.forward * wasd.y + transform.right * wasd.x).normalized * moveSpeed;

        if (lastTargetVelocity != targetVelocity)
        {
            timer = 0;
            startVelocity = velocity;
        }

        if (timer < smoothing)
        {
            timer += Time.deltaTime;
        }

        velocity = Vector3.Lerp(startVelocity, targetVelocity, timer / smoothing);

        rb.velocity = new(velocity.x, rb.velocity.y, velocity.z);
        lastTargetVelocity = targetVelocity;
    }
}
