using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Eosinophil : MonoBehaviour, IEntityBehaviour
{
    [SerializeField] private Animator anim;
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
        radius.radius = cellData.atkRadius / transform.localScale.x; //karena scale badannya gak 1
        target = null;
        isAttacking = false;
    }

    private void Update()
    {
        ClearDeadEnemies();
        if (currentAtkInterval > 0) WaitForInterval();
        if (target != null)
        {
            if (!isAttacking) CheckPriority();
            if(!anim.GetBool("IsAttacking") && anim.GetBool("IsMoving"))
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

            if (Vector2.Distance(transform.position, target.transform.position) < 1f)
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

    private void ClearDeadEnemies()
    {
        enemies = enemies.Where(i => i != null).ToList();
    }

    private void CheckPriority()
    {
        float minDistance = float.PositiveInfinity;
        GameObject tempTarget = target;
        foreach (GameObject go in enemies)
        {
            if (go.GetComponent<Parasite>() != null)
            {
                if (Vector2.Distance(go.transform.position, gameObject.transform.position) < minDistance)
                {
                    minDistance = Vector2.Distance(go.transform.position, gameObject.transform.position);
                    tempTarget = go;
                }
            }
        }
        target = tempTarget;
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
        }
        target = tempTarget;
    }

    public void Attack() //Called using animation event
    {
        if (target == null) return;
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
        //play sound or smth, idk
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
