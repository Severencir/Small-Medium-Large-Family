using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    float fireBallDamage = 10f;
    float fireBallSpeed = 20f;
    float fireBallAoe = 1f;
    float fireBallLifeTime = 1f;
    float fireBallCooldown = 0.5f;
    float fireBallCooldownTimer = 0;

    float lightningRange = 3f;
    float lightningDamage = 20f;
    int lightningBounces = 3;
    float lightningSpeed = 25f;
    float lightningCooldown = 1f;
    float lightningCooldownTimer = 0;
    public LayerMask hitables;

    float auraAoe = 3;
    float auraDamage = 10f;
    float auraDuration = 2f;
    float auraCooldown = 1f;
    float auraCooldownTimer = 0;

    int blockadeQuantity = 3;
    float blockadeDuration = 5;
    float blockadeAoe = 1;
    float blockadeCooldown = 10;
    float blockadeCooldownTimer = 0;

    float stunDuration = 1;
    int stunQuantity = 3;
    float stunSpeed = 10f;
    float stunLifetime = 4f;
    float stunCooldown = 5f;
    float stunCooldownTimer = 0;

    [SerializeField]
    GameObject fireBallPrefab;
    [SerializeField]
    GameObject lightningPrefab;
    [SerializeField]
    GameObject blockadePrefab;
    [SerializeField]
    GameObject stunPrefab;

    static bool isAttacking = false;
    float attackTimer = 0;
    float attackCooldown = 1f;
    public static float graceCountdown = 0;
    public static float grace = 10;
    public static float startTime;
    bool timeset = false;
    public static bool IsAttacking { get { return isAttacking; } }
    private void Start()
    {
        startTime = Time.time;
    }
    void Update()
    {

        float deltaTime = Time.deltaTime;
        if (graceCountdown > 0)
            graceCountdown -= deltaTime;
        else if (!timeset)
        {
            startTime = Time.time;
            timeset = true;
        }

        if (fireBallCooldownTimer > 0)
            fireBallCooldownTimer -= deltaTime;
        if (lightningCooldownTimer > 0)
            lightningCooldownTimer -= deltaTime;
        if (auraCooldownTimer > 0)
            auraCooldownTimer -= deltaTime;
        if (blockadeCooldownTimer > 0)
            blockadeCooldownTimer -= deltaTime;
        if (stunCooldownTimer > 0)
            stunCooldownTimer -= deltaTime;
        if (Inp.inputs.Player.Fire.IsPressed())
            FireBall();
        if (Inp.inputs.Player.Fire2.IsPressed())
            Lightning();
        if (Inp.inputs.Player.Ability1.IsPressed())
            Aura();
        if (Inp.inputs.Player.Ability2.IsPressed())
            Blockade();
        if (Inp.inputs.Player.Ability3.IsPressed())
            Stun();

        if (attackTimer > 0)
        {
            isAttacking = true;
            attackTimer -= deltaTime;
        }
        else isAttacking = false;

        UpdateVariables();
    }

    void Lightning()
    {
        if (lightningCooldownTimer <= 0)
        {
            if (!isAttacking) attackTimer = attackCooldown;
            GameObject lightning = Instantiate(lightningPrefab, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
            LightningScript ls = lightning.GetComponent<LightningScript>();
            ls.bounces = lightningBounces;
            ls.damage = lightningDamage;
            ls.range = lightningRange;
            ls.speed = lightningSpeed;
            ls.origin = transform.position + transform.forward;
            ls.target = transform.position + transform.forward * lightningRange * 2 + transform.up * 0.5f;
            ls.lifeTime = lightningRange / lightningSpeed;

            AudioManager.Play("Lightning");
            lightningCooldownTimer = lightningCooldown;
            
        }
    }
    
    void FireBall()
    {
        if (fireBallCooldownTimer <= 0)
        {
            if (!isAttacking) attackTimer = attackCooldown;
            GameObject fireBall = Instantiate(fireBallPrefab, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
            fireBall.GetComponent<Rigidbody>().velocity = transform.forward * fireBallSpeed;
            Fireball comp = fireBall.GetComponent<Fireball>();
            comp.damage = fireBallDamage;
            comp.aoe = fireBallAoe;
            comp.lifeTime = fireBallLifeTime;
            AudioManager.Play("Fireball");

            fireBallCooldownTimer = fireBallCooldown;
        }
    }

    void Aura()
    {
        if (auraCooldownTimer <= 0)
        {
            if (!isAttacking) attackTimer = attackCooldown;
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, auraAoe, transform.forward, 0, hitables);
            foreach (RaycastHit hit in hits)
            {
                hit.collider.GetComponent<Enemy>().ApplyDot(auraDamage, auraDuration);
            }

            AudioManager.Play("Aura");
            auraCooldownTimer = auraCooldown;
        }
    }

    void Blockade()
    {
        if (blockadeCooldownTimer <= 0)
        {
            for (int i = 0; i < blockadeQuantity; i++)
            {
                float scale = Mathf.Sqrt(blockadeAoe);
                GameObject blockade = Instantiate(blockadePrefab, transform.position + new Vector3(UnityEngine.Random.Range(-2 + blockadeAoe, 2 + blockadeAoe), 
                    0.5f, UnityEngine.Random.Range(-2 + blockadeAoe, 2 + blockadeAoe)), transform.rotation);
                blockade.transform.localScale = new Vector3(scale * 0.5f, 0.25f, scale * 0.5f);

                Destroy(blockade, blockadeDuration);
            }
            AudioManager.Play("Blockade");
            blockadeCooldownTimer = blockadeCooldown;
        }
    }

    void Stun()
    {
        if (stunCooldownTimer <= 0 )
        {
            for (int i = 0; i < stunQuantity; i++)
            {
                GameObject stun = Instantiate(stunPrefab, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
                float offset = Mathf.Log(stunQuantity, 2) * 0.1f;
                stun.GetComponent<Rigidbody>().velocity = (transform.forward + transform.right * UnityEngine.Random.Range(-offset, offset) + 
                    transform.up * UnityEngine.Random.Range(-offset, offset)).normalized * stunSpeed;
                stun.GetComponent<StunScript>().duration = stunDuration;
                Destroy(stun, stunLifetime);
            }
            AudioManager.Play("Stun");

            stunCooldownTimer = stunCooldown;
        }
    }

    void UpdateVariables()
    {
        float redMod = Mathf.Sqrt(SpriteManager.red.active + 1);
        float yellowMod = Mathf.Sqrt(SpriteManager.yellow.active + 1);
        float blueMod = Mathf.Sqrt(SpriteManager.blue.active + 1);
        float greenMod = Mathf.Sqrt(SpriteManager.green.active + 1);
        float purpleMod = Mathf.Sqrt(SpriteManager.purple.active + 1);

        fireBallDamage = 10 + redMod * 2;
        fireBallSpeed = 15 + blueMod;
        fireBallAoe = 1 + greenMod * 0.25f;
        fireBallCooldown = (1 - SpriteManager.red.active / (SpriteManager.red.active + 25f)) * 2;

        lightningRange = 3 + blueMod * 2;
        lightningDamage = 10 + redMod;
        lightningBounces = Mathf.RoundToInt(1 + purpleMod);
        lightningCooldown = (1 - SpriteManager.blue.active / (SpriteManager.blue.active + 25f)) * 2;

        auraAoe = 2 + greenMod;
        auraDamage = 10 + redMod;
        auraDuration = 0.5f + yellowMod * 0.5f;
        auraCooldown = (1 - SpriteManager.green.active / (SpriteManager.green.active + 25f)) * 4;

        blockadeAoe = 1 + greenMod * 0.1f;
        blockadeDuration = 3 + yellowMod * 0.5f;
        blockadeQuantity = Mathf.RoundToInt(1 + purpleMod);
        blockadeCooldown = (1 - SpriteManager.yellow.active / (SpriteManager.yellow.active + 25f)) * 10;

        stunDuration = 1 + yellowMod * 0.25f;
        stunQuantity = Mathf.RoundToInt(1 + purpleMod);
        stunSpeed = 15 + blueMod;
        stunCooldown = (1 - SpriteManager.purple.active / (SpriteManager.purple.active + 25f)) * 3;
        
    }
}
