using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wound : MonoBehaviour
{
    [SerializeField] private SpriteRenderer woundSr;
    [SerializeField] private List<Sprite> woundSprites;
    public int plateletNeeded;

    public float intervalBetweenSpawn;
    public float initialDelay;
    public List<GameObject> enemies;

    private int currentPlateletCount;
    private bool isWoundClosed;
    private List<Transform> initialWaypoints;
    private GameObject tutorialCanvas;

    bool isTutorialActive = false;

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
            if(waypoint.GetComponent<SpriteRenderer>() == null)
                initialWaypoints.Add(waypoint);
        }
        currentPlateletCount = 0;
        isWoundClosed = false;
        tutorialCanvas = GameObject.FindGameObjectWithTag("Tutorial");
        if (tutorialCanvas != null) isTutorialActive = true;
        else StartCoroutine(Spawn());
    }

    private void Update()
    {
        if (tutorialCanvas == null && isTutorialActive) // tutorial selesai, mulai gamenya
        {
            StartCoroutine(Spawn());
            isTutorialActive = false;
        }
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
            enemy.GetComponentInChildren<EnemyWaypoints>().SetWaypointParent(initWaypoint);
            yield return new WaitForSeconds(intervalBetweenSpawn);
        }

    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Platelet")
    //    {
    //        currentPlateletCount++;
    //        if (currentPlateletCount >= plateletNeeded)
    //        {
    //            isWoundClosed = true;
    //            GameManager.instance.CloseWound(this.gameObject);
    //        }
    //    }
    //}

    public void AddPlatelet()
    {
        currentPlateletCount++;
        woundSr.sprite = woundSprites[currentPlateletCount - 1];
        if (currentPlateletCount == plateletNeeded)
        {
            isWoundClosed = true;
            GameManager.instance.CloseWound(this.gameObject);
        }
    }
}
