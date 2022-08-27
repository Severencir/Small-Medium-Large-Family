using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Vector3 playerPos;
    Rigidbody rb;
    float speed = 1.5f;
    Enemy enemy;
    private void Start()
    {
        enemy = GetComponent<Enemy>();
        playerPos = StaticPosition.positions["Player"];
        rb = GetComponent<Rigidbody>();
        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
    }

    private void Update()
    {
        playerPos = StaticPosition.positions["Player"];
        Vector3 direction = (new Vector3(playerPos.x, 0, playerPos.z) - new Vector3(transform.position.x, 0 , transform.position.z)).normalized;
        if (enemy.isStunned)
            rb.velocity = new();
        else
            rb.velocity = new Vector3(direction.x * speed, 0, direction.z * speed);
        transform.LookAt(new Vector3(playerPos.x, transform.position.y, playerPos.z));
    }
}
