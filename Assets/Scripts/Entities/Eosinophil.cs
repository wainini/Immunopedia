using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eosinophil : MonoBehaviour, IEntityBehaviour
{
    [SerializeField] private SpriteRenderer healthFill;
    [SerializeField] private SpriteRenderer healthBar;
    [SerializeField] private CircleCollider2D radius;
    private EntityStats stats;

    public ImmuneCell cellData;

    private float currentAtkInterval;
    private GameObject target;
    private Queue<GameObject> enemies = new Queue<GameObject>();
    private void Start()
    {
        stats = GetComponent<EntityStats>();
        stats.SetHealthUI(healthBar, healthFill);
        radius.radius = cellData.atkRadius;
        target = null;
    }

    private void Update()
    {
        WaitForInterval();
        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, stats.movSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, target.transform.position) < 0.5)
            {
                transform.position = this.transform.position;
                if (IsReadyToAttack())
                {
                    Attack();
                    RestoreInterval();
                }
            }
        }
        else
        {
            SetTarget();
        }
        if (IsDead())
        {
            Destroy(gameObject);
        }
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

    public void AddEnemy(GameObject enemy)
    {
        enemies.Enqueue(enemy);
    }

    public void SetTarget()
    {
        target = (enemies.Count != 0) ? enemies.Dequeue() : null;
    }

    public void Attack()
    {
        int newAtk;
        if(target.GetComponent<Parasite>() == null)
        {
            newAtk = stats.atk * 4 / 5;
        }
        else
        {
            newAtk = stats.atk * 3;
            target.GetComponent<Parasite>().AddEosi(this);
            target.GetComponent<Parasite>().ReduceDef();
        }
        target.GetComponent<EntityStats>().TakeDamage(newAtk, gameObject);
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
