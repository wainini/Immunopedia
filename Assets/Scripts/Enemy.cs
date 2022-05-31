using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform waypointParent;
    private List<Transform> waypoints = new List<Transform>();
    private Transform nextWaypoint;
    private Transform target;

    private int currentHealth;

    private int index = 0;
    [SerializeField] private float speed = 2f;
    [SerializeField] private int enemyHealth = 10;
    [SerializeField] private int enemyAtk = 3;
    [SerializeField] private SpriteRenderer healthBar;
    [SerializeField] private SpriteRenderer healthFill;

    private double atkInterval = 1f;
    private double currentAtkInterval;
    void Start()
    {
        currentAtkInterval = atkInterval;
        currentHealth = enemyHealth;
        healthBar.size = healthFill.size;
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
        currentAtkInterval -= Time.unscaledDeltaTime;
        if (target)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            if(Vector2.Distance(transform.position, target.position) < 0.5)
            {
                transform.position = this.transform.position;
                if(currentAtkInterval <= 0)
                {
                    Attack();
                    currentAtkInterval = atkInterval;
                }
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
        Neutrofil neutrofil = target.GetComponent<Neutrofil>();
        neutrofil.TakeDamage(enemyAtk);
        if (neutrofil.IsDead())
        {
            target = null;
        }
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            //this.enabled = false;
            Destroy(this.gameObject);
        }
        healthFill.size = new Vector2(currentHealth * healthBar.size.x / enemyHealth, healthBar.size.y);
    }

    public bool IsDead()
    {
        return (currentHealth <= 0) ? true : false; 
    }
}
