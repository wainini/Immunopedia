using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform waypointParent;
    private List<Transform> waypoints = new List<Transform>();
    private Transform nextWaypoint;
    private int index = 0;
    [SerializeField] private float speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        waypointParent = GameObject.FindGameObjectWithTag("Waypoint").transform;
        foreach (Transform waypoint in waypointParent)
        {
            waypoints.Add(waypoint);
        }
        nextWaypoint = waypoints[index];
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, nextWaypoint.position) > 0f)
        {
            transform.position = Vector2.MoveTowards(transform.position, nextWaypoint.position, speed * Time.deltaTime);
        }
        else
        {
            if(index < waypoints.Count - 1)
            {
                index++;
                nextWaypoint = waypoints[index];
            }
            else
            {
                transform.position = nextWaypoint.position;
            }
        }
    }
}
