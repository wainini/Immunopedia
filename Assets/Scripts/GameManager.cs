using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int lives;
    [SerializeField] private float spawnCooldown;
    
    public GameObject neutrofilPrefab;
    public GameObject enemyPrefab;

    public Queue<GameObject> wounds = new Queue<GameObject>();

    public Transform initialEnemySpawn;

    
    private void Start()
    {
        var woundArr = GameObject.FindGameObjectsWithTag("Wound");
        wounds = new Queue<GameObject>(woundArr);
        Debug.Log("You have " + lives + " lives");
        //StartCoroutine("SpawnEnemies");
    }

    void Update()
    {
        if(wounds.Count != 0)
        {
            if (wounds.Peek().GetComponent<Wound>().IsWoundClosed())
            {
                wounds.Dequeue();
            }
        }
        else
        {
            Win();
        }
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            Instantiate(enemyPrefab, initialEnemySpawn.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnCooldown);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            lives--;
            Destroy(collision.gameObject);
            Debug.Log("You have " + lives + " lives left");
        }
        if (lives <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        //game over
        Debug.Log("You Died");
        Time.timeScale = 0;
    }

    private void Win()
    {
        //win
        Debug.Log("You Win");
        Time.timeScale = 0;
    }
}
