using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip; // Enemy death sound

    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;

    public GameObject rifleAmmoPickup;
    public GameObject shotgunAmmoPickup;
    public GameObject fullAutoPickup;

    public GameObject healthPickup;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        currentHealth = startingHealth;
    }

    void Update()
    {
        if (isSinking)
        {
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead) return;

        enemyAudio.Play();

        currentHealth -= amount;
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;
        anim.SetTrigger("Dead");
        enemyAudio.clip = deathClip;
        enemyAudio.Play();

        float dropChance = Random.Range(1, 10);//creats a 40% of ammo dropping from a killed enemy
        if (dropChance > 6)
        {
            int ammoType = Random.Range(0, 2);
            if (ammoType == 0)
            {
                Instantiate(rifleAmmoPickup, transform.position, transform.rotation);
            }
            else if (ammoType == 1)
            {
                Instantiate(shotgunAmmoPickup, transform.position, transform.rotation);
            }
            else if (ammoType == 2)
            {
                Instantiate(fullAutoPickup, transform.position, transform.rotation);
            }
        }
        else if (dropChance <= 2) //20% drop chance of receiving a health pick up
        {
            Instantiate(healthPickup, transform.position, transform.rotation);
        }
    }

    public void StartSinking()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        ScoreManager.Instance.ShowScore();
        Destroy(gameObject, 2f);
    }
}
