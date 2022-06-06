using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyBase enemyBase;

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
            if(Vector2.Distance(transform.position, enemyBase.GetWaypoint().position) > 0f)
            {
                transform.position = Vector2.MoveTowards(transform.position, enemyBase.GetWaypoint().position, enemyBase.movSpeed * Time.deltaTime);
            }
            else
            {
                enemyBase.NextWaypoint();
            }
        }
    }
}
