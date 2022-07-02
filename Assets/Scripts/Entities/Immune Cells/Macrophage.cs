using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Macrophage : MonoBehaviour, IEntityBehaviour
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
        ClearDeadEnemies();
        if (currentAtkInterval > 0) WaitForInterval();
        if (target != null)
        {
            if(blockedEnemies.Count < stats.blockCount || !isAttacking)
            {
                CheckPriority();
                Taunt();
            }
            //if(!anim.SetBool("IsAttacking"))
            transform.parent.position = Vector2.MoveTowards(transform.position, target.transform.position, stats.movSpeed * Time.deltaTime);
            //Rotation and Moving Anim
            anim.SetBool("IsMoving", true);
            if (transform.position.x > target.transform.position.x)
            {
                transform.rotation = Quaternion.identity;
            }
            else if (transform.position.x < target.transform.position.x)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            if (Vector2.Distance(transform.position, target.transform.position) < 0.5)
            {
                anim.SetBool("IsMoving", false);
                transform.parent.position = this.transform.position;
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
            Destroy(transform.parent.gameObject);
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
        foreach (GameObject enemy in blockedEnemies)
        {
            enemy.GetComponent<EntityStats>().TakeDamage(0, this.gameObject);
        }
    }

    private void ClearDeadEnemies()
    {
        enemies = enemies.Where(i => i != null).ToList();
        blockedEnemies = blockedEnemies.Where(i => i != null).ToList();
    }
    private void CheckPriority()
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
        if(enemies.Count > stats.blockCount)
        {
            Debug.Log("there are more than 1 enemies detected");
            foreach (GameObject enemy in enemies)
            {
                if(enemy.GetComponent<Bacteria>() != null && !blockedEnemies.Contains(enemy))
                {
                    blockedEnemies.Add(enemy);
                }
                if (blockedEnemies.Count >= stats.blockCount) break;
            }
        }
        else
        {
            foreach (GameObject enemy in enemies)
            {
                if (!blockedEnemies.Contains(enemy)) blockedEnemies.Add(enemy);

            }
        }
        target = blockedEnemies[0];
    }

    public void AddEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }

    public void SetTarget()
    {
        GameObject tempTarget = null;
        float minDistance = float.PositiveInfinity;
        foreach (GameObject enemy in enemies)
        {
            if (Vector2.Distance(enemy.transform.position, gameObject.transform.position) < minDistance)
            {
                minDistance = Vector2.Distance(enemy.transform.position, gameObject.transform.position);
                tempTarget = enemy;
            }
            print("distance: " + minDistance + "\nenemy pos: " + enemy.name);
        }
        target = tempTarget;
    }

    public void Attack() //Called using animation event
    {
        if (target == null) return;
        target.GetComponent<EntityStats>().TakeDamage(stats.atk, gameObject);
        AudioManager.instance.PlaySound("MacrophageHit", SoundOutput.sfx);
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
        currentAtkInterval = (currentAtkInterval > 0) ? currentAtkInterval - Time.unscaledDeltaTime : 0;
    }

    public void RestoreInterval()
    {
        currentAtkInterval = stats.atkInterval;
    }
}
