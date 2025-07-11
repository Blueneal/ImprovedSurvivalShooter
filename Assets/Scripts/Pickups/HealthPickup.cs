using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int heal;

    GameObject player;
    PlayerHealth playerHealth;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    //Checks to see if the player walks into the pick up, if so adds to the current weapon ammo
    public void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            playerHealth.HealPlayer(heal);

            Destroy(gameObject);
        }
    }
}
