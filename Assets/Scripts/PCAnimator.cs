using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCAnimator : VariableGrabber
{
    public Animator animator;

    protected override void OnUpdate()
    {
        // This handles the jump animation
        Vector3 tempSpeed = new(PlayerVelocity.x, 0, PlayerVelocity.z);
        animator.SetFloat("Speed", tempSpeed.magnitude);

        //This handles the attacking animation
        animator.SetBool("IsAttacking", IsAttacking);

        //This handles the death animation
        animator.SetBool("IsDead", IsDead);
        
        //This handles the jumping animation
        animator.SetBool("IsJumping", IsJumping);

        //This handles the death animation
        animator.SetBool("IsDead", IsDead);

        //This is where the character is dead animation should go
    }



}
