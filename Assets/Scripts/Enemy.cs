using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>();
    private Transform nextWaypoint;
    private int index = 0;
    [SerializeField] private float speed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        nextWaypoint = waypoints[index];
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, nextWaypoint.position) > 0.5)
        {
            transform.position = Vector2.MoveTowards(transform.position, nextWaypoint.position, speed * Time.deltaTime);
        }
        else
        {
            if(index < waypoints.Count)
            {
                index++;
            }
            nextWaypoint = waypoints[index];
        }
    }
}
