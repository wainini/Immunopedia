using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform waypointParent;
    private List<Transform> waypoints = new List<Transform>();
    private Transform nextWaypoint;
    private Transform target;

    private int index = 0;
    [SerializeField] private float speed = 2f;
    [SerializeField] private int enemyHealth = 10;
    [SerializeField] private int enemyAtk = 3;
    void Start()
    {
        target = null;
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
        if (target)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            if(Vector2.Distance(transform.position, target.position) < 0.5)
            {
                transform.position = this.transform.position;
                Attack();
            }
        }
        else
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

    private void Attack()
    {
        //attack
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;
        if(enemyHealth <= 0)
        {
            enemyHealth = 0;
            Destroy(this);
        }
    }
}
