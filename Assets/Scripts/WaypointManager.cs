using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public static WaypointManager instance { get; private set; }
    [HideInInspector] public List<Transform> initialWaypoints;
    void Awake()
    {
        if (instance == null) instance = this;
        initialWaypoints = new List<Transform>();
        var waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        foreach (GameObject waypoint in waypoints)
        {
            initialWaypoints.Add(waypoint.transform);
        }
    }
}
