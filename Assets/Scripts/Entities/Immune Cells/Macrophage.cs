using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Macrophage : MonoBehaviour, IEntityBehaviour
{
    [SerializeField] private SpriteRenderer healthFill;
    [SerializeField] private SpriteRenderer healthBar;
    [SerializeField] private CircleCollider2D radius;
    private EntityStats stats;

    public ImmuneCell cellData;

    private float currentAtkInterval;
    private bool isAttacking;
    private GameObject target;
    private List<GameObject> enemies = new List<GameObject>();
    private List<GameObject> blockedEnemies = new List<GameObject>();
    private void Start()
    {
        stats = GetComponent<EntityStats>();
        stats.SetHealthUI(healthBar, healthFill);
        radius.radius = cellData.atkRadius;
        target = null;
        isAttacking = false;
        currentAtkInterval = 0;
    }
    private void Update()
    {
        if(currentAtkInterval > 0) WaitForInterval();
        if (target != null)
        {
            if(blockedEnemies.Count < stats.blockCount)
            {
                Taunt();
                CheckPriority();
            }
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


    private void Taunt()
    {
        int enemyCount = enemies.Count;
        for (int i = 0; i < enemyCount - 1; i++)
        {
            for (int j = i; j < enemyCount; j++)
            {
                if (Vector2.Distance(enemies[i].transform.position, gameObject.transform.position) > Vector2.Distance(enemies[j].transform.position, gameObject.transform.position))
                {
                    GameObject temp = enemies[i];
                    enemies[i] = enemies[j];
                    enemies[j] = temp;
                }
            }
        }
        int tauntAmount = (enemyCount < stats.blockCount) ? enemyCount : stats.blockCount;
        for (int i = 0; i < tauntAmount; i++)
        {
            blockedEnemies.Add(enemies[i]);
        }
    }

    private void ClearDeadEnemies()
    {
        enemies = enemies.Where(i => i != null).ToList();
        blockedEnemies = blockedEnemies.Where(i => i != null).ToList();
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
        target.GetComponent<EntityStats>().TakeDamage(stats.atk, gameObject);
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
