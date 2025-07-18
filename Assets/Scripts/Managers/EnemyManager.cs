using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public static GameObject player;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        EnemyMovement.playerPos = player.transform;
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn()
    {
        if (playerHealth.currentHealth <= 0)
        {
            return;
        }

        int SpawnPointIndex = Random.Range (0, spawnPoints.Length);

        Instantiate(enemy, spawnPoints[SpawnPointIndex].position, spawnPoints[SpawnPointIndex].rotation);
    }
}
