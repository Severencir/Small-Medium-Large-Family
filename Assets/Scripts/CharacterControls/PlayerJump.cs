using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    Rigidbody rb;
    bool canJump = false;
    public float jumpSpeed = 20f;
    public float extraGravityFactor = 3;
    public float fastDrop = 2f;
    float extraGravity;
    private void Start()
    {
        extraGravity = 3 * 9.81f;
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        float deltaTime = Time.deltaTime;
        if (Mathf.Abs(rb.velocity.y) < 0.000001f)
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }

        bool jumpPress = Inp.inputs.Player.Jump.IsPressed();
        if (jumpPress && canJump)
        {
            rb.velocity = new(rb.velocity.x, jumpSpeed, rb.velocity.z);
        }
        if (rb.velocity.y < fastDrop)
        {
            rb.velocity -= new Vector3(0, extraGravity * deltaTime, 0);
        }
    }
}
