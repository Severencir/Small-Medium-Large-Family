using CodeMonkey.HealthSystemCM;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LightningScript : MonoBehaviour
{
    public int bounces;
    public float range;
    public float damage;
    public float speed;
    public float lifeTime;
    public float remainingTime;
    Rigidbody RB;
    HashSet<GameObject> hits;
    public Vector3 origin;
    public Vector3 target;

    public GameObject nearest;
    private void Start()
    {
        RB = GetComponent<Rigidbody>();
        hits = new HashSet<GameObject>();
        remainingTime = lifeTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Barrier"))
            //Destroy(gameObject);
        if (other.CompareTag("Enemy"))
        {
            Jump(other);
        }
    }
    private void Update()
    {
        if (remainingTime > 0)
            remainingTime -= Time.deltaTime;
        if (remainingTime <= 0)
            Destroy(gameObject);
        transform.position = Vector3.Lerp(target, origin, remainingTime/lifeTime);
    }
    void Jump(Collider other)
    {
        other.gameObject.GetComponent<HealthSystemComponent>().GetHealthSystem().Damage(damage);
        hits.Add(other.gameObject);
        nearest = FindClosestEnemy();
        if (bounces <= 0 || nearest == null)
            Destroy(gameObject);
        else
        {
            origin = target;
            target = nearest.transform.position.normalized * range;
        }

        remainingTime = lifeTime;
        bounces--;
    }
    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = range;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            if (hits.Contains(go))
                continue;
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
