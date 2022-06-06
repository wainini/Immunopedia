using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : Entity
{
    [SerializeField] private Transform waypointParent;
    private Queue<Transform> waypoints = new Queue<Transform>();
    private Transform nextWaypoint;

    public EnemyBase()
    {
        foreach (Transform waypoint in waypointParent)
        {
            waypoints.Enqueue(waypoint);
        }
        nextWaypoint = waypoints.Dequeue();
    }

    public void SetWaypointParent(Transform parent)
    {
        waypointParent = parent;
    }

    public void NextWaypoint()
    {
        if(waypoints.Peek() != null)
        {
            nextWaypoint = waypoints.Dequeue();
        }
    }

    public Transform GetWaypoint()
    {
        return (nextWaypoint == null) ? nextWaypoint : null;
    }

    protected override void TakeDamage(int damage, Entity enemy)
    {
        base.TakeDamage(damage, enemy);
        target = enemy;
    }
}
