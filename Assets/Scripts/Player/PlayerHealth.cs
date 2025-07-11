using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float flashSpeed = 5f;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public Color flashColor = new Color(1f, 0f, 0f, .1f);

    private bool isDead;
    private bool damaged;
    private Animator anim;
    private AudioSource playerAudio;
    private PlayerMovement playerMovement;
    private PlayerShooting playerShooting;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();
        currentHealth = startingHealth;
    }

    private void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;

        if (currentHealth > startingHealth)
        {
            currentHealth = startingHealth;
        }
    }

    public void TakeDamage(int amount)
    {
        damaged = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        playerAudio.Play();
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    public void HealPlayer(int amount)
    {
        if (currentHealth >= startingHealth)
        {
            currentHealth = startingHealth;
            healthSlider.value = currentHealth;
        }
        else
        {
            currentHealth += amount;
            healthSlider.value = currentHealth;
        }
    }

    private void Death()
    {
        isDead = true;
        playerShooting.DisableEffects();
        anim.SetTrigger("Die");
        playerAudio.clip = deathClip;
        playerAudio.Play(); // plays "death"
        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }
}
