using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wound : MonoBehaviour
{
    [SerializeField] private int plateletNeeded;

    public float intervalBetweenSpawn;
    public float initialDelay;
    public List<GameObject> enemies;

    private int currentPlateletCount;
    private bool isWoundClosed;

    private Wound()
    {
        enemies = new List<GameObject>();
    }
    private void Start()
    {
        currentPlateletCount = 0;
        isWoundClosed = false;
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().SetWaypoint(gameObject.transform);
        }
        StartCoroutine(Spawn());
    }

    private void Update()
    {
        if(currentPlateletCount >= plateletNeeded)
        {
            isWoundClosed = true;
        }
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(initialDelay);
        while (!isWoundClosed)
        {
            int index;
            if(enemies.Count == 1)
            {
                index = 0;
            }
            else
            {
                index = Random.Range(0, enemies.Count);
            }
            GameObject enemy = Instantiate(enemies[index], gameObject.transform);
            yield return new WaitForSeconds(intervalBetweenSpawn);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platelet")
        {
            Debug.Log("Closing wound");
            currentPlateletCount++;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.name == "Platelet")
        {
            currentPlateletCount++;
        }
    }

    public bool IsWoundClosed()
    {
        return isWoundClosed;
    }
}
