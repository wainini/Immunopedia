using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaypoints : MonoBehaviour
{
    private Transform waypointParent;
    private Queue<Transform> waypoints = new Queue<Transform>();

    public void SetWaypointParent(Transform parent)
    {
        waypointParent = parent;
        SetWaypoints();
    }

    private void SetWaypoints()
    {
        waypoints = new Queue<Transform>();
        foreach (Transform waypoint in waypointParent)
        {
            waypoints.Enqueue(waypoint);
        }
    }

    public Queue<Transform> GetWaypoints()
    {
        return (waypoints.Count != 0) ? waypoints : null;
    }
}
