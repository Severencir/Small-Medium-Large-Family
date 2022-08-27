using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    Rigidbody rb;
    bool canJump = false;
    bool isJumping = false;
    public bool IsJumping { get { return isJumping; } }
    [SerializeField]
    float jumpSpeed = 20f;
    [SerializeField]
    float extraGravityFactor = 3;
    [SerializeField]
    float fastDrop = 2f;
    float extraGravity;
    private void Start()
    {
        extraGravity = extraGravityFactor * 9.81f;
        rb = GetComponent<Rigidbody>();
        isJumping = false;
    }
    private void Update()
    {
        float deltaTime = Time.deltaTime;
        if (Mathf.Abs(rb.velocity.y) < 0.000001f)
        {
            if (isJumping) AudioManager.Play("LandingSound");
            canJump = true;
            isJumping = false;
        }
        else
        {
            canJump = false;
        }

        bool jumpPress = Inp.inputs.Player.Jump.IsPressed();

        if (!SpriteManager.IsDead)
            if (jumpPress && canJump)
        {
            AudioManager.Play("JumpingSound");
            rb.velocity = new(rb.velocity.x, jumpSpeed, rb.velocity.z);
            isJumping = true;
        }
        if (rb.velocity.y < fastDrop)
        {
            rb.velocity -= new Vector3(0, extraGravity * deltaTime, 0);
        }
    }
}
