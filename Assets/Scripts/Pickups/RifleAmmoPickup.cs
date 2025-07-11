using UnityEngine;

public class RifleAmmoPickup : MonoBehaviour
{
    public int ammoPickup;

    GameObject player;
    PlayerShooting playerShooting;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerShooting = player.GetComponentInChildren<PlayerShooting>();
    }

    //Checks to see if the player walks into the pick up, if so adds to the current weapon ammo
    public void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            playerShooting.AddRifleAmmo(ammoPickup);

            Destroy(gameObject);
        }
    }
}
