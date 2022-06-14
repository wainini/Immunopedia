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
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(initialDelay);
        while (!isWoundClosed)
        {
            int index;
            
            if (enemies.Count == 1) 
                index = 0;
            else 
                index = Random.Range(0, enemies.Count);

            Transform initWaypoint;
            int waypointCount = WaypointManager.instance.initialWaypoints.Count;

            if (waypointCount <= 1)
                initWaypoint = WaypointManager.instance.initialWaypoints[0];
            else
                initWaypoint = WaypointManager.instance.initialWaypoints[Random.Range(0, waypointCount)];

            enemies[index].GetComponent<Enemy>().SetWaypoint(initWaypoint);
            GameObject enemy = Instantiate(enemies[index], gameObject.transform);
            yield return new WaitForSeconds(intervalBetweenSpawn);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platelet")
        {
            currentPlateletCount++;
            if (currentPlateletCount >= plateletNeeded)
            {
                isWoundClosed = true;
                GameManager.instance.CloseWound(this.gameObject);
            }
        }
    }
}
