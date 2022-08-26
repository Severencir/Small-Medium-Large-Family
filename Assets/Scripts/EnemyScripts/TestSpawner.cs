using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    public GameObject Spawnee;
    float timer = 0;
    float cooldown = 30;
    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            GameObject spawn = Instantiate(Spawnee);
            spawn.transform.position = transform.position;
            timer = cooldown;
        }
        else
            timer -= Time.deltaTime;
    }
}
