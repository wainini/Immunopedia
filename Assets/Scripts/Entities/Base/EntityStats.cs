using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    [SerializeField] private Entity baseStat;
    public int currentHealth { get; private set; }
    public int atk { get; private set; }
    public int defense { get; private set; }
    public float atkInterval { get; private set; }
    public float movSpeed { get; private set; }
    public int blockCount { get; private set; }
    public GameObject localTarget { get; private set; }

    [Header("Fill in this if it is enemy")]
    public int hpSealReduction;

    private SpriteRenderer healthBar;
    private SpriteRenderer healthFill;
    private void Awake()
    {
        currentHealth = baseStat.maxHealth;
        atk = baseStat.atk;
        defense = baseStat.defense;
        atkInterval = baseStat.atkInterval;
        movSpeed = baseStat.movSpeed;
        blockCount = baseStat.blockCount;
        localTarget = null;
    }

    public void RestoreStats()
    {
        atk = baseStat.atk;
        defense = baseStat.defense;
        movSpeed = baseStat.movSpeed;
    }

    public void ReduceStats(int eosiAmount, float defReductionPercentage, float spdReductionPercentage)
    {
        float reduction = (float)eosiAmount * defReductionPercentage;
        defense = baseStat.defense - (int)(baseStat.defense * reduction);
        defense = (defense <= 0) ? 0 : defense;

        reduction = (float)eosiAmount * spdReductionPercentage;
        movSpeed = baseStat.movSpeed - (baseStat.movSpeed * reduction);
        movSpeed = (movSpeed <= 0) ? 0 : movSpeed;
    }

    public void TakeDamage(int damage, GameObject target)
    {
        if (damage > defense)
        {
            currentHealth -= (damage - defense);
        }
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