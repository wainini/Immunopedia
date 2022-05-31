using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neutrofil : MonoBehaviour
{
    public ImmuneCell immuneCell;

    private int currentHealth; 

    private Queue<Transform> enemies;

    [SerializeField] private SpriteRenderer healthBar;
    [SerializeField] private SpriteRenderer healthFill;

    private double atkInterval = 1f;
    private double currentAtkInterval;
    
    private void Start()
    {
        currentAtkInterval = atkInterval;
        currentHealth = immuneCell.health;
        healthBar.size = healthFill.size;
        //Debug.Log(this.immuneCell.health + " " + this.immuneCell.atk);
        //health = immuneCell.health;
        //atk = immuneCell.atk;
        enemies = new Queue<Transform>();
    }

    private void Update()
    {
        currentAtkInterval -= Time.unscaledDeltaTime;
        if(enemies.Count != 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemies.Peek().position, immuneCell.movSpd * Time.deltaTime);
            if(Vector2.Distance(transform.position, enemies.Peek().position) < 0.5)
            {
                transform.position = this.transform.position;
                Enemy enemyObj = enemies.Peek().GetComponent<Enemy>();
                enemyObj.SetTarget(this.transform);
                if(currentAtkInterval <= 0)
                {
                    Attack(enemyObj);
                    currentAtkInterval = atkInterval;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Transform enemyLocation = collision.gameObject.transform;
            enemies.Enqueue(enemyLocation);
            Debug.Log(enemies.Peek().position);
        }
    }

    private void Attack(Enemy enemy)
    {
        enemy.TakeDamage(immuneCell.atk);
        if (enemy.IsDead())
        {
            enemies.Dequeue();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Destroy(this.gameObject);
        }
        healthFill.size = new Vector2(currentHealth * healthBar.size.x / immuneCell.health, healthBar.size.y);
    }

    public bool IsDead()
    {
        return (currentHealth <= 0) ? true : false;
    }
}
