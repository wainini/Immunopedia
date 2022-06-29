using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacteria : MonoBehaviour, IEntityBehaviour
{
    [SerializeField] private SpriteRenderer healthFill;
    [SerializeField] private SpriteRenderer healthBar;

    private EntityStats stats;
    private Transform nextWaypoint;
    private Queue<Transform> waypoints;

    private float currentAtkInterval;

    private void Start()
    {
        stats = GetComponent<EntityStats>();
        stats.SetHealthUI(healthBar, healthFill);
        waypoints = GetComponent<EnemyWaypoints>().GetWaypoints();
        nextWaypoint = waypoints.Dequeue();
        currentAtkInterval = 0;
    }
    void Update()
    {
        WaitForInterval();
        if (stats.localTarget != null)
        {
            transform.position = this.transform.position;
            if (IsReadyToAttack())
            {
                Attack();
                RestoreInterval();
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, nextWaypoint.position) > 0.1f)
            {
                transform.position = Vector2.MoveTowards(transform.position, nextWaypoint.position, stats.movSpeed * Time.deltaTime);
            }
            else
            {
                nextWaypoint = waypoints.Dequeue();
            }
        }
        if (IsDead())
        {
            Debug.Log(gameObject.name + " is ded");
            Destroy(gameObject);
        }
    }

    public void Attack()
    {
        stats.localTarget.GetComponent<EntityStats>().TakeDamage(stats.atk, gameObject);
    }

    public bool IsDead()
    {
        return (stats.currentHealth <= 0) ? true : false;
    }

    public bool IsReadyToAttack()
    {
        return (currentAtkInterval <= 0) ? true : false;
    }

    public void WaitForInterval()
    {
        currentAtkInterval = (currentAtkInterval > 0)? currentAtkInterval - Time.unscaledDeltaTime : 0;
    }

    public void RestoreInterval()
    {
        currentAtkInterval = stats.atkInterval;
    }
}
