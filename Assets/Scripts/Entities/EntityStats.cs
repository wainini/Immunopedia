using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    [SerializeField] private Entity baseStat;
    public int currentHealth { get; private set; }
    public int atk { get; private set; }
    public float atkInterval { get; private set; }
    public float movSpeed { get; private set; }
    public GameObject localTarget { get; private set; }

    private SpriteRenderer healthBar;
    private SpriteRenderer healthFill;
    private void Awake()
    {
        currentHealth = baseStat.maxHealth;
        atk = baseStat.atk;
        atkInterval = baseStat.atkInterval;
        movSpeed = baseStat.movSpeed;
        localTarget = null;
    }

    public void TakeDamage(int damage, GameObject target)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " current helth: " + currentHealth);
        localTarget = target;
        UpdateHealthUI(currentHealth);
    }

    public void SetHealthUI(SpriteRenderer bar, SpriteRenderer fill)
    {
        healthBar = bar;
        healthFill = fill;
        healthFill.size = healthBar.size;
    }

    public void UpdateHealthUI(int currentHealth)
    {
        healthFill.size = new Vector2(((float)currentHealth / (float)baseStat.maxHealth) * healthBar.size.x, healthFill.size.y);
    }
}
