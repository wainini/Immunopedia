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

    private RectTransform healthBar;
    private RectTransform healthFill;
    private List<SpriteRenderer> sprites;
    private void Awake()
    {
        currentHealth = baseStat.maxHealth;
        atk = baseStat.atk;
        defense = baseStat.defense;
        atkInterval = baseStat.atkInterval;
        movSpeed = baseStat.movSpeed;
        blockCount = baseStat.blockCount;
        localTarget = null;
        sprites = new List<SpriteRenderer>();
        foreach (Transform sprite in gameObject.transform)
        {
            if(sprite.gameObject.GetComponent<SpriteRenderer>() != null)
                sprites.Add(sprite.GetComponent<SpriteRenderer>());
        }
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
        if(damage != 0)
        {
            StartCoroutine(ColorChange());
            if (damage > defense)
            {
                currentHealth -= (damage - defense);
                UpdateHealthUI(currentHealth);
            }
        }
        if(!localTarget) localTarget = target;
    }

    public void SetHealthUI(RectTransform bar, RectTransform fill)
    {
        healthBar = bar;
        healthFill = fill;
        healthFill.sizeDelta = healthBar.sizeDelta;
    }

    public void UpdateHealthUI(int currentHealth)
    {
        healthFill.sizeDelta = new Vector2(((float)currentHealth / (float)baseStat.maxHealth) * healthBar.sizeDelta.x, healthFill.sizeDelta.y);
    }

    IEnumerator ColorChange()
    {
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.color = Color.red;
        }

        yield return new WaitForSeconds(0.3f);
        
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.color = Color.white;
        }
    }
}
