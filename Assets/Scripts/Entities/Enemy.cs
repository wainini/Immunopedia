//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Enemy : MonoBehaviour, IEntityBehaviour
//{
//    [SerializeField] private EnemyBase enemyBase;

//    private Transform nextWaypoint;
//    private Queue<Transform> waypoints;
//    private EntityStats stats;

//    private float currentAtkInterval;

//    private GameObject target;

//    [SerializeField] private SpriteRenderer healthFill;
//    [SerializeField] private SpriteRenderer healthBar;

//    private void Start()
//    {
//        enemyBase.SetHealthUI(healthBar, healthFill);
//        stats = GetComponent<EntityStats>();
//        waypoints = enemyBase.GetWaypoints();
//        nextWaypoint = waypoints.Dequeue();
//        currentAtkInterval = 0;
//    }
//    void Update()
//    {
//        if(currentAtkInterval >= 0)
//        {
//            WaitForInterval();
//        }
//        else
//        {
//            if (target)
//            {
//                transform.position = this.transform.position;
//                if (IsReadyToAttack())
//                {
//                    Attack();
//                    RestoreInterval();
//                }
//            }
//            else
//            {
//                if (Vector2.Distance(transform.position, nextWaypoint.position) > 0.1f)
//                {
//                    transform.position = Vector2.MoveTowards(transform.position, nextWaypoint.position, enemyBase.movSpeed * Time.deltaTime);
//                }
//                else
//                {
//                    nextWaypoint = waypoints.Dequeue();
//                }
//            }
//            if (IsDead())
//            {
//                Destroy(gameObject);
//            }
//        }
//    }


//    public void SetWaypoint(Transform parent)
//    {
//        enemyBase.SetWaypointParent(parent);
//    }

//    public void Attack()
//    {
//        target.GetComponent<EntityStats>().TakeDamage(stats.atk);
//    }

//    public bool IsDead()
//    {
//        return (stats.currentHealth <= 0) ? true : false;
//    }

//    public bool IsReadyToAttack()
//    {
//        return (currentAtkInterval <= 0) ? true : false;
//    }

//    public void WaitForInterval()
//    {
//        currentAtkInterval -= Time.unscaledDeltaTime;
//    }

//    public void RestoreInterval()
//    {
//        currentAtkInterval = stats.atkInterval;
//    }
//}
