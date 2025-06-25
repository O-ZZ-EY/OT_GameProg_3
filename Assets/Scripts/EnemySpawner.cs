using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject myEnemy;
    public float range;
    public float TimerInterval;
    public float currentTimer;

    GameManager myMgr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myMgr = GameManager.instance;
        currentTimer = TimerInterval;
    }

    // Update is called once per frame
    private void Update()
    {
        currentTimer -= Time.deltaTime;    //difference between this and Time.fixedDeltaTime
        if (currentTimer <= 0f)
        {
            Spawn();
            currentTimer = TimerInterval;

        }
    }

    void Spawn()
    {
        //Vector3 position;

        //position = Random.insideUnitSphere * range;
        //position.z = 0f;

        ////Instantiate(reaper, position, Quaternion.identity);
        Debug.Log("spawning this enemy: " + myEnemy.name);
        myMgr.SendMessage("SpawnEnemy", myEnemy);
    }
}