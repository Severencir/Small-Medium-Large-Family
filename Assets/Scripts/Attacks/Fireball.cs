using CodeMonkey.HealthSystemCM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float damage;
    public float aoe;
    public float lifeTime;
    public LayerMask hitables;

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Explode();
        }
    }

    void Explode()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, aoe, transform.forward, 0, hitables);

        foreach (RaycastHit hit in hits)
        {
            hit.collider.GetComponent<HealthSystemComponent>().GetHealthSystem().Damage(damage);
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Barrier"))
        {
            Explode();
        }
    }
}
