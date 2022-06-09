using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyBase enemyBase;

    private Transform nextWaypoint;
    private Queue<Transform> waypoints;

    private void Start()
    {
        waypoints = enemyBase.GetWaypoints();
        nextWaypoint = waypoints.Dequeue();
    }
    void Update()
    {
        enemyBase.WaitForInterval();
        if (enemyBase.target)
        {
            transform.position = this.transform.position;
            if(enemyBase.IsReadyToAttack())
            {
                enemyBase.Attack();
                enemyBase.RestoreInterval();    
            }
        }
        else
        {
            if(Vector2.Distance(transform.position, nextWaypoint.position) > 0f)
            {
                transform.position = Vector2.MoveTowards(transform.position, nextWaypoint.position, enemyBase.movSpeed * Time.deltaTime);
            }
            else
            {
                nextWaypoint = waypoints.Dequeue();
            }
        }
    }


    public void SetWaypoint(Transform parent)
    {
        enemyBase.SetWaypointParent(parent);
    }
}
