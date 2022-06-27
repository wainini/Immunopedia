using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eosinophil : MonoBehaviour, IEntityBehaviour
{
    [SerializeField] private SpriteRenderer healthFill;
    [SerializeField] private SpriteRenderer healthBar;
    [SerializeField] private CircleCollider2D radius;
    public ImmuneCell cellData;
    [Header("Fill in value in decimals")]
    public float defenseReduction;
    public float movSpeedReduction;

    private float currentAtkInterval;
    private bool isAttacking;
    private EntityStats stats;
    private GameObject target;
    [SerializeField]private List<GameObject> enemies = new List<GameObject>();
    
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
            if (!isAttacking) CheckPriority();
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, stats.movSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, target.transform.position) < 0.5)
            {
                transform.position = this.transform.position;
                if (IsReadyToAttack())
                {
                    isAttacking = true;
                    Attack();
                    RestoreInterval();
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

    private void ClearDeadEnemies()
    {
        foreach (GameObject go in enemies)
        {
            if (go == null) enemies.Remove(go);
        }
    }

    private void CheckPriority()
    {
        foreach (GameObject go in enemies)
        {
            if(go.GetComponent<Parasite>() != null)
            {
                target = go;
                //enemies.Remove(go);
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
        //enemies.RemoveAt(0);
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
            Parasite parasite = target.GetComponent<Parasite>();
            newAtk = stats.atk * 3;
            parasite.AddEosi(this);
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
