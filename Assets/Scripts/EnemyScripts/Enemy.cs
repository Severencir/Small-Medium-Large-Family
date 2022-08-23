using CodeMonkey.HealthSystemCM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    HealthSystem healthSystem;

    private void Start()
    {
        if (HealthSystem.TryGetHealthSystem(gameObject, out HealthSystem healthSystem))
        {
            this.healthSystem = healthSystem;
        }
    }

    private void Update()
    {
        if (healthSystem.IsDead())
        {
            Destroy(gameObject);
        }
    }
}
