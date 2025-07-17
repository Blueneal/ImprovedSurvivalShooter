using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;
    public GameObject setPlayer; 

    void Start()
    {
        setPlayer = GameObject.Find("Player");
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
