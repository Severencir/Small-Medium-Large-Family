using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    public GameObject Spawnee;
    float timer = 10;
    float cooldown = 30;
    // Update is called once per frame
    void Update()
    {
        if (Attack.graceCountdown <= 0)
        {
            if (timer <= 0)
            {
                GameObject spawn = Instantiate(Spawnee);
                spawn.transform.position = transform.position;
                timer = cooldown;
            }
            else
                timer -= Time.deltaTime;
            cooldown = 20 * (Mathf.Pow(0.99f, 1 + (Time.time - Attack.startTime) * 0.05f));
        }
    }
}
