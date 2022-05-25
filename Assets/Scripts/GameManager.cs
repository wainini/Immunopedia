using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject neutrofilPrefab;
    public GameObject enemyPrefab;
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
            Instantiate(enemyPrefab, new Vector2(-5f, 0f), Quaternion.identity);
            yield return new WaitForSeconds(5f);
        }
    }
}
