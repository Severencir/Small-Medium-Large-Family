using CodeMonkey.HealthSystemCM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideBar : MonoBehaviour
{
    public Canvas canvas;
    HealthSystem healthSystem;

    private void Start()
    {
        if (HealthSystem.TryGetHealthSystem(gameObject, out HealthSystem healthSystem)){
            this.healthSystem = healthSystem;
        }
    }

    private void Update()
    {
        if (healthSystem.GetHealth() >= healthSystem.GetHealthMax())
        {
            canvas.enabled = false;
        }
        else
        {
            canvas.enabled = true;
        }
    }
}
