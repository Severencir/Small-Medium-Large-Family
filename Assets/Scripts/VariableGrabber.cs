using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableGrabber : MonoBehaviour
{
    bool wasDamagedThisFrameReset = false;
    GameObject player;
    Rigidbody playerRigidBody;
    PlayerJump playerJump;

    Vector3 playerPosition;
    Vector3 playerVelocity;
    bool isJumping = false;
    bool isAttacking = false;
    bool isDead = false;
    bool wasDamagedThisFrame = false;

    public Vector3 PlayerPosition { get { return playerPosition; } }
    public Vector3 PlayerVelocity { get { return playerVelocity; } }
    public bool IsJumping { get { return isJumping; } }
    public bool IsAttacking { get { return isAttacking; } }
    public bool IsDead { get { return isDead; } }
    public bool WasDamagedThisFrame { get { return wasDamagedThisFrame; } }


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRigidBody = player.GetComponent<Rigidbody>();
        playerJump = player.GetComponent<PlayerJump>();
        playerPosition = player.transform.position;
        playerVelocity = playerRigidBody.velocity;
        OnStart();
    }

    private void Update()
    {
        playerPosition = transform.position;
        playerVelocity = playerRigidBody.velocity;
        isJumping = playerJump.IsJumping;
        isAttacking = Attack.IsAttacking;
        isDead = SpriteManager.IsDead;
        wasDamagedThisFrame = SpriteManager.wasDamaged;
        SpriteManager.wasDamaged = false;
        OnUpdate();
    }

    protected virtual void OnUpdate()
    {

    }
    protected virtual void OnStart()
    {

    }
}
