using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    float fireBallDamage = 10;
    float fireBallSpeed = 20;
    float fireBallAoe = 0.5f;
    float fireBallLifeTime = 2;

    [SerializeField]
    GameObject fireBallPrefab;

    void FireBall()
    {
        GameObject fireBall = Instantiate(fireBallPrefab, transform.position + new Vector3(0.5f,0.5f,0.5f), transform.rotation);
        fireBall.GetComponent<Rigidbody>().velocity = transform.forward * fireBallSpeed;
        Fireball comp = fireBall.GetComponent<Fireball>();
        comp.damage = fireBallDamage;
        comp.aoe = fireBallAoe;
        comp.lifeTime = fireBallLifeTime;
    }

    void Update()
    {
        if (Inp.inputs.Player.Fire.WasPressedThisFrame())
        {
            FireBall();
        }
    }
}
