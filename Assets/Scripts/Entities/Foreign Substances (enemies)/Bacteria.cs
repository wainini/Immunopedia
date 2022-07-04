using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacteria : MonoBehaviour, IEntityBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private RectTransform healthFill;
    [SerializeField] private RectTransform healthBar;

    private EntityStats stats;
    private Transform nextWaypoint;
    private Queue<Transform> waypoints;

    private float currentAtkInterval;

    private void Start()
    {
        stats = GetComponent<EntityStats>();
        stats.SetHealthUI(healthBar, healthFill);
        waypoints = GetComponent<EnemyWaypoints>().GetWaypoints();
        currentAtkInterval = 0;
    }
    void Update()
    {
        if (waypoints == null && nextWaypoint == null)
        {
            waypoints = GetComponent<EnemyWaypoints>().GetWaypoints();
            return;
        }
        else if(nextWaypoint == null)
        {
            nextWaypoint = waypoints.Dequeue();
        }
        WaitForInterval();
        if (stats.localTarget != null)
        {
            CalculateRotation();
            if(Vector2.Distance(transform.position, stats.localTarget.transform.position) > 0.5f)
            {
                transform.parent.position = Vector2.MoveTowards(transform.position, stats.localTarget.transform.position, stats.movSpeed * Time.deltaTime);
            }
            else
            {
                anim.SetBool("IsMoving", false);
                transform.parent.position = this.transform.position;
                if (IsReadyToAttack())
                {
                    anim.SetBool("IsAttacking", true);
                }
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, nextWaypoint.position) > 0.1f && !anim.GetBool("IsAttacking"))
            {
                transform.parent.position = Vector2.MoveTowards(transform.position, nextWaypoint.position, stats.movSpeed * Time.deltaTime);
                //Rotation and Moving Anim
                anim.SetBool("IsMoving", true);
                CalculateRotation();

            }
            else
            {
                nextWaypoint = waypoints.Dequeue();
            }
        }
        if (IsDead())
        {
            Destroy(transform.parent.gameObject);
        }
    }

    private void CalculateRotation()
    {
        if (transform.position.x < nextWaypoint.transform.position.x)
        {
            transform.rotation = Quaternion.identity;
        }
        else if (transform.position.x > nextWaypoint.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
    }

    public void Attack() //Called using animation event
    {
        stats.localTarget.GetComponent<EntityStats>().TakeDamage(stats.atk, gameObject);
        //play sound or smth, idk
        AudioManager.instance.PlaySound("BacteriaHit", SoundOutput.sfx);
    }

    public void FinishAttackAnim() //Called using animation event
    {
        anim.SetBool("IsAttacking", false);
        RestoreInterval();
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
