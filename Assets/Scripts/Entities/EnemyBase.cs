//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[CreateAssetMenu(fileName = "New Enemy", menuName = "Entity/Enemy")]
//public class EnemyBase : Entity
//{
//    private Transform waypointParent;
//    private Queue<Transform> waypoints;

//    public void SetWaypointParent(Transform parent)
//    {
//        waypointParent = parent;
//        SetWaypoints();
//    }

//    private void SetWaypoints()
//    {
//        waypoints = new Queue<Transform>();
//        foreach (Transform waypoint in waypointParent)
//        {
//            waypoints.Enqueue(waypoint);
//        }
//    }

//    public Queue<Transform> GetWaypoints()
//    {
//        return (waypoints.Count != 0) ? new Queue<Transform>(waypoints) : null;
//    }
//}
