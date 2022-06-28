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
    private List<Transform> initialWaypoints;

    private Wound()
    {
        enemies = new List<GameObject>();
        initialWaypoints = new List<Transform>();
    }

    private void Start()
    {
        var waypoints = GetComponentInChildren<Transform>();
        foreach (Transform waypoint in waypoints)
        {
            initialWaypoints.Add(waypoint);
        }
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
            int waypointCount = initialWaypoints.Count;

            if (waypointCount <= 1)
                initWaypoint = initialWaypoints[0];
            else
                initWaypoint = initialWaypoints[Random.Range(0, waypointCount)];
            Debug.Log(enemies.Count);
            GameObject enemy = Instantiate(enemies[index], gameObject.transform);
            enemy.GetComponent<EnemyWaypoints>().SetWaypointParent(initWaypoint);
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
