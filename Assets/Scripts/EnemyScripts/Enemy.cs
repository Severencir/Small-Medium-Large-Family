using CodeMonkey.HealthSystemCM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{
    HealthSystem healthSystem;
    List<Dot> dots = new List<Dot>();
    Material material;
    float stunTimer = 0;
    public bool isStunned = false;
    public List<GameObject> sprites;
    float damageCoef = 1;
    Unity.Mathematics.Random rng;
    public GameObject sparks;
    float damageResistanceCoef = 1;
    
    private void Start()
    {
        if (HealthSystem.TryGetHealthSystem(gameObject, out HealthSystem healthSystem))
        {
            this.healthSystem = healthSystem;
        }
        material = GetComponent<MeshRenderer>().material;
        rng = new Unity.Mathematics.Random((uint) UnityEngine.Random.Range(1, int.MaxValue - 1));
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;
        if (healthSystem.IsDead())
        {
            GameObject sprite = Instantiate(sprites[rng.NextInt(0,5)]);
            sprite.transform.position = transform.position + transform.up * 1f;
            Destroy(gameObject);
        }
        for (int i = 0; i < dots.Count; i++)
        {
            Dot tDot = dots[i];
            Damage(tDot.damage * deltaTime);
            tDot.time -= deltaTime;
            if (tDot.time <= 0)
            {
                dots.Remove(dots[i]);
            }
            else
            {
                dots[i] = tDot;
            }
        }
        if (stunTimer > 0)
            stunTimer -= deltaTime;
        else
        {
            material.color = Color.white;
            isStunned = false;
        }
        if (dots.Count > 0)
            sparks.SetActive(true);
        else
            sparks.SetActive(false);

    }

    public void ApplyDot(float damage, float time)
    {
        dots.Add(new Dot { damage = damage, time = time });
    }

    public void ApplyStun(float duration)
    {
        material.color = Color.yellow;
        stunTimer = duration;
        isStunned = true;
    }
    struct Dot
    {
        public float damage;
        public float time;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            print("collision");
            SpriteManager.Damage(GetDamage);
        }
    }
    int GetDamage { get { return (int)(damageCoef * (1 + (Time.time - Attack.startTime) * 0.02f)); } }

    public void Damage(float damage)
    {
        healthSystem.Damage(damage / (1 + (Time.time - Attack.startTime) * 0.02f) * damageResistanceCoef);
    }
}
