using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy;
    public float range;
    public float enemyTimer;
    public float currentTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTimer = enemyTimer;
    }

    // Update is called once per frame
    private void Update()
    {
        currentTimer -= Time.deltaTime;    //difference between this and Time.fixedDeltaTime
        if (currentTimer <= 0f)
        {
            SpawnEnemy();
            Debug.Log("EnemySpawned");
            currentTimer = enemyTimer;
          
        }
    }

    void SpawnEnemy()
    {
        Vector3 position;

        position = Random.insideUnitSphere * range;
        position.z = 0f;

        Instantiate(enemy, position, Quaternion.identity);
    }

}