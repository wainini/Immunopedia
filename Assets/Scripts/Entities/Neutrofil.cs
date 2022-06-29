using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Neutrofil : MonoBehaviour, IEntityBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer healthFill;
    [SerializeField] private SpriteRenderer healthBar;
    [SerializeField] private CircleCollider2D radius;
    private EntityStats stats;

    public ImmuneCell cellData;
 
    private float currentAtkInterval;
    private bool isAttacking;
    private GameObject target;
    private List<GameObject> enemies = new List<GameObject>();
    private void Start()
    {
        stats = GetComponent<EntityStats>();
        stats.SetHealthUI(healthBar, healthFill);
        radius.radius = cellData.atkRadius;
        target = null;
        isAttacking = false;
    }

    private void Update()
    {
        WaitForInterval();
        if (target != null)
        {
            if(!isAttacking) CheckPriority();
            transform.parent.position = Vector2.MoveTowards(transform.position, target.transform.position, stats.movSpeed * Time.deltaTime);

            anim.SetBool("IsMoving", true);
            if(transform.position.x <= target.transform.position.x)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180f, 0));
            }
            else
            {
                transform.rotation = Quaternion.identity;
            }

            if (Vector2.Distance(transform.position, target.transform.position) < 0.5)
            {
                transform.position = this.transform.position;
                anim.SetBool("IsMoving", false);
                if (IsReadyToAttack())
                {
                    isAttacking = true;
                    anim.SetBool("IsAttacking", true);
                }
            }
        }
        else
        {
            SetTarget();
            isAttacking = false;
        }
        if (IsDead())
        {
            Destroy(gameObject);
        }
        ClearDeadEnemies();
        Debug.Log("Current enemy count: " + enemies.Count);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (!enemies.Contains(collision.gameObject))
            {
                AddEnemy(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (enemies.Contains(collision.gameObject))
            {
                enemies.Remove(collision.gameObject);
            }
        }
    }

    private void ClearDeadEnemies()
    {
        enemies = enemies.Where(i => i != null).ToList();
    }
    private void CheckPriority()
    {
        foreach (GameObject go in enemies)
        {
            if (go.GetComponent<Bacteria>() != null)
            {
                target = go;
                return;
            }
        }
    }

    public void AddEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }

    public void SetTarget()
    {
        target = (enemies.Count != 0) ? enemies[0] : null;
    }

    public void Attack()
    {
        Debug.Log("attack");
        target.GetComponent<EntityStats>().TakeDamage(stats.atk, gameObject);
    }

    public void FinishAttack()
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
        currentAtkInterval = (currentAtkInterval > 0) ? currentAtkInterval - Time.unscaledDeltaTime : 0;
    }

    public void RestoreInterval()
    {
        currentAtkInterval = stats.atkInterval;
    }
}
