using System;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class PlayerShooting : MonoBehaviour
{
    public float weaponType = 1;

    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.25f;
    public float range = 100f;
    float timer;

    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    public LineRenderer gunLine;
    public LineRenderer gunLineLeft;
    public LineRenderer gunLineRight;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = .2f;

    public GameObject rifleImage;
    public GameObject shotgunImage;
    public GameObject fullAutoImage;
    GameObject currentIcon;

    int rifleAmmo;
    int shotgunAmmo;
    int fullAutoAmmo;
    int currentAmmo;
    public TextMeshProUGUI ammoText;

    public AudioClip emptyGun;
    public AudioClip hasAmmo;

    private void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponent<ParticleSystem>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();

        rifleImage.SetActive(true);
        currentIcon = rifleImage;

        rifleAmmo = 100;
        shotgunAmmo = 10;
        fullAutoAmmo = 50;
        currentAmmo = rifleAmmo;
        weaponType = 1;

        ResetAmmo();
    }

    void Update()
    {
        SwitchWeapon();

        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets)
        {
            Shoot();
        }

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }

        ammoText.text = "Ammo: " + currentAmmo;
    }

    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
        if (weaponType == 2)
        {
            gunLineLeft.enabled = false;
            gunLineRight.enabled = false;
        }
    }

    //checks to see what weapon the player has, and then if they have ammo before firing. also updates the stored int of the ammo type
    private void Shoot()
    {
        if (weaponType == 1)
        {
            if (currentAmmo > 0)
            {
                timer = 0f;

                gunAudio.clip = hasAmmo;
                gunAudio.Play();
                gunLight.enabled = true;
                gunParticles.Stop();
                gunParticles.Play();

                gunLine.enabled = true;
                gunLine.SetPosition(0, transform.position);

                shootRay.origin = transform.position;
                shootRay.direction = transform.forward;

                if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
                {
                    EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(damagePerShot, shootHit.point);
                    }
                    gunLine.SetPosition(1, shootHit.point);
                    currentAmmo--;
                    PlayerPrefs.SetInt("CurrentRifleAmmo", currentAmmo);
                }
                else
                {
                    gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
                    currentAmmo--;
                    PlayerPrefs.SetInt("CurrentRifleAmmo", currentAmmo);
                }
            }
            else
            {
                gunAudio.clip = emptyGun;
                gunAudio.Play();
            }
        }
        else if (weaponType == 2)
        {
            if (currentAmmo > 0)
            {
                timer = 0f;

                gunAudio.clip = hasAmmo;
                gunAudio.Play();
                gunLight.enabled = true;
                gunParticles.Stop();
                gunParticles.Play();

                gunLine.enabled = true;
                gunLine.SetPosition(0, transform.position);
                gunLineLeft.enabled = true;
                gunLineLeft.SetPosition(0, transform.position);
                gunLineRight.enabled = true;
                gunLineRight.SetPosition(0, transform.position);

                shootRay.origin = transform.position;
                shootRay.direction = transform.forward;

                if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
                {
                    EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(damagePerShot, shootHit.point);
                    }
                    Debug.Log("Shotgun Hits");
                    Vector3 shotgun_Left = new Vector3(shootHit.point.x - 1, shootHit.point.y, shootHit.point.z);
                    Vector3 shotgun_Right = new Vector3(shootHit.point.x + 1, shootHit.point.y, shootHit.point.z);
                    gunLine.SetPosition(1, shootHit.point);
                    gunLineLeft.SetPosition(1, shotgun_Left);
                    gunLineRight.SetPosition(1, shotgun_Right);
                    currentAmmo--;
                    PlayerPrefs.SetInt("CurrentShotgunAmmo", currentAmmo);
                }
                else
                {
                    Debug.Log("Shotgun Misses");
                    Vector3 shootPoint = shootRay.origin + shootRay.direction * range;
                    Vector3 shotgun_Left = new Vector3(shootPoint.x - 1, shootPoint.y, shootPoint.z);
                    Vector3 shotgun_Right = new Vector3(shootPoint.x + 1, shootPoint.y, shootPoint.z);
                    gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
                    gunLineLeft.SetPosition(1, shotgun_Left);
                    gunLineRight.SetPosition(1, shotgun_Right);
                    currentAmmo--;
                    PlayerPrefs.SetInt("CurrentShotgunAmmo", currentAmmo);
                }
            }
            else
            {
                gunAudio.clip = emptyGun;
                gunAudio.Play();
            }
        }
        else if (weaponType == 3)
        {
            if (currentAmmo > 0)
            {
                timer = 0f;

                gunAudio.clip = hasAmmo;
                gunAudio.Play();
                gunLight.enabled = true;
                gunParticles.Stop();
                gunParticles.Play();

                gunLine.enabled = true;
                gunLine.SetPosition(0, transform.position);

                shootRay.origin = transform.position;
                shootRay.direction = transform.forward;

                if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
                {
                    EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(damagePerShot, shootHit.point);
                    }
                    gunLine.SetPosition(1, shootHit.point);
                    currentAmmo--;
                    PlayerPrefs.SetInt("CurrentFullAutoAmmo", currentAmmo);
                }
                else
                {
                    gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
                    currentAmmo--;
                    PlayerPrefs.SetInt("CurrentFullAutoAmmo", currentAmmo);
                }
            }
            else
            {
                gunAudio.clip = emptyGun;
                gunAudio.Play();
            }
        }
    }

    //switches the weapon the player is using via number keys, and changes the weapon stats, ammo type and UI icons.
    //also stores and holds the count for ammo types
    void SwitchWeapon()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            weaponType = 1;
            range = 100f;

            damagePerShot = 20;
            timeBetweenBullets = 0.25f;

            currentIcon.SetActive(false);
            rifleImage.SetActive(true);
            currentIcon = rifleImage;

            if (PlayerPrefs.HasKey("CurrentRifleAmmo"))
            {
                currentAmmo = PlayerPrefs.GetInt("CurrentRifleAmmo");
            }
            else
            {
                currentAmmo = rifleAmmo;
                PlayerPrefs.SetInt("CurrentRifleAmmo", currentAmmo);
            }
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            weaponType = 2;
            range = 50f;

            damagePerShot = 50;
            timeBetweenBullets = 0.50f;

            currentIcon.SetActive(false);
            shotgunImage.SetActive(true);
            currentIcon = shotgunImage;

            if (PlayerPrefs.HasKey("CurrentShotgunAmmo"))
            {
                currentAmmo = PlayerPrefs.GetInt("CurrentShotgunAmmo");
            }
            else
            {
                currentAmmo = shotgunAmmo;
                PlayerPrefs.SetInt("CurrentShotgunAmmo", currentAmmo);
            }
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            weaponType = 3;
            range = 100f;

            damagePerShot = 30;
            timeBetweenBullets = 0.05f;

            currentIcon.SetActive(false);
            fullAutoImage.SetActive(true);
            currentIcon = fullAutoImage;

            if (PlayerPrefs.HasKey("CurrentFullAutoAmmo"))
            {
                currentAmmo = PlayerPrefs.GetInt("CurrentFullAutoAmmo");
            }
            else
            {
                currentAmmo = fullAutoAmmo;
                PlayerPrefs.SetInt("CurrentFullAutoAmmo", currentAmmo);
            }
        }
    }

    //resets the ammo upon every new game to prevent new attempts starting with incorrect ammo
    public void ResetAmmo()
    {
        int resetRifleAmmo = rifleAmmo;
        PlayerPrefs.SetInt("CurrentRifleAmmo", resetRifleAmmo);
        int resetShotgunAmmo = shotgunAmmo;
        PlayerPrefs.SetInt("CurrentShotgunAmmo", resetShotgunAmmo);
        int resetFullAutoAmmo = fullAutoAmmo;
        PlayerPrefs.SetInt("CurrentFullAutoAmmo", resetFullAutoAmmo);
    }

    //adds rifle ammo amount from the ammo pick up
    public void AddRifleAmmo(int amount)
    {
        if (weaponType == 1)
        {
            currentAmmo += amount;
            PlayerPrefs.SetInt("CurrentRifleAmmo", currentAmmo);
            Debug.Log("Rifle Ammo Picked up!");
        }
        else
        {
            rifleAmmo = PlayerPrefs.GetInt("CurrentRifleAmmo");
            int reloadAmmo = rifleAmmo + amount;
            PlayerPrefs.SetInt("CurrentRifleAmmo", reloadAmmo);
        }
    }

    //adds shotgun ammo amount from the ammo pick up
    public void AddShotgunAmmo(int amount)
    {
        if (weaponType == 2)
        {
            currentAmmo += amount;
            PlayerPrefs.SetInt("CurrentShotgunAmmo", currentAmmo);
            Debug.Log("Shotgun Ammo Picked up!");
        }
        else
        {
            shotgunAmmo = PlayerPrefs.GetInt("CurrentShotgunAmmo");
            int reloadAmmo = shotgunAmmo + amount;
            PlayerPrefs.SetInt("CurrentShotgunAmmo", reloadAmmo);
        }
    }

    //adds full-auto ammo amount from the ammo pick up
    public void AddFullAutoAmmo(int amount)
    {
        if (weaponType == 3)
        {
            currentAmmo += amount;
            PlayerPrefs.SetInt("CurrentFullAutoAmmo", currentAmmo);
            Debug.Log("Rifle Ammo Picked up!");
        }
        else
        {
            fullAutoAmmo = PlayerPrefs.GetInt("CurrentFullAutoAmmo");
            int reloadAmmo = fullAutoAmmo + amount;
            PlayerPrefs.SetInt("CurrentFullAutoAmmo", reloadAmmo);
        }
    }
}
