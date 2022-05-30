using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject neutrofilPrefab;
    public GameObject enemyPrefab;

    public Transform initialEnemySpawn;
    private void Start()
    {
        StartCoroutine("SpawnEnemies");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Instantiate(neutrofilPrefab);
        }
    }

    IEnumerator SpawnEnemies()
    {

        for (int i = 0; i < 5; i++)
        {
            Instantiate(enemyPrefab, initialEnemySpawn.position, Quaternion.identity);
            yield return new WaitForSeconds(5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
    }
}
