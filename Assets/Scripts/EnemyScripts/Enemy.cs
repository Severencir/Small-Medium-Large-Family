using CodeMonkey.HealthSystemCM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    HealthSystem healthSystem;
    List<Dot> dots = new List<Dot>();
    Material material;
    float stunTimer = 0;
    public bool isStunned = false;
    public List<GameObject> sprites;
    int damage = 1;

    private void Start()
    {
        if (HealthSystem.TryGetHealthSystem(gameObject, out HealthSystem healthSystem))
        {
            this.healthSystem = healthSystem;
        }
        material = GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        if (healthSystem.IsDead())
        {
            GameObject sprite = Instantiate(sprites[Random.Range(0, sprites.Count - 1)]);
            sprite.transform.position = transform.position + transform.up * 1f;
            Destroy(gameObject);
        }
        for (int i = 0; i < dots.Count; i++)
        {
            Dot tDot = dots[i];
            healthSystem.Damage(tDot.damage * Time.deltaTime);
            tDot.time -= Time.deltaTime;
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
            stunTimer -= Time.deltaTime;
        else
        {
            material.color = Color.white;
            isStunned = false;
        }
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
            SpriteManager.Damage(damage);
        }
    }
}
