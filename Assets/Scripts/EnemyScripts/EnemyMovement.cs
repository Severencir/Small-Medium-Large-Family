using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Vector3 playerPos;
    Rigidbody rb;
    float speed = 1;
    private void Start()
    {
        playerPos = StaticPosition.positions["Player"];
        rb = GetComponent<Rigidbody>(); 
    }

    private void Update()
    {
        Vector3 direction = (new Vector3(playerPos.x, 0, playerPos.z) - new Vector3(transform.position.x, 0 , transform.position.z)).normalized;
        rb.velocity = new Vector3(direction.x * speed, rb.velocity.y, direction.z * speed);
        transform.LookAt(new Vector3(playerPos.x, transform.position.y, playerPos.z));
    }
}
