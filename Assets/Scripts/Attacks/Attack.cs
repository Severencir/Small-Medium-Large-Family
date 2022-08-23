using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Attack : MonoBehaviour
{
    float fireBallDamage = 10f;
    float fireBallSpeed = 20f;
    float fireBallAoe = 1f;
    float fireBallLifeTime = 2f;
    float fireBallCooldown = 0.5f;
    float fireBallCooldownTimer = 0;

    float lightningRange = 10f;
    float lightningDamage = 20f;
    int lightningBounces = 3;
    float lightningSpeed = 10f;
    float lightningCooldown = 1f;
    float lightningCooldownTimer = 0;
    public LayerMask hitables;
    
    [SerializeField]
    GameObject fireBallPrefab;
    [SerializeField]
    GameObject lightningPrefab;

    void FireBall()
    {
        if (fireBallCooldownTimer <= 0)
        {
            GameObject fireBall = Instantiate(fireBallPrefab, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
            fireBall.GetComponent<Rigidbody>().velocity = transform.forward * fireBallSpeed;
            Fireball comp = fireBall.GetComponent<Fireball>();
            comp.damage = fireBallDamage;
            comp.aoe = fireBallAoe;
            comp.lifeTime = fireBallLifeTime;
            fireBallCooldownTimer = fireBallCooldown;
        }
    }

    void Update()
    {
        if (fireBallCooldownTimer > 0)
        {
            fireBallCooldownTimer -= Time.deltaTime;
        }
        if (lightningCooldownTimer > 0)
        {
            lightningCooldownTimer -= Time.deltaTime;
        }
        if (Inp.inputs.Player.Fire.WasPressedThisFrame())
        {
            FireBall();
        }
        if (Inp.inputs.Player.Fire2.WasPressedThisFrame())
        {
            Lightning();
        }
    }

    void Lightning()
    {
        if (lightningCooldownTimer <= 0)
        {
            GameObject lightning = Instantiate(lightningPrefab, transform.position + new Vector3(0, 0.5f, 0),Quaternion.identity);
            LightningScript ls = lightning.GetComponent<LightningScript>();
            ls.bounces = lightningBounces;
            ls.damage = lightningDamage;
            ls.range = lightningRange;
            ls.speed = lightningSpeed;
            ls.origin = transform.position;
            ls.target = transform.forward * lightningRange;
            ls.lifeTime = lightningRange / lightningSpeed;
            
            lightningCooldownTimer = lightningCooldown;
        }
    }
}
