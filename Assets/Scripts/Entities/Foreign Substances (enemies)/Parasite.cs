using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parasite : MonoBehaviour, IEntityBehaviour
{
    [SerializeField] private SpriteRenderer healthFill;
    [SerializeField] private SpriteRenderer healthBar;

    private EntityStats stats;
    private Transform nextWaypoint;
    private Queue<Transform> waypoints;
    private List<Eosinophil> blockingEosi;
    private int eosiAmount;
    private void Start()
    {
        blockingEosi = new List<Eosinophil>();
        stats = GetComponent<EntityStats>();
        stats.SetHealthUI(healthBar, healthFill);
        waypoints = GetComponent<EnemyWaypoints>().GetWaypoints();
        nextWaypoint = waypoints.Dequeue();
    }
    void Update()
    {
        if (Vector2.Distance(transform.parent.position, nextWaypoint.position) > 0.1f)
        {
            transform.parent.position = Vector2.MoveTowards(transform.parent.position, nextWaypoint.position, stats.movSpeed * Time.deltaTime);
        }
        else
        {
            nextWaypoint = waypoints.Dequeue();
        }

        if (IsDead())
        {
            Destroy(transform.parent.gameObject);
        }
        if(blockingEosi.Count != 0)
        {
            ReduceStats(blockingEosi[0].defenseReduction, blockingEosi[0].movSpeedReduction);
            
        }
        else
        {
            stats.RestoreStats();
        }
    }

    private void ReduceStats(float defReductionPercentage, float spdReductionPercentage)
    {
        eosiAmount = blockingEosi.Count;
        stats.ReduceStats(eosiAmount, defReductionPercentage, spdReductionPercentage);
    }

    public void AddEosi(Eosinophil eosi)
    {
        if(!blockingEosi.Contains(eosi)) blockingEosi.Add(eosi);
    }

    public void Attack()
    {
        //This one doesn't attack
    }

    public bool IsDead()
    {
        return (stats.currentHealth <= 0) ? true : false;
    }

    public bool IsReadyToAttack()
    {
        //This one doesn't attack
        return false;
    }

    public void WaitForInterval()
    {
        //This one doesn't attack
    }

    public void RestoreInterval()
    {
        //This one doesn't attack
    }
}
