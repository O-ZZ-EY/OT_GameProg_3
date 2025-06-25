using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;
    public float enemySpawnRange = 10f;
    public GameObject[] enemies;
    public List<GameObject> enemiesList;
    

    [Header("Timer Vars")]
    public TMP_Text Timer_Text;
    public float CurrentTimer;
    public float TimerInterval;

    [Header("Game Over")]
    public GameObject GameOverScreen;
    public TMP_Text GameOverText;

    void Start()
    {
        CurrentTimer = TimerInterval;
        instance = this;
        //player = Movementv2.instance.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //int index = Random.Range(0, enemies.Length - 1);
        //SpawnEnemy(enemies[index]);

        CurrentTimer -= Time.deltaTime;
        Timer_Text.text = "Timer: " + Mathf.CeilToInt(CurrentTimer).ToString();
        if (CurrentTimer <= 0f)
        {

            CurrentTimer = 0f;
            EndGame();

        }
    }

    void SpawnEnemy(GameObject prefab)
    {
        Vector3 position;

        position = Random.insideUnitSphere * enemySpawnRange;
        position.z = 0f;

        enemiesList.Add(Instantiate(prefab, position, Quaternion.identity));
    }

    public void EndGame()
    {
        foreach (GameObject e in enemiesList)
        {
            Destroy(e);
            //e.SetActive(false);
        }
        Destroy(player);

        GameOverScreen.SetActive(true);

        //float finalScore = PlayerScript.instance.GetScore();
        //GameOverText.text = "Game Over\nFinal Score: " + finalScore.ToString();

    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    
   
}
