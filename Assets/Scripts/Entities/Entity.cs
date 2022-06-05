using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : ScriptableObject
{
    public float movSpeed;
    
    [SerializeField] private int maxHealth;
    [SerializeField] private int atk;
    [SerializeField] private float atkInterval;

    [SerializeField] private SpriteRenderer healthBar;
    [SerializeField] private SpriteRenderer healthFill;


    private Entity target;
    private int currentHealth;
    private float currentAtkInterval;
    public Entity()
    {
        healthBar.size = healthFill.size;
        currentAtkInterval = 0;
        currentHealth = maxHealth;
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthFill.size = new Vector2(currentHealth * healthBar.size.x / maxHealth, healthBar.size.y);
    }

    public void Attack()
    {
        target.TakeDamage(atk);
    }

    public bool IsDead()
    {
        return (currentHealth <= 0) ? true : false;
    }

    public bool IsReadyToAttack()
    {
        return (currentAtkInterval <= 0) ? true : false;
    }

    public void WaitForInterval()
    {
        currentAtkInterval -= Time.unscaledDeltaTime;
    }

    public void RestoreInterval()
    {
        currentAtkInterval = atkInterval;
    }

    public void SetTarget(Entity target)
    {
        this.target = target;
    }
}
